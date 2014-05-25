using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace AMHC_SERVER_CLIENT
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void btn_start_server_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = "sv.exe -console -novid -perfectworld +update_addon_paths";
            process.StartInfo.Arguments = "";
            try
            {
                process.Start();
            }
            catch (System.ComponentModel.Win32Exception)
            {
                MessageBox.Show("系统无法运行DOTA2！");
                return;
            }
        }
    }
}
