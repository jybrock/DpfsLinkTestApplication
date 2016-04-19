using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DpfsLinkTestApplication.OldCode {
    class oldcode {
    }
}

    #region Object "Type Conversions
    ////Converting FROM:
    //class MyType {
    //    public static explicit operator MyType(MyOtherType mo) {
    //        // code to convert from MyOtherType to MyType 
    //        // and return a MyType object ie.
    //        MyType m = new MyType();
    //        m.MyProperty = mo.MyOtherTypeProperty;
    //        return m;
    //    }
    //}

    ////Converting TO:
    //class MyType {
    //    public static implicit operator MyOtherType(MyType typ) {
    //        // code to convert from  MyType to MyOtherType 
    //        // and return a MyOtherType object ie.
    //        MyOtherType m = new MyOtherType();
    //        m.MyOtherTypeProperty = typ.MyTypeProperty;
    //        return m;
    //    }
    //}
    #endregion


#region Event Handler for KeyPress Args and KeyPress
 ///// <summary>
 //       /// Event Handler for TextBox on DPScanService Panel.
 //       /// Takes care of "{ENTER}" Key.
 //       /// </summary>
 //       /// <param name="sender"></param>
 //       /// <param name="e">KeyEventArgs</param>
 //       private void tbConsoleTextDPScan_Enter(object sender, KeyEventArgs e) {
 //           if (tbConsoleTextDPScan.Text != null) {                                             // Check if Command exists
 //               strConsoleText = tbConsoleTextDPScan.Text;                          // Assign entire string 
 //               __console__.strConsoleText = strConsoleText;
 //               tbConsoleTextDPScan.Text = "";
 //               int iP = 0;
 //               strConsoleCommands = __console__.strConsoleText.Split(' ');
 //               __console__.strConsoleCommands = strConsoleCommands;
 //               MainForm.queueParams = new Queue();
 //               foreach (string strCmd in __console__.strConsoleCommands) {
 //                   if (strCmd.Contains("-")) {
 //                       queueParams.Enqueue(strCmd);
 //                       lbxConsoleDPScan.SelectedIndex = lbxConsoleDPScan.Items.Count - 1;  // Set focus behind List index
 //                       lbxConsoleDPScan.SelectedIndex = -1;                                // to achieve auto-scroll effect
 //                       iP++;
 //                   }
 //               }
 //               int iC = 0;
 //               strConsoleCommands = __console__.strConsoleText.Split(' ');
 //               __console__.strConsoleCommands = strConsoleCommands;
 //               MainForm.queueCommands = new Queue();
 //               foreach (string strCmd in __console__.strConsoleCommands) {
 //                   if (!strCmd.Contains("-")) {
 //                       queueCommands.Enqueue(strCmd);
 //                       lbxConsoleDPScan.SelectedIndex = lbxConsoleDPScan.Items.Count - 1;  // Set focus behind List index
 //                       lbxConsoleDPScan.SelectedIndex = -1;                                // to achieve auto-scroll effect
 //                       iC++;
 //                   }
 //               }
 //               return 0;
 //           } else if (queueServerSocket.Peek() != null) {                              // Check if Queue exists
 //               lbxConsoleDPScan.Items.Clear();                                         // Clear out the display
 //               string strReceiving = "Receiving message from server";
 //               lbxConsoleDPScan.Items.Add(strReceiving);
 //               string dot = ".";
 //               for (int iWait = 0; iWait < 1 * 800; iWait++) {                         // Loop creating UI effect
 //                   if (dot.Length < 100) {                                             // Only to 100 dots
 //                       dot = dot + ".";
 //                       lbxConsoleDPScan.Items.Insert(0, strReceiving + dot);
 //                   } else {
 //                       dot = ".";
 //                   }
 //               }
 //               int count = queueServerSocket.Count;                                    // Count # Lines to Display
 //               lbxConsoleDPScan.Items.Add("Printing [" + count.ToString()
 //                   + "] Lines:");
 //               Thread.Sleep(5000);                                                     // Chill out
 //               for (int i = 0; i < count; i++) {                                       // Loop through Queue
 //                   lbxConsoleDPScan.Items.Add(queueServerSocket.Dequeue());            // Remove Queue Item[] & Add
 //                   lbxConsoleDPScan.SelectedIndex = lbxConsoleDPScan.Items.Count - 1;  // Set focus behind List index
 //                   lbxConsoleDPScan.SelectedIndex = -1;                                // to achieve auto-scroll effect
 //               }
 //               lbxConsoleDPScan.Items.Add("Finished...Exit[0]");                       // Print some success
 //               queueServerSocket.Clear();                                              // Clean the Queue
 //               return 0;
 //           } else {                                                                    // Display an Error - Mistakes were made!
 //               lbxConsoleDPScan.Items.Add("Error[-1] No command / No Queue from Server...");
 //               lbxConsoleDPScan.Items.Add("Enter a command below.");
 //               return -1;                                                              // Return [-1] Error - Mistakes were made
 //           }
 //       }
#endregion

#region old code
//if (intCapturedPacketCount > 0) {
        //    if (bulkCaptureAnswer == DialogResult.Yes) {
        //        MainForm.boolScanAllNodes = true;
        //        boolScanCheckedNodes = false;
        //        boolScanSelectedNodes = false;
        //    } else if (bulkCaptureAnswer == DialogResult.No) {
        //        boolScanAllNodes = false;
        //        boolScanCheckedNodes = false;
        //        boolScanSelectedNodes = true;
        //    } else {
        //        boolScanAllNodes = false;
        //        boolScanCheckedNodes = false;
        //        boolScanSelectedNodes = false;

        //if (intCapturedPacketCount > 1) {

        //    CheckCaptureAnswer = MessageBox.Show("Capturing: " + Program.CapturedPackets.Count.ToString() + 
        //        " so far. Record by Selection (Recommended Mode)?",
        //        "Total: [" + intCapturedPacketCount + "] ", 
        //        MessageBoxButtons.YesNoCancel);
        //    if (CheckCaptureAnswer == DialogResult.Yes) {
        //        boolScanAllNodes = false;
        //        boolScanCheckedNodes = true;
        //        boolScanSelectedNodes = false;
        //    } else if (CheckCaptureAnswer == DialogResult.No) {
        //        boolScanAllNodes = false;
        //        boolScanCheckedNodes = false;
        //        boolScanSelectedNodes = true;
        //    } else {
        //        boolScanAllNodes = false;
        //        boolScanCheckedNodes = false;
        //        boolScanSelectedNodes = false;
        //    }

        //if (intCapturedPacketCount < 4) {

        //    SelectCaptureAnswer = MessageBox.Show("Capturing: " + Program.CapturedPackets.Count.ToString() + 
        //        " so far. Record (Single IP Header)?",
        //        "Total: [" + intCapturedPacketCount + "] ", 
        //        MessageBoxButtons.YesNoCancel);
        //    if (SelectCaptureAnswer == DialogResult.Yes) {
        //        boolScanAllNodes = false;
        //        boolScanCheckedNodes = false;
        //        boolScanSelectedNodes = true;
        //    } else if (SelectCaptureAnswer == DialogResult.No) {
        //        boolScanAllNodes = false;
        //        boolScanCheckedNodes = true;
        //        boolScanSelectedNodes = false;
        //    } else {
        //        boolScanAllNodes = false;
        //        boolScanCheckedNodes = false;
        //        boolScanSelectedNodes = false;
        #endregion


#region 2-16-2013




//                Dictionary<string,string> loadSettings = new Dictionary<string,string>();
//                Dictionary<string,string>.ValueCollection valueColl = loadSettings.Values;
//                Dictionary<string,string>.KeyCollection keyColl = loadSettings.Keys;

//            foreach(KeyValuePair<string, bool> strIndex in whyUpdateArgs) {
//                bool Value = false;
//                string args = "";
//                loadSettings.
//                if (!(loadSettings.ContainsKey("strUsername")) {
//                    loadSettings.Add("strUsername","sysadm");
//                }
//                if (!(loadSettings.ContainsKey("strPassword")) {
//                    loadSettings.Add("strPassword","power95");
//                    }
//                    if (!(loadSettings.ContainsKey("IsLogin")) {
//                    loadSettings.Add("IsLogin","");
//                    }
//                    if (!(loadSettings.ContainsKey("IsRememberMe")) {
//                    loadSettings.Add("IsRememberMe","");
//                    }
//                    if (!(loadSettings.ContainsKey("strBarCode")) {
//                    loadSettings.Add("strBarCode","");
//                    }
//                    if (!(loadSettings.ContainsKey("strQRCode")) {
//                    loadSettings.Add("strQRCode","");
//                    }
//                    if (!(loadSettings.ContainsKey("intQuantity")) {
//                    loadSettings.Add("intQuantity","");
//                    }
//                    if (!(loadSettings.ContainsKey("strQuantity")) {
//                    loadSettings.Add("strQuantity","");
//                    }
//                    if (!(loadSettings.ContainsKey("strItem")) {
//                    loadSettings.Add("strItem","");
//                    }
//                    if (!(loadSettings.ContainsKey("strDemoMode")) {
//                    loadSettings.Add("strDemoMode","No");
//                    }


//                if (!(loadSettings.ContainsKey("strUsername")) {

//                    loadSettings.Add("intQuantity","sysadm");
//                    loadSettings.Add("intQuantity","sysadm");
//                    loadSettings.Add("intQuantity","sysadm");
//                    loadSettings.Add("intQuantity","sysadm");
//                }
//                if (whyUpdateArgs.TryGetValue("strUsername", out Value)) {   


//                }

//                    //, out true)) {

//                    loadSettings.Add(strIndex,"Fill");

//                }

//                  foreach(bool b in valueColl) {
//                    //Console.WriteLine("Value = {0}", s);

//                  }

//                if (strIndex.Value) {
//                   args = strIndex.Key;




//                   

//                    }
//                }
//                if (whyUpdateArgs[strIndex]) {
//                }
//                switch(args) {
//                case "strUsername":
//                        if (MainForm.intTabCase > 0) {
//                if (uiWaiting) {
//                    if (whyUpdateArgs["strUsername"]) {
//                    }
//                    if ((string)globalSettings["strUsername"] != null) {
//                        if ((string)globalSettings["strUsername"] != "") {
//                            strUsername = (string)globalSettings["strUsername"];
//                        } else {
//                            if ((consoleTextBox != null) && (!uiWaiting)) {
//                                strUsername = (string)globalSettings["strUsername"];
//                            } else {
//                                if ((consoleTextBox != null) && (uiWaiting)) {
//                                    strUsername = consoleTextBox;
//                                }
//                            }
//                        }
//                    }
//                } else {
//                    if (!uiWaiting) {
//                        strUsername = (string)globalSettings["strUsername"];
//                    }
//                }
//                        }
//                        break;
//                    case "strPassword":
//                        break;
//                    case "strDPScanServiceASMX":
//                }
//                        globalSettings.Add("strUsername", string.Empty);
//                    if ((string)globalSettings["strPassword"] != null) {
//                        strPassword = (string)globalSettings["strPassword"];
//                    } else
//                        globalSettings.Add("strPassword", string.Empty);
//                    if (!(bool)globalSettings["IsLogin"]) {
//                        IsLogin = (bool)globalSettings["IsLogin"];
//                    } else
//                        globalSettings.Add("IsLogin", true);
//                    if (!(bool)globalSettings["IsRememberMe"]) {
//                        IsRememberMe = (bool)globalSettings["IsRememberMe"];
//                    } else 
//                        globalSettings.Add("IsRememberMe", true);
//                    }
//                }
#endregion
//private void tbxNode_TextChanged(object sender, EventArgs e) {
        //    BitArray LayerNodeArray0 = new BitArray(32,false);
        //    for (int i = 0; i < bits.Length; i++)
        //        bits[i] = (IsSquare(i) ^ IsFibonacci(i));
        //        string txt = "";
        //    txt += "# True: " + bits.NumTrue() + ", " +
        //    "# False: " + bits.NumFalse() + ", " +
        //    "AndAll: " + bits.AndAll() + ", " +
        //    "OrAll: " + bits.OrAll() +
        //    Environment.NewLine;
        //    txt += bits.ToString("T", " ") + Environment.NewLine;
        //    txt += bits.ToString("X", ".") + Environment.NewLine;
        //    txt += bits.ToString("1", "0", "", 8, " ") + Environment.NewLine;
        //    txt += bits.ToString("TRUE", "false", " ");
        //    txtBits.Text = txt;
        //    txtBits.Select(0, 0);
        //}

        //    BitArray LayerNodeArray1 = new BitArray(32,false);
        //    BitArray LayerNodeArray2 = new BitArray(32,false);
        //    BitArray LayerNodeArray3 = new BitArray(32,false);
        //    BitArray LayerNodeArray4 = new BitArray(32,false);
        //    BitArray LayerNodeArray5 = new BitArray(32,false);
        //    BitArray LayerNodeArray6 = new BitArray(32,false);
        //    BitArray LayerNodeArray7 = new BitArray(32,false);

        //    IPSavedHeaderPackets IipSavedHeaderPackets = new IPSavedHeaderPackets();

        //}

        #region
        // Going to Parse the treeView IP Headers
        //private void //Form1_Load(object sender, EventArgs e) {
        //    // Make a BitArray holding IsSquare Xor IsFibonacci.
        //    BitArray bits = new BitArray(32);
        //    for (int i = 0; i < bits.Length; i++)
        //        bits[i] = (IsSquare(i) ^ IsFibonacci(i));

        //    // Display the result in various ways.
        //    string txt = "";
        //    txt += "# True: " + bits.NumTrue() + ", " +
        //        "# False: " + bits.NumFalse() + ", " +
        //        "AndAll: " + bits.AndAll() + ", " +
        //        "OrAll: " + bits.OrAll() +
        //        Environment.NewLine;
        //    txt += bits.ToString("T", " ") + Environment.NewLine;
        //    txt += bits.ToString("X", ".") + Environment.NewLine;
        //    txt += bits.ToString("1", "0", "", 8, " ") + Environment.NewLine;
        //    txt += bits.ToString("TRUE", "false", " ");
        //    txtBits.Text = txt;
        //    txtBits.Select(0, 0);
        //}
        #endregion

        #region old code
        //    int intLayer = 1;
        //    intColumn = 0;
        //    switch (intLayer) {
        //        case 1:
        //            try {
        //                for (int i = 0; i < 32; i++) {
        //                    intRow = 0;
        //                    if (layer1IPpacket[i]) {
        //                        intColumn = intColumn + 1;
        //                        if (e.Column == intColumn && e.Row == intRow) {
        //                            Graphics g = e.Graphics;
        //                            Rectangle r = e.CellBounds;
        //                            g.FillRectangle(new SolidBrush(Color.Black), r);
        //                        }
        //                    }
        //                    if (!layer1IPpacket[i]) {
        //                        intColumn = intColumn;
        //                        if (e.Column == intColumn && e.Row == intRow) {
        //                            Graphics g = e.Graphics;
        //                            Rectangle r = e.CellBounds;
        //                            g.FillRectangle(new SolidBrush(Color.Black), r);
        //                        }
        //                    }
        //                }
        //            } catch (Exception ex) {
        //                DialogResult msgboxTableLayout = new DialogResult();
        //                msgboxTableLayout = MessageBox.Show(ex.Message, "tableLayoutPanel1_CellPaint", MessageBoxButtons.AbortRetryIgnore);
        //                if (msgboxTableLayout == DialogResult.Abort) {
        //                    Application.Exit();
        //                    break;
        //                }
        //            }
        //            goto case 2;
        //        case 2:
        //            try {
        //                for (int i = 0; i < 32; i++) {
        //                    intRow = 1;
        //                    if (layer1IPpacket[i]) {
        //                        intColumn = intColumn + 1;
        //                        if (e.Column == intColumn && e.Row == intRow) {
        //                            Graphics g = e.Graphics;
        //                            Rectangle r = e.CellBounds;
        //                            g.FillRectangle(new SolidBrush(Color.Black), r);
        //                        }
        //                    }
        //                    if (!layer1IPpacket[i]) {
        //                        intColumn = intColumn;
        //                        if (e.Column == intColumn && e.Row == intRow) {
        //                            Graphics g = e.Graphics;
        //                            Rectangle r = e.CellBounds;
        //                            g.FillRectangle(new SolidBrush(Color.Black), r);
        //                        }
        //                    }
        //                }
        //            } catch (Exception ex) {
        //                DialogResult msgboxTableLayout = new DialogResult();
        //                msgboxTableLayout = MessageBox.Show(ex.Message, "tableLayoutPanel1_CellPaint", MessageBoxButtons.AbortRetryIgnore);
        //                if (msgboxTableLayout == DialogResult.Abort) {
        //                    Application.Exit();
        //                    break;
        //                }
        //            }
        //            goto case 3;
        //        case 3:
        //            try {
        //                for (int i = 0; i < 32; i++) {
        //                    intRow = 2;
        //                    if (layer1IPpacket[i]) {
        //                        intColumn = intColumn + 1;
        //                        if (e.Column == intColumn && e.Row == intRow) {
        //                            Graphics g = e.Graphics;
        //                            Rectangle r = e.CellBounds;
        //                            g.FillRectangle(new SolidBrush(Color.Black), r);
        //                        }
        //                    }
        //                    if (!layer1IPpacket[i]) {
        //                        intColumn = intColumn;
        //                        if (e.Column == intColumn && e.Row == intRow) {
        //                            Graphics g = e.Graphics;
        //                            Rectangle r = e.CellBounds;
        //                            g.FillRectangle(new SolidBrush(Color.Black), r);
        //                        }
        //                    }
        //                }
        //            } catch (Exception ex) {
        //                DialogResult msgboxTableLayout = new DialogResult();
        //                msgboxTableLayout = MessageBox.Show(ex.Message, "tableLayoutPanel1_CellPaint", MessageBoxButtons.AbortRetryIgnore);
        //                if (msgboxTableLayout == DialogResult.Abort) {
        //                    Application.Exit();
        //                    break;
        //                }
        //            }
        //            goto case 4;
        //        case 4:
        //            try {
        //                for (int i = 0; i < 32; i++) {
        //                    intRow = 3;
        //                    if (layer1IPpacket[i]) {
        //                        intColumn = intColumn + 1;
        //                        if (e.Column == intColumn && e.Row == intRow) {
        //                            Graphics g = e.Graphics;
        //                            Rectangle r = e.CellBounds;
        //                            g.FillRectangle(new SolidBrush(Color.Black), r);
        //                        }
        //                    }
        //                    if (!layer1IPpacket[i]) {
        //                        intColumn = intColumn;
        //                        if (e.Column == intColumn && e.Row == intRow) {
        //                            Graphics g = e.Graphics;
        //                            Rectangle r = e.CellBounds;
        //                            g.FillRectangle(new SolidBrush(Color.Black), r);
        //                        }
        //                    }
        //                }
        //            } catch (Exception ex) {
        //                DialogResult msgboxTableLayout = new DialogResult();
        //                msgboxTableLayout = MessageBox.Show(ex.Message, "tableLayoutPanel1_CellPaint", MessageBoxButtons.AbortRetryIgnore);
        //                if (msgboxTableLayout == DialogResult.Abort) {
        //                    Application.Exit();
        //                    break;
        //                }
        //            }
        //            goto case 5;
        //        case 5:
        //            try {
        //                for (int i = 0; i < 32; i++) {
        //                    intRow = 4;
        //                    if (layer1IPpacket[i]) {
        //                        intColumn = intColumn + 1;
        //                        if (e.Column == intColumn && e.Row == intRow) {
        //                            Graphics g = e.Graphics;
        //                            Rectangle r = e.CellBounds;
        //                            g.FillRectangle(new SolidBrush(Color.Black), r);
        //                        }
        //                    }
        //                    if (!layer1IPpacket[i]) {
        //                        intColumn = intColumn;
        //                        if (e.Column == intColumn && e.Row == intRow) {
        //                            Graphics g = e.Graphics;
        //                            Rectangle r = e.CellBounds;
        //                            g.FillRectangle(new SolidBrush(Color.Black), r);
        //                        }
        //                    }
        //                }
        //            } catch (Exception ex) {
        //                DialogResult msgboxTableLayout = new DialogResult();
        //                msgboxTableLayout = MessageBox.Show(ex.Message, "tableLayoutPanel1_CellPaint", MessageBoxButtons.AbortRetryIgnore);
        //                if (msgboxTableLayout == DialogResult.Abort) {
        //                    Application.Exit();
        //                    break;
        //                }
        //            }
        //            goto case 6;
        //        case 6:
        //            try {
        //                for (int i = 0; i < 32; i++) {
        //                    intRow = 5;
        //                    if (layer1IPpacket[i]) {
        //                        intColumn = intColumn + 1;
        //                        if (e.Column == intColumn && e.Row == intRow) {
        //                            Graphics g = e.Graphics;
        //                            Rectangle r = e.CellBounds;
        //                            g.FillRectangle(new SolidBrush(Color.Black), r);
        //                        }
        //                    }
        //                    if (!layer1IPpacket[i]) {
        //                        intColumn = intColumn;
        //                        if (e.Column == intColumn && e.Row == intRow) {
        //                            Graphics g = e.Graphics;
        //                            Rectangle r = e.CellBounds;
        //                            g.FillRectangle(new SolidBrush(Color.Black), r);
        //                        }
        //                    }
        //                }
        //            } catch (Exception ex) {
        //                DialogResult msgboxTableLayout = new DialogResult();
        //                msgboxTableLayout = MessageBox.Show(ex.Message, "tableLayoutPanel1_CellPaint", MessageBoxButtons.AbortRetryIgnore);
        //                if (msgboxTableLayout == DialogResult.Abort) {
        //                    Application.Exit();
        //                    break;
        //                }
        //            }
        //            goto case 7;
        //        case 7:
        //            break;
        //}
        #endregion

        #region old code
        //DialogResult msgboxButton = new DialogResult();
        //msgboxButton = MessageBox.Show("button " + intCapturedPacketCount, 
        //    Program.CapturedPackets.Count().ToString(),
        //    MessageBoxButtons.AbortRetryIgnore);
        //if (msgboxButton == DialogResult.Abort) {
        //    Application.Exit();
        //}

        //IPHeader IipHeader = Program.CapturedPackets[intCapturedPacketCount];
        //BitArray bitIipHeaderV = new BitArray(IipHeader.Buffer);

        //for (int i = 0; i < 32; i++) {
        //    if (bitIipHeaderV[i]) {
        //        layer1IPpacket[i] = true;
        //    }
        //}
        //for (int i = 32; i < 64; i++) {
        //    if (bitIipHeaderV[i]) {
        //        layer1IPpacket[i] = true;
        //    }
        //}
        //for (int i = 64; i < 96; i++) {
        //    if (bitIipHeaderV[i]) {
        //        layer1IPpacket[i] = true;
        //    }
        //}
        //for (int i = 96; i < 128; i++) {
        //    if (bitIipHeaderV[i]) {
        //        layer1IPpacket[i] = true;
        //    }
        //}
        //for (int i = 128; i < 160; i++) {
        //    if (bitIipHeaderV[i]) {
        //        layer1IPpacket[i] = true;
        //    }
        //}
        //for (int i = 160; i < 192; i++) {
        //    if (bitIipHeaderV[i]) {
        //        layer1IPpacket[i] = true;
        //    }
        //}
        //for (int i = 192; i < 224; i++) {
        //    if (bitIipHeaderV[i]) {
        //        layer1IPpacket[i] = true;
        //    }
        //}
        //for (int i = 224; i < 256; i++) {
        //    if (bitIipHeaderV[i]) {
        //        layer1IPpacket[i] = true;
        //    }
        //}
        #endregion
