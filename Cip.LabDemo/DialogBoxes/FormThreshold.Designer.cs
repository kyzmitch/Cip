namespace Cip.LabDemo.DialogBoxes
{
    partial class cipFormThreshold
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(cipFormThreshold));
            this.textBoxLevel = new System.Windows.Forms.TextBox();
            this.trackBarLevel = new System.Windows.Forms.TrackBar();
            this.pBoxPreview = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxManual = new System.Windows.Forms.CheckBox();
            this.groupBoxMode = new System.Windows.Forms.GroupBox();
            this.radioButtonTextSeparation = new System.Windows.Forms.RadioButton();
            this.checkBoxGlobalMode = new System.Windows.Forms.CheckBox();
            this.radioButtonIsoData = new System.Windows.Forms.RadioButton();
            this.radioButtonPotDiff = new System.Windows.Forms.RadioButton();
            this.radioButtonMaxEntropy = new System.Windows.Forms.RadioButton();
            this.radioButtonKi = new System.Windows.Forms.RadioButton();
            this.radioButtonOtsu = new System.Windows.Forms.RadioButton();
            this.radioButtonAuto = new System.Windows.Forms.RadioButton();
            this.btnCancel = new Cip.Components.Buttons.CipCancelButton(this.components);
            this.btnOk = new Cip.Components.Buttons.CipOkButton(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBoxPreview)).BeginInit();
            this.groupBoxMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxLevel
            // 
            resources.ApplyResources(this.textBoxLevel, "textBoxLevel");
            this.textBoxLevel.Name = "textBoxLevel";
            // 
            // trackBarLevel
            // 
            resources.ApplyResources(this.trackBarLevel, "trackBarLevel");
            this.trackBarLevel.Maximum = 255;
            this.trackBarLevel.Name = "trackBarLevel";
            this.trackBarLevel.TickFrequency = 5;
            this.trackBarLevel.Scroll += new System.EventHandler(this.trackBarLevel_Scroll);
            // 
            // pBoxPreview
            // 
            this.pBoxPreview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.pBoxPreview, "pBoxPreview");
            this.pBoxPreview.Name = "pBoxPreview";
            this.pBoxPreview.TabStop = false;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // checkBoxManual
            // 
            resources.ApplyResources(this.checkBoxManual, "checkBoxManual");
            this.checkBoxManual.Checked = true;
            this.checkBoxManual.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxManual.Name = "checkBoxManual";
            this.checkBoxManual.UseVisualStyleBackColor = true;
            this.checkBoxManual.CheckedChanged += new System.EventHandler(this.checkBoxManual_CheckedChanged);
            // 
            // groupBoxMode
            // 
            this.groupBoxMode.Controls.Add(this.radioButtonTextSeparation);
            this.groupBoxMode.Controls.Add(this.checkBoxGlobalMode);
            this.groupBoxMode.Controls.Add(this.radioButtonIsoData);
            this.groupBoxMode.Controls.Add(this.radioButtonPotDiff);
            this.groupBoxMode.Controls.Add(this.radioButtonMaxEntropy);
            this.groupBoxMode.Controls.Add(this.radioButtonKi);
            this.groupBoxMode.Controls.Add(this.radioButtonOtsu);
            this.groupBoxMode.Controls.Add(this.radioButtonAuto);
            this.groupBoxMode.Controls.Add(this.checkBoxManual);
            resources.ApplyResources(this.groupBoxMode, "groupBoxMode");
            this.groupBoxMode.Name = "groupBoxMode";
            this.groupBoxMode.TabStop = false;
            // 
            // radioButtonTextSeparation
            // 
            resources.ApplyResources(this.radioButtonTextSeparation, "radioButtonTextSeparation");
            this.radioButtonTextSeparation.Name = "radioButtonTextSeparation";
            this.radioButtonTextSeparation.TabStop = true;
            this.radioButtonTextSeparation.UseVisualStyleBackColor = true;
            this.radioButtonTextSeparation.Click += new System.EventHandler(this.radioButtonTextSeparation_Click);
            // 
            // checkBoxGlobalMode
            // 
            resources.ApplyResources(this.checkBoxGlobalMode, "checkBoxGlobalMode");
            this.checkBoxGlobalMode.Checked = true;
            this.checkBoxGlobalMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxGlobalMode.Name = "checkBoxGlobalMode";
            this.checkBoxGlobalMode.UseVisualStyleBackColor = true;
            // 
            // radioButtonIsoData
            // 
            resources.ApplyResources(this.radioButtonIsoData, "radioButtonIsoData");
            this.radioButtonIsoData.Name = "radioButtonIsoData";
            this.radioButtonIsoData.TabStop = true;
            this.radioButtonIsoData.UseVisualStyleBackColor = true;
            this.radioButtonIsoData.Click += new System.EventHandler(this.radioButtonAutoGlobal_Click);
            // 
            // radioButtonPotDiff
            // 
            resources.ApplyResources(this.radioButtonPotDiff, "radioButtonPotDiff");
            this.radioButtonPotDiff.Name = "radioButtonPotDiff";
            this.radioButtonPotDiff.TabStop = true;
            this.radioButtonPotDiff.UseVisualStyleBackColor = true;
            this.radioButtonPotDiff.Click += new System.EventHandler(this.radioButtonPotDiff_Click);
            // 
            // radioButtonMaxEntropy
            // 
            resources.ApplyResources(this.radioButtonMaxEntropy, "radioButtonMaxEntropy");
            this.radioButtonMaxEntropy.Name = "radioButtonMaxEntropy";
            this.radioButtonMaxEntropy.TabStop = true;
            this.radioButtonMaxEntropy.UseVisualStyleBackColor = true;
            this.radioButtonMaxEntropy.Click += new System.EventHandler(this.radioButtonMaxEntropy_Click);
            // 
            // radioButtonKi
            // 
            resources.ApplyResources(this.radioButtonKi, "radioButtonKi");
            this.radioButtonKi.Name = "radioButtonKi";
            this.radioButtonKi.TabStop = true;
            this.radioButtonKi.UseVisualStyleBackColor = true;
            this.radioButtonKi.Click += new System.EventHandler(this.radioButtonKi_Click);
            // 
            // radioButtonOtsu
            // 
            resources.ApplyResources(this.radioButtonOtsu, "radioButtonOtsu");
            this.radioButtonOtsu.Name = "radioButtonOtsu";
            this.radioButtonOtsu.TabStop = true;
            this.radioButtonOtsu.UseVisualStyleBackColor = true;
            this.radioButtonOtsu.Click += new System.EventHandler(this.radioButtonOtsu_Click);
            // 
            // radioButtonAuto
            // 
            resources.ApplyResources(this.radioButtonAuto, "radioButtonAuto");
            this.radioButtonAuto.Checked = true;
            this.radioButtonAuto.Name = "radioButtonAuto";
            this.radioButtonAuto.TabStop = true;
            this.radioButtonAuto.UseVisualStyleBackColor = true;
            this.radioButtonAuto.Click += new System.EventHandler(this.radioButtonAuto_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
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
            // cipFormThreshold
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxMode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxLevel);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.pBoxPreview);
            this.Controls.Add(this.trackBarLevel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "cipFormThreshold";
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBoxPreview)).EndInit();
            this.groupBoxMode.ResumeLayout(false);
            this.groupBoxMode.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxLevel;
        private System.Windows.Forms.TrackBar trackBarLevel;
        private System.Windows.Forms.PictureBox pBoxPreview;
        private Cip.Components.Buttons.CipOkButton btnOk;
        private Cip.Components.Buttons.CipCancelButton btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxManual;
        private System.Windows.Forms.GroupBox groupBoxMode;
        private System.Windows.Forms.RadioButton radioButtonOtsu;
        private System.Windows.Forms.RadioButton radioButtonAuto;
        private System.Windows.Forms.RadioButton radioButtonKi;
        private System.Windows.Forms.RadioButton radioButtonMaxEntropy;
        private System.Windows.Forms.RadioButton radioButtonPotDiff;
        private System.Windows.Forms.CheckBox checkBoxGlobalMode;
        private System.Windows.Forms.RadioButton radioButtonIsoData;
        private System.Windows.Forms.RadioButton radioButtonTextSeparation;
    }
}