using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Cip.LabDemo.DialogBoxes
{
    public partial class cipFormSharpness : Form
    {
        #region Designer



        #endregion Designer

        private bool diag;
        private bool neg;
        private Cip.Filters.ColorSpaceMode mode;
        
        public cipFormSharpness()
        {
            InitializeComponent();
        }
        public cipFormSharpness(System.Globalization.CultureInfo selectedCulture)
        {
            Thread.CurrentThread.CurrentUICulture = selectedCulture;
            InitializeComponent();
        }
        public bool IsDiag()
        {
            return this.diag;
        }
        public bool IsNegative()
        {
            return this.neg;
        }
        public Cip.Filters.ColorSpaceMode GetMode()
        {
            return this.mode;
        }

        private void radioButtonRGB_Click(object sender, EventArgs e)
        {
            radioButtonRGB.Checked = true;
            radioButtonHSI.Checked = false;
        }
        private void radioButtonHSI_Click(object sender, EventArgs e)
        {
            radioButtonRGB.Checked = false;
            radioButtonHSI.Checked = true;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.diag = checkBoxDiag.Checked;
            this.neg = checkBoxNegative.Checked;
            if (radioButtonRGB.Checked)
                this.mode = Cip.Filters.ColorSpaceMode.RGB;
            else
                this.mode = Cip.Filters.ColorSpaceMode.HSI;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}