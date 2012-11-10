/////////////////////////////////////////////////////////////////////////////////
// Colour Image Processing Library (CipLibNet)                                 //
// Copyright (C) Andrew [kyzmitch] Ermoshin.                                   //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

#pragma warning disable 1591

namespace Cip.Foundations
{
    
    /// <summary>
    /// HSI colorspace.
    /// </summary>
    public class VectorHsi
    {
        #region Public Fields

        /// <summary>
        /// Hue component
        /// </summary>
        /// <remarks>Hue ranges [0,1]</remarks>
        private float h;
        /// <summary>
        /// Saturation component
        /// </summary>
        /// <remarks>Saturation ranges [0,1]</remarks>
        private float s;
        /// <summary>
        /// Intensity component
        /// </summary>
        /// <remarks>Intensity ranges [0,1]</remarks>
        private float i;

        #endregion

        #region Constructor

        public VectorHsi() { }
        /// <summary>
        /// Initializes a new instance of the HSI class
        /// </summary>
        /// <param name="h">hue</param>
        /// <param name="s">saturation</param>
        /// <param name="i">intensity</param>
        public VectorHsi(float h, float s, float i)
        {
            this.h = h;
            this.s = s;
            this.i = i;
        }
        /// <summary>
        /// Copy Constructor
        /// </summary>
        /// <param name="vec">Vector HSI</param>
        public VectorHsi(VectorHsi vec)
        {
            this.h = vec.h;
            this.s = vec.s;
            this.i = vec.i;
        }
        /// <summary>
        /// Constructor - transformation RGB vector to HSI
        /// </summary>
        /// <param name="vec">RGB vector</param>
        public VectorHsi(VectorRgb vec)
        {
            VectorHsi vecHsi = Cip.Foundations.ColorspaceHelper.RGB2HSI(vec);
            this.h = vecHsi.h;
            this.s = vecHsi.s;
            this.i = vecHsi.i;
        }
        /// <summary>
        /// Constructor - transformation Color pixel to HSI vector
        /// </summary>
        /// <param name="pixel">Color pixel</param>
        public VectorHsi(Color pixel)
        {
            VectorHsi hsi = Cip.Foundations.ColorspaceHelper.RGB2HSI(pixel.R, pixel.G, pixel.B);
            this.h = hsi.h;
            this.s = hsi.s;
            this.i = hsi.i;
        }

        #endregion

        #region Components change

        public void ChangeIntensity(float k)
        {
            this.i = ClampFloatHsi(this.i + k);
        }
        public void ChangeSaturation(float k)
        {
            this.s = ClampFloatHsi(this.s + k);
        }
        public void ChangeHue(float k)
        {
            this.h = ClampFloatHsi(this.h + k);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Clamp
        /// </summary>
        public static VectorHsi Clamp(VectorHsi source, float min, float max)
        {
            VectorHsi result = source;
            if (result.h < min) result.h = min;
            if (result.h > max) result.h = max;
            if (result.s < min) result.s = min;
            if (result.s > max) result.s = max;
            if (result.i < min) result.i = min;
            if (result.i > max) result.i = max;
            return result;
        }
        public static float ClampFloatHsi(float value)
        {
            if (value > 1.0f)
                return 1.0f;
            if (value < 0.0f)
                return 0.0f;
            return value;
        }
        /// <summary>
        /// Transformation HSI into RGB
        /// </summary>
        /// <returns>RGB vector</returns>
        public VectorRgb ToVectorRGB()
        {
            return ColorspaceHelper.HSI2RGB(this);
        }
        /// <summary>
        /// Transformation HSI into Color
        /// </summary>
        /// <returns>Color pixel</returns>
        public Color ToColor()
        {
            return Cip.Foundations.ColorspaceHelper.HSI2COLOR(this);
        }

        #endregion

        #region Properties

        public float this[int index]
        {
            get
            {
                if (0 == index)
                    return h;
                else
                    if (1 == index)
                        return s;
                    else
                        return i;
            }

            set
            {
                if (0 == index)
                    h = value;
                else
                    if (1 == index)
                        s = value;
                    else
                        i = value;
            }
        }
        /// <summary>
        /// Get and set Hue component.
        /// </summary>
        public float H
        {
            get { return this.h; }
            set { this.h = value; }
        }
        /// <summary>
        /// Get and set Saturation component.
        /// </summary>
        public float S
        {
            get { return this.s; }
            set { this.s = value; }
        }
        /// <summary>
        /// Get and set Intensity component.
        /// </summary>
        public float I
        {
            get { return this.i; }
            set { this.i = value; }
        }

        #endregion

        #region Operators and Override

        /// <summary>
        /// Convert components to string
        /// </summary>
        /// <returns>string of HSI</returns>
        public override string ToString()
        {
            return String.Format("HSI: ({0},{1},{2})", this.h, this.s, this.i);
        }

        #endregion

    }
}
