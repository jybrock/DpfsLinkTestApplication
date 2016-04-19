using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services;
using System.Net.Sockets;
using System.Xml.Serialization;
using System.IO.IsolatedStorage;
using System.Runtime.InteropServices;
using System.Security;
using System.IO.Ports;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;
using System.Collections.ObjectModel;
using System.Collections;
using System.Reflection;
using System.Reflection.Emit;
using DpfsLinkTestApplication;
using System.Collections.Concurrent;

namespace DpfsLinkTestApplication.__Console__ {

    /// <summary>
    /// DPFS, Inc. Console .NET v4.5
    /// This Class "Console" is an inheritable class intended as a resource.
    /// Helps with UI console-like GUI, standard configurations, properties, and Isolated Storage.
    /// ;//
    /// ;// Copyright Data Pro Financial Services, Inc. (C) 2004-2013
    /// ;// Copyright GreenSkyware, Inc, (C) 2013
    /// ;// Author Joey Brock - Saint Petersburg, FL 33701
    /// </summary>
    public partial class __console__ : MarshalByRefObject {


        public string _tbConsoleTextDPScan { get; set; }
        public string _strConsoleText(object sender, bool uiWaiting) {
            string str;
            if ((strConsoleText != null) && (sender != null)) {
                if (strConsoleText != "") {
                    str = strConsoleText;
                } else {
                    str = "";
                }
            } else {
                strConsoleText = sender.ToString();
            }
            str = strConsoleText;

            return str;
        }

        #region Console Properties
        public static TextBox tb8 { get; set; }
        public static string strConsoleText { get; set; }
        private static Queue queueCommands { get; set; }
        private static Queue queueParams { get; set; }
        public static string strWriteLine { get; set; }
        public static string[] _settings { get; set; }
        private static string strItem { get; set; }
        private static string strUsername { get; set; }
        private static string strPassword { get; set; }
        public static bool IsRememberMe { get; set; }
        public static bool IsLogin { get; set; }
        public static bool IsDemoMode { get; set; }
        public static int intQuantity { get; set; }
        public static string strQuantity { get; set; }
        #endregion

        #region Console Fields
        public static Uri Url = new Uri("http://www.dpro.com/dpscanservice/dpscan.aspx");
        private static byte[] byteData = new byte[4096];
        #endregion

        #region Isolated Storage
        public static IDictionary<string, object> globalSettings { get; set; }
        public static IsolatedStorageFile globalSettingsFile { get; set; }
        public static IsolatedStorageScope globalSettingsScope { get; set; }
        public static DataObject[] clipboardFromUser { get; set; }
        #endregion

        #region Run-Time Object Helpers
        #endregion

        #region TIMERS & CLIENT SIDE CONSOLE UI
        public static Stopwatch stopwatch { get; set; }
        public static bool boolWaitingforUI { get; set; }
        public static bool boolReceivedUI { get; set; }
        public static int intWaitingforUITime { get; set; }
        public static string[] strUIQuestions { get; set; }
        public static int[] intUIQuestions { get; set; }
        public static string[] strConsoleCommands { get; set; }
        public static bool[] boolconsoleUI { get; set; }
        public static int intLoopsWaitingforUI { get; set; }
        public static int intTimeOut { get; set; }
        public static string strTimeOut { get; set; }
        #endregion

        #region Networking Settings and Configuration
        // Initialise the Changes list.
        public static ObservableCollection<string> Changes { get; set; }
        public static ObservableCollection<Socket> CollectedSockets { get; set; }
        public static ObservableCollection<string> NetworkInterfaces { get; set; }
        public static Socket socketAlpha { get; set; }


        //// Bind the ListBox to the Changes list
        //lbNetworkChanges.DataContext = Changes;
        //lbNetworkInterfaces.DataContext = NetworkInterfaces;
        //// Subscribe to the NetworkAvailabilityChanged event
        //DeviceNetworkInformation.NetworkAvailabilityChanged += new EventHandler<NetworkNotificationEventArgs>(ChangeDetected);
        #endregion


        #region Clipboard User && Console
        public static DataObject ClipFromConsole_obj = new DataObject();
        public static DataObject ClipToConsole_obj = new DataObject();
        public static ObservableCollection<object> CollObsObj = new ObservableCollection<object>();
        public static Dictionary<string, object> DictClipboard = new Dictionary<string, object>();
        public static ArrayList ArrayListDebug = new ArrayList();
        #endregion

        #region Standard/Generic Soap Envelope
        public static string _soapEnvelope =
                        @"<soap:Envelope
                    xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'
                    xmlns:xsd='http://www.w3.org/2001/XMLSchema'
                    xmlns:soap='http://schemas.xmlsoap.org/soap/envelope/'>
                <soap:Body></soap:Body></soap:Envelope>";
        #endregion

        #region DPScanService Specifics
        public static UriParser uriParser { get; set; }
        #endregion


        /// <summary>
        /// Initializes the Class.
        /// </summary>
        private static void main() {
            IsolatedStorageScope _globalSettingsScope = new IsolatedStorageScope();
            globalSettingsScope = _globalSettingsScope;
            //AppDomain domain = AppDomain.CurrentDomain;
            //AssemblyName aName = new AssemblyName("DynamicEnums");
            //AssemblyBuilder ab = domain.DefineDynamicAssembly(aName, AssemblyBuilderAccess.Save);
            //ModuleBuilder mb = ab.DefineDynamicModule(aName.Name, aName.Name + ".dll");
            ////ConstructorInfo referenceObjectConstructor = typeof(ReferenceObject).GetConstructor(new[] { typeof(int) });
            //List<Type> types = new List<Type>();
            //foreach (ReferenceType rt in GetTypes()) {
            //    TypeBuilder tb = mb.DefineType(rt.Name, TypeAttributes.Public | TypeAttributes.BeforeFieldInit);
            //    ConstructorBuilder staticConstructorBuilder = tb.DefineConstructor(MethodAttributes.Public | MethodAttributes.Static, CallingConventions.Standard, Type.EmptyTypes);
            //    ILGenerator staticConstructorILGenerator = staticConstructorBuilder.GetILGenerator();
            //    foreach (Reference r in GetReferences(rt.ID)) {
            //        name = NameFix(r.Name);
            //        // Create a public, static, readonly field to store the
            //        // named ReferenceObject.
            //        FieldBuilder referenceObjectField = tb.DefineField(name, typeof(ReferenceObject), FieldAttributes.Static | FieldAttributes.Public | FieldAttributes.InitOnly);
            //        // Add code to the static constructor to populate the
            //        // ReferenceObject field:
            //        // Load the ReferenceObject's ID value onto the stack as a
            //        // literal 4-byte integer (Int32).
            //        staticConstructorILGenerator.Emit(OpCodes.Ldc_I4, r.ID);
            //        // Create a reference to a new ReferenceObject on the stack
            //        // by calling the ReferenceObject(int32 pValue) reference
            //        // we created earlier.
            //        staticConstructorILGenerator.Emit(OpCodes.Newobj, referenceObjectConstructor);
            //        // Store the ReferenceObject reference to the static
            //        // ReferenceObject field.
            //        staticConstructorILGenerator.Emit(OpCodes.Stsfld, referenceObjectField);
            //    }
            //    staticConstructorILGenerator.Emit(OpCodes.Ret);
            //    types.Add(tb.CreateType());
            //}
            //ab.Save(aName.Name + ".dll");
        }


        /// <summary>
        /// Testing some MemberwiseCloning and MarshalByRefObject.
        /// Not Working.
        /// </summary>
        /// <param name="_console"></param>
        /// <returns></returns>
        private static MarshalByRefObject CreateNewConsole(__console__ _console) {
            _console.MemberwiseClone(true);
            return _console;
        }





        ////public static class MyExtentions
        //public static string ValueString(this Enum e) {
        //   //var castToString = typeof(__Console__.ClientSize).GetMethod(__Console__.Console.CastToString"CastToString");
        //   var ut = Enum.GetUnderlyingType(e.GetType());
        //   var castToString = typeOf(MyExtentions).GetMethod("CastToString");
        //   var gcast = cast.MakeGenericMethod(ut);
        //   var gparams = new object[] {e};
        //   return gcast.Invoke(null, gparams).ToString();
        //}

        //public static string CastToString<T>(object o) {
        //    return ((T)o).ToString();
        //}


        //foreach (var value in (__Console__.ClientSize[]) {
        //    int v = value.GetValue();

        //    value

        //}
        //foreach (Enum enu in __Console__.ClientSize(string)) {
        //}



        /// <summary>
        /// Code to execute when the application is closing (eg, user hit Back).
        /// This code will not execute when the application is deactivated.
        /// Ensure that required application state is persisted here..
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Application_Closing(object sender, EventArgs e) {
            try {
                SaveSettings();
                using (IsolatedStorageFile globalSettingsFile = IsolatedStorageFile.GetUserStoreForApplication()) {
                    using (IsolatedStorageFileStream fileStream = globalSettingsFile.CreateFile("credentials\\settings.txt")) {
                        //Create a new folder and call it "credentials"
                        if (!globalSettingsFile.DirectoryExists("credentials")) { globalSettingsFile.CreateDirectory("credentials"); }
                        //Create a new file and assign a StreamWriter to the store and this new file (login.txt)
                        //Also take the text contents from the txtWrite control and write it to login.txt
                        System.IO.StreamWriter writeFile = new System.IO.StreamWriter(new IsolatedStorageFileStream("credentials\\settings.txt", FileMode.OpenOrCreate, globalSettingsFile));
                        using (StreamWriter writer = new StreamWriter(fileStream)) {
                            string strWriteLine = null;
                            //strWriteLine = LoadSettings();
                            writeFile.WriteLine(strWriteLine);
                            writeFile.Close();
                        }
                    }
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "DPFS, Inc. - Application_Closing", MessageBoxButtons.OK);
            }
        }


        /// <summary>
        /// Provides access to Application Settings.
        /// Saves to Isolated Storage.
        /// </summary>
        void SaveSettings() {
            try {
                if (IsRememberMe != false) {
                    if ((string)globalSettings["strUsername"] != null) {
                        strUsername = (string)globalSettings["strUsername"];
                    }
                    if ((string)globalSettings["strPassword"] != null) {
                        strPassword = (string)globalSettings["strPassword"];
                    }
                    if ((bool)globalSettings["IsRememberMe"]) {
                        IsRememberMe = (bool)globalSettings["RememberMe"];
                    }
                }
                // Obtain the virtual store for application
                using (IsolatedStorageFile globalStorageFile = IsolatedStorageFile.GetUserStoreForApplication()) {
                    //Create a new folder and call it "credentials"
                    if (!globalStorageFile.DirectoryExists("credentials")) { globalStorageFile.CreateDirectory("credentials"); }
                    if (globalStorageFile.FileExists("credentials\\settings.txt")) {
                        StreamWriter writeFile = new StreamWriter(new IsolatedStorageFileStream("credentials\\settings.txt",
                            FileMode.Create, globalStorageFile));
                        if ((strItem != string.Empty) || (strItem != null)) {
                            strItem = strItem;
                        }
                        IDictionary<string, string> someTextData = new Dictionary<string, string>();
                        someTextData["strUsername"] = (string)globalSettings["strUserName"];
                        someTextData["strPassword"] = (string)globalSettings["strPassword"];
                        someTextData["IsRememberMe"] = Convert.ToString((bool)globalSettings["IsRememberMe"]);
                        someTextData["IsLogin"] = Convert.ToString((bool)globalSettings["IsLogin"]);
                        someTextData["strBarCode"] = (string)globalSettings["strBarCode"];
                        someTextData["strQRCode"] = (string)globalSettings["strQRCode"];
                        someTextData["intQuantity"] = Convert.ToString((Int32)globalSettings["intQuantity"]);
                        someTextData["strQuantity"] = (string)globalSettings["strQuantity"];
                        writeFile.WriteLine(someTextData);
                        writeFile.Close();
                    }
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        /// <summary>
        /// Provides a way to save data to Isolated Storage.
        /// Uses XDocument XML.
        /// ;//
        /// ;// FileModes = Append,Create,CreateNew,Open,OpenOrCreate,Truncate
        /// </summary>
        /// <param name="filePath">string format of file location</param>
        /// <param name="fileMode">Specifies how the operating system should open a file.</param>
        /// <param name="xDoc">XDocument used to save XML File</param>
        public static void SaveDataToIsolatedStorage(string filePath, FileMode fileMode, XDocument xDoc) {
            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
            using (IsolatedStorageFileStream location = new IsolatedStorageFileStream(filePath, fileMode, storage)) {
                System.IO.StreamWriter file = new System.IO.StreamWriter(location);
                xDoc.Save(file);
            }
        }
        private static ObservableCollection<string> WhyLoadSettings = new ObservableCollection<string>();
        private static Dictionary<string, bool> WhyUpdateArgs = new Dictionary<string, bool>();
        /// <summary>
        /// Basically a Callback Function for Loading Settings
        /// </summary>
        public static void whyLoadSettings(string[] args) {
            int count = 0;
            ObservableCollection<string> whyAnswer = new ObservableCollection<string>();
            WhyLoadSettings = whyAnswer;
            foreach (string strA in args) {
                WhyLoadSettings[count] = strA;
                count++;
            }
        }
        public static Dictionary<string, bool> whyUpdateArgs = new Dictionary<string, bool>();
        /// <summary>
        /// Ported from WP7 App.
        /// Should be used for Loading Settings from User's Console TextBox.
        /// </summary>
        /// <param name="consoleTextBox">TextBox from Console</param>
        public void LoadSettings(string consoleTextBox, bool uiWaiting) {
            try {
                int intCase = 0;
                // Implement ConcurrentDictionary at some point      
                Dictionary<string, bool> _whyUpdateArgs = new Dictionary<string, bool>();
                Dictionary<string, bool>.ValueCollection U_valueColl = _whyUpdateArgs.Values;
                Dictionary<string, bool>.KeyCollection U_keyColl = _whyUpdateArgs.Keys;
                Dictionary<string, string> _loadSettings = new Dictionary<string, string>();
                Dictionary<string, string>.ValueCollection L_valueColl = _loadSettings.Values;
                Dictionary<string, string>.KeyCollection L_keyColl = _loadSettings.Keys;
                string args = "";
                switch (intCase) {
                    case 0:
                        break;
                    case 1:
                        Queue queConsole = new Queue();
                        foreach (KeyValuePair<string, bool> strIndex in _whyUpdateArgs) {
                            queConsole.Enqueue("// Key = [ " + strIndex.Key + " ], Value = [ " + strIndex.Value + " ]");
                            if (strIndex.Value) {
                                queConsole.Enqueue("// Flag Found! : For [ " + strIndex.Key + " ]");
                                if (strIndex.Key != null) {
                                    args = strIndex.Key;
                                    queConsole.Enqueue("// Loading : [ " + args + " ], and searching for more information...");
                                    if (!_loadSettings.ContainsKey(args)) {
                                        _loadSettings.Add(args, "");
                                    } else {
                                        _loadSettings.Remove(args);
                                        queConsole.Enqueue("// Purging : [ " + args + " ] used data from list of updates");
                                        _loadSettings.Add(args, "");
                                    }
                                    _whyUpdateArgs.Remove(strIndex.Key);
                                }
                            }
                        }
                        args = "";
                        goto case 2;
                    case 2:
                        foreach (string b_key in L_keyColl) {
                            DialogResult result = new DialogResult();
                            if (b_key != null) {
                                args = b_key;
                                queConsole = new Queue();
                                queConsole.Enqueue("// Input new information for : [ " + b_key + " ] by typing in the console.");
                                queConsole.Enqueue("// Change [ " + b_key + " ] values by pressing {ENTER} within the control or {CLICK} the enter button.");
                                tbConsoleTextDPScan.Text = "";
                            }
                            if (tbConsoleTextDPScan.Text.Length > 0) {
                                result = MessageBox.Show("You entered : " + "", "Save These Changes?", MessageBoxButtons.YesNo);
                            }
                            intCase = 3;
                            //_value = false;
                            args = "";
                        }
                        goto case 3;
                    case 3:
                        foreach (string strK in U_keyColl) {
                            if (strK != null) {
                            } else {
                            }
                        }
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                    case 7:
                        break;
                    case 8:
                        break;
                    case 9:
                        break;
                    case 10:
                        break;
                    case 20:
                        break;
                }

            } catch (Exception ex) {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}

            

    

                
                
