using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace imageTester
{
    class PixelSpaceship
    {

        public const int EMPTY = 0;
        public const int AVOID = 1;
        public const int SOLID = 2;
        public const int COKPT = 3; // ::added to aid coloring
        uint seed;
        uint colorseed; // ::added to aid coloring
        int[,] grid;
        int xscale, yscale;
        int xmargin, ymargin;
        miniMT rng;
        int cols = 12;
        int rows = 12;
        public bool xflip;
        public bool yflip;
        public PixelSpaceship(miniMT rng)
        {
            xflip = false;
            yflip = false;
            this.rng = rng;
            grid = new int[rows, cols];
            xscale = yscale = 1;
            xmargin = ymargin = 0;
        }
        public void recolor()
        { // ::added to aid coloring
            colorseed = rng.generate();
        }
        public int getHeight()
        {
            return rows * yscale + ymargin * 2;
        }
        public int getWidth()
        {
            return cols * xscale + xmargin * 2;
        }
        public void setMargins(int xm, int ym)
        {
            xmargin = xm;
            ymargin = ym;
        }
        public void setScales(int xs, int ys)
        {
            xscale = xs;
            yscale = ys;
        }
        public void setSeed(uint s)
        {
            seed = s;
        }
        public void wipe()
        {
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    grid[r, c] = EMPTY;
        } // wipe()
        public int[,] GridSeed
        {

            get
            {

                if (xflip)
                {
                    int[,] tgrid = (int[,])this.grid.Clone();
                    for (int r = 0; r < rows; r++)
                    {
                        for (int c = 0; c < cols / 2; c++)
                            tgrid[r, cols - 1 - c] = tgrid[r, c];
                    }
                    return tgrid;
                }
                if (yflip)
                {
                    int[,] tgrid = (int[,])this.grid.Clone();
                    for (int r = 0; r < rows; r++)
                    {
                        for (int c = 0; c < cols / 2; c++)
                            tgrid[rows - 1 - r, c] = tgrid[r, c];
                    }
                    return tgrid;
                }
                return (int[,])grid.Clone();

            }
            set
            {
                Random rand = new Random();
                int bitmask = 1;
                for (int i = 0; i < value.GetLength(0); i++)
                    for (int j = 0; j < value.GetLength(1); j++)
                    {
                        if (value[i, j] == AVOID)
                        {
                            if (rand.Next(5)<=3)
                            {
                                value[i, j] = AVOID;
                            }
                            else
                            {
                                value[i, j] = EMPTY;
                            }
                        }
                    }
                this.grid = value;
                this.rows = value.GetLength(1);
                this.cols = value.GetLength(0);
                for (int r = 0; r < rows; r++)
                {
                    for (int c = 0; c < cols; c++)
                    {
                        int here = grid[r, c];
                        if (here != EMPTY) continue;
                        bool needsolid = false;
                        if ((c > 0) && (grid[r, c - 1] == AVOID)) needsolid = true;
                        if ((c < cols - 1) && (grid[r, c + 1] == AVOID)) needsolid = true;
                        if ((r > 0) && (grid[r - 1, c] == AVOID)) needsolid = true;
                        if ((r < rows - 1) && (grid[r + 1, c] == AVOID)) needsolid = true;
                        if (needsolid)
                            grid[r, c] = SOLID;
                    }
                }

                // /*

                // */
            }
        }

        public Image draw(int basex, int basey)
        {
            Image rval = new Bitmap(basex + xmargin + cols * xscale, basey + ymargin + rows * yscale);
            Graphics g = Graphics.FromImage(rval);
Random rand = new Random();
            // ::added to aid coloring
            // here's one (of many) possible ways you might color them...
            // float[] sats = { 40, 60, 80, 100, 80, 60, 80, 100, 120, 100, 80, 60 };
            // float[] bris = { 40, 70, 100, 130, 160, 190, 220, 220, 190, 160, 130, 100, 70, 40 };

            // noStroke();
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    int x1 = basex + xmargin + c * xscale;
                    int y1 = basey + ymargin + r * yscale;
                    int m = grid[r, c];
                    // ::added to aid coloring
                    // for monochrome just draw SOLID's as black and you're done
                    // otherwise...
                    if (m == SOLID)
                    {
                        //           fill(#000000);
                        // rect(x1, y1, xscale, yscale);
                        g.FillRectangle(new SolidBrush(Color.Black), new RectangleF(x1, y1, xscale, yscale));
                    }
                    else
                    if (m == AVOID)
                    {
                        //float mysat = sats[r];
                        //  float mybri = bris[c]; //+90;
                        uint h = 0;
                        if (r < 6) h = (colorseed & 0xff00) >> 8;
                        else if (r < 9) h = (colorseed & 0xff0000) >> 16;
                        else h = (colorseed & 0xff000000) >> 24;
                        Brush b = new SolidBrush(Color.Black);
                        Pen p = new Pen(b);

                        // g.DrawRectangle(p, new Rectangle(x1, y1, xscale, yscale));
                        //  colorMode(HSB);
                        // fill(h, mysat, mybri);
                        // rect(x1, y1, xscale, yscale);
                    }
                    else
                    if (m == COKPT)
                    {
                        // float mysat = sats[c];
                        // float mybri = bris[r] + 40;
                        //colorMode(HSB);
                        uint h = (colorseed & 0xff);
                        
                        int gray = rand.Next(255);
                        Brush b = new SolidBrush(Color.FromArgb(gray, gray, gray));
                        g.FillRectangle(b, new RectangleF(x1, y1, xscale, yscale));
                        // fill(h, mysat, mybri);
                        // rect(x1, y1, xscale, yscale);
                    }
                }
            }
            return rval;
        }
    }

}
