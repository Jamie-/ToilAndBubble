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
        //PixelSpaceshipFitter fitter;
        PixelSpaceship ship;
        miniMT rng; // want a slightly more robust rng for 32 significant bits
        
        Dictionary<Color, int> coloroptions;
        Color CurrentColor;
        bool mouseDown = false;
        PictureBox[,] canvasgrid;
        display dis;
        public Form1()
        {   tests();
            InitializeComponent();
            coloroptions = new Dictionary<Color, int>();
            coloroptions.Add(Color.White, PixelSpaceship.EMPTY);
            coloroptions.Add(Color.LightBlue, PixelSpaceship.AVOID);
            coloroptions.Add(Color.Turquoise, PixelSpaceship.COKPT);
            coloroptions.Add(Color.Black, PixelSpaceship.SOLID);
            CurrentColor = Color.LightBlue;
            dis = new display();
            rng = new miniMT();
            ship = new PixelSpaceship(rng);
            rdAVOID.CheckedChanged += RdAVOID_CheckedChanged;
            rdCOKPT.CheckedChanged += RdAVOID_CheckedChanged;
            rdEMPTY.CheckedChanged += RdAVOID_CheckedChanged;
            rdSOLID.CheckedChanged += RdAVOID_CheckedChanged;
            canvasgrid = new PictureBox[panel1.Width / 20, panel1.Width / 20];
            this.KeyDown += (sender, e) => { if (e.KeyCode == Keys.W) mouseDown = true; };
            this.KeyUp += (sender, e) => { if (e.KeyCode == Keys.W) mouseDown = false; };
            for (int i = 0; i < canvasgrid.GetLength(0); i++)
                for (int j = 0; j < canvasgrid.GetLength(1); j++)
                {
                    canvasgrid[i, j] = new PictureBox();
                    canvasgrid[i, j].Size = new Size(19, 19);
                    canvasgrid[i, j].Location = new Point(20 * i, 20 * j);
                    canvasgrid[i, j].BackColor = Color.White;
                    canvasgrid[i, j].MouseEnter += (sender, e) => { pictureBox_Click(sender, e, i, j); };
                    panel1.Controls.Add(canvasgrid[i, j]);
                }
            dis.Show();
            // fitter = new PixelSpaceshipFitter(480 / 16, 480 / 16, rng);
            // next();
            ship.setScales(7, 7);
            ship.setMargins(2 * 7, 2 * 7);
            ship.setSeed(rng.generate());
            
        }
        public void tests()
        {
            bool passed = true;
            for (int i = 0; i < 256; i++)
                for (int j = 0; j < 256; j++)
                    for (int k = 0; k < 256; k++)
                    {
                        Color testc = Color.FromArgb(i, j, k);
                        HSVColor h = new HSVColor(testc);
                        passed &= testc.Equals(h.RgbColor);
                    }
            Console.WriteLine("passed: " + passed);


        }


        private void RdAVOID_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rd = sender as RadioButton;
            if (rd != null)
            {
                if (rd.Checked)
                {
                    switch (rd.Name)
                    {
                        case "rdEMPTY":
                            CurrentColor = Color.White;
                            break;
                        case "rdAVOID":
                            CurrentColor = Color.LightBlue;
                            break;
                        case "rdCOKPT":
                            CurrentColor = Color.Turquoise;
                            break;
                        case "rdSOLID":
                            CurrentColor = Color.Black;
                            break;
                    }
                }
            }
        }

        private void pictureBox_Click(object sender, EventArgs e, int i, int j)
        {
            PictureBox current = sender as PictureBox;
            if (current != null && mouseDown)
            {
                current.BackColor = CurrentColor;

            }
        }



        private void button1_Click(object sender, EventArgs e)
        {

            Random R = new Random();
            int[,] seed = new int[canvasgrid.GetLength(0), canvasgrid.GetLength(1)];
            for (int index = 0; index < canvasgrid.GetLength(0); index++)
                for (int j = 0; j < canvasgrid.GetLength(1); j++)
                {
                    seed[index, j] = coloroptions[canvasgrid[index, j].BackColor];
                }
            ship.GridSeed = seed;
            ship.recolor();
            Image i = ship.draw(32, 32);
            dis.Size = new Size(i.Width + 50, i.Height + 50);
            dis.BackgroundImage = i;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            ship.xflip = !ship.xflip;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            ship.yflip = !ship.yflip;
        }
    }
}
