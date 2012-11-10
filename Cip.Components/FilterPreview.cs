// Image Processing Lab
//
// Copyright © Andrew Kirillov, 2005
// andrew.kirillov@gmail.com
//

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Reflection;
using Cip.Filters;
using Cip.Foundations;

namespace Cip.Components
{
    /// <summary>
    /// FilterPreview window
    /// </summary>
    public class FilterPreview : System.Windows.Forms.Control
    {
        private Bitmap previewImage;
        private Bitmap image;
        private Cip.Foundations.ImageFilter filter;
        private Pen blackPen = new Pen( Color.Black, 1 );
        
        private Cursor cursorHand;
        private Cursor cursorHandMove;

        private int areaWidth, areaHeight;
        private int prevWidth, prevHeight;
        private int halfW, halfH;
        private int imageX, imageY;
        private bool tracking = false;

        private int startTrackingX, startTrackingY;
        private int oldImageX, oldImageY;

        // Image property
        [Browsable( false )]
        public Bitmap Image
        {
            get { return image; }
            set
            {
                image = value;

                if ( value != null )
                {
                    // calculate size of preview area
                    areaWidth = Math.Min( ClientRectangle.Width - 2, image.Width );
                    areaHeight = Math.Min( ClientRectangle.Height - 2, image.Height );
                    prevWidth = areaWidth;
                    prevHeight = areaHeight;

                    halfW = (int)(image.Width / 2f);
                    Cip.Transformations.CipSize nSize = Cip.Transformations.Resample.SizeAdaptHeight(image.Size, halfW);
                    halfH = nSize.Height;

                    // calculate image position
                    imageX = ( image.Width - areaWidth ) >> 1;
                    imageY = ( image.Height - areaHeight ) >> 1;
                }

                RefreshFilter( );
            }
        }
        // Filter property
        [Browsable( false )]
        public Cip.Foundations.ImageFilter Filter
        {
            set
            {
                filter = value;
                RefreshFilter( );
            }
        }
        
        /// <summary>
        /// Get area of preview.
        /// </summary>
        public Size GetArea()
        {
            return new Size(prevWidth, prevHeight);
        }
        /// <summary>
        /// Set area of preview.
        /// </summary>
        public void SetArea(Size size)
        {
            prevWidth = Math.Min(size.Width, image.Width);
            //prevHeight = Math.Min(size.Height, image.Height);
            Cip.Transformations.CipSize nnSize = Cip.Transformations.Resample.SizeAdaptHeight(image.Size, prevWidth);
            prevHeight = nnSize.Height;
            
            if ((prevWidth >= halfW) || (prevHeight >= halfH))
            {
                prevWidth = halfW;
                prevHeight = halfH;
            }

            // calculate image position
            if (imageX > (image.Width - prevWidth))
                imageX = image.Width - prevWidth;
            if (imageY > (image.Height - prevHeight))
                imageY = image.Height - prevHeight;

            RefreshFilter();
        }

        // Constructor
        public FilterPreview( )
        {
            InitializeComponent( );
            SetStyle( ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw |
                ControlStyles.DoubleBuffer | ControlStyles.UserPaint, true );
        }

        // Dispose
        protected override void Dispose( bool disposing )
        {
            if ( disposing )
            {
                blackPen.Dispose( );

                if ( previewImage != null )
                {
                    previewImage.Dispose( );
                    cursorHand.Dispose( );
                    cursorHandMove.Dispose( );
                }
            }
            base.Dispose( disposing );
        }

        // Initialize control
        private void InitializeComponent( )
        {
            this.MouseUp += new System.Windows.Forms.MouseEventHandler( this.FilterPreview_MouseUp );
            this.MouseMove += new System.Windows.Forms.MouseEventHandler( this.FilterPreview_MouseMove );
            this.MouseDown += new System.Windows.Forms.MouseEventHandler( this.FilterPreview_MouseDown );
        }

        // Paint control
        protected override void OnPaint( PaintEventArgs pe )
        {
            Graphics g = pe.Graphics;
            Rectangle rc = ClientRectangle;
            int width = rc.Width;
            int height = rc.Height;
            int x, y;

            // calculate size of preview area
            if ( image != null )
            {
                width = areaWidth + 2;
                height = areaHeight + 2;
            }
            // calculate position of preview area
            x = ( rc.Width - width ) >> 1;
            y = ( rc.Height - height ) >> 1;

            // draw rectangle
            g.DrawRectangle( blackPen, x, y, width - 1, height - 1 );

            x++;
            y++;

            if ( image != null )
            {
                if ( previewImage == null )
                {
                    // draw original image
                    g.DrawImage(image, new Rectangle(x, y, areaWidth, areaHeight), imageX, imageY, prevWidth, prevHeight, GraphicsUnit.Pixel);
                }
                else
                {
                    // draw preview image
                    g.DrawImage( previewImage, x, y, areaWidth, areaHeight );
                }
            }

            // Calling the base class OnPaint
            base.OnPaint( pe );
        }

        // Refresh preview
        public void RefreshFilter( )
        {
            // release old image
            if ( previewImage != null )
            {
                previewImage.Dispose( );
                previewImage = null;
            }

            if ( ( image != null ) && ( filter != null ) )
            {
                try
                {
                    Bitmap tmp = image.Clone(new Rectangle(imageX, imageY, prevWidth, prevHeight), image.PixelFormat);
                    previewImage = filter.ProcessWithoutWorker(new Raster(tmp)).ToBitmap();
                    // release temp image
                    tmp.Dispose();
                }
                catch
                {
                    throw new ArgumentOutOfRangeException("Rectangle of preview area is out of range!");
                }
            }

            // repaint
            Invalidate( );
        }

        // On mouse move
        private void FilterPreview_MouseMove( object sender, System.Windows.Forms.MouseEventArgs e )
        {
            if ( image != null )
            {
                if ( !tracking )
                {
                    // calculate position of preview area
                    int x = ((ClientRectangle.Width - prevWidth - 2) >> 1) + 1;
                    int y = ((ClientRectangle.Height - prevHeight - 2) >> 1) + 1;

                    // check mouse coordinates
                    if ( ( e.X >= x ) && ( e.Y >= y ) &&
                        (e.X < x + prevWidth) && (e.Y < y + prevHeight))
                    {
                        Cursor = cursorHand;
                    }
                    else
                        Cursor = Cursors.Default;
                }
                else
                {
                    int dx = e.X - startTrackingX;
                    int dy = e.Y - startTrackingY;

                    imageX = Math.Max(0, Math.Min(image.Width - prevWidth, oldImageX - dx));
                    imageY = Math.Max(0, Math.Min(image.Height - prevHeight, oldImageY - dy));
                    

                    Invalidate( );

                    Cursor = cursorHandMove;
                }
            }
        }

        // On mouse button down
        private void FilterPreview_MouseDown( object sender, System.Windows.Forms.MouseEventArgs e )
        {
            if ( ( image != null ) && ( e.Button == MouseButtons.Left ) )
            {
                // start tracking
                tracking = true;
                Capture = true;

                startTrackingX = e.X;
                startTrackingY = e.Y;

                oldImageX = imageX;
                oldImageY = imageY;

                // release preview image
                if ( previewImage != null )
                {
                    previewImage.Dispose( );
                    previewImage = null;
                }
                // repaint
                Invalidate( );

                // set cursor
                Cursor = cursorHandMove;
            }
        }

        // On mouse button up
        private void FilterPreview_MouseUp( object sender, System.Windows.Forms.MouseEventArgs e )
        {
            if ( tracking )
            {
                // stop tracking
                tracking = false;
                Capture = false;

                RefreshFilter( );

                // set cursor
                Cursor = cursorHand;
            }
        }
    }
}
