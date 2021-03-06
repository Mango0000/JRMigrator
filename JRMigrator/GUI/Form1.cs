﻿using System;
using System.Windows.Forms;

namespace JRMigrator
{
    public partial class Form1 : Form
    {
        private int DB1tested = 0;
        private int DB2tested = 0;
        public Form1()
        {
            
            InitializeComponent();
            cbDB1.Enabled = false;
            test();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            //throw new System.NotImplementedException();
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {
            //throw new System.NotImplementedException();
        }

        private void bt1_Click(object sender, EventArgs e)
        {
            DB1tested = 1;
            callDB1();
        }


        private void cb1_CheckedChanged(object sender, EventArgs e)
        {
            if (!cb1.Checked)
            {
                tbPw1.PasswordChar = '*';
               // MessageBox.Show("help");
            }
            else
            {
                tbPw1.PasswordChar = '\0';
              
            }
        }

        private void cb2_CheckedChanged(object sender, EventArgs e)
        {
            if (!cb2.Checked)
            {
                tbPw2.PasswordChar = '*';
            }
            else
            {
                tbPw2.PasswordChar = '\0';
              
            }
        }
        private void defaultValues(object sender, EventArgs e)
        {
            if (cbDB2.SelectedItem.Equals("OracleSQL"))
            {
                tbdatabasename2.Text = "";
                tbAdr2.Text = "";
                tbPort2.Text = "1521";
                tbUser2.Text = "";
                tbPw2.Text = "";

            }else{
                tbdatabasename2.Text = "";
                tbAdr2.Text = "";
                tbPort2.Text = "1433";
                tbUser2.Text = "";
                tbPw2.Text = "";  
        }
        }
        private void bt2_Click(object sender, EventArgs e)
        {
            DB2tested = 1;
            callDB2();
        }

        private void daten_Click(object sender, EventArgs e)
        {
            closeConnections();
            callDB1();
            callDB2();
            migrate();
            
        }

      
    }
}
