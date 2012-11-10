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
    /// <summary>
    /// Mode of the function.
    /// </summary>
    public enum BrightnessRegionExcisionMode
    {
        /// <summary>
        /// Other Brightness levels bringing to const level
        /// </summary>
        Const = 0,
        /// <summary>
        /// Other Brightness levels keeps their own values
        /// </summary>
        NonConst = 1,
    }

    #region Filters from diplom1 prg

    /// <summary>
    /// Logarithmic transformation.
    /// </summary>
    public class LogarithmicTransformation : ImageFilter
    {
        private int gamma;
        public LogarithmicTransformation(int _gamma)
        {
            this.gamma = _gamma;
        }
        public override Raster ProcessWithoutWorker(Raster rOriginal)
        {
            int width = rOriginal.Width;
            int height = rOriginal.Height;
            int n_pixels = width * height;
            int i, j;
            Cip.Foundations.Raster raster = new Cip.Foundations.Raster(width, height);
            Cip.Filters.GrayScale filterGray = new GrayScale();
            Cip.Foundations.Raster gray = filterGray.ProcessWithoutWorker(rOriginal);

            float C = 0.5f;//=0.5;
            int int_c = 50;
            float tmp;
            float max;
            byte color;
            float[,] mas = new float[width, height];
            C = ((float)int_c) / 10;

            for (j = 0; j < height; j++)
                for (i = 0; i < width; i++)
                {
                    tmp = (float)gray[i, j].R;
                    mas[i, j] = C * (float)System.Math.Log(1 + tmp);
                }
            max = 0;
            for (j = 0; j < height; j++)
                for (i = 0; i < width; i++)
                    if (max < mas[i, j]) max = mas[i, j];

            for (j = 0; j < height; j++)
                for (i = 0; i < width; i++)
                {
                    color = Cip.Foundations.VectorRgb.ClampByte(mas[i, j] / max * 255);
                    raster[i, j] = new VectorRgb(color, color, color);
                }

            return raster;
        }
        public override Raster ProcessRaster(Raster rOriginal, System.ComponentModel.BackgroundWorker worker)
        {
            DateTime startTime = DateTime.Now;
            int width = rOriginal.Width;
            int height = rOriginal.Height;
            int n_pixels = width * height;
            int i, j;
            Cip.Foundations.Raster raster = new Cip.Foundations.Raster(width, height);
            Cip.Filters.GrayScale filterGray = new GrayScale();
            Cip.Foundations.Raster gray = filterGray.ProcessWithoutWorker(rOriginal);

            float C = 0.5f;//=0.5;
            int int_c = 50;
            float tmp;
            float max;
            byte color;
            float[,] mas = new float[width, height];
            C = ((float)int_c) / 10;

            for (j = 0; j < height; j++)
                for (i = 0; i < width; i++)
                {
                    tmp = (float)gray[i, j].R;
                    mas[i, j] = C * (float)System.Math.Log(1 + tmp);
                }
            max = 0;
            for (j = 0; j < height; j++)
                for (i = 0; i < width; i++)
                    if (max < mas[i, j]) max = mas[i, j];

            for (j = 0; j < height; j++)
            {
                for (i = 0; i < width; i++)
                {
                    color = Cip.Foundations.VectorRgb.ClampByte(mas[i, j] / max * 255);
                    raster[i, j] = new VectorRgb(color, color, color);
                }
                worker.ReportProgress((int)(100f * j / height), DateTime.Now - startTime);
            }

            return raster;
        }
    }
    /// <summary>
    /// Exponential transformation.
    /// </summary>
    public class ExponentialTransformation : ImageFilter
    {
        private int m_gamma;
        public ExponentialTransformation(int _gamma)
        { m_gamma = _gamma; }
        public override Raster ProcessRaster(Raster rOriginal, System.ComponentModel.BackgroundWorker worker)
        {
            DateTime startTime = DateTime.Now;
            int width = rOriginal.Width;
            int height = rOriginal.Height;
            int n_pixels = width * height;
            int i, j;
            Cip.Foundations.Raster raster = new Cip.Foundations.Raster(width, height);
            Cip.Filters.GrayScale filterGray = new GrayScale();
            Cip.Foundations.Raster gray = filterGray.ProcessWithoutWorker(rOriginal);

            float gamma = 0.5f;//=0.5;
            float tmp;
            float max;
            byte color;
            float[,] mas = new float[width, height];
            gamma = ((float)m_gamma) / 10;

            for (j = 0; j < height; j++)
                for (i = 0; i < width; i++)
                {
                    tmp = (float)gray[i, j].R;
                    mas[i, j] = (float)System.Math.Pow((double)tmp, (double)gamma);
                }

            max = 0;
            for (j = 0; j < height; j++)
                for (i = 0; i < width; i++)
                    if (max < mas[i, j]) max = mas[i, j];

            for (j = 0; j < height; j++)
            {
                for (i = 0; i < width; i++)
                {
                    color = Cip.Foundations.VectorRgb.ClampByte(mas[i, j] / max * 255);
                    raster[i, j] = new VectorRgb(color, color, color);
                }
                worker.ReportProgress((int)(100f * j / height), DateTime.Now - startTime);
            }

            return raster;
        }
        public override Raster ProcessWithoutWorker(Raster rOriginal)
        {
            int width = rOriginal.Width;
            int height = rOriginal.Height;
            int n_pixels = width * height;
            int i, j;
            Cip.Foundations.Raster raster = new Cip.Foundations.Raster(width, height);
            Cip.Filters.GrayScale filterGray = new GrayScale();
            Cip.Foundations.Raster gray = filterGray.ProcessWithoutWorker(rOriginal);

            float gamma = 0.5f;//=0.5;
            float tmp;
            float max;
            byte color;
            float[,] mas = new float[width, height];
            gamma = ((float)m_gamma) / 10;

            for (j = 0; j < height; j++)
                for (i = 0; i < width; i++)
                {
                    tmp = (float)gray[i, j].R;
                    mas[i, j] = (float)System.Math.Pow((double)tmp, (double)gamma);
                }

            max = 0;
            for (j = 0; j < height; j++)
                for (i = 0; i < width; i++)
                    if (max < mas[i, j]) max = mas[i, j];

            for (j = 0; j < height; j++)
            {
                for (i = 0; i < width; i++)
                {
                    color = Cip.Foundations.VectorRgb.ClampByte(mas[i, j] / max * 255);
                    raster[i, j] = new VectorRgb(color, color, color);
                }
            }

            return raster;
        }
    }
    /// <summary>
    /// Piece linear function.
    /// </summary>
    public class PieceLinearFilter : ImageFilter
    {
        //private int COUNT_GREY_SCAYLE = 256;

        public PieceLinearFilter()
        { }
        private delegate byte PieceLinearFunction(byte value);
        /// <summary>
        /// Piece linear function with 2 parameters.
        /// </summary>
        /// <param name="value">Input byte value</param>
        /// <returns>Function byte value</returns>
        private static byte ContrastIncreaseFunction(byte value)
        {
            byte result = value;
            if ((value >= 0) && (value <= 96))
                result = (byte)(value / 3);
            else
                if ((value >= 160) && (value <= 255))
                    result = (byte)((value + 8) / 3);
                else
                    result = (byte)(5 * value - 8);
            return result;
        }
        public override Raster ProcessWithoutWorker(Raster rOriginal)
        {
            PieceLinearFunction func;
            int width = rOriginal.Width;
            int height = rOriginal.Height;
            int n_pixels = width * height;
            int i, j;
            Cip.Foundations.Raster raster = new Cip.Foundations.Raster(width, height);
            Cip.Filters.GrayScale filterGray = new GrayScale();
            Cip.Foundations.Raster gray = filterGray.ProcessWithoutWorker(rOriginal);
            //Cip.Foundations.Raster graph = new Cip.Foundations.Raster(COUNT_GREY_SCAYLE, COUNT_GREY_SCAYLE, System.Drawing.Color.White);

            //int max = 0, min = 255;
            byte color;
            byte[,] mas = new byte[width, height];

            for (j = 0; j < height; j++)
                for (i = 0; i < width; i++)
                    mas[i, j] = gray[i, j].R;

            /*for (j = 0; j < height; j++)
                for (i = 0; i < width; i++)
                {
                    if (mas[i, j] < min) min = (int)mas[i, j];
                    if (mas[i, j] > max) max = (int)mas[i, j];
                }*/
            func = new PieceLinearFunction(ContrastIncreaseFunction);

            for (j = 0; j < height; j++)
            {
                for (i = 0; i < width; i++)
                {
                    color = func(mas[i, j]);
                    raster[i, j] = new Cip.Foundations.VectorRgb(color);
                }
            }

            return raster;
        }
        public override Raster ProcessRaster(Raster rOriginal, System.ComponentModel.BackgroundWorker worker)
        {
            DateTime startTime = DateTime.Now;
            PieceLinearFunction func;
            int width = rOriginal.Width;
            int height = rOriginal.Height;
            int n_pixels = width * height;
            int i, j;
            Cip.Foundations.Raster raster = new Cip.Foundations.Raster(width, height);
            Cip.Filters.GrayScale filterGray = new GrayScale();
            Cip.Foundations.Raster gray = filterGray.ProcessWithoutWorker(rOriginal);
            //Cip.Foundations.Raster graph = new Cip.Foundations.Raster(COUNT_GREY_SCAYLE, COUNT_GREY_SCAYLE, System.Drawing.Color.White);

            //int max = 0, min = 255;
            byte color;
            byte[,] mas = new byte[width, height];

            for (j = 0; j < height; j++)
                for (i = 0; i < width; i++)
                    mas[i, j] = gray[i, j].R;

            /*for (j = 0; j < height; j++)
                for (i = 0; i < width; i++)
                {
                    if (mas[i, j] < min) min = (int)mas[i, j];
                    if (mas[i, j] > max) max = (int)mas[i, j];
                }*/
            func = new PieceLinearFunction(ContrastIncreaseFunction);

            for (j = 0; j < height; j++)
            {
                for (i = 0; i < width; i++)
                {
                    color = func(mas[i, j]);
                    raster[i, j] = new Cip.Foundations.VectorRgb(color);
                }
                worker.ReportProgress((int)(100f * j / height), DateTime.Now - startTime);
            }

            return raster;
        }
    }
    /// <summary>
    /// Brightness region excision.
    /// </summary>
    public class BrightnessRegionExcision : ImageFilter
    {
        //private int COUNT_GREY_SCAYLE = 256;
        private BrightnessRegionExcisionMode m_mode;
        public BrightnessRegionExcision()
        {
            m_mode = BrightnessRegionExcisionMode.Const;
        }
        public BrightnessRegionExcision(BrightnessRegionExcisionMode mode)
        {
            m_mode = mode;
        }
        public override Raster ProcessWithoutWorker(Raster rOriginal)
        {
            int width = rOriginal.Width;
            int height = rOriginal.Height;
            int n_pixels = width * height;
            int i, j;
            Cip.Foundations.Raster raster = new Cip.Foundations.Raster(width, height);
            Cip.Filters.GrayScale filterGray = new GrayScale();
            Cip.Foundations.Raster gray = filterGray.ProcessWithoutWorker(rOriginal);
            //Cip.Foundations.Raster graph = new Cip.Foundations.Raster(COUNT_GREY_SCAYLE, COUNT_GREY_SCAYLE, System.Drawing.Color.White);

            byte max = 120, min = 100;
            byte tmp;
            byte color;
            byte[,] mas = new byte[width, height];

            for (j = 0; j < height; j++)
                for (i = 0; i < width; i++)
                    mas[i, j] = gray[i, j].R;

            /*for (j = 0; j < height; j++)
                for (i = 0; i < width; i++)
                {
                    if (mas[i, j] < min) min = mas[i, j];
                    if (mas[i, j] > max) max = mas[i, j];
                }*/

            if (this.m_mode == BrightnessRegionExcisionMode.Const)
            {
                for (j = 0; j < height; j++)
                    for (i = 0; i < width; i++)
                    {
                        tmp = mas[i, j];
                        if ((tmp >= min) && (tmp <= max))
                            color = 160;
                        else
                            color = 20;
                        raster[i, j] = new Cip.Foundations.VectorRgb(color);
                    }
            }
            else
            {
                for (j = 0; j < height; j++)
                    for (i = 0; i < width; i++)
                    {
                        tmp = mas[i, j];
                        if ((tmp >= min) && (tmp <= max))
                            color = 160;
                        else
                            color = tmp;
                        raster[i, j] = new Cip.Foundations.VectorRgb(color);
                    }
            }

            return raster;
        }
        public override Raster ProcessRaster(Raster rOriginal, System.ComponentModel.BackgroundWorker worker)
        {
            DateTime startTime = DateTime.Now;
            int width = rOriginal.Width;
            int height = rOriginal.Height;
            int n_pixels = width * height;
            int i, j;
            Cip.Foundations.Raster raster = new Cip.Foundations.Raster(width, height);
            Cip.Filters.GrayScale filterGray = new GrayScale();
            Cip.Foundations.Raster gray = filterGray.ProcessWithoutWorker(rOriginal);
            //Cip.Foundations.Raster graph = new Cip.Foundations.Raster(COUNT_GREY_SCAYLE, COUNT_GREY_SCAYLE, System.Drawing.Color.White);

            byte max = 120, min = 100;
            byte tmp;
            byte color;
            byte[,] mas = new byte[width, height];

            for (j = 0; j < height; j++)
                for (i = 0; i < width; i++)
                    mas[i, j] = gray[i, j].R;

            /*for (j = 0; j < height; j++)
                for (i = 0; i < width; i++)
                {
                    if (mas[i, j] < min) min = mas[i, j];
                    if (mas[i, j] > max) max = mas[i, j];
                }*/

            if (this.m_mode == BrightnessRegionExcisionMode.Const)
            {
                for (j = 0; j < height; j++)
                {
                    for (i = 0; i < width; i++)
                    {
                        tmp = mas[i, j];
                        if ((tmp >= min) && (tmp <= max))
                            color = 160;
                        else
                            color = 20;
                        raster[i, j] = new Cip.Foundations.VectorRgb(color);
                    }
                    worker.ReportProgress((int)(100f * j / height), DateTime.Now - startTime);
                }
            }
            else
            {
                for (j = 0; j < height; j++)
                {
                    for (i = 0; i < width; i++)
                    {
                        tmp = mas[i, j];
                        if ((tmp >= min) && (tmp <= max))
                            color = 160;
                        else
                            color = tmp;
                        raster[i, j] = new Cip.Foundations.VectorRgb(color);
                    }
                    worker.ReportProgress((int)(100f * j / height), DateTime.Now - startTime);
                }
            }

            return raster;
        }
    }

    public class BitPlainExcision : ImageFilter
    {
        private int m_nNumberOfPlane;
        public BitPlainExcision(int numberOfPlane)
        {
            m_nNumberOfPlane = numberOfPlane;
        }
        public override Raster ProcessWithoutWorker(Raster rOriginal)
        {
            int width = rOriginal.Width;
            int height = rOriginal.Height;
            int i, j;
            Cip.Foundations.Raster raster = new Cip.Foundations.Raster(width, height);
            Cip.Filters.GrayScale filterGray = new GrayScale();
            Cip.Foundations.Raster gray = filterGray.ProcessWithoutWorker(rOriginal);

            byte color = 0;
            byte[,] mas = new byte[width, height];

            for (j = 0; j < height; j++)
                for (i = 0; i < width; i++)
                    mas[i, j] = gray[i, j].R;

            for (j = 0; j < height; j++)
            {
                for (i = 0; i < width; i++)
                {
                    //if else construction better than switch
                    if (m_nNumberOfPlane == 0)
                        color = ((mas[i, j] >= 0) && (mas[i, j] < 32)) ? (byte)255 : (byte)0;
                    else if (m_nNumberOfPlane == 1)
                        color = ((mas[i, j] >= 32) && (mas[i, j] < 64)) ? (byte)255 : (byte)0;
                    else if (m_nNumberOfPlane == 2)
                        color = ((mas[i, j] >= 64) && (mas[i, j] < 96)) ? (byte)255 : (byte)0;
                    else if (m_nNumberOfPlane == 3)
                        color = ((mas[i, j] >= 96) && (mas[i, j] < 128)) ? (byte)255 : (byte)0;
                    else if (m_nNumberOfPlane == 4)
                        color = ((mas[i, j] >= 128) && (mas[i, j] < 160)) ? (byte)255 : (byte)0;
                    else if (m_nNumberOfPlane == 5)
                        color = ((mas[i, j] >= 160) && (mas[i, j] < 192)) ? (byte)255 : (byte)0;
                    else if (m_nNumberOfPlane == 6)
                        color = ((mas[i, j] >= 192) && (mas[i, j] < 224)) ? (byte)255 : (byte)0;
                    else if (m_nNumberOfPlane == 7)
                        color = ((mas[i, j] >= 224) && (mas[i, j] < 128)) ? (byte)255 : (byte)0;

                    raster[i, j] = new Cip.Foundations.VectorRgb(color);
                }
            }

            return raster;
        }
        public override Raster ProcessRaster(Raster rOriginal, System.ComponentModel.BackgroundWorker worker)
        {
            DateTime startTime = DateTime.Now;
            int width = rOriginal.Width;
            int height = rOriginal.Height;
            int i, j;
            Cip.Foundations.Raster raster = new Cip.Foundations.Raster(width, height);
            Cip.Filters.GrayScale filterGray = new GrayScale();
            Cip.Foundations.Raster gray = filterGray.ProcessWithoutWorker(rOriginal);

            byte color = 0;
            byte[,] mas = new byte[width, height];

            for (j = 0; j < height; j++)
                for (i = 0; i < width; i++)
                    mas[i, j] = gray[i, j].R;

            for (j = 0; j < height; j++)
            {
                for (i = 0; i < width; i++)
                {
                    //if else construction better than switch
                    if (m_nNumberOfPlane == 0)
                        color = ((mas[i, j] >= 0) && (mas[i, j] < 32)) ? (byte)255 : (byte)0;
                    else if (m_nNumberOfPlane == 1)
                        color = ((mas[i, j] >= 32) && (mas[i, j] < 64)) ? (byte)255 : (byte)0;
                    else if (m_nNumberOfPlane == 2)
                        color = ((mas[i, j] >= 64) && (mas[i, j] < 96)) ? (byte)255 : (byte)0;
                    else if (m_nNumberOfPlane == 3)
                        color = ((mas[i, j] >= 96) && (mas[i, j] < 128)) ? (byte)255 : (byte)0;
                    else if (m_nNumberOfPlane == 4)
                        color = ((mas[i, j] >= 128) && (mas[i, j] < 160)) ? (byte)255 : (byte)0;
                    else if (m_nNumberOfPlane == 5)
                        color = ((mas[i, j] >= 160) && (mas[i, j] < 192)) ? (byte)255 : (byte)0;
                    else if (m_nNumberOfPlane == 6)
                        color = ((mas[i, j] >= 192) && (mas[i, j] < 224)) ? (byte)255 : (byte)0;
                    else if (m_nNumberOfPlane == 7)
                        color = ((mas[i, j] >= 224) && (mas[i, j] < 128)) ? (byte)255 : (byte)0;
 
                    raster[i, j] = new Cip.Foundations.VectorRgb(color);
                }
                worker.ReportProgress((int)(100f * j / height), DateTime.Now - startTime);
            }

            return raster;
        }
    }

    #endregion Filters from diplom1 prg
}