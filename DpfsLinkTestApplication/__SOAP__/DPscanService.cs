using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Web.Services;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Xml.Serialization;
using System.Threading;
using System.Collections.Specialized;
using System.Threading.Tasks;
using DpfsLinkTestApplication.__Monster__;
using DpfsLinkTestApplication.__SOAP__;
using DpfsLinkTestApplication.__Console__;
using DpfsLinkTestApplication.com.dpro.www;
using System.Collections;
using System.Windows.Forms;

namespace DpfsLinkTestApplication.__SOAP__ {

    ///-------------------------------------------------------------------------------------------------
    /// <summary>
    /// A pscan service.
    /// </summary>
    ///
    /// <remarks>
    /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
    /// </remarks>
    ///
    /// <seealso cref="T:DpfsLinkTestApplication.com.dpro.www.DPScanService"/>
    ///-------------------------------------------------------------------------------------------------

    public partial class DPscanService : DPScanService {

        #region SendKeys Commands

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// For Keyboard Commands Send VIA SendKeys and SEND and SENDWAIT in .NET. Special Characters
        /// Precede Key Codes Below.
        /// .NET Key Press Entrys using Special Characters are encapsulated by parentheses.
        /// For example, "SHIFT &amp;&amp; 'E' &amp;&amp; 'R' == '+(ER)'.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///-------------------------------------------------------------------------------------------------

        public static void KeyCommands() {                                                                          
            IDictionary<string, string> SENDKEYSSPECIAL = new Dictionary<string, string>();                         // SAME IDICTIONARY FOR SPECIAL KEYS
            // Use these with KEYPRESS actions which require more than one key.
            #region Special Character                                                                               
            SENDKEYSSPECIAL["SHIFT"] = "+";
            SENDKEYSSPECIAL["CTRL"] = "^";
            SENDKEYSSPECIAL["ALT"] = "%";
            #endregion      
            IDictionary<string, string> SENDKEYS = new Dictionary<string, string>();                                // ENUM (ACTUALLY IDICTIONARY FOR KEYBOARD)
            // These are the standard .NET string value of SENDKEYS for most keyboards.
            SENDKEYS["BACKSPACE"] = "{BACKSPACE}";
            SENDKEYS["TAB"] = "{TAB}";
            SENDKEYS["CAPSLOCK"] = "{CAPSLOCK}";
            SENDKEYS["DEL"] = "{DEL}";
            SENDKEYS["BREAK"] = "{BREAK}";
            SENDKEYS["DELETE"] = "{DELETE}";
            SENDKEYS["END"] = "{END}";
            SENDKEYS["ESC"] = "{ESC}";
            SENDKEYS["HELP"] = "{HELP}";
            SENDKEYS["HOME"] = "{HOME}";
            SENDKEYS["INS"] = "{INS}";
            SENDKEYS["INSERT"] = "{INSERT}";
            SENDKEYS["SCROLLLOCK"] = "{SCROLLLOCK}";
            SENDKEYS["NUMLOCK"] = "{NUMLOCK}";
            SENDKEYS["PAGEDOWN"] = "{PGDN}";
            SENDKEYS["PAGEUP"] = "{PGUP}";
            SENDKEYS["PRINTSCREEN"] = "{PRTSC}";
            SENDKEYS["UP"] = "{UP}";
            SENDKEYS["DOWN"] = "{DOWN}";
            SENDKEYS["LEFT"] = "{LEFT}";
            SENDKEYS["RIGHT"] = "{RIGHT}";
            SENDKEYS["ENTER"] = "{ENTER}";
            SENDKEYS["F1"] = "{F1}";
            SENDKEYS["F2"] = "{F2}";
            SENDKEYS["F3"] = "{F3}";
            SENDKEYS["F4"] = "{F4}";
            SENDKEYS["F5"] = "{F5}";
            SENDKEYS["F6"] = "{F6}";
            SENDKEYS["F7"] = "{F7}";
            SENDKEYS["F8"] = "{F8}";
            SENDKEYS["F9"] = "{F9}";
            SENDKEYS["F10"] = "{F10}";
            SENDKEYS["F11"] = "{F11}";
            SENDKEYS["F12"] = "{F12}";
            SENDKEYS["F13"] = "{F13}";
            SENDKEYS["F14"] = "{F14}";
            SENDKEYS["F15"] = "{F15}";
            SENDKEYS["F16"] = "{F16}";
            SENDKEYS["ADD"] = "{ADD}";
            SENDKEYS["SUBTRACT"] = "{SUBTRACT}";
            SENDKEYS["MULTIPLY"] = "{MULTIPLY}";
            SENDKEYS["DIVIDE"] = "{DIVIDE}";
        }
            #endregion                                                                  

        #region DPscanService Class Properties

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the que dp scan initialize.
        /// </summary>
        ///
        /// <value>
        /// The que dp scan initialize.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static Queue queDPScanInitialize { get; set; }                                   // Queue for UI Updates via Text (WinForm Initialization Process)

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the que text.
        /// </summary>
        ///
        /// <value>
        /// The que text.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static Queue queText { get; set; }                                               // Queue for UI Updates via Text

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        ///
        /// <value>
        /// The string password.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static string strPassword { get; set; }                                          // Basic Authentication Level Password

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        ///
        /// <value>
        /// The string username.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static string strUsername { get; set; }                                          // Username for Session

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets a value indicating whether the account access.
        /// </summary>
        ///
        /// <value>
        /// true if account access, false if not.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static bool boolAccountAccess { get; set; }                                      // User Authentication Certification

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the int u icontinue.
        /// </summary>
        ///
        /// <value>
        /// The int u icontinue.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static int intUIcontinue { get; set; }                                           // Generic Counter for User Session Instances

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the URI dp scan a smx.
        /// </summary>
        ///
        /// <value>
        /// The URI dp scan a smx.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static Uri uriDPScanASMX { get; set; }                                           // URI For DPSCANSERVICE ASMX

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the dp scan a smx.
        /// </summary>
        ///
        /// <value>
        /// The string dp scan a smx.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static string strDPScanASMX { get; set; }                                        // HTTP Address for DPSCANSERVICES VIA ASMX as string

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the URI dp scan wsdl.
        /// </summary>
        ///
        /// <value>
        /// The URI dp scan wsdl.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static Uri uriDPScanWSDL { get; set; }                                           // URI For DPSCANSERVICE WSDL

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the dp scan wsdl.
        /// </summary>
        ///
        /// <value>
        /// The string dp scan wsdl.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static string strDPScanWSDL { get; set; }                                        // HTTP Address for DPSCANSERVICES VIA WSDL as string

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets a value indicating whether the dp scan service open.
        /// </summary>
        ///
        /// <value>
        /// true if dp scan service open, false if not.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static bool boolDPScanServiceOpen { get; set; }                                  // Current User Session has Authentiated with CountAccess at least once

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the make queue dp scan.
        /// </summary>
        ///
        /// <value>
        /// The make queue dp scan.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        private static Queue MakeQueueDPScan { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the list array dp services.
        /// </summary>
        ///
        /// <value>
        /// The list array dp services.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static ArrayList listArrayDPServices { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the xmldp scan wsdl.
        /// </summary>
        ///
        /// <value>
        /// The xmldp scan wsdl.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static XmlDocument XMLDPScanWSDL { get; set; }
        #endregion

        #region DPscanService Class Fields
        //public static Uri uriDPScanASMX = new Uri("http://www.dpro.com/dpscanservice/dpscan.asmx");
        //public static Uri uriDPScanWSDL = new Uri("http://www.dpro.com/dpscanservice/dpscan.asmx?WSDL");

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// DialogBox result for use later on.
        /// </summary>
        ///-------------------------------------------------------------------------------------------------

        public static DialogResult uiDRresult = new DialogResult();

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// No Idea.
        /// </summary>
        ///-------------------------------------------------------------------------------------------------

        public static string[] strText = new string[3];

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Instantiated DPSCANSERVICE.
        /// </summary>
        ///-------------------------------------------------------------------------------------------------

        public static DPScanService clientDPScan = new DPScanService();

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Instantiated DPSCANSERVICE COUNTACCESS Properties.
        /// </summary>
        ///-------------------------------------------------------------------------------------------------

        public static AccessRequest aRequest = new AccessRequest();

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Instantiated DPSCANSERVICE COUNTACCESS Properties.
        /// </summary>
        ///-------------------------------------------------------------------------------------------------

        public static AccessResponse aResponse = new AccessResponse();
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Main Entry point for DPScanService Form.
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

        public static void Main(object sender, EventArgs e) {
            SelectDPScanService selectDPScanService = new SelectDPScanService();
            if (!boolDPScanServiceOpen) {
                selectDPScanService.Show();
                boolDPScanServiceOpen = true;
            }
            DPScanService _dpscanservice = new DPScanService();
            strDPScanASMX = "http://www.dpro.com/dpscanservice/dpscan.asmx";
            strDPScanWSDL = "http://www.dpro.com/dpscanservice/dpscan.asmx?WSDL";
            if (strDPScanASMX != null) {
                // Can grab Uri Properties here or later
                uriDPScanASMX = new Uri(strDPScanASMX);
                _dpscanservice.Url = strDPScanASMX;
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Function that returns a Queue of Text for the UI. Made to be used with DPSCANSERVICE to
        /// Display to User helpful information.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="args">
        ///     .
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        public static void makeQueueDPScan(string args) {
            if (MakeQueueDPScan != null) {
                MakeQueueDPScan.Enqueue(args);
            } else {
                Queue _makeQueueDPScan = new Queue();
                _makeQueueDPScan.Enqueue(args);
                MakeQueueDPScan = _makeQueueDPScan;
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Simple call to check DPSCAN Access.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="dpscans">
        ///     The dpscans.
        /// </param>
        ///
        /// <returns>
        /// true if it succeeds, false if it fails.
        /// </returns>
        ///-------------------------------------------------------------------------------------------------

        public static bool AccessDPScan(DPScanService dpscans) {
            if ((strUsername != null) && (strPassword != null)) {
                makeQueueDPScan("Checking for Username and Password");
                aRequest.Username = strUsername;
                makeQueueDPScan("Found : Username [" + strUsername + " ]");
                aRequest.Password = strPassword;
                makeQueueDPScan("Found : Password [" + strPassword + " ]");
                try {
                    makeQueueDPScan("Attempting to connect...");
                    aResponse = clientDPScan.CountAccess(aRequest);
                    if (aResponse.Success) {
                        makeQueueDPScan("Connection : [ SUCCESSFUL ] " + System.DateTime.Now.TimeOfDay.ToString());
                        //MessageBox.Show(aResponse.Success.ToString(), aResponse.ToString(), MessageBoxButtons.OK);
                        return true;
                    } else {
                        makeQueueDPScan("Connection : [ FAILED ] " + System.DateTime.Now.TimeOfDay.ToString());
                    }
                } catch (Exception ex) {
                    if ((ex.ToString().Contains("Unhandled"))) {
                        MessageBox.Show("Internet Connection : Disconnected", "DPScanService : NETWORK ERROR", MessageBoxButtons.OK);
                    } else {
                        MessageBox.Show("Exception : " + ex.Message, "DPScanService", MessageBoxButtons.OK);
                    }
                    makeQueueDPScan("Exception Error : " + ex.Message + " [ " + System.DateTime.Now.TimeOfDay.ToString() + " ]");
                    return false;
                }
                return true;
            } else {
                //DoIndependentWork(clientDPScan);
                return false;
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets the components from a URI.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="uri">
        ///     System.Uri - Uri to Parse.
        /// </param>
        /// <param name="components">
        ///     System.UriComponents - The UriComponents to retrieve from uri.
        /// </param>
        /// <param name="format">
        ///     System.UriFormat - One of the UriFormat values that controls how special characters are
        ///     escaped.
        /// </param>
        ///
        /// <returns>
        /// A string that contains the components.
        /// </returns>
        ///-------------------------------------------------------------------------------------------------

        protected virtual string GetComponents(Uri uri,UriComponents components, UriFormat format) {
            // Use the GetComponents method to determine the value of various parts of the URI, such as the Scheme, Host, or Port.
            // The componens are returned in the order that they appear in the URI. For example, if Scheme is specified, it appears first.
            string strCompenents = "";
            return strCompenents;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Called by Uri constructors and Uri.TryCreate() to resolve a relative URI.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="baseUri">
        ///     System.Uri.
        /// </param>
        /// <param name="relativeUri">
        ///     System.Uri.
        /// </param>
        /// <param name="parsingError">
        ///     System.UriFormatException.
        /// </param>
        ///
        /// <returns>
        /// The string of the resolved relative Uri.
        /// </returns>
        ///-------------------------------------------------------------------------------------------------

        protected virtual string Resolve(Uri baseUri, Uri relativeUri, out UriFormatException parsingError) {
            // Uri constructors and Uri.TryCreate() use Resolve to construct a URI from baseUri and relativeUri.
            // If a parsing error occurs, the returned string for the resolved relative Uri is null. 
            string strResolvedUri = "";
            parsingError = new UriFormatException();
            try {
                if (relativeUri != null) {
                    // Resolving was a success
                    strResolvedUri =  baseUri.OriginalString;
                } else {
                // Resolving failed
                    //__DEBUG__.DebugForm.
                    return parsingError.Message;
                }
            } catch (UriFormatException ex) {
                MessageBox.Show("Error : " + ex.Message,"Resolving Uri",MessageBoxButtons.OK);
            }
            return strResolvedUri;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Associates a scheme and port number with a UriParser.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="uriParser">
        ///     System.UriParser.
        /// </param>
        /// <param name="schemeName">
        ///     System.String.
        /// </param>
        /// <param name="defaultPort">
        ///     System.Int32.
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        public static void Register(UriParser uriParser, string schemeName, int defaultPort) {
            // If the defaultPort parameter is set to -1, 
            // the Register method registers no default value for the port number.
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Initialize the state of the parser and validate the URI.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="uri">
        ///     The T:System.Uri to validate.
        /// </param>
        /// <param name="parsingError">
        ///     Validation errors, if any.
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        protected virtual void InitializeAndValidate(Uri uri, out UriFormatException parsingError) {
            // The InitializeAndValidate method is called every time a Uri is instantiated
            parsingError = new UriFormatException();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Simple DPSCAN Access Check Event Handler.
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
        ///     CountAccessCompletedEventArgs.
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void clientDPScan_CountAccessCompleted(object sender, CountAccessCompletedEventArgs e) {
            try {
                if (e.Result.Success.ToString() != null) {
                    bool boolAccountAccess = e.Result.Success;
                    if (boolAccountAccess) {
                        uiDRresult = MessageBox.Show(
                            "DPScan Connection Success! : CountAccessAsync /r/n" + e.UserState.ToString() + "", "Incoming Message...For:  " + strUsername);
                        if (uiDRresult == DialogResult.OK) {
                            DoIndependentWork(sender);
                        }
                    }
                }
                if (e.Result.ErrorCode != null) {
                    MessageBox.Show("clientDPScan ErrorCode" + e.Result.ErrorCode, "CountAccess Mistakes Were Made");
                }
                if (e.Result.ErrorDesc != null) {
                    MessageBox.Show("clientDPScan ErrorDesc" + e.Result.ErrorDesc, "CountAccess Mistakes Were Made");
                }
            } catch (Exception ex) {
                if ((ex.Message == null) && (ex.StackTrace == null)) {
                    MessageBox.Show(ex.Message, "clientDPScan Exception - ex.Message");
                    MessageBox.Show(ex.StackTrace, "clientDPScan Exception - ex.StackTrace");
                }
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Determines whether baseUri is a base URI for relativeUri.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="baseUri">
        ///     The base URI.
        /// </param>
        /// <param name="relativeUri">
        ///     The URI to test.
        /// </param>
        ///
        /// <returns>
        /// true if baseUri is a base URI for relativeUri; otherwise, false.
        /// </returns>
        ///-------------------------------------------------------------------------------------------------

        protected virtual bool IsBaseOf(Uri baseUri, Uri relativeUri) {
            bool boolIsBase = false;
            bool boolIsBaseAb = false;
            string strNewUri = "";
            string strRelUri = "";
            string strBasUri = "";
            try {
                Uri uri = new Uri(baseUri, strNewUri);
                //
                // Construct the AbsoluteUri First
                strRelUri = relativeUri.AbsoluteUri;
                strBasUri = baseUri.AbsoluteUri;
                boolIsBaseAb = Equals(strBasUri, strRelUri);
                //
                // Check Host
                strRelUri = relativeUri.DnsSafeHost;
                strBasUri = baseUri.DnsSafeHost;
                boolIsBase = Equals(strRelUri, strBasUri);
            } catch (Exception ex) {
                MessageBox.Show("Message : " + ex.Message, "IsBaseOf Uri Error", MessageBoxButtons.OK);
            }
            return Equals(boolIsBase, boolIsBaseAb);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Spare method to do threadsafe work. Not being used.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <param name="sender">
        ///     .
        /// </param>
        ///-------------------------------------------------------------------------------------------------

        private void DoIndependentWork(object sender) {
            //throw new NotImplementedException();
        }


    }
}
