using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using DpfsLinkTestApplication.__Monster__;
using DpfsLinkTestApplication.__SOAP__;
using DpfsLinkTestApplication.__Console__;
using DpfsLinkTestApplication.com.dpro.www;

namespace DpfsLinkTestApplication {
    public partial class SelectDPScanService : Form {

        #region Select DPScanService Properties
        private static string strUsername { get; set; }
        private static string strPassword { get; set; }
        private static string strDPScanServiceWSDL { get; set; }
        private static string strDPScanServiceASMX { get; set; }
        private static Uri uriDPScanServiceASMX { get; set; }
        private static Uri uriDPScanServiceWSDL { get; set; }
        private static Uri uriDPScanASMX { get; set; }
        private static string strDPScanASMX { get; set; }
        private static Uri uriDPScanWSDL { get; set; }
        private static string strDPScanWSDL { get; set; }
        public static string strSelectedDPScanService { get; set; }
        public static int intSelIndex { get; set; }
        public static DataGrid datagrid { get; set; }
        public static XmlDocument wsdlDPScanService { get; set; }
        public static string[] strText = new string[3];
        public static bool boolAccountAccess { get; set; }
        public static Queue queDPScanInitialize { get; set; }
        public static int intUIcontinue { get; set; }
        public static com.dpro.www.DPScanService dpscanservice { get; set; }
        public static Dictionary<string, string> DictListDPServices { get; set; }
        #endregion

        #region Select DPScanService Fields
        #endregion

        /// <summary>
        /// Entry point for Select DPScanService form.
        /// </summary>
        public SelectDPScanService() {
            InitializeComponent();
            intSelIndex = 0;
        }


        /// <summary>
        /// Try to load DPScanService Default Settings?
        /// </summary>
        public static void GetInstanceofDPscan() {
            DialogResult result = new DialogResult();
            string[] strA = new string[3];
            result = MessageBox.Show("Try to load DPScanService Defaults?", "Loading Defaults", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes) {
                strPassword = __SOAP__.DPscanService.strPassword;
                strText = __SOAP__.DPscanService.strText;
                boolAccountAccess = __SOAP__.DPscanService.boolAccountAccess;
                strUsername = __SOAP__.DPscanService.strUsername;
                queDPScanInitialize = __SOAP__.DPscanService.queDPScanInitialize;
                intUIcontinue = __SOAP__.DPscanService.intUIcontinue;
                strDPScanASMX = __SOAP__.DPscanService.strDPScanASMX;
                uriDPScanWSDL = __SOAP__.DPscanService.uriDPScanWSDL;
                strDPScanWSDL = __SOAP__.DPscanService.strDPScanWSDL;
                strA[0] = " ";
                strA[1] = "// ...there's nothing Simple about 'S.O.A.P.!";
                strA[2] = " ";
            } else {
                strA[0] = " ";
                strA[1] = "// ...Good Luck, very brave one indeed!";
                strA[2] = " ";
            }
            Queue q = new Queue();
            foreach (string str in strA) {
                q.Enqueue(str);
            }
        }



        /// <summary>
        /// Used to update or modify the DPScanService WSDL location.
        /// HTTP Protocol.
        /// Any changes overwritten upon application restart.
        /// </summary>
        private void UpdateDPScanWSDL(string newWSDL) {
            if (newWSDL != null) {
                if (newWSDL.Length > 0) {
                    if (newWSDL.Contains(".asmx?WSDL")) {
                        //
                        // Go ahead and update the run-time instance value of the DPScanService WSDL.
                        DialogResult result = new DialogResult();
                        result = MessageBox.Show("New DPScanService WSDL : " + newWSDL, "Make Changes?", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes) {
                            strDPScanServiceWSDL = newWSDL;
                        } else {
                            //
                            // Downhill of errors (changes aborted).
                            MessageBox.Show("No changes were made", "DPScanService Update Aborted", MessageBoxButtons.OK);
                        }
                    } else {
                        MessageBox.Show("Invalid WSDL Format", "DPScanService Location Verification", MessageBoxButtons.OK);
                    }
                } else {
                    MessageBox.Show("Invalid WSDL Format", "DPScanService Location Verification", MessageBoxButtons.OK);
                }
            } else {
                MessageBox.Show("Empty WSDL Format", "DPScanService Location Verification", MessageBoxButtons.OK);
            }
        }



        /// <summary>
        /// Used to update or modify the DPScanService WSDL location.
        /// HTTP Protocol.
        /// Any changes overwritten upon application restart.
        /// </summary>
        private void UpdateDPScanASMX(string newASMX) {
            if (newASMX != null) {
                if (newASMX.Length > 0) {
                    if (newASMX.Contains(".asmx")) {
                        //
                        // Go ahead and update the run-time instance value of the DPScanService WSDL.
                        DialogResult result = new DialogResult();
                        result = MessageBox.Show("New DPScanService ASMX : " + newASMX, "Make Changes?", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes) {
                            strDPScanServiceASMX = newASMX;
                        } else {
                            //
                            // Downhill of errors (changes aborted).
                            MessageBox.Show("No changes were made", "DPScanService Update Aborted", MessageBoxButtons.OK);
                        }
                    } else {
                        MessageBox.Show("Invalid ASMX Format", "DPScanService Location Verification", MessageBoxButtons.OK);
                    }
                } else {
                    MessageBox.Show("Invalid ASMX Format", "DPScanService Location Verification", MessageBoxButtons.OK);
                }
            } else {
                MessageBox.Show("Empty ASMX Format", "DPScanService Location Verification", MessageBoxButtons.OK);
            }
        }



        /// <summary>
        /// Event Handler for WinForm onLoad event.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void SelectDPScanService_Load(object sender, EventArgs e) {
            Application.DoEvents();
            try {
                webSelectDPScanService.Url = __SOAP__.DPscanService.uriDPScanASMX;


                //__SOAP__.DPscanService.strDPScanASMX = "http://www.dpro.com/dpscanservice/dpscan.asmx";
                //strDPScanServiceWSDL = "http://www.dpro.com/dpscanservice/dpscan.asmx?WSDL";
                //uriDPScanServiceASMX = __SOAP__.DPscanService.uriDPScanASMX;
                //uriDPScanServiceWSDL = __SOAP__.DPscanService.uriDPScanWSDL;
                //webSelectDPScanService.Url = uriDPScanServiceASMX;
                //webSelectDPScanService.Url = new Uri(__SOAP__.DPscanService.strDPScanASMX);

            } catch (Exception ex) {
                MessageBox.Show(ex.Message + " "
                    + ex.InnerException, "Internet Connection Failed", MessageBoxButtons.OK);
            } finally {
                // Not much going on here.
            }
            //this.reportViewer1.RefreshReport();
        }



        /// <summary>
        /// Event Handler for WinForm onShown event.
        /// Uses HashTable version of GenerateXmLMappings.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void SelectDPScanService_Shown(object sender, EventArgs e) {
            try {
                __SOAP__.DPscanService.listArrayDPServices = new ArrayList();                                   // Create ArrayList to place DPScanService Web Service Mappings
                __SOAP__.DPscanService.XMLDPScanWSDL = new XmlDocument();                                       // Make new XmL Document to hold DPScanService Web Service References
                // Turning Up Null
                //__SOAP__.DPscanService.XMLDPScanWSDL.Load(__SOAP__.DPscanService.strDPScanASMX);
                __SOAP__.DPscanService.XMLDPScanWSDL.Load(__SOAP__.DPscanService.uriDPScanASMX.OriginalString);
                wsdlDPScanService = __SOAP__.DPscanService.XMLDPScanWSDL;
                com.dpro.www.DPScanService.GenerateXmlMappings(uriDPScanServiceASMX.GetHashCode().GetType(), __SOAP__.DPscanService.listArrayDPServices);
            } catch (Exception ex) {
                if (ex.HResult.Equals(-2147467261))
                    return;
                if (ex.HResult.Equals(0x80131534))
                    return;
                if (ex.HResult < 0) {
                    MessageBox.Show(ex.Source + " [" + ex.HResult + "] " + ex.HelpLink + ": " + ex.Message, "Error : " + this.GetType().ToString(), MessageBoxButtons.OK);
                } else {
                    MessageBox.Show(ex.GetHashCode().ToString(), "", MessageBoxButtons.OK);
                }
            } finally {
                // Someday make use of the available HashTable
            }
        }
    
    


        /// <summary>
        /// Event Handler for DPScanService Specific Item Checked event.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ItemCheckEventArgs</param>
        private void clbxMsgTypeSelect_ItemCheck(object sender, ItemCheckEventArgs e) {
            intSelIndex = e.Index;
            if (clbxMsgTypeSelect.SelectedItems.Count == 0) {
                e.NewValue = CheckState.Checked;
            }
            if (clbxMsgTypeSelect.SelectedItems.Count > 1) {
                e.NewValue = CheckState.Unchecked;
                sender = e.CurrentValue;
            } else if (clbxMsgTypeSelect.SelectedItems.Count > 1) {
                e.NewValue = CheckState.Unchecked;
            }
        }



        /// <summary>
        /// Event Handler for DPScanService Selected Item Index Changed event.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void clbxMsgTypeSelect_SelectedIndexChanged_2(object sender, EventArgs e) {
            try {
                intSelIndex = clbxMsgTypeSelect.SelectedIndex;
                webSelectDPScanService.Stop();
                if (clbxMsgTypeSelect.SelectedItem == null) {
                    clbxMsgTypeSelect.SetSelected(0, true);
                }
                string strItem = clbxMsgTypeSelect.GetItemText(clbxMsgTypeSelect.SelectedIndex);
                webSelectDPScanService.Url = new Uri("http://www.dpro.com/dpscanservice/dpscan.asmx?op=" + clbxMsgTypeSelect.SelectedItem.ToString());
            } catch (Exception ex) {
                MessageBox.Show("Error : " + ex.Message, "DPScanService Selected Item Index", MessageBoxButtons.OK);
            }
        }



        /// <summary>
        /// Event Handler for DPScanService for Click event.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void clbxMsgTypeSelect_Click_1(object sender, EventArgs e) {
            clbxMsgTypeSelect.SetItemChecked(intSelIndex, false);
            sender = CheckState.Checked;
            intSelIndex = clbxMsgTypeSelect.SelectedIndex;
        }


        /// <summary>
        /// Event Handler for DPScanService Select WebService Page Load Button.
        /// Handles the click event of the Load button.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void btnLoadMessage_Click(object sender, EventArgs e) {
            webSelectDPScanService.Stop();
            webSelectDPScanService.Url = new Uri("http://www.dpro.com/dpscanservice/dpscan.asmx");
        }


        /// <summary>
        /// Event Handler for DPScanService Select WebService Page Save Button.
        /// Handles the click event for the save button.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void btnMessageSave_Click(object sender, EventArgs e) {


        }


        /// <summary>
        /// Event Handler for DPScanService Select WebService Page the Clear Button.
        /// Handles the click event for the clear button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMessageClear_Click(object sender, EventArgs e) {
            webSelectDPScanService.Stop();
            webSelectDPScanService.Url = uriDPScanServiceASMX;
            clbxMsgTypeSelect.ClearSelected();
        }


        /// <summary>
        /// Event Handler for DPScanService Select WebService Page the Exit Button.
        /// Handles the click event for the exit button.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void btnMessageExit_Click(object sender, EventArgs e) {
            webSelectDPScanService.Stop();
            this.Close();
        }




        /// <summary>
        /// Event Handler for Binding Data Source Changes at DPScanService
        /// Web Service Binding.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void bindSourceDPScan_CurrentChanged(object sender, EventArgs e) {

        }




        /// <summary>
        /// Interface for Dictionary Collection DictListDPServices.
        /// Collect of DPScanService Exposed Web Methods.
        /// Contains Default Definition Set and Dynamiclly Generated Defs.
        /// </summary>
        /// <param name="DPList">Dictionary string,string </param>
        /// <returns>IDictionary DictListDPServices</returns>
        private static IDictionary<string,string> dictListDPServices(Dictionary<string,string> DPList) {
            DPList = new Dictionary<string,string>();
            DPList.Add("CountAccess","string");
            DPList.Add("GetItem", "string");
            DPList.Add("GetProject", "string");
            DPList.Add("ItemToProject", "string");
            DPList.Add("ItemToProjectAccess", "string");
            DPList.Add("ItemTransfer", "string");
            DPList.Add("ItemTransferAccess", "string");
            DPList.Add("ReceivingAccess", "string");
            DPList.Add("ReceivingPostReceipt", "string");
            DPList.Add("ReceivingValidateItem ", "string");
            DPList.Add("ReceivingValidatePO", "string");
            DPList.Add("ShippingAccess", "string");
            DPList.Add("ShippingPostShipment ", "string");
            DPList.Add("ShippingValidateItem", "string");
            DPList.Add("ShippingValidateSO ", "string");
            DPList.Add("UpdateCount", "string");
            DictListDPServices = DPList;

            // not implemented
            com.dpro.www.DPScanService _dpscanservice = new com.dpro.www.DPScanService();
            ObservableCollection<object> groupsDPScan = new ObservableCollection<object>();
            //        DynamicMethod multiplyHidden = new DynamicMethod(
            //            "", 
            //            typeof(int), 
            //            methodArgs2, 
            //            typeof(Example));
            // not implemented
            //        OneParameter<int, int> invoke = (OneParameter<int, int>)
            //            multiplyHidden.CreateDelegate(
            //                typeof(OneParameter<int, int>), 
            //                new Example(42)
            //            );
            //        Console.WriteLine("3 * test = {0}", invoke(3));
            int iCount = 0;
            int i = 0;

            iCount = _dpscanservice.ConnectionGroupName.Count();
            for (i = 0; i < iCount; i++) {
                groupsDPScan.Add(_dpscanservice.ConnectionGroupName.ElementAtOrDefault(i));
            }

            return DictListDPServices;
        }

        private void dataRepeater1_CurrentItemIndexChanged(object sender, EventArgs e) {

        }

     

    
    }
}


