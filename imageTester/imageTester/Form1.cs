using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace imageTester
{
    public partial class Form1 : Form
    {
PixelSpaceshipFitter fitter;
PixelSpaceship ship;
miniMT rng; // want a slightly more robust rng for 32 significant bits
int currentSeed = 0;




void next()
{
   // background(255);
    fitter.make(++currentSeed);
}

void draw()
{
     pictureBox1.BackgroundImage= fitter.drawone();
    if (fitter.at00())
        next();
}

        public Form1()
        {
            InitializeComponent();
            Image tst = new Bitmap(50, 50);
            Graphics g = Graphics.FromImage(tst);
            g.FillRectangle(new SolidBrush(Color.Aqua), new Rectangle(25, 25, 25, 25));
            pictureBox1.BackgroundImage = tst;        
            rng = new miniMT();            
            fitter = new PixelSpaceshipFitter(480 / 16, 480 / 16, rng);
            next();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {

            next();
            draw();
        }
    }
}
// Pixel Spaceships
// David Bollinger - July 2006
// http://www.davebollinger.com
// for Processing 0115 beta
// (updated for 0119 Beta)

/**
Click mouse to advance early to next pattern<br>



*/
// the generator
 class PixelSpaceship
{
    const int cols = 12;
    const int rows = 12;
    const int EMPTY = 0;
    const int AVOID = 1;
    const int SOLID = 2;
    const int COKPT = 3; // ::added to aid coloring
    uint seed;
    uint colorseed; // ::added to aid coloring
    int[,] grid;
    int xscale, yscale;
    int xmargin, ymargin;
    miniMT rng;
   public PixelSpaceship(miniMT rng)
    {
        this.rng = rng;
        grid = new int[rows,cols];
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
                grid[r,c] = EMPTY;
    } // wipe()
    public void generate()
    {
        wipe();
        // FILL IN THE REQUIRED SOLID CELLS
        int[] solidcs = { 5, 5, 5, 5, 5 };
        int[] solidrs = { 2, 3, 4, 5, 9 };
        for (int i = 0; i < 5; i++)
        {
            int c = solidcs[i];
            int r = solidrs[i];
            grid[r,c] = SOLID;
        }
        // FILL IN THE SEED-SPECIFIED BODY CELLS, AVOID OR EMPTY
        int[] avoidcs = { 4, 5, 4, 3, 4, 3, 4, 2, 3, 4, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3, 4, 3, 4, 5 };
        int[] avoidrs = { 1, 1, 2, 3, 3, 4, 4, 5, 5, 5, 6, 6, 6, 7, 7, 7, 8, 8, 8, 9, 9, 9, 9, 10, 10, 10 };
        int bitmask = 1;
        for (int i = 0; i < 26; i++)
        {
            int c = avoidcs[i];
            int r = avoidrs[i];
            grid[r,c] = ((seed & bitmask) != 0) ? AVOID : EMPTY;
            bitmask <<= 1;
        }
        // FLIP THE SEED-SPECIFIED COCKPIT CELLS, SOLID OR EMPTY
        int[] emptycs = { 4, 5, 4, 5, 4, 5 };
        int[] emptyrs = { 6, 6, 7, 7, 8, 8 };
        bitmask = 1 << 26;
        for (int i = 0; i < 6; i++)
        {
            int c = emptycs[i];
            int r = emptyrs[i];
            grid[r,c] = ((seed & bitmask) != 0) ? SOLID : COKPT; // ::added to aid coloring
            bitmask <<= 1;
        }
        // SKINNING -- wrap the AVOIDs with SOLIDs where EMPTY
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                int here = grid[r,c];
                if (here != EMPTY) continue;
                bool needsolid = false;
                if ((c > 0) && (grid[r,c - 1] == AVOID)) needsolid = true;
                if ((c < cols - 1) && (grid[r,c + 1] == AVOID)) needsolid = true;
                if ((r > 0) && (grid[r - 1,c] == AVOID)) needsolid = true;
                if ((r < rows - 1) && (grid[r + 1,c] == AVOID)) needsolid = true;
                if (needsolid)
                    grid[r,c] = SOLID;
            }
        }
        // mirror left side into right side
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols / 2; c++)
                grid[r,cols - 1 - c] = grid[r,c];
        }
    }
    public Image draw(int basex, int basey)
    {
        Image rval = new Bitmap(basex + xmargin + cols * xscale, basey + ymargin + rows * yscale);
        Graphics g = Graphics.FromImage(rval);
        
        // ::added to aid coloring
        // here's one (of many) possible ways you might color them...
        float[] sats = { 40, 60, 80, 100, 80, 60, 80, 100, 120, 100, 80, 60 };
        float[] bris = { 40, 70, 100, 130, 160, 190, 220, 220, 190, 160, 130, 100, 70, 40 };

       // noStroke();
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                int x1 = basex + xmargin + c * xscale;
                int y1 = basey + ymargin + r * yscale;
                int m = grid[r,c];
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
                    float mysat = sats[r];
                    float mybri = bris[c]; //+90;
                    uint h = 0;
                    if (r < 6) h = (colorseed & 0xff00) >> 8;
                    else if (r < 9) h = (colorseed & 0xff0000) >> 16;
                    else h = (colorseed & 0xff000000) >> 24;
                    Brush b = new SolidBrush(HSBToRGB(h, mysat/255f, mybri/255f));
                    Pen p = new Pen(b);
                    g.DrawRectangle(p, new Rectangle(x1, y1, xscale, yscale));
                    //  colorMode(HSB);
                   // fill(h, mysat, mybri);
                   // rect(x1, y1, xscale, yscale);
                }
                else
                if (m == COKPT)
                {
                    float mysat = sats[c];
                    float mybri = bris[r] + 40;
                    //colorMode(HSB);
                    uint h = (colorseed & 0xff);
                    Brush b = new SolidBrush(HSBToRGB(h, mysat / 255f, mybri / 255f));
                    g.FillRectangle(b, new RectangleF(x1, y1, xscale, yscale));
                   // fill(h, mysat, mybri);
                   // rect(x1, y1, xscale, yscale);
                }
            }
        }
        return rval;
    }

    private Color HSBToRGB(uint hue, float saturation, float brightness)
    {
        return Color.Black;

    }
}
class PixelSpaceshipFitter
{
    PixelSpaceship ship;
    int cols, rows;
    int col, row;
    int seed;
    miniMT rng;
    // cells store the box fit pattern, contents are byte encoded as:
    // (cell >> 16) & 0xff == work to do
    // (cell) & 0xff == size of allocated area
    // (this somewhat odd encoding scheme is a remnant of the
    //  fractal subdivision method used for the pixel robots t-shirt image)
    int[,] cells;
    // the types of work that may occur in a cell
    const int WORK_NONE = 0;
    const int WORK_DONE = 1;
    public PixelSpaceshipFitter(int c, int r,miniMT rng)
    {
        this.rng = rng;
        cols = c;
        rows = r;
        ship = new PixelSpaceship(rng);
        cells = new int[rows,cols];
    }
    // reset the pattern grid
    void wipe()
    {
        for (int r = 0; r < rows; r++)
            for (int c = 0; c < cols; c++)
                cells[r,c] = 0;
        col = row = 0;
    }
    // determines if cells already contain defined area(s)
    bool isOccupied(int col, int row, int wid, int hei)
    {
        for (int r = row; r < row + hei; r++)
        {
            for (int c = col; c < col + wid; c++)
            {
                if (cells[r,c] != 0)
                {
                    return true;
                }
            }
        }
        return false;
    }
    // marks cells as containing an area
    void doOccupy(int col, int row, int wid, int hei, int val)
    {
        for (int r = row; r < row + hei; r++)
        {
            for (int c = col; c < col + wid; c++)
            {
                cells[r,c] = val;
            }
        }
    }
    // define the pattern grid
    public void make(int s)
    {
        seed = s;
        Random random = new Random(s);
        wipe();
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                int cell = cells[r,c];
                if (cell != 0) continue;
                // figure out the size of area to occupy
                int sizer, limit;
                do
                {
                    limit = Math.Min(cols - c, rows - r);
                    limit = Math.Min(limit, 8);
                    sizer = random.Next(limit) + 1;
                } while (isOccupied(c, r, sizer, sizer));
                // flag all cells as occupied by width x height area
                doOccupy(c, r, sizer, sizer, sizer);
                // indicate work to perform in upper-left cell
                int work = WORK_DONE;
                cells[r,c] |= (work << 16);
            } // for c
        } // for r
    }
    // is cursor at 0,0
  public  bool at00()
    {
        return ((col == 0) && (row == 0));
    }
    // step the cursor forward
    void advance(int advcol)
    {
        col += advcol;
        if (col >= cols)
        {
            col = 0;
            row++;
            if (row >= rows)
            {
                row = 0;
            }
        }
    }
    // advance through the pattern and draw the next robot
    public Image drawone()
    {
        bool drawn = false;
        do
        {
            int cell = cells[row,col];
            int work = (cell >> 16) & 0xff;
            int sizer = (cell) & 0xff;
            if (work != WORK_NONE)
            {
                int y1 = 16 * row;
                int x1 = 16 * col;
                ship.setScales(sizer, sizer);
                ship.setMargins(2 * sizer, 2 * sizer);
                ship.setSeed(rng.generate());
                ship.generate();
                ship.recolor();
                Image i =ship.draw(x1, y1);
                drawn = true;
                return i;
            }
            advance(sizer);
            if (at00()) return null;
        } while (!drawn);
        return null;
    }
}

// a minimal version of the Mersenne Twister
class miniMT
{
    private long seed;
    private const int N = 624;
    private const int M = 397;
    private const uint MATRIX_A = 0x9908b0df;
    private const uint UPPER_MASK = 0x80000000;
    private const int LOWER_MASK = 0x7fffffff;
    private const uint TEMPERING_MASK_B = 0x9d2c5680;
    private const uint TEMPERING_MASK_C = 0xefc60000;
    private uint[] mt;
    private int mti;
    private uint[] mag01 = { 0x0, MATRIX_A };
    public miniMT()
    {
        this.setSeed(0);
    }
    public miniMT(long seed)
    {
        this.setSeed(seed);
    }
    public void setSeed(long _seed)
    {
        seed = _seed;
        mt = new uint[N];
        mt[0] = (uint)(seed & 0xfffffff);
        for (mti = 1; mti < N; mti++)
        {
            mt[mti] = (1812433253 * (mt[mti - 1] ^ (mt[mti - 1] >> 30)) + (uint)mti);
            mt[mti] &= 0xffffffff;
        }
    }
    public uint generate()
    {
        uint y;
        if (mti >= N)
        {
            int kk;
            for (kk = 0; kk < N - M; kk++)
            {
                y = (mt[kk] & UPPER_MASK) | (mt[kk + 1] & LOWER_MASK);
                mt[kk] = mt[kk + M] ^ (y >> 1) ^ mag01[y & 0x1];
            }
            for (; kk < N - 1; kk++)
            {
                y = (mt[kk] & UPPER_MASK) | (mt[kk + 1] & LOWER_MASK);
                mt[kk] = mt[kk + (M - N)] ^ (y >> 1) ^ mag01[y & 0x1];
            }
            y = (mt[N - 1] & UPPER_MASK) | (mt[0] & LOWER_MASK);
            mt[N - 1] = mt[M - 1] ^ (y >> 1) ^ mag01[y & 0x1];
            mti = 0;
        }
        y = mt[mti++];
        y ^= y >> 11;
        y ^= (y << 7) & TEMPERING_MASK_B;
        y ^= (y << 15) & TEMPERING_MASK_C;
        y ^= (y >> 18);
        return y;
    }
}