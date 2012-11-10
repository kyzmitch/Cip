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
using Cip.Foundations;

#pragma warning disable 1591

namespace Cip.Filters
{
    /// <remarks>
    /// Linear filter for image processing.
    /// Matrix processing.
    /// </remarks>
    public class LinearFilter : ImageFilter
    {
        /// <summary>
        /// Kernel of the linear filter.
        /// </summary>
        private float[,] kernel = null;
        private int divisor;
        private int bias;
        /// <summary>
        /// Get kernel of the linear filter.
        /// </summary>
        public float[,] Kernel
        {
            get { return this.kernel; }
        }
        public int Divisor
        {
            get { return this.divisor; }
        }
        public int Bias
        {
            get { return this.bias; }
        }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="ker">Kernel of the filter.</param>
        public LinearFilter(float[,] ker)
        {
            this.kernel = ker;
        }
        public LinearFilter(float[,] ker, int _divisor, int _bias)
        {
            this.kernel = ker;
            this.divisor = _divisor;
            this.bias = _bias;
        }
        /// <summary>
        /// Image processing with linear matrix filter.
        /// </summary>
        public override Raster ProcessRaster(Raster rOriginal, System.ComponentModel.BackgroundWorker worker)
        {
            worker.WorkerReportsProgress = true;

            // define radius of filter by X
            int radiusX = kernel.GetLength(0) / 2;
            // define radius of filter by Y
            int radiusY = kernel.GetLength(1) / 2;
            //define width and height of image
            int width = rOriginal.Width;
            int height = rOriginal.Height;
            //create result raster
            Raster raster = new Raster(width, height);
            float r, g, b;
            byte R,G,B;
            VectorRgb rgb;
            int rk, rl;
            //save time of beginning processing
            DateTime startTime = DateTime.Now;
            //processing
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    r = g = b = 0;

                    for (int l = -radiusY; l <= radiusY; l++)
                        for (int k = -radiusX; k <= radiusX; k++)
                            if ((i + k) >= 0 && (i + k) < width && (j + l) < height && (j + l) >= 0)
                            {
                                rgb = rOriginal[i + k, j + l];
                                rk = k + radiusX;
                                rl = l + radiusY;
                                r += rgb.R * kernel[rk, rl];
                                g += rgb.G * kernel[rk, rl];
                                b += rgb.B * kernel[rk, rl];
                            }
                    R = VectorRgb.ClampByte(r);
                    G = VectorRgb.ClampByte(g);
                    B = VectorRgb.ClampByte(b);
                    raster[i, j] = new VectorRgb(R, G, B);
                }
                worker.ReportProgress((int)(100f * j / height), DateTime.Now - startTime);
            }

            //fill bound of raster
            /*for (int i = 0; i < width; i++)
            {
                raster[i, 0] = rOriginal[i, 0];
                raster[i, height - 1] = rOriginal[i, height - 1];
            }
            for (int j = 0; j < height; j++)
            {
                raster[0, j] = rOriginal[0, j];
                raster[width - 1, j] = rOriginal[width - 1, j];
            }*/

            return raster;
        }
        /// <summary>
        /// Image processing with linear matrix filter.
        /// Without backgroundworker.
        /// </summary>
        public override Raster ProcessWithoutWorker(Raster rOriginal)
        {
            // define radius of filter by X
            int radiusX = kernel.GetLength(0) / 2;
            // define radius of filter by Y
            int radiusY = kernel.GetLength(1) / 2;
            //define width and height of image
            int width = rOriginal.Width;
            int height = rOriginal.Height;
            //create result raster
            Raster raster = new Raster(width, height);
            float r, g, b;
            byte R, G, B;
            VectorRgb rgb;
            int rk, rl;

            //processing
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    r = g = b = 0;

                    for (int l = -radiusY; l <= radiusY; l++)
                        for (int k = -radiusX; k <= radiusX; k++)
                            if ((i + k) >= 0 && (i + k) < width && (j + l) < height && (j + l) >= 0)
                            {
                                rgb = rOriginal[i + k, j + l];
                                rk = k + radiusX;
                                rl = l + radiusY;
                                r += rgb.R * kernel[rk, rl];
                                g += rgb.G * kernel[rk, rl];
                                b += rgb.B * kernel[rk, rl];
                            }
                    R = VectorRgb.ClampByte(r);
                    G = VectorRgb.ClampByte(g);
                    B = VectorRgb.ClampByte(b);
                    raster[i, j] = new VectorRgb(R, G, B);
                }
            }

            //fill bound of raster
            /*for (int i = 0; i < width; i++)
            {
                raster[i, 0] = rOriginal[i, 0];
                raster[i, height - 1] = rOriginal[i, height - 1];
            }
            for (int j = 0; j < height; j++)
            {
                raster[0, j] = rOriginal[0, j];
                raster[width - 1, j] = rOriginal[width - 1, j];
            }*/

            return raster;
        }
        
        public Bitmap ProcessBitmapWithoutWorker(Bitmap sourceBmp)
        {
            int radiusX = kernel.GetLength(0) / 2;
            int radiusY = kernel.GetLength(1) / 2;

            //define width and height of image
            int width = sourceBmp.Width;
            int height = sourceBmp.Height;

            float r, g, b;
            byte R, G, B;
            VectorRgb rgb;
            int rk, rl;
            Bitmap resultBmp = (Bitmap)sourceBmp.Clone();

            BitmapDecorator sourceDec = new BitmapDecorator(sourceBmp);
            BitmapDecorator resultDec = new BitmapDecorator(resultBmp);

            //processing
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    r = g = b = 0;

                    for (int l = -radiusY; l <= radiusY; l++)
                        for (int k = -radiusX; k <= radiusX; k++)
                            if ((i + k) >= 0 && (i + k) < width && (j + l) < height && (j + l) >= 0)
                            {
                                rgb = sourceDec.GetVectorRgb(i + k, j + l);
                                rk = k + radiusX;
                                rl = l + radiusY;
                                r += rgb.R * kernel[rk, rl];
                                g += rgb.G * kernel[rk, rl];
                                b += rgb.B * kernel[rk, rl];
                            }
                    R = VectorRgb.ClampByte(r);
                    G = VectorRgb.ClampByte(g);
                    B = VectorRgb.ClampByte(b);
                    resultDec.SetPixel(i, j, R, G, B);
                }
            }

            sourceDec.Dispose();
            resultDec.Dispose();

            return resultBmp;
        }

        #region Linear filters samples

        public static LinearFilter Custom(float[,] _kernel, int _divizior, int _bias)
        {
            int div = _divizior;
            if (div == 0)
                div = 1;
            int size = _kernel.GetLength(0);
            float[,] ker = _kernel;
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                {
                    ker[i, j] = _kernel[i, j] / (_divizior);
                    ker[i, j] = ker[i, j] + _bias;
                }
            return new LinearFilter(ker,_divizior,_bias);
        }
        /// <summary>
        /// Simple blur filter.
        /// </summary>
        /// <param name="radius">Radius.</param>
        public static LinearFilter SimpleBlurFilter(int radius)
        {
            // define size of kernel
            int size = 2 * radius + 1;

            // create kernel of a filter
            float[,] kernel = new float[size, size];

            // fill kernel
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    kernel[i, j] = 1.0f / (size * size);

            return new LinearFilter(kernel,size*size,0);
        }
        /// <summary>
        /// Gaussian blur filter.
        /// </summary>
        /// <param name="radius">Radius.</param>
        /// <param name="sigma">Sigma (example 2f).</param>
        public static LinearFilter GaussianBlurFilter(int radius, float sigma)
        {
            // define size of kernel
            int size = 2 * radius + 1;
            // create kernel of a filter
            float[,] kernel = new float[size, size];
            //radius of kernel action
            int half = size / 2;
            //coefficient of kernel norm
            float norm = 0;
            //calculate kernel of linear filter
            for (int i = -half; i <= half; i++)
            {
                for (int j = -half; j <= half; j++)
                {
                    kernel[i + half, j + half] = (float)(Math.Exp(-(i * i + j * j) / (sigma * sigma)));

                    norm += kernel[i + half, j + half];
                }
            }
            //kernel norming
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    kernel[i, j] /= norm;
                }
            }
            return new LinearFilter(kernel,(int)norm,0);
        }
        /// <summary>
        /// Endge filter, and mask for point detection.
        /// </summary>
        public static LinearFilter Edges3x3()
        {
            float[,] kernel = { { -1.0f, -1.0f, -1.0f}, 
			                    { -1.0f,  8.0f, -1.0f},
			                    { -1.0f, -1.0f,  -1.0f} };
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    kernel[i, j] /= (-1.0f);
                    kernel[i, j] += 255.0f;
                }

            return new LinearFilter(kernel,-1,255);
        }
        /// <summary>
        /// Endge filter, and mask for point detection.
        /// </summary>
        public static LinearFilter Edges5x5()
        {
            float[,] kernel = {{-1f,-1f,-1f,-1f,-1f},
                               {-1f,-2f,-2f,-2f,-1f},
                               {-1f,-2f,32f,-2f,-1f},
                               {-1f,-2f,-2f,-2f,-1f},
                               {-1f,-1f,-1f,-1f,-1f} };

            return new LinearFilter(kernel,0,0);
        }
        /// <summary>
        /// Like stamping effect.
        /// </summary>
        public static LinearFilter Emboss3x3()
        {
            float[,] kernel = { {  0.0f,  0.0f, -1.0f}, 
			                    {  0.0f,  0.0f,  0.0f},
			                    {  1.0f,  0.0f,  0.0f} };
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    kernel[i, j] /= (-1.0f);
                    kernel[i, j] += 127.0f;
                }

            return new LinearFilter(kernel,-1,127);
        }
        /// <summary>
        /// Color pen filter.
        /// </summary>
        public static LinearFilter ColorPenFilter()
        {
            float[,] kernel = {{-1f,-1f,-1f,-1f,-1f},
                               {-1f,-2f,-2f,-2f,-1f},
                               {-1f,-2f,34f,-2f,-1f},
                               {-1f,-2f,-2f,-2f,-1f},
                               {-1f,-1f,-1f,-1f,-1f} };

            return new LinearFilter(kernel,0,0);
        }
        /// <summary>
        /// Stamping mask filter
        /// </summary>
        /// <returns></returns>
        public static LinearFilter Stamping()
        {
            float[,] kernel = { { 0.0f, 1.0f, 0.0f}, 
			                    { 1.0f,  0.0f, -1.0f},
			                    { 0.0f, -1.0f,  0.0f} };

            return new LinearFilter(kernel,0,0);
        }
        /// <summary>
        /// Light sharpen filter.
        /// </summary>
        public static LinearFilter LightSharpnessIncrease()
        {
            float[,] kernel = { { 0.0f, -1.0f,  0.0f}, 
			                    {-1.0f,  5.0f, -1.0f},
			                    { 0.0f, -1.0f,  0.0f} };

            return new LinearFilter(kernel,0,0);
        }
        /// <summary>
        /// High sharpen filter.
        /// </summary>
        public static LinearFilter HighSharpnessIncrease()
        {
            float[,] kernel = { {-1.0f, -1.0f, -1.0f}, 
			                    {-1.0f,  9.0f, -1.0f},
			                    {-1.0f, -1.0f, -1.0f} };

            return new LinearFilter(kernel,0,0);
        }
        /// <summary>
        /// Sharpen 3x3 filter.
        /// </summary>
        public static LinearFilter Sharpen3x3()
        {
            float[,] kernel = { {-1.0f, -1.0f, -1.0f}, 
			                    {-1.0f, 15.0f, -1.0f},
			                    {-1.0f, -1.0f, -1.0f} };

            // normalize kernel
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    kernel[i, j] /= 7.0f;

            return new LinearFilter(kernel,7,0);
        }
        /// <summary>
        /// Soft filter.
        /// </summary>
        /// <returns></returns>
        public static LinearFilter Soften()
        {

            float[,] kernel = { {1.0f, 1.0f, 1.0f}, 
			                    {1.0f, 8.0f, 1.0f},
			                    {1.0f, 1.0f, 1.0f} };
            // normalize kernel
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    kernel[i, j] /= 16.0f;

            return new LinearFilter(kernel,16,0);
        }
        /// <summary>
        /// Weighted smoothing.
        /// </summary>
        public static LinearFilter Soften5x5()
        {
            float[,] kernel = {{ 1f, 1f, 1f, 1f, 1f},
                               { 1f, 8f, 8f, 8f, 1f},
                               { 1f, 8f,64f, 8f, 1f},
                               { 1f, 8f, 8f, 8f, 1f},
                               { 1f, 1f, 1f, 1f, 1f} };

            return new LinearFilter(kernel);
        }
        /// <summary>
        /// Gaussian 5x5 blur filter, decrease defocusing.
        /// </summary>
        public static LinearFilter Gaussian5x5()
        {
            float[,] kernel = {{ 0f, 1f, 2f, 1f, 0f},
                               { 1f, 3f, 4f, 3f, 1f},
                               { 2f, 4f, 8f, 4f, 2f},
                               { 1f, 3f, 4f, 3f, 1f},
                               { 0f, 1f, 2f, 1f, 0f} };

            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    kernel[i, j] /= 52.0f;

            return new LinearFilter(kernel,52,0);
        }
        /// <summary>
        /// Gaussian 3x3 blur filter, decrease defocusing.
        /// </summary>
        public static LinearFilter Gaussian3x3()
        {
            float[,] kernel = { {1.0f, 2.0f, 1.0f}, 
			                    {2.0f, 4.0f, 2.0f},
			                    {1.0f, 2.0f, 1.0f} };
            // normalize kernel
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    kernel[i, j] /= 16.0f;

            return new LinearFilter(kernel,16,0);
        }
        /// <summary> 
        /// Prewitt linear filter. 
        /// </summary>
        public static LinearFilter PrewittFilter()
        {
            float[,] kernel = { {-1.0f, 0.0f, 1.0f}, 
			                    {-1.0f, 0.0f, 1.0f},
			                    {-1.0f, 0.0f, 1.0f} };

            return new LinearFilter(kernel);
        }
        /// <summary> 
        /// Sobel linear filter. 
        /// </summary>
        public static LinearFilter SobelFilter()
        {
            float[,] kernel = { {-1.0f, 0.0f, 1.0f}, 
			                    {-2.0f, 0.0f, 2.0f},
			                    {-1.0f, 0.0f, 1.0f} };

            return new LinearFilter(kernel);
        }
        /// <summary> 
        /// Laplas linear filter. 
        /// </summary>
        public static LinearFilter LaplasFilter()
        {
            float[,] kernel = { {0.0f,  1.0f,  0.0f}, 
			                    {1.0f, -4.0f,  1.0f},
			                    {0.0f,  1.0f,  0.0f} };

            return new LinearFilter(kernel);
        }

        #endregion
    }
    
}
