using System;
using System.Windows.Forms;

namespace JRMigrator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            
            InitializeComponent();
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

        private void bt2_Click(object sender, EventArgs e)
        {
            callDB2();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            migrate();
        }
    }
}
