/////////////////////////////////////////////////////////////////////////////////
// Colour Image Processing Library (CipLibNet)                                 //
// Copyright (C) Andrew [kyzmitch] Ermoshin.                                   //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
//using System.Drawing.Drawing2D;
using Cip.Foundations;

#pragma warning disable 1591

namespace Cip.Filters
{
    /// <remarks>
    /// Enum for splitter.
    /// </remarks>
    public enum SplitMode
    { 
        Hue = 0,
        Saturation = 1,
        Intensity = 2,
        Red = 3,
        Green = 4,
        Blue = 5,
    }
    /// <remarks>
    /// Gray Scale Filter.
    /// </remarks>
    public class GrayScale : ImageFilter
    {
        public override Raster ProcessRaster(Raster rOriginal, BackgroundWorker worker)
        {
            worker.WorkerReportsProgress = true;
            int width = rOriginal.Width;
            int height = rOriginal.Height;
            Raster raster = new Raster(width, height);
            //VectorHSI hsi;
            //float intensity;
            DateTime startTime = DateTime.Now;

            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    /*hsi = rOriginal[i, j].ToVectorHSI();
                    intensity = hsi.I;
                    raster[i, j] = new VectorRGB(intensity, intensity, intensity);*/
                    raster[i, j] = rOriginal[i, j].ToMonochrome();
                }
                worker.ReportProgress((int)(100f * j / height), DateTime.Now - startTime);
            }
            return raster;
        }
        public override Raster ProcessWithoutWorker(Raster rOriginal)
        {
            int width = rOriginal.Width;
            int height = rOriginal.Height;
            Raster raster = new Raster(width, height);
            //VectorHSI hsi;
            //float intensity;

            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    //hsi = rOriginal[i, j].ToVectorHSI();
                    //intensity = hsi.I;
                    //raster[i, j] = new VectorRGB(intensity, intensity, intensity);
                    raster[i, j] = rOriginal[i, j].ToMonochrome();
                }
            }
            return raster;
        }
    }
    /// <remarks>
    /// Split HSI and RGB components
    /// </remarks>
    public class SplitterFilter : ImageFilter
    {
        /// <summary>
        /// Split component.
        /// </summary>
        private SplitMode mode;
        /// <summary>
        /// Default constructor.
        /// </summary>
        public SplitterFilter()
        {
            mode = SplitMode.Hue;
        }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="Mode">Split component.</param>
        public SplitterFilter(SplitMode Mode)
        {
            mode = Mode;
        }
        /// <summary>
        /// Get mode of split filter.
        /// </summary>
        public SplitMode SplitMode
        {
            get { return this.mode; }
        }
        public override Raster ProcessRaster(Raster rOriginal, BackgroundWorker worker)
        {
            worker.WorkerReportsProgress = true;
            int width = rOriginal.Width;
            int height = rOriginal.Height;
            Raster raster = new Raster(width, height);
            VectorHsi hsi;
            DateTime startTime = DateTime.Now;

            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    switch (mode)
                    {
                        case SplitMode.Hue:
                            {
                                hsi = rOriginal[i, j].ToVectorHSI();
                                raster[i, j] = new VectorRgb(hsi.H, hsi.H, hsi.H);
                                break;
                            }
                        case SplitMode.Saturation:
                            {
                                hsi = rOriginal[i, j].ToVectorHSI();
                                raster[i, j] = new VectorRgb(hsi.S, hsi.S, hsi.S);
                                break;
                            }
                        case SplitMode.Intensity:
                            {
                                hsi = rOriginal[i, j].ToVectorHSI();
                                raster[i, j] = new VectorRgb(hsi.I, hsi.I, hsi.I);
                                break;
                            }
                        case SplitMode.Red:
                            {
                                raster[i, j] = new VectorRgb(rOriginal[i, j].R, rOriginal[i, j].R, rOriginal[i, j].R);
                                break;
                            }
                        case SplitMode.Green:
                            {
                                raster[i, j] = new VectorRgb(rOriginal[i, j].G, rOriginal[i, j].G, rOriginal[i, j].G);
                                break;
                            }
                        case SplitMode.Blue:
                            {
                                raster[i, j] = new VectorRgb(rOriginal[i, j].B, rOriginal[i, j].B, rOriginal[i, j].B);
                                break;
                            }
                    }
                }
                worker.ReportProgress((int)(100f * j / height), DateTime.Now - startTime);
            }
            return raster;
        }
        public override Raster ProcessWithoutWorker(Raster rOriginal)
        {
            throw new NotImplementedException();
        }
        public Raster ProcessWithProgress(Raster rOriginal, ToolStripProgressBar progress)
        {
            double progr = 0;
            int width = rOriginal.Width;
            int height = rOriginal.Height;
            Raster raster = new Raster(width, height);
            VectorHsi hsi;

            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    switch (mode)
                    {
                        case SplitMode.Hue:
                            {
                                hsi = rOriginal[i, j].ToVectorHSI();
                                raster[i, j] = new VectorRgb(hsi.H, hsi.H, hsi.H);
                                break;
                            }
                        case SplitMode.Saturation:
                            {
                                hsi = rOriginal[i, j].ToVectorHSI();
                                raster[i, j] = new VectorRgb(hsi.S, hsi.S, hsi.S);
                                break;
                            }
                        case SplitMode.Intensity:
                            {
                                hsi = rOriginal[i, j].ToVectorHSI();
                                raster[i, j] = new VectorRgb(hsi.I, hsi.I, hsi.I);
                                break;
                            }
                        case SplitMode.Red:
                            {
                                raster[i, j] = new VectorRgb(rOriginal[i, j].R, rOriginal[i, j].R, rOriginal[i, j].R);
                                break;
                            }
                        case SplitMode.Green:
                            {
                                raster[i, j] = new VectorRgb(rOriginal[i, j].G, rOriginal[i, j].G, rOriginal[i, j].G);
                                break;
                            }
                        case SplitMode.Blue:
                            {
                                raster[i, j] = new VectorRgb(rOriginal[i, j].B, rOriginal[i, j].B, rOriginal[i, j].B);
                                break;
                            }
                    }
                }
                progr += (double)progress.Maximum / height;
                if (progress.Value < (int)progr)
                    progress.Value++;
            }
            progress.Value = 0;
            return raster;
        }
    }
    /// <remarks>
    /// Colour addition filter (RGB and HSI colorspaces).
    /// </remarks>
    public class ColorAddition : ImageFilter
    {
        /// <summary>
        /// Mode of method (RGB or HSI).
        /// </summary>
        private ColorSpaceMode mode;
        /// <summary>
        /// Constructor.
        /// </summary>
        public ColorAddition() { mode = ColorSpaceMode.RGB; }
        /// <summary>
        /// Parametrized constructor.
        /// </summary>
        /// <param name="Mode">Mode of method (RGB or HSI).</param>
        public ColorAddition(ColorSpaceMode Mode) { mode = Mode; }
        /// <summary>
        /// Color addition processing.
        /// </summary>
        public override Raster ProcessRaster(Raster rOriginal, System.ComponentModel.BackgroundWorker worker)
        {
            worker.WorkerReportsProgress = true;
            int width = rOriginal.Width;
            int height = rOriginal.Height;
            int i, j;
            float H;
            Raster raster = new Raster(width, height);
            VectorHsi hsi;
            DateTime startTime = DateTime.Now;

            switch (mode)
            {
                case ColorSpaceMode.RGB:
                    {
                        for (j = 0; j < height; j++)
                        {
                            for (i = 0; i < width; i++)
                            {
                                raster[i, j] = new VectorRgb(255 - rOriginal[i, j].R,
                                                             255 - rOriginal[i, j].G,
                                                             255 - rOriginal[i, j].B);
                            }
                            worker.ReportProgress((int)(100f * j / height), DateTime.Now - startTime);
                        }
                        break;
                    }
                case ColorSpaceMode.HSI:
                    {
                        for (j = 0; j < height; j++)
                        {
                            for (i = 0; i < width; i++)
                            {
                                hsi = rOriginal[i, j].ToVectorHSI();
                                H = 0;
                                //hue
                                if (hsi.H >= 0 && hsi.H <= 0.5)
                                    H = 0.5f + hsi.H;
                                else
                                {
                                    if (hsi.H > 0.5 && hsi.H <= 1.0)
                                        H = hsi.H - 0.5f;
                                }
                                hsi.H = H;
                                //saturation will be the same
                                hsi.I = 1f - hsi.I;
                                raster[i, j] = hsi.ToVectorRGB();
                            }
                            worker.ReportProgress((int)(100f * j / height), DateTime.Now - startTime);
                        }
                        break;
                    }
            }
            return raster;
        }

        public override Raster ProcessWithoutWorker(Raster rOriginal)
        {
            int width = rOriginal.Width;
            int height = rOriginal.Height;
            int i, j;
            float H;
            Raster raster = new Raster(width, height);
            VectorHsi hsi;

            switch (mode)
            {
                case ColorSpaceMode.RGB:
                    {
                        for (j = 0; j < height; j++)
                        {
                            for (i = 0; i < width; i++)
                            {
                                raster[i, j] = new VectorRgb(255 - rOriginal[i, j].R,
                                                             255 - rOriginal[i, j].G,
                                                             255 - rOriginal[i, j].B);
                            }
                        }
                        break;
                    }
                case ColorSpaceMode.HSI:
                    {
                        for (j = 0; j < height; j++)
                        {
                            for (i = 0; i < width; i++)
                            {
                                hsi = rOriginal[i, j].ToVectorHSI();
                                H = 0;
                                //hue
                                if (hsi.H >= 0 && hsi.H <= 0.5)
                                    H = 0.5f + hsi.H;
                                else
                                {
                                    if (hsi.H > 0.5 && hsi.H <= 1.0)
                                        H = hsi.H - 0.5f;
                                }
                                hsi.H = H;
                                //saturation will be the same
                                hsi.I = 1f - hsi.I;
                                raster[i, j] = hsi.ToVectorRGB();
                            }
                        }
                        break;
                    }
            }
            return raster;
        }

        public Bitmap ProcessBitmapWithoutWorker(Bitmap sourceBmp)
        {
            int width = sourceBmp.Width;
            int height = sourceBmp.Height;
            Bitmap resultBmp = (Bitmap)sourceBmp.Clone();

            BitmapDecorator sourceDec = new BitmapDecorator(sourceBmp);
            BitmapDecorator resultDec = new BitmapDecorator(resultBmp);
            Color clr;
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    clr = sourceDec.GetPixel(i, j);
                    resultDec.SetPixel(i, j,
                                       255 - clr.R,
                                       255 - clr.G,
                                       255 - clr.B);
                }
            }
            sourceDec.Dispose();
            resultDec.Dispose();

            return resultBmp;
        }
    }
}
