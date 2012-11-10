namespace Cip.LabDemo.DialogBoxes
{
    partial class cipFormMatrixFilter
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(cipFormMatrixFilter));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataMatrix = new System.Windows.Forms.DataGridView();
            this.textBoxDivisor = new System.Windows.Forms.TextBox();
            this.labelDivisor = new System.Windows.Forms.Label();
            this.upDownDimension = new System.Windows.Forms.NumericUpDown();
            this.labelDimension = new System.Windows.Forms.Label();
            this.labelBias = new System.Windows.Forms.Label();
            this.textBoxBias = new System.Windows.Forms.TextBox();
            this.checkBoxDivisor = new System.Windows.Forms.CheckBox();
            this.checkBoxBias = new System.Windows.Forms.CheckBox();
            this.comboBoxFilters = new System.Windows.Forms.ComboBox();
            this.cipBevel1 = new Cip.Components.CipBevel();
            this.divisorCalcButton = new Cip.Components.Buttons.CipButton(this.components);
            this.cipBevel2 = new Cip.Components.CipBevel();
            this.cancelButton = new Cip.Components.Buttons.CipCancelButton(this.components);
            this.okButton = new Cip.Components.Buttons.CipOkButton(this.components);
            this.filterPreviewBox1 = new Cip.Components.FilterPreviewBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataMatrix)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownDimension)).BeginInit();
            this.SuspendLayout();
            // 
            // dataMatrix
            // 
            this.dataMatrix.AllowUserToAddRows = false;
            this.dataMatrix.AllowUserToDeleteRows = false;
            this.dataMatrix.AllowUserToResizeColumns = false;
            this.dataMatrix.AllowUserToResizeRows = false;
            this.dataMatrix.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataMatrix.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataMatrix.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataMatrix.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.dataMatrix, "dataMatrix");
            this.dataMatrix.Name = "dataMatrix";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataMatrix.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataMatrix.SelectionChanged += new System.EventHandler(this.dataMatrix_SelectionChanged);
            // 
            // textBoxDivisor
            // 
            resources.ApplyResources(this.textBoxDivisor, "textBoxDivisor");
            this.textBoxDivisor.Name = "textBoxDivisor";
            // 
            // labelDivisor
            // 
            resources.ApplyResources(this.labelDivisor, "labelDivisor");
            this.labelDivisor.Name = "labelDivisor";
            // 
            // upDownDimension
            // 
            resources.ApplyResources(this.upDownDimension, "upDownDimension");
            this.upDownDimension.Name = "upDownDimension";
            this.upDownDimension.ValueChanged += new System.EventHandler(this.upDownDimension_ValueChanged);
            // 
            // labelDimension
            // 
            resources.ApplyResources(this.labelDimension, "labelDimension");
            this.labelDimension.Name = "labelDimension";
            // 
            // labelBias
            // 
            resources.ApplyResources(this.labelBias, "labelBias");
            this.labelBias.Name = "labelBias";
            // 
            // textBoxBias
            // 
            resources.ApplyResources(this.textBoxBias, "textBoxBias");
            this.textBoxBias.Name = "textBoxBias";
            // 
            // checkBoxDivisor
            // 
            resources.ApplyResources(this.checkBoxDivisor, "checkBoxDivisor");
            this.checkBoxDivisor.Name = "checkBoxDivisor";
            this.checkBoxDivisor.UseVisualStyleBackColor = true;
            this.checkBoxDivisor.CheckedChanged += new System.EventHandler(this.checkBoxDivisor_CheckedChanged);
            // 
            // checkBoxBias
            // 
            resources.ApplyResources(this.checkBoxBias, "checkBoxBias");
            this.checkBoxBias.Name = "checkBoxBias";
            this.checkBoxBias.UseVisualStyleBackColor = true;
            this.checkBoxBias.CheckedChanged += new System.EventHandler(this.checkBoxBias_CheckedChanged);
            // 
            // comboBoxFilters
            // 
            this.comboBoxFilters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFilters.FormattingEnabled = true;
            this.comboBoxFilters.Items.AddRange(new object[] {
            resources.GetString("comboBoxFilters.Items"),
            resources.GetString("comboBoxFilters.Items1"),
            resources.GetString("comboBoxFilters.Items2"),
            resources.GetString("comboBoxFilters.Items3"),
            resources.GetString("comboBoxFilters.Items4"),
            resources.GetString("comboBoxFilters.Items5"),
            resources.GetString("comboBoxFilters.Items6"),
            resources.GetString("comboBoxFilters.Items7"),
            resources.GetString("comboBoxFilters.Items8"),
            resources.GetString("comboBoxFilters.Items9")});
            resources.ApplyResources(this.comboBoxFilters, "comboBoxFilters");
            this.comboBoxFilters.Name = "comboBoxFilters";
            this.comboBoxFilters.SelectedIndexChanged += new System.EventHandler(this.comboBoxFilters_SelectedIndexChanged);
            // 
            // cipBevel1
            // 
            resources.ApplyResources(this.cipBevel1, "cipBevel1");
            this.cipBevel1.Name = "cipBevel1";
            // 
            // divisorCalcButton
            // 
            this.divisorCalcButton.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
            this.divisorCalcButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            resources.ApplyResources(this.divisorCalcButton, "divisorCalcButton");
            this.divisorCalcButton.Name = "divisorCalcButton";
            this.divisorCalcButton.UseVisualStyleBackColor = true;
            this.divisorCalcButton.Click += new System.EventHandler(this.divisorCalcButton_Click);
            // 
            // cipBevel2
            // 
            resources.ApplyResources(this.cipBevel2, "cipBevel2");
            this.cipBevel2.Name = "cipBevel2";
            // 
            // cancelButton
            // 
            this.cancelButton.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.cancelButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            resources.ApplyResources(this.cancelButton, "cancelButton");
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // okButton
            // 
            this.okButton.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.okButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            resources.ApplyResources(this.okButton, "okButton");
            this.okButton.Name = "okButton";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // filterPreviewBox1
            // 
            resources.ApplyResources(this.filterPreviewBox1, "filterPreviewBox1");
            this.filterPreviewBox1.Name = "filterPreviewBox1";
            // 
            // cipFormMatrixFilter
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.filterPreviewBox1);
            this.Controls.Add(this.cipBevel1);
            this.Controls.Add(this.comboBoxFilters);
            this.Controls.Add(this.checkBoxBias);
            this.Controls.Add(this.checkBoxDivisor);
            this.Controls.Add(this.divisorCalcButton);
            this.Controls.Add(this.textBoxBias);
            this.Controls.Add(this.labelBias);
            this.Controls.Add(this.labelDimension);
            this.Controls.Add(this.upDownDimension);
            this.Controls.Add(this.cipBevel2);
            this.Controls.Add(this.labelDivisor);
            this.Controls.Add(this.textBoxDivisor);
            this.Controls.Add(this.dataMatrix);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "cipFormMatrixFilter";
            ((System.ComponentModel.ISupportInitialize)(this.dataMatrix)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownDimension)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Cip.Components.Buttons.CipOkButton okButton;
        private Cip.Components.Buttons.CipCancelButton cancelButton;
        private System.Windows.Forms.DataGridView dataMatrix;
        private System.Windows.Forms.TextBox textBoxDivisor;
        private System.Windows.Forms.Label labelDivisor;
        private Cip.Components.CipBevel cipBevel2;
        private System.Windows.Forms.NumericUpDown upDownDimension;
        private System.Windows.Forms.Label labelDimension;
        private System.Windows.Forms.Label labelBias;
        private System.Windows.Forms.TextBox textBoxBias;
        private Cip.Components.Buttons.CipButton divisorCalcButton;
        private System.Windows.Forms.CheckBox checkBoxDivisor;
        private System.Windows.Forms.CheckBox checkBoxBias;
        private System.Windows.Forms.ComboBox comboBoxFilters;
        private Cip.Components.CipBevel cipBevel1;
        private Cip.Components.FilterPreviewBox filterPreviewBox1;
    }
}