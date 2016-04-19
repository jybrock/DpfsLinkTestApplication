using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DpfsLinkTestApplication {
    public partial class AddMessage : Form {
        public static bool _boolSorC { get; set; }
        public static string strClientMessage { get; set; }
        public static string strServerMessage { get; set; }
        private static int _count { get; set; }
        public static IDictionary<string, string> Cmsg = new Dictionary<string, string>();
        public static IDictionary<string, string> Smsg = new Dictionary<string, string>();
        public static string strMtUp { get; set; }


        /// <summary>
        /// This is the main entry point of the Message form.
        /// </summary>
        public AddMessage() {
            InitializeMsgs();
            InitializeComponent();
        }

        public static void InitializeMsgs() {
            _boolSorC = MainForm.boolSorC;
            if (_boolSorC) {                                                                // Server is True
                strServerMessage = "";
                IDictionary<string, string> setSmsg = new Dictionary<string, string>();
                Smsg = setSmsg;
            } else {
                strClientMessage = "";
                IDictionary<string, string> setCmsg = new Dictionary<string, string>();
                Cmsg = setCmsg;
            }
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle {
            get {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0) {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "") {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion {
            get {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription {
            get {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0) {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct {
            get {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0) {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright {
            get {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0) {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany {
            get {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0) {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion


        /// <summary>
        /// Saves default messages and values from the main message Dictionary.
        /// Either the Client or Server is loaded depending on how this form was loaded.
        /// Saved messages are discarded upon application restarts.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMessageSave_Click(object sender, EventArgs e) {
            strMtUp = "";
            int i = 0;
            string strMessageType = "";
            if (clbxMsgTypeSelect.SelectedItem == null) {
                strMessageType = "raw";
                strMtUp = "IPv4";
            } else {
                strClientMessage = rtbxMessage.Text;
                strMessageType = clbxMsgTypeSelect.SelectedItem.ToString();
                strMessageType = strMessageType.ToLower();
                strMtUp = strMessageType;
            }
            DialogResult SaveChoice = new DialogResult();
            switch (i) {
                case 0:
                    if ((rtbxMessage.Text != null) &&
                         (Program.ClientMessage.ContainsKey(strMessageType))) {
                        goto case 5;
                    } else {
                        int tl = rtbxMessage.TextLength;
                        MessageBox.Show("Message Length : [" + tl + "]", "What should I save?");
                    }
                    goto default;
                case 5:
                    SaveChoice = MessageBox.Show("Save as [" + strMtUp + "] message payload?", "Protocol Header Message", MessageBoxButtons.YesNo);
                    if (SaveChoice == DialogResult.Yes) {
                        goto case 10;
                    }
                    if (SaveChoice == DialogResult.No) {
                        clbxMsgTypeSelect.ClearSelected();
                        goto default;
                    }
                    break;
                case 10:
                    if (_boolSorC == false) {
                        goto case 68;
                    }
                    if (_boolSorC) {
                        goto case 79;
                    }
                    break;
                case 68:                // Client
                    strClientMessage = rtbxMessage.Text;
                    Program.ClientMessage[strMessageType] = strClientMessage;
                    break;
                case 79:                // Server
                    strServerMessage = rtbxMessage.Text;
                    // Add ServerMessage
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// Loads default messages and values saved during this application sessoin from the main message Dictionary.
        /// Either the Client or Server is loaded depending on how this form was loaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadMessage_Click(object sender, EventArgs e) {

            int i = 0;
            string strMessageType = "";
            if (clbxMsgTypeSelect.SelectedItem == null) {
                strMessageType = "raw";
            } else {
                strMessageType = clbxMsgTypeSelect.SelectedItem.ToString();
                strMessageType = strMessageType.ToLower();
            }
            DialogResult LoadChoice = new DialogResult();
            switch (i) {
                case 0:
                    if (rtbxMessage.TextLength != 0) {
                        goto case 10;
                    }
                    if (rtbxMessage.TextLength == 0) {
                        goto default;
                    }
                    break;
                case 10:
                    LoadChoice = MessageBox.Show("Discard Unsaved Work?", "Loading Saved Message", MessageBoxButtons.YesNo);
                    if (LoadChoice == DialogResult.Yes) {
                        strClientMessage = "";
                        rtbxMessage.Text = "";
                        strMessageType = clbxMsgTypeSelect.SelectedItem.ToString();
                        strMessageType = strMessageType.ToLower();
                        goto case 50;
                    }
                    if (LoadChoice == DialogResult.No) {
                        goto default;
                    }
                    break;
                case 50:
                    if (_boolSorC) {                                                                // Server is True
                        // Add Server Message
                        goto default;
                    }
                    if (_boolSorC != true) {
                        rtbxMessage.Text = Program.ClientMessage[strMessageType];
                        goto default;
                    }
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// Event handler for CheckedListBox selection of Protocol Message.
        /// Each Time the message is potentially erased if not saved first.
        /// The main program message Dictionary is updated with each save.  
        /// Default and saved values are loaded from the main message Dictionary as well.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clbxMsgTypeSelect_SelectedIndexChanged(object sender, EventArgs e) {

            _count++;
            string strMessageType = "";
            strMessageType = clbxMsgTypeSelect.SelectedItem.ToString();
            strMessageType = strMessageType.ToLower();
            if (_count > 9)

                switch (_count) {
                    case 10:
                        DialogResult TypeChoice = MessageBox.Show("Discard Unsaved Work?", "Changing Protocol", MessageBoxButtons.YesNo);
                        if (TypeChoice == DialogResult.Yes)
                            goto case 16;
                        if (TypeChoice == DialogResult.No)
                            goto default;
                        break;
                    case 16:
                        _count = 17;
                        if ((_boolSorC == false) && (Program.ClientMessage.ContainsKey(strMessageType))) {
                            goto case 68;
                        }
                        if ((_boolSorC == true) && (Program.ClientMessage.ContainsKey(strMessageType))) {
                            goto case 79;
                        }
                        break;
                    case 13:
                        if (rtbxMessage.Text == null) {
                        }
                        _count = 0;
                        break;
                    case 68:
                        strClientMessage = Program.ClientMessage[strMessageType];
                        rtbxMessage.Text = strClientMessage;
                        Cmsg = Program.ClientMessage;
                        goto case 13;
                    case 79:
                        strServerMessage = Program.ClientMessage[strMessageType];
                        rtbxMessage.Text = strServerMessage;
                        // Add ServerMessage
                        goto case 13;
                    default:
                        _count = 0;
                        break;
                }

        }

        private void btnMessageExit_Click(object sender, EventArgs e) {
            int i = 0;
            switch (i) {
                case 0:
                    if (rtbxMessage.TextLength == 0) {
                        goto case 5;
                    } else {
                        goto case 10;
                    }
                case 5:
                    goto default;
                case 10:
                    if (_boolSorC != true) {
                        goto case 15;
                    }
                    if (_boolSorC == true) {
                        goto case 20;
                    }
                    break;
                case 15:
                    Queue queueServerM = new Queue();
                    string st = "";
                    strServerMessage = rtbxMessage.Text;
                    Char[] strA = strServerMessage.ToCharArray(0,strServerMessage.Length);
                    for (int intChar = 0; intChar < strServerMessage.Length; intChar += 40) {
                        for (int g = 0; g < 40; g++) {
                            st = strA[intChar].ToString();
                        }
                        queueServerM.Enqueue(st);
                    }
                    MainForm.queueServerSocket = queueServerM;
                    break;
                case 20:
                    strClientMessage = rtbxMessage.Text;
                    break;
                default:
                    break;
            }
            this.Close();
        }
        
        

        private void btnMessageClear_Click(object sender, EventArgs e) {

            rtbxMessage.Clear();
        }


        #region neat switch case in C
        //        int testInt = 0;
        //testInt = ("a" == "b") ? 1 : testInt;
        //testInt = ("a" == "c") ? 2 : testInt;
        //testInt = ("a" == "a") ? 3 : testInt;
        //testInt = ("a" == "d") ? 4 : testInt;
        //string resultString;
        //switch (testInt) {
        //case 1:
        //resultString = "1";
        //break;
        //case 2:
        //resultString = "2";
        //break;
        //case 3:
        //resultString = "3";
        //break;
        //case 4:
        //resultString = "4";
        //break;
        //default:
        //resultString = "";
        //break;
        //}
        #endregion




    }



}



