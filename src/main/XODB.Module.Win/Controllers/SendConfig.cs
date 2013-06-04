using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace XODB.Module.Win.Controllers
{
    public partial class SendConfig : DevExpress.XtraEditors.XtraForm
    {
        public SendConfig()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            var path = Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf("\\"));
            if (txtRecipients.Text != null)
            {
                var recipients = txtRecipients.Text.Split(new string[] { ",", ";", " ", "\t", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                path.SendUserConfig(recipients);
            }
            this.Close();
        }
    }
}