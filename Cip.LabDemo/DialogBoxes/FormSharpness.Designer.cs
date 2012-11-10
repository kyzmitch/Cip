namespace Cip.LabDemo.DialogBoxes
{
    partial class cipFormSharpness
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(cipFormSharpness));
            this.checkBoxDiag = new System.Windows.Forms.CheckBox();
            this.checkBoxNegative = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonHSI = new System.Windows.Forms.RadioButton();
            this.radioButtonRGB = new System.Windows.Forms.RadioButton();
            this.buttonOk = new Cip.Components.Buttons.CipOkButton(this.components);
            this.buttonCancel = new Cip.Components.Buttons.CipCancelButton(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBoxDiag
            // 
            resources.ApplyResources(this.checkBoxDiag, "checkBoxDiag");
            this.checkBoxDiag.Name = "checkBoxDiag";
            this.checkBoxDiag.UseVisualStyleBackColor = true;
            // 
            // checkBoxNegative
            // 
            resources.ApplyResources(this.checkBoxNegative, "checkBoxNegative");
            this.checkBoxNegative.Name = "checkBoxNegative";
            this.checkBoxNegative.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonHSI);
            this.groupBox1.Controls.Add(this.radioButtonRGB);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // radioButtonHSI
            // 
            resources.ApplyResources(this.radioButtonHSI, "radioButtonHSI");
            this.radioButtonHSI.Name = "radioButtonHSI";
            this.radioButtonHSI.UseVisualStyleBackColor = true;
            this.radioButtonHSI.Click += new System.EventHandler(this.radioButtonHSI_Click);
            // 
            // radioButtonRGB
            // 
            resources.ApplyResources(this.radioButtonRGB, "radioButtonRGB");
            this.radioButtonRGB.Checked = true;
            this.radioButtonRGB.Name = "radioButtonRGB";
            this.radioButtonRGB.TabStop = true;
            this.radioButtonRGB.UseVisualStyleBackColor = true;
            this.radioButtonRGB.Click += new System.EventHandler(this.radioButtonRGB_Click);
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
            // cipFormSharpness
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.checkBoxNegative);
            this.Controls.Add(this.checkBoxDiag);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "cipFormSharpness";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxDiag;
        private System.Windows.Forms.CheckBox checkBoxNegative;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonHSI;
        private System.Windows.Forms.RadioButton radioButtonRGB;
        private Cip.Components.Buttons.CipOkButton buttonOk;
        private Cip.Components.Buttons.CipCancelButton buttonCancel;
    }
}