/////////////////////////////////////////////////////////////////////////////////
// Colour Image Processing Library (CipLibNet)                                 //
// Copyright (C) Andrew [kyzmitch] Ermoshin.                                   //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Drawing;
using Cip;
using Cip.Filters;
using Cip.Foundations;

#pragma warning disable 1591

namespace Cip.Filters
{
    /// <remarks>
    /// Enhance the variations between adjacent pixels.
    /// </remarks>
    public class EdgeFilter : ImageFilter
    { 
        /// <summary>
        /// Range of mask (should be odd).
        /// </summary>
        private int n;

        public EdgeFilter(int radius)
        {
            n = radius * 2 + 1;
        }
        public override Raster ProcessRaster(Raster rOriginal, System.ComponentModel.BackgroundWorker worker)
        {
            worker.WorkerReportsProgress = true;
            byte r, g, b, rr, gg, bb;
            VectorRgb c;
            int width = rOriginal.Width;
            int height = rOriginal.Height;
            Raster raster = new Raster(width, height, Color.Black);
            DateTime startTime = DateTime.Now;
            //processing
            for (int i = 0; i < width; i++)
            {
                //moving by columns
                for (int j = 0; j < height; j++)
                {
                    r = g = b = 0;
                    rr = gg = bb = 255;
                    for (int k = i - (n - 1) / 2; k <= i + (n - 1) / 2; k++)
                    {
                        for (int l = j - (n - 1) / 2; l <= j + (n - 1) / 2; l++)
                        {
                            if (k >= 0 && k < width && l < height && l >= 0)
                            {
                                c = rOriginal[k, l];
                                if (c.R > r) r = c.R;
                                if (c.G > g) g = c.G;
                                if (c.B > b) b = c.B;

                                if (c.R < rr) rr = c.R;
                                if (c.G < gg) gg = c.G;
                                if (c.B < bb) bb = c.B;
                                
                            }
                        }
                    }
                    raster[i, j].R = (byte)(255 - Math.Abs(r - rr));
                    raster[i, j].G = (byte)(255 - Math.Abs(g - gg));
                    raster[i, j].B = (byte)(255 - Math.Abs(b - bb));
                }
                worker.ReportProgress((int)(100f * i / width), DateTime.Now - startTime);
            }
            return raster;
        }
        public override Raster ProcessWithoutWorker(Raster rOriginal)
        {
            throw new NotImplementedException();
        }
    }
    /// <remarks>
    /// Enhance the dark areas of the image.
    /// </remarks>
    public class ErodeFilter : ImageFilter
    {
        /// <summary>
        /// range of mask, should be odd.
        /// </summary>
        private int n;

        public ErodeFilter(int radius)
        {
            n = radius * 2 + 1;
        }
        public override Raster ProcessRaster(Raster rOriginal, System.ComponentModel.BackgroundWorker worker)
        {
            worker.WorkerReportsProgress = true;
            byte r, g, b;
            VectorRgb c;
            int width = rOriginal.Width;
            int height = rOriginal.Height;
            Raster raster = new Raster(width, height);
            DateTime startTime = DateTime.Now;
            //processing
            for (int i = 0; i < width; i++)
            {
                //moving by columns
                for (int j = 0; j < height; j++)
                {
                    r = g = b = 255;
                    for (int k = i - (n - 1) / 2; k <= i + (n - 1) / 2; k++)
                    {
                        for (int l = j - (n - 1) / 2; l <= j + (n - 1) / 2; l++)
                        {
                            if (k >= 0 && k < width && l < height && l >= 0)
                            {
                                c = rOriginal[k, l];
                                if (c.R < r) r = c.R;
                                if (c.G < g) g = c.G;
                                if (c.B < b) b = c.B;
                            }
                        }
                    }
                    raster[i, j] = new VectorRgb(r, g, b);
                }
                worker.ReportProgress((int)(100f * i / width), DateTime.Now - startTime);
            }
            return raster;
        }

        public override Raster ProcessWithoutWorker(Raster rOriginal)
        {
            throw new NotImplementedException();
        }
    }
    /// <remarks>
    /// Enhance the light areas of the image.
    /// </remarks>
    public class DilateFilter : ImageFilter
    {
        /// <summary>
        /// range of mask, should be odd.
        /// </summary>
        private int n;

        public DilateFilter(int radius)
        {
            n = radius * 2 + 1;
        }
        public override Raster ProcessRaster(Raster rOriginal, System.ComponentModel.BackgroundWorker worker)
        {
            worker.WorkerReportsProgress = true;
            byte r, g, b;
            VectorRgb c;
            int width = rOriginal.Width;
            int height = rOriginal.Height;
            Raster raster = new Raster(width, height);
            DateTime startTime = DateTime.Now;
            //processing
            for (int i = 0; i < width; i++)
            {
                //moving by columns
                for (int j = 0; j < height; j++)
                {
                    r = g = b = 0;
                    for (int k = i - (n - 1) / 2; k <= i + (n - 1) / 2; k++)
                    {
                        for (int l = j - (n - 1) / 2; l <= j + (n - 1) / 2; l++)
                        {
                            if (k >= 0 && k < width && l < height && l >= 0)
                            {
                                c = rOriginal[k, l];
                                if (c.R > r) r = c.R;
                                if (c.G > g) g = c.G;
                                if (c.B > b) b = c.B;
                            }
                        }
                    }
                    raster[i, j] = new VectorRgb(r, g, b);
                }
                worker.ReportProgress((int)(100f * i / width), DateTime.Now - startTime);
            }
            return raster;
        }

        public override Raster ProcessWithoutWorker(Raster rOriginal)
        {
            throw new NotImplementedException();
        }
    }
}
