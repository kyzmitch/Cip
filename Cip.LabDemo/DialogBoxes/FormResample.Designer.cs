namespace Cip.LabDemo.DialogBoxes
{
    partial class cipFormResample
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(cipFormResample));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelNewSizeVar = new System.Windows.Forms.Label();
            this.labelOriginalSizeVar = new System.Windows.Forms.Label();
            this.labelNewSize = new System.Windows.Forms.Label();
            this.labelOriginalSize = new System.Windows.Forms.Label();
            this.groupBoxPixels = new System.Windows.Forms.GroupBox();
            this.numericUpDownHeight = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownWidth = new System.Windows.Forms.NumericUpDown();
            this.labelPixelsHeight = new System.Windows.Forms.Label();
            this.labelPixelsWidth = new System.Windows.Forms.Label();
            this.radioButtonPixels = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBoxStandartSizes = new System.Windows.Forms.ComboBox();
            this.labelStandartWxh = new System.Windows.Forms.Label();
            this.radioButtonStandart = new System.Windows.Forms.RadioButton();
            this.cipOk = new Cip.Components.Buttons.CipOkButton(this.components);
            this.cipCancel = new Cip.Components.Buttons.CipCancelButton(this.components);
            this.comboBoxFilter = new System.Windows.Forms.ComboBox();
            this.labelFilter = new System.Windows.Forms.Label();
            this.checkBoxAspectRatio = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBoxSizeFactor = new System.Windows.Forms.TextBox();
            this.radioButtonSizeFactor = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBoxPixels.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidth)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelNewSizeVar);
            this.groupBox1.Controls.Add(this.labelOriginalSizeVar);
            this.groupBox1.Controls.Add(this.labelNewSize);
            this.groupBox1.Controls.Add(this.labelOriginalSize);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // labelNewSizeVar
            // 
            resources.ApplyResources(this.labelNewSizeVar, "labelNewSizeVar");
            this.labelNewSizeVar.Name = "labelNewSizeVar";
            // 
            // labelOriginalSizeVar
            // 
            resources.ApplyResources(this.labelOriginalSizeVar, "labelOriginalSizeVar");
            this.labelOriginalSizeVar.Name = "labelOriginalSizeVar";
            // 
            // labelNewSize
            // 
            resources.ApplyResources(this.labelNewSize, "labelNewSize");
            this.labelNewSize.Name = "labelNewSize";
            // 
            // labelOriginalSize
            // 
            resources.ApplyResources(this.labelOriginalSize, "labelOriginalSize");
            this.labelOriginalSize.Name = "labelOriginalSize";
            // 
            // groupBoxPixels
            // 
            this.groupBoxPixels.Controls.Add(this.numericUpDownHeight);
            this.groupBoxPixels.Controls.Add(this.numericUpDownWidth);
            this.groupBoxPixels.Controls.Add(this.labelPixelsHeight);
            this.groupBoxPixels.Controls.Add(this.labelPixelsWidth);
            this.groupBoxPixels.Controls.Add(this.radioButtonPixels);
            resources.ApplyResources(this.groupBoxPixels, "groupBoxPixels");
            this.groupBoxPixels.Name = "groupBoxPixels";
            this.groupBoxPixels.TabStop = false;
            // 
            // numericUpDownHeight
            // 
            resources.ApplyResources(this.numericUpDownHeight, "numericUpDownHeight");
            this.numericUpDownHeight.Maximum = new decimal(new int[] {
            4000,
            0,
            0,
            0});
            this.numericUpDownHeight.Name = "numericUpDownHeight";
            this.numericUpDownHeight.ValueChanged += new System.EventHandler(this.numericUpDownHeight_ValueChanged);
            // 
            // numericUpDownWidth
            // 
            resources.ApplyResources(this.numericUpDownWidth, "numericUpDownWidth");
            this.numericUpDownWidth.Maximum = new decimal(new int[] {
            4000,
            0,
            0,
            0});
            this.numericUpDownWidth.Name = "numericUpDownWidth";
            this.numericUpDownWidth.ValueChanged += new System.EventHandler(this.numericUpDownWidth_ValueChanged);
            // 
            // labelPixelsHeight
            // 
            resources.ApplyResources(this.labelPixelsHeight, "labelPixelsHeight");
            this.labelPixelsHeight.Name = "labelPixelsHeight";
            // 
            // labelPixelsWidth
            // 
            resources.ApplyResources(this.labelPixelsWidth, "labelPixelsWidth");
            this.labelPixelsWidth.Name = "labelPixelsWidth";
            // 
            // radioButtonPixels
            // 
            resources.ApplyResources(this.radioButtonPixels, "radioButtonPixels");
            this.radioButtonPixels.Name = "radioButtonPixels";
            this.radioButtonPixels.UseVisualStyleBackColor = true;
            this.radioButtonPixels.Click += new System.EventHandler(this.radioButtonPixels_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBoxStandartSizes);
            this.groupBox2.Controls.Add(this.labelStandartWxh);
            this.groupBox2.Controls.Add(this.radioButtonStandart);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // comboBoxStandartSizes
            // 
            this.comboBoxStandartSizes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStandartSizes.Items.AddRange(new object[] {
            resources.GetString("comboBoxStandartSizes.Items"),
            resources.GetString("comboBoxStandartSizes.Items1"),
            resources.GetString("comboBoxStandartSizes.Items2"),
            resources.GetString("comboBoxStandartSizes.Items3"),
            resources.GetString("comboBoxStandartSizes.Items4"),
            resources.GetString("comboBoxStandartSizes.Items5"),
            resources.GetString("comboBoxStandartSizes.Items6"),
            resources.GetString("comboBoxStandartSizes.Items7"),
            resources.GetString("comboBoxStandartSizes.Items8"),
            resources.GetString("comboBoxStandartSizes.Items9"),
            resources.GetString("comboBoxStandartSizes.Items10"),
            resources.GetString("comboBoxStandartSizes.Items11")});
            resources.ApplyResources(this.comboBoxStandartSizes, "comboBoxStandartSizes");
            this.comboBoxStandartSizes.Name = "comboBoxStandartSizes";
            this.comboBoxStandartSizes.SelectedIndexChanged += new System.EventHandler(this.comboBoxStandartSizes_SelectedIndexChanged);
            // 
            // labelStandartWxh
            // 
            resources.ApplyResources(this.labelStandartWxh, "labelStandartWxh");
            this.labelStandartWxh.Name = "labelStandartWxh";
            // 
            // radioButtonStandart
            // 
            resources.ApplyResources(this.radioButtonStandart, "radioButtonStandart");
            this.radioButtonStandart.Checked = true;
            this.radioButtonStandart.Name = "radioButtonStandart";
            this.radioButtonStandart.TabStop = true;
            this.radioButtonStandart.UseVisualStyleBackColor = true;
            this.radioButtonStandart.Click += new System.EventHandler(this.radioButtonStandart_Click);
            // 
            // cipOk
            // 
            this.cipOk.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.cipOk.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            resources.ApplyResources(this.cipOk, "cipOk");
            this.cipOk.Name = "cipOk";
            this.cipOk.UseVisualStyleBackColor = true;
            this.cipOk.Click += new System.EventHandler(this.cipOk_Click);
            // 
            // cipCancel
            // 
            this.cipCancel.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.cipCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            resources.ApplyResources(this.cipCancel, "cipCancel");
            this.cipCancel.Name = "cipCancel";
            this.cipCancel.UseVisualStyleBackColor = true;
            this.cipCancel.Click += new System.EventHandler(this.cipCancel_Click);
            // 
            // comboBoxFilter
            // 
            this.comboBoxFilter.DisplayMember = "0";
            this.comboBoxFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFilter.Items.AddRange(new object[] {
            resources.GetString("comboBoxFilter.Items"),
            resources.GetString("comboBoxFilter.Items1"),
            resources.GetString("comboBoxFilter.Items2")});
            resources.ApplyResources(this.comboBoxFilter, "comboBoxFilter");
            this.comboBoxFilter.Name = "comboBoxFilter";
            this.comboBoxFilter.SelectedIndexChanged += new System.EventHandler(this.comboBoxFilter_SelectedIndexChanged);
            // 
            // labelFilter
            // 
            resources.ApplyResources(this.labelFilter, "labelFilter");
            this.labelFilter.Name = "labelFilter";
            // 
            // checkBoxAspectRatio
            // 
            resources.ApplyResources(this.checkBoxAspectRatio, "checkBoxAspectRatio");
            this.checkBoxAspectRatio.Checked = true;
            this.checkBoxAspectRatio.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAspectRatio.Name = "checkBoxAspectRatio";
            this.checkBoxAspectRatio.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBoxSizeFactor);
            this.groupBox3.Controls.Add(this.radioButtonSizeFactor);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // textBoxSizeFactor
            // 
            resources.ApplyResources(this.textBoxSizeFactor, "textBoxSizeFactor");
            this.textBoxSizeFactor.Name = "textBoxSizeFactor";
            this.textBoxSizeFactor.TextChanged += new System.EventHandler(this.textBoxSizeFactor_TextChanged);
            // 
            // radioButtonSizeFactor
            // 
            resources.ApplyResources(this.radioButtonSizeFactor, "radioButtonSizeFactor");
            this.radioButtonSizeFactor.Name = "radioButtonSizeFactor";
            this.radioButtonSizeFactor.UseVisualStyleBackColor = true;
            this.radioButtonSizeFactor.Click += new System.EventHandler(this.radioButtonSizeFactor_Click);
            // 
            // cipFormResample
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.checkBoxAspectRatio);
            this.Controls.Add(this.labelFilter);
            this.Controls.Add(this.comboBoxFilter);
            this.Controls.Add(this.cipCancel);
            this.Controls.Add(this.cipOk);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBoxPixels);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "cipFormResample";
            this.Load += new System.EventHandler(this.cipFormResample_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxPixels.ResumeLayout(false);
            this.groupBoxPixels.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidth)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelNewSize;
        private System.Windows.Forms.Label labelOriginalSize;
        private System.Windows.Forms.Label labelNewSizeVar;
        private System.Windows.Forms.Label labelOriginalSizeVar;
        private System.Windows.Forms.GroupBox groupBoxPixels;
        private System.Windows.Forms.Label labelPixelsHeight;
        private System.Windows.Forms.Label labelPixelsWidth;
        private System.Windows.Forms.RadioButton radioButtonPixels;
        private System.Windows.Forms.NumericUpDown numericUpDownHeight;
        private System.Windows.Forms.NumericUpDown numericUpDownWidth;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox comboBoxStandartSizes;
        private System.Windows.Forms.Label labelStandartWxh;
        private System.Windows.Forms.RadioButton radioButtonStandart;
        private Cip.Components.Buttons.CipOkButton cipOk;
        private Cip.Components.Buttons.CipCancelButton cipCancel;
        private System.Windows.Forms.ComboBox comboBoxFilter;
        private System.Windows.Forms.Label labelFilter;
        private System.Windows.Forms.CheckBox checkBoxAspectRatio;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioButtonSizeFactor;
        private System.Windows.Forms.TextBox textBoxSizeFactor;
    }
}