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
using System.Drawing.Drawing2D;

namespace Cip.LabDemo
{
    public class ImageOutForm : Form
    {
        /// <summary>
        /// Target picture box.
        /// </summary>
        private PictureBox picBoxOut;

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
            this.SuspendLayout();
            // 
            // ImageOutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(273, 228);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ImageOutForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Zoom";
            this.Load += new System.EventHandler(this.ImageOutForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
        
        public ImageOutForm()
        {
            InitializeComponent();
        }
        public ImageOutForm(Bitmap sourceBitmap)
        {
            InitializeComponent();
            Rectangle ScreenRect = Screen.PrimaryScreen.Bounds;

            this.picBoxOut = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxOut)).BeginInit();
            this.SuspendLayout();
            this.picBoxOut.Image = sourceBitmap;
            this.picBoxOut.Location = new System.Drawing.Point(0, 0);
            this.picBoxOut.BorderStyle = BorderStyle.FixedSingle;
            this.picBoxOut.MaximumSize = new Size(ScreenRect.Width, ScreenRect.Height);
            this.picBoxOut.SizeMode = PictureBoxSizeMode.Zoom;
            this.picBoxOut.Name = "picBoxOut";
            this.picBoxOut.Size = new Size(ScreenRect.Width, ScreenRect.Height);
            this.picBoxOut.Dock = DockStyle.Fill;
            this.picBoxOut.Click += new EventHandler(picBoxOut_Click);
            this.Controls.Add(this.picBoxOut);
            //full-screen mode
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            //this.Size = new Size(ScreenRect.Width, ScreenRect.Height);
            this.BackColor = Color.Black;
        }
        private void picBoxOut_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void SetImage(Bitmap bmp)
        {
            if (bmp != null) picBoxOut.Image = bmp;
        }

        private void ImageOutForm_Load(object sender, EventArgs e)
        {
            /*string text = this.picBoxOut.Image.Width.ToString() + "x" + this.picBoxOut.Image.Height.ToString();
            Font font = new Font("Arial", 8, FontStyle.Bold);
            Rectangle rectOld = Screen.PrimaryScreen.Bounds;
            using(Graphics graphics = Graphics.FromImage(this.picBoxOut.Image))
            {
                //painting text
                SolidBrush brushStr = new SolidBrush(Color.GreenYellow);
                graphics.DrawString(text, font, brushStr, rectOld);
            }*/
            
        }

        
        
    }
}