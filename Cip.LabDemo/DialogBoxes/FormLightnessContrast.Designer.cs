namespace Cip.LabDemo.DialogBoxes
{
    partial class cipFormLightnessContrast
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(cipFormLightnessContrast));
            this.trackBarLightness = new System.Windows.Forms.TrackBar();
            this.labelLightness = new System.Windows.Forms.Label();
            this.textBoxLightness = new System.Windows.Forms.TextBox();
            this.trackBarContrast = new System.Windows.Forms.TrackBar();
            this.labelContrast = new System.Windows.Forms.Label();
            this.textBoxContrast = new System.Windows.Forms.TextBox();
            this.pBoxPreview = new System.Windows.Forms.PictureBox();
            this.bevel1 = new Cip.Components.CipBevel();
            this.buttonOk = new Cip.Components.Buttons.CipOkButton(this.components);
            this.buttonCancel = new Cip.Components.Buttons.CipCancelButton(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLightness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarContrast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBoxPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBarLightness
            // 
            this.trackBarLightness.LargeChange = 10;
            resources.ApplyResources(this.trackBarLightness, "trackBarLightness");
            this.trackBarLightness.Maximum = 255;
            this.trackBarLightness.Minimum = -255;
            this.trackBarLightness.Name = "trackBarLightness";
            this.trackBarLightness.TickFrequency = 600;
            this.trackBarLightness.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBarLightness.Scroll += new System.EventHandler(this.trackBarLightness_Scroll);
            // 
            // labelLightness
            // 
            resources.ApplyResources(this.labelLightness, "labelLightness");
            this.labelLightness.Name = "labelLightness";
            // 
            // textBoxLightness
            // 
            resources.ApplyResources(this.textBoxLightness, "textBoxLightness");
            this.textBoxLightness.Name = "textBoxLightness";
            // 
            // trackBarContrast
            // 
            this.trackBarContrast.LargeChange = 10;
            resources.ApplyResources(this.trackBarContrast, "trackBarContrast");
            this.trackBarContrast.Maximum = 100;
            this.trackBarContrast.Minimum = -100;
            this.trackBarContrast.Name = "trackBarContrast";
            this.trackBarContrast.TickFrequency = 600;
            this.trackBarContrast.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBarContrast.Scroll += new System.EventHandler(this.trackBarContrast_Scroll);
            // 
            // labelContrast
            // 
            resources.ApplyResources(this.labelContrast, "labelContrast");
            this.labelContrast.Name = "labelContrast";
            // 
            // textBoxContrast
            // 
            resources.ApplyResources(this.textBoxContrast, "textBoxContrast");
            this.textBoxContrast.Name = "textBoxContrast";
            // 
            // pBoxPreview
            // 
            this.pBoxPreview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.pBoxPreview, "pBoxPreview");
            this.pBoxPreview.Name = "pBoxPreview";
            this.pBoxPreview.TabStop = false;
            // 
            // bevel1
            // 
            resources.ApplyResources(this.bevel1, "bevel1");
            this.bevel1.Name = "bevel1";
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
            // cipFormLightnessContrast
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.bevel1);
            this.Controls.Add(this.pBoxPreview);
            this.Controls.Add(this.textBoxContrast);
            this.Controls.Add(this.labelContrast);
            this.Controls.Add(this.trackBarContrast);
            this.Controls.Add(this.textBoxLightness);
            this.Controls.Add(this.labelLightness);
            this.Controls.Add(this.trackBarLightness);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "cipFormLightnessContrast";
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLightness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarContrast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBoxPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBarLightness;
        private System.Windows.Forms.Label labelLightness;
        private System.Windows.Forms.TextBox textBoxLightness;
        private System.Windows.Forms.TrackBar trackBarContrast;
        private System.Windows.Forms.Label labelContrast;
        private System.Windows.Forms.TextBox textBoxContrast;
        private System.Windows.Forms.PictureBox pBoxPreview;
        private Cip.Components.CipBevel bevel1;
        private Cip.Components.Buttons.CipOkButton buttonOk;
        private Cip.Components.Buttons.CipCancelButton buttonCancel;
    }
}