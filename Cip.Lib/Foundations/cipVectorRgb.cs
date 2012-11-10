/////////////////////////////////////////////////////////////////////////////////
// Colour Image Processing Library (CipLibNet)                                 //
// Copyright (C) Andrew [kyzmitch] Ermoshin.                                   //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using System;
using System.Drawing;
using System.Runtime.InteropServices;

#pragma warning disable 1591

namespace Cip.Foundations
{
    
    /// <summary>
    /// RGB colorspace
    /// </summary>
    /// <remarks>All components normalized</remarks>
    public class VectorRgb
    {
        #region Public Fields

        /// <summary>
        /// Red component
        /// </summary>
        private byte r;

        /// <summary>
        /// Green component
        /// </summary>
        private byte g;

        /// <summary>
        /// Blue component
        /// </summary>
        private byte b;

        #endregion

        #region Constructor

        public VectorRgb() { r = 0; g = 0; b = 0; }
        public VectorRgb(byte r, byte g, byte b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }
        public VectorRgb(byte color)
        {
            r = g = b = color;
        }
        public VectorRgb(int r, int g, int b)
        {
            this.r = (byte)r;
            this.g = (byte)g;
            this.b = (byte)b;
        }
        /// <summary>
        /// Initializes a new instance of the RGB class
        /// </summary>
        /// <param name="r">brightness of red</param>
        /// <param name="g">brightness of green</param>
        /// <param name="b">brightness of blue</param>
        public VectorRgb(float r, float g, float b)
        {
            this.r = (byte)(r * 255f);
            this.g = (byte)(g * 255f);
            this.b = (byte)(b * 255f);
        }
        /// <summary>
        /// Initializes a new instance of the RGB class
        /// </summary>
        /// <param name="color">Input color</param>
        public VectorRgb(Color color)
        {
            this.r = color.R;
            this.g = color.G;
            this.b = color.B;
        }
        /// <summary>
        /// Copy Constructor
        /// </summary>
        /// <param name="vec">Color vector</param>
        public VectorRgb(VectorRgb vec)
        {
            r = vec.r;
            g = vec.g;
            b = vec.b;
        }
        /// <summary>
        /// Constructor - transformation HSI to RGB
        /// </summary>
        /// <param name="vec">Vector HSI</param>
        public VectorRgb(VectorHsi vec)
        {
            System.Drawing.Color cl = ColorspaceHelper.HSI2COLOR(vec);
            this.r = cl.R;
            this.g = cl.G;
            this.b = cl.B;
        }

        #endregion

        #region Operators

        /// <summary>
        /// Conversion from Cip.Foundations.VectorHsi to Cip.Foundations.VectorRgb.
        /// </summary>
        /// <param name="vec">Source.</param>
        /// <returns>Destination.</returns>
        public static implicit operator VectorRgb(VectorHsi vec)
        {
            return new VectorRgb(vec);
        }
        /// <summary>
        /// Conversion from Cip.Foundations.VectorRgb to Cip.Foundations.VectorHsi.
        /// </summary>
        /// <param name="vec">Source.</param>
        /// <returns>Destination.</returns>
        public static explicit operator VectorHsi(VectorRgb vec)
        {
            return ColorspaceHelper.RGB2HSI(vec);
        }

        public static VectorRgb operator +(VectorRgb left, byte right)
        {
            
            return new VectorRgb(ClampByte(left.r + right), 
                                 ClampByte(left.g + right), 
                                 ClampByte(left.b + right));
        }
        public static VectorRgb operator +(VectorRgb left, int right)
        {

            return new VectorRgb(ClampByte(left.r + right),
                                 ClampByte(left.g + right),
                                 ClampByte(left.b + right));
        }
        public static VectorRgb operator -(VectorRgb left, byte right)
        {
            return new VectorRgb(ClampByte(left.r - right),
                                 ClampByte(left.g - right),
                                 ClampByte(left.b - right));
        }
        public static VectorRgb operator -(VectorRgb left, int right)
        {
            return new VectorRgb(ClampByte(left.r - right),
                                 ClampByte(left.g - right),
                                 ClampByte(left.b - right));
        }
        public static VectorRgb operator +(VectorRgb left, VectorRgb right)
        {
            return new VectorRgb(ClampByte(left.r + right.r),
                                 ClampByte(left.g + right.g),
                                 ClampByte(left.b + right.b));
        }
        public static VectorRgb operator -(VectorRgb left, VectorRgb right)
        {
            return new VectorRgb(ClampByte(left.r - right.r), 
                                 ClampByte(left.g - right.g), 
                                 ClampByte(left.b - right.b));
        }
        public static VectorRgb operator *(VectorRgb left, float right)
        {
            return new VectorRgb(ClampByte(left.r * right), 
                                 ClampByte(left.g * right), 
                                 ClampByte(left.b * right));
        }
        public static VectorRgb operator *(float left, VectorRgb right)
        {
            return new VectorRgb(ClampByte(left * right.r), 
                                 ClampByte(left * right.g), 
                                 ClampByte(left * right.b));
        }
        public static VectorRgb operator *(VectorRgb left, VectorRgb right)
        {
            return new VectorRgb(ClampByte(left.r * right.r), 
                                 ClampByte(left.g * right.g), 
                                 ClampByte(left.b * right.b));
        }
        public static bool operator <(VectorRgb left, VectorRgb right)
        {
            return left.r * left.b * left.g < right.r * right.g * right.b ? true : false;
        }
        public static bool operator >(VectorRgb left, VectorRgb right)
        {
            return left.r * left.b * left.g > right.r * right.g * right.b ? true : false;
        }
        public static bool operator ==(VectorRgb left, VectorRgb right)
        {
            return ((left.r == right.r) && (left.g == right.g) && (left.b == right.b)) ? true : false;
        }
        public static bool operator !=(VectorRgb left, VectorRgb right)
        {
            return ((left.r == right.r) && (left.g == right.g) && (left.b == right.b)) ? false : true;
        }

        public override bool Equals(object obj)
        {
            if (this.GetType() != obj.GetType())
                return false;
            else 
                if (this != (VectorRgb)obj)
                    return false;
                else
                    return true;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return String.Format("RGB: ({0},{1},{2})", this.r, this.g, this.b);
        }
        

        #endregion

        #region Public Methods

        /// <summary>
        /// Intensity in RGB color space.
        /// </summary>
        /// <returns>gray level.</returns>
        public byte GetIntensity()
        {
            return Cip.Foundations.ColorspaceHelper.RGB2GRAY(this);
        }
        /// <summary>
        /// Transform color VectorRgb to monochrome version of him.
        /// </summary>
        /// <returns>Monochrome RGB vector.</returns>
        public VectorRgb ToMonochrome()
        {
            byte gray = Cip.Foundations.ColorspaceHelper.RGB2GRAY(this);
            return new VectorRgb(gray, gray, gray);
        }
        /// <summary>
        /// Transform this RGB vector to monochrome version of him.
        /// </summary>
        public void ToMono()
        {
            byte gray = Cip.Foundations.ColorspaceHelper.RGB2GRAY(this);
            this.r = gray;
            this.g = gray;
            this.b = gray;
        }
        /// <summary>
        /// Transformation RGB into HSI
        /// </summary>
        public VectorHsi ToVectorHSI()
        {
            return ColorspaceHelper.RGB2HSI(this);
        }
        /// <summary>
        /// Return array of color components ranged [0,1]
        /// </summary>
        public byte[] ToArray()
        {
            return new byte[] { r, g, b };
        }
        /// <summary>
        /// Transformation to Color object
        /// </summary>
        /// <returns> RGB color </returns>
        public Color ToColor()
        { 
            return Color.FromArgb(this.r, this.g, this.b);
        }
        /// <summary>
        /// Clamping of RGB vector
        /// </summary>
        /// <param name="source"> Source vector</param>
        /// <param name="min">minimum</param>
        /// <param name="max">maximum</param>
        /// <returns></returns>
        public static VectorRgb Clamp(VectorRgb source, byte min, byte max)
        {
            VectorRgb result = source;

            if (result.r < min) result.r = min;
            if (result.r > max) result.r = max;
            if (result.g < min) result.g = min;
            if (result.g > max) result.g = max;
            if (result.b < min) result.b = min;
            if (result.b > max) result.b = max;

            return result;
        }
        /// <summary>
        /// Mix of 2 vectors
        /// </summary>
        /// <param name="left">Left vector</param>
        /// <param name="right">Right vector</param>
        /// <param name="coeff">coefficient</param>
        /// <returns></returns>
        public static VectorRgb Mix(VectorRgb left, VectorRgb right, float coeff)
        {
            return left * (1.0f - coeff) + right * coeff;
        }
        /// <summary>
        /// Dot of 2 vectors
        /// </summary>
        /// <param name="left">Left vector</param>
        /// <param name="right">Right vector</param>
        /// <returns>Double dot value</returns>
        public static float Dot(VectorRgb left, VectorRgb right)
        {
            int r = left.r * right.r; // 65025.0f
            int g = left.g * right.g;
            int b = left.b * right.b;
            return (r + g + b) / 65025.0f;
        }
        public static byte ClampByte(int value)
        {
            byte result = (byte)value;
            if (value > 255)
                result = 255;
            if (value < 0)
                result = 0;
            return result;
        }
        public static byte ClampByte(float value)
        {
            byte result = (byte)value;
            if (value > 255.0f)
                result = 255;
            if (value < 0.0f)
                result = 0;
            return result;
        }

        #endregion

        #region Properties

        public byte this[int index]
        {
            get
            {
                if (0 == index)
                    return r;
                else
                    if (1 == index)
                        return g;
                    else
                        return b;
            }

            set
            {
                if (0 == index)
                    r = value;
                else
                    if (1 == index)
                        g = value;
                    else
                        b = value;
            }
        }
        /// <summary>
        /// Get or set Red component.
        /// </summary>
        public byte R
        {
            get { return this.r; }
            set { this.r = value; }
        }
        /// <summary>
        /// Get or set Green component.
        /// </summary>
        public byte G
        {
            get { return this.g; }
            set { this.g = value; }
        }
        /// <summary>
        /// Get or set Blue component.
        /// </summary>
        public byte B
        {
            get { return this.b; }
            set { this.b = value; }
        }
        /// <summary>
        /// Get black color.
        /// </summary>
        public static VectorRgb Black
        {
            get
            {
                return new VectorRgb(0, 0, 0);
            }
        }
        /// <summary>
        /// Get white color.
        /// </summary>
        public static VectorRgb White
        {
            get
            {
                return new VectorRgb(255, 255, 255);
            }
        }
        /// <summary>
        /// Get grey color.
        /// </summary>
        public static VectorRgb Grey
        {
            get
            {
                return new VectorRgb(125, 125, 125);
            }
        }
        /// <summary>
        /// Get red color.
        /// </summary>
        public static VectorRgb Red
        {
            get
            {
                return new VectorRgb(255, 0, 0);
            }
        }
        /// <summary>
        /// Get green color.
        /// </summary>
        public static VectorRgb Green
        {
            get
            {
                return new VectorRgb(0, 255, 0);
            }
        }
        /// <summary>
        /// Get blue color.
        /// </summary>
        public static VectorRgb Blue
        {
            get
            {
                return new VectorRgb(0, 0, 255);
            }
        }

        #endregion
    }
}
