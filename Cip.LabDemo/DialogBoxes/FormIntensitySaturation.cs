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
    public partial class cipFormIntensitySaturation : Form
    {
        #region Designer



        #endregion Designer

        private int levelIntensity = 0;
        private int levelSaturation = 0;
        //private Image ThumbnailImage;
        private Bitmap ThumbnailBitmap;
        private Raster raster;
        private Thread thread;

        public cipFormIntensitySaturation()
        {
            InitializeComponent();
        }
        public cipFormIntensitySaturation(Image image)
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
        public cipFormIntensitySaturation(Image image, Raster source)
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
        public cipFormIntensitySaturation(Image image, Raster source, System.Globalization.CultureInfo selectedCulture)
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
        public bool IsRGB()
        {
            return this.checkBoxRGB.Checked;
        }
        public int GetIntensity()
        {
            return this.levelIntensity;
        }
        public int GetSaturation()
        {
            return this.levelSaturation;
        }
        public void ReDraw()
        {
            this.levelIntensity = trackBarIntensity.Value;
            this.levelSaturation = trackBarSaturation.Value;
            Raster newRaster;
            if (this.IsRGB())
            {
                IntensityChanger filter = new Cip.Filters.IntensityChanger(this.levelIntensity);
                newRaster = filter.ProcessWithoutWorker(raster);
            }
            else
            {
                HsiCorrectionFilter filter = new Cip.Filters.HsiCorrectionFilter(this.levelIntensity, this.levelSaturation);
                newRaster = filter.ProcessWithoutWorker(raster);
            }
            newRaster.ShowFilter(this.pBoxPreview);
        }
        private void trackBarIntensity_Scroll(object sender, EventArgs e)
        {
            textBoxLevelIntensity.Text = Convert.ToString(trackBarIntensity.Value);
            if (!this.thread.IsAlive)
            {
                thread = new Thread(new ThreadStart(this.ReDraw));
                thread.Priority = ThreadPriority.Normal;
                thread.Start();
            }
        }
        private void trackBarSaturation_Scroll(object sender, EventArgs e)
        {
            textBoxLevelSaturation.Text = Convert.ToString(trackBarSaturation.Value);
            if (!this.thread.IsAlive)
            {
                thread = new Thread(new ThreadStart(this.ReDraw));
                thread.Priority = ThreadPriority.Normal;
                thread.Start();
            }
        }
        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.levelIntensity = trackBarIntensity.Value;
            this.levelSaturation = trackBarSaturation.Value;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void checkBoxRGB_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxRGB.Checked)
            {
                this.trackBarSaturation.Enabled = false;
            }
            else
            {
                this.trackBarSaturation.Enabled = true;
            }
        }

        

        
    }
}