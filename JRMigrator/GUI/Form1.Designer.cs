﻿using System;
using System.Data;
using System.Windows.Forms;
using JRMigrator.beans;
using JRMigrator.BL;

namespace JRMigrator
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        public void newButton_OnClicked(object s, EventArgs e)
        {
            MessageBox.Show("Clicked!");
        }
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbDB1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lb1 = new System.Windows.Forms.Label();
            this.tbdatabasename1 = new System.Windows.Forms.TextBox();
            this.cb1 = new System.Windows.Forms.CheckBox();
            this.bt1 = new System.Windows.Forms.Button();
            this.tbPort1 = new System.Windows.Forms.TextBox();
            this.lbPort1 = new System.Windows.Forms.Label();
            this.cbDB1 = new System.Windows.Forms.ComboBox();
            this.tbUser1 = new System.Windows.Forms.TextBox();
            this.tbAdr1 = new System.Windows.Forms.TextBox();
            this.tbPw1 = new System.Windows.Forms.TextBox();
            this.lbPw1 = new System.Windows.Forms.Label();
            this.lbUser1 = new System.Windows.Forms.Label();
            this.lbAdr1 = new System.Windows.Forms.Label();
            this.lbDB1 = new System.Windows.Forms.Label();
            this.gbDB2 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.daten = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbdatabasename2 = new System.Windows.Forms.TextBox();
            this.cb2 = new System.Windows.Forms.CheckBox();
            this.bt2 = new System.Windows.Forms.Button();
            this.tbPort2 = new System.Windows.Forms.TextBox();
            this.lbPort2 = new System.Windows.Forms.Label();
            this.cbDB2 = new System.Windows.Forms.ComboBox();
            this.tbUser2 = new System.Windows.Forms.TextBox();
            this.tbAdr2 = new System.Windows.Forms.TextBox();
            this.tbPw2 = new System.Windows.Forms.TextBox();
            this.lbPw2 = new System.Windows.Forms.Label();
            this.lbUser2 = new System.Windows.Forms.Label();
            this.lbAdr2 = new System.Windows.Forms.Label();
            this.lbDB2 = new System.Windows.Forms.Label();
            this.gbSum = new System.Windows.Forms.GroupBox();
            this.taSummary = new System.Windows.Forms.TextBox();
            this.gbDB1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gbDB2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.gbSum.SuspendLayout();
            this.SuspendLayout();
            this.gbDB1.Controls.Add(this.panel1);
            this.gbDB1.Location = new System.Drawing.Point(12, 12);
            this.gbDB1.Name = "gbDB1";
            this.gbDB1.Size = new System.Drawing.Size(506, 246);
            this.gbDB1.TabIndex = 0;
            this.gbDB1.TabStop = false;
            this.gbDB1.Text = "Datenbank 1";
            this.panel1.Controls.Add(this.lb1);
            this.panel1.Controls.Add(this.tbdatabasename1);
            this.panel1.Controls.Add(this.cb1);
            this.panel1.Controls.Add(this.bt1);
            this.panel1.Controls.Add(this.tbPort1);
            this.panel1.Controls.Add(this.lbPort1);
            this.panel1.Controls.Add(this.cbDB1);
            this.panel1.Controls.Add(this.tbUser1);
            this.panel1.Controls.Add(this.tbAdr1);
            this.panel1.Controls.Add(this.tbPw1);
            this.panel1.Controls.Add(this.lbPw1);
            this.panel1.Controls.Add(this.lbUser1);
            this.panel1.Controls.Add(this.lbAdr1);
            this.panel1.Controls.Add(this.lbDB1);
            this.panel1.Location = new System.Drawing.Point(10, 22);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(457, 218);
            this.panel1.TabIndex = 4;
            this.lb1.Location = new System.Drawing.Point(241, 65);
            this.lb1.Name = "lb1";
            this.lb1.Size = new System.Drawing.Size(103, 27);
            this.lb1.TabIndex = 13;
            this.lb1.Text = "Datenbankname";
            this.tbdatabasename1.Location = new System.Drawing.Point(357, 62);
            this.tbdatabasename1.Name = "tbdatabasename1";
            this.tbdatabasename1.Size = new System.Drawing.Size(74, 23);
            this.tbdatabasename1.TabIndex = 12;
            this.cb1.Location = new System.Drawing.Point(315, 160);
            this.cb1.Name = "cb1";
            this.cb1.Size = new System.Drawing.Size(124, 28);
            this.cb1.TabIndex = 11;
            this.cb1.Text = "Passwort anzeigen";
            this.cb1.UseVisualStyleBackColor = true;
            this.cb1.CheckedChanged += new System.EventHandler(this.cb1_CheckedChanged);
            this.bt1.Location = new System.Drawing.Point(313, 113);
            this.bt1.Name = "bt1";
            this.bt1.Size = new System.Drawing.Size(126, 23);
            this.bt1.TabIndex = 10;
            this.bt1.Text = "Verbindung testen";
            this.bt1.UseVisualStyleBackColor = true;
            this.bt1.Click += new System.EventHandler(this.bt1_Click);
            this.tbPort1.Location = new System.Drawing.Point(374, 14);
            this.tbPort1.Name = "tbPort1";
            this.tbPort1.Size = new System.Drawing.Size(56, 23);
            this.tbPort1.TabIndex = 9;
            this.tbPort1.Text = "33000";
            this.lbPort1.Location = new System.Drawing.Point(307, 14);
            this.lbPort1.Name = "lbPort1";
            this.lbPort1.Size = new System.Drawing.Size(31, 23);
            this.lbPort1.TabIndex = 8;
            this.lbPort1.Text = "Port";
            this.lbPort1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbDB1.FormattingEnabled = true;
            this.cbDB1.Location = new System.Drawing.Point(100, 13);
            this.cbDB1.Name = "cbDB1";
            this.cbDB1.Size = new System.Drawing.Size(112, 23);
            this.cbDB1.TabIndex = 7;
            this.tbUser1.Location = new System.Drawing.Point(100, 114);
            this.tbUser1.Name = "tbUser1";
            this.tbUser1.Size = new System.Drawing.Size(115, 23);
            this.tbUser1.TabIndex = 6;
            this.tbUser1.Text = "";
            this.tbAdr1.Location = new System.Drawing.Point(100, 62);
            this.tbAdr1.Name = "tbAdr1";
            this.tbAdr1.Size = new System.Drawing.Size(115, 23);
            this.tbAdr1.TabIndex = 5;
            this.tbAdr1.Text = "";
            this.tbPw1.Location = new System.Drawing.Point(100, 165);
            this.tbPw1.Name = "tbPw1";
            this.tbPw1.Size = new System.Drawing.Size(115, 23);
            this.tbPw1.TabIndex = 4;
            this.tbPw1.Text = "";
            this.lbPw1.Location = new System.Drawing.Point(8, 168);
            this.lbPw1.Name = "lbPw1";
            this.lbPw1.Size = new System.Drawing.Size(124, 28);
            this.lbPw1.TabIndex = 3;
            this.lbPw1.Text = "Passwort";
            this.lbUser1.Location = new System.Drawing.Point(8, 117);
            this.lbUser1.Name = "lbUser1";
            this.lbUser1.Size = new System.Drawing.Size(124, 28);
            this.lbUser1.TabIndex = 2;
            this.lbUser1.Text = "Benutzername";
            this.lbAdr1.Location = new System.Drawing.Point(8, 65);
            this.lbAdr1.Name = "lbAdr1";
            this.lbAdr1.Size = new System.Drawing.Size(124, 28);
            this.lbAdr1.TabIndex = 1;
            this.lbAdr1.Text = "Adresse";
            this.lbDB1.Location = new System.Drawing.Point(8, 13);
            this.lbDB1.Name = "lbDB1";
            this.lbDB1.Size = new System.Drawing.Size(124, 28);
            this.lbDB1.TabIndex = 0;
            this.lbDB1.Text = "Datenbank";
            this.gbDB2.Controls.Add(this.panel2);
            this.gbDB2.Location = new System.Drawing.Point(12, 258);
            this.gbDB2.Name = "gbDB2";
            this.gbDB2.Size = new System.Drawing.Size(506, 287);
            this.gbDB2.TabIndex = 1;
            this.gbDB2.TabStop = false;
            this.gbDB2.Text = "Datenbank 2";
            this.panel2.Controls.Add(this.daten);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.tbdatabasename2);
            this.panel2.Controls.Add(this.cb2);
            this.panel2.Controls.Add(this.bt2);
            this.panel2.Controls.Add(this.tbPort2);
            this.panel2.Controls.Add(this.lbPort2);
            this.panel2.Controls.Add(this.cbDB2);
            this.panel2.Controls.Add(this.tbUser2);
            this.panel2.Controls.Add(this.tbAdr2);
            this.panel2.Controls.Add(this.tbPw2);
            this.panel2.Controls.Add(this.lbPw2);
            this.panel2.Controls.Add(this.lbUser2);
            this.panel2.Controls.Add(this.lbAdr2);
            this.panel2.Controls.Add(this.lbDB2);
            this.panel2.Location = new System.Drawing.Point(13, 22);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(457, 258);
            this.panel2.TabIndex = 4;
            this.daten.Location = new System.Drawing.Point(307, 211);
            this.daten.Name = "daten";
            this.daten.Size = new System.Drawing.Size(129, 28);
            this.daten.TabIndex = 3;
            this.daten.Text = "Daten übertragen";
            this.daten.UseVisualStyleBackColor = true;
            this.daten.Click += new System.EventHandler(this.daten_Click);
            this.label2.Location = new System.Drawing.Point(241, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 29);
            this.label2.TabIndex = 14;
            this.label2.Text = "Datenbankname";
            this.tbdatabasename2.Location = new System.Drawing.Point(351, 62);
            this.tbdatabasename2.Name = "tbdatabasename2";
            this.tbdatabasename2.Size = new System.Drawing.Size(77, 23);
            this.tbdatabasename2.TabIndex = 13;
            this.tbdatabasename2.Text = "testdatabase";
            this.cb2.Location = new System.Drawing.Point(311, 162);
            this.cb2.Name = "cb2";
            this.cb2.Size = new System.Drawing.Size(124, 28);
            this.cb2.TabIndex = 12;
            this.cb2.Text = "Passwort anzeigen";
            this.cb2.UseVisualStyleBackColor = true;
            this.cb2.CheckedChanged += new System.EventHandler(this.cb2_CheckedChanged);
            this.bt2.Location = new System.Drawing.Point(310, 114);
            this.bt2.Name = "bt2";
            this.bt2.Size = new System.Drawing.Size(126, 23);
            this.bt2.TabIndex = 11;
            this.bt2.Text = "Verbindung testen";
            this.bt2.UseVisualStyleBackColor = true;
            this.bt2.Click += new System.EventHandler(this.bt2_Click);
            this.tbPort2.Location = new System.Drawing.Point(374, 13);
            this.tbPort2.Name = "tbPort2";
            this.tbPort2.Size = new System.Drawing.Size(53, 23);
            this.tbPort2.TabIndex = 9;
            this.tbPort2.Text = "1433";
            this.lbPort2.Location = new System.Drawing.Point(303, 12);
            this.lbPort2.Name = "lbPort2";
            this.lbPort2.Size = new System.Drawing.Size(35, 23);
            this.lbPort2.TabIndex = 8;
            this.lbPort2.Text = "Port";
            this.lbPort2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbDB2.FormattingEnabled = true;
            this.cbDB2.Location = new System.Drawing.Point(100, 14);
            this.cbDB2.Name = "cbDB2";
            this.cbDB2.Size = new System.Drawing.Size(112, 23);
            this.cbDB2.TabIndex = 7;
            this.tbUser2.Location = new System.Drawing.Point(100, 115);
            this.tbUser2.Name = "tbUser2";
            this.tbUser2.Size = new System.Drawing.Size(115, 23);
            this.tbUser2.TabIndex = 6;
            this.tbUser2.Text = "SA";
            this.tbAdr2.Location = new System.Drawing.Point(100, 65);
            this.tbAdr2.Name = "tbAdr2";
            this.tbAdr2.Size = new System.Drawing.Size(115, 23);
            this.tbAdr2.TabIndex = 5;
            this.tbPw2.Location = new System.Drawing.Point(100, 165);
            this.tbPw2.Name = "tbPw2";
            this.tbPw2.Size = new System.Drawing.Size(115, 23);
            this.tbPw2.TabIndex = 4;
            this.lbPw2.Location = new System.Drawing.Point(8, 168);
            this.lbPw2.Name = "lbPw2";
            this.lbPw2.Size = new System.Drawing.Size(124, 28);
            this.lbPw2.TabIndex = 3;
            this.lbPw2.Text = "Passwort";
            this.lbUser2.Location = new System.Drawing.Point(8, 117);
            this.lbUser2.Name = "lbUser2";
            this.lbUser2.Size = new System.Drawing.Size(124, 28);
            this.lbUser2.TabIndex = 2;
            this.lbUser2.Text = "Benutzername";
            this.lbAdr2.Location = new System.Drawing.Point(8, 65);
            this.lbAdr2.Name = "lbAdr2";
            this.lbAdr2.Size = new System.Drawing.Size(124, 28);
            this.lbAdr2.TabIndex = 1;
            this.lbAdr2.Text = "Adresse";
            this.lbDB2.Location = new System.Drawing.Point(8, 13);
            this.lbDB2.Name = "lbDB2";
            this.lbDB2.Size = new System.Drawing.Size(124, 28);
            this.lbDB2.TabIndex = 0;
            this.lbDB2.Text = "Datenbank";
            this.gbSum.Controls.Add(this.taSummary);
            this.gbSum.Location = new System.Drawing.Point(10, 547);
            this.gbSum.Name = "gbSum";
            this.gbSum.Size = new System.Drawing.Size(506, 127);
            this.gbSum.TabIndex = 2;
            this.gbSum.TabStop = false;
            this.gbSum.Text = "Summary";
            this.taSummary.Location = new System.Drawing.Point(6, 18);
            this.taSummary.Multiline = true;
            this.taSummary.Name = "taSummary";
            this.taSummary.Size = new System.Drawing.Size(494, 99);
            this.taSummary.TabIndex = 0;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 680);
            this.Controls.Add(this.gbSum);
            this.Controls.Add(this.gbDB2);
            this.Controls.Add(this.gbDB1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gbDB1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.gbDB2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.gbSum.ResumeLayout(false);
            this.gbSum.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox gbSum;
        private System.Windows.Forms.GroupBox gbDB2;
        private System.Windows.Forms.GroupBox gbDB1;
        private System.Windows.Forms.Label lbDB1;
        private System.Windows.Forms.Label lbAdr1;
        private System.Windows.Forms.Label lbUser1;
        private System.Windows.Forms.Label lbPw1;
        private System.Windows.Forms.Label lbPort2;
        private System.Windows.Forms.Label lbPort1;
        private System.Windows.Forms.Label lbDB2;
        private System.Windows.Forms.Label lbAdr2;
        private System.Windows.Forms.Label lbUser2;
        private System.Windows.Forms.Label lbPw2;
        private System.Windows.Forms.TextBox tbPw2;
        private System.Windows.Forms.TextBox tbAdr2;
        private System.Windows.Forms.TextBox tbUser2;
        private System.Windows.Forms.ComboBox cbDB2;
        private System.Windows.Forms.TextBox tbPw1;
        private System.Windows.Forms.TextBox tbAdr1;
        private System.Windows.Forms.TextBox tbUser1;
        private System.Windows.Forms.ComboBox cbDB1;
        private System.Windows.Forms.TextBox taSummary;
        private System.Windows.Forms.Button bt1;
        private System.Windows.Forms.Button bt2;
        private System.Windows.Forms.CheckBox cb2;
        private System.Windows.Forms.CheckBox cb1;
        public void test()
        {
            cbDB1.Items.Add(DBType.CubridDB+"");
            cbDB2.Items.Add(DBType.OracleSQL+"");
            cbDB2.Items.Add(DBType.MSSQL+"");
            cbDB2.SelectedIndexChanged+=defaultValues;
            if (!cb1.Checked)
            {
                tbPw1.PasswordChar = '*';
                //MessageBox.Show("help");
            }
            if (!cb2.Checked)
            {
                tbPw2.PasswordChar = '*';
            }
            cbDB1.SelectedIndex = cbDB1.FindStringExact(DBType.CubridDB + "");
            cbDB2.SelectedIndex = cbDB2.FindStringExact(DBType.OracleSQL + "");
        }

        public void callDB1()
        {
            String compare = cbDB1.GetItemText(cbDB1.SelectedItem);
            int type = 0;
            if (compare.Equals(DBType.CubridDB+""))
            {
                
                type = 1;
            }
            Class1.start(tbAdr1.Text, tbPort1.Text, tbdatabasename1.Text, tbUser1.Text, tbPw1.Text,type);

            //Class1.start();
        }

            public void callDB2()
        {
            String compare2 = cbDB2.GetItemText(cbDB2.SelectedItem);
            int type = 0;
            if (compare2.Equals(DBType.OracleSQL+""))
            {
                type = 3;
            }

            if (compare2.Equals(DBType.MSSQL+""))
            {
                type = 2;
            }
            Class1.start(tbAdr2.Text, tbPort2.Text, tbdatabasename2.Text, tbUser2.Text, tbPw2.Text,type);

        }

        public void closeConnections()
        {
            Class1.closeConnection();
        }

        public void migrate()
        {
            Class1 cs=new Class1();
            cs.migrate();
           taSummary.Text=cs.getErfolgreich();
           
        }

        private System.Windows.Forms.TextBox tbPort1;
        private System.Windows.Forms.TextBox tbPort2;
        private System.Windows.Forms.TextBox tbdatabasename1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbdatabasename2;
        private System.Windows.Forms.Label lb1;
        private System.Windows.Forms.Button daten;
    }
    
    
}

