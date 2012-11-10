/////////////////////////////////////////////////////////////////////////////////
// Colour Image Processing Library (CipLibNet)                                 //
// Copyright (C) Andrew [kyzmitch] Ermoshin.                                   //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using Cip;
using Cip.Foundations;

#pragma warning disable 1591

namespace Cip.Transformations
{
    /// <remarks>
    /// Enum for interpolation mode.
    /// </remarks>
    public enum CipInterpolationMode
    { 
        NearestPixel = 0,
        BicubicSpline = 1,
        Bilinear = 2,
    }
    /// <remarks>
    /// Size struct.
    /// </remarks>
    public struct CipSize
    {
        private int width;
        private int height;
        public CipSize(int width,int height)
        {
            this.width = width;
            this.height = height;
        }
        public CipSize(CipSize size)
        {
            this.width = size.width;
            this.height = size.height;
        }
        public CipSize(System.Drawing.Size size)
        {
            this.width = size.Width;
            this.height = size.Height;
        }
        public int Width
        {
            get { return this.width; }
            set { this.width = value; }
        }
        public int Height
        {
            get { return this.height; }
            set { this.height = value; }
        }
        public override string ToString()
        {
            return width.ToString() + 'x' + height.ToString();
        }
    }
    /// <remarks>
    /// Resizes the image.
    /// </remarks>
    public class Resample: ImageFilter
    {
        #region Fields

        /// <summary>
        /// mode can be 0 for slow (bilinear) method ,
        /// 1 for fast (nearest pixel) method, or 2 for accurate (bicubic spline interpolation) method.
        /// </summary>
        private Cip.Transformations.CipInterpolationMode mode;
        /// <summary>
        /// New width.
        /// </summary>
        private int newx;
        /// <summary>
        /// New height.
        /// </summary>
        private int newy;

        #endregion Fields

        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        /// <param name="mode">
        /// mode can be 0 for fast (nearest pixel) method, 
        /// 1 for accurate (bicubic spline interpolation) method, 
        /// or 2 for slow (bilinear) method.</param>
        /// <param name="newx">New width.</param>
        /// <param name="newy">New height.</param>
        public Resample(int newx, int newy, Cip.Transformations.CipInterpolationMode mode)
        {
            this.newx = newx;
            this.newy = newy;
            this.mode = mode;
        }
        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        /// <param name="mode">
        /// mode can be 0 for fast (nearest pixel) method, 
        /// 1 for accurate (bicubic spline interpolation) method, 
        /// or 2 for slow (bilinear) method.</param>
        /// <param name="newSize">New size.</param>
        public Resample(CipSize newSize, Cip.Transformations.CipInterpolationMode mode)
        {
            this.newx = newSize.Width;
            this.newy = newSize.Height;
            this.mode = mode;
        }
        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        /// <param name="mode">
        /// mode can be 0 for fast (nearest pixel) method, 
        /// 1 for accurate (bicubic spline interpolation) method, 
        /// or 2 for slow (bilinear) method.</param>
        /// <param name="newSize">New size.</param>
        public Resample(System.Drawing.Size newSize, Cip.Transformations.CipInterpolationMode mode)
        {
            this.newx = newSize.Width;
            this.newy = newSize.Height;
            this.mode = mode;
        }
        /// <summary>
        /// Processing resample.
        /// </summary>
        public override Raster ProcessRaster(Raster rOriginal, System.ComponentModel.BackgroundWorker worker)
        {
            worker.WorkerReportsProgress = true;
            DateTime startTime = DateTime.Now;

            if (newx == 0 || newy == 0) return null;

            if (rOriginal.Width == newx && rOriginal.Height == newy)
                return new Raster(rOriginal);//copy

            float xScale, yScale, fX, fY;
            xScale = (float)rOriginal.Width / (float)newx;
            yScale = (float)rOriginal.Height / (float)newy;

            Raster newImage = new Raster(newx, newy);

            switch (this.mode)
            {
                #region nearest pixel
                case Cip.Transformations.CipInterpolationMode.NearestPixel:
                    {
                        // nearest pixel
                        for (int y = 0; y < newy; y++)
                        {
                            fY = y * yScale;
                            for (int x = 0; x < newx; x++)
                            {
                                fX = x * xScale;
                                newImage[x, y] = rOriginal[(int)fX, (int)fY];
                            }
                            worker.ReportProgress((int)(100f * y / newy), DateTime.Now - startTime);
                        }
                        break;
                    }
                #endregion nearest pixel
                #region bicubic interpolation
                case Cip.Transformations.CipInterpolationMode.BicubicSpline:
                    {
                        // bicubic interpolation by Blake L. Carlson <blake-carlson(at)uiowa(dot)edu
                        float f_x, f_y, a, b, r1, r2;
                        byte rr, gg, bb;
                        int i_x, i_y, xx, yy;
                        VectorRgb rgb;

                        for (int y = 0; y < newy; y++)
                        {
                            f_y = (float)y * yScale - 0.5f;
                            i_y = (int) Math.Floor(f_y);
                            a = f_y - (float)Math.Floor(f_y);
                            for (int x = 0; x < newx; x++)
                            {
                                f_x = (float)x * xScale - 0.5f;
                                i_x = (int)Math.Floor(f_x);
                                b = f_x - (float)Math.Floor(f_x);

                                rr = gg = bb = 0;
                                for (int m = -1; m < 3; m++)
                                {
                                    r1 = CipInterpolationFunctions.KernelBSpline((float)m - a);
                                    yy = i_y + m;
                                    if (yy < 0) yy = 0;
                                    if (yy >= rOriginal.Height) yy = rOriginal.Height - 1;
                                    for (int n = -1; n < 3; n++)
                                    {
                                        r2 = r1 * CipInterpolationFunctions.KernelBSpline(b - (float)n);
                                        xx = i_x + n;
                                        if (xx < 0) xx = 0;
                                        if (xx >= rOriginal.Width) xx = rOriginal.Width - 1;

                                        rgb = rOriginal[xx, yy];

                                        rr += (byte) (rgb.R * r2);
                                        gg += (byte) (rgb.G * r2);
                                        bb += (byte) (rgb.B * r2);
                                    }//end for n
                                }//end for m
                                newImage[x, y] = new VectorRgb(rr, gg, bb);
                            }//end for x
                            worker.ReportProgress((int)(100f * y / newy), DateTime.Now - startTime);
                        }//end for y

                        break;
                    }
                #endregion bicubic interpolation
                #region bilinear interpolation
                case Cip.Transformations.CipInterpolationMode.Bilinear:
                    {
                        // bilinear interpolation
                        double fraction_x, fraction_y, one_minus_x, one_minus_y;
                        int ceil_x, ceil_y, floor_x, floor_y;
                        
                        VectorRgb c1 = new VectorRgb();
                        VectorRgb c2 = new VectorRgb();
                        VectorRgb c3 = new VectorRgb();
                        VectorRgb c4 = new VectorRgb();
                        byte red, green, blue;

                        byte b1, b2;

                        for (int x = 0; x < newx; ++x)
                        {
                            for (int y = 0; y < newy; ++y)
                            {
                                // Setup
                                floor_x = (int)Math.Floor(x * xScale);
                                floor_y = (int)Math.Floor(y * yScale);
                                ceil_x = floor_x + 1;
                                if (ceil_x >= rOriginal.Width) ceil_x = floor_x;
                                ceil_y = floor_y + 1;
                                if (ceil_y >= rOriginal.Height) ceil_y = floor_y;
                                fraction_x = x * xScale - floor_x;
                                fraction_y = y * yScale - floor_y;
                                one_minus_x = 1.0f - fraction_x;
                                one_minus_y = 1.0f - fraction_y;

                                c1 = rOriginal[floor_x, floor_y];
                                c2 = rOriginal[ceil_x, floor_y];
                                c3 = rOriginal[floor_x, ceil_y];
                                c4 = rOriginal[ceil_x, ceil_y];

                                // Blue
                                b1 = (byte)(one_minus_x * c1.B + fraction_x * c2.B);
                                b2 = (byte)(one_minus_x * c3.B + fraction_x * c4.B);
                                blue = (byte)(one_minus_y * (double)(b1) + fraction_y * (double)(b2));

                                // Green
                                b1 = (byte)(one_minus_x * c1.G + fraction_x * c2.G);
                                b2 = (byte)(one_minus_x * c3.G + fraction_x * c4.G);
                                green = (byte)(one_minus_y * (double)(b1) + fraction_y * (double)(b2));

                                // Red
                                b1 = (byte)(one_minus_x * c1.R + fraction_x * c2.R);
                                b2 = (byte)(one_minus_x * c3.R + fraction_x * c4.R);
                                red = (byte)(one_minus_y * (double)(b1) + fraction_y * (double)(b2));

                                newImage[x, y] = new VectorRgb(red, green, blue);
                            }
                            worker.ReportProgress((int)(100f * x / newx), DateTime.Now - startTime);
                        }
                        break;

                    }
                #endregion bilinear interpolation
                default:
                    {
                        // nearest pixel
                        for (int y = 0; y < newy; y++)
                        {
                            fY = y * yScale;
                            for (int x = 0; x < newx; x++)
                            {
                                fX = x * xScale;
                                newImage[x, y] = rOriginal[(int)fX, (int)fY];
                            }
                            worker.ReportProgress((int)(100f * y / newy), DateTime.Now - startTime);
                        }
                        break;
                    }
            }//end switch

            return newImage;
        }
        /// <summary>
        /// Processing resample without Background Worker.
        /// </summary>
        /// <param name="rOriginal"></param>
        /// <returns></returns>
        public override Raster ProcessWithoutWorker(Raster rOriginal)
        {
            if (newx == 0 || newy == 0) return null;

            if (rOriginal.Width == newx && rOriginal.Height == newy)
                return new Raster(rOriginal);//copy

            float xScale, yScale, fX, fY;
            int widthOriginal = rOriginal.Width;
            int heightOriginal = rOriginal.Height;

            xScale = (float)widthOriginal / (float)newx;
            yScale = (float)heightOriginal / (float)newy;

            Raster newImage = new Raster(newx, newy);

            switch (this.mode)
            {
                #region nearest pixel
                case Cip.Transformations.CipInterpolationMode.NearestPixel:
                    {
                        // nearest pixel
                        for (int y = 0; y < newy; y++)
                        {
                            fY = y * yScale;
                            for (int x = 0; x < newx; x++)
                            {
                                fX = x * xScale;
                                newImage[x, y] = rOriginal[(int)fX, (int)fY];
                            }
                        }
                        break;
                    }
                #endregion nearest pixel
                #region bicubic spline interpolation
                case Cip.Transformations.CipInterpolationMode.BicubicSpline:
                    {
                        // bicubic interpolation by Blake L. Carlson <blake-carlson(at)uiowa(dot)edu
                        float f_x, f_y, a, b, r1, r2;
                        byte rr, gg, bb;
                        int i_x, i_y, xx, yy;
                        VectorRgb rgb;

                        for (int y = 0; y < newy; y++)
                        {
                            f_y = (float)y * yScale - 0.5f;
                            i_y = (int)Math.Floor(f_y);
                            a = f_y - (float)Math.Floor(f_y);
                            for (int x = 0; x < newx; x++)
                            {
                                f_x = (float)x * xScale - 0.5f;
                                i_x = (int)Math.Floor(f_x);
                                b = f_x - (float)Math.Floor(f_x);

                                rr = gg = bb = 0;
                                for (int m = -1; m < 3; m++)
                                {
                                    r1 = CipInterpolationFunctions.KernelBSpline((float)m - a);
                                    yy = i_y + m;
                                    if (yy < 0) yy = 0;
                                    if (yy >= rOriginal.Height) yy = rOriginal.Height - 1;
                                    for (int n = -1; n < 3; n++)
                                    {
                                        r2 = r1 * CipInterpolationFunctions.KernelBSpline(b - (float)n);
                                        xx = i_x + n;
                                        if (xx < 0) xx = 0;
                                        if (xx >= rOriginal.Width) xx = rOriginal.Width - 1;

                                        rgb = rOriginal[xx, yy];

                                        rr += (byte)(rgb.R * r2);
                                        gg += (byte)(rgb.G * r2);
                                        bb += (byte)(rgb.B * r2);
                                    }//end for n
                                }//end for m
                                newImage[x, y] = new VectorRgb(rr, gg, bb);
                            }//end for x
                        }//end for y

                        break;
                    }
                #endregion bicubic spline interpolation
                #region bilinear interpolation
                case Cip.Transformations.CipInterpolationMode.Bilinear:
                    {
                        // bilinear interpolation
                        double fraction_x, fraction_y, one_minus_x, one_minus_y;
                        int ceil_x, ceil_y, floor_x, floor_y;

                        VectorRgb c1 = new VectorRgb();
                        VectorRgb c2 = new VectorRgb();
                        VectorRgb c3 = new VectorRgb();
                        VectorRgb c4 = new VectorRgb();
                        byte red, green, blue;

                        byte b1, b2;

                        for (int x = 0; x < newx; ++x)
                            for (int y = 0; y < newy; ++y)
                            {
                                // Setup
                                floor_x = (int)Math.Floor(x * xScale);
                                floor_y = (int)Math.Floor(y * yScale);
                                ceil_x = floor_x + 1;
                                if (ceil_x >= widthOriginal) ceil_x = floor_x;
                                ceil_y = floor_y + 1;
                                if (ceil_y >= heightOriginal) ceil_y = floor_y;
                                fraction_x = x * xScale - floor_x;
                                fraction_y = y * yScale - floor_y;
                                one_minus_x = 1.0f - fraction_x;
                                one_minus_y = 1.0f - fraction_y;

                                c1 = rOriginal[floor_x, floor_y];
                                c2 = rOriginal[ceil_x, floor_y];
                                c3 = rOriginal[floor_x, ceil_y];
                                c4 = rOriginal[ceil_x, ceil_y];

                                // Blue
                                b1 = (byte)(one_minus_x * c1.B + fraction_x * c2.B);
                                b2 = (byte)(one_minus_x * c3.B + fraction_x * c4.B);
                                blue = (byte)(one_minus_y * (double)(b1) + fraction_y * (double)(b2));

                                // Green
                                b1 = (byte)(one_minus_x * c1.G + fraction_x * c2.G);
                                b2 = (byte)(one_minus_x * c3.G + fraction_x * c4.G);
                                green = (byte)(one_minus_y * (double)(b1) + fraction_y * (double)(b2));

                                // Red
                                b1 = (byte)(one_minus_x * c1.R + fraction_x * c2.R);
                                b2 = (byte)(one_minus_x * c3.R + fraction_x * c4.R);
                                red = (byte)(one_minus_y * (double)(b1) + fraction_y * (double)(b2));

                                newImage[x, y] = new VectorRgb(red, green, blue);
                            }
                        break;

                    }
                #endregion bilinear interpolation
                default:// bilinear interpolation
                    {
                        // nearest pixel
                        for (int y = 0; y < newy; y++)
                        {
                            fY = y * yScale;
                            for (int x = 0; x < newx; x++)
                            {
                                fX = x * xScale;
                                newImage[x, y] = rOriginal[(int)fX, (int)fY];
                            }
                        }
                        break;
                    }
            }//end switch

            return newImage;
        }

        #region Scale Methods

        /// <summary>
        /// Scale size by size factor.
        /// </summary>
        /// <param name="nw">Current width.</param>
        /// <param name="nh">Current height.</param>
        /// <param name="sizeFactor">Factor.</param>
        /// <returns>New scaled size.</returns>
        public static CipSize SizeFactorScale(int nw, int nh, float sizeFactor)
        {
            double f = Convert.ToDouble(Math.Abs(sizeFactor));
            if (f == 0)
                return new CipSize(nw, nh);
            int w = (int)(nw * f);
            int h = (int)(nh * f);
            if (w != 0 && h != 0)
                return new CipSize(w, h);
            else
                return new CipSize(nw, nh);
        }
        /// <summary>
        /// Scale size by size factor.
        /// </summary>
        /// <param name="size">Current size.</param>
        /// <param name="sizeFactor">Factor.</param>
        /// <returns>New scaled size.</returns>
        public static CipSize SizeFactorScale(CipSize size, float sizeFactor)
        {
            return SizeFactorScale(size.Width, size.Height, sizeFactor);
        }
        /// <summary>
        /// Adapts current height to new width.
        /// </summary>
        /// <param name="oldSize">Current size.</param>
        /// <param name="nw">New width.</param>
        /// <returns>New scaled size.</returns>
        public static CipSize SizeAdaptHeight(CipSize oldSize, int nw)
        {
            if (oldSize.Height != 0)
            {
                float ration = (float)oldSize.Width / (float)oldSize.Height;
                int h = (int)(nw / ration + 0.5f);
                return new CipSize(nw, h);
            }
            else
                return oldSize;
        }
        /// <summary>
        /// Adapts current height to new width.
        /// </summary>
        /// <param name="oldSize">Current size.</param>
        /// <param name="nw">New width.</param>
        /// <returns>New scaled size.</returns>
        public static CipSize SizeAdaptHeight(Size oldSize, int nw)
        {
            return SizeAdaptHeight(new CipSize(oldSize), nw);
        }
        /// <summary>
        /// Adapts current width to new height.
        /// </summary>
        /// <param name="oldSize">Current size.</param>
        /// <param name="nh">New height.</param>
        /// <returns>New scaled size.</returns>
        public static CipSize SizeAdaptWidth(CipSize oldSize, int nh)
        {
            if (oldSize.Height != 0)
            {
                float ration = (float)oldSize.Width / (float)oldSize.Height;
                int w = (int)(nh * ration + 0.5f);
                return new CipSize(w, nh);
            }
            else
                return oldSize;
        }
        /// <summary>
        /// Adapts current width to new height.
        /// </summary>
        /// <param name="oldSize">Current size.</param>
        /// <param name="nh">New height.</param>
        /// <returns>New scaled size.</returns>
        public static CipSize SizeAdaptWidth(Size oldSize, int nh)
        {
            return SizeAdaptWidth(new CipSize(oldSize), nh);
        }

        #endregion Scale Methods

    }
    /// <remarks>
    /// GDI+ based methods for resizing images.
    /// </remarks>
    public static class ResampleGdi
    {
        /// <summary>
        /// Fast image resize.
        /// </summary>
        /// <param name="img">Target image.</param>
        /// <param name="newWidth">New width.</param>
        /// <param name="newHeight">New height.</param>
        /// <returns>Resized image.</returns>
        public static Image ResizeImage(Image img, int newWidth, int newHeight)
        {
            return img.GetThumbnailImage(newWidth, newHeight, null, IntPtr.Zero);
        }
        /// <summary>
        /// High-Quality image resize.
        /// </summary>
        /// <param name="img">Target image.</param>
        /// <param name="newWidth">New width.</param>
        /// <param name="newHeight">New height.</param>
        /// <returns>Resized image.</returns>
        public static Image ResizeImageHQ(Image img, int newWidth, int newHeight)
        {
            Image thumbImg = new Bitmap(newWidth, newHeight);

            using (Graphics gr = Graphics.FromImage(thumbImg))
            {
                gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                gr.DrawImage(
                    img,
                    new Rectangle(0, 0, newWidth, newHeight),
                    0, 0,
                    img.Width, img.Height,
                    GraphicsUnit.Pixel);
            }
            return thumbImg;
        }
        /// <summary>
        /// Image resize, using GDI+.
        /// </summary>
        /// <param name="img">Target image.</param>
        /// <param name="width">New width.</param>
        /// <param name="height">New height.</param>
        /// <param name="mode">Interpolation mode.</param>
        /// <returns>Resized image.</returns>
        public static Image ResizeImage(Image img, int width, int height, System.Drawing.Drawing2D.InterpolationMode mode)
        {
            Image thumbImg = new Bitmap(width, height);

            using (Graphics gr = Graphics.FromImage(thumbImg))
            {
                gr.InterpolationMode = mode;
                gr.DrawImage(
                    img,
                    new Rectangle(0, 0, width, height),
                    0, 0,
                    img.Width, img.Height,
                    GraphicsUnit.Pixel);
            }
            return thumbImg;
        }
    }

}
