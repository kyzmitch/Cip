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
using System.Drawing.Drawing2D;
using Cip.Foundations;

#pragma warning disable 1591

namespace Cip.Filters
{
    /// <remarks>
    /// Enum for excision mode selection
    /// </remarks>
    public enum ExcisionMode
    {
        Sphere = 0,
        Cube = 1,
    }
    /// <remarks>
    /// Thresholding.
    /// </remarks>
    public class ThresholdFilter : ImageFilter
    {
        #region Fields

        /// <summary>
        /// Threshold level.
        /// </summary>
        private byte threshold;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ThresholdFilter()
        {
            this.threshold = 128;
        }
        /// <summary>
        /// Constructor with parameter.
        /// </summary>
        /// <param name="level">Threshold level.</param>
        public ThresholdFilter(byte level)
        {
            this.threshold = level;
        }

        #endregion Constructors

        #region Processing methods

        /// <summary>
        /// Processing with background worker.
        /// </summary>
        /// <param name="rOriginal">original raster.</param>
        /// <param name="worker">Background worker.</param>
        /// <returns>modifyed raster.</returns>
        public override Raster ProcessRaster(Raster rOriginal, BackgroundWorker worker)
        {
            worker.WorkerReportsProgress = true;
            int width = rOriginal.Width;
            int height = rOriginal.Height;
            Raster result = new Raster(width, height);
            DateTime startTime = DateTime.Now;
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    if (Cip.Foundations.ColorspaceHelper.RGB2GRAY(rOriginal[i, j]) > this.threshold)
                        result[i, j] = VectorRgb.White;
                    else
                        result[i, j] = VectorRgb.Black;
                }
                worker.ReportProgress((int)(100f * j / height), DateTime.Now - startTime);
            }
            return result;
        }

        /// <summary>
        /// Processing without background worker.
        /// </summary>
        /// <param name="rOriginal">original raster.</param>
        /// <returns>modifyed raster.</returns>
        public override Raster ProcessWithoutWorker(Raster rOriginal)
        {
            int width = rOriginal.Width;
            int height = rOriginal.Height;
            Raster result = new Raster(width, height);
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    if (Cip.Foundations.ColorspaceHelper.RGB2GRAY(rOriginal[i, j]) > this.threshold)
                        result[i, j] = VectorRgb.White;
                    else
                        result[i, j] = VectorRgb.Black;
                }
            }
            return result;
        }

        #endregion Processing methods

        #region Methods for finding threshold

        /// <summary>
        /// A Novel Thresholding Method for Text Separation and Document Enhancement 
        /// [Adnan Khashman and Boran Sekeroglu].
        /// </summary>
        /// <param name="rOriginal">Source raster.</param>
        /// <returns>Returns text separation threshold.</returns>
        public static int TextSeparationThresholding(Raster rOriginal)
        {
            int width = rOriginal.Width;
            int height = rOriginal.Height;

            //build gray scaled array
            byte[,] array = rOriginal.ToGrayScaledArray();

            long[] p = new long[256];
            int intensity;
            //build histogram
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                {
                    intensity = array[x, y];
                    p[intensity]++;
                }

            //finds maximum gray point in histogram to determine luminance
            int luminance = 255;
            while (luminance > 0 && p[luminance] == 0) luminance--;

            //finds mean of intensities of an image
            long M = 0;
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    M += array[x, y];
            int mean = (int)(M / (width * height));

            //local deviation
            int D = luminance - mean;

            //exact separation point for a document image
            int threshold = Math.Abs(mean - D);

            return threshold;

        }
        /// <summary>
        /// Finds the optimal global threshold.
        /// Variation of k-average: isodata clustering.
        /// </summary>
        /// <param name="rOriginal">Source raster.</param>
        /// <param name="imageType">
        /// 0 = foreground and background 50/50(default),
        /// 1 = foreground or background dominates.
        /// </param>
        /// <param name="rBox">Rectangle area of calculation.</param>
        /// <returns>Returns optimal global threshold; -1 = error.</returns>
        public static int IsodataClusteringThreshold(Raster rOriginal, int imageType, Rectangle rBox)
        {
            int width = rOriginal.Width;
            int height = rOriginal.Height;

            int xmin, xmax, ymin, ymax;
            if (!rBox.IsEmpty)
            {
                xmin = Math.Max(rBox.Left, 0);
                xmax = Math.Min(rBox.Right, width);
                ymin = Math.Max(rBox.Bottom, 0);
                ymax = Math.Min(rBox.Top, height);
            }
            else
            {
                xmin = ymin = 0;
                xmax = width; ymax = height;
            }
            //check for error
            if (xmin >= xmax || ymin >= ymax)
                return -1;

            double[] p = new double[256];
            int intensity;
            //build histogram
            for (int y = ymin; y < ymax; y++)
            {
                for (int x = xmin; x < xmax; x++)
                {
                    intensity = Cip.Foundations.ColorspaceHelper.RGB2GRAYI(rOriginal[x, y]);
                    p[intensity]++;
                }
            }
            //form initial assessment of threshold
            int t0 = 0;
            switch (imageType)
            {
                case 1:
                    {
                        //foreground or background dominates
                        //find histogram limits
                        int gray_min = 0;
                        while (gray_min < 255 && p[gray_min] == 0) gray_min++;
                        int gray_max = 255;
                        while (gray_max > 0 && p[gray_max] == 0) gray_max--;
                        t0 = (int)((gray_min + gray_max) / 2);
                        break;
                    }
                default:
                    {
                        double sum = 0;
                        //foreground and background 50/50, average level
                        for (int i = 0; i < 256; i++)
                            sum += i * p[i];
                        t0 = (int)(sum / (width * height));
                        break;
                    }
            }//end switch

            int delta = 0;
            int threshold = t0;
            int t1 = t0 - delta - 5;
            int iteration = 0;
            while (Convert.ToInt32(Math.Abs(threshold - t1)) > delta)
            {
                iteration++;
                //step 2: segmentation
                double count0 = 0;
                double count1 = 0;
                double sum0 = 0;
                double sum1 = 0;
                for (int i = 0; i < 256; i++)
                {
                    if (i > threshold)
                    {
                        sum1 += i * p[i];
                        count1 += p[i];
                    }
                    else
                    {
                        sum0 += i * p[i];
                        count0 += p[i];
                    }
                }
                //step 3: average intensitys in two areas 
                int i0, i1;
                i0 = (int)(sum0 / count0);
                i1 = (int)(sum1 / count1);
                //step 4: new threshold
                t1 = threshold;//old
                threshold = (int)((i0 + i1) / 2);//new
            }

            return threshold;
        }
        /// <summary>
        /// Finds the optimal (global or local) treshold for image binarization.
        /// </summary>
        /// <param name="rOriginal">source raster.</param>
        /// <param name="method">0 = average all methods (default); 1 = Otsu; 2 = Kittler and Illingworth; 3 = max entropy; 4 = potential difference;</param>
        /// <param name="rBox">region from where the threshold is computed; null = full image (default).</param>
        /// <returns>Returns optimal threshold; -1 = error.</returns>
        public static int OptimalThreshold(Raster rOriginal, int method, Rectangle rBox)
        {
            int width = rOriginal.Width;
            int height = rOriginal.Height;

            int xmin, xmax, ymin, ymax;
            if (!rBox.IsEmpty)
            {
                xmin = Math.Max(rBox.Left, 0);
                xmax = Math.Min(rBox.Right, width);
                ymin = Math.Max(rBox.Bottom, 0);
                ymax = Math.Min(rBox.Top, height);
            }
            else
            {
                xmin = ymin = 0;
                xmax = width; ymax = height;
            }
            //check for error
            if (xmin >= xmax || ymin >= ymax)
                return -1;

            double[] p = new double[256];
            int intensity;
            //build histogram
            for (int y = ymin; y < ymax; y++)
            {
                for (int x = xmin; x < xmax; x++)
                {
                    intensity = Cip.Foundations.ColorspaceHelper.RGB2GRAYI(rOriginal[x, y]);
                    p[intensity]++;
                }
            }
            //find histogram limits
            int gray_min = 0;
            while (gray_min < 255 && p[gray_min] == 0) gray_min++;
            int gray_max = 255;
            while (gray_max > 0 && p[gray_max] == 0) gray_max--;
            //error
            if (gray_min > gray_max)
                return -1;
            if (gray_min == gray_max)
            {
                if (gray_min == 0)
                    return 0;
                else
                    return gray_max - 1;//error
            }

            //compute total moments 0th,1st,2nd order
            int i, k;
            double w_tot = 0;
            double m_tot = 0;
            double q_tot = 0;
            for (i = gray_min; i <= gray_max; i++)
            {
                w_tot += p[i];
                m_tot += i * p[i];
                q_tot += i * i * p[i];
            }

            double L, L1max, L2max, L3max, L4max; //objective functions
            int th1, th2, th3, th4; //optimal thresholds
            L1max = L2max = L3max = L4max = 0;
            th1 = th2 = th3 = th4 = -1;

            double w1, w2, m1, m2, q1, q2, s1, s2;
            w1 = m1 = q1 = 0;
            for (i = gray_min; i < gray_max; i++)
            {
                w1 += p[i];
                w2 = w_tot - w1;
                m1 += i * p[i];
                m2 = m_tot - m1;
                q1 += i * i * p[i];
                q2 = q_tot - q1;
                s1 = q1 / w1 - m1 * m1 / w1 / w1; //s1 = q1/w1-pow(m1/w1,2);
                s2 = q2 / w2 - m2 * m2 / w2 / w2; //s2 = q2/w2-pow(m2/w2,2);

                //Otsu
                L = -(s1 * w1 + s2 * w2); //implemented as definition
                //L = w1 * w2 * (m2 / w2 - m1 / w1) * (m2 / w2 - m1 / w1); //implementation that doesn't need s1 & s2
                if (L1max < L || th1 < 0)
                {
                    L1max = L;
                    th1 = i;
                }

                //Kittler and Illingworth
                if (s1 > 0 && s2 > 0)
                {
                    L = w1 * Math.Log(w1 / Math.Sqrt(s1)) + w2 * Math.Log(w2 / Math.Sqrt(s2));
                    //L = w1 * Math.Log(w1 * w1 / s1) + w2 * Math.Log(w2 * w2 / s2);
                    if (L2max < L || th2 < 0)
                    {
                        L2max = L;
                        th2 = i;
                    }
                }

                //max entropy
                L = 0;
                for (k = gray_min; k <= i; k++) if (p[k] > 0) L -= p[k] * Math.Log(p[k] / w1) / w1;
                for (k = i + 1; k <= gray_max; k++) if (p[k] > 0) L -= p[k] * Math.Log(p[k] / w2) / w2;
                if (L3max < L || th3 < 0)
                {
                    L3max = L;
                    th3 = i;
                }

                //potential difference (based on Electrostatic Binarization method by J. Acharya & G. Sreechakra)
                // L=-fabs(vdiff/vsum); molto selettivo, sembra che L=-fabs(vdiff) o L=-(vsum)
                double vdiff = 0;
                for (k = gray_min; k <= i; k++)
                    vdiff += p[k] * (i - k) * (i - k);
                double vsum = vdiff;
                for (k = i + 1; k <= gray_max; k++)
                {
                    double dv = p[k] * (k - i) * (k - i);
                    vdiff -= dv;
                    vsum += dv;
                }
                if (vsum > 0) L = -Convert.ToDouble(Math.Abs(vdiff / vsum)); else L = 0;
                if (L4max < L || th4 < 0)
                {
                    L4max = L;
                    th4 = i;
                }
            }
            //end for

            int threshold;
            switch (method)
            {
                case 1: //Otsu
                    {
                        threshold = th1;
                        break;
                    }
                case 2: //Kittler and Illingworth
                    {
                        threshold = th2;
                        break;
                    }
                case 3: //max entropy
                    {
                        threshold = th3;
                        break;
                    }
                case 4: //potential difference
                    {
                        threshold = th4;
                        break;
                    }
                default: //auto
                    {
                        int nt = 0;
                        threshold = 0;
                        if (th1 >= 0) { threshold += th1; nt++; }
                        if (th2 >= 0) { threshold += th2; nt++; }
                        if (th3 >= 0) { threshold += th3; nt++; }
                        if (th4 >= 0) { threshold += th4; nt++; }
                        if (nt != 0)
                            threshold /= nt;
                        else
                            threshold = (gray_min + gray_max) / 2;

                        /*better(?) but really expensive alternative:
                        n = 0:255;
                        pth1 = c1(th1)/sqrt(2*pi*s1(th1))*exp(-((n - m1(th1)).^2)/2/s1(th1)) + c2(th1)/sqrt(2*pi*s2(th1))*exp(-((n - m2(th1)).^2)/2/s2(th1));
                        pth2 = c1(th2)/sqrt(2*pi*s1(th2))*exp(-((n - m1(th2)).^2)/2/s1(th2)) + c2(th2)/sqrt(2*pi*s2(th2))*exp(-((n - m2(th2)).^2)/2/s2(th2));
                        ...
                        mse_th1 = sum((p-pth1).^2);
                        mse_th2 = sum((p-pth2).^2);
                        ...
                        select th# that gives minimum mse_th#
                        */
                        break;
                    }
            }
            //end switch

            if (threshold <= gray_min || threshold >= gray_max)
                threshold = (gray_min + gray_max) / 2;

            return threshold;
        }
        
        #endregion Methods for finding threshold

    }
    /// <remarks>
    /// Color range excision filter
    /// </remarks>
    public class ColorRangeExcision : ImageFilter
    {
        private ExcisionMode mode;
        private double radius;
        private Color clPrototypeColor;
        public ColorRangeExcision()
        {
            mode = ExcisionMode.Sphere;
            radius = 0.3;
            clPrototypeColor = Color.Red;
        }
        public ColorRangeExcision(ExcisionMode Mode, double R, Color cl)
        {
            mode = Mode;
            radius = R;
            clPrototypeColor = cl;
        }
        public override Raster ProcessRaster(Raster rOriginal, System.ComponentModel.BackgroundWorker worker)
        {
            worker.WorkerReportsProgress = true;
            int width = rOriginal.Width;
            int height = rOriginal.Height;
            Raster raster = new Raster(width, height);
            VectorRgb CurrentPixel;
            VectorRgb Prototype = new VectorRgb(clPrototypeColor);
            int sum;
            int iRadius = (int)( this.radius * 255);
            DateTime startTime = DateTime.Now;

            switch (mode)
            {
                case ExcisionMode.Sphere:
                    {
                        for (int j = 0; j < height; j++)
                        {
                            for (int i = 0; i < width; i++)
                            {
                                CurrentPixel = rOriginal[i, j];
                                sum = Cip.CipMath.SquareOfDistance(CurrentPixel, Prototype);

                                if (sum > iRadius * iRadius)
                                    raster[i, j] = VectorRgb.Grey;
                                else
                                    raster[i, j] = CurrentPixel;
                            }
                            worker.ReportProgress((int)(100f * j / height), DateTime.Now - startTime);
                        }
                        break;
                    }
                case ExcisionMode.Cube:
                    {
                        for (int j = 0; j < height; j++)
                        {
                            for (int i = 0; i < width; i++)
                            {
                                CurrentPixel = rOriginal[i, j];
                                if ((Math.Abs(CurrentPixel[0] - Prototype[0]) > (iRadius / 2)) &&
                                    (Math.Abs(CurrentPixel[1] - Prototype[1]) > (iRadius / 2)) &&
                                    (Math.Abs(CurrentPixel[2] - Prototype[2]) > (iRadius / 2)))
                                    raster[i, j] = VectorRgb.Grey;
                                else
                                    raster[i, j] = CurrentPixel;
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
            throw new NotImplementedException();
        }
    }


}
