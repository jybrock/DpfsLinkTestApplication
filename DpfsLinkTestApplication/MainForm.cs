using System;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Net.Sockets;
using DpfsLinkTestApplication.__Monster__;
using DpfsLinkTestApplication.__SOAP__;
using DpfsLinkTestApplication.__Console__;
using DpfsLinkTestApplication.com.dpro.www;
using System.Diagnostics;
using System.Collections.ObjectModel;


namespace DpfsLinkTestApplication {
    #region Protocol ENUM

    ///-------------------------------------------------------------------------------------------------
    /// <summary>
    /// Protocol Type enumeration used with datagrams, their IP Header and Payload. You'll find
    /// references in MainForm and the IPHeader class and IPHeader associated classes.
    /// </summary>
    ///
    /// <remarks>
    /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
    /// </remarks>
    ///-------------------------------------------------------------------------------------------------

    public enum Protocol {
        /// <summary>
        /// An enum constant representing the TCP option.
        /// </summary>
        TCP = 6,
        /// <summary>
        /// An enum constant representing the UDP option.
        /// </summary>
        UDP = 17,
        /// <summary>
        /// An enum constant representing the unknown option.
        /// </summary>
        Unknown = -1
    };
    #endregion

    ///-------------------------------------------------------------------------------------------------
    /// <summary>
    /// MainForm is the main class of the DpfsLinkTestApplication.
    /// </summary>
    ///
    /// <remarks>
    /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
    /// </remarks>
    ///
    /// <seealso cref="T:System.Windows.Forms.Form"/>
    ///-------------------------------------------------------------------------------------------------

    public partial class MainForm : Form {

        #region Monitor Mode Objects

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets a value indicating whether the view i pstream tc ptab.
        /// </summary>
        ///
        /// <value>
        /// true if view i pstream tc ptab, false if not.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static bool boolViewIPstreamTCPtab { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the int monitor clicks.
        /// </summary>
        ///
        /// <value>
        /// The int monitor clicks.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static int intMonitorClicks { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the int transmit clicks.
        /// </summary>
        ///
        /// <value>
        /// The int transmit clicks.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static int intTransmitClicks { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// The layer 1 i ppacket.
        /// </summary>
        ///-------------------------------------------------------------------------------------------------

        public static Dictionary<int, bool> layer1IPpacket;

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// The layer 2 i ppacket.
        /// </summary>
        ///-------------------------------------------------------------------------------------------------

        public static Dictionary<int, bool> layer2IPpacket;

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// The layer 3 i ppacket.
        /// </summary>
        ///-------------------------------------------------------------------------------------------------

        public static Dictionary<int, bool> layer3IPpacket;

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// The layer 4 i ppacket.
        /// </summary>
        ///-------------------------------------------------------------------------------------------------

        public static Dictionary<int, bool> layer4IPpacket;

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// The layer 5 i ppacket.
        /// </summary>
        ///-------------------------------------------------------------------------------------------------

        public static Dictionary<int, bool> layer5IPpacket;

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// The layer 6 i ppacket.
        /// </summary>
        ///-------------------------------------------------------------------------------------------------

        public static Dictionary<int, bool> layer6IPpacket;

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// The layer 7 i ppacket.
        /// </summary>
        ///-------------------------------------------------------------------------------------------------

        public static Dictionary<int, bool> layer7IPpacket;

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// The socket which captures all incoming packets.
        /// </summary>
        ///-------------------------------------------------------------------------------------------------

        private Socket mainSocket;

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// A standard BinaryReader Buffer.
        /// </summary>
        ///-------------------------------------------------------------------------------------------------

        private byte[] byteData = new byte[4096];

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// A flag to check if packets are to be captured or not.
        /// </summary>
        ///-------------------------------------------------------------------------------------------------

        private bool bContinueCapturing = false;

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Adds a tree node.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="node">
        ///     The node.
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private delegate void AddTreeNode(TreeNode node);
        #endregion

        #region MainForm UI Properties and Settings

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets a value indicating whether the tab 8.
        /// </summary>
        ///
        /// <value>
        /// true if tab 8, false if not.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static bool boolTab8 { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the que text.
        /// </summary>
        ///
        /// <value>
        /// The que text.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public Queue queText { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the int tab case.
        /// </summary>
        ///
        /// <value>
        /// The int tab case.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static int intTabCase { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the dpscanservice.
        /// </summary>
        ///
        /// <value>
        /// The dpscanservice.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static DPscanService dpscanservice { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// The paneltest.
        /// </summary>
        ///-------------------------------------------------------------------------------------------------

        public Panel paneltest;

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the int column.
        /// </summary>
        ///
        /// <value>
        /// The int column.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static int intColumn { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the int row.
        /// </summary>
        ///
        /// <value>
        /// The int row.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static int intRow { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the number of int captured packets.
        /// </summary>
        ///
        /// <value>
        /// The number of int captured packets.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static int intCapturedPacketCount { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the console text.
        /// </summary>
        ///
        /// <value>
        /// The string console text.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static string strConsoleText { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the queue commands.
        /// </summary>
        ///
        /// <value>
        /// The queue commands.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static Queue queueCommands { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets options for controlling the queue.
        /// </summary>
        ///
        /// <value>
        /// Options that control the queue.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static Queue queueParams { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the queue server socket.
        /// </summary>
        ///
        /// <value>
        /// The queue server socket.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static Queue queueServerSocket { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the queue client socket.
        /// </summary>
        ///
        /// <value>
        /// The queue client socket.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static Queue queueClientSocket { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the queue client TCP console.
        /// </summary>
        ///
        /// <value>
        /// The queue client TCP console.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static Queue queueClientTcpConsole { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the que console.
        /// </summary>
        ///
        /// <value>
        /// The que console.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static Queue queConsole { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the int text lines.
        /// </summary>
        ///
        /// <value>
        /// The int text lines.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static int intTextLines { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets information describing the TCP.
        /// </summary>
        ///
        /// <value>
        /// Information describing the TCP.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static string strTcpDesc { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the int TCP server type cbx focus.
        /// </summary>
        ///
        /// <value>
        /// The int TCP server type cbx focus.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static int intTcpSrvTypeCbxFocus { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the number of. 
        /// </summary>
        ///
        /// <value>
        /// The count.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        private static int _count { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets a message describing the open add.
        /// </summary>
        ///
        /// <value>
        /// A message describing the open add.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        private static int _openAddMsg { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets a value indicating whether the sor c.
        /// </summary>
        ///
        /// <value>
        /// true if sor c, false if not.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static bool boolSorC { get; set; }                                               // Server is True
        #endregion

        #region Client Run-time Settings

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets a value indicating whether the power mode.
        /// </summary>
        ///
        /// <value>
        /// true if power mode, false if not.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static bool boolPowerMode { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the client destination address right.
        /// </summary>
        ///
        /// <value>
        /// The client destination address right.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static string clientDestAddrRT { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the client destination port right.
        /// </summary>
        ///
        /// <value>
        /// The client destination port right.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static string clientDestPortRT { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the client message right.
        /// </summary>
        ///
        /// <value>
        /// The client message right.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static string clientMessageRT { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the client protocol right.
        /// </summary>
        ///
        /// <value>
        /// The client protocol right.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static string clientProtocolRT { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the client socket right.
        /// </summary>
        ///
        /// <value>
        /// The client socket right.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static string clientSocketRT { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the client buffer size right.
        /// </summary>
        ///
        /// <value>
        /// The client buffer size right.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static string clientBufSizeRT { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets a value indicating whether the client is UDP right.
        /// </summary>
        ///
        /// <value>
        /// true if client is UDP right, false if not.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static bool clientIsUdpRT { get; set; }
        #endregion

        #region Server Run-time Settings

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the server destination address right.
        /// </summary>
        ///
        /// <value>
        /// The server destination address right.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static string serverDestAddrRT { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the server destination port right.
        /// </summary>
        ///
        /// <value>
        /// The server destination port right.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static string serverDestPortRT { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the server message right.
        /// </summary>
        ///
        /// <value>
        /// The server message right.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static string serverMessageRT { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the server protocol right.
        /// </summary>
        ///
        /// <value>
        /// The server protocol right.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static string serverProtocolRT { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the server socket right.
        /// </summary>
        ///
        /// <value>
        /// The server socket right.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static string serverSocketRT { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the server buffer size right.
        /// </summary>
        ///
        /// <value>
        /// The server buffer size right.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static string serverBufSizeRT { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets a value indicating whether the server is UDP right.
        /// </summary>
        ///
        /// <value>
        /// true if server is UDP right, false if not.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static bool serverIsUdpRT { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the server count right.
        /// </summary>
        ///
        /// <value>
        /// The server count right.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static int serverCountRT { get; set; }
        #endregion

        #region Non-standard Client Run-Time Settings
        //public static byte[] sendBuffer = new byte[bufferSize];
        //public static byte[] recvBuffer = new byte[bufferSize];

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets a message describing the byte.
        /// </summary>
        ///
        /// <value>
        /// A message describing the byte.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static byte[] byteMessage { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the arguments.
        /// </summary>
        ///
        /// <value>
        /// The arguments.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static string[] args { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the rectangle.
        /// </summary>
        ///
        /// <value>
        /// The rectangle.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static int rc { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// { get; set; }
        /// </summary>
        ///-------------------------------------------------------------------------------------------------

        public static byte[] sendBuffer = new byte[bufferSize];

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// { get; set; }
        /// </summary>
        ///-------------------------------------------------------------------------------------------------

        public static byte[] recvBuffer = new byte[bufferSize];

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the client socket.
        /// </summary>
        ///
        /// <value>
        /// The client socket.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static Socket clientSocket { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the resolved host.
        /// </summary>
        ///
        /// <value>
        /// The resolved host.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static IPHostEntry resolvedHost { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the Destination for the.
        /// </summary>
        ///
        /// <value>
        /// The destination.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static IPEndPoint destination { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the type of the sock.
        /// </summary>
        ///
        /// <value>
        /// The type of the sock.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static SocketType sockType { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the sock protocol.
        /// </summary>
        ///
        /// <value>
        /// The sock protocol.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static ProtocolType sockProtocol { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the name of the remote.
        /// </summary>
        ///
        /// <value>
        /// The name of the remote.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static string remoteName { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets a message describing the text.
        /// </summary>
        ///
        /// <value>
        /// A message describing the text.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static string textMessage { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// The UDP connect.
        /// </summary>
        ///-------------------------------------------------------------------------------------------------

        public static bool udpConnect = false;

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the remote port.
        /// </summary>
        ///
        /// <value>
        /// The remote port.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static int remotePort { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the size of the buffer.
        /// </summary>
        ///
        /// <value>
        /// The size of the buffer.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static int bufferSize { get; set; }
        #endregion


        #region TIMERS & CLIENT SIDE CONSOLE UI

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the stopwatch.
        /// </summary>
        ///
        /// <value>
        /// The stopwatch.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static Stopwatch stopwatch { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets a value indicating whether the waitingfor user interface.
        /// </summary>
        ///
        /// <value>
        /// true if waitingfor user interface, false if not.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static bool boolWaitingforUI { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the dictionary waitingfor user interface.
        /// </summary>
        ///
        /// <value>
        /// The dictionary waitingfor user interface.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static Dictionary<string, string> dictWaitingforUI { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets a value indicating whether the received user interface.
        /// </summary>
        ///
        /// <value>
        /// true if received user interface, false if not.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static bool boolReceivedUI { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the int waitingfor user interface time.
        /// </summary>
        ///
        /// <value>
        /// The int waitingfor user interface time.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static int intWaitingforUITime { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the user interface questions.
        /// </summary>
        ///
        /// <value>
        /// The string user interface questions.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static string[] strUIQuestions { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the int user interface questions.
        /// </summary>
        ///
        /// <value>
        /// The int user interface questions.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static int[] intUIQuestions { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the console commands.
        /// </summary>
        ///
        /// <value>
        /// The string console commands.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static string[] strConsoleCommands { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the boolconsole user interface.
        /// </summary>
        ///
        /// <value>
        /// The boolconsole user interface.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static bool[] boolconsoleUI { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the int loops waitingfor user interface.
        /// </summary>
        ///
        /// <value>
        /// The int loops waitingfor user interface.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static int intLoopsWaitingforUI { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the int time out.
        /// </summary>
        ///
        /// <value>
        /// The int time out.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static int intTimeOut { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the time out.
        /// </summary>
        ///
        /// <value>
        /// The string time out.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static string strTimeOut { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// The items.
        /// </summary>
        ///-------------------------------------------------------------------------------------------------

        private DictionaryEntry[] Items;

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// The int items in use.
        /// </summary>
        ///-------------------------------------------------------------------------------------------------

        private Int32 intItemsInUse = 0;

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the why update arguments.
        /// </summary>
        ///
        /// <value>
        /// The why update arguments.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        private static Dictionary<string, bool> WhyUpdateArgs { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the why update arguments.
        /// </summary>
        ///
        /// <value>
        /// The why update arguments.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static Dictionary<string, bool> whyUpdateArgs { get; set; }
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Main Form Initialization.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///-------------------------------------------------------------------------------------------------

        public MainForm() {
            InitializeComponent();
            //this.paneltest = new System.Windows.Forms.Panel();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Send strConsoleText the buffered text to display to the user.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <returns>
        /// An int.
        /// </returns>
        ///-------------------------------------------------------------------------------------------------

        private int UpdateConsoleText() {
            if (tbConsoleText.Text != null) {                                       // Check if Command exists
                strConsoleText = tbConsoleText.Text;                                // Assign entire string 
                tbConsoleText.Text = "";
                int iP = 0;
                strConsoleCommands = strConsoleText.Split(' ');
                MainForm.queueParams = new Queue();
                foreach (string strCmd in strConsoleCommands) {
                    if (strCmd.Contains("-")) {
                        queueParams.Enqueue(strCmd);
                        lbxConsole.SelectedIndex = lbxConsole.Items.Count - 1;  // Set focus behind List index
                        lbxConsole.SelectedIndex = -1;                          // to achieve auto-scroll effect
                        iP++;
                    }
                }
                int iC = 0;
                strConsoleCommands = strConsoleText.Split(' ');
                MainForm.queueCommands = new Queue();
                foreach (string strCmd in strConsoleCommands) {
                    if (!strCmd.Contains("-")) {
                        queueCommands.Enqueue(strCmd);
                        lbxConsole.SelectedIndex = lbxConsole.Items.Count - 1;  // Set focus behind List index
                        lbxConsole.SelectedIndex = -1;                          // to achieve auto-scroll effect
                        iC++;
                    }
                }
                return 0;
            } else if (queueServerSocket.Peek() != null) {                  // Check if Queue exists
                lbxConsole.Items.Clear();                                   // Clear out the display
                string strReceiving = "Receiving message from server";
                lbxConsole.Items.Add(strReceiving);
                string dot = ".";
                for (int iWait = 0; iWait < 1 * 800; iWait++) {             // Loop creating UI effect
                    if (dot.Length < 100) {                                 // Only to 100 dots
                        dot = dot + ".";
                        lbxConsole.Items.Insert(0, strReceiving + dot);
                    } else {
                        dot = ".";
                    }
                }
                int count = queueServerSocket.Count;                        // Count # Lines to Display
                lbxConsole.Items.Add("Printing [" + count.ToString()
                    + "] Lines:");
                Thread.Sleep(5000);                                         // Chill out
                for (int i = 0; i < count; i++) {                           // Loop through Queue
                    lbxConsole.Items.Add(queueServerSocket.Dequeue());      // Remove Queue Item[] & Add
                    lbxConsole.SelectedIndex = lbxConsole.Items.Count - 1;  // Set focus behind List index
                    lbxConsole.SelectedIndex = -1;                          // to achieve auto-scroll effect
                }
                lbxConsole.Items.Add("Finished...Exit[0]");                 // Print some success
                queueServerSocket.Clear();                                  // Clean the Queue

                return 0;
            } else {                                                        // Display an Error - Mistakes were made!
                lbxConsole.Items.Add("Error[-1] No command / No Queue from Server...");
                lbxConsole.Items.Add("Enter a command below.");
                return -1;                                                  // Return [-1] Error - Mistakes were made
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// As text is changed...
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     .
        /// </param>
        /// <param name="e">
        ///     .
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void tbConsoleText_TextChanged(object sender, EventArgs e) {
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Actions upon clicking the "enter" button.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     .
        /// </param>
        /// <param name="e">
        ///     .
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void btnEnter_Click(object sender, EventArgs e) {
            Start();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Actions when pressing the "enter" key while focus on the Console's command textbox.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     object.
        /// </param>
        /// <param name="e">
        ///     KeyEventArgs.
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void tbConsoleText_KeyDown(object sender, KeyEventArgs e) {
            string asciiKeyCode = "";
            string strArgs = "";
            string strTextLine = "";
            // Assign KeyCode in ASCII Format
            asciiKeyCode = e.KeyCode.ToString();
            MessageBox.Show(asciiKeyCode, "strKeyCode", MessageBoxButtons.OK);
            // Create Console UI Text Args
            strArgs = "The KeyCode { " + asciiKeyCode + " } was pressed. { KeyData [ " + e.KeyData.ToString() +
                        " ] / KeyValue [ " + e.KeyValue.ToString() + " ] }";

            // Send to Console 
            if (ConfirmUpdateConsole(strArgs, true, true)) {
                // Prepare next Text Line Args
                strArgs = "Reponse is needed.";
            }
            // Check if Console Text Box is Empty
            if (lbxConsoleDPScan.Text == "") {
                // If so, send Text Line Args
                bool boolResult = ConfirmUpdateConsole(strArgs, true, true);

            } else {
                // Otherwise assign Console Text Box Text 
                strTextLine = lbxConsoleDPScan.Text;
                bool boolResult = ConfirmUpdateConsole(strTextLine, true, true);
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Console action and main logic. Run when a click event occurs on the enter button or the key
        /// down event occurs within the console textbox from the user. Commands are parsed (if any)
        /// and executed. Then if any messages are available in the Queue, the text is displayed to the
        /// user within the Console.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///-------------------------------------------------------------------------------------------------

        private void Start() {

            int result = -1;                                                        // Reset Result Code
            result = UpdateConsoleText();

            if (result != 0) {
                MessageBox.Show("Major prollem  -- mistakes were made", "UpdateCTxt : Start");
            } else {
                // Testing  //
                int intC = queueCommands.Count;                                     // Count how many Commands
                int intP = queueParams.Count;                                       // Count how many Parameters
                lbxConsole.Items.Add("Processing...");
                lbxConsole.Items.Add(" [" + intC + "] COMMANDS");
                for (int i = 0; i < intC; i++) {
                    if (queueCommands.Peek() != null)
                        lbxConsole.Items.Add("Command:" + " [" + i + "] " + queueCommands.Dequeue());
                    lbxConsole.SelectedIndex = lbxConsole.Items.Count - 1;  // Set focus behind List index
                    lbxConsole.SelectedIndex = -1;                          // to achieve auto-scroll effect
                } //          //
                // Testing  //
                lbxConsole.Items.Add("Processing...");
                lbxConsole.Items.Add(" [" + intP + "] PARAMETERS");
                for (int i = 0; i < intP; i++) {
                    if (queueParams.Peek() != null)
                        lbxConsole.Items.Add("Parameter:" + " [" + i + "] " + queueParams.Dequeue());
                    lbxConsole.SelectedIndex = lbxConsole.Items.Count - 1;  // Set focus behind List index
                    lbxConsole.SelectedIndex = -1;                          // to achieve auto-scroll effect
                } // Testing  //
                //          //
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Event handler for the main StartServer button click event. Starts the SocketServer. Loads
        /// Manual Pages into the user's display console.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     .
        /// </param>
        /// <param name="e">
        ///     .
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void btnStartServer_Click(object sender, EventArgs e) {

            // ServerSocket Queue
            queueServerSocket = FillConsoleIni();                                       // Fill Queue with Manual Pages
            lbxConsole.Items.Clear();
            lbxConsole.Items.Add("Loading the SocketServer Manual Pages...");
            int count = queueServerSocket.Count;
            lbxConsole.Items.Add("Printing [" + count.ToString() + "] Lines:");
            for (int i = 0; i < count; i++) {
                lbxConsole.Items.Add(queueServerSocket.Dequeue());
                lbxConsole.SelectedIndex = lbxConsole.Items.Count - 1;
                lbxConsole.SelectedIndex = -1;
            }
            lbxConsole.Items.Add("Finished...SocketServer Manual Pages.");
            lbxConsole.Items.Add("Exit[0]");
            queueServerSocket.Clear();                                                  // Clear Queue
            // ClientSocket for TCP
            queueClientSocket = clFillConsoleIni();
            lbxTcp.Items.Clear();
            lbxTcp.Items.Add("Loading the ClientServer Manual Pages...");
            count = 0;
            count = queueClientSocket.Count;
            lbxTcp.Items.Add("Printing [" + count.ToString() + "] Lines:");
            for (int i = 0; i < count; i++) {
                lbxTcp.Items.Add(queueClientSocket.Dequeue());
                lbxTcp.SelectedIndex = lbxTcp.Items.Count - 1;
                lbxTcp.SelectedIndex = -1;
            }
            lbxTcp.Items.Add("Finished...ClientServer Manual Pages.");
            lbxTcp.Items.Add("Exit[0]");
            queueClientSocket.Clear();                                                  // Clear Queue
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Event handler for Tcp Client Start Server button click event. Simply triggers the main
        /// StartServer button click event. All Client StartServer and StopServer buttons will trigger
        /// the main button events, but may have additional features added as needed.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     .
        /// </param>
        /// <param name="e">
        ///     .
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void btnStartServerTcp_Click(object sender, EventArgs e) {

            btnStartServer_Click(sender, e);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Event Handler for IP Header Tab Selection event.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     .
        /// </param>
        /// <param name="e">
        ///     .
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void tabIPHeader_Click(object sender, EventArgs e) {

            string strSelectedTab = tabControl1.SelectedTab.ToString();
            if (strSelectedTab == "TabPage: {IP Header}") {
                SnifferForm_Load(sender, e);
                //MessageBox.Show("You clicked me", "Getting Network Interfaces", MessageBoxButtons.OK);
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Event handler for Tcp Server radio button click event. Loads selection description
        /// information.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     .
        /// </param>
        /// <param name="e">
        ///     .
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void rbTcpServer_CheckedChanged(object sender, EventArgs e) {

            Queue qrbTcpServer = new Queue();
            qrbTcpServer.Enqueue("Loading Client Tcp Server Address configuration...");
            cbxHostName.Visible = true;
            cbxIpv4.Visible = true;
            cbxIpv6.Visible = true;
            strTcpDesc = "Server HostName or IPAddress to connect/send to";
            lbTcpDesc.Text = strTcpDesc;
            if ((intTcpSrvTypeCbxFocus > -1) && (rbTcpServer.Checked)) {
                string strSvrAddr;
                strSvrAddr = Program.ServerAddress[intTcpSrvTypeCbxFocus];
                qrbTcpServer.Enqueue("Found...[ " + strSvrAddr + " ] Server Address");
                tbxTcpInput.Text = strSvrAddr;
                queueClientTcpConsole = qrbTcpServer;
                UpdateTcpConsole();
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Event handler for Tcp Port radio button click event. Loads selection description information.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     .
        /// </param>
        /// <param name="e">
        ///     .
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void rbTcpPort_CheckedChanged(object sender, EventArgs e) {

            Queue qrbTcpPort = new Queue();
            qrbTcpPort.Enqueue("Loading Client Tcp Server Port configuration...");
            cbxHostName.Visible = false;
            cbxIpv4.Visible = false;
            cbxIpv6.Visible = false;
            strTcpDesc = "Server Port Number for Client connect/send to";
            lbTcpDesc.Text = strTcpDesc;
            tbxTcpInput.Text = "";
            if (rbTcpPort.Checked) {
                int i = 0;
                string strSvrPort = "";
                strSvrPort = Program.ServerPort["tcp"];
                if (strSvrPort != null) {
                    qrbTcpPort.Enqueue("Found... Tcp Server Port : [" + strSvrPort + " ]");
                    i = i + 1;
                } else {
                    qrbTcpPort.Enqueue("Null... Tcp Server Port : [0]");
                }
                strSvrPort = Program.ServerPort["dpfs"];
                if (strSvrPort != null) {
                    qrbTcpPort.Enqueue("Found... Dpfs Server Port : [" + strSvrPort + " ]");
                    i = i + 1;
                } else {
                    qrbTcpPort.Enqueue("Null... Dpfs Server Port : [0]");
                }
                strSvrPort = Program.ServerPort["powerserver"];
                if (strSvrPort != null) {
                    qrbTcpPort.Enqueue("Found... POWERServer Server Port : [" + strSvrPort + " ]");
                    i = i + 1;
                } else {
                    qrbTcpPort.Enqueue("Null... POWERServer Server Port : [0]");
                }
                qrbTcpPort.Enqueue("Finished...Found [" + i + "] server port socket connections.");
                queueClientTcpConsole = qrbTcpPort;
                tbxTcpInput.Text = Program.ServerPort["tcp"];
                UpdateTcpConsole();
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Event handler for Message Payload radio button click event. Loads selection description
        /// information.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     .
        /// </param>
        /// <param name="e">
        ///     .
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void rbMessage_CheckedChanged(object sender, EventArgs e) {

            Queue qrbMessage = new Queue();
            qrbMessage.Enqueue("Loading Client Messages available for all Protocol Headers and Sockets...");
            cbxHostName.Visible = false;
            cbxIpv4.Visible = false;
            cbxIpv6.Visible = false;
            strTcpDesc = "String message payload to format in request buffer";
            lbTcpDesc.Text = strTcpDesc;
            tbxTcpInput.Text = "";
            if (rbMessage.Checked) {
                int i = 0;
                qrbMessage.Enqueue("Note : The client and server configuration will change the message payload(s) displayed here.");
                qrbMessage.Enqueue("Note : For example, changing the buffer size and protocol will affect the number of packets.");
                qrbMessage.Enqueue("");
                string strTcpClientMsg = "";
                strTcpClientMsg = Program.ClientMessage["tcp"];
                if (strTcpClientMsg != null) {
                    qrbMessage.Enqueue(" Client payload [TCP Message] :: " + strTcpClientMsg);
                    i = i + 1;
                    //} else if (AddMessage.Cmsg["tcp"] != null) {
                    //    strTcpClientMsg = AddMessage.Cmsg["tcp"];
                    //    qrbMessage.Enqueue(" Client payload [TCP Message] :: " + strTcpClientMsg);
                    //    i = i + 1;
                } else {
                    qrbMessage.Enqueue(" Client payload [TCP Message] :: " + "No message.");
                }
                string strIpv4ClientMsg = "";
                strIpv4ClientMsg = Program.ClientMessage["ipv4"];
                if (strIpv4ClientMsg != null) {
                    qrbMessage.Enqueue(" Client payload [IPv4 Message] :: " + strIpv4ClientMsg);
                    i = i + 1;
                    //} else if (AddMessage.Cmsg["ipv4"] != null) {
                    //    strTcpClientMsg = AddMessage.Cmsg["ipv4"];
                    //    qrbMessage.Enqueue(" Client payload [IPv4 Message] :: " + strTcpClientMsg);
                    //    i = i + 1;
                } else {
                    qrbMessage.Enqueue(" Client payload [IPv4 Message] :: " + "No message.");
                }
                string strUnknownClientMsg = "";
                strUnknownClientMsg = Program.ClientMessage["unknown"];
                if (strUnknownClientMsg != null) {
                    qrbMessage.Enqueue(" Client payload [unknown Message] :: " + strUnknownClientMsg);
                    i = i + 1;
                    //} else if (AddMessage.Cmsg["unknown"] != null) {
                    //    strTcpClientMsg = AddMessage.Cmsg["unknown"];
                    //    qrbMessage.Enqueue(" Client payload [unknown Message] :: " + strTcpClientMsg);
                    //    i = i + 1;
                } else {
                    qrbMessage.Enqueue(" Client payload [unknown Message] :: " + "No message.");
                }
                string strRawClientMsg = "";
                strRawClientMsg = Program.ClientMessage["raw"];
                if (strRawClientMsg != null) {
                    qrbMessage.Enqueue(" Client payload [RAW Message] :: " + strRawClientMsg);
                    i = i + 1;
                    //} else if (AddMessage.Cmsg["raw"] != null) {
                    //    strTcpClientMsg = AddMessage.Cmsg["raw"];
                    //    qrbMessage.Enqueue(" Client payload [RAW Message] :: " + strTcpClientMsg);
                    //    i = i + 1;
                } else {
                    qrbMessage.Enqueue(" Client payload [RAW Message] :: " + "No message.");
                }
                string strUdpClientMsg = "";
                strUdpClientMsg = Program.ClientMessage["udp"];
                if (strUdpClientMsg != null) {
                    qrbMessage.Enqueue(" Client payload [UDP Message] :: " + strUdpClientMsg);
                    i = i + 1;
                    //} else if (AddMessage.Cmsg["udp"] != null) {
                    //    strTcpClientMsg = AddMessage.Cmsg["udp"];
                    //    qrbMessage.Enqueue(" Client payload [UDP Message] :: " + strTcpClientMsg);
                    //    i = i + 1;
                } else {
                    qrbMessage.Enqueue(" Client payload [UDP Message] :: " + "No message.");
                }
                string strIpv6ClientMsg = "";
                strIpv6ClientMsg = Program.ClientMessage["ipv6"];
                if (strIpv4ClientMsg != null) {
                    qrbMessage.Enqueue(" Client payload [IPv6 Message] :: " + strIpv6ClientMsg);
                    i = i + 1;
                    //} else if (AddMessage.Cmsg["ipv6"] != null) {
                    //    strTcpClientMsg = AddMessage.Cmsg["ipv6"];
                    //    qrbMessage.Enqueue(" Client payload [IPv6 Message] :: " + strTcpClientMsg);
                    //    i = i + 1;
                } else {
                    qrbMessage.Enqueue(" Client payload [IPv6 Message] :: " + "No message.");
                }
                string strIpv6FragClientMsg = "";
                strIpv6FragClientMsg = Program.ClientMessage["ipv6frag"];
                if (strIpv6FragClientMsg != null) {
                    qrbMessage.Enqueue(" Client payload [IPv6Frag Message] :: " + strIpv6FragClientMsg);
                    i = i + 1;
                    //} else if (AddMessage.Cmsg["ipv6frag"] != null) {
                    //    strTcpClientMsg = AddMessage.Cmsg["ipv6frag"];
                    //    qrbMessage.Enqueue(" Client payload [IPv6Frag Message] :: " + strTcpClientMsg);
                    //    i = i + 1;
                } else {
                    qrbMessage.Enqueue(" Client payload [IPv6Frag Message] :: " + "No message.");
                }
                string strIpClientMsg = "";
                strIpClientMsg = Program.ClientMessage["ip"];
                if (strIpClientMsg != null) {
                    qrbMessage.Enqueue(" Client payload [IP Message] :: " + strIpClientMsg);
                    i = i + 1;
                    //} else if (AddMessage.Cmsg["ip"] != null) {
                    //    strTcpClientMsg = AddMessage.Cmsg["ip"];
                    //    qrbMessage.Enqueue(" Client payload [IP Message] :: " + strTcpClientMsg);
                    //    i = i + 1;
                } else {
                    qrbMessage.Enqueue(" Client payload [IP Message] :: " + "No message.");
                }
                string strLcmpClientMsg = "";
                strLcmpClientMsg = Program.ClientMessage["lcmp"];
                if (strLcmpClientMsg != null) {
                    qrbMessage.Enqueue(" Client payload [Lcmp Message] :: " + strLcmpClientMsg);
                    i = i + 1;
                    //} else if (AddMessage.Cmsg["lcmp"] != null) {
                    //    strTcpClientMsg = AddMessage.Cmsg["lcmp"];
                    //    qrbMessage.Enqueue(" Client payload [Lcmp Message] :: " + strTcpClientMsg);
                    //    i = i + 1;
                } else {
                    qrbMessage.Enqueue(" Client payload [Lcmp Message] :: " + "No message.");
                }
                string strLgmpClientMsg = "";
                strLgmpClientMsg = Program.ClientMessage["lgmp"];
                if (strLgmpClientMsg != null) {
                    qrbMessage.Enqueue(" Client payload [Lgmp Message] :: " + strLgmpClientMsg);
                    i = i + 1;
                    //} else if (AddMessage.Cmsg["lgmp"] != null) {
                    //    strTcpClientMsg = AddMessage.Cmsg["lgmp"];
                    //    qrbMessage.Enqueue(" Client payload [Lgmp Message] :: " + strTcpClientMsg);
                    //    i = i + 1;
                } else {
                    qrbMessage.Enqueue(" Client payload [Lgmp Message] :: " + "No message.");
                }
                string strIpxClientMsg = "";
                strIpxClientMsg = Program.ClientMessage["ipx"];
                if (strIpxClientMsg != null) {
                    qrbMessage.Enqueue(" Client payload [Ipx Message] :: " + strIpxClientMsg);
                    i = i + 1;
                    //} else if (AddMessage.Cmsg["ipx"] != null) {
                    //    strTcpClientMsg = AddMessage.Cmsg["ipx"];
                    //    qrbMessage.Enqueue(" Client payload [Ipx Message] :: " + strTcpClientMsg);
                    //    i = i + 1;
                } else {
                    qrbMessage.Enqueue(" Client payload [Ipx Message] :: " + "No message.");
                }
                qrbMessage.Enqueue("Finished...Found [" + i + "] Client message payloads ready to go.");
                queueClientTcpConsole = qrbMessage;
                UpdateTcpConsole();
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Event handler for BufferSize radio button click event. Loads selection description
        /// information.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     .
        /// </param>
        /// <param name="e">
        ///     .
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void rbSizeBf_CheckedChanged(object sender, EventArgs e) {

            int intC = 0;
            Queue qbSizeBf = new Queue();
            string buff = "";
            tbxTcpInput.Text = "";
            qbSizeBf.Enqueue("Loading Client Buffer Size configuration...");
            cbxHostName.Visible = false;
            cbxIpv4.Visible = false;
            cbxIpv6.Visible = false;
            strTcpDesc = "Size of send and receive buffers";
            lbTcpDesc.Text = strTcpDesc;
            buff = "4096";
            // fix later
            // Took out Enum
            if (buff != null) {
                qbSizeBf.Enqueue("Found...Tcp Buffer Size Settings : Limit [" + buff + "] .");
                intC++;
                tbxTcpInput.Text = buff;
            } else {
                qbSizeBf.Enqueue("...Tcp Buffer Size Settings Not Found : Limit [0] .");
            }
            //
            // Same here
            //buff = Program.ClientSize["dpfs"];
            buff = "512";
            if (buff != null) {
                qbSizeBf.Enqueue("Found...Dpfs Buffer Size Settings : Limit [" + buff + "] .");
                intC++;
            } else {
                qbSizeBf.Enqueue("...Dpfs Buffer Size Settings Not Found : Limit [0] .");
            }
            //
            // And again
            // I took out the dynamic calc for Byte size
            //buff = Program.ClientSize["powerserver"];
            buff = "1024";
            if (buff != null) {
                qbSizeBf.Enqueue("Found...POWERServer Buffer Size Settings : Limit [" + buff + "] .");
                intC++;
            } else {
                qbSizeBf.Enqueue("...POWERServer Buffer Size Settings Not Found : Limit [0] .");
            }
            qbSizeBf.Enqueue("Finished...Updated Buffer Size :  Bytes = [" + buff + "] for [" + intC + "] Client 'outgoing' messages payloads.");
            queueClientTcpConsole = qbSizeBf;
            UpdateTcpConsole();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// An event handler for the Save Button click even. It saves new configuration settings for the
        /// radio button that has focus.  This utility does not save configuration settings locally.
        /// Keep in mind for future versioning.  
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     .
        /// </param>
        /// <param name="e">
        ///     .
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void btnTcpOptSave_Click(object sender, EventArgs e) {

            Queue qTcpOptSave = new Queue();
            qTcpOptSave.Enqueue("Saving Client Tcp Configuration...");
            string strOpt = FindTcpRadioButton();
            UpdateTcpConsole();
            if (strOpt == "s") {
                string strSvrAddr = tbxTcpInput.Text;
                Program.ServerAddress[intTcpSrvTypeCbxFocus] = strSvrAddr;
                qTcpOptSave.Enqueue("New settings found for ServerAddress.");
                if (intTcpSrvTypeCbxFocus == 0)
                    qTcpOptSave.Enqueue("[ " + strSvrAddr + " ] has been saved as the HostName Server Address.");
                clientDestAddrRT = strSvrAddr;
                if (intTcpSrvTypeCbxFocus == 1)
                    qTcpOptSave.Enqueue("[ " + strSvrAddr + " ] has been saved as the IPv4 Server Address.");
                clientDestAddrRT = strSvrAddr;
                if (intTcpSrvTypeCbxFocus == 2)
                    qTcpOptSave.Enqueue("[ " + strSvrAddr + " ] has been saved as the IPv6 Server Address.");
                clientDestAddrRT = strSvrAddr;
            }
            if (strOpt == "x") {
                string strBufSize = tbxTcpInput.Text;
                //Program.ClientSize["tcp"] = strBufSize;
                qTcpOptSave.Enqueue("New settings found for ServerAddress.");
                if (intTcpSrvTypeCbxFocus == 0)
                    qTcpOptSave.Enqueue("[ " + strBufSize + " ] has been saved as the HostName Server Address.");
                clientDestAddrRT = strBufSize;
                if (intTcpSrvTypeCbxFocus == 1)
                    qTcpOptSave.Enqueue("[ " + strBufSize + " ] has been saved as the IPv4 Server Address.");
                clientDestAddrRT = strBufSize;
                if (intTcpSrvTypeCbxFocus == 2)
                    qTcpOptSave.Enqueue("[ " + strBufSize + " ] has been saved as the IPv6 Server Address.");
                clientDestAddrRT = strBufSize;
            }
            if (strOpt == "m") {
                int intM = 0;
                string strMPl = tbxTcpInput.Text;
                Program.ClientMessage["ipv4"] = strMPl;
                clientMessageRT = strMPl;
                qTcpOptSave.Enqueue("Looking for new message payload(s) ready to be sent to Server.");
                strMPl = Program.ClientMessage["unknown"];
                if ((strMPl != null) && (strMPl.Length > 0)) {
                    qTcpOptSave.Enqueue("Found...Message of Header Type [ unknown ].");
                    qTcpOptSave.Enqueue("Message : [ " + strMPl + " ].");
                    clientMessageRT = strMPl;
                    intM++;
                } else {
                    qTcpOptSave.Enqueue("...Message of Header Type [ unknown ]...Not Found!");
                }
                strMPl = Program.ClientMessage["raw"];
                if ((strMPl != null) && (strMPl.Length > 0)) {
                    qTcpOptSave.Enqueue("Found...Message of Header Type [ raw ].");
                    qTcpOptSave.Enqueue("Message : [ " + strMPl + " ].");
                    clientMessageRT = strMPl;
                    intM++;
                } else {
                    qTcpOptSave.Enqueue("...Message of Header Type [ raw ]...Not Found!");
                }
                strMPl = Program.ClientMessage["tcp"];
                if ((strMPl != null) && (strMPl.Length > 0)) {
                    qTcpOptSave.Enqueue("Found...Message of Header Type [ tcp ].");
                    qTcpOptSave.Enqueue("Message : [ " + strMPl + " ].");
                    clientMessageRT = strMPl;
                    intM++;
                } else {
                    qTcpOptSave.Enqueue("...Message of Header Type [ tcp ]...Not Found!");
                }
                strMPl = Program.ClientMessage["udp"];
                if ((strMPl != null) && (strMPl.Length > 0)) {
                    qTcpOptSave.Enqueue("Found...Message of Header Type [ udp ].");
                    qTcpOptSave.Enqueue("Message : [ " + strMPl + " ].");
                    clientMessageRT = strMPl;
                    intM++;
                } else {
                    qTcpOptSave.Enqueue("...Message of Header Type [ udp ]...Not Found!");
                }
                strMPl = Program.ClientMessage["ipv6"];
                if ((strMPl != null) && (strMPl.Length > 0)) {
                    qTcpOptSave.Enqueue("Found...Message of Header Type [ ipv6 ].");
                    qTcpOptSave.Enqueue("Message : [ " + strMPl + " ].");
                    clientMessageRT = strMPl;
                    intM++;
                } else {
                    qTcpOptSave.Enqueue("...Message of Header Type [ ipv6 ]...Not Found!");
                }
                strMPl = Program.ClientMessage["ipv6frag"];
                if ((strMPl != null) && (strMPl.Length > 0)) {
                    qTcpOptSave.Enqueue("Found...Message of Header Type [ ipv6frag ].");
                    qTcpOptSave.Enqueue("Message : [ " + strMPl + " ].");
                    clientMessageRT = strMPl;
                    intM++;
                } else {
                    qTcpOptSave.Enqueue("...Message of Header Type [ ipv6frag ]...Not Found!");
                }
                strMPl = Program.ClientMessage["ip"];
                if ((strMPl != null) && (strMPl.Length > 0)) {
                    qTcpOptSave.Enqueue("Found...Message of Header Type [ ip ].");
                    qTcpOptSave.Enqueue("Message : [ " + strMPl + " ].");
                    clientMessageRT = strMPl;
                    intM++;
                } else {
                    qTcpOptSave.Enqueue("...Message of Header Type [ ip ]...Not Found!");
                }
                strMPl = Program.ClientMessage["lcmp"];
                if ((strMPl != null) && (strMPl.Length > 0)) {
                    qTcpOptSave.Enqueue("Found...Message of Header Type [ lcmp ].");
                    qTcpOptSave.Enqueue("Message : [ " + strMPl + " ].");
                    intM++;
                } else {
                    qTcpOptSave.Enqueue("...Message of Header Type [ lcmp ]...Not Found!");
                }
                strMPl = Program.ClientMessage["lgmp"];
                if ((strMPl != null) && (strMPl.Length > 0)) {
                    qTcpOptSave.Enqueue("Found...Message of Header Type [ lgmp ].");
                    qTcpOptSave.Enqueue("Message : [ " + strMPl + " ].");
                    clientMessageRT = strMPl;
                    intM++;
                } else {
                    qTcpOptSave.Enqueue("...Message of Header Type [ lgmp ]...Not Found!");
                }
                strMPl = Program.ClientMessage["ipx"];
                if ((strMPl != null) && (strMPl.Length > 0)) {
                    qTcpOptSave.Enqueue("Found...Message of Header Type [ ipx ].");
                    qTcpOptSave.Enqueue("Message : [ " + strMPl + " ].");
                    clientMessageRT = strMPl;
                    intM++;
                } else {
                    qTcpOptSave.Enqueue("...Message of Header Type [ ipx ]...Not Found!");
                }
                strMPl = Program.ClientMessage["ipv4"];
                if ((strMPl != null) && (strMPl.Length > 0)) {
                    qTcpOptSave.Enqueue("Found...Message of Header Type [ ipv4 ].");
                    qTcpOptSave.Enqueue("Message : [ " + strMPl + " ].");
                    clientMessageRT = strMPl;
                    intM++;
                } else {
                    qTcpOptSave.Enqueue("...Message of Header Type [ ipv4 ]...Not Found!");
                }
                qTcpOptSave.Enqueue("NOTE : All settings are discarded on application restart.");
                qTcpOptSave.Enqueue("Finished...Found [" + intM + "] message payloads successfully.");
                MainForm.queueClientTcpConsole = qTcpOptSave;
                UpdateTcpConsole();
            }
            if (strOpt == "p") {
                int intP = 0;
                string strP = tbxTcpInput.Text;
                Program.ServerPort["tcp"] = strP;
                clientDestPortRT = strP;
                qTcpOptSave.Enqueue("Looking for changes to destination ports.");
                strP = Program.ServerPort["tcp"];
                if ((strP != null) && (strP.Length > 0)) {
                    qTcpOptSave.Enqueue("Found...Port for [ tcp ].");
                    qTcpOptSave.Enqueue("Port Number # : [ " + strP + " ].");
                    clientMessageRT = strP;
                    intP++;
                } else {
                    qTcpOptSave.Enqueue("...Port number was not found for [ tcp ]!");
                }
                strP = Program.ServerPort["dpfs"];
                if ((strP != null) && (strP.Length > 0)) {
                    qTcpOptSave.Enqueue("Found...Port for [ dpfs ].");
                    qTcpOptSave.Enqueue("Port Number # : [ " + strP + " ].");
                    clientMessageRT = strP;
                    intP++;
                } else {
                    qTcpOptSave.Enqueue("...Port number was not found for [ dpfs ]!");
                }
                strP = Program.ServerPort["powerserver"];
                if ((strP != null) && (strP.Length > 0)) {
                    qTcpOptSave.Enqueue("Found...Port for [ powerserver ].");
                    qTcpOptSave.Enqueue("Port Number # : [ " + strP + " ].");
                    clientMessageRT = strP;
                    intP++;
                } else {
                    qTcpOptSave.Enqueue("...Port number was not found for [ powerserver ]!");
                }
                qTcpOptSave.Enqueue("NOTE : All settings are discarded on application restart.");
                qTcpOptSave.Enqueue("Finished...Found [" + intP + "] socket ports to available servers!");
                MainForm.queueClientTcpConsole = qTcpOptSave;
                UpdateTcpConsole();
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Function that parses and updates the Client Tcp Console using the queueClientTcpConsole Queue.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///-------------------------------------------------------------------------------------------------

        private void UpdateTcpConsole() {

            int intQue = 0;
            intQue = queueClientTcpConsole.Count;
            for (int i = 0; i < intQue; i++) {
                lbxTcp.Items.Add(queueClientTcpConsole.Dequeue());
                lbxTcp.SelectedIndex = lbxTcp.Items.Count - 1;      // Set focus behind List index
                lbxTcp.SelectedIndex = -1;                          // to achieve auto-scroll effect
            }
            lbxTcp.Items.Add("Exit[0]");
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// An event handler for the Default Button click event.  It sets the default value for the
        /// configuration setting radio button that has focus.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     .
        /// </param>
        /// <param name="e">
        ///     .
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void btnDefault_Click(object sender, EventArgs e) {

            string strOpt = FindTcpRadioButton();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets the current Tcp Client Configuration Radio Button selected. [Server] [BufferSize]
        /// [Message/Payload] [Port] The TcpServer selection returns a value of [s] and more checks are
        /// done later on to determine if the configuration is for an address [l] or hostname [n].
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <returns>
        /// Config Option Focus.
        /// </returns>
        ///-------------------------------------------------------------------------------------------------

        public string FindTcpRadioButton() {

            Queue qFindTcpRadioButton = new Queue();
            qFindTcpRadioButton.Enqueue("Checking for configuration changes...");
            string x = "";
            string strType = "";
            if (rbTcpServer.Checked) {
                x = "s";
                strType = "Server Address Settings";
            } else if (rbSizeBf.Checked) {
                x = "x";
                strType = "Server Buffer Size Settings";
            } else if (rbMessage.Checked) {
                x = "m";
                strType = "Message payload";
            } else if (rbTcpPort.Checked) {
                x = "p";
                strType = "Server Port Settings";
            }
            qFindTcpRadioButton.Enqueue("Found..." + " [ " + strType + " ]");
            MainForm.queueClientTcpConsole = qFindTcpRadioButton;

            return x;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// The default setting for Server Address.  The hostname value is a string type initially set to
        /// "localhost."  Check to make sure the server side is also set to this default value. If a new
        /// address was entered in the ServerAddress dictionary is updated with the new value.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     .
        /// </param>
        /// <param name="e">
        ///     .
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void cbxHostName_CheckedChanged(object sender, EventArgs e) {

            if (cbxHostName.Checked) {
                intTcpSrvTypeCbxFocus = 0;
                cbxIpv4.Checked = false;
                cbxIpv6.Checked = false;
                DialogResult msgboxHn = MessageBox.Show("Change the Server Address to: HostName?", "Server Address [HostName]", MessageBoxButtons.YesNo);
                if (msgboxHn == DialogResult.Yes) {
                    string strSvrAddr = Program.ServerAddress[0];
                    if ((strSvrAddr != "") && (strSvrAddr != "localhost")) {
                        tbxTcpInput.Text = Program.ServerAddress[0];
                    } else if ((tbxTcpInput.Text == "") || (tbxTcpInput.Text == "localhost")) {
                        tbxTcpInput.Text = Program.ServerAddress[0];
                    }
                    tbxTcpInput.Text = Program.ServerAddress[0];
                    if (intTcpSrvTypeCbxFocus != 0) {
                        cbxHostName.Checked = false;
                        tbxTcpInput.Text = Program.ServerAddress[intTcpSrvTypeCbxFocus];
                    }
                    if (!(cbxHostName.Checked) && !(cbxIpv4.Checked) && !(cbxIpv6.Checked)) {
                    } else if ((cbxHostName.Checked) && !(cbxIpv4.Checked) && !(cbxIpv6.Checked)) {
                        if ((tbxTcpInput.Text != "") && (tbxTcpInput.Text != "localhost")) {
                        } else if (tbxTcpInput.Text == "") {
                            strSvrAddr = Program.ServerAddress[0];
                        }
                    }
                } else {
                    cbxHostName.Checked = false;
                }
            }
        }



        #region checkbxHostNameworking
        //if (cbxHostName.Checked) {
        //    intTcpSrvTypeCbxFocus = 0;
        //    cbxIpv4.Checked = false;
        //    cbxIpv6.Checked = false;
        //}

        //if (intTcpSrvTypeCbxFocus != 0) {
        //    cbxHostName.Checked = false;
        //    tbxTcpInput.Text = Program.ServerAddress[intTcpSrvTypeCbxFocus];
        //}

        //if (!(cbxHostName.Checked) && !(cbxIpv4.Checked) && !(cbxIpv6.Checked)) {
        //    cbxHostName.Checked = true;

        //} else if ((cbxHostName.Checked) && !(cbxIpv4.Checked) && !(cbxIpv6.Checked)) {

        //    DialogResult msgboxHn = MessageBox.Show("Change the Server Address to: HostName?", "Server Address [HostName]", MessageBoxButtons.YesNo);
        //    if (tbxTcpInput.Text != "") {
        //        string strSvrAddr = tbxTcpInput.Text;
        //        if (msgboxHn == DialogResult.Yes) {
        //            Program.ServerAddress[0] = strSvrAddr;
        //        }
        //    }
        //    string xHn;
        //    xHn = Program.ServerAddress[0];
        //    tbxTcpInput.Text = xHn;
        //} 
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// The IPv4 default value is a string "0.0.0.0" and later converted into a Byte[4] 32bit array
        /// as an IPAddress endpoint. If a new address was entered in the ServerAddress dictionary is
        /// updated with the new value.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     .
        /// </param>
        /// <param name="e">
        ///     .
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void cbxIpv4_CheckedChanged(object sender, EventArgs e) {

            if (cbxIpv4.Checked) {
                intTcpSrvTypeCbxFocus = 1;
                cbxHostName.Checked = false;
                cbxIpv6.Checked = false;
                DialogResult msgboxIpv4 = MessageBox.Show("Change the Server Address to: IPv4?", "Server Address [IPv4]", MessageBoxButtons.YesNo);
                if (msgboxIpv4 == DialogResult.Yes) {
                    string strSvrAddr = tbxTcpInput.Text;
                    if ((strSvrAddr != "") && (strSvrAddr != "0.0.0.0")) {
                        tbxTcpInput.Text = Program.ServerAddress[1];
                    } else if ((strSvrAddr == "") || (strSvrAddr == "0.0.0.0")) {
                        tbxTcpInput.Text = Program.ServerAddress[1];
                    }
                    tbxTcpInput.Text = Program.ServerAddress[1];
                    if (intTcpSrvTypeCbxFocus != 1) {
                        cbxIpv4.Checked = false;
                        tbxTcpInput.Text = Program.ServerAddress[intTcpSrvTypeCbxFocus];
                    }
                    if (!(cbxIpv4.Checked) && !(cbxHostName.Checked) && !(cbxIpv6.Checked)) {
                    } else if ((cbxIpv4.Checked) && !(cbxHostName.Checked) && !(cbxIpv6.Checked)) {
                        if ((tbxTcpInput.Text != "") && (tbxTcpInput.Text != "0.0.0.0")) {
                        } else if (tbxTcpInput.Text == "") {
                            strSvrAddr = Program.ServerAddress[1];
                        }
                    }
                } else {
                    cbxIpv4.Checked = false;
                }
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// The IPv6 default value is a string "::" and later converted into a Byte[8] 64bit array as an
        /// IPAddress endpoint. If a new address was entered in the ServerAddress dictionary is updated
        /// with the new value.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     .
        /// </param>
        /// <param name="e">
        ///     .
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void cbxIpv6_CheckedChanged(object sender, EventArgs e) {

            if (cbxIpv6.Checked) {
                intTcpSrvTypeCbxFocus = 2;
                cbxHostName.Checked = false;
                cbxIpv4.Checked = false;
                DialogResult msgboxIpv6 = MessageBox.Show("Change the Server Address to: IPv6?", "Server Address [IPv6]", MessageBoxButtons.YesNo);
                if (msgboxIpv6 == DialogResult.Yes) {
                    string strSvrAddr = tbxTcpInput.Text;
                    if ((strSvrAddr != "") && (strSvrAddr != "::")) {
                        tbxTcpInput.Text = Program.ServerAddress[2];
                    } else if ((strSvrAddr == "") || (strSvrAddr == "::")) {
                        tbxTcpInput.Text = Program.ServerAddress[2];
                    }
                    tbxTcpInput.Text = Program.ServerAddress[2];
                    if (intTcpSrvTypeCbxFocus != 2) {
                        cbxIpv6.Checked = false;
                        tbxTcpInput.Text = Program.ServerAddress[intTcpSrvTypeCbxFocus];
                    }
                    if (!(cbxIpv6.Checked) && !(cbxHostName.Checked) && !(cbxIpv4.Checked)) {
                    } else if ((cbxIpv6.Checked) && !(cbxHostName.Checked) && !(cbxIpv4.Checked)) {
                        if ((tbxTcpInput.Text != "") && (tbxTcpInput.Text != "::")) {
                        } else if (tbxTcpInput.Text == "") {
                            strSvrAddr = Program.ServerAddress[2];
                        }
                    }
                } else {
                    cbxIpv6.Checked = false;
                }
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Event handler for Client Message Button click event. Opens a new form to load, edit, and save
        /// a message for any registered Protocol Header.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     .
        /// </param>
        /// <param name="e">
        ///     .
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void btnCMessage_Click(object sender, EventArgs e) {

            boolSorC = false;
            _openAddMsg++;
            AddMessage cAddMessage = new AddMessage();
            cAddMessage.Show();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Event handler for Server Message Button click event. Opens a new form to load, edit, and save
        /// a message for any registered Protocol Header.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     .
        /// </param>
        /// <param name="e">
        ///     .
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void btnSMessage_Click(object sender, EventArgs e) {

            string strMessageButton = "";
            strMessageButton = sender.ToString();
            if (strMessageButton.Contains("Server")) {
                boolSorC = true;
                _openAddMsg++;
                AddMessage sAddMessage = new AddMessage();
                sAddMessage.Name = "Server :: Add Message to Send ==> Client";
                sAddMessage.Show();
            } else {
                MessageBox.Show("Mistakes were made.", "Whoops!", MessageBoxButtons.OK);
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Client Server Start Event Handler At this point it is pretty much assumed the following
        /// properties have been set.  If not, what will happen are some default values configured in the
        /// client run-time function. clientIsUdp = Udp? clientSocket = Socket Type clientDestAddr =
        /// Server Address clientDestPort = Server Port clientMessage = Message Payload clientProtocol =
        /// Protocol Header clientBufSize = Buffer Size.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     .
        /// </param>
        /// <param name="e">
        ///     .
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void btnStartClientTcp_Click(object sender, EventArgs e) {

            int s = 0;
            switch (s) {
                case 0:                                                     // Udp connection initialization
                    if (clientIsUdpRT) {
                        goto case 5;
                    } else {
                        goto case 5;
                    }
                case 5:                                                     // Server address
                    if (clientDestAddrRT != null) {
                        goto case 10;
                    } else {
                        clientDestAddrRT = "0.0.0.0";
                        goto case 10;
                    }
                case 10:                                                    // Port
                    if (clientDestPortRT != null) {
                        goto case 15;
                    } else {
                        clientDestPortRT = "5150";
                        goto case 15;
                    }
                case 15:                                                    // Message
                    if (clientMessageRT != null) {
                        goto case 20;
                    } else {
                        clientMessageRT = "This is a Test. ClientServer.";
                        goto case 20;
                    }
                case 20:                                                    // Tcp|Udp
                    if (clientIsUdpRT) {
                        goto case 25;
                    } else {
                        goto case 25;
                    }
                case 25:                                                    // Size
                    if (clientBufSizeRT != null) {
                        goto case 68;
                    } else {
                        clientBufSizeRT = "1024";
                        goto case 68;
                    }
                case 30:
                    break;
                case 35:
                    break;
                case 68:
                    RunClient();
                    break;
                case 79:
                    break;
                default:
                    break;
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// This routine repeatedly copies a string message into a byte array until filled.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="dataBuffer">
        ///     Byte buffer to fill with string message.
        /// </param>
        /// <param name="message">
        ///     String message to encode.
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        public static void FormatBuffer(byte[] dataBuffer, string message) {

            byteMessage = System.Text.Encoding.ASCII.GetBytes(message);                 // Pull message into encoded Byte[0]
            int index = 0;                                                              // Start counter for dataBuffer[]
            // First convert the string to bytes and then copy into send buffer
            while (index < dataBuffer.Length) {                                         // Loop int with [int].Length
                for (int j = 0; j < byteMessage.Length; j++) {
                    dataBuffer[index] = byteMessage[j];
                    index++;
                    // Make sure we don't go past the send buffer length
                    if (index >= dataBuffer.Length) {
                        break;
                    }
                }
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// This is the main function for the simple client. It parses the command line and creates a
        /// socket of the requested type. For TCP, it will resolve the name and attempt to connect to
        /// each resolved address until a successful connection is made. Once connected a request message
        /// is sent followed by shutting down the send connection. The client then receives data until
        /// the server closes its side at which point the client socket is closed. For UDP, the socket is
        /// created and if indicated connected to the server's address. A single request datagram
        /// message. The client then waits to receive a response and continues to do so until a zero byte
        /// datagram is receive which indicates the end of the response.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///-------------------------------------------------------------------------------------------------

        public void RunClient() {

            // Here is your default configuration
            SocketType sockType = SocketType.Stream;
            ProtocolType sockProtocol = ProtocolType.Tcp;
            remoteName = "localhost";
            textMessage = "Client: This is a test";
            udpConnect = false;
            remotePort = 5150;
            bufferSize = 4096;
            // Let's see if there are different settings configured right now.
            int s = 0;
            switch (s) {
                case 0:                         // "Connect" the UDP socket to the destination
                    if (clientIsUdpRT) {
                        udpConnect = true;
                        goto case 5;
                    }
                    goto case 10;
                case 5:
                    MessageBox.Show("Udp Connect is required : Socket to Destination.");
                    goto case 10;
                case 10:                        // Destination address to connect to or send to
                    if (clientDestAddrRT != null) {
                        remoteName = clientDestAddrRT;
                        goto case 30;
                    }
                    goto case 20;
                case 20:                        // Text message to put into the send buffer
                    if (clientMessageRT != null)
                        textMessage = clientMessageRT;
                    goto case 30;
                case 30:                        // Port number for the destination
                    if (clientDestPortRT != null) {
                        remotePort = System.Convert.ToInt32(clientDestPortRT);
                    }
                    goto case 40;
                case 40:                        // Specified TCP or UDP
                    if (clientSocketRT == "dgram") {
                        sockType = SocketType.Dgram;
                        sockProtocol = ProtocolType.Udp;
                    }
                    if (clientIsUdpRT) {
                        sockType = SocketType.Stream;
                        sockProtocol = ProtocolType.Tcp;
                        return;
                    }
                    if (clientProtocolRT == "udp") {
                    }
                    goto case 50;
                case 50:                        // Size of the send and receive buffers
                    if (clientBufSizeRT != null)
                        bufferSize = System.Convert.ToInt32(clientBufSizeRT);
                    break;
            }
            // Format the string message into the send buffer
            FormatBuffer(sendBuffer, textMessage);
            try {
                // Try to resolve the remote host name or address
                resolvedHost = Dns.GetHostEntry(remoteName);
                // Client: GetHostEntry() is OK...");
                // Try each address returned
                foreach (IPAddress addr in resolvedHost.AddressList) {
                    // Create a socket corresponding to the address family of the resolved address
                    clientSocket = new Socket(
                        addr.AddressFamily,
                        sockType,
                        sockProtocol
                        );
                    // Client: Socket() is OK...");
                    try {
                        // Create the endpoint that describes the destination
                        destination = new IPEndPoint(addr, remotePort);
                        // Client: IPEndPoint() is OK. IP Address: {0}, server port: {1}", addr, remotePort);
                        if ((sockProtocol == ProtocolType.Udp)
                           && (udpConnect == false)) {
                            // Client: Destination address is: {0}", destination.ToString());
                            break;
                        } else {
                            // Client: Attempting connection to: {0}", destination.ToString());
                        }
                        clientSocket.Connect(destination);
                        // Client: Connect() is OK...");
                        break;
                    } catch (SocketException) {
                        // Connect failed, so close the socket and try the next address
                        clientSocket.Close();
                        // Client: Close() is OK...");
                        clientSocket = null;
                        continue;
                    }
                }
                // Make sure we have a valid socket before trying to use it
                if ((clientSocket != null)
                    && (destination != null)) {
                    try {
                        // Send the Udp request to the server
                        if ((sockProtocol == ProtocolType.Udp)
                           && (udpConnect == false)) {
                            clientSocket.SendTo(sendBuffer, destination);
                            // Client: SendTo() is OK...UDP...");
                        } else {
                            rc = clientSocket.Send(sendBuffer);
                            // Client: send() is OK...TCP...");
                            // Client: Sent request of {0} bytes", rc);
                            // For TCP, shutdown sending on our side since the client won't send any more data
                            if (sockProtocol == ProtocolType.Tcp) {
                                clientSocket.Shutdown(SocketShutdown.Send);
                                // Client: Shutdown() is OK...");
                            }
                        }
                        // Receive data in a loop until the server closes the connection. For
                        //    TCP this occurs when the server performs a shutdown or closes
                        //    the socket. For UDP, we'll know to exit when the remote host
                        //    sends a zero byte datagram.
                        while (true) {
                            if ((sockProtocol == ProtocolType.Tcp)
                               && (udpConnect == true)) {
                                rc = clientSocket.Receive(recvBuffer);
                                // Client: Receive() is OK...");
                                // Client: Read {0} bytes", rc);
                            } else {
                                IPEndPoint fromEndPoint = new IPEndPoint(destination.Address, 0);
                                // Client: IPEndPoint() is OK...");
                                EndPoint castFromEndPoint = (EndPoint)fromEndPoint;
                                rc = clientSocket.ReceiveFrom(recvBuffer, ref castFromEndPoint);
                                // Client: ReceiveFrom() is OK...");
                                fromEndPoint = (IPEndPoint)castFromEndPoint;
                                // Client: Read {0} bytes from {1}", rc, fromEndPoint.ToString());
                            }
                            // Exit loop if server indicates shutdown
                            if (rc == 0) {
                                clientSocket.Close();
                                // Client: Close() is OK...");
                                break;
                            }
                        }
                    } catch (SocketException err) {
                        // Client: Error occurred while sending or receiving data.");
                        // "   Error: {0}", err.Message);
                    }
                } else {
                    // Client: Unable to establish connection to server!");
                }
            } catch (SocketException err) {
                // Client: Socket error occurred: {0}", err.Message);
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Creates a Queue of the Manual for ServerSocket.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <returns>
        /// Object manInitialize "Queue".
        /// </returns>
        ///-------------------------------------------------------------------------------------------------

        public static Queue FillConsoleIni() {
            //make object of the queue class
            Queue manInitialize = new Queue();
            //insert the item in queue by Enqueue method
            manInitialize.Enqueue("");
            manInitialize.Enqueue("// This is a simple TCP and UDP server. A socket of the requested type is created that");
            manInitialize.Enqueue("// waits for clients. For TCP, the server waits for an incoming TCP connection after which");
            manInitialize.Enqueue("// it receives a 'request'. The request is terminated by the client shutting down the connection.");
            manInitialize.Enqueue("// After the request is received, the server sends data in response followed by shutting down its");
            manInitialize.Enqueue("// connection and closing the socket. The UDP server simply waits for a datagram request. The ");
            manInitialize.Enqueue("// request consists of a single datagram packet. The server then sends a number of responses to");
            manInitialize.Enqueue("// the source address of the request followed by a number of zero byte datagrams. The zero");
            manInitialize.Enqueue("// byte datagrams will indicate to the client that no more data will follow.");
            manInitialize.Enqueue("//");
            manInitialize.Enqueue("// usage:");
            manInitialize.Enqueue("//      Executable_file_name [-l bind-address] [-m message] [-n count] [-p port]");
            manInitialize.Enqueue("//                       [-t tcp|udp] [-x size]");
            manInitialize.Enqueue("//        -l bind-address        Local address to bind to");
            manInitialize.Enqueue("//        -m message             Text message to format into send buffer");
            manInitialize.Enqueue("//        -n count               Number of times to send a message");
            manInitialize.Enqueue("//        -p port                Local port to bind to");
            manInitialize.Enqueue("//        -t udp | tcp           Indicates which protocol to use");
            manInitialize.Enqueue("//        -x size                Size of send and receive buffer");
            manInitialize.Enqueue("//");
            manInitialize.Enqueue("// sample usage:");
            manInitialize.Enqueue("//      The following command line invokes an IPv6 TCP server bound to the wildcard address (::)");
            manInitialize.Enqueue("//      and port 5150 that sends 10 messages of 1024 bytes filled with the string 'hola'. The");
            manInitialize.Enqueue("//      command line following the server's is the client side that can be used to connect to");
            manInitialize.Enqueue("//      the server. For the client substitute the appropriate IPv6 address of the server.");
            manInitialize.Enqueue("//");
            manInitialize.Enqueue("//          Executable_file_name -l :: -p 5150 -n 10 -m 'hola' -x 1024 -t tcp");
            manInitialize.Enqueue("//          Executable_file_name -n 3ffe::1 -p 5150 -t tcp");
            manInitialize.Enqueue("//");
            manInitialize.Enqueue("//      The following command line invokes an IPv4 UDP server bound to a specific local interface");
            manInitialize.Enqueue("//      on port 5150 that sends 5 messages of 512 bytes filled with the string 'response'. The ");
            manInitialize.Enqueue("//      second command line is an example of the client's command line used to connect to the server.");
            manInitialize.Enqueue("//      NOTE: For UDP the buffer size on client and server should match - otherwise, an exception");
            manInitialize.Enqueue("//            will be thrown since the smaller buffer won't be able to hold the larger received datagram.");
            manInitialize.Enqueue("//");
            manInitialize.Enqueue("//          Executable_file_name -l 10.10.10.1 -p 5150 -n 5 -m 'response' -x 512 -t udp");
            manInitialize.Enqueue("//          Executable_file_name -n 10.10.10.1 -p 5150 -t udp -x 512");

            return manInitialize;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Create Manual Pages for ClientSocket.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <returns>
        /// Object manInitialize "Queue".
        /// </returns>
        ///-------------------------------------------------------------------------------------------------

        public static Queue clFillConsoleIni() {
            //make object of the queue class
            Queue manInitialize = new Queue();
            manInitialize.Enqueue(" ");
            manInitialize.Enqueue("// This is a simple TCP client application. For TCP, the server name is");
            //manInitialize.Enqueue("// This is a simple TCP and UDP client application. For TCP, the server name is");
            manInitialize.Enqueue("// resolved and a socket is created to attempt a connection to each address");
            manInitialize.Enqueue("// returned until a connection succeeds. Once connected the client sends a 'request'");
            manInitialize.Enqueue("// message to the server and shuts down the sending side. The client then loops");
            manInitialize.Enqueue("// to receive the server response until the server closes the connection at which");
            //manInitialize.Enqueue("// point the client closes its socket and exits. For UDP, the server name is resolved");
            //manInitialize.Enqueue("// and the first address returned is used (since there is no indication that there");
            //manInitialize.Enqueue("// is a UDP server at the endpoint). The UDP client then sends a single datagram");
            //manInitialize.Enqueue("// 'request' message and then waits to receive a response from the server. The client");
            manInitialize.Enqueue("// continues to receive until a zero byte datagram is received. Note that the server");
            manInitialize.Enqueue("// sends several zero byte datagrams but if they are lost, the client will never");
            manInitialize.Enqueue("// exit.");
            manInitialize.Enqueue("//");
            manInitialize.Enqueue("// usage:");
            manInitialize.Enqueue("//      Executable_file_name [-c] [-n server]|[-l server] [-p port] [-m message] [-t tcp|udp] [-x size]");
            manInitialize.Enqueue("//");
            //manInitialize.Enqueue("//           -c                         If UDP connect the socket before sending");
            manInitialize.Enqueue("//           -c                         UDP only");
            manInitialize.Enqueue("//           -n server             Server name to connect/send to");
            manInitialize.Enqueue("//           -l server             Server address to connect/send to (IPv4 or IPv6)");
            manInitialize.Enqueue("//           -p port                 Port number to connect/send to");
            manInitialize.Enqueue("//           -m message      String message to format in request buffer");
            manInitialize.Enqueue("//           -t tcp|udp            Indicates to use either the TCP or UDP protocol");
            manInitialize.Enqueue("//           -x size                 Size of send and receive buffers");
            manInitialize.Enqueue("//");
            manInitialize.Enqueue("// sample usage:");
            manInitialize.Enqueue("//      The following command line establishes a TCP connection to the given server");
            manInitialize.Enqueue("//      on port 5150. The other two command lines are sample server command lines --");
            manInitialize.Enqueue("//      one for IPv4 and one for IPv6. Since the client will attempt to resolve");
            manInitialize.Enqueue("//      the server's name, it should attempt to connect over IPv4 and IPv6 as long");
            manInitialize.Enqueue("//      as the addresses are registered in DNS.");
            manInitialize.Enqueue("//");
            manInitialize.Enqueue("//          Executable_file_name -n server -p 5150 -t tcp");
            manInitialize.Enqueue("//          Executable_file_name -l :: -p 5150 -t tcp");
            manInitialize.Enqueue("//          Executable_file_name -l 0.0.0.0 -p 5150 -t tcp");
            manInitialize.Enqueue("//");
            manInitialize.Enqueue("//      The following command line creates a connected UDP socket that sends");
            manInitialize.Enqueue("//      data to the server x.y.z.w on port 5150. While the second entry is the");
            manInitialize.Enqueue("//      server command line used. ");
            //manInitialize.Enqueue("//      NOTE: For UDP sockets, the buffer size on the client and server should match");
            //manInitialize.Enqueue("//            as an exception will be thrown if the receiving buffer is smaller than");
            //manInitialize.Enqueue("//            the incoming datagram.");
            manInitialize.Enqueue("//");
            manInitialize.Enqueue("//          Executable_file_name -n x.y.z.w -p 5150 -t udp -c -x 512");
            manInitialize.Enqueue("//          Executable_file_name -l x.y.z.w -p 5150 -t udp -x 512");
            manInitialize.Enqueue("//");

            return manInitialize;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Event Handler for the Stop Server button client event on the TCP Tab.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     object.
        /// </param>
        /// <param name="e">
        ///     EventArgs.
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void btnStopServerTcp_Click(object sender, EventArgs e) {

            btnStopServer_Click(sender, e);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Event Handler for the Stop Server button client event on the Main Tab.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     object.
        /// </param>
        /// <param name="e">
        ///     EventArgs.
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        public void btnStopServer_Click(object sender, EventArgs e) {

            lbxConsole.Items.Clear();
            lbxTcp.Items.Clear();
            Application.DoEvents();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Empty Event Handler for the Start Minitor Window. Used to handle the button click event that
        /// launched the window. Carried over from another project.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     object.
        /// </param>
        /// <param name="e">
        ///     EventArgs.
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void btnStartMonitor_Click(object sender, EventArgs e) {

        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Empty Event Handler for the TreeView button click event after selecting a Tree Node. For
        /// implementing UI Controls activity and for later use.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     object.
        /// </param>
        /// <param name="e">
        ///     TreeViewEventArgs.
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e) {

        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Empty Event Handler for the Monitor Mode button click event. Used to launch the Monitor Mode
        /// windows form.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     object.
        /// </param>
        /// <param name="e">
        ///     EventArgs.
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void btnMonitorMode_Click(object sender, EventArgs e) {

            switch (boolPowerMode) {
                case false:
                    if (intTransmitClicks > 1) {
                        intTransmitClicks = 0;
                        intMonitorClicks = 0;
                        boolPowerMode = true;
                        goto case true;
                    }
                    if (intTransmitClicks < 1) {
                        btnMonitorMode.Text = "Transmit Mode";
                        btnStart.Text = "&Start";
                        btnMonitorMode.ForeColor = Color.Black;
                        bContinueCapturing = false;
                        if (mainSocket != null) {
                            mainSocket.Close();
                        }
                        intTransmitClicks = 1;
                        break;
                    }
                    if (intTransmitClicks == 1) {
                        btnMonitorMode.ForeColor = Color.Firebrick;
                        intTransmitClicks = 2;
                        break;
                    }
                    break;
                case true:
                    if (intMonitorClicks > 1) {
                        intMonitorClicks = 0;
                        intTransmitClicks = 0;
                        boolPowerMode = false;
                        goto case false;
                    }
                    if (intMonitorClicks < 1) {
                        btnMonitorMode.Text = "Monitor Mode";
                        btnStart.Text = "&Stop";
                        btnMonitorMode.ForeColor = Color.Black;
                        intMonitorClicks = 1;
                        break;
                    }
                    if (intMonitorClicks == 1) {
                        btnMonitorMode.ForeColor = Color.Green;
                        intMonitorClicks = 2;
                        break;
                    }
                    break;
                default:
                    break;
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Event Handler for the Start Monitoring button within the RAW_IP tab/window.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     .
        /// </param>
        /// <param name="e">
        ///     .
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void btnStart_Click(object sender, EventArgs e) {

            if (cmbInterfaces.Text == "") {
                MessageBox.Show("Select an Interface to capture the packets.",
                    "Monitor Mode", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            } else {
                try {
                    if (!bContinueCapturing) {
                        intCapturedPacketCount = 0;
                        // Start capturing the packets...
                        btnStart.Text = "&Stop";
                        btnMonitorMode.Text = "Monitor Mode {0}";
                        bContinueCapturing = true;
                        // For sniffing the socket to capture the packets has to be a raw socket, with the
                        // address family being of type internetwork, and protocol being IP
                        mainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.IP);
                        // Bind the socket to the selected IP address
                        mainSocket.Bind(new IPEndPoint(IPAddress.Parse(cmbInterfaces.Text), 0));
                        // Set the socket  options
                        mainSocket.SetSocketOption(SocketOptionLevel.IP,            // Applies only to IP packets
                                                   SocketOptionName.HeaderIncluded, // Set the include the header
                                                   true);                           // option to true
                        byte[] byTrue = new byte[4] { 1, 0, 0, 0 };
                        byte[] byOut = new byte[4] { 1, 0, 0, 0 };                  // Capture outgoing packets
                        // Socket.IOControl is analogous to the WSAIoctl method of Winsock 2
                        mainSocket.IOControl(IOControlCode.ReceiveAll,              // Winsock 2 SIO_RCVALL Equivalent
                                             byTrue,
                                             byOut);
                        // Start receiving the packets asynchronously
                        mainSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(OnReceive), null);
                    } else {
                        btnStart.Text = "&Start";
                        bContinueCapturing = false;
                        // To stop capturing the packets close the socket
                        mainSocket.Close();
                    }
                } catch (Exception ex) {
                    MessageBox.Show("btnStart_Click" + ex.Message, "Monitor Mode", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnStart.Text = "&Start";
                    bContinueCapturing = false;
                }
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// A Callback Event Handler used for asynchronous actions for received packets.  It parses the
        /// received data and restarts the capturing of incoming packets.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="ar">
        ///     Async Interface Result.
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void OnReceive(IAsyncResult ar) {

            try {
                int nReceived = mainSocket.EndReceive(ar);
                //Analyze the bytes received...
                ParseData(byteData, nReceived);
                if (bContinueCapturing) {
                    byteData = new byte[4096];

                    btnMonitorMode.Text = "Monitor Mode {" + intCapturedPacketCount + "}";
                    //Another call to BeginReceive so that we continue to receive the incoming packets
                    mainSocket.BeginReceive(byteData,
                        0,
                        byteData.Length,
                        SocketFlags.None,
                        new AsyncCallback(OnReceive),
                        null);
                    intCapturedPacketCount++;
                }
            } catch (ObjectDisposedException) {
            } catch (Exception ex) {
                MessageBox.Show("OnReceive" + ex.Message, "Monitor Mode", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Method for parsing an IP Header Packet.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="byteData">
        ///     A Byte array of Data.
        /// </param>
        /// <param name="nReceived">
        ///     Count.
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void ParseData(byte[] byteData, int nReceived) {

            // Instantiate a new Node for the IP datagram to put into the TreeView later
            TreeNode rootNode = new TreeNode();
            // Since all protocol packets are encapsulated in the IP datagram we start by parsing the IP header and
            // see what protocol data is being carried by it
            //IPSavedHeaderPackets iPSavedHeaderPackets = new IPSavedHeaderPackets(byteData, nReceived);

            IPHeader ipHeader = new IPHeader(byteData, nReceived);
            // Add all child nodes and IP Header values for each to this ipNode
            //IPHeaderArrayCopy();
            TreeNode ipNode = MakeIPTreeNode(ipHeader);
            rootNode.Nodes.Add(ipNode);
            switch (ipHeader.ProtocolType) {
                case Protocol.TCP:
                    TCPHeader tcpHeader = new TCPHeader(ipHeader.Data,                     // Data being carried 
                                                        ipHeader.MessageLength);           // Length of the data field
                    TreeNode tcpNode = MakeTCPTreeNode(tcpHeader);
                    rootNode.Nodes.Add(tcpNode);
                    if (tcpHeader.DestinationPort == "53" ||    // If Port is equal to 53 then underlying protocol is DNS
                        tcpHeader.SourcePort == "53") {         // NOTE: DNS uses TCP or UDP so we check twice
                        TreeNode dnsNode = MakeDNSTreeNode(tcpHeader.Data, (int)tcpHeader.MessageLength);
                        rootNode.Nodes.Add(dnsNode);
                    }
                    break;
                case Protocol.UDP:
                    UDPHeader udpHeader = new UDPHeader(ipHeader.Data,                      // Data being carried
                                                        (int)ipHeader.MessageLength);       // Length of the data field           
                    TreeNode udpNode = MakeUDPTreeNode(udpHeader);
                    rootNode.Nodes.Add(udpNode);
                    if (udpHeader.DestinationPort == "53" ||    // If Port is equal to 53 then underlying protocol is DNS
                        udpHeader.SourcePort == "53") {         // NOTE: DNS uses TCP or UDP so we check twice
                        TreeNode dnsNode = MakeDNSTreeNode(udpHeader.Data,              // Data being carried
                            Convert.ToInt32(udpHeader.Length) - 8);                    // Length of UDP header is always
                        rootNode.Nodes.Add(dnsNode);
                    }
                    break;
                case Protocol.Unknown:
                    break;
            }
            if (intCapturedPacketCount < 3) {
                MessageBox.Show("Capturing: 0-" + intCapturedPacketCount + " so far.", "Total: [0-" + intCapturedPacketCount + "] ", MessageBoxButtons.OK);
            }
            boolScanSelectedNodes = true;
            // OnAddTreeNode's Target is rootNode
            AddTreeNode addTreeNode = new AddTreeNode(OnAddTreeNode);
            //intCapturedPacketCount = Program.CapturedPackets.Count;
            rootNode.Text = " [" + intCapturedPacketCount + "] " + ipHeader.SourceAddress.ToString() + "-" + ipHeader.DestinationAddress.ToString();
            //Thread safe adding of the nodes
            treeView.Invoke(addTreeNode, new object[] { rootNode });
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// IP header array copy.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///-------------------------------------------------------------------------------------------------

        private void IPHeaderArrayCopy() {

            //Array.Copy(IPSavedHeaderPackets.LayerNodeArray0, 0, IPSavedHeaderPackets.Version, 0, 4);
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
            //tableLayoutPanel1.CellPaint += new TableLayoutCellPaintEventHandler(tableLayoutPanel1_CellPaint);

        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Helper function which returns the information contained in the IP header as a tree node.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="ipHeader">
        ///     Instance of IPHeader Class containing Byte[] byteData and Int32 nReceive count.
        /// </param>
        ///
        /// <returns>
        /// ipNode.
        /// </returns>
        ///-------------------------------------------------------------------------------------------------

        private TreeNode MakeIPTreeNode(IPHeader ipHeader) {

            //Array.Copy(IPSavedHeaderPackets.LayerNodeArray0, 0, IPSavedHeaderPackets.Version, 0, 4);
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
            //tableLayoutPanel1.CellPaint += new TableLayoutCellPaintEventHandler(tableLayoutPanel1_CellPaint);

            TreeNode ipNode = new TreeNode();
            ipNode.Text = "IP";
            ipNode.Nodes.Add("Version: " + ipHeader.Version);
            ipNode.Nodes.Add("Header Length: " + ipHeader.HeaderLength);
            ipNode.Nodes.Add("Differentiated Services: " + ipHeader.DifferentiatedServices);
            ipNode.Nodes.Add("Total Length: " + ipHeader.TotalLength);
            ipNode.Nodes.Add("Identification: " + ipHeader.Identification);
            ipNode.Nodes.Add("Flags: " + ipHeader.Flags);
            ipNode.Nodes.Add("Fragmentation Offset: " + ipHeader.FragmentationOffset);
            ipNode.Nodes.Add("Time to Live: " + ipHeader.TTL);
            switch (ipHeader.ProtocolType) {
                case Protocol.TCP:
                    ipNode.Nodes.Add("Protocol: " + "TCP");
                    break;
                case Protocol.UDP:
                    ipNode.Nodes.Add("Protocol: " + "UDP");
                    break;
                case Protocol.Unknown:
                    ipNode.Nodes.Add("Protocol: " + "Unknown");
                    break;
            }
            ipNode.Nodes.Add("Checksum: " + ipHeader.Checksum);
            ipNode.Nodes.Add("Source: " + ipHeader.SourceAddress.ToString());
            ipNode.Nodes.Add("Destination: " + ipHeader.DestinationAddress.ToString());

            return ipNode;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Helper function which returns the information contained in the TCP header as a tree node.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="tcpHeader">
        ///     Instance of the TCPHeader Class.
        /// </param>
        ///
        /// <returns>
        /// tcpNode.
        /// </returns>
        ///-------------------------------------------------------------------------------------------------

        private TreeNode MakeTCPTreeNode(TCPHeader tcpHeader) {

            TreeNode tcpNode = new TreeNode();
            tcpNode.Text = "TCP";
            tcpNode.Nodes.Add("Source Port: " + tcpHeader.SourcePort);
            tcpNode.Nodes.Add("Destination Port: " + tcpHeader.DestinationPort);
            tcpNode.Nodes.Add("Sequence Number: " + tcpHeader.SequenceNumber);
            if (tcpHeader.AcknowledgementNumber != "")
                tcpNode.Nodes.Add("Acknowledgement Number: " + tcpHeader.AcknowledgementNumber);
            tcpNode.Nodes.Add("Header Length: " + tcpHeader.HeaderLength);
            tcpNode.Nodes.Add("Flags: " + tcpHeader.Flags);
            tcpNode.Nodes.Add("Window Size: " + tcpHeader.WindowSize);
            tcpNode.Nodes.Add("Checksum: " + tcpHeader.Checksum);
            if (tcpHeader.UrgentPointer != "")
                tcpNode.Nodes.Add("Urgent Pointer: " + tcpHeader.UrgentPointer);

            return tcpNode;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Helper function which returns the information contained in the UDP header as a tree node.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="udpHeader">
        ///     Instance of UdpHeader Class.
        /// </param>
        ///
        /// <returns>
        /// udpNode.
        /// </returns>
        ///-------------------------------------------------------------------------------------------------

        private TreeNode MakeUDPTreeNode(UDPHeader udpHeader) {

            TreeNode udpNode = new TreeNode();
            udpNode.Text = "UDP";
            udpNode.Nodes.Add("Source Port: " + udpHeader.SourcePort);
            udpNode.Nodes.Add("Destination Port: " + udpHeader.DestinationPort);
            udpNode.Nodes.Add("Length: " + udpHeader.Length);
            udpNode.Nodes.Add("Checksum: " + udpHeader.Checksum);

            return udpNode;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Helper function which returns the information contained in the DNS header as a tree node.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="byteData">
        ///     ipHeader Byte[] of containing Header and Payload.
        /// </param>
        /// <param name="nLength">
        ///     Count.
        /// </param>
        ///
        /// <returns>
        /// dnsNode.
        /// </returns>
        ///-------------------------------------------------------------------------------------------------

        private TreeNode MakeDNSTreeNode(byte[] byteData, int nLength) {

            DNSHeader dnsHeader = new DNSHeader(byteData, nLength);
            TreeNode dnsNode = new TreeNode();
            dnsNode.Text = "DNS";
            dnsNode.Nodes.Add("Identification: " + dnsHeader.Identification);
            dnsNode.Nodes.Add("Flags: " + dnsHeader.Flags);
            dnsNode.Nodes.Add("Questions: " + dnsHeader.TotalQuestions);
            dnsNode.Nodes.Add("Answer RRs: " + dnsHeader.TotalAnswerRRs);
            dnsNode.Nodes.Add("Authority RRs: " + dnsHeader.TotalAuthorityRRs);
            dnsNode.Nodes.Add("Additional RRs: " + dnsHeader.TotalAdditionalRRs);

            return dnsNode;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Method for adding a threadsafe Node passed as the only argument to TreeView.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="node">
        ///     TreeNode.
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void OnAddTreeNode(TreeNode node) {

            treeView.Nodes.Add(node);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Event Handler to load IP Address of Host(s) available to choose as Network Interface.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     object.
        /// </param>
        /// <param name="e">
        ///     EventArgs.
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void SnifferForm_Load(object sender, EventArgs e) {

            string strIP = null;
            IPHostEntry HosyEntry = Dns.GetHostEntry((Dns.GetHostName()));
            if (HosyEntry.AddressList.Length > 0) {
                foreach (IPAddress ip in HosyEntry.AddressList) {
                    strIP = ip.ToString();
                    cmbInterfaces.Items.Add(strIP);
                }
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Event Handler that closes Soket opened.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     object.
        /// </param>
        /// <param name="e">
        ///     FormClosingEventArgs.
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void SnifferForm_FormClosing(object sender, FormClosingEventArgs e) {

            if (bContinueCapturing) {
                mainSocket.Close();
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Boolean trigger for choosing scan mode. Uses all received incoming IP Headers.
        /// </summary>
        ///
        /// <value>
        /// true if scan checked nodes, false if not.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static bool boolScanCheckedNodes { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Boolean trigger for choosing scan mode. Uses all received incoming IP Headers.
        /// </summary>
        ///
        /// <value>
        /// true if scan all nodes, false if not.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static bool boolScanAllNodes { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Boolean trigger for choosing scan mode. This is for selected mode - Click Event for each IP
        /// Header.
        /// </summary>
        ///
        /// <value>
        /// true if scan selected nodes, false if not.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static bool boolScanSelectedNodes { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Event Handler for TreeView IP Header Selection click event. Multiple modes are available.
        /// Scan All IP Headers Scan Selected IP Headers Scan Checked IP Headers.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     .
        /// </param>
        /// <param name="e">
        ///     .
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e) {

            int intSwitch = 0;
            switch (intSwitch) {
                case 0:
                    if (boolScanAllNodes) {
                        goto case 1;
                    } else {
                        if (boolScanCheckedNodes) {
                            goto case 2;
                        } else {
                            int i = e.Node.Index;
                            // Check if client is on Root Node
                            if (e.Node.Level == 0) {
                                // This loop will iterate through depth level 01
                                lbxConsole.Items.Add(this.treeView.Nodes[i].Text.ToString());
                                lbxConsole.SelectedIndex = lbxConsole.Items.Count - 1;
                                lbxConsole.SelectedIndex = -1;
                                // To go further down, iterate through each node
                                // this.treeView1.Nodes[ i ].Nodes.Count;
                                for (int j = 0; j < this.treeView.Nodes[i].Nodes.Count; j++) {
                                    lbxConsole.Items.Add(this.treeView.Nodes[i].Nodes[j].Text.ToString());
                                    lbxConsole.SelectedIndex = lbxConsole.Items.Count - 1;
                                    lbxConsole.SelectedIndex = -1;
                                    // To go further down, iterate through each node
                                    for (int w = 0; w < this.treeView.Nodes[i].Nodes[j].Nodes.Count; w++) {
                                        lbxConsole.Items.Add(this.treeView.Nodes[i].Nodes[j].Nodes[w].Text.ToString());
                                        lbxConsole.SelectedIndex = lbxConsole.Items.Count - 1;
                                        lbxConsole.SelectedIndex = -1;
                                    }
                                }
                            }
                        }
                        break;
                    }
                case 1:
                    if (boolScanSelectedNodes) {
                        goto case 0;
                    } else {
                        if (boolScanCheckedNodes) {
                            goto case 2;
                        } else {
                            // This loop will iterate through depth level 01
                            for (int i = 0; i < this.treeView.Nodes.Count; i++) {
                                lbxConsole.Items.Add(this.treeView.Nodes[i].Text.ToString());
                                lbxConsole.SelectedIndex = lbxConsole.Items.Count - 1;
                                lbxConsole.SelectedIndex = -1;
                                // To go further down, iterate through each node
                                for (int j = 0; j < this.treeView.Nodes[i].Nodes.Count; j++) {
                                    lbxConsole.Items.Add(this.treeView.Nodes[i].Nodes[j].Text.ToString());
                                    lbxConsole.SelectedIndex = lbxConsole.Items.Count - 1;
                                    lbxConsole.SelectedIndex = -1;
                                    // To go further down, iterate through each node
                                    for (int w = 0; w < this.treeView.Nodes[i].Nodes[j].Nodes.Count; w++) {
                                        lbxConsole.Items.Add(this.treeView.Nodes[i].Nodes[j].Nodes[w].Text.ToString());
                                        lbxConsole.SelectedIndex = lbxConsole.Items.Count - 1;
                                        lbxConsole.SelectedIndex = -1;
                                    }
                                }
                            }
                        }
                        break;
                    }
                case 2:
                    if (!boolScanCheckedNodes) {
                        goto case 0;
                    } else {
                        // This loop will iterate through depth level 01
                        for (int i = 0; i < this.treeView.Nodes.Count; i++) {
                            lbxConsole.Items.Add(this.treeView.Nodes[i].Text.ToString());
                            lbxConsole.SelectedIndex = lbxConsole.Items.Count - 1;
                            lbxConsole.SelectedIndex = -1;
                            // To go further down, iterate through each node
                            for (int j = 0; j < this.treeView.Nodes[i].Nodes.Count; j++) {
                                lbxConsole.Items.Add(this.treeView.Nodes[i].Nodes[j].Text.ToString());
                                lbxConsole.SelectedIndex = lbxConsole.Items.Count - 1;
                                lbxConsole.SelectedIndex = -1;
                                // To go further down, iterate through each node
                                for (int w = 0; w < this.treeView.Nodes[i].Nodes[j].Nodes.Count; w++) {
                                    lbxConsole.Items.Add(this.treeView.Nodes[i].Nodes[j].Nodes[w].Text.ToString());
                                    lbxConsole.SelectedIndex = lbxConsole.Items.Count - 1;
                                    lbxConsole.SelectedIndex = -1;
                                }
                            }
                        }
                        break;
                    }
                default:
                    // If you wind up here go ahead and have the checkbox mode.
                    goto case 2;
            }
        }



        #region LOOP THROUGH ALL NODES AND ALL SUBNODES
        //                // ***************************************************************************************************
        //                //  LOOP THROUGH ONLY ROOT NODE AND ALL SUBNODES 
        //                // ***************************************************************************************************
        //                Queue queNodes = new Queue();
        //                // This loop will iterate through depth level 01
        //                //for (int i = e.Node.Index; i <= e.Node.Index; i = e.Node.Index) {
        //                //MessageBox.Show(this.treeView.Nodes[i].Text.ToString());
        //                //queNodes.Enqueue(this.treeView.Nodes[i].Text.ToString());
        //                int i = e.Node.Index;
        //                lbxConsole.Items.Add(this.treeView.Nodes[i].Text.ToString());
        //                lbxConsole.SelectedIndex = lbxConsole.Items.Count - 1;
        //                lbxConsole.SelectedIndex = -1;
        //                // To go further down, iterate through each node
        //                // this.treeView1.Nodes[ i ].Nodes.Count;
        //                for (int j = 0; j < this.treeView.Nodes[i].Nodes.Count; j++) {
        //                    // MessageBox.Show(this.treeView.Nodes[i].Nodes[j].Text.ToString());
        //                    //queNodes.Enqueue(this.treeView.Nodes[i].Nodes[j].Text.ToString());
        //                    lbxConsole.Items.Add(this.treeView.Nodes[i].Nodes[j].Text.ToString());
        //                    lbxConsole.SelectedIndex = lbxConsole.Items.Count - 1;
        //                    lbxConsole.SelectedIndex = -1;
        //                    // To go further down, iterate through each node
        //                    // this.treeView1.Nodes[ i ].Nodes.Count;
        //                    for (int w = 0; w < this.treeView.Nodes[i].Nodes[j].Nodes.Count; w++) {
        //                        //MessageBox.Show(this.treeView.Nodes[i].Nodes[j].Nodes[w].Text.ToString());
        //                        //queNodes.Enqueue(this.treeView.Nodes[i].Nodes[j].Nodes[w].Text.ToString());
        //                        lbxConsole.Items.Add(this.treeView.Nodes[i].Nodes[j].Nodes[w].Text.ToString());
        //                        lbxConsole.SelectedIndex = lbxConsole.Items.Count - 1;
        //                        lbxConsole.SelectedIndex = -1;
        //                    }
        //                }
        //            }
        //            break;

        //        // ***************************************************************************************************
        //        //  LOOP THROUGH ONLY ROOT NODE AND ALL SUBNODES 
        //        // ***************************************************************************************************
        #endregion



        #region LOOP THROUGH ALL NODES AND ALL SUBNODES
        //        case 1:
        //            if (boolScanCheckNodes) {
        //                goto default;
        //                break;
        //            }
        //             if (boolScanAllNodes) {

        //                //// ***************************************************************************************************
        //                ////  LOOP THROUGH ALL NODES AND ALL SUBNODES 
        //                //// ***************************************************************************************************
        //                Queue queNodes = new Queue();
        //                // This loop will iterate through depth level 01
        //                for (int i = 0; i < this.treeView.Nodes.Count; i++) {
        //                    //MessageBox.Show(this.treeView.Nodes[i].Text.ToString());
        //                    //queNodes.Enqueue(this.treeView.Nodes[i].Text.ToString());
        //                    lbxConsole.Items.Add(this.treeView.Nodes[i].Text.ToString());
        //                    lbxConsole.SelectedIndex = lbxConsole.Items.Count - 1;
        //                    lbxConsole.SelectedIndex = -1;
        //                    // To go further down, iterate through each node
        //                    // this.treeView1.Nodes[ i ].Nodes.Count;
        //                    for (int j = 0; j < this.treeView.Nodes[i].Nodes.Count; j++) {
        //                        // MessageBox.Show(this.treeView.Nodes[i].Nodes[j].Text.ToString());
        //                        //queNodes.Enqueue(this.treeView.Nodes[i].Nodes[j].Text.ToString());
        //                        lbxConsole.Items.Add(this.treeView.Nodes[i].Nodes[j].Text.ToString());
        //                        lbxConsole.SelectedIndex = lbxConsole.Items.Count - 1;
        //                        lbxConsole.SelectedIndex = -1;
        //                        // To go further down, iterate through each node
        //                        // this.treeView1.Nodes[ i ].Nodes.Count;
        //                        for (int w = 0; w < this.treeView.Nodes[i].Nodes[j].Nodes.Count; w++) {
        //                            //MessageBox.Show(this.treeView.Nodes[i].Nodes[j].Nodes[w].Text.ToString());
        //                            //queNodes.Enqueue(this.treeView.Nodes[i].Nodes[j].Nodes[w].Text.ToString());
        //                            lbxConsole.Items.Add(this.treeView.Nodes[i].Nodes[j].Nodes[w].Text.ToString());
        //                            lbxConsole.SelectedIndex = lbxConsole.Items.Count - 1;
        //                            lbxConsole.SelectedIndex = -1;
        //                        }
        //                    }
        //                }
        //            }
        //            break;
        //        default:
        //            return;
        //    }
        //}
        //// ***************************************************************************************************
        ////  LOOP THROUGH ALL NODES AND ALL SUBNODES 
        //// ***************************************************************************************************
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Function that sends the received IP Header Packets to the TCP Console.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     .
        /// </param>
        /// <param name="e">
        ///     Event information.
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void SendPacketsToConsole(object sender, EventArgs e) {

            try {
                for (int i = treeView.TopNode.Index; i < this.treeView.Nodes.Count; i++) {
                    lbxTcp.Items.Add(this.treeView.Nodes[i].Text.ToString());
                    lbxTcp.SelectedIndex = lbxTcp.Items.Count - 1;
                    lbxTcp.SelectedIndex = -1;
                    for (int j = 0; j < this.treeView.Nodes[i].Nodes.Count; j++) {
                        lbxTcp.Items.Add(this.treeView.Nodes[i].Nodes[j].Text.ToString());
                        lbxTcp.SelectedIndex = lbxTcp.Items.Count - 1;
                        lbxTcp.SelectedIndex = -1;
                        for (int w = 0; w < this.treeView.Nodes[i].Nodes[j].Nodes.Count; w++) {
                            lbxTcp.Items.Add(this.treeView.Nodes[i].Nodes[j].Nodes[w].Text.ToString());
                            lbxTcp.SelectedIndex = lbxTcp.Items.Count - 1;
                            lbxTcp.SelectedIndex = -1;
                        }
                    }
                }
            } catch (Exception ex) {
                MessageBox.Show("Scanned Packets Failed : Tcp Console Failed: " + ex.Message, "IP Header Monitoring", MessageBoxButtons.OK);
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Test Button Used to Populate the Panels on the TableLayoutPanel for each Bit.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     .
        /// </param>
        /// <param name="e">
        ///     .
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        public void buFillBits_Click(object sender, EventArgs e) {

            tableLayoutPanel1.CellPaint += new TableLayoutCellPaintEventHandler(tableLayoutPanel1_CellPaint);

            Panel bitsPanels = new Panel();
            int j = 0;
            bitsPanels.BackColor = Color.Black;
            // 
            // panelTest
            // 
            bitsPanels.BackColor = Color.Black;
            bitsPanels.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel1.SetColumnSpan(bitsPanels, 1);
            bitsPanels.Dock = DockStyle.Fill;
            bitsPanels.Location = new Point(483, 253);
            bitsPanels.Name = "bit [" + j + "] ";
            ++j;
            bitsPanels.Size = new Size(14, 30);
            bitsPanels.TabIndex = 20;
            tableLayoutPanel1.Controls.Add(bitsPanels, 0, 0);

        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Event Handler for the TableLayoutPanel UI.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     .
        /// </param>
        /// <param name="e">
        ///     .
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void tableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e) {

            Rectangle r = e.CellBounds;
            Graphics g = e.Graphics;
            g.FillRectangle(new SolidBrush(Color.Black), r);
            int intTrue = 0;
            Int32 intCly1 = 0;
            Int32 intRow = 0;
            intColumn = intCly1;
            if (e.Column == intColumn && e.Row == intRow) {
                for (int i = intTrue; i < 4096; i++) {
                    switch (intTrue) {
                        case 0:
                            // Layer One (1) of Tcp Protocol
                            if (IPSavedHeaderPackets.LayerNodeArray0[i]) {
                                buFillBits_Click(sender, e);
                                intColumn++;
                            } else {
                                intColumn++;
                            }
                            if ((intRow > 31) && (intColumn > 31)) {
                                intRow++;
                                goto case 1;
                            }
                            break;
                        case 1:
                            // Layer Onen (2) of Tcp Protocol
                            if (IPSavedHeaderPackets.LayerNodeArray0[i]) {
                                buFillBits_Click(sender, e);
                                intColumn++;
                            } else {
                                intColumn++;
                            }
                            if ((intColumn == 63) && (intColumn < 64)) {
                                intRow++;
                                goto case 11;
                            }
                            break;
                        case 11:
                            // Layer Onen (3) of Tcp Protocol
                            if (IPSavedHeaderPackets.LayerNodeArray0[i]) {
                                buFillBits_Click(sender, e);
                                intColumn++;
                            } else {
                                intColumn++;
                            }
                            if ((intCly1 == 95) && (intCly1 < 96)) {
                                intRow++;
                                goto case 12;
                            }
                            break;
                        case 12:
                            // Layer Onen (4) of Tcp Protocol
                            if (IPSavedHeaderPackets.LayerNodeArray0[i]) {
                                buFillBits_Click(sender, e);
                                intColumn++;
                            } else {
                                intColumn++;
                            }
                            if ((intColumn == 127) && (intColumn < 128)) {
                                intRow++;
                                goto case 13;
                            }
                            break;
                        case 13:
                            // Layer Onen (5) of Tcp Protocol
                            if (IPSavedHeaderPackets.LayerNodeArray0[i]) {
                                buFillBits_Click(sender, e);
                                intColumn++;
                            } else {
                                intColumn++;
                            }
                            if ((intColumn == 159) && (intColumn < 160)) {
                                intRow++;
                                goto case 14;
                            }
                            break;
                        case 14:
                            // Layer Onen (6) of Tcp Protocol
                            if (IPSavedHeaderPackets.LayerNodeArray0[i]) {
                                buFillBits_Click(sender, e);
                                intColumn++;
                            } else {
                                intColumn++;
                            }
                            if ((intColumn == 191) && (intColumn < 192)) {
                                intRow++;
                                goto case 15;
                            }
                            break;
                        case 15:
                            // Layer Onen (7) of Tcp Protocol
                            if (IPSavedHeaderPackets.LayerNodeArray0[i]) {
                                buFillBits_Click(sender, e);
                                intColumn++;
                            } else {
                                intColumn++;
                            }
                            if ((intColumn == 223) && (intColumn < 224)) {
                                intRow++;
                                goto case 16;
                            }
                            break;
                        case 16:
                            // Layer Onen (8) of Tcp Protocol
                            if (IPSavedHeaderPackets.LayerNodeArray0[i]) {
                                buFillBits_Click(sender, e);
                                intColumn++;
                            } else {
                                intColumn++;
                            }
                            if ((intColumn == 255) && (intColumn < 256)) {
                                intRow++;
                                goto case 17;
                            }
                            break;
                        case 17:
                            // Layer Onen (9) of Tcp Protocol
                            if (IPSavedHeaderPackets.LayerNodeArray0[i]) {
                                buFillBits_Click(sender, e);
                                intColumn++;
                            } else {
                                intColumn++;
                            }
                            if ((intColumn == 4095) && (intColumn < 4096)) {
                                return;
                            }
                            break;
                    }
                }
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Event Handlers to Transverse the Nodes in the TreeView checking all the children nodes.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     .
        /// </param>
        /// <param name="e">
        ///     .
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void trvMenuList_AfterCheck(object sender, TreeViewEventArgs e) {
            SetChildrenChecked(e.Node, e.Node.Checked);

        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Function that "Checks" the Node Checked State.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="treeNode">
        ///     .
        /// </param>
        /// <param name="checkedState">
        ///     .
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void SetChildrenChecked(TreeNode treeNode, bool checkedState) {
            foreach (TreeNode item in treeNode.Nodes) {
                if (item.Checked != checkedState) {
                    item.Checked = checkedState;
                }
                SetChildrenChecked(item, item.Checked);
            }
        }



        #region Drag and Drop TreeView

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Event handler. Called by tree for mouse down events.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     .
        /// </param>
        /// <param name="e">
        ///     Mouse event information.
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void tree_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) { // Get the tree. 
            TreeView tree = (TreeView)sender; // Get the node underneath the mouse. 
            TreeNode node = tree.GetNodeAt(e.X, e.Y);
            tree.SelectedNode = node;
            // Start the drag-and-drop operation with a cloned copy of the node. 
            if (node != null) {
                tree.DoDragDrop(node.Clone(), DragDropEffects.Copy);
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Event handler. Called by tree for drag over events.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     .
        /// </param>
        /// <param name="e">
        ///     Drag event information.
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void tree_DragOver(object sender, System.Windows.Forms.DragEventArgs e) {
            // Get the tree. 
            TreeView treeView = (TreeView)sender;
            // Drag and drop 
            //e.DoDragDrop(Node, DragDropEffects.Copy);
            //treeView.TreeNode nodeDragged = e.Data.GetData(typeof(TreeNode));
            // Copy to new position. 
            //node.Nodes.Add(nodeDragged.Clone());
            // Remove from original position.
            // nodeDragged.Remove();
            treeView.AllowDrop = true;
            //treeTwo.AllowDrop = true; 
            treeView.HideSelection = false;
            //treeTwo.HideSelection = false; 
            e.Effect = DragDropEffects.None;
            // Is it a valid format? 
            if (e.Data.GetData(typeof(TreeNode)) != null) {
                // Get the screen point. 
                Point pt = new Point(e.X, e.Y);
                // Convert to a point in the TreeView's coordinate system. 
                pt = treeView.PointToClient(pt);
                // Is the mouse over a valid node? 
                TreeNode node = treeView.GetNodeAt(pt);
                if (node != null) {
                    e.Effect = DragDropEffects.Copy;
                    treeView.SelectedNode = node;
                }
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Event handler. Called by tree for drag drop events.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     .
        /// </param>
        /// <param name="e">
        ///     Drag event information.
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void tree_DragDrop(object sender, System.Windows.Forms.DragEventArgs e) {

            // Get the tree. 
            TreeView treeView = (TreeView)sender;
            // Get the screen point. 
            Point pt = new Point(e.X, e.Y);
            // Convert to a point in the TreeView's coordinate system. 
            pt = treeView.PointToClient(pt);
            // Get the node underneath the mouse. 
            TreeNode node = treeView.GetNodeAt(pt);
            // Add a child node.
            node.Nodes.Add((TreeNode)e.Data.GetData(typeof(TreeNode)));
            // Show the newly added node if it is not already visible. 
            node.Expand();
        }


        #endregion



        #region Alternate IPHeader Functions
        //     // index = r * num_cols + c
        //     // Copy the values and display the result.
        //     private void btnCopy2d_Click(object sender, EventArgs e) {
        //         string[,] new_values = new string[3, 3];
        //         for (int row = 0; row <= new_values.GetUpperBound(0); row++) {
        //             for (int col = 0; col <= new_values.GetUpperBound(1); col++) {
        //                 new_values[row, col] = "------";
        //             }
        //         }

        //         Array.Copy(byteData, 6, new_values, 2, 6);
        //         //ShowValues(lstCopy, new_values);
        //     }

        // private void DetailSnifer_Load(object sender, EventArgs e)  { 
        //      IPHeader ipheader=(IPHeader) ipheader1; 
        //      insert_ip(ipheader); 
        //      switch (ipheader.ProtocolType) 
        //      { 
        //          case Protocol.TCP: 
        //              TCPHeader tcp= new TCPHeader(ipheader.Data,ipheader.Data.Length); 
        //              if (tcp.DestinationPort == "53" || tcp.SourcePort == "53") 
        //              { 
        //                  insert_DNS(tcp.Data, (int)tcp.MessageLength); 
        //              } 
        //              insert_Tcp(tcp); 
        //              break; 
        //          case Protocol.UDP: 
        //              UDPHeader udpHeader = new UDPHeader(ipheader.Data, ipheader.Data.Length); 
        //              if (udpHeader.DestinationPort == "53" || udpHeader.SourcePort == "53") 
        //              { 

        //                 insert_DNS(udpHeader.Data,Convert.ToInt32(udpHeader.Length) – 8);                       
        //              } 
        //              insert_UDP(udpHeader); 
        //              break; 
        //          case Protocol.Unknown: 
        //              break; 
        //          default: 
        //              break; 
        //      } 
        //  } 
        // void insertsniffdata_IP(IPHeader ipheader)  { 
        //    if (listView1.InvokeRequired)  { 
        //        insertinto inst = new insertinto(insert); 
        //        listView1.BeginInvoke(inst, ipheader);                
        //    } 
        //} 
        // void insert(object obj)  { 
        //     IPHeader ipheader = (IPHeader)obj; 
        //     listView1.Items.Add(new ListViewItem(new string[] {
        //         global.ToString(), 
        //         ipheader.Version, 
        //         ipheader.SourceAddress.ToString(), 
        //         ipheader.DestinationAddress.ToString() 
        //     })); 
        //     A_ipheader.Add(ipheader); 
        //     global++; 
        // } 
        // private void listView1_DoubleClick(object sender, EventArgs e)  { 
        //     ListView l= (ListView) sender; 
        //     DetailSnifer obj = new DetailSnifer(); 
        //     obj.ipheader1=(IPHeader) A_ipheader[l.SelectedItems[0].Index]; 
        //     obj.ShowDialog(); 
        // } 
        // void insert_UDP(UDPHeader udpHeader)  { 
        //      listView1.Items.Add(new ListViewItem(new string[] {"Source Port", udpHeader.SourcePort})); 
        //      listView1.Items.Add(new ListViewItem(new string[] {"Destination Port", udpHeader.DestinationPort})); 
        //      listView1.Items.Add(new ListViewItem(new string[] {"Length", udpHeader.Length})); 
        //      listView1.Items.Add(new ListViewItem(new string[] {"Checksum", udpHeader.Checksum})); 
        //      listView1.Items.Add(new ListViewItem(new string[] { "Data", Encoding.ASCII.GetString(udpHeader.Data) })); 
        //  } 
        // void insert_DNS(byte[] buffer, int nums)  { 
        //      DNSHeader dnsHeader = new DNSHeader(buffer, nums);                       
        //      listView1.Items.Add(new ListViewItem(new string[] {"Identification", dnsHeader.Identification})); 
        //      listView1.Items.Add(new ListViewItem(new string[] {"Flags", dnsHeader.Flags})); 
        //      listView1.Items.Add(new ListViewItem(new string[] {"Questions", dnsHeader.TotalQuestions})); 
        //      listView1.Items.Add(new ListViewItem(new string[] {"Answer RRs", dnsHeader.TotalAnswerRRs})); 
        //      listView1.Items.Add(new ListViewItem(new string[] {"Authority RRs", dnsHeader.TotalAuthorityRRs})); 
        //      listView1.Items.Add(new ListViewItem(new string[] { "Additional RRs", dnsHeader.TotalAdditionalRRs })); 
        //  } 

        // void insert_Tcp(TCPHeader tcp)  { 
        //      listView1.Items.Add(new ListViewItem(new string[] { "Source Port", tcp.SourcePort.ToString() })); 
        //      listView1.Items.Add(new ListViewItem(new string[] { "Destination Port", tcp.DestinationPort.ToString() })); 
        //      listView1.Items.Add(new ListViewItem(new string[] { "Sequence Number", tcp.SequenceNumber.ToString() })); 
        //      listView1.Items.Add(new ListViewItem(new string[] { "Acknowledge Number", tcp.AcknowledgementNumber.ToString() })); 
        //      listView1.Items.Add(new ListViewItem(new string[] { "Header Length", tcp.HeaderLength.ToString() })); 
        //      listView1.Items.Add(new ListViewItem(new string[] { "Flags", tcp.Flags.ToString() })); 
        //      listView1.Items.Add(new ListViewItem(new string[] { "Window Size", tcp.WindowSize.ToString() })); 
        //      listView1.Items.Add(new ListViewItem(new string[] { "Check Sum", tcp.Checksum.ToString() })); 
        //  } 

        // void insert_ip(IPHeader ipheader)  { 
        //     listView1.Items.Add(new ListViewItem(new string[] { "Source", ipheader.SourceAddress.ToString() })); 
        //     listView1.Items.Add(new ListViewItem(new string[] { "Destintion", ipheader.DestinationAddress.ToString() })); 
        //     listView1.Items.Add(new ListViewItem(new string[] { "Version", ipheader.Version.ToString() })); 
        //     listView1.Items.Add(new ListViewItem(new string[] { "Differentiated Srvice", ipheader.DifferentiatedServices.ToString() })); 
        //     listView1.Items.Add(new ListViewItem(new string[] { "Total Length", ipheader.TotalLength.ToString() })); 
        //     listView1.Items.Add(new ListViewItem(new string[] { "Identification", ipheader.Identification.ToString() })); 
        //     listView1.Items.Add(new ListViewItem(new string[] { "Flags", ipheader.Flags.ToString() })); 
        //     listView1.Items.Add(new ListViewItem(new string[] { "Fragmentation", ipheader.FragmentationOffset.ToString() })); 
        //     listView1.Items.Add(new ListViewItem(new string[] { "TTL", ipheader.TTL.ToString() })); 
        //     listView1.Items.Add(new ListViewItem(new string[] { "Data", Encoding.UTF8.GetString(ipheader.Data)})); 
        //     listView1.Items.Add(new ListViewItem(new string[] { "Protocol", ipheader.ProtocolType.ToString() })); 
        //     listView1.Items.Add(new ListViewItem(new string[] { "Checksum", ipheader.Checksum.ToString() })); 
        //     textBox1.Text = Encoding.UTF8.GetString(ipheader.Data).GetHashCode().ToString(); 
        // }
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Event Handler for IP Header Monitor Scan Results. View results on TCP Tab.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     .
        /// </param>
        /// <param name="e">
        ///     .
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void btnViewIPStream_Click(object sender, EventArgs e) {

        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Event Handler for TCP "Real-Time" Feature.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     .
        /// </param>
        /// <param name="e">
        ///     .
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void btnRealTimeTcp_Click(object sender, EventArgs e) {
            for (int i = intCapturedPacketCount; i < 100; ) {
                int intOn = 1;
                switch (intOn) {
                    case 1:
                        if (!boolViewIPstreamTCPtab)
                            treeView.SuspendLayout();
                        boolViewIPstreamTCPtab = true;
                        btnRealTimeTcp.Text = "Streaming Packet Capture";
                        goto case 3;
                    case 2:
                        if (boolViewIPstreamTCPtab)
                            treeView.SuspendLayout();
                        boolViewIPstreamTCPtab = false;
                        btnRealTimeTcp.Text = "Real-Time";
                        goto case 3;
                    case 3:
                        if (treeView.CanFocus)
                            treeView.ResumeLayout();
                        goto case 2;
                }
                return;
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Event Handler for Bobo MonsterGame Button click. Not used for anything but testing right now.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     .
        /// </param>
        /// <param name="e">
        ///     .
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void btnBobo_Click(object sender, EventArgs e) {
            Monster monster = new Monster();
            monster.Show();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// DPScanService Generic Method for threadsafe functions. Not used.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///-------------------------------------------------------------------------------------------------

        private void DoSideWork() {
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Event Handler for UI Tab/Page Selected and in Focus.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     .
        /// </param>
        /// <param name="e">
        ///     .
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void tabControl1_Selected(object sender, TabControlEventArgs e) {
 
        }



        #region DOMANT DPScanService Event Handlers NOT USED

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Event Handler for Tab Selecting event.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     .
        /// </param>
        /// <param name="e">
        ///     .
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e) {
            Queue queText = new Queue();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Event Handler for DPScanService Console Text Box. This is for "Text" Changed events.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     .
        /// </param>
        /// <param name="e">
        ///     .
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void tbConsoleTextDPScan_TextChanged(object sender, EventArgs e) {
            //tbConsoleTextDPScan23.TabStop = true;
            //tbConsoleTextDPScan23.TabIndex = 0;
            //SendKeys.SendWait("{ENTER}");
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Event Handler for DPScanService "{ENTER}" Button. This is for the "{ENTER}" KeyDown events of
        /// the "{ENTER}" Button.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     .
        /// </param>
        /// <param name="e">
        ///     .
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void btnEnterDPScan_Enter(object sender, EventArgs e) {

        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Event Handler for Enter Button KeyDown Async CallBack Not Being Used.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     .
        /// </param>
        /// <param name="e">
        ///     Key event information.
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        void btnEnterDPScan_KeyDown(object sender, KeyEventArgs e) {
            //if ((e.KeyCode == Keys.Enter) && (tbConsoleTextDPScan23.ContainsFocus)) {

            //}
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Event handler. Called by btnEnterDPScan for mouse click events.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     .
        /// </param>
        /// <param name="e">
        ///     .
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void btnEnterDPScan_MouseClick(object sender, MouseEventArgs e) {

        }
        #endregion



        #region DPScanService Event Handlers Being Used

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Event Handler for Mouse Click on DPScanService TabPage8.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     .
        /// </param>
        /// <param name="e">
        ///     .
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void tabControl1_MouseClick(object sender, MouseEventArgs e) {
            bool test = (tabControl1.SelectedIndex).Equals(tabPage7);
            switch (tabControl1.SelectedIndex) {
                case 0:
                    
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;
                    //__console__.whyLoadSettings( = tbConsoleTextDPScan23.Text;
                    //MainForm.strConsoleText = __console__.LoadSettings(lbxConsoleDPScan,boolWaitingforUI);
                  
                //__console__ _console_7 = new __console__();
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Event Handler for TextBox on DPScanService Panel. Takes care of "{ENTER}" Key.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     .
        /// </param>
        /// <param name="e">
        ///     KeyEventArgs.
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void tbConsoleTextDPScan_Enter(object sender, KeyPressEventArgs e) {
            string strArgs = "";
            if (ConfirmUpdateConsole(strArgs, true, false)) {
                strArgs = "Processing command... ";
            }
            if (ConfirmUpdateConsole(strArgs, true, false)) {
                strArgs = "Test parse of Commands";
            }
            if (ConfirmUpdateConsole(strArgs, false, false)) {
                strArgs = "Potential Result Set from Command(s) && [args]";
            }
            if (ConfirmUpdateConsole(strArgs, false, true)) {
                strArgs = "";
            }
            //DataBindings.Control.BindingContext = btnEnterDPScan.BindingContext;// (btnEnterDPScan_Click, e);
            //bool boolSuccess;
            //string strArgs = "";
            //string strParams = "";
            //string strSwitch = "";
            //int iP = 0;
            //int iC = 0;

            //boolSuccess = ConfirmUpdateConsole(strParams, false, false);
            //switch (value) {
            //    case "Atlas Moth":
            //    case "Beet Armyworm":
            //    case "Indian Meal Moth":
            //    case "Ash Pug":
            //    case "Latticed Heath":
            //    case "Ribald Wave":
            //    case "The Streak":
            //        boolSuccess = ConfirmUpdateConsole(strParams, false, false);
            //        return boolSuccess = ConfirmUpdateConsole(strParams, false, false);
            //    default:
            //        return false;
            //}
    }

        //    bool boolSuccess;
        //    string strArgs = "";
        //    string strSwitch = "";
        //    int iP = 0;
        //    int iC = 0;

        //    if (tbConsoleTextDPScan.Text != null) {                                             // Check if Command exists
        //        strSwitch = "get";
        //        switch (strSwitch) {

        //            case ("get"):
        //                strConsoleText = tbConsoleTextDPScan.Text;                                      // Assign entire string 
        //                __console__.strConsoleText = strConsoleText;
        //                tbConsoleTextDPScan.Text = "";
        //                strSwitch = "prms";


        //                goto case ("prms");
        //            case ("prms"):
        //                do {
        //                strConsoleCommands = __console__.strConsoleText.Split(' ');
        //                __console__.strConsoleCommands = strConsoleCommands;
        //                MainForm.queueParams = new Queue();
        //                foreach (string strParams in __console__.strConsoleCommands) {
        //                    if (strParams.Contains("-")) {
        //                        boolSuccess = ConfirmUpdateConsole(strParams, false, false);
        //                        lbxConsoleDPScan.SelectedIndex = lbxConsoleDPScan.Items.Count - 1;  // Set focus behind List index
        //                        lbxConsoleDPScan.SelectedIndex = -1;                                // to achieve auto-scroll effect
        //                        iP++;
        //                        if (!boolSucfcess) {
        //                            strSwitch = "error";
        //                            goto case ("error");
        //                        } else {
        //                            //
        //                        }
        //                    } else {
        //                        strSwitch = "cmds";
        //                    }
        //                        if (!boolSuccess) {
        //                            strSwitch = "error";
        //                        }
        //                } while(strSwitch == "prms") {
        //                    goto case ("error");
        //                }
          
        //                goto case ("cmds");
        //            case ("cmds"):

        //                strConsoleCommands = __console__.strConsoleText.Split(' ');
        //                __console__.strConsoleCommands = strConsoleCommands;
        //                MainForm.queueCommands = new Queue();
        //                foreach (string strCmds in __console__.strConsoleCommands) {
        //                    if (!strCmds.Contains("-")) {
        //                        boolSuccess = ConfirmUpdateConsole(strCmds, false, false);
        //                        lbxConsoleDPScan.SelectedIndex = lbxConsoleDPScan.Items.Count - 1;  // Set focus behind List index
        //                        lbxConsoleDPScan.SelectedIndex = -1;                                // to achieve auto-scroll effect
        //                        iC++;
        //                    }
        //                }
        //                if (!boolSuccess) {
        //                    strSwitch = "error";
        //                    goto case ("error");
        //                }

        //                strSwitch = "message";
        //                goto case ("message");
                    
        //            case ("message"):
        //                strArgs = "Receiving message from server";
        //                boolSuccess = ConfirmUpdateConsole(strArgs, true, false);
        //                string dot = ".";
        //                for (int iWait = 0; iWait < 1 * 800; iWait++) {                         // Loop creating UI effect
        //                    if (dot.Length < 100) {                                             // Only to 100 dots
        //                        dot = dot + ".";
        //                        boolSuccess = ConfirmUpdateConsole(strArgs + dot, false, false);
        //                    } else {
        //                        dot = ".";
        //                    }
        //                    if (!boolSuccess) {
        //                        strSwitch = "error";
        //                        goto case strSwitch = ("error");
        //                    }
        //                }
        //                    strSwitch = "result";
        //                    goto case ("result");
                    
        //            case ("result"):
        //                int count = queueServerSocket.Count;                                    // Count # Lines to Display
        //                strArgs = "Printing [" + count.ToString() + "] Lines:";
        //                boolSuccess = ConfirmUpdateConsole(strArgs, false, false);                   // Remove Queue Item[] & Add                                                                      // Chill out
        //                for (int i = 0; i < count; i++) {                                       // Loop through Queue
        //                    strArgs = queueServerSocket.Dequeue().ToString();
        //                    boolSuccess = ConfirmUpdateConsole(strArgs, false, false);            // Remove Queue Item[] & Add
        //                    lbxConsoleDPScan.SelectedIndex = lbxConsoleDPScan.Items.Count - 1;  // Set focus behind List index
        //                    lbxConsoleDPScan.SelectedIndex = -1;                                // to achieve auto-scroll effect
        //                }
        //                strSwitch = "end";
        //                goto case ("end");
                    
        //            case ("end"):
        //                queueServerSocket.Clear();
        //                strArgs = "Finished...Exit[0]";                                         // Print some success
        //                boolSuccess = ConfirmUpdateConsole(strArgs, false, true);                   // Remove Queue Item[] & Add                                                                      // Chill out
        //                if (!boolSuccess) {
        //                    strSwitch = "error";
        //                    goto case ("error");
        //                }
        //                strSwitch = "default";
        //                goto case ("default");
        //            case ("error"):
        //                strArgs = "Error[-1] No command / No Queue from Server...";
        //                boolSuccess = ConfirmUpdateConsole(strArgs, true, false);
        //                strArgs = "Enter a command below.";
        //                boolSuccess = ConfirmUpdateConsole(strArgs, false, true);
        //                if (!boolSuccess) {
        //                    strSwitch = ("error");
        //                    goto case ("error");
        //                }                                                         

        //                goto case ("default");
        //            case ("default"):
        //                if (boolSuccess) {
        //                    goto case ("end");
        //                }
        //                break;
        //        }
        //    }
        //}

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Event Handler for Select DPScanService Web Service click event Covers the "{MOUSEDOWN}" event.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     .
        /// </param>
        /// <param name="e">
        ///     .
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void btnServerMessageDPscan_MouseDown(object sender, MouseEventArgs e) {
            btnServerMessageDPscan.Text.Insert(0, "");
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Event Handler for DPScanService "{ENTER}" Button. This is for the Click events of the
        /// "{ENTER}" Button.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     .
        /// </param>
        /// <param name="e">
        ///     .
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void btnEnterDPScan_Click(object sender, EventArgs e) {
            string strArgs = "";
            if (ConfirmUpdateConsole(strArgs, true, false)) {
                strArgs = "Processing command... ";
            }
            if (ConfirmUpdateConsole(strArgs, true, false)) {
                strArgs = "Test parse of Commands";
            }
            if (ConfirmUpdateConsole(strArgs, false, false)) {
                strArgs = "Potential Result Set from Command(s) && [args]";
            }
            if (ConfirmUpdateConsole(strArgs, false, true)) {
                strArgs = "";
            }
        }

    ///-------------------------------------------------------------------------------------------------
    /// <summary>
    /// Event Handler for Button Click to reach DPScanService.
    /// </summary>
    ///
    /// <remarks>
    /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
    /// </remarks>
    ///
    /// <param name="sender">
    ///     .
    /// </param>
    /// <param name="e">
    ///     .
    /// </param>
    ///-------------------------------------------------------------------------------------------------

    private void btnServerMessageDPscan_Click(object sender, EventArgs e) {
            //TabControl _tabControl = new TabControl.TabPageCollection <EventHandler>.tabControl1(sender, e);
            SelectDPScanService selectDPScanService = new SelectDPScanService();
            //ObservableCollection<object> dpsscanColl = new ObservableCollection<object>(dpscanservice.CreateObjRef(UriParser));
            selectDPScanService.Show();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Event Handler for DPScanService Start Client Button. Handles the {MOUSECLICK} event. It's a
        /// generic TabControl Focus Event Handler which has all actions for all the tabs. Should be
        /// inherited when possible.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     .
        /// </param>
        /// <param name="e">
        ///     .
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void btnStartClientDPScan_MouseClick(object sender, MouseEventArgs e) {
            //lbxConsoleDPScan.Items.Add(" ");
            //lbxConsoleDPScan.TopIndex++;
            //lbxConsoleDPScan.Items.Add("// Starting DPScanService ClientWorks Engine... ");
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Event Handler for DPScanService Start Client Button. Handles the {CLICK} event. It's a
        /// generic TabControl Focus Event Handler which has all actions for all the tabs. Should be
        /// inherited when possible.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     .
        /// </param>
        /// <param name="e">
        ///     .
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void btnStartClientDPScan_Click(object sender, EventArgs e) {
            string strArgs = "";
            lbxConsoleDPScan.Items.Add(" ");
            lbxConsoleDPScan.TopIndex++;
            lbxConsoleDPScan.Items.Add("// Starting DPScanService ClientWorks Engine... ");
            strArgs = "Starting DPScanService ClientWorks Engine... ";
            if (!ConfirmUpdateConsole(strArgs, true, false)) {
                //MessageBox.Show("ConfirmUpdateConsole : made it happen", "False", MessageBoxButtons.OK);
            } else {
                //MessageBox.Show("DPScanService : Started", "True", MessageBoxButtons.OK);
            }
            int intlistBoxCounter = lbxConsoleDPScan.TopIndex;                                  // Get Top Node INDEX
            int intlistBoxCount = lbxConsoleDPScan.Items.Count;                                 // Get Total Count
            intlistBoxCount = intlistBoxCount - 1;                                              // Offset Top Index Node by Count(-1)
            lbxConsoleDPScan.TopIndex = intlistBoxCount;                                        // This gives us scrolling
            intTabCase = tabControl1.SelectedIndex;

            switch (intTabCase) {
                case 0:
                    goto case 7;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    // DPScanService Tab

                    lbxConsoleDPScan.TopIndex++;
                    lbxConsoleDPScan.TopIndex++;
                    DPscanService _dpscanservice = new DPscanService();
                    dpscanservice = _dpscanservice;
                    lbxConsoleDPScan.Items.Add(" ");
                    lbxConsoleDPScan.TopIndex++;
                    lbxConsoleDPScan.Items.Add("// Looking for DPScanService Authentication Settings... ");
                    lbxConsoleDPScan.TopIndex++;
                    __SOAP__.DPscanService.strUsername = "sysadm";
                    __SOAP__.DPscanService.strPassword = "power95";
                    __SOAP__.DPscanService.strDPScanASMX = "http://www.dpro.com/dpscanservice/dpscan.asmx";
                    __SOAP__.DPscanService.strDPScanWSDL = "http://www.dpro.com/dpscanservice/dpscan.asmx?WSDL";

                    if ((__SOAP__.DPscanService.strUsername != null) && (__SOAP__.DPscanService.strUsername != "")) {
                        lbxConsoleDPScan.Items.Add(" ");
                        lbxConsoleDPScan.TopIndex++;
                        lbxConsoleDPScan.Items.Add("// Username Found! : " + __SOAP__.DPscanService.strUsername);
                        lbxConsoleDPScan.TopIndex++;
                    }

                    if ((__SOAP__.DPscanService.strPassword != null) && (__SOAP__.DPscanService.strPassword != "")) {
                        lbxConsoleDPScan.Items.Add("// Password Found! : " + "******" + " ");
                        lbxConsoleDPScan.Items.Add("// To view or change the current password : Use " + "[ --help ]");
                        lbxConsoleDPScan.TopIndex++;
                        lbxConsoleDPScan.Items.Add(" ");
                        lbxConsoleDPScan.TopIndex++;
                        //if ((__SOAP__.DPscanService.strUsername.ToString() == __SOAP__.DPscanService.strPassword.ToString())) {
                        //    lbxConsoleDPScan.Items.Add( ";// ;//   WARNING!!!  ;// ;// ");
                        //    lbxConsoleDPScan.TopIndex++;
                        //    lbxConsoleDPScan.Items.Add( "// We noticed your Username and Password are the same.  Are you sure this is the best way to go?");
                        //    lbxConsoleDPScan.TopIndex++;
                        //    lbxConsoleDPScan.Items.Add( " ");
                        //    lbxConsoleDPScan.TopIndex++;
                        //}
                        lbxConsoleDPScan.Items.Add("// Looking for DPScanService Web Service Configurations... ");
                        lbxConsoleDPScan.TopIndex++;

                    }

                    if ((__SOAP__.DPscanService.strDPScanASMX != null) && (__SOAP__.DPscanService.strDPScanASMX != "")) {
                        lbxConsoleDPScan.Items.Add("// Found DPScanService Web ASMX Address : " + __SOAP__.DPscanService.strDPScanASMX);
                        lbxConsoleDPScan.TopIndex++;
                    }

                    if ((__SOAP__.DPscanService.strDPScanWSDL != null) && (__SOAP__.DPscanService.strDPScanASMX != "")) {
                        lbxConsoleDPScan.Items.Add("// Found DPScanService Web WSDL Address : " + __SOAP__.DPscanService.strDPScanWSDL);
                        lbxConsoleDPScan.TopIndex++;

                    }

                    if ((__SOAP__.DPscanService.uriDPScanASMX != null) && (__SOAP__.DPscanService.strDPScanASMX != "")) {
                        lbxConsoleDPScan.Items.Add("// Initializing and Setting Up DPScanService Web ASMX URI... : " + __SOAP__.DPscanService.uriDPScanASMX.ToString());
                        lbxConsoleDPScan.TopIndex++;
                        UriFormatException parsingError = new UriFormatException();
                        //UriParser uriParsed = Uri.(__SOAP__.DPscanService.uriDPScanASMX.ToString());
                        //UriParser uriParsed = UriParser.dpscanUriASMX.Uri();// this.(dpscanUriASMX);
                        


                        //UriParser _uriParser = DpfsLinkTestApplication.__SOAP__.DPscanService.strDPScanASMX.
                        //    <_uriParser>(__DPscanService.uriDPScanASMX);

                        //DPScanService.__SOAP__.DPscanService.uriDPScanASMX<__SOAP__.DPscanService)();
                        lbxConsoleDPScan.Items.Add("// Loading DPScanService ASMX URI Location...");
                        lbxConsoleDPScan.TopIndex++;
                        lbxConsoleDPScan.Items.Add("// Loading DPScanService ASMX URI Location...");
                        lbxConsoleDPScan.TopIndex++;
                        lbxConsoleDPScan.Items.Add("// Loading DPScanService ASMX URI Location...");
                        lbxConsoleDPScan.TopIndex++;
                        lbxConsoleDPScan.Items.Add("// Loading DPScanService ASMX URI Location...");
                        lbxConsoleDPScan.TopIndex++;
                        lbxConsoleDPScan.Items.Add("// Loading DPScanService ASMX URI Location...");
                        lbxConsoleDPScan.TopIndex++;
                        lbxConsoleDPScan.Items.Add("// Loading DPScanService ASMX URI Location...");
                        lbxConsoleDPScan.TopIndex++;
                        lbxConsoleDPScan.Items.Add("// Loading DPScanService ASMX URI Location...");
                        lbxConsoleDPScan.TopIndex++;
                        lbxConsoleDPScan.Items.Add("// Loading DPScanService ASMX URI Location...");
                        lbxConsoleDPScan.TopIndex++;

                    }

                    if ((__SOAP__.DPscanService.uriDPScanASMX != null) && (__SOAP__.DPscanService.strDPScanWSDL != "")) {
                        lbxConsoleDPScan.Items.Add("// Recording DPScanService Web WSDL URI... : " + __SOAP__.DPscanService.strDPScanWSDL);
                        lbxConsoleDPScan.TopIndex++;
                        lbxConsoleDPScan.Items.Add("// Logging DPScanService WSDL URI Location...");
                        lbxConsoleDPScan.TopIndex++;
                        lbxConsoleDPScan.Items.Add(" ");
                        lbxConsoleDPScan.TopIndex++;
                        break;
                    }

                    if (__SOAP__.DPscanService.strDPScanWSDL == "") {
                        lbxConsoleDPScan.Items.Add("// For some reason we found the DPScanWSDL string format HTTP was empty : ");
                        lbxConsoleDPScan.TopIndex++;
                        // No Username on record
                        __SOAP__.DPscanService.boolAccountAccess = false;
                        __console__.IsLogin = false;
                        //lbxConsoleDPScan.Items.Add("// Part of DPScanService ASMX 'URI' Configuration may have errors : ");
                        //lbxConsoleDPScan.TopIndex++;
                        //lbxConsoleDPScan.Items.Add("// DPScanService WSDL HTTP Address' Configuration may have errors : : ");
                        //lbxConsoleDPScan.TopIndex++;
                        //lbxConsoleDPScan.Items.Add("// We also run a check with web services at this point...check your DNS...");
                        //lbxConsoleDPScan.TopIndex++;
                        //lbxConsoleDPScan.Items.Add("// Opening up configuration settings...");
                        //lbxConsoleDPScan.TopIndex++;
                        goto case 25;
                    }
                    if (__SOAP__.DPscanService.AccessDPScan(dpscanservice)) {
                        __SOAP__.DPscanService.boolAccountAccess = true;
                        __console__.IsLogin = true;
                    }
                    goto case 22;
                case 8:
                    // Trying New User Login Authentication.
                    if (!DPscanService.AccessDPScan(dpscanservice)) {
                        __console__.IsLogin = false;
                        // Login Failed
                        lbxConsoleDPScan.Items.Add("// Connecting to User Authentication Servers...");
                        lbxConsoleDPScan.TopIndex++;
                        lbxConsoleDPScan.Items.Add("// Please be patient...this may take a few seconds.");
                        lbxConsoleDPScan.TopIndex++;
                        lbxConsoleDPScan.Items.Add("// User Authentication : FAILURE!");
                        lbxConsoleDPScan.TopIndex++;
                        lbxConsoleDPScan.Items.Add("// Tracing Internet Protocol Routing...");
                        lbxConsoleDPScan.TopIndex++;
                        lbxConsoleDPScan.TopIndex++;
                        lbxConsoleDPScan.Items.Add("// Logging IP Address, location, and client information...");
                        lbxConsoleDPScan.TopIndex++;
                        lbxConsoleDPScan.Items.Add("// Sending notification to administrators...");
                        lbxConsoleDPScan.TopIndex++;
                        lbxConsoleDPScan.Items.Add("// [Good]bye");
                        lbxConsoleDPScan.TopIndex++;
                        lbxConsoleDPScan.Items.Add(" ");
                        lbxConsoleDPScan.TopIndex++;

                        goto case 9;
                    }
                    if (DPscanService.AccessDPScan(dpscanservice)) {
                        // Login Success
                        lbxConsoleDPScan.Items.Add("// Connecting to User Authentication Servers...");
                        lbxConsoleDPScan.TopIndex++;
                        lbxConsoleDPScan.Items.Add("// Please be patient...this may take a few seconds.");
                        lbxConsoleDPScan.TopIndex++;
                        lbxConsoleDPScan.Items.Add("// User Authentication : SUCCESS!");
                        lbxConsoleDPScan.TopIndex++;
                        lbxConsoleDPScan.Items.Add("// Welcome! : " + __SOAP__.DPscanService.strUsername);
                        lbxConsoleDPScan.TopIndex++;
                        lbxConsoleDPScan.Items.Add(" ");
                        lbxConsoleDPScan.TopIndex++;
                        __SOAP__.DPscanService.boolAccountAccess = true;
                        __console__.IsLogin = true;

                    }
                    goto case 10;
                case 9:
                    // New User Login Failure!
                    lbxConsoleDPScan.Items.Add("// User Login Failed!");
                    lbxConsoleDPScan.TopIndex++;
                    lbxConsoleDPScan.Items.Add("// Nothing left to do.");
                    lbxConsoleDPScan.TopIndex++;
                    lbxConsoleDPScan.Items.Add("// ...Shutting Down!");
                    lbxConsoleDPScan.TopIndex++;
                    break;
                case 10:
                    // New User Login Success!
                    lbxConsoleDPScan.Items.Add("// User Credentials Successful.");
                    lbxConsoleDPScan.TopIndex++;
                    lbxConsoleDPScan.Items.Add("// NOTE : Other DPScanService web service account access may require different privileges.");
                    lbxConsoleDPScan.TopIndex++;
                    lbxConsoleDPScan.Items.Add(" ");
                    lbxConsoleDPScan.TopIndex++;
                    lbxConsoleDPScan.Items.Add(" ");
                    lbxConsoleDPScan.TopIndex++;
                    break;
                case 11:
                    break;
                case 12:
                    break;
                case 13:
                    break;
                case 14:
                    break;
                case 15:
                    break;
                case 16:
                    break;
                case 17:
                    break;
                case 18:
                    break;
                case 19:
                    // Getting Username from Console
                    lbxConsoleDPScan.Items.Add(" ");
                    lbxConsoleDPScan.TopIndex++;
                    lbxConsoleDPScan.Items.Add("// Username and Password are Empty!");
                    lbxConsoleDPScan.TopIndex++;
                    lbxConsoleDPScan.Items.Add("// Enter Username : ");
                    lbxConsoleDPScan.TopIndex++;
                    lbxConsoleDPScan.Items.Add("// Type a username in the console below.");
                    lbxConsoleDPScan.TopIndex++;
                    lbxConsoleDPScan.Items.Add("// Press or click 'ENTER' to login.");
                    lbxConsoleDPScan.TopIndex++;
                    lbxConsoleDPScan.Items.Add("//");
                    lbxConsoleDPScan.TopIndex++;
                    // Giving User Instructions for Console UI
                    intUIQuestions.SetValue(30, intUIQuestions.Length);
                    whyUpdateArgs["strUsername"] = true;
                    dictWaitingforUI["strUsername"] = "";
                    goto case 30;
                case 20:
                    // Getting Password from Console 
                    lbxConsoleDPScan.Items.Add("//");
                    lbxConsoleDPScan.TopIndex++;
                    lbxConsoleDPScan.Items.Add("// Enter Password : ");
                    lbxConsoleDPScan.TopIndex++;
                    lbxConsoleDPScan.Items.Add("//");
                    lbxConsoleDPScan.TopIndex++;
                    lbxConsoleDPScan.Items.Add("// Thank you, " + __SOAP__.DPscanService.strUsername);
                    lbxConsoleDPScan.TopIndex++;
                    // Giving User Instructions for Console UI
                    intUIQuestions.SetValue(31, intUIQuestions.Length);
                    whyUpdateArgs["strPassword"] = true;
                    dictWaitingforUI["strPassword"] = "";
                    goto case 31;
                case 21:
                    break;
                case 22:
                    // Attempting User Login Authentication.
                    if (__SOAP__.DPscanService.boolAccountAccess) {
                        lbxConsoleDPScan.Items.Add("// Connecting to User Authentication Servers...");
                        lbxConsoleDPScan.TopIndex++;
                        lbxConsoleDPScan.Items.Add("// Please be patient while we connect you...");
                        lbxConsoleDPScan.TopIndex++;
                        lbxConsoleDPScan.Items.Add("// User Authentication : SUCCESS!");
                        lbxConsoleDPScan.TopIndex++;
                        lbxConsoleDPScan.Items.Add("// Welcome! : " + __SOAP__.DPscanService.strUsername);
                        lbxConsoleDPScan.TopIndex++;
                        lbxConsoleDPScan.Items.Add(" ");
                        lbxConsoleDPScan.TopIndex++;
                        __console__.IsLogin = true;
                        goto case 23;
                    }
                    break;
                case 23:
                    //
                    // Testing for a separate Function<Tbool> used to send single lines of text 
                    if (ConfirmUpdateConsole("Testing a manual string arguement", false, false)) {
                        if (ConfirmUpdateConsole(strArgs + "test original string with addition", false, false)) {
                            strArgs = "Brand new Text Line : With Crazy string reference " + __console__.strConsoleText;
                            if (ConfirmUpdateConsole(strArgs, false, true)) {
                                MessageBox.Show("ConfirmUpdateConsole : made it happen", "False", MessageBoxButtons.OK);
                            }
                        }
                    } else {
                        MessageBox.Show("ConfirmUpdateConsole : manual string arguement", "False", MessageBoxButtons.OK);
                        if (ConfirmUpdateConsole(strArgs + "test original string with addition", false, false)) {
                            strArgs = "Brand new Text Line : With Crazy string reference " + __console__.strConsoleText;
                            if (ConfirmUpdateConsole(strArgs, false, true)) {
                                MessageBox.Show("ConfirmUpdateConsole : made it happen", "False", MessageBoxButtons.OK);
                            }
                        }
                    }
                    MessageBox.Show("ConfirmUpdateConsole : made it happen", "True", MessageBoxButtons.OK);
                    break;
                case 24:
                    break;
                case 25:
                    // Verifying User Authentication...
                    // User Login Gathering
                    __SOAP__.DPscanService.aRequest.Username = __SOAP__.DPscanService.strUsername;
                    __SOAP__.DPscanService.aRequest.Password = __SOAP__.DPscanService.strPassword;
                    __SOAP__.DPscanService.aResponse = dpscanservice.CountAccess(__SOAP__.DPscanService.aRequest);
                    lbxConsoleDPScan.Items.Add("// Verifying User Authentication... : ");
                    lbxConsoleDPScan.TopIndex++;
                    lbxConsoleDPScan.Items.Add("// Checking Web Service ASMX basic CountAccess... : ");
                    lbxConsoleDPScan.TopIndex++;
                    if (__SOAP__.DPscanService.aResponse.Success) {
                        MessageBox.Show("You checked out", " : ) ", MessageBoxButtons.OK);
                        lbxConsoleDPScan.Items.Add("// Connection closed : Thank you " + __SOAP__.DPscanService.strUsername);
                        lbxConsoleDPScan.TopIndex++;
                        DialogResult resultTest = MessageBox.Show("Test Timer Code?", "BetaMode", MessageBoxButtons.YesNo);
                        if (resultTest == DialogResult.Yes) {
                            goto case 30;
                        } else {
                            if (resultTest == DialogResult.No) {
                                break;
                            }
                        }
                    }
                    if (!__SOAP__.DPscanService.aResponse.Success) {
                        MessageBox.Show("Mistakes were made.", " Login Unsuccessfull ", MessageBoxButtons.OK);
                        MessageBox.Show("Error Code : " + __SOAP__.DPscanService.aResponse.ErrorCode + " Error Desc : " +
                        __SOAP__.DPscanService.aResponse.ErrorDesc.ToString(), "This is still testing", MessageBoxButtons.OK);

                    }
                    break;
                case 26:
                    break;
                case 27:
                    // Waiting for TimeOut
                    int iC = 0;
                    intTimeOut = 20;
                    strTimeOut = "TimeOut : " + intTimeOut;
                    // Start TimeOut Loop
                    while (stopwatch.Elapsed.Seconds < 20) {
                        lbxConsoleDPScan.Items.Add(strTimeOut);
                        intTimeOut = intTimeOut - 1;
                    }
                    if (boolWaitingforUI) {
                        intTimeOut = 20;
                        intLoopsWaitingforUI++;
                        goto case 30;
                    } else {
                        intTimeOut = 0;
                        intLoopsWaitingforUI = 0;
                        goto case 30;
                    }
                case 30:
                    // Getting Username from Console 
                    if ((intTimeOut != 0) || (intLoopsWaitingforUI == 0)) {
                        goto case 27;
                    } else
                        for (iC = 0; iC < 20; intLoopsWaitingforUI++) {
                            if (intLoopsWaitingforUI == 20) {
                                MessageBox.Show("intLoopsWaitingforUI > 20", "", MessageBoxButtons.OK);
                            }
                        }
                    // Gave Username Instructions (Part 1 of 3)
                    boolconsoleUI[0] = true;
                    boolWaitingforUI = true;

                    if (stopwatch.IsRunning) {
                        if (boolWaitingforUI) {
                            // Reset Console
                        } else {                                                    // Not Waiting for UI
                            if (intTabCase == 7) {
                                stopwatch.Stop();
                                boolWaitingforUI = false;
                                boolconsoleUI.Initialize();
                            }
                            // Timer is now reset 
                        }
                        if (!boolWaitingforUI) {
                            boolWaitingforUI = false;
                            boolconsoleUI.Initialize();
                        } else {
                        }
                    } else {
                        if (!stopwatch.IsRunning) {
                            boolWaitingforUI = true;
                            strConsoleCommands[0] = tbConsoleTextDPScan.Text;

                            boolWaitingforUI = false;
                            boolconsoleUI.Initialize();
                        } else {
                            stopwatch.Reset();
                        }
                    }
                    break;
                case 31:
                    break;
                case 32:
                    break;
                case 50:
                    // This case is for the decision process within the application for Outstanding UI we are waiting for.
                    if (intUIQuestions.Length != 0) {
                        int iX = intUIQuestions.LastOrDefault();
                        if (iX.Equals(30)) {
                            goto case 30;
                        } else if (iX.Equals(31)) {
                            goto case 31;
                        } else if (iX.Equals(32)) {
                            goto case 32;
                        }
                        break;
                    } else {
                        break;
                    }
            }
        #endregion
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Retrieves the next control forward or back in thetab order of child records.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     .
        /// </param>
        /// <param name="e">
        ///     .
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void lbxConsoleDPScan_DisplayMemberChanged(object sender, EventArgs e) {
            //lbxConsoleDPScan.GetNextControl(lbxConsoleDPScan.SetSelected, false);
            lbxConsoleDPScan.SelectNextControl(lbxConsoleDPScan, true, true, true, false);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Simple function to send text to any console with UI updates for user.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="strArg">
        ///     Line of Text as string.
        /// </param>
        /// <param name="first">
        ///     bool indicating this Line of Text is the first in a series.
        /// </param>
        /// <param name="last">
        ///     bool indicating this Line of Text is the last in a series.
        /// </param>
        ///
        /// <returns>
        /// true if it succeeds, false if it fails.
        /// </returns>
        ///-------------------------------------------------------------------------------------------------

        public bool ConfirmUpdateConsole(string strArg, bool first, bool last) {
            string strTabTitle = "";
            int intTabIndex = 0;
            int intlistBoxCounter = 0;
            int intlistBoxCount = 0;
            int intSwitch = 0;
            int intFirst = 0;
            int intLast = 0;
            strTabTitle = tabControl1.SelectedTab.Text;
            intTabIndex = tabControl1.SelectedIndex;
            intlistBoxCounter = lbxConsoleDPScan.TopIndex;                                      // Get Top Node INDEX
            intlistBoxCount = lbxConsoleDPScan.Items.Count;                                     // Get Total Count
            intlistBoxCount = intlistBoxCount - 1;                                              // Offset Top Index Node by Count(-1)
            lbxConsoleDPScan.TopIndex = intlistBoxCount;                                        // This gives us scrolling
            //intTabIndex = tabControl1.SelectedIndex;
            __console__.strConsoleText = "";
            __console__.strConsoleText = lbxConsoleDPScan.Text;
            if (first)intFirst++;
            if (last)intLast++;
            intSwitch = intLast + intFirst;
            try {
                switch (intSwitch) {
                    case 0:
                        // No Flags
                        lbxConsoleDPScan.Items.Add("// " + strArg);
                        lbxConsoleDPScan.TopIndex++;
                        return true;
                    case 1:
                        // One Flag
                        // Must be first or last Args
                        if (first) {
                            lbxConsoleDPScan.Items.Add("// Incoming data received from : [ " + strTabTitle + " {" + intTabIndex.ToString() + "/" + lbxConsoleDPScan.TopIndex + "} ] on " + DateTime.Now.ToLongDateString() + ".");
                            lbxConsoleDPScan.TopIndex++;
                            lbxConsoleDPScan.Items.Add("// " + strArg);
                        } else {
                            goto case 3;
                        }
                            return true;
                    case 2:
                        //
                        // Two Flags
                        // Must be first and last
                        lbxConsoleDPScan.Items.Add(" ");
                        lbxConsoleDPScan.TopIndex++;
                        lbxConsoleDPScan.Items.Add("// Incoming data received from : [ " + strTabTitle + " {" + intTabIndex.ToString() + "/" + lbxConsoleDPScan.TopIndex + "} ] on " + DateTime.Now.ToLongDateString() + ".");
                        lbxConsoleDPScan.Items.Add("// " + strArg);
                        return true;
                    case 3:
                        //
                        // Must be last Args
                        lbxConsoleDPScan.Items.Add("// " + strArg);
                        lbxConsoleDPScan.TopIndex++;
                        lbxConsoleDPScan.Items.Add("// Connection Closed : [ " + strTabTitle + " / " + last.ToString() + " ]   { " + intTabIndex.ToString() + "/" + lbxConsoleDPScan.TopIndex + "}  on  " + DateTime.Now.ToLongDateString() + ".");
                        return true;
                }
            } catch (Exception ex) {
                MessageBox.Show("Update Failed : " + ex.Message, "Console Error", MessageBoxButtons.OK);
                return false;
            } finally {
                lbxConsoleDPScan.Text = "";
                strTabTitle = tabControl1.SelectedTab.Text;
                tabControl1.SelectedIndex = intTabIndex;
                intlistBoxCounter = lbxConsoleDPScan.TopIndex;
                intlistBoxCount = lbxConsoleDPScan.Items.Count;
                intlistBoxCount = intlistBoxCount - 1;
                lbxConsoleDPScan.TopIndex = intlistBoxCount;
            }
            return true;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Event Handler for Console Text Copy {CLICK} event.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     object.
        /// </param>
        /// <param name="e">
        ///     EventArgs.
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void btnCopyConsoleText_Click(object sender, EventArgs e) {
            IDataObject clipToConsole_obj = new DataObject(Clipboard.GetDataObject());
            ObservableCollection<object> collObsObj = new ObservableCollection<object>();
            IDictionary<string,object> dictClipboard = new Dictionary<string,object>();
            IDataObject clipFromConsole_obj = new DataObject();
            int intCountlbx = lbxConsoleDPScan.Items.Count;
            bool boolIsSel = ((lbxConsoleDPScan.TopIndex - lbxConsoleDPScan.Items.Count) != 0);
            foreach (var format in clipFromConsole_obj.GetFormats()) {
                switch(boolIsSel) {
                    case true:
                        dictClipboard.Add(format, clipToConsole_obj.GetData(format));
                        break;
                    case false:
                        dictClipboard.Add(format, clipToConsole_obj.GetData(format));
                        break;
                }
            }
            __console__.DictClipboard = new Dictionary<string,object>(dictClipboard);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets clipboard data.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <returns>
        /// The clipboard data.
        /// </returns>
        ///-------------------------------------------------------------------------------------------------

        public IDictionary<string, object> GetClipboardData() {
            var dict = new Dictionary<string, object>();
            var dataObject = Clipboard.GetDataObject();
            foreach (var format in dataObject.GetFormats()) {
                dict.Add(format, dataObject.GetData(format));
            }
            return dict;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Sets clipboard data.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="dict">
        ///     .
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        public void SetClipboardData(IDictionary<string, object> dict) {
            var dataObject = Clipboard.GetDataObject();
            foreach (var kvp in dict) {
                dataObject.SetData(kvp.Key, kvp.Value);
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Event Handler for Clipboard Get From Console {CLICK} event.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     object.
        /// </param>
        /// <param name="e">
        ///     EventArgs.
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void btnGetClipboard_Click(object sender, EventArgs e) {
            int intCountlbx = 0;
            //IDataObject clipFromConsole_obj = new DataObject();
            IDataObject clipToConsole_obj = new DataObject();
            ObservableCollection<object> collObsObj = new ObservableCollection<object>();
            IDictionary<string, object> dictClipboard = new Dictionary<string, object>();
            // Get accurate count of lines/items in Console
            intCountlbx = intCountlbx + lbxConsoleDPScan.Items.Count;
            clipToConsole_obj = Clipboard.GetDataObject();
            // Do something with the clipboard...
            var backup = GetClipboardData();    // Guest code
            SetClipboardData(backup);           // Guest code
            // Do something with the clipboard...
            foreach (var format in clipToConsole_obj.GetFormats()) {
                dictClipboard.Add(format, clipToConsole_obj.GetData(format));
            }
            __console__.DictClipboard = new Dictionary<string, object>(dictClipboard);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Event Handler for Clipboard Set From Console {CLICK} event.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     object.
        /// </param>
        /// <param name="e">
        ///     EventArgs.
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void btnSetClipboard_Click(object sender, EventArgs e) {
            int intCountlbx = 0;
            IDataObject clipFromConsole_obj = new DataObject();
            IDataObject clipToConsole_obj = new DataObject();
            ObservableCollection<object> collObsObj = new ObservableCollection<object>();
            IDictionary<string, object> dictClipboard = new Dictionary<string, object>();
            // Get accurate count of lines/items in Console
            intCountlbx = intCountlbx + lbxConsoleDPScan.Items.Count;
            //clipFromConsole_obj = Clipboard.GetDataObject();
            dictClipboard = new Dictionary<string, object>(__console__.DictClipboard);
            // Do something with the clipboard...
            foreach (var kvp in dictClipboard) {
                clipFromConsole_obj.SetData(kvp.Key, kvp.Value);
                intCountlbx++;
            }
            Clipboard.SetDataObject(clipFromConsole_obj, true, 2, 2);
           __console__.ClipFromConsole_obj.SetData(dictClipboard);
            __console__.ClipFromConsole_obj.SetData(clipFromConsole_obj);
            string strArgs = "Found a Console Buffer of  [" + intCountlbx + " ] ";
            if (ConfirmUpdateConsole(strArgs, true, true)) {
                
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Event Handler for DPScan Console Enter Button {MOUSEUP} event.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     object.
        /// </param>
        /// <param name="e">
        ///     MouseEventArgs.
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void btnEnterDPScan_MouseUp(object sender, MouseEventArgs e) {
            if (btnEnterDPScan.Focused) {
                btnEnterDPScan.Text = "enter";
            } else {
                Application.DoEvents();
            }
            if (!btnEnterDPScan.Focused) {
                btnEnterDPScan.Focus();
                btnEnterDPScan.Text = "enter";
            } else {
                btnEnterDPScan.Focus();
                btnEnterDPScan.Text = "enter";
            }
            btnEnterDPScan.Text = "enter";
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Event Handler for DPScan Console Enter button {MOUSEDOWN} event.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     object.
        /// </param>
        /// <param name="e">
        ///     MouseEventArgs.
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void btnEnterDPScan_MouseDown(object sender, MouseEventArgs e) {
            if (btnEnterDPScan.Focused) {
                btnEnterDPScan.Text = "";
            } else {
                Application.DoEvents();
            }
            if (!btnEnterDPScan.Focused) {
                btnEnterDPScan.Focus();
                btnEnterDPScan.Text = "";
            } else {
                btnEnterDPScan.Focus();
                btnEnterDPScan.Text = "";
            }
            btnEnterDPScan.Text = "";
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Event Handler for DEBUG Open Form Button {CLICK} event.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     object.
        /// </param>
        /// <param name="e">
        ///     EventArgs.
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void btn_DEBUG__Click(object sender, EventArgs e) {
            // Bring up a new Class that loads public properties into a WinFOrm to display 
            // Log data, statistics, and values, etc.
            //__debug__ debugForm = new __debug__();
            
            // Load any __DEBUG__ Classes
            com.dpro.www.DPScanService dp = new DPScanService();
           // __debug__.DebugDataSources.Add("this", "test");
           // MessageBox.Show(__debug__.DebugDataSources.ContainsKey("this").ToString(), "__DEBUG__", MessageBoxButtons.OK);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Provides Global Cancel Actions for any control with focus. Hidden Button provides Event
        /// Handler for escape key event.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     object.
        /// </param>
        /// <param name="e">
        ///     EventArgs.
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void btnGlobalCancel_Click(object sender, EventArgs e) {

        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Event handler. Called by lbxConsole for selected index changed events.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     .
        /// </param>
        /// <param name="e">
        ///     Event information.
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void lbxConsole_SelectedIndexChanged(object sender, EventArgs e) {

        }


    }
}

