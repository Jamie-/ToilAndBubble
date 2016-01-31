using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace imageTester
{
    class HSVColor
    {
        public HSVColor(Color c)
        {
            this.RgbColor = c;
        }
        public HSVColor(double h, double s, double v)
        {
            this.hue = h;
            this.saturation = s;
            this.value = v;
        }
        public double hue;
        public double saturation;
        public double value;
        double Hue { get { return this.hue; }
            set { this.hue = (value<0)?255+value:value%255; }
        }
        double Saturation { get { return this.saturation; } set { this.saturation = value; } }
        double Value { get { return this.value; } set { this.value = value; } }
        public Color RgbColor
        {
            get
            {
                return HSVToColor(this.hue, this.saturation, this.value);
            }
            set
            {
                ColorToHSV(value,out this.hue, out this.saturation, out this.value);
            }
        }

        public static void ColorToHSV(Color color, out double hue, out double saturation, out double value)
        {
            int max = Math.Max(color.R, Math.Max(color.G, color.B));
            int min = Math.Min(color.R, Math.Min(color.G, color.B));

            hue = color.GetHue();
            saturation = (max == 0) ? 0 : 1d - (1d * min / max);
            value = max / 255d;
        }

        public static Color HSVToColor(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }

    }
   
}
