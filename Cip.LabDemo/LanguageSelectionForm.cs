/////////////////////////////////////////////////////////////////////////////////
// Cip.LabDemo                                                       //
// Copyright (C) Andrew [kyzmitch] Ermoshin.                                   //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Cip.LabDemo
{
    public class LanguageSelectionForm : Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonRu = new System.Windows.Forms.Button();
            this.buttonEn = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonRu
            // 
            this.buttonRu.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonRu.Location = new System.Drawing.Point(0, 0);
            this.buttonRu.Name = "buttonRu";
            this.buttonRu.Size = new System.Drawing.Size(393, 28);
            this.buttonRu.TabIndex = 0;
            this.buttonRu.Text = "Russian";
            this.buttonRu.UseVisualStyleBackColor = true;
            this.buttonRu.Click += new System.EventHandler(this.buttonRu_Click);
            // 
            // buttonEn
            // 
            this.buttonEn.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonEn.Location = new System.Drawing.Point(0, 28);
            this.buttonEn.Name = "buttonEn";
            this.buttonEn.Size = new System.Drawing.Size(393, 28);
            this.buttonEn.TabIndex = 1;
            this.buttonEn.Text = "English";
            this.buttonEn.UseVisualStyleBackColor = true;
            this.buttonEn.Click += new System.EventHandler(this.buttonEn_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonExit.Location = new System.Drawing.Point(0, 56);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(393, 28);
            this.buttonExit.TabIndex = 2;
            this.buttonExit.Text = "Exit";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // LanguageSelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 84);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonEn);
            this.Controls.Add(this.buttonRu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "LanguageSelectionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Language Selection";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonRu;
        private System.Windows.Forms.Button buttonEn;
        private System.Windows.Forms.Button buttonExit;
        
        public LanguageSelectionForm()
        {
            InitializeComponent();
            
        }

        private void buttonRu_Click(object sender, EventArgs e)
        {
            System.Globalization.CultureInfo info = new System.Globalization.CultureInfo("ru-RU",true);
            MainForm mainForm = new MainForm(info);
            mainForm.Show();
            this.Hide();
        }

        private void buttonEn_Click(object sender, EventArgs e)
        {
            System.Globalization.CultureInfo info = new System.Globalization.CultureInfo("en-US", true);
            MainForm mainForm = new MainForm(info);
            mainForm.Show();
            this.Hide();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}