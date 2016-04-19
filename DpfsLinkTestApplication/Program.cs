using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;


namespace DpfsLinkTestApplication {

    public static class Program {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            InitializeClients();
            //MessageBox.Show(ServerAddress.Count.ToString());      // Shows initial count at startup
            Application.Run(new MainForm());
        }


        #region Client
        /// <summary>
        /// Default Settings and Initial Configuration for all Client Sockets
        /// </summary>
        public static void InitializeClients() {

            IDictionary<int, byte[]> setRawPacket = new Dictionary<int, byte[]>();
            RawPacket = setRawPacket;

            IDictionary<TreeNode, IPHeader> setCapturedPackets = new Dictionary<TreeNode, IPHeader>();
            CapturedPackets = setCapturedPackets;                   // Instantiate CapturedPackets Dictionary

            IDictionary<int, string> setServerAddress = new Dictionary<int, string>();
            setServerAddress.Add(0, "localhost");
            setServerAddress.Add(1, "0.0.0.0");
            setServerAddress.Add(2, "::");
            ServerAddress = setServerAddress;                       // Instantiate ServerAddress Dictionary

            IDictionary<string, string> setServerPort = new Dictionary<string, string>();
            setServerPort.Add("tcp", "5150");
            setServerPort.Add("dpfs", "6879");
            setServerPort.Add("powerserver", "9500");
            ServerPort = setServerPort;                             // Instantiate ServerPort Dictionary

            IDictionary<string, string> setClientMessage = new Dictionary<string, string>();
            setClientMessage.Add("ipv4", null);
            setClientMessage.Add("unknown", null);
            setClientMessage.Add("raw", null);
            setClientMessage.Add("tcp", null);
            setClientMessage.Add("udp", null);
            setClientMessage.Add("ipv6", null);
            setClientMessage.Add("ipv6frag", null);
            setClientMessage.Add("ip", null);
            setClientMessage.Add("lcmp", null);
            setClientMessage.Add("lgmp", null);
            setClientMessage.Add("ipx", null);
            ClientMessage = setClientMessage;                       // Instantiate ServerMessage Dictionary



            IDictionary<string, string> setClientSize = new Dictionary<string, string>();
            setClientSize.Add("tcp", __Console__.ClientSize.tcp.ToString());
            setClientSize.Add("dpfs", __Console__.ClientSize.dpfs.ToString());
            setClientSize.Add("powerserver", __Console__.ClientSize.powerserver.ToString());
            IClientSize = setClientSize;                             // Instantiate ServerSize Dictionary




            IDictionary<string, string> setHeaderType = new Dictionary<string, string>();
            setHeaderType.Add("tcp", "stream");
            setHeaderType.Add("udp", "dgram");
            setHeaderType.Add("raw", "unknown");
            setHeaderType.Add("ipv4", "raw");
            setHeaderType.Add("ipv6", "raw");
            setHeaderType.Add("ipv6frag", "raw");
            setHeaderType.Add("ip", "raw");
            setHeaderType.Add("icmp", "raw");
            setHeaderType.Add("igmp", "ipv4");
            setHeaderType.Add("ipx", "spx");
            HeaderType = setHeaderType;                             // Instantiate ServerType Dictionary

        }

            //string[] strMessage;
            //ICollection keysMessages = Program.ServerMessage.Keys; 
            //IDictionary Program.ServerMessage<string,string>();
        
    
        


        /// <summary>
        /// An Integer index representing the UI configuration of the type of Server Address.
        /// The address value is a string type.
        /// The Default is HostName localhost
        /// 0 = DNS HostName
        /// 1 = IPv4 Address
        /// 2 = IPv6 Address
        /// </summary>
        public static IDictionary<int, string> ServerAddress = new Dictionary<int, string>();


        /// <summary>
        /// An Integer index representing a generic description of the purpose of the Server Port 
        /// to connection to. The client will attempt to connect to this Server Port.
        /// The port value is a string type.
        /// The Default is 5150.
        /// tcp = 5150
        /// dpfs = 6879
        /// powerserver = 9500               
        /// </summary>
        public static IDictionary<string, string> ServerPort = new Dictionary<string, string>();


        /// <summary>
        /// A string index representing the Protocol Header Type of the message and the value of each
        /// each is the associated message payload as a string.  The payload is later parsed into
        /// a Byte[] Array for sending with a single packet or fragmented according to the maximum
        /// buffer size and packet size.
        /// The default is IPv4.
        /// ipv4 = messagePayload
        /// unknown = messagePayload
        /// raw = messagePayload
        /// tcp = messagePayload
        /// udp = messagePayload
        /// ipv6 = messagePayload
        /// ipv6frag = messagePayload
        /// ip = messagePayload
        /// lcmp = messagePayload
        /// lgmp = messagePayload
        /// ipx = messagePayload
        /// </summary>
        public static IDictionary<string, string> ClientMessage = new Dictionary<string, string>();


        /// <summary>
        /// An Integer index representing a generic description of the purpose.  The value of each key
        /// is the buffer size acceptable between the client and server.
        /// NOTE : With UDP, a buffer size mis-match will result in packet rejection.
        /// The buffer size value is a string type.
        /// The Default is 4096.
        /// tcp = 4096
        /// dpfs = 512
        /// powerserver = 1024               
        /// </summary>
        public static IDictionary<string, string> IClientSize = new Dictionary<string, string>();


        /// <summary>
        /// A string index representing the Protocol Header Type used between the client and server.
        /// The value is of type string and contains the Socket Type.
        /// The default is ProtocolType.Tcp and SocketType.Stream.
        /// tcp = stream
        /// udp = dgram
        /// raw = unknown
        /// ipv4 = raw
        /// ipv6 = raw
        /// ipv6frag = raw
        /// ip = raw
        /// icmp = raw
        /// igmp = ipv4
        /// ipx = spx
        /// </summary>
        public static IDictionary<string, string> HeaderType = new Dictionary<string, string>();



        /// <summary>
        /// Dictionary of index IPEndPoint and Value IPHeader.
        /// </summary>
        public static IDictionary<TreeNode, IPHeader> CapturedPackets = new Dictionary<TreeNode, IPHeader>();



        /// <summary>
        /// Dictionary of byte[] for received packet.
        /// </summary>
        public static IDictionary<int, byte[]> RawPacket = new Dictionary<int, byte[]>();
        #endregion
    }

}

