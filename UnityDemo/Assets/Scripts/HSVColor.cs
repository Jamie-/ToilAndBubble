using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class HSVColor
    {
        const bool logging = false;
        public static void Log(String msg)
        {
            if (logging)
            {
                Debug.Log(msg);
            }
        }


        public HSVColor(Color32 c)
        {
            RgbColor = c;
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
        double Hue{get { return this.hue; }set { this.hue = value; }}
        double Saturation { get { return this.saturation; } set { this.saturation = value; } }
        double Value { get { return this.value; } set { this.value = value; } }
        public Color32 RgbColor
        {
            get
            {
                return HSVToColor(this.hue, this.saturation, this.value);
            }
            set
            {
                ColorToHSV(value, out this.hue, out this.saturation, out this.value);
            }
        }
        public static void ColorToHSV(Color32 c, out double hue, out double saturation, out double value)
        {
            Log("r=" + c.r + ", " + "g=" + c.g + ", " + "b=" + c.b);
            double r = ((double)c.r) / 255d;
            double g = ((double)c.g) / 255d;
            double b = ((double)c.b) / 255d;
            Log("r=" + r + ", " + "g=" + g + ", " + "b=" + b);

            double cMax = Math.Max(r, Math.Max(g, b));
            double cMin = Math.Min(r, Math.Min(g, b));
            Log("Max=" + cMax + " Min=" + cMin); 

            double delta = cMax - cMin;
            Log("Delta=" + delta);

            if (delta == 0)
            {  
                hue = 0;
                Log("NoMax");
            }
            else if (cMax == r)
            {
                /*Math.Abs(g-b) prevents the hue from being negative if 
                 *b is larger than g, which might have been causing the grey goo
                 *scenario. same applies to for cMax=g and cMax=b
                 */                
                hue = 60d * ((Math.Abs(g - b) / delta) % 6d);
                Log("R Max");
            }
            else if (cMax == g)
            {
                hue = 60d * ((Math.Abs(b - r) / delta) + 2d);
                Log("G Max");
            }
            else
            {
                hue = 60d * ((Math.Abs(r - g) / delta) + 4d);
                Log("B Max");
            }

            Log("Hue=" + hue);

            if (delta == 0)
            {
                saturation = 0;
            }
            else
            {
                saturation = delta / cMax;
            }
            value = cMax;

            saturation = 0.8;
            value = 1d;            
        }

        public static Color32 HSVToColor(double hue, double saturation, double value)
        {
            
            double c = value * saturation;
            double x = c * (1d - Math.Abs(((hue / 60d) % 2d) - 1d));
            double m = value - c;

            double r = 0, g = 0, b = 0;

            switch (((int)Math.Floor(hue / 60d)) % 6)
            {
                case 0:
                    r = c; g = x; b = 0;
                    break;
                case 1:
                    r = x; g = c; b = 0;
                    break;
                case 2:
                    r = 0; g = c; b = x;
                    break;
                case 3:
                    r = 0; g = x; b = c;
                    break;
                case 4:
                    r = x; g = 0; b = c;
                    break;
                case 5:
                    r = c; g = 0; b = x;
                    break;
            }

            byte R, G, B;

            R = (byte)((r + m) * 255);
            G = (byte)((g + m) * 255);
            B = (byte)((b + m) * 255);

            
            return new Color32(R, G, B, 255);
        }
    }
}
