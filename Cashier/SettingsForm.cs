using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cashier.Properties;

namespace Cashier
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();

            erpEnabled.Checked = Settings.Default.erpEnabled;
            erpAddress.Text = Settings.Default.erp;
            erpToken.Text = Settings.Default.token;

            excelColumns.Text = Settings.Default.columns.ToString();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Settings.Default.erpEnabled = erpEnabled.Checked;
            Settings.Default.erp = erpAddress.Text;
            Settings.Default.token = erpToken.Text;
            Settings.Default.columns = int.Parse(excelColumns.Text);
            Settings.Default.Save();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
