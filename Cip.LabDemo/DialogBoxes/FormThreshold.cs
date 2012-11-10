using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;

using Cip.Foundations;
using Cip.Filters;
using Cip.Transformations;

namespace Cip.LabDemo.DialogBoxes
{
    public partial class cipFormThreshold : Form
    {
        private byte level;

        private Bitmap ThumbnailBitmap;
        private Bitmap sourceBitmap;
        private Thread thread;
        private Raster raster;
        private Raster rasterSource;

        public cipFormThreshold()
        {
            InitializeComponent();
        }
        public cipFormThreshold(Image image, CultureInfo selectedCulture)
        {
            Thread.CurrentThread.CurrentUICulture = selectedCulture;
            InitializeComponent();

            sourceBitmap = new Bitmap(image);
            rasterSource = new Raster(sourceBitmap);

            CipSize size = new CipSize(image.Size);
            CipSize newSize = Resample.SizeAdaptHeight(size, 200);
            Resample resampleFIlter = new Resample(newSize, CipInterpolationMode.BicubicSpline);
            raster = resampleFIlter.ProcessWithoutWorker(rasterSource);
            this.ThumbnailBitmap = raster.ToBitmap();
            this.pBoxPreview.Image = this.ThumbnailBitmap;

            thread = new Thread(new ThreadStart(this.ReDraw));
            thread.Priority = ThreadPriority.Normal;
            this.level = 0;
        }
        public cipFormThreshold(Image image, Raster source, CultureInfo selectedCulture)
        {
            Thread.CurrentThread.CurrentUICulture = selectedCulture;
            InitializeComponent();
            sourceBitmap = new Bitmap(image);
            rasterSource = source;

            CipSize size = new CipSize(image.Size);
            CipSize newSize = Resample.SizeAdaptHeight(size, 200);
            Resample resampleFIlter = new Resample(newSize, CipInterpolationMode.BicubicSpline);
            raster = resampleFIlter.ProcessWithoutWorker(rasterSource);
            this.ThumbnailBitmap = raster.ToBitmap();
            this.pBoxPreview.Image = this.ThumbnailBitmap;
            
            thread = new Thread(new ThreadStart(this.ReDraw));
            thread.Priority = ThreadPriority.Normal;
            this.level = 0;
        }
        public byte GetLevel()
        {
            return level;
        }
        private void ReDraw()
        {
            ThresholdFilter filter = new Cip.Filters.ThresholdFilter(this.level);
            Raster result = filter.ProcessWithoutWorker(raster);
            result.ShowFilter(this.pBoxPreview);
        }
        private void RunThread()
        {
            if (!this.thread.IsAlive)
            {
                thread = new Thread(new ThreadStart(this.ReDraw));
                thread.Priority = ThreadPriority.Normal;
                thread.Start();
            }
        }
        private void RunThreadOptimalThreshold(int methodNumber)
        {
            this.level = (byte)Cip.Filters.ThresholdFilter.OptimalThreshold(this.rasterSource, methodNumber, Rectangle.Empty);
            this.textBoxLevel.Text = Convert.ToString(this.level);
            this.trackBarLevel.Value = (int)level;
            RunThread();
        }
        private void RunThreadIsoDataThreshold(int mode)
        {
            this.level = (byte)Cip.Filters.ThresholdFilter.IsodataClusteringThreshold(this.rasterSource, mode, Rectangle.Empty);
            this.textBoxLevel.Text = Convert.ToString(this.level);
            this.trackBarLevel.Value = (int)level;
            RunThread();
        }
        private void RunThreadTextSeparation()
        {
            this.level = (byte)Cip.Filters.ThresholdFilter.TextSeparationThresholding(this.rasterSource);
            this.textBoxLevel.Text = Convert.ToString(this.level);
            this.trackBarLevel.Value = (int)level;
            RunThread();
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            //level = (byte)this.textBoxLevel.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void trackBarLevel_Scroll(object sender, EventArgs e)
        {
            textBoxLevel.Text = Convert.ToString(trackBarLevel.Value);
            level = (byte)trackBarLevel.Value;
            RunThread();
        }

        private void checkBoxManual_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxManual.Checked==false)
            {
                this.trackBarLevel.Enabled = false;
                this.radioButtonAuto.Enabled = true;
                this.radioButtonOtsu.Enabled = true;
                this.radioButtonKi.Enabled = true;
                this.radioButtonMaxEntropy.Enabled = true;
                this.radioButtonPotDiff.Enabled = true;
                this.radioButtonIsoData.Enabled = true;
                this.checkBoxGlobalMode.Enabled = true;
                this.radioButtonTextSeparation.Enabled = true;
                this.radioButtonAuto_Click(sender, e);
            }
            else
            {
                this.trackBarLevel.Enabled = true;
                this.radioButtonAuto.Enabled = false;
                this.radioButtonOtsu.Enabled = false;
                this.radioButtonKi.Enabled = false;
                this.radioButtonMaxEntropy.Enabled = false;
                this.radioButtonPotDiff.Enabled = false;
                this.radioButtonIsoData.Enabled = false;
                this.checkBoxGlobalMode.Enabled = false;
                this.radioButtonTextSeparation.Enabled = false;
            }
        }
        private void radioButtonAuto_Click(object sender, EventArgs e)
        {
            this.radioButtonAuto.Checked = true;
            this.radioButtonOtsu.Checked = false;
            this.radioButtonKi.Checked = false;
            this.radioButtonMaxEntropy.Checked = false;
            this.radioButtonPotDiff.Checked = false;
            this.radioButtonIsoData.Checked = false;
            RunThreadOptimalThreshold(0);
            
        }
        private void radioButtonOtsu_Click(object sender, EventArgs e)
        {
            this.radioButtonAuto.Checked = false;
            this.radioButtonOtsu.Checked = true;
            this.radioButtonKi.Checked = false;
            this.radioButtonMaxEntropy.Checked = false;
            this.radioButtonPotDiff.Checked = false;
            this.radioButtonIsoData.Checked = false;
            RunThreadOptimalThreshold(1);
        }
        private void radioButtonKi_Click(object sender, EventArgs e)
        {
            this.radioButtonAuto.Checked = false;
            this.radioButtonOtsu.Checked = false;
            this.radioButtonKi.Checked = true;
            this.radioButtonMaxEntropy.Checked = false;
            this.radioButtonPotDiff.Checked = false;
            this.radioButtonIsoData.Checked = false;
            RunThreadOptimalThreshold(2);
        }
        private void radioButtonMaxEntropy_Click(object sender, EventArgs e)
        {
            this.radioButtonAuto.Checked = false;
            this.radioButtonOtsu.Checked = false;
            this.radioButtonKi.Checked = false;
            this.radioButtonMaxEntropy.Checked = true;
            this.radioButtonPotDiff.Checked = false;
            this.radioButtonIsoData.Checked = false;
            RunThreadOptimalThreshold(3);
        }
        private void radioButtonPotDiff_Click(object sender, EventArgs e)
        {
            this.radioButtonAuto.Checked = false;
            this.radioButtonOtsu.Checked = false;
            this.radioButtonKi.Checked = false;
            this.radioButtonMaxEntropy.Checked = false;
            this.radioButtonPotDiff.Checked = true;
            this.radioButtonIsoData.Checked = false;
            RunThreadOptimalThreshold(4);
        }
        private void radioButtonAutoGlobal_Click(object sender, EventArgs e)
        {
            this.radioButtonAuto.Checked = false;
            this.radioButtonOtsu.Checked = false;
            this.radioButtonKi.Checked = false;
            this.radioButtonMaxEntropy.Checked = false;
            this.radioButtonPotDiff.Checked = false;
            this.radioButtonIsoData.Checked = true;
            int mode;
            if(this.checkBoxGlobalMode.Checked)
                mode = 0;
            else
                mode = 1;
            this.RunThreadIsoDataThreshold(mode);
        }

        private void radioButtonTextSeparation_Click(object sender, EventArgs e)
        {
            this.radioButtonAuto.Checked = false;
            this.radioButtonOtsu.Checked = false;
            this.radioButtonKi.Checked = false;
            this.radioButtonMaxEntropy.Checked = false;
            this.radioButtonPotDiff.Checked = false;
            this.radioButtonIsoData.Checked = false;
            this.radioButtonTextSeparation.Checked = true;
            this.RunThreadTextSeparation();
        }


    }
}