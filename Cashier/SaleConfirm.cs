using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Cashier.Properties;


namespace Cashier
{
    public partial class SaleConfirm : Form
    {
        private ArrayList list;
        private String savePath;

        public SaleConfirm()
        {
            InitializeComponent();
        }

        public void SetOrderNo(string orderNo)
        {
            orderNoTextBox.Text = orderNo;
        }

        public void SetList(ArrayList snList)
        {
            list = snList;
            snTotalTextBox.Text = list.Count.ToString();
        }


        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件路径";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                savePath = dialog.SelectedPath + "\\sn_"+ orderNoTextBox.Text + "_"+list.Count.ToString()+".xls";


                if(Excel.ListToExcel(savePath, list, orderNoTextBox.Text, Settings.Default.columns))
                {

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                   
                }
            }
            
        }
    }
}
