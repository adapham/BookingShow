using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SE1615_Group2.GUI
{
    public partial class MainGUI : Form
    {
        public MainGUI()
        {
            InitializeComponent();
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowGUI f = new ShowGUI();
            f.TopLevel = false;
            f.FormBorderStyle = FormBorderStyle.None;
            f.Show();


            toolStripContainer1.ContentPanel.Controls.Clear();
            toolStripContainer1.ContentPanel.Controls.Add(f);

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripContainer1_ContentPanel_Load(object sender, EventArgs e)
        {

        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (loginToolStripMenuItem.Text.StartsWith("Login"))
            {
                LoginGUI f = new LoginGUI();
                DialogResult dr = f.ShowDialog();
            }
            else
            {
                Settings.UserName = "";
                MessageBox.Show("You are logged out");
            }
        }

        private void MainGUI_Activated(object sender, EventArgs e)
        {
            
                if (Settings.UserName == "")
                    loginToolStripMenuItem.Text = "Login";
                else
                    loginToolStripMenuItem.Text = $"Logout ({Settings.UserName})";
        }
    }
}
