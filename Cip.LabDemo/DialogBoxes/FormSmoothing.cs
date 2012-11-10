using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using Cip.Foundations;
using Cip.Filters;
using Cip.Transformations;

namespace Cip.LabDemo.DialogBoxes
{
    public partial class cipFormSmoothing : Form
    {
        #region Designer



        #endregion Designer

        private Cip.Filters.ColorSpaceMode mode;
        private float sigma;
        private Cip.Filters.SmoothingMode sMode;
        private int radius;

        private Bitmap ThumbnailBitmap;
        private Thread thread;
        private Raster raster;
        
        public cipFormSmoothing()
        {
            InitializeComponent();
        }
        public cipFormSmoothing(Image image)
        {
            InitializeComponent();

            CipSize size = new CipSize(image.Size);
            CipSize newSize = Resample.SizeAdaptHeight(size, 200);
            Raster rOriginal = new Raster((Bitmap)image);
            Resample resampleFIlter = new Resample(newSize, CipInterpolationMode.BicubicSpline);
            raster = resampleFIlter.ProcessWithoutWorker(rOriginal);
            this.ThumbnailBitmap = raster.ToBitmap();
            this.pBoxPreview.Image = this.ThumbnailBitmap;

            thread = new Thread(new ThreadStart(this.ReDraw));
            thread.Priority = ThreadPriority.Normal;
        }
        public cipFormSmoothing(Image image,Raster source)
        {
            InitializeComponent();

            CipSize size = new CipSize(image.Size);
            CipSize newSize = Resample.SizeAdaptHeight(size, 200);
            Resample resampleFIlter = new Resample(newSize, CipInterpolationMode.BicubicSpline);
            raster = resampleFIlter.ProcessWithoutWorker(source);
            this.ThumbnailBitmap = raster.ToBitmap();
            this.pBoxPreview.Image = this.ThumbnailBitmap;

            thread = new Thread(new ThreadStart(this.ReDraw));
            thread.Priority = ThreadPriority.Normal;
        }
        public cipFormSmoothing(Image image, Raster source, System.Globalization.CultureInfo selectedCulture)
        {
            Thread.CurrentThread.CurrentUICulture = selectedCulture;
            InitializeComponent();

            CipSize size = new CipSize(image.Size);
            CipSize newSize = Resample.SizeAdaptHeight(size, 200);
            Resample resampleFIlter = new Resample(newSize, CipInterpolationMode.BicubicSpline);
            raster = resampleFIlter.ProcessWithoutWorker(source);
            this.ThumbnailBitmap = raster.ToBitmap();
            this.pBoxPreview.Image = this.ThumbnailBitmap;

            thread = new Thread(new ThreadStart(this.ReDraw));
            thread.Priority = ThreadPriority.Normal;
        }

        public Cip.Filters.ColorSpaceMode GetMode()
        {
            return this.mode;
        }
        public Cip.Filters.SmoothingMode GetSMode()
        {
            return this.sMode;
        }
        public float GetSigma()
        {
            return this.sigma;
        }
        public int GetRadius()
        {
            return this.radius;
        }
        private void Check()
        {
            if (radioButtonRGB.Checked)
                mode = Cip.Filters.ColorSpaceMode.RGB;
            else
                mode = Cip.Filters.ColorSpaceMode.HSI;

            sigma = (float)Convert.ToDouble(textBoxSigma.Text);
            radius = trackBarRadius.Value;

            if (radioButtonSmoothing.Checked)
                sMode = Cip.Filters.SmoothingMode.Smoothing;
            else
                if (radioButtonBlur.Checked)
                    sMode = Cip.Filters.SmoothingMode.Blur;
                else
                    sMode = Cip.Filters.SmoothingMode.GaussianBlur;
        }
        public void ReDraw()
        {
            this.Check();
            switch (sMode)
            {
                case SmoothingMode.Smoothing:
                    {
                        SmoothingFilter filter = new Cip.Filters.SmoothingFilter(this.mode, this.radius);
                        Raster result = filter.ProcessWithoutWorker(this.raster);
                        result.ShowFilter(this.pBoxPreview);
                        break;
                    }
                case SmoothingMode.Blur:
                    {
                        LinearFilter filter = Cip.Filters.LinearFilter.SimpleBlurFilter(this.radius);
                        Raster result = filter.ProcessWithoutWorker(this.raster);
                        result.ShowFilter(this.pBoxPreview);
                        break;
                    }
                case SmoothingMode.GaussianBlur:
                    {
                        LinearFilter filter = Cip.Filters.LinearFilter.GaussianBlurFilter(this.radius, this.sigma);
                        Raster result = filter.ProcessWithoutWorker(this.raster);
                        result.ShowFilter(this.pBoxPreview);
                        break;
                    }
            }
        }

        private void trackBarRadius_Scroll(object sender, EventArgs e)
        {
            textBoxRadius.Text = Convert.ToString(trackBarRadius.Value);
            if (!this.thread.IsAlive)
            {
                thread = new Thread(new ThreadStart(this.ReDraw));
                thread.Priority = ThreadPriority.Normal;
                thread.Start();
            }
        }
        private void radioButtonSmoothing_Click(object sender, EventArgs e)
        {
            radioButtonSmoothing.Checked = true;
            radioButtonBlur.Checked = false;
            radioButtonGauss.Checked = false;
            groupBoxColorMode.Enabled = true;
            groupBoxSigma.Enabled = false;
        }
        private void radioButtonBlur_Click(object sender, EventArgs e)
        {
            radioButtonSmoothing.Checked = false;
            radioButtonBlur.Checked = true;
            radioButtonGauss.Checked = false;
            groupBoxColorMode.Enabled = false;
            groupBoxSigma.Enabled = false;
        }
        private void radioButtonGauss_Click(object sender, EventArgs e)
        {
            radioButtonSmoothing.Checked = false;
            radioButtonBlur.Checked = false;
            radioButtonGauss.Checked = true;
            groupBoxColorMode.Enabled = false;
            groupBoxSigma.Enabled = true;
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

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.Check();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}