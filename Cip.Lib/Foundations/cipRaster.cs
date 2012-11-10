/////////////////////////////////////////////////////////////////////////////////
// Colour Image Processing Library (CipLibNet)                                 //
// Copyright (C) Andrew [kyzmitch] Ermoshin.                                   //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;
using System.Threading;

#pragma warning disable 1591

namespace Cip.Foundations
{
    /// <summary>
    /// Raster class
    /// </summary>
    public class Raster
    {
        #region Private Fields

        /// <summary>
        /// Pixels array.
        /// </summary>
        private VectorRgb[,] pixels;
        /// <summary>
        /// Height of raster.
        /// </summary>
        private int height;
        /// <summary>
        /// Width of raster.
        /// </summary>
        private int width;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the Raster class from bit matrix.
        /// </summary>
        /// <param name="bitmap">bit matrix</param>
        public Raster(Bitmap bitmap)
        {
            this.height = bitmap.Height;
            this.width = bitmap.Width;

            // create pixels array
            pixels = new VectorRgb[width, height];

            BitmapData bmData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            int stride = bmData.Stride;
            IntPtr Scan0 = bmData.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                int nOffset = stride - width * 3;
                byte red, green, blue;

                for (int y = 0; y < height; ++y)
                {
                    for (int x = 0; x < width; ++x)
                    {
                        // GDI+ lies to us - the return format is BGR, NOT RGB.
                        blue = p[0];
                        green = p[1];
                        red = p[2];

                        pixels[x, y] = new VectorRgb(red, green, blue);

                        p += 3;
                    }
                    p += nOffset;
                }
            }

            bitmap.UnlockBits(bmData);
            
            #region Old code
            /*
            for (int j = 0; j < this.height; j++)
                for (int i = 0; i < this.width; i++)
                    pixels[i, j] = new VectorRgb(bitmap.GetPixel(i, j));
            */
            #endregion Old code
        }
        /// <summary>
        /// Initializes a new instance of the Raster class (all pixels are white)
        /// </summary>
        /// <param name="width">width</param>
        /// <param name="height">height</param>
        public Raster(int width, int height)
        {
            // create pixels array
            pixels = new VectorRgb[width, height];
            this.height = height;
            this.width = width;
        }
        /// <summary>
        /// Initializes a new instance of the Raster class with fixed color
        /// </summary>
        /// <param name="width">width</param>
        /// <param name="height">height</param>
        /// <param name="color">color of raster</param>
        public Raster(int width, int height, Color color)
        {
            // create pixles array
            pixels = new VectorRgb[width, height];
            this.height = height;
            this.width = width;

            // appropriate white color to all pixels of the raster
            for (int j = 0; j < height; j++)
                for (int i = 0; i < width; i++)
                    pixels[i, j] = new VectorRgb(color);
        }
        /// <summary>
        /// Initializes a new instance of the Raster class from file
        /// </summary>
        /// <param name="filename">File name</param>
        public Raster(string filename)
        {
            try
            {
                // create Bitmap object from file
                Bitmap bitmap = new Bitmap(filename);
                this.height = bitmap.Height;
                this.width = bitmap.Width;
                // create pixels array
                pixels = new VectorRgb[this.width, this.height];

                BitmapData bmData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
                int stride = bmData.Stride;
                IntPtr Scan0 = bmData.Scan0;

                unsafe
                {
                    byte* p = (byte*)(void*)Scan0;

                    int nOffset = stride - width * 3;
                    byte red, green, blue;

                    for (int y = 0; y < height; ++y)
                    {
                        for (int x = 0; x < width; ++x)
                        {
                            // GDI+ lies to us - the return format is BGR, NOT RGB.
                            blue = p[0];
                            green = p[1];
                            red = p[2];

                            pixels[x, y] = new VectorRgb(red, green, blue);

                            p += 3;
                        }
                        p += nOffset;
                    }
                }

                bitmap.UnlockBits(bmData);

                #region Old code
                /*
                for (int j = 0; j < this.height; j++)
                    for (int i = 0; i < this.width; i++)
                        pixels[i, j] = new VectorRgb(bitmap.GetPixel(i, j));
                */
                #endregion Old code
            }
            catch
            {
                throw new ArgumentException("It's not BITMAP file");
            }
        }
        /// <summary>
        /// Copy Constructor
        /// </summary>
        /// <param name="value">Raster, which copying</param>
        public Raster(Raster value)
        {
            // create array of pixels
            this.width = value.Width;
            this.height = value.Height;
            pixels = new VectorRgb[this.width, this.height];

            for (int j = 0; j < this.height; j++)
                for (int i = 0; i < this.width; i++)
                    pixels[i, j] = value.pixels[i, j];
         
        }
        
        #endregion

        #region Public Methods

        /// <summary>
        /// Creates filled bitmap.
        /// </summary>
        /// <param name="width">Width.</param>
        /// <param name="height">Height.</param>
        /// <param name="color">Color.</param>
        /// <returns>Bitmap.</returns>
        public static Bitmap CreateBitmap(int width, int height, Color color)
        {
            // create new Bitmap object
            Bitmap bitmap = new Bitmap(width, height);

            using (BitmapDecorator bitDecorator = new BitmapDecorator(bitmap))
            {
                for (int j = 0; j < height; j++)
                    for (int i = 0; i < width; i++)
                        bitDecorator.SetPixel(i, j, color);
            }

            return bitmap;
        }
        /// <summary>
        /// Converting THIS raster into Bitmap object.
        /// </summary>
        /// <returns>Bitmap object</returns>
        public Bitmap ToBitmap()
        {
            // create new Bitmap object
            Bitmap bitmap = new Bitmap(this.width, this.height);

            #region Old code
            /*using (BitmapDecorator bitDecorator = new BitmapDecorator(bitmap))
            {
                // copying of the raster contents
                for (int j = 0; j < this.height; j++)
                    for (int i = 0; i < this.width; i++)
                        bitDecorator.SetPixel(i, j, pixels[i, j].R, 
                                                    pixels[i, j].G, 
                                                    pixels[i, j].B);
            }*/
            #endregion Old code

            BitmapData bmData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
            int stride = bmData.Stride;
            IntPtr Scan0 = bmData.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                int nOffset = stride - width * 3;

                for (int y = 0; y < height; ++y)
                {
                    for (int x = 0; x < width; ++x)
                    {
                        p[0] = this.pixels[x, y].B;
                        p[1] = this.pixels[x, y].G;
                        p[2] = this.pixels[x, y].R;

                        p += 3;
                    }
                    p += nOffset;
                }
            }

            bitmap.UnlockBits(bmData);
            
            return bitmap;
        }
        /// <summary>
        /// Converting Raster into System.Drawing.Bitmap object.
        /// </summary>
        /// <param name="raster">Source raster.</param>
        /// <returns>Bitmap.</returns>
        static public Bitmap ToBitmap(Raster raster)
        {
            int w = raster.Width;
            int h = raster.Height;
            // create new Bitmap object
            Bitmap bitmap = new Bitmap(w, h);

            BitmapData bmData = bitmap.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
            int stride = bmData.Stride;
            IntPtr Scan0 = bmData.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                int nOffset = stride - w * 3;

                for (int y = 0; y < h; ++y)
                {
                    for (int x = 0; x < w; ++x)
                    {
                        p[0] = raster.pixels[x, y].B;
                        p[1] = raster.pixels[x, y].G;
                        p[2] = raster.pixels[x, y].R;

                        p += 3;
                    }
                    p += nOffset;
                }
            }

            bitmap.UnlockBits(bmData);

            return bitmap;
        }
        /// <summary>
        /// Save raster into adjusted graphic file
        /// </summary>
        /// <param name="filename">Name of the file</param>
        public void Save(string filename)
        {
            // take a Bitmap object
            Bitmap bitmap = this.ToBitmap();

            // save object into Bitmap file
            bitmap.Save(filename);
        }
        /// <summary>
        /// Show image in the picturebox.
        /// </summary>
        /// <param name="pictureBox">PictureBox.</param>
        private void ShowFilterWithoutThread(object pictureBox)
        {
            try
            {
                ((PictureBox)pictureBox).Image = this.ToBitmap();
            }
            catch
            {
                MessageBox.Show("This object isn't Picture Box", 
                                "Error in method parametr", 
                                MessageBoxButtons.OK, 
                                MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Show image in the picturebox.
        /// </summary>
        /// <param name="pictureBox">PictureBox.</param>
        public void ShowFilter(PictureBox pictureBox)
        {
            pictureBox.Image = this.ToBitmap();
        }
        /// <summary>
        /// Show image in the picturebox in grayscale mode.
        /// </summary>
        /// <param name="PB">PictureBox.</param>
        /// <param name="worker">Background worker.</param>
        public void ShowFilterBlackWhite(PictureBox PB, System.ComponentModel.BackgroundWorker worker)
        {
            Cip.Filters.GrayScale filter = new Cip.Filters.GrayScale();
            Raster BlackWhiteRaster = filter.ProcessRaster(this, worker);
            PB.Image = BlackWhiteRaster.ToBitmap();
        }
        /// <summary>
        /// Show image in the picturebox in grayscale mode.
        /// </summary>
        /// <param name="PB">PictureBox.</param>
        public void ShowFilterBlackWhite(PictureBox PB)
        {
            Cip.Filters.GrayScale filter = new Cip.Filters.GrayScale();
            Raster BlackWhiteRaster = filter.ProcessWithoutWorker(this);
            PB.Image = BlackWhiteRaster.ToBitmap();
        }
        /// <summary>
        /// Check image gray or not.
        /// </summary>
        /// <returns>true if image is gray, else false.</returns>
        public bool IsGrayScaled()
        {
            for (int j = 0; j < this.height; j++)
                for (int i = 0; i < this.width; i++)
                    if (!((pixels[i, j].R == pixels[i, j].G)&&(pixels[i, j].R == pixels[i, j].B)))
                        return false;
            return true;
        }
        /// <summary>
        /// Converts Raster to intensity 2 dimension byte array.
        /// </summary>
        /// <returns>Byte array.</returns>
        public byte[,] ToGrayScaledArray()
        {
            byte[,] array = new byte[this.width, this.height];

            for (int j = 0; j < height; j++)
                for (int i = 0; i < width; i++)
                    array[i, j] = this.pixels[i, j].GetIntensity();

            return array;
        }

        #endregion

        #region Properties

        /// <summary> Width of Raster. </summary>
        public int Width
        {
            get
            {
                return this.width;
            }
        }
        /// <summary> Height of Raster. </summary>
        public int Height
        {
            get
            {
                return this.height;
            }
        }
        /// <summary>
        /// Indexer.
        /// Provide direct access to Raster pixels.
        /// </summary>
        /// <param name="i">column</param>
        /// <param name="j">row</param>
        /// <returns>RGB vector</returns>
        public VectorRgb this[int i, int j]
        {
            get
            {
            	if (i < 0) i  = 0;
                if (i > this.width - 1) i = this.width - 1;
            	if (j < 0) j  = 0;
                if (j > this.height - 1) j = this.height - 1;
                return pixels[i, j];
            }
            set
            {
                if (i > -1 && i < this.width)
                    if (j > -1 && j < this.height)
                        pixels[i, j] = value;
            }
        }

        #endregion

        #region Operators

        /// <summary>
        /// Difference between two images.
        /// </summary>
        public static Raster Difference(Raster left, Raster right)
        {
            if ((left.Width != right.Width) || (left.Height != right.Height))
                return null;
            else
            {
                Raster result = new Raster(left.Width, left.Height);
                byte l;

                for (int j = 0; j < left.Height; j++)
                {
                    for (int i = 0; i < left.Width; i++)
                    {
                        if (left[i, j] == right[i, j])
                            result[i, j] = VectorRgb.Grey;
                        else
                        {
                            l = VectorRgb.ClampByte(left[i, j].GetIntensity() - right[i, j].GetIntensity());
                            result[i, j] = new VectorRgb(l, l, l);
                        }
                    }
                }
                return result;
            }
        }
        /// <remarks>
        /// Declaring a conversion from System.Drawing.Bitmap to Raster. 
        /// Note the the use of the operator keyword. This is a conversion 
        /// operator named Raster
        /// </remarks>
        /// <summary>
        /// Conversion from System.Drawing.Bitmap to Cip.Foundations.Raster.
        /// </summary>
        /// <param name="bitmap">Source.</param>
        /// <returns>Destination.</returns>
        public static implicit operator Raster(System.Drawing.Bitmap bitmap)
        {
            return new Raster(bitmap);
        }
        /// <summary>
        /// Conversion from Cip.Foundations.Raster to System.Drawing.Bitmap.
        /// </summary>
        /// <param name="raster">Source.</param>
        /// <returns>Destination.</returns>
        public static explicit operator Bitmap(Cip.Foundations.Raster raster)
        {
            return ToBitmap(raster);
        }

        public static Raster operator -(Raster left, Raster right)
        {
            if ((left.Width != right.Width) || (left.Height != right.Height))
                return null;
            else
            {
                Raster result = new Raster(left.Width, left.Height);

                for (int j = 0; j < left.Height; j++)
                    for (int i = 0; i < left.Width; i++)
                        //result[i,j] = VectorRgb.Clamp(left[i, j] - right[i, j], 0, 255);
                        result[i, j] = left[i, j] - right[i, j];
                return result;
            }
        }
        public static Raster operator +(Raster left, Raster right)
        {
            if ((left.Width != right.Width) || (left.Height != right.Height))
                return null;
            else
            {
                Raster result = new Raster(left.Width, left.Height);

                for (int j = 0; j < left.Height; j++)
                    for (int i = 0; i < left.Width; i++)
                        //result[i, j] = VectorRgb.Clamp(left[i, j] + right[i, j], 0, 255);
                        result[i, j] = left[i, j] + right[i, j];
                return result;
            }
        }
        public static bool operator ==(Raster left, Raster right)
        {
            if ((object)right == null)
                if ((object)left == null)
                    return true;
                else
                    if (left.pixels != null) return false;
                    else return true;

            if ((left.Width != right.Width) || (left.Height != right.Height))
                return false;
            else
            {
                for (int j = 0; j < left.Height; j++)
                {
                    for (int i = 0; i < left.Width; i++)
                    {
                        if (left[i, j] != right[i, j])
                            return false;
                    }
                }
                return true;
            }
        }
        public static bool operator !=(Raster left, Raster right)
        {
            if ((object)right == null)
                if ((object)left == null)
                    return false;
                else
                    if (left.pixels != null) return true;
                    else return false;

            if ((left.Width != right.Width) || (left.Height != right.Height))
                return true;
            else
            {
                for (int j = 0; j < left.Height; j++)
                {
                    for (int i = 0; i < left.Width; i++)
                    {
                        if (left[i, j] != right[i, j])
                            return true;
                    }
                }
                return false;
            }
        }
        public override bool Equals(object obj)
        {
            if (this.GetType() != obj.GetType())
                return false;
            else
                if (this != (Raster)obj)
                    return false;
                else
                    return true;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion
    }
    /// <summary>
    /// Abstract filter for image processing.
    /// </summary>
    public abstract class ImageFilter
    {
        public abstract Raster ProcessRaster(Raster rOriginal, System.ComponentModel.BackgroundWorker worker);
        public abstract Raster ProcessWithoutWorker(Raster rOriginal);
    }
}
