namespace Cip.LabDemo.DialogBoxes
{
    partial class cipFormHistogram
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(cipFormHistogram));
            this.groupBoxThreshold = new System.Windows.Forms.GroupBox();
            this.trackBarThreshold = new System.Windows.Forms.TrackBar();
            this.textBoxLevel = new System.Windows.Forms.TextBox();
            this.groupBoxStretch = new System.Windows.Forms.GroupBox();
            this.radioButtonIndependentChannels = new System.Windows.Forms.RadioButton();
            this.radioButtonLinkedChannels = new System.Windows.Forms.RadioButton();
            this.radioButtonLuminance = new System.Windows.Forms.RadioButton();
            this.radioButtonHistNorm = new System.Windows.Forms.RadioButton();
            this.radioButtonHistEqualization = new System.Windows.Forms.RadioButton();
            this.btnOk = new Cip.Components.Buttons.CipOkButton(this.components);
            this.cipCancelButton1 = new Cip.Components.Buttons.CipCancelButton(this.components);
            this.pBoxPreview = new System.Windows.Forms.PictureBox();
            this.groupBoxThreshold.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarThreshold)).BeginInit();
            this.groupBoxStretch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBoxPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxThreshold
            // 
            this.groupBoxThreshold.Controls.Add(this.trackBarThreshold);
            resources.ApplyResources(this.groupBoxThreshold, "groupBoxThreshold");
            this.groupBoxThreshold.Name = "groupBoxThreshold";
            this.groupBoxThreshold.TabStop = false;
            // 
            // trackBarThreshold
            // 
            this.trackBarThreshold.LargeChange = 1;
            resources.ApplyResources(this.trackBarThreshold, "trackBarThreshold");
            this.trackBarThreshold.Maximum = 1000;
            this.trackBarThreshold.Name = "trackBarThreshold";
            this.trackBarThreshold.TickFrequency = 100;
            this.trackBarThreshold.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBarThreshold.Value = 5;
            this.trackBarThreshold.Scroll += new System.EventHandler(this.trackBarThreshold_Scroll);
            // 
            // textBoxLevel
            // 
            resources.ApplyResources(this.textBoxLevel, "textBoxLevel");
            this.textBoxLevel.Name = "textBoxLevel";
            // 
            // groupBoxStretch
            // 
            this.groupBoxStretch.Controls.Add(this.radioButtonIndependentChannels);
            this.groupBoxStretch.Controls.Add(this.radioButtonLinkedChannels);
            this.groupBoxStretch.Controls.Add(this.radioButtonLuminance);
            resources.ApplyResources(this.groupBoxStretch, "groupBoxStretch");
            this.groupBoxStretch.Name = "groupBoxStretch";
            this.groupBoxStretch.TabStop = false;
            // 
            // radioButtonIndependentChannels
            // 
            resources.ApplyResources(this.radioButtonIndependentChannels, "radioButtonIndependentChannels");
            this.radioButtonIndependentChannels.Checked = true;
            this.radioButtonIndependentChannels.Name = "radioButtonIndependentChannels";
            this.radioButtonIndependentChannels.TabStop = true;
            this.radioButtonIndependentChannels.UseVisualStyleBackColor = true;
            this.radioButtonIndependentChannels.Click += new System.EventHandler(this.radioButtonIndependentChannels_Click);
            // 
            // radioButtonLinkedChannels
            // 
            resources.ApplyResources(this.radioButtonLinkedChannels, "radioButtonLinkedChannels");
            this.radioButtonLinkedChannels.Name = "radioButtonLinkedChannels";
            this.radioButtonLinkedChannels.UseVisualStyleBackColor = true;
            this.radioButtonLinkedChannels.Click += new System.EventHandler(this.radioButtonLinkedChannels_Click);
            // 
            // radioButtonLuminance
            // 
            resources.ApplyResources(this.radioButtonLuminance, "radioButtonLuminance");
            this.radioButtonLuminance.Name = "radioButtonLuminance";
            this.radioButtonLuminance.UseVisualStyleBackColor = true;
            this.radioButtonLuminance.Click += new System.EventHandler(this.radioButtonLuminance_Click);
            // 
            // radioButtonHistNorm
            // 
            resources.ApplyResources(this.radioButtonHistNorm, "radioButtonHistNorm");
            this.radioButtonHistNorm.Name = "radioButtonHistNorm";
            this.radioButtonHistNorm.UseVisualStyleBackColor = true;
            this.radioButtonHistNorm.Click += new System.EventHandler(this.radioButtonHistNorm_Click);
            // 
            // radioButtonHistEqualization
            // 
            resources.ApplyResources(this.radioButtonHistEqualization, "radioButtonHistEqualization");
            this.radioButtonHistEqualization.Name = "radioButtonHistEqualization";
            this.radioButtonHistEqualization.UseVisualStyleBackColor = true;
            this.radioButtonHistEqualization.Click += new System.EventHandler(this.radioButtonHistEqualization_Click);
            // 
            // btnOk
            // 
            this.btnOk.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnOk.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // cipCancelButton1
            // 
            this.cipCancelButton1.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.cipCancelButton1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            resources.ApplyResources(this.cipCancelButton1, "cipCancelButton1");
            this.cipCancelButton1.Name = "cipCancelButton1";
            this.cipCancelButton1.UseVisualStyleBackColor = true;
            this.cipCancelButton1.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pBoxPreview
            // 
            this.pBoxPreview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.pBoxPreview, "pBoxPreview");
            this.pBoxPreview.Name = "pBoxPreview";
            this.pBoxPreview.TabStop = false;
            // 
            // cipFormHistogram
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pBoxPreview);
            this.Controls.Add(this.cipCancelButton1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.radioButtonHistNorm);
            this.Controls.Add(this.textBoxLevel);
            this.Controls.Add(this.radioButtonHistEqualization);
            this.Controls.Add(this.groupBoxStretch);
            this.Controls.Add(this.groupBoxThreshold);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "cipFormHistogram";
            this.groupBoxThreshold.ResumeLayout(false);
            this.groupBoxThreshold.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarThreshold)).EndInit();
            this.groupBoxStretch.ResumeLayout(false);
            this.groupBoxStretch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBoxPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxThreshold;
        private System.Windows.Forms.TrackBar trackBarThreshold;
        private System.Windows.Forms.TextBox textBoxLevel;
        private System.Windows.Forms.GroupBox groupBoxStretch;
        private System.Windows.Forms.RadioButton radioButtonIndependentChannels;
        private System.Windows.Forms.RadioButton radioButtonLinkedChannels;
        private System.Windows.Forms.RadioButton radioButtonLuminance;
        private System.Windows.Forms.RadioButton radioButtonHistNorm;
        private System.Windows.Forms.RadioButton radioButtonHistEqualization;
        private Cip.Components.Buttons.CipOkButton btnOk;
        private Cip.Components.Buttons.CipCancelButton cipCancelButton1;
        private System.Windows.Forms.PictureBox pBoxPreview;
    }
}