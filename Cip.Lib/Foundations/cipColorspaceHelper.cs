/////////////////////////////////////////////////////////////////////////////////
// Colour Image Processing Library (CipLibNet)                                 //
// Copyright (C) Andrew [kyzmitch] Ermoshin.                                   //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

#pragma warning disable 1591

namespace Cip.Foundations
{
    /// <remarks>
    /// This helper class provides with colorspace-convertion extension methods
    /// </remarks>
    public class ColorspaceHelper
    {
        private static byte HSIMAX = 255;
        private static byte RGBMAX = 255;
        private static byte HSIUNDEFINED = 170;
        
        
        #region Public Methods

        /// <summary>
        /// Minimum of 3 numbers
        /// </summary>
        /// <param name="v1">number 1</param>
        /// <param name="v2">number 2</param>
        /// <param name="v3">number 3</param>
        private static float Min(float v1, float v2, float v3)
        {
            return Math.Min(v1, Math.Min(v2, v3));
        }
        /// <summary>
        /// (F. Livraghi) fixed implementation for HSI2RGB routine.
        /// </summary>
        private static float HueToRGB(float n1, float n2, float hue)
        {
            //<F. Livraghi> fixed implementation for HSI2RGB routine
            float rValue;

            if (hue > 360)
                hue = hue - 360;
            else if (hue < 0)
                hue = hue + 360;

            if (hue < 60)
                rValue = n1 + (n2 - n1) * hue / 60.0f;
            else if (hue < 180)
                rValue = n2;
            else if (hue < 240)
                rValue = n1 + (n2 - n1) * (240 - hue) / 60;
            else
                rValue = n1;

            return rValue;
        }
        /// <summary>
        /// Convert RGB colorspace to HSI colorspace.
        /// </summary>
        public static VectorHsi RGB2HSI(VectorRgb rgb)
        {
            VectorHsi hsi = new VectorHsi();
            
            //method from CxImageLib. ximadsp.cpp file.
            byte H, S, I;
            UInt16 Rdelta, Gdelta, Bdelta;

            //get R, G, and B in 8-bit
            byte R = rgb.R;
            byte G = rgb.G;
            byte B = rgb.B;

            byte cMax = Math.Max(Math.Max(R, G), B);// calculate lightness (intensity)
            byte cMin = Math.Min(Math.Min(R, G), B);
            I = (byte)((((cMax + cMin) * HSIMAX) + RGBMAX) / (2 * RGBMAX));

            if (cMax == cMin){          // r=g=b --> achromatic case
                S = 0;					// saturation
                H = HSIUNDEFINED;       // hue
            }
            else {                      // chromatic case
                if (I <= (HSIMAX / 2))	// saturation
                    S = (byte)((((cMax - cMin) * HSIMAX) + ((cMax + cMin) / 2)) / (cMax + cMin));
                else
                    S = (byte)((((cMax - cMin) * HSIMAX) + ((2 * RGBMAX - cMax - cMin) / 2)) / (2 * RGBMAX - cMax - cMin));
                // hue
                Rdelta = (UInt16)((((cMax - R) * (HSIMAX / 6)) + ((cMax - cMin) / 2)) / (cMax - cMin));
                Gdelta = (UInt16)((((cMax - G) * (HSIMAX / 6)) + ((cMax - cMin) / 2)) / (cMax - cMin));
                Bdelta = (UInt16)((((cMax - B) * (HSIMAX / 6)) + ((cMax - cMin) / 2)) / (cMax - cMin));

                if (R == cMax)
                    H = (byte)(Bdelta - Gdelta);
                else if (G == cMax)
                    H = (byte)((HSIMAX / 3) + Rdelta - Bdelta);
                else // B == cMax
                    H = (byte)(((2 * HSIMAX) / 3) + Gdelta - Rdelta);

              //if (H < 0) H += HSIMAX;     //always false
                if (H > HSIMAX) H -= HSIMAX;
            }
            hsi.H = (float)(H / 255.0f);
            hsi.S = (float)(S / 255.0f);
            hsi.I = (float)(I / 255.0f);
            
            return hsi;
        }
        /// <summary>
        /// Convert Color struct to HSI colorspace.
        /// </summary>
        public static VectorHsi RGB2HSI(byte R, byte G, byte B)
        {
            VectorHsi hsi = new VectorHsi();
            byte H, S, I;
            UInt16 Rdelta, Gdelta, Bdelta;

            byte cMax = Math.Max(Math.Max(R, G), B);// calculate lightness (intensity)
            byte cMin = Math.Min(Math.Min(R, G), B);
            I = (byte)((((cMax + cMin) * HSIMAX) + RGBMAX) / (2 * RGBMAX));

            if (cMax == cMin)
            {          // r=g=b --> achromatic case
                S = 0;					// saturation
                H = HSIUNDEFINED;       // hue
            }
            else
            {                      // chromatic case
                if (I <= (HSIMAX / 2))	// saturation
                    S = (byte)((((cMax - cMin) * HSIMAX) + ((cMax + cMin) / 2)) / (cMax + cMin));
                else
                    S = (byte)((((cMax - cMin) * HSIMAX) + ((2 * RGBMAX - cMax - cMin) / 2)) / (2 * RGBMAX - cMax - cMin));
                // hue
                Rdelta = (UInt16)((((cMax - R) * (HSIMAX / 6)) + ((cMax - cMin) / 2)) / (cMax - cMin));
                Gdelta = (UInt16)((((cMax - G) * (HSIMAX / 6)) + ((cMax - cMin) / 2)) / (cMax - cMin));
                Bdelta = (UInt16)((((cMax - B) * (HSIMAX / 6)) + ((cMax - cMin) / 2)) / (cMax - cMin));

                if (R == cMax)
                    H = (byte)(Bdelta - Gdelta);
                else if (G == cMax)
                    H = (byte)((HSIMAX / 3) + Rdelta - Bdelta);
                else // B == cMax
                    H = (byte)(((2 * HSIMAX) / 3) + Gdelta - Rdelta);

                //if (H < 0) H += HSIMAX;     //always false
                if (H > HSIMAX) H -= HSIMAX;
            }
            hsi.H = (float)(H / 255f);
            hsi.S = (float)(S / 255f);
            hsi.I = (float)(I / 255f);


            return hsi;
        }
        /// <summary>
        /// Convert HSI colorspace to RGB colorspace.
        /// </summary>
        public static VectorRgb HSI2RGB(VectorHsi hsi)
        {
            float h = hsi.H;
            float s = hsi.S;
            float i = hsi.I;
            
            //method from CxImageLib. ximadsp.cpp file.
            float m1, m2;
            byte R, G, B;

            h = (float)h * 360.0f;

            if (i <= 0.5) m2 = i * (1 + s);
            else m2 = i + s - i * s;

            m1 = 2 * i - m2;

            if (s == 0)
            {
                R = G = B = (byte)(i * 255.0f);
            }
            else
            {
                R = (byte)(HueToRGB(m1, m2, h + 120) * 255.0f);
                G = (byte)(HueToRGB(m1, m2, h) * 255.0f);
                B = (byte)(HueToRGB(m1, m2, h - 120) * 255.0f);
            }

            return new VectorRgb(R, G, B);
        }
        /// <summary>
        /// Convert HSI colorspace to Color struct.
        /// </summary>
        public static Color HSI2COLOR(VectorHsi hsi)
        {
            float h = hsi.H;
            float s = hsi.S;
            float i = hsi.I;

            float m1, m2;
            byte R, G, B;

            h = (float)h * 360.0f;

            if (i <= 0.5) m2 = i * (1 + s);
            else m2 = i + s - i * s;

            m1 = 2 * i - m2;

            if (s == 0)
            {
                R = G = B = (byte)(i * 255.0f);
            }
            else
            {
                R = (byte)(HueToRGB(m1, m2, h + 120) * 255.0f);
                G = (byte)(HueToRGB(m1, m2, h) * 255.0f);
                B = (byte)(HueToRGB(m1, m2, h - 120) * 255.0f);
            }

            return Color.FromArgb(R, G, B);
        }
        /// <summary>
        /// Convert RGB colorspace to YUV colorspace.
        /// </summary>
        public static VectorRgb RGB2YUV(VectorRgb rgb)
        {
            int Y, U, V;
            byte R = rgb.R;
            byte G = rgb.G;
            byte B = rgb.B;

            Y = (int)(0.299f * R + 0.587f * G + 0.114f * B);
            U = (int)((B - Y) * 0.565f + 128);
            V = (int)((R - Y) * 0.713f + 128);

            Y = Math.Min(255, Math.Max(0, Y));
            U = Math.Min(255, Math.Max(0, U));
            V = Math.Min(255, Math.Max(0, V));

            return new VectorRgb(Y, U, V);
        }
        /// <summary>
        /// Convert YUV colorspace to RGB colorspace.
        /// </summary>
        public static VectorRgb YUV2RGB(VectorRgb yuv)
        { 
            int R, G, B;
            byte Y = yuv.R;
            int U = yuv.G - 128;
            int V = yuv.B - 128;

            R = (int)(Y + 1.403f * V);
            G = (int)(Y - 0.344f * U - 0.714f * V);
            B = (int)(Y + 1.770f * U);

            R = Math.Min(255, Math.Max(0, R));
            G = Math.Min(255, Math.Max(0, G));
            B = Math.Min(255, Math.Max(0, B));

            return new VectorRgb(R, G, B);
        }
        /// <summary>
        /// Convert RGB colorspace to gray level in byte.
        /// </summary>
        public static byte RGB2GRAY(VectorRgb rgb)
        {
          //return (byte)(rgb.B * 0.3f + rgb.G * 0.59f + rgb.R * 0.11f);
            return (byte)(rgb.R * 0.299 + rgb.G * 0.587 + rgb.B * 0.114);   
        }
        /// <summary>
        /// Convert RGB colorspace to gray level in float.
        /// </summary>
        public static float RGB2GRAYF(VectorRgb rgb)
        {
          //return (rgb.B * 0.3f + rgb.G * 0.59f + rgb.R * 0.11f) / 255.0f;
            return (rgb.R * 0.299f + rgb.G * 0.587f + rgb.B * 0.114f) / 255.0f;
        }
        /// <summary>
        /// Convert RGB colorspace to gray level in integer.
        /// </summary>
        public static int RGB2GRAYI(VectorRgb rgb)
        {
          //return (int)(rgb.B * 0.3f + rgb.G * 0.59f + rgb.R * 0.11f);
            return (int)(rgb.R * 0.299 + rgb.G * 0.587 + rgb.B * 0.114);
        }

        #endregion
    }
}
