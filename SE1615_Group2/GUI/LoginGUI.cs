﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SE1615_Group2.GUI
{
    public partial class LoginGUI : Form
    {
        public LoginGUI()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string name, pass;
            var conf = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            name = conf["User:Name"];
            pass = conf["User:Password"];
            //MessageBox.Show($"Name = {name}, Pass = {pass}");
            if (txtName.Text == name && txtPassword.Text == pass)
            {
                MessageBox.Show("You are logged in as administrator");
                Settings.UserName = name;
            }
            else
                MessageBox.Show("Don't have that user");
        }

        private void LoginGUI_Activated(object sender, EventArgs e)
        {

        }

        private void LoginGUI_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
