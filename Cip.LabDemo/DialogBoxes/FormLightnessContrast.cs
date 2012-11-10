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
    public partial class cipFormLightnessContrast : Form
    {
        #region Designer



        #endregion Designer

        private int lightness = 0;
        private int contrast = 0;

        private Bitmap ThumbnailBitmap;
        private Raster raster;
        private Thread thread;

        public cipFormLightnessContrast()
        {
            InitializeComponent();
        }
        public cipFormLightnessContrast(Image image)
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
        public cipFormLightnessContrast(Image image, Raster source)
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
        public cipFormLightnessContrast(Image image, Raster source, System.Globalization.CultureInfo selectedCulture)
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
        public int GetLightness()
        {
            return this.lightness;
        }
        public int GetContrast()
        {
            return this.contrast;
        }
        public void ReDraw()
        {
            this.lightness = trackBarLightness.Value;
            this.contrast = trackBarContrast.Value;
            
            LightFilter filter = new Cip.Filters.LightFilter(this.lightness, this.contrast);
            Raster newRaster = filter.ProcessWithoutWorker(raster);
            newRaster.ShowFilter(this.pBoxPreview);
        }
        private void trackBarLightness_Scroll(object sender, EventArgs e)
        {
            this.textBoxLightness.Text = Convert.ToString(trackBarLightness.Value);
            if (!this.thread.IsAlive)
            {
                thread = new Thread(new ThreadStart(this.ReDraw));
                thread.Priority = ThreadPriority.Normal;
                thread.Start();
            }
        }
        private void trackBarContrast_Scroll(object sender, EventArgs e)
        {
            this.textBoxContrast.Text = Convert.ToString(trackBarContrast.Value);
            if (!this.thread.IsAlive)
            {
                thread = new Thread(new ThreadStart(this.ReDraw));
                thread.Priority = ThreadPriority.Normal;
                thread.Start();
            }
        }
        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.lightness = trackBarLightness.Value;
            this.contrast = trackBarContrast.Value;
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