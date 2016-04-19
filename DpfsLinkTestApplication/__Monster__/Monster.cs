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

namespace DpfsLinkTestApplication.__Monster__ {

    ///-------------------------------------------------------------------------------------------------
    /// <summary>
    /// A monster.
    /// </summary>
    ///
    /// <remarks>
    /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
    /// </remarks>
    ///
    /// <seealso cref="T:System.Windows.Forms.Form"/>
    ///-------------------------------------------------------------------------------------------------

    public partial class Monster : Form {

        #region MonsterGame Properties

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
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Monster Game. This runs like a script for the most part. Use this to play with background
        /// tasks; Web Services; Protocol Bit-Level Encrypted Transactions. For Bobo; of course.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///-------------------------------------------------------------------------------------------------

        public Monster() {

            InitializeComponent();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Function to change WinForms Title.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///-------------------------------------------------------------------------------------------------

        public void UserConfront() {

            this.Text = "Oh No! Monster! : Aaaahh!";
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gives response value for User's response to either run or stay during Monster Game.
        /// Originally a void but turned into a boolean function. Originally the Class __Monster__ ran
        /// like a script.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///
        /// <returns>
        /// true if it succeeds, false if it fails.
        /// </returns>
        ///-------------------------------------------------------------------------------------------------

        public bool UserAction() {


            DialogResult MonsterResult = new DialogResult();
            MonsterResult = MessageBox.Show("Run away?", "Monster Attack!", MessageBoxButtons.YesNo);
            if (MonsterResult == DialogResult.Yes) {
                picMonster.Visible = false;
                picDead.Visible = true;
                boolBoboLife = false;
                this.Text = "The Monster Caught You!  You're Dead!";
                
            } else if (MonsterResult == DialogResult.No) {
                picMonster.Visible = false;
                picWin.Visible = true;
                boolBoboLife = true;
                this.Text = "You Win!";
            }

            return boolBoboLife;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Main entry point for Monster Game. Refreshes UI and starts the Monster Game.
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

        private void Monster_Load(object sender, EventArgs e) {

            Application.DoEvents();
            UserConfront();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Hold User's decision to run or stay.
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

        private void Monster_Shown(object sender, EventArgs e) {
            //
            // Instantiate private scope UI result pointer
            bool boolUserResult = false;
            //
            // Assign UI result
            boolUserResult = UserAction();
            //
            // Flip through switch (faster than eyeef)
            switch (boolUserResult) {
                //
                // Unfortunately the User died!
                case false:
                    if (!(boolUserResult) && !(boolBoboLife)) {
                        //
                        // Do something (or not...the user is dead)
                        return;
                    } else {
                        MessageBox.Show("Either the User lost all lives or had the wrong result", "This is not possible", MessageBoxButtons.OK);
                        goto case true;
                    }
                case true:
                    //
                    // Fortunately for the User they chose the right path!
                    //BoboSuccess bobosuccess = new BoboSuccess();
                    //bobosuccess.AccessDPScan();
                    //
                    // Verify User has life and UI result is good.
                    if ((boolUserResult) && (boolBoboLife)) {
                        GotoNextLevel();
                    } else {
                        //
                        // Either User result was bad or User has no life!
                        this.Refresh();
                        UserConfront();
                        picMonster.Visible = true;
                        picWin.Visible = false;
                        boolBoboLife = false;
                        UserAction();
                        //MessageBox.Show("Something went terribly wrong", "This is not possible", MessageBoxButtons.OK);
                    }
                    return;
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Moves User to Level 1.
        /// </summary>
        ///
        /// <remarks>
        /// Joey Brock <jybrock@dpro.com>, 4/19/2013.
        /// </remarks>
        ///-------------------------------------------------------------------------------------------------

        public void GotoNextLevel() {
            DialogResult uiLevel1 = new DialogResult();
            BoboSuccess bobosuccess = new BoboSuccess();
            //
            // Give User choice to exit now.
            uiLevel1 = MessageBox.Show("Want to see what happens next?", "YOU WIN!!!", MessageBoxButtons.YesNo);
            //
            // User is weak!
            if (uiLevel1 == DialogResult.No) {
                boolBoboLife = false;
                this.Close();
                Monster monster = new Monster();
                monster.Show();
            } else {
                //
                // User is ready!
                this.Close();
                boolBoboLife = true;
                bobosuccess.Show();
                bobosuccess.BringToFront();
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Event handler. Called by picWin for click events.
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

        private void picWin_Click(object sender, EventArgs e) {

        }


        

    }
}

    

