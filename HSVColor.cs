using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WGP
{
    /// <summary>
    /// Color using the HSV format.
    /// </summary>
    public struct HSVColor : IEquatable<HSVColor>
    {
        #region Private Fields

        private float hue;
        private float saturation;
        private float value;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Copy Constructor.
        /// </summary>
        /// <param name="copy">Color to copy.</param>
        public HSVColor(HSVColor copy)
        {
            hue = copy.hue;
            saturation = copy.saturation;
            value = copy.value;
            A = copy.A;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="h">Hue.</param>
        /// <param name="s">Saturation.</param>
        /// <param name="v">Value.</param>
        /// <param name="a">Alpha.</param>
        public HSVColor(float h, float s, float v, byte a = 255)
        {
            hue = h;
            saturation = s;
            value = v;
            A = a;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="color">SFML color.</param>
        public HSVColor(Color color)
        {
            float min, max, delta;
            float r, g, b;
            r = color.R / 255f;
            g = color.G / 255f;
            b = color.B / 255f;
            min = Utilities.Min(r, Utilities.Min(g, b));
            max = Utilities.Max(r, Utilities.Max(g, b));
            value = max;
            delta = max - min;
            if (delta < .0001f)
            {
                saturation = 0;
                hue = 0;
            }
            else
            {
                if (max > 0)
                {
                    saturation = delta / max;
                    if (r == max)
                        hue = (int)((g - b) / delta * 60);
                    else if (g == max)
                        hue = (int)(((b - r) / delta + 2) * 60);
                    else
                        hue = (int)(((r - g) / delta + 4) * 60);
                    if (hue < 0)
                        hue += 360;
                }
                else
                {
                    saturation = 0;
                    hue = 0;
                }
            }
            A = color.A;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Alpha channel of the color.
        /// </summary>
        public byte A { get; set; }

        /// <summary>
        /// Hue of the color.
        /// </summary>
        /// <value>Must be between [0,360[</value>
        public float H
        {
            get => hue;
            set => hue = ((value % 360) + 360) % 360;
        }

        /// <summary>
        /// Saturation of the color.
        /// </summary>
        /// <value>Must be between [0,1]</value>
        public float S
        {
            get => saturation;
            set => saturation = Utilities.Max(0, Utilities.Min(1, value));
        }

        /// <summary>
        /// Value of the color.
        /// </summary>
        /// <value>Must be between [0,1]</value>
        public float V
        {
            get => value;
            set => this.value = Utilities.Max(0, Utilities.Min(1, value));
        }

        #endregion Public Properties

        #region Public Methods

        public static implicit operator Color(HSVColor color)
        {
            return color.ToRgb();
        }

        public static implicit operator HSVColor(Color color)
        {
            return new HSVColor(color);
        }

        public bool Equals(HSVColor other)
        {
            return (H == other.H && S == other.S && V == other.V && A == other.A);
        }

        public override bool Equals(object obj)
        {
            return Equals((HSVColor)obj);
        }

        public override int GetHashCode()
        {
            return ToRgb().GetHashCode();
        }

        /// <summary>
        /// Convert the color to the RGB format.
        /// </summary>
        /// <returns>RGB color0</returns>
        public Color ToRgb()
        {
            byte resA = 0, resB = 0, resG = 0, resR = 0;
            resA = A;
            float hh, p, q, t, ff;
            int i;

            if (S == 0)
            {
                resR = (byte)(V * 255);
                resG = (byte)(V * 255);
                resB = (byte)(V * 255);
                return Color.FromArgb(resA, resR, resG, resB);
            }
            hh = H;
            hh /= 60;
            i = (int)hh;
            ff = hh - i;
            p = V * (1f - S);
            q = V * (1f - S * ff);
            t = V * (1f - S * (1f - ff));

            switch (i)
            {
                case 0:
                    resR = (byte)(V * 255);
                    resG = (byte)(t * 255);
                    resB = (byte)(p * 255);
                    break;

                case 1:
                    resR = (byte)(q * 255);
                    resG = (byte)(V * 255);
                    resB = (byte)(p * 255);
                    break;

                case 2:
                    resR = (byte)(p * 255);
                    resG = (byte)(V * 255);
                    resB = (byte)(t * 255);
                    break;

                case 3:
                    resR = (byte)(p * 255);
                    resG = (byte)(q * 255);
                    resB = (byte)(V * 255);
                    break;

                case 4:
                    resR = (byte)(t * 255);
                    resG = (byte)(p * 255);
                    resB = (byte)(V * 255);
                    break;

                case 5:
                    resR = (byte)(V * 255);
                    resG = (byte)(p * 255);
                    resB = (byte)(q * 255);
                    break;
            }
            return Color.FromArgb(resA, resR, resG, resB);
        }

        public override string ToString()
        {
            return "{ [H:" + H + "] , [S:" + S + "] , [V:" + V + "] , [A:" + A + "] }";
        }

        #endregion Public Methods
    }
}