using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using Cip;
using Cip.Foundations;
using Cip.Transformations;

namespace Cip.LabDemo.DialogBoxes
{
    public partial class cipFormResample : Form
    {
        #region Designer



        #endregion Designer

        private Cip.Transformations.CipSize nSize;
        private Cip.Transformations.CipInterpolationMode mode;
        private readonly Cip.Transformations.CipSize oldSize;


        public cipFormResample()
        {
            InitializeComponent();

            this.radioButtonSizeFactor.Checked = false;
            this.radioButtonPixels.Checked = false;
            this.radioButtonStandart.Checked = true;
            this.textBoxSizeFactor.Enabled = false;
            this.numericUpDownWidth.Enabled = false;
            this.numericUpDownHeight.Enabled = false;
            this.comboBoxStandartSizes.Enabled = true;
        }
        public cipFormResample(Image image)
        {
            InitializeComponent();

            this.radioButtonSizeFactor.Checked = false;
            this.radioButtonPixels.Checked = false;
            this.radioButtonStandart.Checked = true;
            this.textBoxSizeFactor.Enabled = false;
            this.numericUpDownWidth.Enabled = false;
            this.numericUpDownHeight.Enabled = false;
            this.comboBoxStandartSizes.Enabled = true;

            oldSize = new CipSize(image.Width, image.Height);
            nSize = new CipSize(oldSize);
            this.labelOriginalSizeVar.Text = oldSize.ToString();
            this.CipShowNewSize();
            this.CipChangeNumericSize();
            this.comboBoxStandartSizes.SelectedIndex = 0;
            this.comboBoxFilter.SelectedIndex = 1;
            this.mode = CipInterpolationMode.BicubicSpline;
            
        }
        public cipFormResample(Image image, System.Globalization.CultureInfo selectedCulture)
        {
            Thread.CurrentThread.CurrentUICulture = selectedCulture;
            InitializeComponent();

            this.radioButtonSizeFactor.Checked = false;
            this.radioButtonPixels.Checked = false;
            this.radioButtonStandart.Checked = true;
            this.textBoxSizeFactor.Enabled = false;
            this.numericUpDownWidth.Enabled = false;
            this.numericUpDownHeight.Enabled = false;
            this.comboBoxStandartSizes.Enabled = true;

            oldSize = new CipSize(image.Width, image.Height);
            nSize = new CipSize(oldSize);
            this.labelOriginalSizeVar.Text = oldSize.ToString();
            this.CipShowNewSize();
            this.CipChangeNumericSize();
            this.comboBoxStandartSizes.SelectedIndex = 0;
            this.comboBoxFilter.SelectedIndex = 1;
            this.mode = CipInterpolationMode.BicubicSpline;

        }
        private void cipFormResample_Load(object sender, EventArgs e)
        {
            this.radioButtonSizeFactor.Checked = true;
            this.radioButtonPixels.Checked = false;
            this.radioButtonStandart.Checked = false;
            this.textBoxSizeFactor.Enabled = true;
            this.numericUpDownWidth.Enabled = false;
            this.numericUpDownHeight.Enabled = false;
            this.comboBoxStandartSizes.Enabled = false;
        }

        #region Show methods
        private void CipShowNewSize()
        {
            this.labelNewSizeVar.Text = nSize.ToString();
        }
        private void CipChangeNumericSize()
        {
            this.numericUpDownWidth.Value = nSize.Width;
            this.numericUpDownHeight.Value = nSize.Height;
            this.labelNewSizeVar.Text = nSize.ToString();
        }
        #endregion Show methods

        #region Event handlers
        private void comboBoxStandartSizes_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.comboBoxStandartSizes.SelectedIndex)
            {
                case 0:
                    {
                        //screen size
                        Rectangle rect = Screen.PrimaryScreen.Bounds;
                        nSize.Width = rect.Width;
                        nSize.Height = rect.Height;
                        this.CipShowNewSize();
                        break;
                    }
                case 1:
                    { 
                        //160x120
                        nSize.Width = 160;
                        nSize.Height = 120;
                        this.CipShowNewSize();
                        break;
                    }
                case 2:
                    {
                        //320x240
                        nSize.Width = 320;
                        nSize.Height = 240;
                        this.CipShowNewSize();
                        break;
                    }
                case 3:
                    {
                        //640x480
                        nSize.Width = 640;
                        nSize.Height = 480;
                        this.CipShowNewSize();
                        break;
                    }
                case 4:
                    {
                        //800x600
                        nSize.Width = 800;
                        nSize.Height = 600;
                        this.CipShowNewSize();
                        break;
                    }
                case 5:
                    {
                        //1024x768
                        nSize.Width = 1024;
                        nSize.Height = 768;
                        this.CipShowNewSize();
                        break;
                    }
                case 6:
                    {
                        //1200x900
                        nSize.Width = 1200;
                        nSize.Height = 900;
                        this.CipShowNewSize();
                        break;
                    }
                case 7:
                    {
                        //1280x1024
                        nSize.Width = 1280;
                        nSize.Height = 1024;
                        this.CipShowNewSize();
                        break;
                    }
                case 8:
                    {
                        //1600x1200
                        nSize.Width = 1600;
                        nSize.Height = 1200;
                        this.CipShowNewSize();
                        break;
                    }
                case 9:
                    {
                        //2048x1536
                        nSize.Width = 2048;
                        nSize.Height = 1536;
                        this.CipShowNewSize();
                        break;
                    }
                case 10:
                    {
                        //50%
                        nSize = Cip.Transformations.Resample.SizeFactorScale(oldSize, 0.5f);
                        this.CipShowNewSize();
                        break;
                    }
                case 11:
                    { 
                        //200%
                        nSize = Cip.Transformations.Resample.SizeFactorScale(oldSize, 2.0f);
                        this.CipShowNewSize();
                        break;
                    }
                default:
                    {
                        nSize = new CipSize(oldSize);
                        this.CipShowNewSize();
                        break;
                    }
            }
        }
        private void numericUpDownWidth_ValueChanged(object sender, EventArgs e)
        {
            int nw = Convert.ToInt32(numericUpDownWidth.Value);
            nSize = Cip.Transformations.Resample.SizeAdaptHeight(oldSize, nw);
            this.CipChangeNumericSize();
        }
        private void numericUpDownHeight_ValueChanged(object sender, EventArgs e)
        {
            int nh = Convert.ToInt32(numericUpDownHeight.Value);
            nSize = Cip.Transformations.Resample.SizeAdaptWidth(oldSize, nh);
            this.CipChangeNumericSize();
        }
        private void textBoxSizeFactor_TextChanged(object sender, EventArgs e)
        {
            try
            {
                float factor = (float)Convert.ToDouble(this.textBoxSizeFactor.Text);
                nSize = Cip.Transformations.Resample.SizeFactorScale(oldSize, factor);
                this.CipShowNewSize();
            }
            catch
            {
                MessageBox.Show("Enter floating point number like 1,5");
                this.textBoxSizeFactor.Text = "1,5";
            }
        }
        private void radioButtonSizeFactor_Click(object sender, EventArgs e)
        {
            this.radioButtonSizeFactor.Checked = true;
            this.radioButtonPixels.Checked = false;
            this.radioButtonStandart.Checked = false;
            this.textBoxSizeFactor.Enabled = true;
            this.numericUpDownWidth.Enabled = false;
            this.numericUpDownHeight.Enabled = false;
            this.comboBoxStandartSizes.Enabled = false;
        }
        private void radioButtonPixels_Click(object sender, EventArgs e)
        {
            this.radioButtonSizeFactor.Checked = false;
            this.radioButtonPixels.Checked = true;
            this.radioButtonStandart.Checked = false;
            this.textBoxSizeFactor.Enabled = false;
            this.numericUpDownWidth.Enabled = true;
            this.numericUpDownHeight.Enabled = true;
            this.comboBoxStandartSizes.Enabled = false;
        }
        private void radioButtonStandart_Click(object sender, EventArgs e)
        {
            this.radioButtonSizeFactor.Checked = false;
            this.radioButtonPixels.Checked = false;
            this.radioButtonStandart.Checked = true;
            this.textBoxSizeFactor.Enabled = false;
            this.numericUpDownWidth.Enabled = false;
            this.numericUpDownHeight.Enabled = false;
            this.comboBoxStandartSizes.Enabled = true;
        }
        private void comboBoxFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.comboBoxFilter.SelectedIndex)
            {
                case 0:
                    {
                        this.mode = CipInterpolationMode.NearestPixel;
                        break;
                    }
                case 1:
                    {
                        this.mode = CipInterpolationMode.BicubicSpline;
                        break;
                    }
                case 2:
                    {
                        this.mode = CipInterpolationMode.Bilinear;
                        break;
                    }
            }
        }
        #endregion Event handlers

        #region Properties

        public Cip.Transformations.CipSize NewImageSize
        {
            get
            {
                return this.nSize;
            }
        }
        public Cip.Transformations.CipInterpolationMode Mode
        {
            get
            {
                return this.mode;
            }
        }

        #endregion Properties

        private void cipOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cipCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        


    }
}