using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using DpfsLinkTestApplication.com.dpro.www;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using DpfsLinkTestApplication.__Monster__;
using DpfsLinkTestApplication.__SOAP__;
using DpfsLinkTestApplication.Properties;


namespace DpfsLinkTestApplication.__Monster__ {

    ///-------------------------------------------------------------------------------------------------
    /// <summary>
    /// A bobo success.
    /// </summary>
    ///
    /// <remarks>
    /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
    /// </remarks>
    ///
    /// <seealso cref="T:System.Windows.Forms.Form"/>
    ///-------------------------------------------------------------------------------------------------

    public partial class BoboSuccess : Form {

        #region MonsterGame Properties

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// The que text.
        /// </summary>
        ///-------------------------------------------------------------------------------------------------

        Queue queText = new Queue();

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets a value indicating whether the bobo life.
        /// </summary>
        ///
        /// <value>
        /// true if bobo life, false if not.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static bool boolBoboLife { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the int u icontinue.
        /// </summary>
        ///
        /// <value>
        /// The int u icontinue.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        private int intUIcontinue { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets a value indicating whether the account access.
        /// </summary>
        ///
        /// <value>
        /// true if account access, false if not.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static bool boolAccountAccess { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        ///
        /// <value>
        /// The string username.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static string strUsername { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        ///
        /// <value>
        /// The string password.
        /// </value>
        ///-------------------------------------------------------------------------------------------------

        public static string strPassword { get; set; }
        #endregion

        #region MonsterGame Fields

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// The text.
        /// </summary>
        ///-------------------------------------------------------------------------------------------------

        public string[] strText = new string[3];
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Monster_Game Level 1 On Bobo Success!!!
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///-------------------------------------------------------------------------------------------------

        public BoboSuccess() {
            InitializeComponent();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Entry Point for Level 1.
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

        private void BoboSuccess_Load(object sender, EventArgs e) {
            MakeStrText();
            //for (intUIcontinue = 0; intUIcontinue < strText.Count(); intUIcontinue++) {                   // Giving {3} Possible Stages
            //    UIContinue();
            //}
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Sets the current MonsterGame User Level's UI Continue Text Array.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///-------------------------------------------------------------------------------------------------

        private void MakeStrText() {
            if (strText != null) {
                strText[0] = "The best thing to do is stay and fight.  Then you win!";
                strText[1] = "The village has peace. The End!";
                strText[2] = "Incoming Message......";
            } else {
                Queue strQueue = new Queue();
                strQueue.Enqueue("The best thing to do is stay and fight.  Then you win!");
                strQueue.Enqueue("The village has peace. The End!");
                strQueue.Enqueue("Incoming Message......");
                queText = strQueue;
                string[] strQ = new string[2];
                strQ[0] = strQueue.Dequeue().ToString();
                strQ[1] = strQueue.Dequeue().ToString();
                strQ[2] = strQueue.Dequeue().ToString();
                strText = strQ;
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets the dpscanservice.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <returns>
        /// A DPScanService.
        /// </returns>
        ///-------------------------------------------------------------------------------------------------

        public static DPScanService _dpscanservice() {
            DPScanService dp = new DPScanService();
            dp.InitializeLifetimeService();
            return dp;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Does some work during DPScan async transaction.
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

        public void DoIndependentWork(object sender) {
            Application.DoEvents();
            btnReadMessage.Visible = true;
            btnReadMessage.Text = "Read New Message";
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Event Handler for when the form is completely shown/drawn.
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

        private void BoboSuccess_Shown(object sender, EventArgs e) {
            //DialogResult uiContinue = new DialogResult();
            //
            // Start our counter for this user session.
            int i = 0;
            //
            // Start our loop for this user session.
            for (intUIcontinue = 0; intUIcontinue < 3; intUIcontinue++) {                   // Giving {3} Possible Stages
                //
                // We'll base the UI text on our user session counter.
                switch (i) {
                    //
                    // First time around for User.
                    case 0:
                        picBobo0002.Visible = false;
                        picBobo0001.Visible = true;
                        this.Text = "The Houses Are Saved!";
                        if (intUIcontinue == 0) {
                            MessageBox.Show(strText[i], "Level {1} : CONTINUE?", MessageBoxButtons.OK);
                            i++;            // Incriment our user session counter
                            goto case 10;   // Jump to the actions case {case 10}
                        } else {
                            i++;            // Incriment our user session counter
                            goto case 1;
                        }
                    //
                    // Second time around for User.
                    case 1:
                        picBobo0001.Visible = false;
                        picBobo0002.Visible = true;
                        if (intUIcontinue == 1) {
                            MessageBox.Show(strText[i], "Level {1} : CONTINUE?", MessageBoxButtons.OK);
                            i++;            // Incriment our user session counter
                            goto case 10;
                        } else {
                            break;
                        }
                    //
                    // Third time around for User.
                    case 2:
                        //this.Text = "...or is it? THE END?";
                        btnReadMessage.Visible = true;
                        btnReadMessage.Text = "Read Message";
                           MessageBox.Show(strText[i], "Level {1} : COMPLETED", MessageBoxButtons.OK);
                            //btnReadMessage.Text = "...INCOMING...MESSAGE...";
                            //i++;    // Incriment our user session counter
                           bool boolTest = DPscanService.AccessDPScan(MainForm.dpscanservice);
                           if (boolTest) {
                               goto case 20;
                           } else {
                               goto case 0;
                           }
                    //
                    // Provide actions based on UI (DialogBox) result.
                    case 10:
                        intUIcontinue++;
                        //int xi = 0;
                        //if (uiContinue == DialogResult.Yes) {
                        //    // Move on
                        //    btnReadMessage.Visible = true;
                        //    btnReadMessage.Text = "...INCOMING...MESSAGE...";
                        //    xi++;
                        //    goto case 11;
                        //}
                        //if (uiContinue == DialogResult.No) {
                        //    // Stop here
                        //    btnReadMessage.Visible = true;
                        //    btnReadMessage.Text = "...INCOMING...MESSAGE...";
                        //    xi++;
                        //    goto case 12;
                        //}
                        //if (uiContinue == DialogResult.Cancel) {
                        //    // Reset?
                        //    btnReadMessage.Visible = false;
                        //    goto case 13;
                        //}
                        //if (xi == 0) {
                        //    DialogResult err = MessageBox.Show("Something went wrong", "This is not supposed to happen", MessageBoxButtons.AbortRetryIgnore);
                        //    if (err == DialogResult.Abort)
                        //        break;
                        //    if (err == DialogResult.Retry)
                        //        break;
                        //    if (err == DialogResult.Ignore)
                        //        break;
                        //}
                        break;
                    case 11:                        // User answered Yes whether to Continue
                        intUIcontinue++;            // Increment UI Continue counter
                        i = -1;                     // Set our User session counter for err
                        break;
                    case 12:                        // User answered No whether to Continue
                        i = -1;
                        break;
                    case 13:                        // User answered Cancel whether to Continue
                        i = -1;
                        break;
                    case 20:
                        DoIndependentWork(sender);
                        return;
                }
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// UI Button used for testing on Level 1.
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

        private void button1_Click(object sender, EventArgs e) {

            DPscanService.AccessDPScan(MainForm.dpscanservice);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Event handler. Called by picBobo0002 for click events.
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

        private void picBobo0002_Click(object sender, EventArgs e) {

        }
 
    
    
    }
}


#region OLD UIContinue code
///// <summary>
///// Holds the Story Line.
///// Runs a Loop of potential places the User could be at.
///// </summary>
//private void UIContinue() {
//    //
//    // Instantiate the DialogBox Result object.
//    DialogResult uiContinue = new DialogResult();
//    //
//    // Start our counter for this user session.
//    int i = -1;
//    //
//    // Start our loop for this user session.
//    //for (intUIcontinue = 0; intUIcontinue < strText.Count(); intUIcontinue++) {                   // Giving {3} Possible Stages
//    //
//    // We'll base the UI text on our user session counter.
//    switch (i) {
//        case -1:
//            break;
//        //
//        // First time around for User.
//        case 0:
//            picBobo0002.Visible = false;
//            picBobo0001.Visible = true;
//            if (intUIcontinue == 0) {
//                uiContinue = MessageBox.Show(queText.Dequeue().ToString(), "Level {1} : CONTINUE?", MessageBoxButtons.YesNoCancel);
//                i++;    // Incriment our user session counter
//                goto case 10;   // Jump to the actions case {case 10}
//            } else {
//                uiContinue = MessageBox.Show(strText[i], "Something is wrong!", MessageBoxButtons.YesNoCancel);
//                i++;    // Incriment our user session counter
//                goto case 1;
//            }
//        //
//        // Second time around for User.
//        case 1:
//            picBobo0001.Visible = false;
//            picBobo0002.Visible = true;
//            if (intUIcontinue == 1) {
//                uiContinue = MessageBox.Show(strText[i], "Level {1} : CONTINUE?", MessageBoxButtons.YesNoCancel);
//                i++;    // Incriment our user session counter
//                goto case 10;
//            } else {
//                uiContinue = MessageBox.Show(strText[i], "Something is wrong!", MessageBoxButtons.YesNoCancel);
//                i++;    // Incriment our user session counter
//                goto case 2;
//            }
//        //
//        // Third time around for User.
//        case 2:
//            picBobo0002.Visible = false;
//            if (picBobo0001.Visible)
//                picBobo0001.Visible = false;
//            if (intUIcontinue == 2) {
//                uiContinue = MessageBox.Show(strText[i], "Level {1} : CONTINUE?", MessageBoxButtons.YesNoCancel);
//                i++;    // Incriment our user session counter
//                goto case 10;
//            } else {
//                uiContinue = MessageBox.Show(strText[i], "Something is wrong!", MessageBoxButtons.YesNoCancel);
//                i++;    // Incriment our user session counter
//                goto case 0;
//            }
//        //
//        // Provide actions based on UI (DialogBox) result.
//        case 10:
//            int xi = 0;
//            if (uiContinue == DialogResult.Yes) {
//                // Move on
//                xi++;
//                goto case 11;
//            }
//            if (uiContinue == DialogResult.No) {
//                // Stop here
//                xi++;
//                goto case 12;
//            }
//            if (uiContinue == DialogResult.Cancel) {
//                // Reset?
//                xi++;
//                goto case 13;
//            }
//            if (xi == 0) {
//                DialogResult err = MessageBox.Show("Something went wrong", "This is not supposed to happen", MessageBoxButtons.AbortRetryIgnore);
//                if (err == DialogResult.Abort)
//                    break;
//                if (err == DialogResult.Retry)
//                    break;
//                if (err == DialogResult.Ignore)
//                    break;
//            }
//            break;
//        case 11:                        // User answered Yes whether to Continue
//            intUIcontinue++;            // Increment UI Continue counter
//            i = -1;                     // Set our User session counter for err
//            break;
//        case 12:                        // User answered No whether to Continue
//            i = -1;
//            break;
//        case 13:                        // User answered Cancel whether to Continue
//            i = -1;
//            break;
//    }
//    if (intUIcontinue > strText.Count()) {
//        //
//        // Do something for Level 2.
//    }
//}
#endregion