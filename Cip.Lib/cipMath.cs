/////////////////////////////////////////////////////////////////////////////////
// Colour Image Processing Library (CipLibNet)                                 //
// Copyright (C) Andrew [kyzmitch] Ermoshin.                                   //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using Cip.Foundations;

#pragma warning disable 1591

namespace Cip
{
    public static class CipMath
    {
        /// <summary>
        /// Maximum number in array.
        /// </summary>
        /// <param name="array">Integer array.</param>
        /// <returns>Maximum number in array.</returns>
        public static int Maximum(int[] array)
        {
            int length = array.Length;
            int Max = array[0];
            for (int i = 1; i < length; i++)
            {
                if (array[i] > Max)
                    Max = array[i];
            }
            return Max;
        }

        #region Distance between points

        /// <summary>
        /// Square of distance between 2 RGB byte vectors.
        /// </summary>
        /// <param name="rgb1">RGB vector number 1.</param>
        /// <param name="rgb2">RGB vector number 2.</param>
        /// <returns>Square of distance</returns>
        public static int SquareOfDistance(Cip.Foundations.VectorRgb rgb1,
                                           Cip.Foundations.VectorRgb rgb2)
        {
            int sum = 0;
            int s0 = rgb1[0] - rgb2[0];
            int s1 = rgb1[1] - rgb2[1];
            int s2 = rgb1[2] - rgb2[2];
            sum += s0 * s0;
            sum += s1 * s1;
            sum += s2 * s2;
            return sum;
        }
        /// <summary>
        /// Distance between 2 RGB byte vectors.
        /// </summary>
        /// <param name="rgb1">RGB vector number 1.</param>
        /// <param name="rgb2">RGB vector number 2.</param>
        /// <returns>Double number, which represents distance.</returns>
        public static double Distance(Cip.Foundations.VectorRgb rgb1,
                                      Cip.Foundations.VectorRgb rgb2)
        {
            return System.Math.Sqrt(Cip.CipMath.SquareOfDistance(rgb1, rgb2));
        }
        /// <summary>
        /// Square of distance between 2 points.
        /// </summary>
        /// <param name="point1">Point number 1.</param>
        /// <param name="point2">Point number 2.</param>
        /// <returns>Square of distance.</returns>
        public static int SquareOfDistance(int[] point1, int[] point2)
        {
            int dimension1 = point1.Length;
            if (dimension1 == point2.Length)
            {
                int sum = 0;
                int s = 0;
                for (int i = 0; i < dimension1; i++)
                {
                    s = point1[i] - point2[i];
                    sum += s * s;
                }
                return sum;
            }
            else
            {
                throw new ArgumentException("In Cip.CipMath.SquareOfDistance method dimensions of points does not coincides", "rgb2");
            }
        }
        /// <summary>
        /// Distance between 2 points.
        /// </summary>
        /// <param name="point1">Point number 1.</param>
        /// <param name="point2">Point number 2.</param>
        /// <returns>Double number, which represents distance.</returns>
        public static double Distance(int[] point1, int[] point2)
        {
            return System.Math.Sqrt(Cip.CipMath.SquareOfDistance(point1,point2));
        }
        /// <summary>
        /// Square of distance between 2 points.
        /// </summary>
        /// <param name="point1">Point number 1.</param>
        /// <param name="point2">Point number 2.</param>
        /// <returns>Square of distance.</returns>
        public static int SquareOfDistance(System.Drawing.Point point1,
                                           System.Drawing.Point point2)
        {
            int sum = 0;
            int s1 = point1.X - point2.X;
            int s2 = point1.Y - point2.Y;
            sum += s1 * s1;
            sum += s2 * s2;
            return sum;
        }
        /// <summary>
        /// Distance between 2 points.
        /// </summary>
        /// <param name="point1">Point number 1.</param>
        /// <param name="point2">Point number 2.</param>
        /// <returns>Double number, which represents distance.</returns>
        public static double Distance(System.Drawing.Point point1,
                                      System.Drawing.Point point2)
        {
            return System.Math.Sqrt(Cip.CipMath.SquareOfDistance(point1, point2));
        }

        #endregion Distance between points


    }
}
