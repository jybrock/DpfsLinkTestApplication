using System.Net;
using System.Text;
using System;
using System.IO;
using System.Windows.Forms;
using System.Collections;

namespace DpfsLinkTestApplication {
    public class IPHeader {

        #region IP Header Fields
        private byte byVersionAndHeaderLength;      // Eight bits for version and header length
        private byte byDifferentiatedServices;      // Eight bits for differentiated services (TOS)
        private ushort usTotalLength;               // Sixteen bits for total length of the datagram (header + message)
        private ushort usIdentification;            // Sixteen bits for identification
        private ushort usFlagsAndOffset;            // Eight bits for flags and fragmentation offset
        private byte byTTL;                         // Eight bits for TTL (Time To Live)
        private byte byProtocol;                    // Eight bits for the underlying protocol
        private short sChecksum;                    // Sixteen bits containing the checksum of the header
        //    (checksum can be negative so taken as short)
        private uint uiSourceIPAddress;             // Thirty two bit source IP Address
        private uint uiDestinationIPAddress;        // Thirty two bit destination IP Address
        #endregion

        #region IP Header Fields - Details
        //private BitArray bitVersion;                // Bits 0-3: Version
        //private BitArray bitHeaderLength;           // Bits 4-7: bitHeaderLength
        //private BitArray bitPrecedence;             // Bits 8-10 Precedence
        //private BitArray bitDelay;                  // Bit 11: Delay (0 = Normal Delay, 1 = Low Delay)
        //private BitArray bitThroughput;             // Bit 12: Throughput (0 = Normal Throughput, 1 = High Throughput)
        //private BitArray bitReliability;            // Bit 13: Reliability (0 = Normal Reliability, 1 = High Reliability)
        //private BitArray bitReserved;               // Bits 14-15: Reserved for Future Use.
        #endregion
        private BitArray bIPHeader = new BitArray(4096,false);
        private byte byHeaderLength;                // Header length
        private byte[] byIPData = new byte[4096];   // Data carried by the datagram

        public IPHeader(byte[] byBuffer, int nReceived) {
            try {
                // Create MemoryStream out of the received bytes
                MemoryStream memoryStream = new MemoryStream(byBuffer, 0, nReceived);
                // Next we create a BinaryReader out of the MemoryStream
                BinaryReader binaryReader = new BinaryReader(memoryStream);
                BitArray bIPHeader = new BitArray(byBuffer);
                // The first eight bits of the IP header contain the version and header length so we read them
                byVersionAndHeaderLength = binaryReader.ReadByte();
                // The next eight bits contain the Differentiated services
                byDifferentiatedServices = binaryReader.ReadByte();
                // Next eight bits hold the total length of the datagram
                usTotalLength = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
                // Next sixteen have the identification bytes
                usIdentification = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
                // Next sixteen bits contain the flags and fragmentation offset
                usFlagsAndOffset = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
                // Next eight bits have the TTL value
                byTTL = binaryReader.ReadByte();
                // Next eight represnts the protocol encapsulated in the datagram
                byProtocol = binaryReader.ReadByte();
                // Next sixteen bits contain the checksum of the header
                sChecksum = IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
                // Next thirty two bits have the source IP address
                uiSourceIPAddress = (uint)(binaryReader.ReadInt32());
                // Next thirty two hold the destination IP address
                uiDestinationIPAddress = (uint)(binaryReader.ReadInt32());
                // Now we calculate the header length from the last four bits of byVersionAndHeaderLength
                byHeaderLength = byVersionAndHeaderLength;
                byHeaderLength <<= 4;       // We shift byVersionAndHeaderLength back 4-Bits (stopping on Bit[0] of our value)
                byHeaderLength >>= 4;       // We shift byVersionAndHeaderLength forward 4-Bits (assigning header length value)
                byHeaderLength *= 4;        // Now multiplied by 4 to get the exact Int32 value
                // Copy the data carried by the datagram into another array according to IP Header Type (index Header Length)
                Array.Copy(byBuffer,                        // Source Array we want
                           byHeaderLength,                  // Index to start copying from (end of the header)
                           byIPData,                        // Destination Array we want to copy to
                           0,                               // Starting Index of Destination Array we copy to
                           usTotalLength - byHeaderLength); // Length of Array we are copying expressed as Int32
                //Array.Copy(byBuffer,                        // Source Array we want
                //           0,            // Index to start copying from (end of the header)
                //           Program.RawPacket[0],        // Destination Array we want to copy to
                //           0,                               // Starting Index of Destination Array we copy to
                //           usTotalLength); // Length of Array we are copying expressed as Int32
               
                Array.Copy(byBuffer,                        // Source Array we want
                          0,                 // Index to start copying from (end of the header)
                          IPSavedHeaderPackets.byIPData,        // Destination Array we want to copy to
                          0,                               // Starting Index of Destination Array we copy to
                          usTotalLength); // Length of Array we are copying expressed as Int32
                //Array.Copy(iPHeaderArrayCopy.LayerNodeArray0, 0, IPSavedHeaderPackets.Version, 0, 4);
                //Array.Copy(IPSavedHeaderPackets.LayerNodeArray0, 0, IPSavedHeaderPackets.HeaderLength, 7, 4);
                //Array.Copy(IPSavedHeaderPackets.LayerNodeArray0, 0, IPSavedHeaderPackets.Precedence, 10, 3);
                //Array.Copy(IPSavedHeaderPackets.LayerNodeArray0, 0, IPSavedHeaderPackets.Delay, 11, 1);
                //Array.Copy(IPSavedHeaderPackets.LayerNodeArray0, 0, IPSavedHeaderPackets.Throughput, 12, 1);
                //Array.Copy(IPSavedHeaderPackets.LayerNodeArray0, 0, IPSavedHeaderPackets.Reliability, 13, 1);
                //Array.Copy(IPSavedHeaderPackets.LayerNodeArray0, 0, IPSavedHeaderPackets.Reserved, 15, 2);
                //Array.Copy(IPSavedHeaderPackets.LayerNodeArray0, 0, IPSavedHeaderPackets.TotalLength, 31, 16);
                //Array.Copy(IPSavedHeaderPackets.LayerNodeArray0, 0, IPSavedHeaderPackets.Identification, 47, 16);
                //Array.Copy(IPSavedHeaderPackets.LayerNodeArray0, 0, IPSavedHeaderPackets.Flags, 50, 3);
                //Array.Copy(IPSavedHeaderPackets.LayerNodeArray0, 0, IPSavedHeaderPackets.FragmentationOffset, 63, 13);
                //Array.Copy(IPSavedHeaderPackets.LayerNodeArray0, 0, IPSavedHeaderPackets.TTL, 71, 8);
                //Array.Copy(IPSavedHeaderPackets.LayerNodeArray0, 0, IPSavedHeaderPackets.ProtocolType, 79, 8);
                //Array.Copy(IPSavedHeaderPackets.LayerNodeArray0, 0, IPSavedHeaderPackets.Checksum, 95, 16);
                //Array.Copy(IPSavedHeaderPackets.LayerNodeArray0, 0, IPSavedHeaderPackets.SourceAddress, 127, 32);
                //Array.Copy(IPSavedHeaderPackets.LayerNodeArray0, 0, IPSavedHeaderPackets.DestinationAddress, 160, 32);
                //Array.Copy(IPSavedHeaderPackets.LayerNodeArray0, 0, IPSavedHeaderPackets.Options, 193, 4);
                //Array.Copy(IPSavedHeaderPackets.LayerNodeArray0, 0, IPSavedHeaderPackets.Data, 4096, 4032);


            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "MainForm", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public BitArray Buffer {
            get {
                return bIPHeader;
            }
        }


        /// <summary>
        /// Public property for the Version of the IP Header. Calculates the 4-Bit IP Version from the 1-Byte[8-Bits]
        /// byVersionAndHeaderLength field : Byte[Bit[0,1,2,3]].
        /// A string value of the Version.
        /// </summary>
        public string Version {
            get {
                if ((byVersionAndHeaderLength >> 4) == 4) {             // We shift 4-Bits (ToS field) out for 4-Bits we want
                    return "IP v4";                                     // A value of 4 means an IPv4 Version
                } else if ((byVersionAndHeaderLength >> 4) == 6) {
                    return "IP v6";                                     // A value of 6 means an IPv6 Version
                } else
                    return "Unknown";                                   // Anything else is type "Unknown"
            }
        }


        /// <summary>
        /// Public property for the Version of the IP Header.
        /// Returns Bool[].
        /// </summary>
        public bool[] bitVersion {
            get {
                BitArray bVersion = new BitArray(byVersionAndHeaderLength);
                for (int i = 0; i < 4; i++) {
                    bitVersion[i] = bitVersion[i];
                }
                return bitVersion;
            }
        }

        /// <summary>
        /// Public property of the IP Header for a string value of the Header Length.
        /// Gives a value calculated using a BinaryReader starting 4-Bits from the end of byVersionAndHeaderLength
        /// and reading forward 4-Bits.  That value is multiplied by 4 to get the header length and stored in byHeaderLength. 
        /// A string value of the Header Length.
        /// </summary>
        public string HeaderLength {
            get {
                return byHeaderLength.ToString();
            }
        }


        /// <summary>
        /// Public property for the Version of the IP Header.
        /// Returns Bool[].
        /// </summary>
        public bool[] bitHeaderLength {
            get {
                BitArray bHeaderLength = new BitArray(byVersionAndHeaderLength);
                for (int i = 4; i < 8; i++) {
                    bitHeaderLength[i] = bHeaderLength[i];
                }
                return bitHeaderLength;
            }
        }



        /// <summary>
        /// Public property for the Message Length of the IP Header. Calculated by subtracting the numeric ushort usTotalLength
        /// field and numeric Byte byHeaderLength field.
        /// A numeric ushort value of the entire Message Length.
        /// </summary>
        public ushort MessageLength {
            get {
                return (ushort)(usTotalLength - byHeaderLength);
            }
        }


        /// <summary>
        /// Public property for the Differentiated Services of the IP Header. 
        /// A string value of the Differentiated Services in Hexadecimal format.
        /// </summary>
        public string DifferentiatedServices {
            get {
                return string.Format("0x{0:x2} ({1})", byDifferentiatedServices, byDifferentiatedServices);
            }
        }


        /// <summary>
        /// Public property for the Flags of the IP Header. The first three bits of the 16-Bit usFlagsAndOffset field
        /// represent the flags (which indicate whether the data is fragmented or not).
        /// A string value of the Flags.
        /// </summary>
        public string Flags {
            get {
                int nFlags = usFlagsAndOffset >> 13;        // We shift 13-Bits (Frag field) out for 3-Bits we want as Int32
                if (nFlags == 2) {                          // A value 2 means Fragmentation is off 
                    return "Don't fragment";
                } else if (nFlags == 1) {                   // A value 1 means Fragmentation is on 
                    return "More fragments to come";
                } else {
                    return nFlags.ToString();
                }
            }
        }


        /// <summary>
        /// Public property for the Fragmentation Offset of the IP Header. Calculated using the 16-Bit usFlagsAndOffset field.
        /// A string value of the Fragmentation Offset.
        /// </summary>
        public string FragmentationOffset {
            get {
                int nOffset = usFlagsAndOffset << 3;        // We shift 3-Bits (Flag field) out for 13-Bits we want as Int32
                nOffset >>= 3;                              // Get value of 3-Bits containing the fragmentation offset
                return nOffset.ToString();
            }
        }


        /// <summary>
        /// Public property for the Time to Live (TTL) of the IP Header.
        /// A string value of the TTL (Time to Live).
        /// </summary>
        public string TTL {
            get {
                return byTTL.ToString();
            }
        }


        /// <summary>
        /// Public property for the Protocol Type of the IP Header. Represents the protocol carrying data in datagram.
        /// One (of 3) enumerated Protocol types created in the MainForm class. [6 = TCP] [17 = UDP] [-1(null) = Unknown]
        /// </summary>
        public Protocol ProtocolType {
            get {
                if (byProtocol == 6) {              // A value of 6 represents the TCP protocol
                    return Protocol.TCP;
                } else if (byProtocol == 17) {      // A value of 17 represents the TCP protocol
                    return Protocol.UDP;
                } else {                            // A value of -1, null, or anything else represents the Unknown protocol
                    return Protocol.Unknown;
                }
            }
        }


        /// <summary>
        /// Public property for the Checksum of the IP Header. Calculated from the numeric ushort field sChecksum.
        /// A string value in Hexadecimal format of the Checksum.
        /// </summary>
        public string Checksum {
            get {
                return string.Format("0x{0:x2}", sChecksum);    // Returns the checksum in hexadecimal format
            }
        }


        /// <summary>
        /// Public property for the Source Address of the IP Header. Uses the unsigned Int32 uiSourceIPAddress.
        /// A new IPAddress class of a IP Address.
        /// </summary>
        public IPAddress SourceAddress {
            get {
                return new IPAddress(uiSourceIPAddress);
            }
        }


        /// <summary>
        /// Public property for the Destination Address of the IP Header. Uses the unsigned Int32 uiDestinationIPAddress.
        /// A new IPAddress class of a IP Address.
        /// </summary>
        public IPAddress DestinationAddress {
            get {
                return new IPAddress(uiDestinationIPAddress);
            }
        }


        /// <summary>
        /// Public property for the Total Length of the IP Header. Uses the numeric ushort 16-Bit usTotalLength field.
        /// A string value of the Total Length.
        /// </summary>
        public string TotalLength {
            get {
                return usTotalLength.ToString();
            }
        }


        /// <summary>
        /// Public property for the Identification of the IP Header. Uses the numeric ushort 16-Bit usIdentification field.
        /// A string value of the Identification.
        /// </summary>
        public string Identification {
            get {
                return usIdentification.ToString();
            }
        }


        /// <summary>
        /// Public property for the Data Payload in the message of the IP Header. Uses the Byte[4096] formatted byIPData field.
        /// A Byte Array of the Data Payload in the message.
        /// </summary>
        public byte[] Data {
            get {
                return byIPData;
            }
        }


        /// <summary>
        /// Public property for the Precedence of the IP Header. Calculated using the 8-Bit Byte byDifferentiatedServices field.
        /// A string value of the Fragmentation Offset.
        /// </summary>
        public string Precedence {
            get {
                BitArray bToSs = new BitArray(byDifferentiatedServices);
                bool b0 = false;
                string strPrecedence = "";
                switch (b0) {
                    case true:
                        if (bToSs[0]) {
                            if (bToSs[1]) {
                                if (bToSs[2]) {     // 111 - Network Control
                                    strPrecedence = "Network Control";
                                    break;
                                } else {            // 110 - Internetwork Control
                                    strPrecedence = "Internetwork Control";
                                    break;
                                }
                            } else {
                                if (bToSs[2]) {     // 101 - CRITIC/ECP
                                    strPrecedence = "CRITIC/ECP";
                                    break;
                                } else {            // 100 - Flash Override
                                    strPrecedence = "Flash Override";
                                    break;
                                }
                            }
                        } else {
                            goto case false;
                        }
                    case false:
                        if (!bToSs[0]) {
                            if (bToSs[1]) {
                                if (bToSs[2]) {     // 011 - Flash
                                    strPrecedence = "Flash";
                                    break;
                                } else {            // 010 - Immediate
                                    strPrecedence = "Immediate";
                                    break;
                                }
                            } else {
                                if (bToSs[2]) {     // 001 - Priority
                                    strPrecedence = "Priority";
                                    break;
                                } else {            // 000 - Routine
                                    strPrecedence = "Routine";
                                    break;
                                }
                            }
                        } else {
                            goto case true;
                        }
                }
                return strPrecedence;
            }
        }



    }
}
                    
 


           //                     case "Internetwork Control":  
           //                 if ((bToSs[i]) && (bToSs[1]) && (!bToSs[2])) {
           //                     x = "Internetwork Control";
           //                 if ((bToSs[i]) && (!bToSs[1]) && (bToSs[2])) {
           //                     case "CRITIC/ECP":
           //                 }
           //                 if ((bToSs[i]) && (!bToSs[1]) && (!bToSs[2])) {
           //                     x = "Flash Override";
           //                 }
           //                 if ((!bToSs[i]) && (bToSs[1]) && (bToSs[2])) {
           //                     x = "Flash";
           //                 }
           //                 if ((!bToSs[i]) && (bToSs[1]) && (!bToSs[2])) {
           //                     x = "Immediate";
           //                 }
           //                 if ((!bToSs[i]) && (!bToSs[1]) && (bToSs[2])) {
           //                     x = "Priority";
           //                 }
           //                 if ((!bToSs[i]) && (!bToSs[1]) && (!bToSs[2])) {
           //                     x = "Routine";
           //                 }

           //                 break;
                        
           //                 break;
           //             case 2:
           //                 break;
           //             default:
// Bits 0-2: Precedence.
// Bit 3: Delay (0 = Normal Delay, 1 = Low Delay)
// Bit 4: Throughput (0 = Normal Throughput, 1 = High Throughput)
// Bit 5: Reliability (0 = Normal Reliability, 1 = High Reliability)
// Bits 6-7: Reserved for Future Use.


