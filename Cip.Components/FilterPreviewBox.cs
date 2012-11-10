using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Cip.Components
{
    public partial class FilterPreviewBox : UserControl
    {
        private int widthOriginal;
        private int heightOriginal;

        public FilterPreviewBox()
        {
            InitializeComponent();
        }
        public FilterPreviewBox(Bitmap image)
        {
            InitializeComponent();
            this.filterPreview.Image = image;
            this.widthOriginal = image.Width;
            this.heightOriginal = image.Height;
        }
        public override string Text
        {
            get
            {
                return this.groupBox.Text;
            }
            set
            {
                this.groupBox.Text = value;
            }
        }
        public void SetBitmap(Bitmap image)
        {
            this.filterPreview.Image = image;
            this.widthOriginal = image.Width;
            this.heightOriginal = image.Height;
        }
        public void SetFilter(Cip.Foundations.ImageFilter filter)
        {
            this.filterPreview.Filter = filter;
        }

        private void buttonPlus_Click(object sender, EventArgs e)
        {
            System.Drawing.Size oldSize = this.filterPreview.GetArea();
            int nWidth = Math.Min(oldSize.Width - 30, this.widthOriginal);
            int nHeight = Math.Min(oldSize.Height - 30, this.widthOriginal);
            this.filterPreview.SetArea( new Size(nWidth, nHeight));
        }

        private void buttonMinus_Click(object sender, EventArgs e)
        {
            System.Drawing.Size oldSize = this.filterPreview.GetArea();
            int nWidth = Math.Max(oldSize.Width + 30, 40);
            int nHeight = Math.Max(oldSize.Height + 30, 40);
            this.filterPreview.SetArea(new Size(nWidth, nHeight));
        }
    }
}
