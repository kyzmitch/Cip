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
    public partial class cipFormHistogram : Form
    {
        #region Designer



        #endregion Designer

        private double level;
        private Cip.Filters.HistogramMode mode;
        
        private Bitmap ThumbnailBitmap;
        private Thread thread;
        private Raster raster;

        public cipFormHistogram()
        {
            InitializeComponent();
            level = trackBarThreshold.Value / 1000f;
            textBoxLevel.Text = Convert.ToString(this.level);
        }
        public cipFormHistogram(Image image)
        {
            InitializeComponent();
            level = trackBarThreshold.Value / 1000f;
            textBoxLevel.Text = Convert.ToString(this.level);
            
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
        public cipFormHistogram(Image image,Raster source)
        {
            InitializeComponent();
            level = trackBarThreshold.Value / 1000f;
            textBoxLevel.Text = Convert.ToString(this.level);

            CipSize size = new CipSize(image.Size);
            CipSize newSize = Resample.SizeAdaptHeight(size, 200);
            Resample resampleFIlter = new Resample(newSize, CipInterpolationMode.BicubicSpline);
            raster = resampleFIlter.ProcessWithoutWorker(source);
            this.ThumbnailBitmap = raster.ToBitmap();
            this.pBoxPreview.Image = this.ThumbnailBitmap;

            thread = new Thread(new ThreadStart(this.ReDraw));
            thread.Priority = ThreadPriority.Normal;
        }
        public cipFormHistogram(Image image, Raster source, System.Globalization.CultureInfo selectedCulture)
        {
            Thread.CurrentThread.CurrentUICulture = selectedCulture;
            InitializeComponent();
            level = trackBarThreshold.Value / 1000f;
            textBoxLevel.Text = Convert.ToString(this.level);

            CipSize size = new CipSize(image.Size);
            CipSize newSize = Resample.SizeAdaptHeight(size, 200);
            Resample resampleFIlter = new Resample(newSize, CipInterpolationMode.BicubicSpline);
            raster = resampleFIlter.ProcessWithoutWorker(source);
            this.ThumbnailBitmap = raster.ToBitmap();
            this.pBoxPreview.Image = this.ThumbnailBitmap;

            thread = new Thread(new ThreadStart(this.ReDraw));
            thread.Priority = ThreadPriority.Normal;
        }
        public void ReDraw()
        {
            switch (this.mode)
            {
                case HistogramMode.Equalize:
                    {
                        HistogramEqualization filter = new HistogramEqualization();
                        Raster result = filter.ProcessWithoutWorker(this.raster);
                        result.ShowFilter(this.pBoxPreview);
                        break;
                    }
                case HistogramMode.Normalize:
                    {
                        HistogramNormalize filter = new HistogramNormalize();
                        Raster result = filter.ProcessWithoutWorker(this.raster);
                        if(result!=null)
                            result.ShowFilter(this.pBoxPreview);
                        break;
                    }
                default:
                    { 
                        int modeNum;
                        if (this.mode == HistogramMode.StretchLuminance)
                            modeNum = 0;
                        else if(this.mode == HistogramMode.StretchLinkedChannels)
                                modeNum = 1;
                             else
                                modeNum = 2;
                        HistogramStretch filter = new HistogramStretch(modeNum, this.level);
                        Raster result = filter.ProcessWithoutWorker(this.raster);
                        if (result != null)
                            result.ShowFilter(this.pBoxPreview);
                        break;
                    }
            }
        }
        public double Level
        {
            get
            {
                return this.level;
            }
            set
            {
                this.level = value;
            }
        }
        public Cip.Filters.HistogramMode Mode
        {
            get
            {
                return this.mode;
            }
            set {
                this.mode = value;
            }
        }
        private void trackBarThreshold_Scroll(object sender, EventArgs e)
        {
            this.Level = trackBarThreshold.Value / 1000f;
            textBoxLevel.Text = Convert.ToString(this.level);
            if (!this.thread.IsAlive)
            {
                thread = new Thread(new ThreadStart(this.ReDraw));
                thread.Priority = ThreadPriority.Normal;
                thread.Start();
            }
        }

        private void radioButtonHistNorm_Click(object sender, EventArgs e)
        {
            this.mode = Cip.Filters.HistogramMode.Normalize;
            groupBoxThreshold.Enabled = false;

            radioButtonHistNorm.Checked = true;
            radioButtonHistEqualization.Checked = false;
            radioButtonLuminance.Checked = false;
            radioButtonLinkedChannels.Checked = false;
            radioButtonIndependentChannels.Checked = false;
            if (!this.thread.IsAlive)
            {
                thread = new Thread(new ThreadStart(this.ReDraw));
                thread.Priority = ThreadPriority.Normal;
                thread.Start();
            }
        }
        private void radioButtonHistEqualization_Click(object sender, EventArgs e)
        {
            this.mode = Cip.Filters.HistogramMode.Equalize;
            groupBoxThreshold.Enabled = false;

            radioButtonHistNorm.Checked = false;
            radioButtonHistEqualization.Checked = true;
            radioButtonLuminance.Checked = false;
            radioButtonLinkedChannels.Checked = false;
            radioButtonIndependentChannels.Checked = false;
            if (!this.thread.IsAlive)
            {
                thread = new Thread(new ThreadStart(this.ReDraw));
                thread.Priority = ThreadPriority.Normal;
                thread.Start();
            }
        }
        private void radioButtonLuminance_Click(object sender, EventArgs e)
        {
            this.mode = Cip.Filters.HistogramMode.StretchLuminance;
            groupBoxThreshold.Enabled = true;

            radioButtonHistNorm.Checked = false;
            radioButtonHistEqualization.Checked = false;
            radioButtonLuminance.Checked = true;
            radioButtonLinkedChannels.Checked = false;
            radioButtonIndependentChannels.Checked = false;
        }
        private void radioButtonLinkedChannels_Click(object sender, EventArgs e)
        {
            this.mode = Cip.Filters.HistogramMode.StretchLinkedChannels;
            groupBoxThreshold.Enabled = true;

            radioButtonHistNorm.Checked = false;
            radioButtonHistEqualization.Checked = false;
            radioButtonLuminance.Checked = false;
            radioButtonLinkedChannels.Checked = true;
            radioButtonIndependentChannels.Checked = false;
        }
        private void radioButtonIndependentChannels_Click(object sender, EventArgs e)
        {
            this.mode = Cip.Filters.HistogramMode.StretchIndependentChannels;
            groupBoxThreshold.Enabled = true;

            radioButtonHistNorm.Checked = false;
            radioButtonHistEqualization.Checked = false;
            radioButtonLuminance.Checked = false;
            radioButtonLinkedChannels.Checked = false;
            radioButtonIndependentChannels.Checked = true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Level = trackBarThreshold.Value / 1000f;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        
    }
}