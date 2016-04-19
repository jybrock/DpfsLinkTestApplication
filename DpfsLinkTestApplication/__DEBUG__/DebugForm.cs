#region USINGS
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DpfsLinkTestApplication;
using DpfsLinkTestApplication.__Console__;
using DpfsLinkTestApplication.DEBUG;
using System.Diagnostics;

#endregion

namespace DpfsLinkTestApplication {
    public partial class DebugForm  {


        /// <summary>
        /// Main Entry Point for Debug Form.
        /// Initializer.
        /// </summary>
        public DebugForm(object sender, EventArgs e) {
            InitializeComponent();
        }


        
        private void btnMessageExit_Click(object sender, EventArgs e) {
            this.Close();
            int i = 1;
        }



    }
}