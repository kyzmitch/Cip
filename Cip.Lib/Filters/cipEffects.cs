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
    /// Stamping filter.
    /// </remarks>
    public class StampingFilter : ImageFilter
    {
        public override Raster ProcessRaster(Raster rOriginal, System.ComponentModel.BackgroundWorker worker)
        {
            worker.WorkerReportsProgress = true;
            DateTime startTime = DateTime.Now;
            int width = rOriginal.Width;
            int height = rOriginal.Height;
            Raster raster;
            //stamping filter
            LinearFilter filter = Cip.Filters.LinearFilter.Stamping();
            raster = filter.ProcessRaster(rOriginal, worker);

            //VectorHSI hsi;
            int intensity;
            byte g;
            //convert to gray image and increase lightness
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    intensity = Cip.Foundations.ColorspaceHelper.RGB2GRAYI(raster[i, j]);
                    intensity += 128;
                    g = VectorRgb.ClampByte(intensity);
                    raster[i, j] = new VectorRgb(g, g, g);
                }
                worker.ReportProgress((int)(100f * j / height), DateTime.Now - startTime);
            }

            return raster;
        }
        public override Raster ProcessWithoutWorker(Raster rOriginal)
        {
            throw new NotImplementedException();
        }
    }
    /// <remarks>
    /// Bloom filter.
    /// </remarks>
    public class Bloom : ImageFilter
    {
        #region Private fields.

        /// <summary>
        /// intensity of bloom filter.
        /// </summary>
        private float bloomBlendFactor;
        /// <summary>
        /// level of light.
        /// </summary>
        private float lightThreshold;
        /// <summary>
        /// radius of blur filter.
        /// </summary>
        private int blurRadius;

        #endregion Private fields.

        #region Constructors.

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="blendFactor">intensity of bloom filter.</param>
        public Bloom(float blendFactor)
        {
            this.bloomBlendFactor = blendFactor;
            this.lightThreshold = 0.99f;
            this.blurRadius = 3;
        }
        /// <summary>
        /// Constructor with more parameters.
        /// </summary>
        /// <param name="blendFactor">intensity of bloom filter.</param>
        /// <param name="lightThreshold">level of light.</param>
        /// <param name="blurRadius">radius of blur filter.</param>
        public Bloom(float blendFactor, float lightThreshold, int blurRadius)
        {
            this.bloomBlendFactor = blendFactor;
            this.lightThreshold = lightThreshold;
            this.blurRadius = blurRadius;
        }

        #endregion Constructors.

        /// <summary>
        /// Function that apply bloom filter to raster.
        /// </summary>
        /// <param name="rOriginal">Original raster.</param>
        /// <param name="worker">Background worker.</param>
        /// <returns>Modifyed raster.</returns>
        public override Raster ProcessRaster(Raster rOriginal, System.ComponentModel.BackgroundWorker worker)
        {
            worker.WorkerReportsProgress = true;
            int width = rOriginal.Width;
            int height = rOriginal.Height;
            Raster raster = new Raster(width, height);

            DateTime startTime = DateTime.Now;
            byte threshold = (byte)(this.lightThreshold * 255);
            VectorRgb color;
            //bright pass
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    color = rOriginal[i, j];
                    if (color.R > threshold || color.G > threshold || color.B > threshold)
                        raster[i, j] = color;
                    else
                        raster[i, j] = new VectorRgb(0, 0, 0);
                }
                worker.ReportProgress((int)(100f * i / width), DateTime.Now - startTime);
            }
            //blur
            SmoothingFilter filterSmoothing = new SmoothingFilter(ColorSpaceMode.RGB, this.blurRadius);
            raster = filterSmoothing.ProcessRaster(raster, worker);
            //processing (mixing original raster with blured light raster)
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    color = raster[i, j] * this.bloomBlendFactor;
                    raster[i, j] = rOriginal[i, j] + color;
                }
                worker.ReportProgress((int)(100f * i / width), DateTime.Now - startTime);
            }
            return raster;
        }
        /// <summary>
        /// Processing without background worker.
        /// </summary>
        public override Raster ProcessWithoutWorker(Raster rOriginal)
        {
            int width = rOriginal.Width;
            int height = rOriginal.Height;
            Raster raster = new Raster(width, height);

            byte threshold = (byte)(this.lightThreshold * 255);
            VectorRgb color;
            //bright pass
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    color = rOriginal[i, j];
                    if (color.R > threshold || color.G > threshold || color.B > threshold)
                        raster[i, j] = color;
                    else
                        raster[i, j] = new VectorRgb(0, 0, 0);
                }
            }
            //blur
            SmoothingFilter filterSmoothing = new SmoothingFilter(ColorSpaceMode.RGB, this.blurRadius);
            raster = filterSmoothing.ProcessWithoutWorker(raster);
            //processing (mixing original raster with blured light raster)
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    color = raster[i, j] * this.bloomBlendFactor;
                    raster[i, j] = rOriginal[i, j] + color;
                }
            }
            return raster;
        }
    }
    /// <remarks>
    /// Sepia filter.
    /// </remarks>
    public class SepiaFilter : ImageFilter
    {
        /// <summary>
        /// Color of filter.
        /// </summary>
        private VectorRgb sepiacolor;
        /// <summary>
        /// Intensity coefficients of colour components.
        /// </summary>
        private VectorRgb luminance;
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="color">
        /// Color of filter (use Color.Empty to set def. color).
        /// </param>
        public SepiaFilter(System.Drawing.Color color)
        {
            luminance = new VectorRgb(0.3f, 0.59f, 0.11f);

            if (color == Color.Empty)
                sepiacolor = new VectorRgb(1.0f, 0.89f, 0.54f);
            else
                sepiacolor = new VectorRgb(color);
        }

        public override Raster ProcessRaster(Raster rOriginal, System.ComponentModel.BackgroundWorker worker)
        {
            worker.WorkerReportsProgress = true;
            int width = rOriginal.Width;
            int height = rOriginal.Height;
            Raster raster = new Raster(width, height);
            DateTime startTime = DateTime.Now;

            //processing
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                    raster[i, j] = sepiacolor * VectorRgb.Dot(rOriginal[i, j], luminance);
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
