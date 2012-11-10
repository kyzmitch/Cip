namespace Cip.LabDemo.DialogBoxes
{
    partial class cipFormSmoothing
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(cipFormSmoothing));
            this.trackBarRadius = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxRadius = new System.Windows.Forms.TextBox();
            this.groupBoxMethod = new System.Windows.Forms.GroupBox();
            this.radioButtonGauss = new System.Windows.Forms.RadioButton();
            this.radioButtonBlur = new System.Windows.Forms.RadioButton();
            this.radioButtonSmoothing = new System.Windows.Forms.RadioButton();
            this.groupBoxColorMode = new System.Windows.Forms.GroupBox();
            this.radioButtonHSI = new System.Windows.Forms.RadioButton();
            this.radioButtonRGB = new System.Windows.Forms.RadioButton();
            this.groupBoxSigma = new System.Windows.Forms.GroupBox();
            this.textBoxSigma = new System.Windows.Forms.TextBox();
            this.pBoxPreview = new System.Windows.Forms.PictureBox();
            this.buttonOk = new Cip.Components.Buttons.CipOkButton(this.components);
            this.buttonCancel = new Cip.Components.Buttons.CipCancelButton(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRadius)).BeginInit();
            this.groupBoxMethod.SuspendLayout();
            this.groupBoxColorMode.SuspendLayout();
            this.groupBoxSigma.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBoxPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBarRadius
            // 
            this.trackBarRadius.LargeChange = 10;
            resources.ApplyResources(this.trackBarRadius, "trackBarRadius");
            this.trackBarRadius.Minimum = 1;
            this.trackBarRadius.Name = "trackBarRadius";
            this.trackBarRadius.TickFrequency = 600;
            this.trackBarRadius.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBarRadius.Value = 1;
            this.trackBarRadius.Scroll += new System.EventHandler(this.trackBarRadius_Scroll);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // textBoxRadius
            // 
            resources.ApplyResources(this.textBoxRadius, "textBoxRadius");
            this.textBoxRadius.Name = "textBoxRadius";
            // 
            // groupBoxMethod
            // 
            this.groupBoxMethod.Controls.Add(this.radioButtonGauss);
            this.groupBoxMethod.Controls.Add(this.radioButtonBlur);
            this.groupBoxMethod.Controls.Add(this.radioButtonSmoothing);
            resources.ApplyResources(this.groupBoxMethod, "groupBoxMethod");
            this.groupBoxMethod.Name = "groupBoxMethod";
            this.groupBoxMethod.TabStop = false;
            // 
            // radioButtonGauss
            // 
            resources.ApplyResources(this.radioButtonGauss, "radioButtonGauss");
            this.radioButtonGauss.Name = "radioButtonGauss";
            this.radioButtonGauss.UseVisualStyleBackColor = true;
            this.radioButtonGauss.Click += new System.EventHandler(this.radioButtonGauss_Click);
            // 
            // radioButtonBlur
            // 
            resources.ApplyResources(this.radioButtonBlur, "radioButtonBlur");
            this.radioButtonBlur.Name = "radioButtonBlur";
            this.radioButtonBlur.UseVisualStyleBackColor = true;
            this.radioButtonBlur.Click += new System.EventHandler(this.radioButtonBlur_Click);
            // 
            // radioButtonSmoothing
            // 
            resources.ApplyResources(this.radioButtonSmoothing, "radioButtonSmoothing");
            this.radioButtonSmoothing.Checked = true;
            this.radioButtonSmoothing.Name = "radioButtonSmoothing";
            this.radioButtonSmoothing.TabStop = true;
            this.radioButtonSmoothing.UseVisualStyleBackColor = true;
            this.radioButtonSmoothing.Click += new System.EventHandler(this.radioButtonSmoothing_Click);
            // 
            // groupBoxColorMode
            // 
            this.groupBoxColorMode.Controls.Add(this.radioButtonHSI);
            this.groupBoxColorMode.Controls.Add(this.radioButtonRGB);
            resources.ApplyResources(this.groupBoxColorMode, "groupBoxColorMode");
            this.groupBoxColorMode.Name = "groupBoxColorMode";
            this.groupBoxColorMode.TabStop = false;
            // 
            // radioButtonHSI
            // 
            resources.ApplyResources(this.radioButtonHSI, "radioButtonHSI");
            this.radioButtonHSI.Name = "radioButtonHSI";
            this.radioButtonHSI.UseVisualStyleBackColor = true;
            // 
            // radioButtonRGB
            // 
            resources.ApplyResources(this.radioButtonRGB, "radioButtonRGB");
            this.radioButtonRGB.Checked = true;
            this.radioButtonRGB.Name = "radioButtonRGB";
            this.radioButtonRGB.TabStop = true;
            this.radioButtonRGB.UseVisualStyleBackColor = true;
            // 
            // groupBoxSigma
            // 
            this.groupBoxSigma.Controls.Add(this.textBoxSigma);
            resources.ApplyResources(this.groupBoxSigma, "groupBoxSigma");
            this.groupBoxSigma.Name = "groupBoxSigma";
            this.groupBoxSigma.TabStop = false;
            // 
            // textBoxSigma
            // 
            resources.ApplyResources(this.textBoxSigma, "textBoxSigma");
            this.textBoxSigma.Name = "textBoxSigma";
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
            // cipFormSmoothing
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.pBoxPreview);
            this.Controls.Add(this.groupBoxSigma);
            this.Controls.Add(this.groupBoxColorMode);
            this.Controls.Add(this.groupBoxMethod);
            this.Controls.Add(this.textBoxRadius);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBarRadius);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "cipFormSmoothing";
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRadius)).EndInit();
            this.groupBoxMethod.ResumeLayout(false);
            this.groupBoxMethod.PerformLayout();
            this.groupBoxColorMode.ResumeLayout(false);
            this.groupBoxColorMode.PerformLayout();
            this.groupBoxSigma.ResumeLayout(false);
            this.groupBoxSigma.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBoxPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBarRadius;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxRadius;
        private System.Windows.Forms.GroupBox groupBoxMethod;
        private System.Windows.Forms.RadioButton radioButtonBlur;
        private System.Windows.Forms.RadioButton radioButtonSmoothing;
        private System.Windows.Forms.RadioButton radioButtonGauss;
        private System.Windows.Forms.GroupBox groupBoxColorMode;
        private System.Windows.Forms.RadioButton radioButtonHSI;
        private System.Windows.Forms.RadioButton radioButtonRGB;
        private System.Windows.Forms.GroupBox groupBoxSigma;
        private System.Windows.Forms.TextBox textBoxSigma;
        private System.Windows.Forms.PictureBox pBoxPreview;
        private Cip.Components.Buttons.CipOkButton buttonOk;
        private Cip.Components.Buttons.CipCancelButton buttonCancel;

    }
}