namespace Cip.LabDemo.DialogBoxes
{
    partial class cipFormIntensitySaturation
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(cipFormIntensitySaturation));
            this.trackBarIntensity = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxLevelIntensity = new System.Windows.Forms.TextBox();
            this.checkBoxRGB = new System.Windows.Forms.CheckBox();
            this.pBoxPreview = new System.Windows.Forms.PictureBox();
            this.buttonOk = new Cip.Components.Buttons.CipOkButton(this.components);
            this.buttonCancel = new Cip.Components.Buttons.CipCancelButton(this.components);
            this.trackBarSaturation = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxLevelSaturation = new System.Windows.Forms.TextBox();
            this.labelIntensity = new System.Windows.Forms.Label();
            this.labelSaturation = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarIntensity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBoxPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSaturation)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBarIntensity
            // 
            this.trackBarIntensity.LargeChange = 10;
            resources.ApplyResources(this.trackBarIntensity, "trackBarIntensity");
            this.trackBarIntensity.Maximum = 255;
            this.trackBarIntensity.Minimum = -255;
            this.trackBarIntensity.Name = "trackBarIntensity";
            this.trackBarIntensity.TickFrequency = 10;
            this.trackBarIntensity.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBarIntensity.Scroll += new System.EventHandler(this.trackBarIntensity_Scroll);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // textBoxLevelIntensity
            // 
            resources.ApplyResources(this.textBoxLevelIntensity, "textBoxLevelIntensity");
            this.textBoxLevelIntensity.Name = "textBoxLevelIntensity";
            this.textBoxLevelIntensity.ReadOnly = true;
            // 
            // checkBoxRGB
            // 
            resources.ApplyResources(this.checkBoxRGB, "checkBoxRGB");
            this.checkBoxRGB.Name = "checkBoxRGB";
            this.checkBoxRGB.UseVisualStyleBackColor = true;
            this.checkBoxRGB.CheckedChanged += new System.EventHandler(this.checkBoxRGB_CheckedChanged);
            // 
            // pBoxPreview
            // 
            this.pBoxPreview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.pBoxPreview, "pBoxPreview");
            this.pBoxPreview.Name = "pBoxPreview";
            this.pBoxPreview.TabStop = false;
            // 
            // buttonOk
            // 
            this.buttonOk.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.buttonOk.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.buttonCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // trackBarSaturation
            // 
            resources.ApplyResources(this.trackBarSaturation, "trackBarSaturation");
            this.trackBarSaturation.LargeChange = 10;
            this.trackBarSaturation.Maximum = 255;
            this.trackBarSaturation.Minimum = -255;
            this.trackBarSaturation.Name = "trackBarSaturation";
            this.trackBarSaturation.TickFrequency = 10;
            this.trackBarSaturation.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBarSaturation.Scroll += new System.EventHandler(this.trackBarSaturation_Scroll);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // textBoxLevelSaturation
            // 
            resources.ApplyResources(this.textBoxLevelSaturation, "textBoxLevelSaturation");
            this.textBoxLevelSaturation.Name = "textBoxLevelSaturation";
            this.textBoxLevelSaturation.ReadOnly = true;
            // 
            // labelIntensity
            // 
            resources.ApplyResources(this.labelIntensity, "labelIntensity");
            this.labelIntensity.Name = "labelIntensity";
            // 
            // labelSaturation
            // 
            resources.ApplyResources(this.labelSaturation, "labelSaturation");
            this.labelSaturation.Name = "labelSaturation";
            // 
            // cipFormIntensitySaturation
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelSaturation);
            this.Controls.Add(this.labelIntensity);
            this.Controls.Add(this.textBoxLevelSaturation);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.trackBarSaturation);
            this.Controls.Add(this.textBoxLevelIntensity);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.checkBoxRGB);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.pBoxPreview);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBarIntensity);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "cipFormIntensitySaturation";
            ((System.ComponentModel.ISupportInitialize)(this.trackBarIntensity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBoxPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSaturation)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBarIntensity;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxLevelIntensity;
        private System.Windows.Forms.CheckBox checkBoxRGB;
        private System.Windows.Forms.PictureBox pBoxPreview;
        private Cip.Components.Buttons.CipOkButton buttonOk;
        private Cip.Components.Buttons.CipCancelButton buttonCancel;
        private System.Windows.Forms.TrackBar trackBarSaturation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxLevelSaturation;
        private System.Windows.Forms.Label labelIntensity;
        private System.Windows.Forms.Label labelSaturation;
    }
}