using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

using Cip.Foundations;
using Cip.Filters;
using Cip.Transformations;

namespace Cip.LabDemo.DialogBoxes
{
    public class cipFormBloom : Form
    {
        #region Designer

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(cipFormBloom));
            this.btnOk = new Cip.Components.Buttons.CipOkButton(this.components);
            this.btnCancel = new Cip.Components.Buttons.CipCancelButton(this.components);
            this.textBoxRadius = new System.Windows.Forms.TextBox();
            this.labelRadius = new System.Windows.Forms.Label();
            this.trackBarRadius = new System.Windows.Forms.TrackBar();
            this.pBoxPreview = new System.Windows.Forms.PictureBox();
            this.textBoxLevel = new System.Windows.Forms.TextBox();
            this.trackBarLevel = new System.Windows.Forms.TrackBar();
            this.labelLevel = new System.Windows.Forms.Label();
            this.textBoxBloomFactor = new System.Windows.Forms.TextBox();
            this.labelFactor = new System.Windows.Forms.Label();
            this.trackBarBloomFactor = new System.Windows.Forms.TrackBar();
            this.cipBevel = new Cip.Components.CipBevel();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRadius)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBoxPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBloomFactor)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnOk.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // textBoxRadius
            // 
            resources.ApplyResources(this.textBoxRadius, "textBoxRadius");
            this.textBoxRadius.Name = "textBoxRadius";
            // 
            // labelRadius
            // 
            resources.ApplyResources(this.labelRadius, "labelRadius");
            this.labelRadius.Name = "labelRadius";
            // 
            // trackBarRadius
            // 
            this.trackBarRadius.LargeChange = 1;
            resources.ApplyResources(this.trackBarRadius, "trackBarRadius");
            this.trackBarRadius.Minimum = 1;
            this.trackBarRadius.Name = "trackBarRadius";
            this.trackBarRadius.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBarRadius.Value = 3;
            this.trackBarRadius.Scroll += new System.EventHandler(this.trackBarRadius_Scroll);
            // 
            // pBoxPreview
            // 
            this.pBoxPreview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.pBoxPreview, "pBoxPreview");
            this.pBoxPreview.Name = "pBoxPreview";
            this.pBoxPreview.TabStop = false;
            // 
            // textBoxLevel
            // 
            resources.ApplyResources(this.textBoxLevel, "textBoxLevel");
            this.textBoxLevel.Name = "textBoxLevel";
            // 
            // trackBarLevel
            // 
            this.trackBarLevel.LargeChange = 1;
            resources.ApplyResources(this.trackBarLevel, "trackBarLevel");
            this.trackBarLevel.Maximum = 1000;
            this.trackBarLevel.Name = "trackBarLevel";
            this.trackBarLevel.TickFrequency = 100;
            this.trackBarLevel.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBarLevel.Value = 990;
            this.trackBarLevel.Scroll += new System.EventHandler(this.trackBarLevel_Scroll);
            // 
            // labelLevel
            // 
            resources.ApplyResources(this.labelLevel, "labelLevel");
            this.labelLevel.Name = "labelLevel";
            // 
            // textBoxBloomFactor
            // 
            resources.ApplyResources(this.textBoxBloomFactor, "textBoxBloomFactor");
            this.textBoxBloomFactor.Name = "textBoxBloomFactor";
            // 
            // labelFactor
            // 
            resources.ApplyResources(this.labelFactor, "labelFactor");
            this.labelFactor.Name = "labelFactor";
            // 
            // trackBarBloomFactor
            // 
            this.trackBarBloomFactor.LargeChange = 10;
            resources.ApplyResources(this.trackBarBloomFactor, "trackBarBloomFactor");
            this.trackBarBloomFactor.Maximum = 500;
            this.trackBarBloomFactor.Name = "trackBarBloomFactor";
            this.trackBarBloomFactor.TickFrequency = 100;
            this.trackBarBloomFactor.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBarBloomFactor.Value = 100;
            this.trackBarBloomFactor.Scroll += new System.EventHandler(this.trackBarBloomFactor_Scroll);
            // 
            // cipBevel1
            // 
            resources.ApplyResources(this.cipBevel, "cipBevel1");
            this.cipBevel.Name = "cipBevel1";
            // 
            // cipFormBloom
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.cipBevel);
            this.Controls.Add(this.trackBarBloomFactor);
            this.Controls.Add(this.labelFactor);
            this.Controls.Add(this.textBoxBloomFactor);
            this.Controls.Add(this.labelLevel);
            this.Controls.Add(this.trackBarLevel);
            this.Controls.Add(this.textBoxLevel);
            this.Controls.Add(this.pBoxPreview);
            this.Controls.Add(this.textBoxRadius);
            this.Controls.Add(this.labelRadius);
            this.Controls.Add(this.trackBarRadius);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "cipFormBloom";
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRadius)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBoxPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBloomFactor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Cip.Components.Buttons.CipOkButton btnOk;
        private Cip.Components.Buttons.CipCancelButton btnCancel;
        private Cip.Components.CipBevel cipBevel;
        private System.Windows.Forms.TextBox textBoxRadius;
        private System.Windows.Forms.Label labelRadius;
        private System.Windows.Forms.TrackBar trackBarRadius;
        private System.Windows.Forms.PictureBox pBoxPreview;
        private System.Windows.Forms.TextBox textBoxLevel;
        private System.Windows.Forms.TrackBar trackBarLevel;
        private System.Windows.Forms.Label labelLevel;
        private System.Windows.Forms.TextBox textBoxBloomFactor;
        private System.Windows.Forms.Label labelFactor;
        private System.Windows.Forms.TrackBar trackBarBloomFactor;

        #endregion Designer

        private int radius;
        private float thresholdLevel;
        private float bloomFactor;
        
        private Bitmap ThumbnailBitmap;
        private Thread thread;
        
        private Raster raster;

        public cipFormBloom()
        {
            InitializeComponent();
        }
        public cipFormBloom(Image image)
        {
            InitializeComponent();
            this.ReadParams();

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
        public cipFormBloom(Image image,Raster source)
        {
            InitializeComponent();
            this.ReadParams();

            CipSize size = new CipSize(image.Size);
            CipSize newSize = Resample.SizeAdaptHeight(size, 200);
            Resample resampleFIlter = new Resample(newSize, CipInterpolationMode.BicubicSpline);
            raster = resampleFIlter.ProcessWithoutWorker(source);
            this.ThumbnailBitmap = raster.ToBitmap();
            this.pBoxPreview.Image = this.ThumbnailBitmap;

            thread = new Thread(new ThreadStart(this.ReDraw));
            thread.Priority = ThreadPriority.Normal;
        }
        public cipFormBloom(Image image, Raster source, System.Globalization.CultureInfo selectedCulture)
        {
            Thread.CurrentThread.CurrentUICulture = selectedCulture;
            InitializeComponent();
            this.ReadParams();

            CipSize size = new CipSize(image.Size);
            CipSize newSize = Resample.SizeAdaptHeight(size, 200);
            Resample resampleFIlter = new Resample(newSize, CipInterpolationMode.BicubicSpline);
            raster = resampleFIlter.ProcessWithoutWorker(source);
            this.ThumbnailBitmap = raster.ToBitmap();
            this.pBoxPreview.Image = this.ThumbnailBitmap;

            thread = new Thread(new ThreadStart(this.ReDraw));
            thread.Priority = ThreadPriority.Normal;
        }

        #region Properties

        public int Radius
        {
            get
            {
                return this.radius;
            }
        }
        public float BlendFactor
        {
            get 
            {
                return this.bloomFactor;   
            }
        }
        public float ThresholdLevel
        {
            get
            {
                return this.thresholdLevel;
            }
        }

        #endregion Properties

        #region Private functions

        private void ReadParams()
        {
            this.radius = this.trackBarRadius.Value;
            this.thresholdLevel = trackBarLevel.Value / 1000f;
            this.bloomFactor = trackBarBloomFactor.Value / 100f;
        }
        private void ReDraw()
        {
            Bloom filter = new Bloom(this.bloomFactor, this.thresholdLevel, this.radius);
            Raster result = filter.ProcessWithoutWorker(this.raster);
            result.ShowFilter(this.pBoxPreview);
        }
        private void CreateReDrawThread()
        {
            if (!this.thread.IsAlive)
            {
                thread = new Thread(new ThreadStart(this.ReDraw));
                thread.Priority = ThreadPriority.Normal;
                thread.Start();
            }
        }

        #endregion Private functions

        private void trackBarRadius_Scroll(object sender, EventArgs e)
        {
            textBoxRadius.Text = Convert.ToString(trackBarRadius.Value);
            this.radius = this.trackBarRadius.Value;
            CreateReDrawThread();
        }
        private void trackBarLevel_Scroll(object sender, EventArgs e)
        {
            this.thresholdLevel = trackBarLevel.Value / 1000f;
            textBoxLevel.Text = Convert.ToString(this.thresholdLevel);
            CreateReDrawThread();
        }
        private void trackBarBloomFactor_Scroll(object sender, EventArgs e)
        {
            this.bloomFactor = trackBarBloomFactor.Value / 100f;
            textBoxBloomFactor.Text = Convert.ToString(this.bloomFactor);
            CreateReDrawThread();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
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