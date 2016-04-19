using System.Net;
using System.Text;
using System;
using System.IO;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;

namespace DpfsLinkTestApplication {
    public class IPSavedHeaderPackets {

        public static IDictionary<int, Byte[]> ipSavedHeaders = new Dictionary<int, Byte[]>();
      //  private static int indexIPpacket = new Int32();
        #region IP Header Fields
        private static byte byVersionAndHeaderLength;      // Eight bits for version and header length
        private static byte byDifferentiatedServices;      // Eight bits for differentiated services (TOS)
        private static ushort usTotalLength;               // Sixteen bits for total length of the datagram (header + message)
        private static ushort usIdentification;            // Sixteen bits for identification
        private static ushort usFlagsAndOffset;            // Eight bits for flags and fragmentation offset
        private static byte byTTL;                         // Eight bits for TTL (Time To Live)
        private static byte byProtocol;                    // Eight bits for the underlying protocol
        private static short sChecksum;                    // Sixteen bits containing the checksum of the header
        //    (checksum can be negative so taken as short)
        private static uint uiSourceIPAddress;             // Thirty two bit source IP Address
        private static uint uiDestinationIPAddress;        // Thirty two bit destination IP Address
        #endregion

        #region IP Header Fields - Details
        public static BitArray bitVersion = new BitArray(4,false);                // Bits 0-3: Version
        public static BitArray bitHeaderLength = new BitArray(4, false);          // Bits 4-7: bitHeaderLength
        public static BitArray bitPrecedence = new BitArray(3, false);            // Bits 8-10 Precedence
        public static BitArray bitDelay = new BitArray(1, false);                    // Bit 11: Delay (0 = Normal Delay, 1 = Low Delay)
        public static BitArray bitThroughput = new BitArray(1, false);               // Bit 12: Throughput (0 = Normal Throughput, 1 = High Throughput)
        public static BitArray bitReliability = new BitArray(1, false);              // Bit 13: Reliability (0 = Normal Reliability, 1 = High Reliability)
        public static BitArray bitReserved = new BitArray(2, false);              // Bits 14-15: Reserved for Future Use.
        public static BitArray bitTotalLength = new BitArray(16, false);              // Bits 33-48: Reserved for Future Use.
        public static BitArray bitIdentification = new BitArray(16, false);          // Bits 16-32: Reserved for Future Use.
        public static BitArray bitFlags = new BitArray(3, false);                   // Bits 49-51: Reserved for Future Use.
        public static BitArray bitFragmentation = new BitArray(13, false);           // Bits 52-65: Reserved for Future Use.
        public static BitArray bitTTL = new BitArray(8, false);                    // Bits 52-65: Reserved for Future Use.
        public static BitArray bitProtocol = new BitArray(8, false);               // Bits 52-65: Reserved for Future Use.
        public static BitArray bitChecksum = new BitArray(16, false);               // Bits 52-65: Reserved for Future Use.
        public static BitArray bitSourceAddress = new BitArray(32, false);           // Bits 52-65: Reserved for Future Use.
        public static BitArray bitDestinationAddress = new BitArray(32, false);      // Bits 52-65: Reserved for Future Use.
        public static BitArray bitOptions = new BitArray(32, false);               // Bits 52-65: Reserved for Future Use.
        public static BitArray bitPadding = new BitArray(8, false);               // Bits 52-65: Reserved for Future Use.
        public static BitArray bitData = new BitArray(4096, false);                    // Bits 52-65: Reserved for Future Use.
        #endregion
        public static bool[] LayerNodeArray0 = new bool[4096];


        private static byte byHeaderLength;                // Header length
        public static byte[] byIPData = new byte[4096];   // Data carried by the datagram

        public IPSavedHeaderPackets(byte[] byBuffer, int nReceived) {
          //  indexIPpacket = 0;
            //indexIPpacket = nReceived;
           // indexIPpacket = index;
            try {
                // Save the IP Header Packet First
              //  ipSavedHeaders[indexIPpacket] = byBuffer;
                // Create MemoryStream out of the received bytes
                MemoryStream memoryStream = new MemoryStream(byBuffer, 0, nReceived);
                // Next we create a BinaryReader out of the MemoryStream
                BinaryReader binaryReader = new BinaryReader(memoryStream);
                // The first eight bits of the IP header contain the version and header length so we read them
                byVersionAndHeaderLength = binaryReader.ReadByte();
                // The first 4-Bits contain Version and the last 4-Bits contain Header Length
                BitArray baVH = new BitArray(byVersionAndHeaderLength);
                for (int i = 0; i < 4; i++) {
                    bitVersion[i] = baVH[i];
                }
                for (int i = 4; i < 8; i++) {
                    bitHeaderLength[i] = baVH[i];
                }
                baVH = null;
                // The next eight bits contain the Differentiated services
                byDifferentiatedServices = binaryReader.ReadByte();
                BitArray baDS = new BitArray(byDifferentiatedServices);
                for (int i = 0; i < 3; i++) {                           // 3-Bits: Precedence 
                    bitPrecedence[i] = baDS[i];     
                }
                for (int i = 3; i < 4; i++) {                           // 1-Bit: Delay 
                    bitDelay[i] = baDS[i];
                }
                for (int i = 4; i < 5; i++) {                           // 1-Bit: Throughput 
                    bitThroughput[i] = baDS[i];
                }
                for (int i = 5; i < 6; i++) {                           // 1-Bit: Reliability 
                    bitReliability[i] = baDS[i];
                }
                for (int i = 6; i < 8; i++) {                           // 2-Bit: Reserved 
                    bitReserved[i] = baDS[i];
                }
                baDS = null;
                // Next 16 bits hold the total length of the datagram
                usTotalLength = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
                BitArray baTL = new BitArray(usTotalLength);
                for (int i = 0; i < 16; i++) {                           // 16-Bits: TotalLength 
                    bitTotalLength[i] = baTL[i];
                }
                baTL = null;
                // Next sixteen have the identification bytes
                usIdentification = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
                BitArray baID = new BitArray(usIdentification);
                for (int i = 0; i < 16; i++) {                           // 16-Bits: Identification 
                    bitIdentification[i] = baID[i];
                }
                baID = null;
                // Next sixteen bits contain the flags and fragmentation offset
                usFlagsAndOffset = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
                BitArray baFF = new BitArray(usFlagsAndOffset);
                for (int i = 0; i < 3; i++) {                           // 3-Bits: Flag 
                    bitFlags[i] = baFF[i];
                }
                for (int i = 3; i < 16; i++) {                           // 13-Bits: Fragmentation Offset 
                    bitFragmentation[i] = baFF[i];
                }
                baFF = null;
                // Next eight bits have the TTL value
                byTTL = binaryReader.ReadByte();
                BitArray baTT = new BitArray(byTTL);
                for (int i = 0; i < 8; i++) {                           // 8-Bits: Time to Live
                    bitTTL[i] = baTT[i];
                }
                baTT = null;
                // Next eight represents the protocol encapsulated in the datagram
                byProtocol = binaryReader.ReadByte();
                BitArray baPC = new BitArray(byProtocol);
                for (int i = 0; i < 8; i++) {                           // 8-Bits: Protocol
                    bitProtocol[i] = baPC[i];
                }
                baPC = null;
                // Next sixteen bits contain the checksum of the header
                sChecksum = IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
                BitArray baCS = new BitArray(sChecksum);
                for (int i = 0; i < 16; i++) {                           // 16-Bits: Checksum
                    bitChecksum[i] = baCS[i];
                }
                baCS = null;
                // Next thirty two bits have the source IP address
                uiSourceIPAddress = (uint)(binaryReader.ReadInt32());
                byte[] bySA = BitConverter.GetBytes(uiSourceIPAddress);
                BitArray baSA = new BitArray(bySA);
                for (int i = 0; i < 32; i++) {                           // 32-Bits: Source IP Address
                    bitSourceAddress[i] = baSA[i];
                }
                bySA = null;
                baSA = null;
                // Next thirty two hold the destination IP address
                uiDestinationIPAddress = (uint)(binaryReader.ReadInt32());
                byte[] byDA = BitConverter.GetBytes(uiDestinationIPAddress);
                BitArray baDA = new BitArray(byDA);
                for (int i = 0; i < 32; i++) {                           // 32-Bits: Destination IP Address
                    bitDestinationAddress[i] = baDA[i];
                }
                byDA = null;
                baDA = null;
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
                Array.Copy(IPSavedHeaderPackets.LayerNodeArray0, 0, IPSavedHeaderPackets.Version, 0, 4);
                Array.Copy(IPSavedHeaderPackets.LayerNodeArray0, 7, IPSavedHeaderPackets.HeaderLength, 0, 4);
                Array.Copy(IPSavedHeaderPackets.LayerNodeArray0, 10, IPSavedHeaderPackets.Precedence, 0, 3);
                Array.Copy(IPSavedHeaderPackets.LayerNodeArray0, 11, IPSavedHeaderPackets.Delay, 0, 1);
                Array.Copy(IPSavedHeaderPackets.LayerNodeArray0, 12, IPSavedHeaderPackets.Throughput, 0, 1);
                Array.Copy(IPSavedHeaderPackets.LayerNodeArray0, 13, IPSavedHeaderPackets.Reliability, 0, 1);
                Array.Copy(IPSavedHeaderPackets.LayerNodeArray0, 15, IPSavedHeaderPackets.Reserved, 0, 2);
                Array.Copy(IPSavedHeaderPackets.LayerNodeArray0, 31, IPSavedHeaderPackets.TotalLength, 0, 16);
                Array.Copy(IPSavedHeaderPackets.LayerNodeArray0, 47, IPSavedHeaderPackets.Identification, 0, 16);
                Array.Copy(IPSavedHeaderPackets.LayerNodeArray0, 50, IPSavedHeaderPackets.Flags, 0, 3);
                Array.Copy(IPSavedHeaderPackets.LayerNodeArray0, 63, IPSavedHeaderPackets.FragmentationOffset, 0, 13);
                Array.Copy(IPSavedHeaderPackets.LayerNodeArray0, 71, IPSavedHeaderPackets.TTL, 0, 8);
                Array.Copy(IPSavedHeaderPackets.LayerNodeArray0, 79, IPSavedHeaderPackets.ProtocolType, 0, 8);
                Array.Copy(IPSavedHeaderPackets.LayerNodeArray0, 95, IPSavedHeaderPackets.Checksum, 0, 16);
                Array.Copy(IPSavedHeaderPackets.LayerNodeArray0, 127, IPSavedHeaderPackets.SourceAddress, 0, 32);
                Array.Copy(IPSavedHeaderPackets.LayerNodeArray0, 160, IPSavedHeaderPackets.DestinationAddress, 0, 32);
                Array.Copy(IPSavedHeaderPackets.LayerNodeArray0, 193, IPSavedHeaderPackets.Options, 0, 4);
                Array.Copy(IPSavedHeaderPackets.LayerNodeArray0, 4096, IPSavedHeaderPackets.Data, 0, 4032);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "MainForm", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // <summary>
        // Test stuff
        // </summary>
        // <param name="Iindex"></param>
        // <returns></returns>
        //public static byte[] Saved {
        //    get {
        //        byte[] Saved = ipSavedHeaders[indexIPpacket];
        //        return Saved;
        //    }
        //}




        // <summary>
        // Index of the IP Header Packet.
        // </summary>
        //public static int Index {
        //    get {
        //        return indexIPpacket;
        //    }
        //}

        /// <summary>
        /// Public property for the Version of the IP Header.
        /// Returns Bool[].
        /// </summary>
        public static bool[] Version {
            get {
                for (int i = 0; i < 4; i++)
                   Version[i] = bitVersion[i];
                return Version;
            }
        }


        /// <summary>
        /// Public property for the Header Length of the IP Header.
        /// Returns Bool[].
        /// </summary>
        public static bool[] HeaderLength {
            get {
                for (int i = 0; i < 4; i++) {
                    HeaderLength[i] = bitHeaderLength[i];
                }
                return HeaderLength;
            }
        }


        /// <summary>
        /// Public property for the Precedence of the IP Header.
        /// Returns Bool[].
        /// </summary>
        public static bool[] Precedence {
            get {
                for (int i = 0; i < 2; i++) {
                    Precedence[i] = bitPrecedence[i];
                }
                return Precedence;
            }
        }


        /// <summary>
        /// Public property for the Delay of the IP Header.
        /// Returns Bool[].
        /// </summary>
        public static bool[] Delay {
            get {
                for (int i = 0; i < 1; i++) {
                    Delay[i] = bitDelay[i];
                }
                return Delay;
            }
        }


        /// <summary>
        /// Public property for the Delay of the IP Header.
        /// Returns Bool[].
        /// </summary>
        public static bool[] Throughput {
            get {
                for (int i = 0; i < 1; i++) {
                    Throughput[i] = bitThroughput[i];
                }
                return Throughput;
            }
        }


        /// <summary>
        /// Public property for the Delay of the IP Header.
        /// Returns Bool[].
        /// </summary>
        public static bool[] Reliability {
            get {
                for (int i = 0; i < 1; i++) {
                    Reliability[i] = bitReliability[i];
                }
                return Reliability;
            }
        }


        /// <summary>
        /// Public property for the Delay of the IP Header.
        /// Returns Bool[].
        /// </summary>
        public static bool[] Reserved {
            get {
                for (int i = 0; i < 1; i++) {
                    Reserved[i] = bitReserved[i];
                }
                return Reserved;
            }
        }


        /// <summary>
        /// Public property for the Total Length of the IP Header.
        /// Returns Bool[].
        /// </summary>
        public static bool[] TotalLength {
            get {
                for (int i = 0; i < 16; i++) {
                    TotalLength[i] = bitTotalLength[i];
                }
                return TotalLength;
            }
        }


        /// <summary>
        /// Public property for the Identification of the IP Header.
        /// Returns Bool[].
        /// </summary>
        public static bool[] Identification {
            get {
                for (int i = 0; i < 8; i++) {
                    Identification[i] = bitIdentification[i];
                }
                return Identification;
            }
        }


        /// <summary>
        /// Public property for the Message Length of the IP Header. Calculated by subtracting the numeric ushort usTotalLength
        /// field and numeric Byte byHeaderLength field.
        /// A numeric ushort value of the entire Message Length.
        /// </summary>
        public static ushort MessageLength {
            get {
                return (ushort)(usTotalLength - byHeaderLength);
            }
        }


        /// <summary>
        /// Public property for the Flags of the IP Header.
        /// Returns Bool[].
        /// </summary>
        public static bool[] Flags {
            get {
                for (int i = 0; i < 3; i++) {
                    Flags[i] = bitFlags[i];
                }
                return Flags;
            }
        }


        /// <summary>
        /// Public property for the Fragmentation Offset of the IP Header.
        /// Returns Bool[].
        /// </summary>
        public static bool[] FragmentationOffset {
            get {
                for (int i = 0; i < 13; i++) {
                    FragmentationOffset[i] = bitFragmentation[i];
                }
                return FragmentationOffset;
            }
        }


        /// <summary>
        /// Public property for the TTL of the IP Header.
        /// Returns Bool[].
        /// </summary>
        public static bool[] TTL {
            get {
                for (int i = 0; i < 8; i++) {
                    TTL[i] = bitTTL[i];
                }
                return TTL;
            }
        }


        /// <summary>
        /// Public property for the Protocol Type of the IP Header.
        /// Returns Bool[].
        /// </summary>
        public static bool[] ProtocolType {
            get {
          for (int i = 0; i < 8; i++) {
                    ProtocolType[i] = bitProtocol[i];
                }
                return ProtocolType;
                
            }
        }


        /// <summary>
        /// Public property for the Checksum Type of the IP Header.
        /// Returns Bool[].
        /// </summary>
        public static bool[] Checksum {
            get {
                for (int i = 0; i < 16; i++) {
                    Checksum[i] = bitChecksum[i];
                }
                return Checksum;
            }
        }


        /// <summary>
        /// Public property for the Source Address of the IP Header.
        /// Returns Bool[].
        /// </summary>
        public static bool[] SourceAddress {
            get {
                for (int i = 0; i < 32; i++) {
                    SourceAddress[i] = bitSourceAddress[i];
                }
                return SourceAddress;
            }
        }


        /// <summary>
        /// Public property for the Destination Address of the IP Header.
        /// Returns Bool[].
        /// </summary>
        public static bool[] DestinationAddress {
            get {
                for (int i = 0; i < 32; i++) {
                    DestinationAddress[i] = bitDestinationAddress[i];
                }
                return DestinationAddress;
            }
        }


        /// <summary>
        /// Public property for the Data Payload in the message of the IP Header. Uses the Byte[4096] formatted byIPData field.
        /// A Byte Array of the Data Payload in the message.
        /// </summary>
        public static byte[] Data {
            get {
                return byIPData;
            }
        }


        /// <summary>
        /// Public property for the Data Payload in the message of the IP Header. Uses the Byte[4096] formatted byIPData field.
        /// A Byte Array of the Data Payload in the message.
        /// </summary>
        public static bool[] DataPreview {
            get {
                bitData = new BitArray(byIPData);
                for (int i = 0; i < 4096; i++) {
                    DataPreview[i] = bitData[i];
                }
                return DataPreview;
            }
        }


        /// <summary>
        /// Public property for the Destination Address of the IP Header.
        /// Returns Bool[].
        /// </summary>
        public static bool[] Options {
            get {
                for (int i = 0; i < 32; i++) {
                    DestinationAddress[i] = bitDestinationAddress[i];
                }
                return DestinationAddress;
            }
        }

    }
}