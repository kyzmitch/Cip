/////////////////////////////////////////////////////////////////////////////////
// Colour Image Processing Library (CipLibNet)                                 //
// Copyright (C) Andrew [kyzmitch] Ermoshin.                                   //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Drawing;
using Cip.Foundations;
using Cip;

#pragma warning disable 1591

namespace Cip.Filters
{
    #region Enums
    /// <summary>
    /// Mode of the histogram method.
    /// </summary>
    public enum HistogramMode
    { 
        /// <summary>
        /// Histogram equalize.
        /// </summary>
        Equalize = 0,
        /// <summary>
        /// Histogram normalize.
        /// </summary>
        Normalize = 1,
        /// <summary>
        /// Histogram stretch luminance.
        /// </summary>
        StretchLuminance = 2,
        /// <summary>
        /// Histogram stretch linked channels.
        /// </summary>
        StretchLinkedChannels = 3,
        /// <summary>
        /// Histogram stretch independent channels.
        /// </summary>
        StretchIndependentChannels = 4,
    }
    /// <remarks>
    /// Mode of the intensity correction.
    /// </remarks>
    public enum IntensityCorrectionMode
    {
        /// <summary>
        /// Soft Image.
        /// </summary>
        SoftImage = 0,
        /// <summary>
        /// Light Image.
        /// </summary>
        LightImage = 1,
        /// <summary>
        /// Dark Image.
        /// </summary>
        DarkImage = 2,
    }
    /// <remarks>
    /// Mode of the colorspace.
    /// </remarks>
    public enum ColorSpaceMode
    { 
        /// <summary>
        /// Hue, saturation and intensity.
        /// </summary>
        HSI = 0,
        /// <summary>
        /// Red, green and blue.
        /// </summary>
        RGB = 1,
    }
    /// <summary>
    /// Mode of the smoothing.
    /// </summary>
    public enum SmoothingMode
    {
        /// <summary>
        /// Smoothing.
        /// </summary>
        Smoothing = 0,
        /// <summary>
        /// Simple blur.
        /// </summary>
        Blur = 1,
        /// <summary>
        /// Gaussian blur.
        /// </summary>
        GaussianBlur = 2,
    }

    #endregion

    /// <remarks>
    /// Changes hue, saturation and intensity.
    /// </remarks>
    public class HsiCorrectionFilter : ImageFilter
    {
        /// <summary>
        /// Hue.
        /// </summary>
        private int hue;
        /// <summary>
        /// Intensity.
        /// </summary>
        private int intensity;
        /// <summary>
        /// Saturation.
        /// </summary>
        private int saturation;
        /// <summary>
        /// Default constructor.
        /// </summary>
        public HsiCorrectionFilter()
        {
            this.intensity = 30;
            this.saturation = 0;
            this.hue = 0;
        }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="intensity">Intensity.</param>
        /// <param name="saturation">Saturation.</param>
        public HsiCorrectionFilter(int intensity, int saturation)
        {
            this.intensity = intensity;
            this.saturation = saturation;
        }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="intensity">Intensity.</param>
        /// <param name="saturation">Saturation.</param>
        /// <param name="hue">Hue.</param>
        public HsiCorrectionFilter(int intensity, int saturation, int hue)
        {
            this.intensity = intensity;
            this.saturation = saturation;
            this.hue = hue;
        }
        /// <summary>
        /// Filter processing, with BackgroundWorker.
        /// </summary>
        /// <param name="rOriginal">Original raster.</param>
        /// <param name="worker">BackgroundWorker.</param>
        /// <returns>Raster.</returns>
        public override Raster ProcessRaster(Raster rOriginal, System.ComponentModel.BackgroundWorker worker)
        {
            worker.WorkerReportsProgress = true;
            int width = rOriginal.Width;
            int height = rOriginal.Height;

            Raster raster = new Raster(width, height);
            VectorHsi hsi;
            float fIntensity = (float)this.intensity / 255f;
            float fSaturation = (float)this.saturation / 255f;
            float fHue = (float)this.hue / 255f;

            DateTime startTime = DateTime.Now;
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    hsi = new VectorHsi(rOriginal[i, j]);
                    hsi.ChangeSaturation(fSaturation);
                    hsi.ChangeIntensity(fIntensity);
                    hsi.ChangeHue(fHue);
                    hsi = VectorHsi.Clamp(hsi, 0.0f, 1.0f);
                    raster[i, j] = hsi.ToVectorRGB();
                }
                worker.ReportProgress((int)(100f * j / height), DateTime.Now - startTime);
            }
            
            return raster;
        }
        /// <summary>
        /// Filter processing.
        /// </summary>
        /// <param name="rOriginal">Original raster.</param>
        /// <returns>Raster.</returns>
        public override Raster ProcessWithoutWorker(Raster rOriginal)
        {
            int width = rOriginal.Width;
            int height = rOriginal.Height;

            Raster raster = new Raster(width, height);
            VectorHsi hsi;
            float fIntensity = (float)this.intensity / 255f;
            float fSaturation = (float)this.saturation / 255f;
            float fHue = (float)this.hue / 255f;

            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    hsi = new VectorHsi(rOriginal[i, j]);
                    hsi.ChangeSaturation(fSaturation);
                    hsi.ChangeIntensity(fIntensity);
                    hsi.ChangeHue(fHue);
                    hsi = VectorHsi.Clamp(hsi, 0f, 1f);
                    raster[i, j] = hsi.ToVectorRGB();
                }
            }

            return raster;
        }
    }
    /// <remarks>
    /// Changes lightness and contrast.
    /// </remarks>
    public class LightFilter : ImageFilter
    {
        /// <summary>
        /// Can be from -255 to 255, if brightness is negative, the image becomes dark.
        /// </summary>
        private int brightness;
        /// <summary>
        /// Can be from -100 to 100, the neutral value is 0.
        /// </summary>
        private int contrast;

        public LightFilter()
        {
            brightness = 10;
            contrast = 0;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="iBrightness">Can be from -255 to 255</param>
        /// <param name="iContrast">Can be from -100 to 100</param>
        public LightFilter(int iBrightness, int iContrast)
        {
            if ((iBrightness >= -255) && (iBrightness <= 255) && (iContrast >= -100) && (iContrast <= 100))
            {
                brightness = iBrightness;
                contrast = iContrast;
            }
            else
            {
                brightness = 10;
                contrast = 0;
            }
        }
        public override Raster ProcessRaster(Raster rOriginal, System.ComponentModel.BackgroundWorker worker)
        {
            worker.WorkerReportsProgress = true;
            int width = rOriginal.Width;
            int height = rOriginal.Height;
            Raster raster = new Raster(width, height);
            DateTime startTime = DateTime.Now;

            //processing
            float c = (100 + contrast) / 100.0f;
            brightness += 128;
            byte[] cTable = new byte[256];
            for (int i = 0; i < 256; i++)
            {
                cTable[i] = (byte)Math.Max(0, Math.Min(255, (int)((i - 128) * c + brightness + 0.5f)));
            }
            VectorRgb newcolor = new VectorRgb();;
            Color cl;

            //image processing
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    newcolor = new VectorRgb();
                    cl = rOriginal[i, j].ToColor();
                    newcolor.R = cTable[cl.R];
                    newcolor.G = cTable[cl.G];
                    newcolor.B = cTable[cl.B];
                    raster[i, j] = newcolor;
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

            //processing
            float c = (100 + contrast) / 100.0f;
            brightness += 128;
            byte[] cTable = new byte[256];
            for (int i = 0; i < 256; i++)
            {
                cTable[i] = (byte)Math.Max(0, Math.Min(255, (int)((i - 128) * c + brightness + 0.5f)));
            }
            VectorRgb newcolor = new VectorRgb(); ;
            Color cl;

            //image processing
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    newcolor = new VectorRgb();
                    cl = rOriginal[i, j].ToColor();
                    newcolor.R = cTable[cl.R];
                    newcolor.G = cTable[cl.G];
                    newcolor.B = cTable[cl.B];
                    raster[i, j] = newcolor;
                }
            }
            return raster;
        }

    }
    /// <remarks>
    /// Change intensity in RGB colorspace.
    /// </remarks>
    public class IntensityChanger : ImageFilter
    {
        /// <summary>
        /// Level of change.
        /// </summary>
        private int level;

        public IntensityChanger()
        {
            this.level = 30;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Level">level of change</param>
        public IntensityChanger(int Level)
        {
            level = Level;
        }

        public override Raster ProcessRaster(Raster rOriginal, System.ComponentModel.BackgroundWorker worker)
        {
            worker.WorkerReportsProgress = true;
            int width = rOriginal.Width;
            int height = rOriginal.Height;
            Raster raster = new Raster(width, height);
            DateTime startTime = DateTime.Now;

            //processing
                for (int j = 0; j < height; j++)
                {
                    for (int i = 0; i < width; i++)
                        raster[i, j] = rOriginal[i, j] + this.level;
                    worker.ReportProgress((int)(100f * j / height), DateTime.Now - startTime);
                }

            return raster;
        }
        
        public override Raster ProcessWithoutWorker(Raster rOriginal)
        {
            int width = rOriginal.Width;
            int height = rOriginal.Height;
            Raster raster = new Raster(width, height);

            //processing
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                    raster[i, j] = rOriginal[i, j] + this.level;
            }
            return raster;
        }
    }
    /// <remarks>
    /// Intensity correction filter.
    /// </remarks>
    public class IntensityCorrection : ImageFilter
    {
        /// <summary>
        /// type of image
        /// </summary>
        private IntensityCorrectionMode mode;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Mode">type of image</param>
        public IntensityCorrection(IntensityCorrectionMode Mode){mode = Mode;}
        /// <summary>
        /// Delegare for correction function
        /// </summary>
        /// <param name="value">double value</param>
        /// <returns>function value</returns>
        private delegate float CorrectionFunction(float value);
        /// <summary>
        /// Cosine func
        /// </summary>
        /// <param name="value">double value</param>
        /// <returns>function value</returns>
        private static float SoftImageCorrection(float value)
        {
            //OMG:)
            float y = (float) (1 + Math.Cos(Math.PI + value * Math.PI)) / 2;
            return y;
        }
        /// <summary>
        /// Pow func upper 1
        /// </summary>
        /// <param name="value">double value</param>
        /// <returns>function value</returns>
        private static float LightImageCorrection(float value)
        {
            return (float)Math.Pow(value, 2.5f);
        }
        /// <summary>
        /// Pow func lower 1
        /// </summary>
        /// <param name="value">double value</param>
        /// <returns>function value</returns>
        private static float DarkImageCorrection(float value)
        {
            return (float) Math.Pow(value, (float)1 / 4);
        }

        public override Raster ProcessRaster(Raster rOriginal, System.ComponentModel.BackgroundWorker worker)
        {
            worker.WorkerReportsProgress = true;
            int width = rOriginal.Width;
            int height = rOriginal.Height;
            Raster raster = new Raster(width, height);
            CorrectionFunction func;
            float r, g, b;
            DateTime startTime = DateTime.Now;
            //definition of used function
            //delegates is defined as a named methods.
            switch (mode)
            {
                case IntensityCorrectionMode.LightImage:
                    {
                        func = new CorrectionFunction(LightImageCorrection);
                        //INFORMATION: or use anonymous delegate
                        //func = delegate(float value) {return (float)Math.Pow(value, 2.5f);}
                        break;
                    }
                case IntensityCorrectionMode.DarkImage:
                    {
                        func = new CorrectionFunction(DarkImageCorrection);

                        break;
                    }
                case IntensityCorrectionMode.SoftImage:
                    {
                        func = new CorrectionFunction(SoftImageCorrection);
                        break;
                    }
                default:
                    {
                        func = new CorrectionFunction(SoftImageCorrection);
                        break;
                    }
            }

            //processing
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    r = func(rOriginal[i, j].R / 255.0f);
                    g = func(rOriginal[i, j].G / 255.0f);
                    b = func(rOriginal[i, j].B / 255.0f);
                    raster[i, j] = new VectorRgb(r, g, b);
                }
                worker.ReportProgress((int)(100f * j / height), DateTime.Now - startTime);
            }
            return raster;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rOriginal"></param>
        /// <returns></returns>
        public override Raster ProcessWithoutWorker(Raster rOriginal)
        {
            int width = rOriginal.Width;
            int height = rOriginal.Height;
            Raster raster = new Raster(width, height);
            CorrectionFunction func;
            float r, g, b;
            //definition of used function
            //delegates is defined as a named methods.
            switch (mode)
            {
                case IntensityCorrectionMode.LightImage:
                    {
                        func = new CorrectionFunction(LightImageCorrection);
                        //INFORMATION: or use anonymous delegate
                        //func = delegate(float value) {return (float)Math.Pow(value, 2.5f);}
                        break;
                    }
                case IntensityCorrectionMode.DarkImage:
                    {
                        func = new CorrectionFunction(DarkImageCorrection);

                        break;
                    }
                case IntensityCorrectionMode.SoftImage:
                    {
                        func = new CorrectionFunction(SoftImageCorrection);
                        break;
                    }
                default:
                    {
                        func = new CorrectionFunction(SoftImageCorrection);
                        break;
                    }
            }

            //processing
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    r = func(rOriginal[i, j].R / 255.0f);
                    g = func(rOriginal[i, j].G / 255.0f);
                    b = func(rOriginal[i, j].B / 255.0f);
                    raster[i, j] = new VectorRgb(r, g, b);
                }
            }
            return raster;
        }
    }
    /// <remarks>
    /// Equalize histogram.
    /// </remarks>
    public class HistogramEqualization : ImageFilter
    {
        public override Raster ProcessRaster(Raster rOriginal, System.ComponentModel.BackgroundWorker worker)
        {
            worker.WorkerReportsProgress = true;
            int width = rOriginal.Width;
            int height = rOriginal.Height;
            int iIntensity;
            Raster raster = new Raster(width, height);
            VectorHsi hsi;
            DateTime startTime = DateTime.Now;

            //get array
            float[] HistogramNormalized = CipTools.GetHistogramNormalized(rOriginal);

            //calculates raster
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    hsi = rOriginal[i, j].ToVectorHSI();
                    //intensity component
                    iIntensity = (int)(hsi.I * 255f);
                    hsi.I = HistogramNormalized[iIntensity];
                    raster[i, j] = new VectorRgb(hsi);
                }
                worker.ReportProgress((int)(100f * j / height), DateTime.Now - startTime);
            }

            return raster;
        }
        public override Raster ProcessWithoutWorker(Raster rOriginal)
        {
            int width = rOriginal.Width;
            int height = rOriginal.Height;
            int iIntensity;
            Raster raster = new Raster(width, height);
            VectorHsi hsi;

            //get array
            float[] HistogramNormalized = CipTools.GetHistogramNormalized(rOriginal);

            //calculates raster
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    hsi = rOriginal[i, j].ToVectorHSI();
                    //intensity component
                    iIntensity = (int)(hsi.I * 255f);
                    hsi.I = HistogramNormalized[iIntensity];
                    raster[i, j] = new VectorRgb(hsi);
                }
            }

            return raster;
        }
    }
    /// <remarks>
    /// Normalize histogram.
    /// </remarks>
    public class HistogramNormalize : ImageFilter
    {
        public override Raster ProcessRaster(Raster rOriginal, System.ComponentModel.BackgroundWorker worker)
        {
            worker.WorkerReportsProgress = true;
            int width = rOriginal.Width;
            int height = rOriginal.Height;
            Raster raster = new Raster(width, height);
            DateTime startTime = DateTime.Now;
            
            int[] histogram = new int[256];
            int threshold_intensity, intense;
            int i;
            uint[] normalize_map = new uint[256];
            uint high, low, YVal;
            VectorRgb yuvClr;

            // form histogram
            for (int j = 0; j < height; j++)
                for (int k = 0; k < width; k++)
                {
                    YVal = (uint)ColorspaceHelper.RGB2GRAY(rOriginal[k, j]);
                    histogram[YVal]++;
                }

            // find histogram boundaries by locating the 1 percent levels
            threshold_intensity = (int) (width * height / 100);

            intense = 0;
            for (low = 0; low < 255; low++)
            {
                intense += histogram[low];
                if (intense > threshold_intensity) break;
            }

            intense = 0;
            for (high = 255; high != 0; high--)
            {
                intense += histogram[high];
                if (intense > threshold_intensity) break;
            }

            if (low == high)
            {
                // Unreasonable contrast;  use zero threshold to determine boundaries.
                threshold_intensity = 0;
                intense = 0;
                for (low = 0; low < 255; low++)
                {
                    intense += histogram[low];
                    if (intense > threshold_intensity) break;
                }
                intense = 0;
                for (high = 255; high != 0; high--)
                {
                    intense += histogram[high];
                    if (intense > threshold_intensity) break;
                }
            }
            if (low == high) return null;  // zero span bound

            // Stretch the histogram to create the normalized image mapping.
            for (i = 0; i <= 255; i++)
            {
                if (i < (int)low)
                {
                    normalize_map[i] = 0;
                }
                else
                {
                    if (i > (int)high)
                        normalize_map[i] = 255;
                    else
                        normalize_map[i] = (uint)((255 - 1) * (i - low) / (high - low));
                }
            }

            // Normalize
            for (int j = 0; j < height; j++)
            {
                for (int k = 0; k < width; k++)
                {
                    yuvClr = ColorspaceHelper.RGB2YUV(rOriginal[k, j]);
                    yuvClr.R = (byte) normalize_map[yuvClr.R];
                    raster[k, j] = ColorspaceHelper.YUV2RGB(yuvClr);
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

            int[] histogram = new int[256];
            int threshold_intensity, intense;
            int i;
            uint[] normalize_map = new uint[256];
            uint high, low, YVal;
            VectorRgb yuvClr;

            // form histogram
            for (int j = 0; j < height; j++)
                for (int k = 0; k < width; k++)
                {
                    YVal = (uint)ColorspaceHelper.RGB2GRAY(rOriginal[k, j]);
                    histogram[YVal]++;
                }

            // find histogram boundaries by locating the 1 percent levels
            threshold_intensity = (int)(width * height / 100);

            intense = 0;
            for (low = 0; low < 255; low++)
            {
                intense += histogram[low];
                if (intense > threshold_intensity) break;
            }

            intense = 0;
            for (high = 255; high != 0; high--)
            {
                intense += histogram[high];
                if (intense > threshold_intensity) break;
            }

            if (low == high)
            {
                // Unreasonable contrast;  use zero threshold to determine boundaries.
                threshold_intensity = 0;
                intense = 0;
                for (low = 0; low < 255; low++)
                {
                    intense += histogram[low];
                    if (intense > threshold_intensity) break;
                }
                intense = 0;
                for (high = 255; high != 0; high--)
                {
                    intense += histogram[high];
                    if (intense > threshold_intensity) break;
                }
            }
            if (low == high) return null;  // zero span bound

            // Stretch the histogram to create the normalized image mapping.
            for (i = 0; i <= 255; i++)
            {
                if (i < (int)low)
                {
                    normalize_map[i] = 0;
                }
                else
                {
                    if (i > (int)high)
                        normalize_map[i] = 255;
                    else
                        normalize_map[i] = (uint)((255 - 1) * (i - low) / (high - low));
                }
            }

            // Normalize
            for (int j = 0; j < height; j++)
            {
                for (int k = 0; k < width; k++)
                {
                    yuvClr = ColorspaceHelper.RGB2YUV(rOriginal[k, j]);
                    yuvClr.R = (byte) normalize_map[yuvClr.R];
                    raster[k, j] = ColorspaceHelper.YUV2RGB(yuvClr);
                    //raster[k, j] = yuvClr;
                }
            }
            return raster;
        }
    }
    /// <remarks>
    /// Histogram stretch.
    /// author [dave] and [nipper]; changes [DP] and me [Kyzmitch].
    /// </remarks>
    public class HistogramStretch : ImageFilter
    {
        /// <summary>
        /// param method: 0 = luminance (default), 1 = linked channels , 2 = independent channels.
        /// </summary>
        private int method;
        /// <summary>
        /// param threshold: minimum percentage level in the histogram to recognize it as meaningful. Range: 0.0 to 1.0; default = 0; typical = 0.005 (0.5%).
        /// </summary>
        private double threshold;
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="method">
        /// param method: 0 = luminance (default), 1 = linked channels , 2 = independent channels.
        /// </param>
        /// <param name="threshold">
        /// param threshold: minimum percentage level in the histogram to recognize it as meaningful. Range: 0.0 to 1.0; default = 0; typical = 0.005 (0.5%).
        /// </param>
        public HistogramStretch(int method, double threshold)
        {
            this.method = method;
            this.threshold = threshold;
        }
        public HistogramStretch()
        {
            this.method = 0;
            this.threshold = 0.005;
        }
        public override Raster ProcessRaster(Raster rOriginal, System.ComponentModel.BackgroundWorker worker)
        {
            worker.WorkerReportsProgress = true;
            int width = rOriginal.Width;
            int height = rOriginal.Height;
            Raster raster = new Raster(width, height);
            DateTime startTime = DateTime.Now;
            VectorRgb color;

            double dbScaler = 50.0f / height;
            long x, y;

            //Scip part of method for GrayScale 8-bit images

            switch (this.method)
            {
                #region 1st

                case 1:
                    {
                        // <nipper>
                        double[] p = new double[256];

                        for (int j = 0; j < height; j++)
                        {
                            for (int i = 0; i < width; i++)
                            {
                                color = rOriginal[i, j];
                                p[color.R]++;
                                p[color.G]++;
                                p[color.B]++;
                            }
                            //worker.ReportProgress((int)(100f * j / height), DateTime.Now - startTime);
                        }
                        
                        double maxh = 0;
                        for (y = 0; y < 255; y++) if (maxh < p[y]) maxh = p[y];
                        threshold *= maxh;
                        int minc = 0;
                        while (minc < 255 && p[minc] <= threshold) minc++;
                        int maxc = 255;
                        while (maxc > 0 && p[maxc] <= threshold) maxc--;

                        if (minc == 0 && maxc == 255) return null;
                        if (minc >= maxc) return null;

                        // calculate LUT
		                byte[] lut = new byte[256];
		                for (x = 0; x <256; x++)
			                lut[x] = (byte)Math.Max(0,Math.Min(255,(255 * (x - minc) / (maxc - minc))));
                        
                        // normalize image
                        for (int j = 0; j < height; j++)
                        {
                            for (int i = 0; i < width; i++)
                            {
                                color = rOriginal[i, j];
                                raster[i, j] = new VectorRgb(lut[color.R], 
                                                             lut[color.G], 
                                                             lut[color.B]);
                            }
                            worker.ReportProgress((int)(100f * j / height), DateTime.Now - startTime);
                        }
                        break;
                    }

                #endregion

                #region 2nd

                case 2:
                    {
                        // <nipper>
                        double[] pR = new double[256];
                        double[] pG = new double[256];
                        double[] pB = new double[256];

                        for (int j = 0; j < height; j++)
                        {
                            for (int i = 0; i < width; i++)
                            {
                                color = rOriginal[i, j];
                                pR[color.R]++;
                                pG[color.G]++;
                                pB[color.B]++;
                            }
                            //worker.ReportProgress((int)(100f * j / height), DateTime.Now - startTime);
                        }

                        double maxh = 0;
                        for (y = 0; y < 255; y++) if (maxh < pR[y]) maxh = pR[y];
                        double threshold2 = threshold * maxh;
                        int minR = 0;
                        while (minR < 255 && pR[minR] <= threshold2) minR++;
                        int maxR = 255;
                        while (maxR > 0 && pR[maxR] <= threshold2) maxR--;

                        maxh = 0;
                        for (y = 0; y < 255; y++) if (maxh < pG[y]) maxh = pG[y];
                        threshold2 = threshold * maxh;
                        int minG = 0;
                        while (minG < 255 && pG[minG] <= threshold2) minG++;
                        int maxG = 255;
                        while (maxG > 0 && pG[maxG] <= threshold2) maxG--;

                        maxh = 0;
                        for (y = 0; y < 255; y++) if (maxh < pB[y]) maxh = pB[y];
                        threshold2 = threshold * maxh;
                        int minB = 0;
                        while (minB < 255 && pB[minB] <= threshold2) minB++;
                        int maxB = 255;
                        while (maxB > 0 && pB[maxB] <= threshold2) maxB--;

                        if (minR == 0 && maxR == 255 && minG == 0 && maxG == 255 && minB == 0 && maxB == 255)
                            return null;

                        // calculate LUT
		                byte[] lutR = new byte[256];
		                byte range = (byte)(maxR - minR);
		                if (range != 0)	{
			                for (x = 0; x <256; x++){
				                lutR[x] = (byte)Math.Max(0,Math.Min(255,(255 * (x - minR) / range)));
			                }
		                } else lutR[minR] = (byte)minR;

                        byte[] lutG = new byte[256];
                        range = (byte)(maxG - minG);
                        if (range != 0){
                            for (x = 0; x < 256; x++){
                                lutG[x] = (byte)Math.Max(0, Math.Min(255, (255 * (x - minG) / range)));
                            }
                        } else lutG[minG] = (byte)minG;

                        byte[] lutB = new byte[256];
                        range = (byte)(maxB - minB);
                        if (range != 0){
                            for (x = 0; x < 256; x++){
                                lutB[x] = (byte)Math.Max(0, Math.Min(255, (255 * (x - minB) / range)));
                            }
                        } else lutB[minB] = (byte)minB;

                        // normalize image
                        for (int j = 0; j < height; j++)
                        {
                            for (int i = 0; i < width; i++)
                            {
                                color = rOriginal[i, j];
                                raster[i, j] = new VectorRgb(lutR[color.R], 
                                                             lutG[color.G], 
                                                             lutB[color.B]);
                            }
                            worker.ReportProgress((int)(100f * j / height), DateTime.Now - startTime);
                        }
                        break;
                    }

                #endregion

                #region default

                default:
                    {
                        // <dave>
                        VectorRgb yuvClr;
                        double[] p = new double[256];

                        for (int j = 0; j < height; j++)
                        {
                            for (int i = 0; i < width; i++)
                            {
                                color = rOriginal[i, j];
                                p[Cip.Foundations.ColorspaceHelper.RGB2GRAY(color)]++;
                            }
                            //worker.ReportProgress((int)(100f * j / height), DateTime.Now - startTime);
                        }

                        double maxh = 0;
                        for (y = 0; y < 255; y++) if (maxh < p[y]) maxh = p[y];
                        threshold *= maxh;
                        int minc = 0;
                        while (minc < 255 && p[minc] <= threshold) minc++;
                        int maxc = 255;
                        while (maxc > 0 && p[maxc] <= threshold) maxc--;

                        if (minc == 0 && maxc == 255) return null;
                        if (minc >= maxc) return null;

                        // calculate LUT
                        byte[] lut = new byte[256];
                        for (x = 0; x <256; x++){
                            lut[x] = (byte)Math.Max(0, Math.Min(255, (255 * (x - minc) / (maxc - minc))));
		                }

                        for (int j = 0; j < height; j++)
                        {
                            for (int i = 0; i < width; i++)
                            {
                                color = rOriginal[i, j];
                                yuvClr = Cip.Foundations.ColorspaceHelper.RGB2YUV(color);
                                yuvClr.R = lut[yuvClr.R];
                                color = Cip.Foundations.ColorspaceHelper.YUV2RGB(yuvClr);
                                raster[i, j] = color;
                            }
                            worker.ReportProgress((int)(100f * j / height), DateTime.Now - startTime);
                        }
                        break;

                    }

                #endregion
            }
            return raster;
        }
        public override Raster ProcessWithoutWorker(Raster rOriginal)
        {
            int width = rOriginal.Width;
            int height = rOriginal.Height;
            Raster raster = new Raster(width, height);
            VectorRgb color;

            double dbScaler = 50.0f / height;
            long x, y;

            //Scip part of method for GrayScale 8-bit images

            switch (this.method)
            {
                #region 1st

                case 1:
                    {
                        // <nipper>
                        double[] p = new double[256];

                        for (int j = 0; j < height; j++)
                        {
                            for (int i = 0; i < width; i++)
                            {
                                color = rOriginal[i, j];
                                p[color.R]++;
                                p[color.G]++;
                                p[color.B]++;
                            }
                        }

                        double maxh = 0;
                        for (y = 0; y < 255; y++) if (maxh < p[y]) maxh = p[y];
                        threshold *= maxh;
                        int minc = 0;
                        while (minc < 255 && p[minc] <= threshold) minc++;
                        int maxc = 255;
                        while (maxc > 0 && p[maxc] <= threshold) maxc--;

                        if (minc == 0 && maxc == 255) return null;
                        if (minc >= maxc) return null;

                        // calculate LUT
                        byte[] lut = new byte[256];
                        for (x = 0; x < 256; x++)
                            lut[x] = (byte)Math.Max(0, Math.Min(255, (255 * (x - minc) / (maxc - minc))));

                        // normalize image
                        for (int j = 0; j < height; j++)
                        {
                            for (int i = 0; i < width; i++)
                            {
                                color = rOriginal[i, j];
                                raster[i, j] = new VectorRgb(lut[color.R],
                                                             lut[color.G],
                                                             lut[color.B]);
                            }
                        }
                        break;
                    }

                #endregion

                #region 2nd

                case 2:
                    {
                        // <nipper>
                        double[] pR = new double[256];
                        double[] pG = new double[256];
                        double[] pB = new double[256];

                        for (int j = 0; j < height; j++)
                        {
                            for (int i = 0; i < width; i++)
                            {
                                color = rOriginal[i, j];
                                pR[color.R]++;
                                pG[color.G]++;
                                pB[color.B]++;
                            }
                        }

                        double maxh = 0;
                        for (y = 0; y < 255; y++) if (maxh < pR[y]) maxh = pR[y];
                        double threshold2 = threshold * maxh;
                        int minR = 0;
                        while (minR < 255 && pR[minR] <= threshold2) minR++;
                        int maxR = 255;
                        while (maxR > 0 && pR[maxR] <= threshold2) maxR--;

                        maxh = 0;
                        for (y = 0; y < 255; y++) if (maxh < pG[y]) maxh = pG[y];
                        threshold2 = threshold * maxh;
                        int minG = 0;
                        while (minG < 255 && pG[minG] <= threshold2) minG++;
                        int maxG = 255;
                        while (maxG > 0 && pG[maxG] <= threshold2) maxG--;

                        maxh = 0;
                        for (y = 0; y < 255; y++) if (maxh < pB[y]) maxh = pB[y];
                        threshold2 = threshold * maxh;
                        int minB = 0;
                        while (minB < 255 && pB[minB] <= threshold2) minB++;
                        int maxB = 255;
                        while (maxB > 0 && pB[maxB] <= threshold2) maxB--;

                        if (minR == 0 && maxR == 255 && minG == 0 && maxG == 255 && minB == 0 && maxB == 255)
                            return null;

                        // calculate LUT
                        byte[] lutR = new byte[256];
                        byte range = (byte)(maxR - minR);
                        if (range != 0)
                        {
                            for (x = 0; x < 256; x++)
                            {
                                lutR[x] = (byte)Math.Max(0, Math.Min(255, (255 * (x - minR) / range)));
                            }
                        }
                        else lutR[minR] = (byte)minR;

                        byte[] lutG = new byte[256];
                        range = (byte)(maxG - minG);
                        if (range != 0)
                        {
                            for (x = 0; x < 256; x++)
                            {
                                lutG[x] = (byte)Math.Max(0, Math.Min(255, (255 * (x - minG) / range)));
                            }
                        }
                        else lutG[minG] = (byte)minG;

                        byte[] lutB = new byte[256];
                        range = (byte)(maxB - minB);
                        if (range != 0)
                        {
                            for (x = 0; x < 256; x++)
                            {
                                lutB[x] = (byte)Math.Max(0, Math.Min(255, (255 * (x - minB) / range)));
                            }
                        }
                        else lutB[minB] = (byte)minB;

                        // normalize image
                        for (int j = 0; j < height; j++)
                        {
                            for (int i = 0; i < width; i++)
                            {
                                color = rOriginal[i, j];
                                raster[i, j] = new VectorRgb(lutR[color.R],
                                                             lutG[color.G],
                                                             lutB[color.B]);
                            }
                        }
                        break;
                    }

                #endregion

                #region default

                default:
                    {
                        // <dave>
                        VectorRgb yuvClr;
                        double[] p = new double[256];

                        for (int j = 0; j < height; j++)
                        {
                            for (int i = 0; i < width; i++)
                            {
                                color = rOriginal[i, j];
                                p[Cip.Foundations.ColorspaceHelper.RGB2GRAY(color)]++;
                            }
                        }

                        double maxh = 0;
                        for (y = 0; y < 255; y++) if (maxh < p[y]) maxh = p[y];
                        threshold *= maxh;
                        int minc = 0;
                        while (minc < 255 && p[minc] <= threshold) minc++;
                        int maxc = 255;
                        while (maxc > 0 && p[maxc] <= threshold) maxc--;

                        if (minc == 0 && maxc == 255) return null;
                        if (minc >= maxc) return null;

                        // calculate LUT
                        byte[] lut = new byte[256];
                        for (x = 0; x < 256; x++)
                        {
                            lut[x] = (byte)Math.Max(0, Math.Min(255, (255 * (x - minc) / (maxc - minc))));
                        }

                        for (int j = 0; j < height; j++)
                        {
                            for (int i = 0; i < width; i++)
                            {
                                color = rOriginal[i, j];
                                yuvClr = Cip.Foundations.ColorspaceHelper.RGB2YUV(color);
                                yuvClr.R = lut[yuvClr.R];
                                color = Cip.Foundations.ColorspaceHelper.YUV2RGB(yuvClr);
                                raster[i, j] = color;
                            }
                        }
                        break;

                    }

                #endregion
            }
            return raster;
        }
    }
    /// <remarks>
    /// Smoothing filter.
    /// </remarks>
    public class SmoothingFilter : ImageFilter
    {
        /// <summary>
        /// HSI or RGB colorspace.
        /// </summary>
        private Cip.Filters.ColorSpaceMode mode;
        /// <summary>
        /// range of mask, should be odd.
        /// </summary>
        private int n;
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="Mode">HSI or RGB colorspace.</param>
        /// <param name="radius">Range of mask.</param>
        public SmoothingFilter(Cip.Filters.ColorSpaceMode Mode, int radius)
        {
            mode = Mode;
            this.n = radius * 2 + 1;
        }
        public override Raster ProcessRaster(Raster rOriginal, System.ComponentModel.BackgroundWorker worker)
        {
            worker.WorkerReportsProgress = true;
            int s;//number of pixels in current mask
            float CurrentIntensity, intensity;
            int width = rOriginal.Width;
            int height = rOriginal.Height;
            Raster raster = new Raster(width, height);
            VectorHsi hsi;
            int r, g, b;
            int mDeltaI, pDeltaI, mDeltaJ, pDeltaJ;

            DateTime startTime = DateTime.Now;
            int delta = (n - 1) / 2;

            //mode choose
            if (mode == ColorSpaceMode.RGB)
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        s = r = b = g = 0;
                        mDeltaI = i - delta;
                        pDeltaI = i + delta;
                        mDeltaJ = j - delta;
                        pDeltaJ = j + delta;

                        for (int k = mDeltaI; k <= pDeltaI; k++)
                        {
                            for (int l = mDeltaJ; l <= pDeltaJ; l++)
                            {
                                if (k >= 0 && k < width && l < height && l >= 0)
                                {
                                    r += rOriginal[k, l].R;
                                    g += rOriginal[k, l].G;
                                    b += rOriginal[k, l].B;
                                    s++;
                                }
                            }
                        }
                        raster[i, j] = new VectorRgb((byte)(r / s),
                                                     (byte)(g / s),
                                                     (byte)(b / s));
                    }
                    worker.ReportProgress((int)(100f * i / width), DateTime.Now - startTime);
                }
            }
            else//if HSI mode
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        CurrentIntensity = 0;
                        s = 0;//number of pixels in current mask
                        mDeltaI = i - delta;
                        pDeltaI = i + delta;
                        mDeltaJ = j - delta;
                        pDeltaJ = j + delta;
                        for (int k = mDeltaI; k <= pDeltaI; k++)
                        {
                            for (int l = mDeltaJ; l <= pDeltaJ; l++)
                            {
                                if (k >= 0 && k < width && l < height && l >= 0)
                                {
                                    CurrentIntensity += rOriginal[k, l].ToVectorHSI().I;
                                    s++;
                                }
                            }
                        }
                        intensity = CurrentIntensity / s;
                        hsi = rOriginal[i, j].ToVectorHSI();
                        hsi.I = intensity;
                        raster[i, j] = hsi.ToVectorRGB();
                    }
                    worker.ReportProgress((int)(100f * i / width), DateTime.Now - startTime);
                }
            }
            return raster;
        }
        public override Raster ProcessWithoutWorker(Raster rOriginal)
        {
            int s;//number of pixels in current mask
            float CurrentIntensity, intensity;
            int width = rOriginal.Width;
            int height = rOriginal.Height;
            Raster raster = new Raster(width, height);
            VectorHsi hsi;
            int r, g, b;
            int mDeltaI, pDeltaI, mDeltaJ, pDeltaJ;

            int delta = (n - 1) / 2;

            //mode choose
            if (mode == ColorSpaceMode.RGB)
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        s = r = b = g = 0;
                        mDeltaI = i - delta;
                        pDeltaI = i + delta;
                        mDeltaJ = j - delta;
                        pDeltaJ = j + delta;
                        for (int k = mDeltaI; k <= pDeltaI; k++)
                        {
                            for (int l = mDeltaJ; l <= pDeltaJ; l++)
                            {
                                if (k >= 0 && k < width && l < height && l >= 0)
                                {
                                    r += rOriginal[k, l].R;
                                    g += rOriginal[k, l].G;
                                    b += rOriginal[k, l].B;
                                    s++;
                                }
                            }
                        }
                        raster[i, j] = new VectorRgb((byte)(r / s),
                                                     (byte)(g / s),
                                                     (byte)(b / s));
                    }
                }
            }
            else//if HSI mode
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        CurrentIntensity = 0;
                        s = 0;//number of pixels in current mask
                        mDeltaI = i - delta;
                        pDeltaI = i + delta;
                        mDeltaJ = j - delta;
                        pDeltaJ = j + delta;
                        for (int k = mDeltaI; k <= pDeltaI; k++)
                        {
                            for (int l = mDeltaJ; l <= pDeltaJ; l++)
                            {
                                if (k >= 0 && k < width && l < height && l >= 0)
                                {
                                    CurrentIntensity += rOriginal[k, l].ToVectorHSI().I;
                                    s++;
                                }
                            }
                        }
                        intensity = CurrentIntensity / s;
                        hsi = rOriginal[i, j].ToVectorHSI();
                        hsi.I = intensity;
                        raster[i, j] = hsi.ToVectorRGB();
                    }
                }
            }
            return raster;
        }
    }
    /// <remarks>
    /// Sharpness increase filter.
    /// </remarks>
    public class SharpnessIncreaseFilter : ImageFilter
    {
        private Cip.Filters.ColorSpaceMode mode;
        private bool diag;
        private bool negative;
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="Mode">HSI or RGB colorspace.</param>
        /// <param name="Diag">true -> diag elements (45), false -> no diag (90).</param>
        /// <param name="Negative">central coeff: true -> positive, false -> negative.</param>
        public SharpnessIncreaseFilter(ColorSpaceMode Mode, bool Diag, bool Negative)
        {
            mode = Mode;
            diag = Diag;
            negative = Negative;
        }
        public override Raster ProcessRaster(Raster rOriginal, System.ComponentModel.BackgroundWorker worker)
        {
            worker.WorkerReportsProgress = true;
            int width = rOriginal.Width;
            int height = rOriginal.Height;
            Raster raster = new Raster(width, height);
            float lapR, lapG, lapB;
            byte bLapR, bLapG, bLapB;
            float dLaplasian;
            VectorHsi hsi;
            VectorRgb rgb;
            DateTime startTime = DateTime.Now;

            #region Function construction

            // mask function
            float[,] f = new float[3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    f[i, j] = 1.0f;
            //diag check
            if (diag == true)
                f[1, 1] = -8.0f;
            else
            {
                f[0, 0] = f[0, 2] = f[2, 0] = f[2, 2] = 0.0f;
                f[1, 1] = -4.0f;
            }
            //negative check
            if (negative == true)
            {
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                        f[i, j] = f[i, j] * (-1);
            }//else nothing to do
            #endregion

            if (mode == ColorSpaceMode.RGB)
            {
                //Process
                for (int k = 1; k < width - 1; k++)
                {
                    for (int l = 1; l < height - 1; l++)
                    {
                        //laplasian

                        lapR =      f[0, 0] * rOriginal[k - 1, l + 1].R +
                                    f[0, 1] * rOriginal[k, l + 1].R +
                                    f[0, 2] * rOriginal[k + 1, l + 1].R +
                                    f[1, 0] * rOriginal[k - 1, l].R +
                                    f[1, 1] * rOriginal[k, l].R +
                                    f[1, 2] * rOriginal[k + 1, l].R +
                                    f[2, 0] * rOriginal[k - 1, l - 1].R +
                                    f[2, 1] * rOriginal[k, l - 1].R +
                                    f[2, 2] * rOriginal[k + 1, l - 1].R;

                        lapG =      f[0, 0] * rOriginal[k - 1, l + 1].G +
                                    f[0, 1] * rOriginal[k, l + 1].G +
                                    f[0, 2] * rOriginal[k + 1, l + 1].G +
                                    f[1, 0] * rOriginal[k - 1, l].G +
                                    f[1, 1] * rOriginal[k, l].G +
                                    f[1, 2] * rOriginal[k + 1, l].G +
                                    f[2, 0] * rOriginal[k - 1, l - 1].G +
                                    f[2, 1] * rOriginal[k, l - 1].G +
                                    f[2, 2] * rOriginal[k + 1, l - 1].G;

                        lapB =      f[0, 0] * rOriginal[k - 1, l + 1].B +
                                    f[0, 1] * rOriginal[k, l + 1].B +
                                    f[0, 2] * rOriginal[k + 1, l + 1].B +
                                    f[1, 0] * rOriginal[k - 1, l].B +
                                    f[1, 1] * rOriginal[k, l].B +
                                    f[1, 2] * rOriginal[k + 1, l].B +
                                    f[2, 0] * rOriginal[k - 1, l - 1].B +
                                    f[2, 1] * rOriginal[k, l - 1].B +
                                    f[2, 2] * rOriginal[k + 1, l - 1].B;

                        bLapR = VectorRgb.ClampByte(lapR);
                        bLapG = VectorRgb.ClampByte(lapG);
                        bLapB = VectorRgb.ClampByte(lapB);
                        rgb = new VectorRgb(bLapR, bLapB, bLapG);

                        if (f[1, 1] < 0)
                            raster[k, l] = rOriginal[k, l] - rgb;
                        else
                            raster[k, l] = rOriginal[k, l] + rgb;
                    }
                    worker.ReportProgress((int)(100f * k / (width - 1)), DateTime.Now - startTime);
                }
            }
            else//HSI mode
            {
                //Process
                for (int k = 1; k < width - 1; k++)
                {
                    for (int l = 1; l < height - 1; l++)
                    {
                        hsi = rOriginal[k, l].ToVectorHSI();
                        //laplasian
                        dLaplasian = f[0, 0] * rOriginal[k - 1, l + 1].ToVectorHSI().I +
                                     f[0, 1] * rOriginal[k, l + 1].ToVectorHSI().I +
                                     f[0, 2] * rOriginal[k + 1, l + 1].ToVectorHSI().I +
                                     f[1, 0] * rOriginal[k - 1, l].ToVectorHSI().I +
                                     f[1, 1] * rOriginal[k, l].ToVectorHSI().I +
                                     f[1, 2] * rOriginal[k + 1, l].ToVectorHSI().I +
                                     f[2, 0] * rOriginal[k - 1, l - 1].ToVectorHSI().I +
                                     f[2, 1] * rOriginal[k, l - 1].ToVectorHSI().I +
                                     f[2, 2] * rOriginal[k + 1, l - 1].ToVectorHSI().I;

                        if (f[1, 1] < 0)
                            hsi.I = hsi.I - dLaplasian;
                        else
                            hsi.I = hsi.I + dLaplasian;
                        raster[k, l] = VectorHsi.Clamp(hsi, 0.0f, 1.0f).ToVectorRGB();
                    }
                    worker.ReportProgress((int)(100f * k / (width - 1)), DateTime.Now - startTime);
                }
            }
            //fill bound of raster
            for (int i = 0; i < width; i++)
            {
                raster[i, 0] = rOriginal[i, 0];
                raster[i, height - 1] = rOriginal[i, height - 1];
            }
            for (int j = 0; j < height; j++)
            {
                raster[0, j] = rOriginal[0, j];
                raster[width - 1, j] = rOriginal[width - 1, j];
            }

            return raster;
        }

        public override Raster ProcessWithoutWorker(Raster rOriginal)
        {
            throw new NotImplementedException();
        }
        
    }

}
