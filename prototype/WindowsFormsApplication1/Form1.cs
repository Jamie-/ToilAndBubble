using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        const int satuationVal = 60;
        const int LuminosityVal = 60;
        Random r;
        int tick;
        List<ingred> beingused;
        ingred[] items;

        public Form1()
        {
            InitializeComponent();
            lblBinLeft.Hide();
            lblBinRight.Hide();
            r = new Random();
            beingused = new List<ingred>();
            items = new ingred[8];
            items[0] = new ingred(PctColor, LblPosneg, PrgTimer);
            items[1] = new ingred(pictureBox1, label1, progressBar1);
            items[2] = new ingred(pictureBox2, label2, progressBar2);
            items[3] = new ingred(pictureBox3, label3, progressBar3);
            items[4] = new ingred(pictureBox4, label4, progressBar4);
            items[5] = new ingred(pictureBox5, label5, progressBar5);
            items[6] = new ingred(pictureBox6, label6, progressBar6);
            items[7] = new ingred(pictureBox7, label7, progressBar7);
            tmr.Tick += Tmr_Tick;
            int p1 = r.Next(255);
            RightTarget.BackColor = HsvToRgb(p1, satuationVal, LuminosityVal);// Color.FromArgb(r.Next(255), r.Next(255), r.Next(255));
            LeftTarget.BackColor = HsvToRgb(255-p1, satuationVal, LuminosityVal);// Color.FromArgb(r.Next(255), r.Next(255), r.Next(255));
            foreach (ingred i in items)
            {
                CreateIng(i);
            }
            tmr.Enabled = true;

            ShowResults();
        }




        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
                lblBinLeft.Show();
            else
                lblBinLeft.Hide();
            if (e.Shift)
                lblBinRight.Show();
            else
                lblBinRight.Hide();
            
            ShowResults();
            if (tmr.Enabled)
                switch (e.KeyCode)
                {
                  
                    case Keys.D1:
                        if (!e.Control)
                            useing(items[0]);
                        else
                            useing(items[0], false);
                        break;
                    case Keys.D2:
                        if (!e.Control)
                            useing(items[1]);
                        else
                            useing(items[1], false);
                        break;
                    case Keys.D3:
                        if (!e.Control)
                            useing(items[2]);
                        else
                            useing(items[2], false);
                        break;
                    case Keys.D4:                        
                        if (!e.Control)
                            useing(items[3]);
                        else
                            useing(items[3], false);
                        break;
                    case Keys.D7:
                        if (!e.Shift)
                            useing(items[4]);
                        else
                            useing(items[4], false);
                        break;
                    case Keys.D8:
                        if (!e.Shift)
                            useing(items[5]);
                        else
                            useing(items[5], false);
                        break;
                    case Keys.D9:
                        if (!e.Shift)
                            useing(items[6]);
                        else
                            useing(items[6], false);
                        break;
                    case Keys.D0:
                        if (!e.Shift)
                            useing(items[7]);
                        else
                            useing(items[7], false);
                        break;

                }
        }
        void useing(ingred ing)
        {
            useing(ing, true);
        }
        void useing(ingred ing, bool use)
        {
            if (!beingused.Contains(ing))
            {
                if (use)
                    Cauldren.BackColor = Combine(Cauldren.BackColor, ing.p.BackColor, ing.pos.Text);
                ing.prg.Visible = true;
                beingused.Add(ing);
            }
        }
/// <summary>
/// 
/// </summary>
/// <param name="backColor1"></param>
/// <param name="backColor2"></param>
/// <param name="pos"></param>
/// <returns></returns>
        private Color Combine(Color backColor1, Color backColor2, String pos)
        {
            Color rval = backColor1;
            if (pos == "+")
            {
                rval = Color.FromArgb(Math.Min(255, rval.R + backColor2.R), Math.Min(255, rval.G + backColor2.G), Math.Min(255, rval.B + backColor2.B));
            }
            else
            if (pos == "-")
            {

                rval = Color.FromArgb( Math.Max(0,rval.R - backColor2.R),Math.Max(0,rval.G - backColor2.G), Math.Max(0, rval.B - backColor2.B));
              //  rval=Color
            }
            return rval;
        }

        private void Tmr_Tick(object sender, EventArgs e)
        {
            tick++;
            
                for (int i = beingused.Count - 1; i >= 0; i--)
                {
                    if (beingused[i].prg.Value <= 0)
                    {
                        CreateIng(beingused[i]);
                        beingused.RemoveAt(i);
                    }
                    else
                        beingused[i].prg.Value--;
                }

           
            label8.Text = "time: " + (int)(tick / 100);
            if (tick >= 6000)
            {
                tmr.Enabled = false;
                MessageBox.Show("game over");
                lblwinning.Text = "Won";
                ShowResults();
            }
        }

        private void ShowResults()
        {
            int[] p1score = GetScore(this.RightTarget.BackColor, Cauldren.BackColor);
            int[] p2score = GetScore(this.LeftTarget.BackColor, Cauldren.BackColor);
            lblPlayer1score.Text = String.Format("red: {0}\ngreen: {1}\n blue: {2}\n total: {3}", p1score[0], p1score[1], p1score[2], p1score[0] + p1score[2] + p1score[1]);
         lblPlayer2Score.Text= String.Format("red: {0}\ngreen: {1}\n blue: {2}\n total: {3}", p2score[0], p2score[1], p2score[2], p2score[0] + p2score[2] + p2score[1]);
            int p2total = p2score[0] + p2score[2] + p2score[1];
            int p1total = p1score[0] + p1score[2] + p1score[1];
            if (p2total!= p1total)
            {
                if (p2total < p1total)
                {
                    ScoreIndicater.Text = "<<";
                }
                else
                {
                    ScoreIndicater.Text = ">>";
                }
               }
            else
            {
                ScoreIndicater.Text = "||";
            }
            //lblPlayer1score.Text;
        }
        int[] GetScore(Color Target,Color Cauldren)
        {
            int[] scores = new int[3];
            scores[0] = (int)(100 * (1- ((double)Math.Abs(Target.R - Cauldren.R) / 255)));
            scores[1] = (int)(100 * (1-((double)Math.Abs(Target.G - Cauldren.G) /255)));
            scores[2] =(int)( 100 * (1-((double)Math.Abs(Target.B - Cauldren.B) / 255)));
            return scores;
        }

        private void CreateIng(ingred ingred)
        {
            ingred.p.BackColor = HsvToRgb(0, satuationVal, LuminosityVal);
            ingred.pos.Text = r.Next(2) == 0 ? "-" : "+";
            ingred.prg.Maximum = (r.Next(5) + 1)*100;
            ingred.prg.Value = ingred.prg.Maximum;
            ingred.prg.Visible = false;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control)
                lblBinLeft.Show();
            else
                lblBinLeft.Hide();
            if (e.Shift)
                lblBinRight.Show();
            else
                lblBinRight.Hide();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            MessageBox.Show("GetReady");
        }














        Color HsvToRgb(double h, double S, double V)
        {
            int r;
             int g;
            int b;
            double H = h;
            while (H < 0) { H += 360; };
            while (H >= 360) { H -= 360; };
            double R, G, B;
            if (V <= 0)
            { R = G = B = 0; }
            else if (S <= 0)
            {
                R = G = B = V;
            }
            else
            {
                double hf = H / 60.0;
                int i = (int)Math.Floor(hf);
                double f = hf - i;
                double pv = V * (1 - S);
                double qv = V * (1 - S * f);
                double tv = V * (1 - S * (1 - f));
                switch (i)
                {

                    // Red is the dominant color

                    case 0:
                        R = V;
                        G = tv;
                        B = pv;
                        break;

                    // Green is the dominant color

                    case 1:
                        R = qv;
                        G = V;
                        B = pv;
                        break;
                    case 2:
                        R = pv;
                        G = V;
                        B = tv;
                        break;

                    // Blue is the dominant color

                    case 3:
                        R = pv;
                        G = qv;
                        B = V;
                        break;
                    case 4:
                        R = tv;
                        G = pv;
                        B = V;
                        break;

                    // Red is the dominant color

                    case 5:
                        R = V;
                        G = pv;
                        B = qv;
                        break;

                    // Just in case we overshoot on our math by a little, we put these here. Since its a switch it won't slow us down at all to put these here.

                    case 6:
                        R = V;
                        G = tv;
                        B = pv;
                        break;
                    case -1:
                        R = V;
                        G = pv;
                        B = qv;
                        break;

                    // The color is not defined, we should throw an error.

                    default:
                        //LFATAL("i Value error in Pixel conversion, Value is %d", i);
                        R = G = B = V; // Just pretend its black/white
                        break;
                }
            }
            r = Clamp((int)(R * 255.0));
            g = Clamp((int)(G * 255.0));
            b = Clamp((int)(B * 255.0));
            return Color.FromArgb(r, g, b);
        }

        /// <summary>
        /// Clamp a value to 0-255
        /// </summary>
        int Clamp(int i)
        {
            if (i < 0) return 0;
            if (i > 255) return 255;
            return i;
        }







    }
    class ingred
    {
        public PictureBox p;
        public Label pos;
        public ProgressBar prg;
        public ingred(PictureBox p, Label pos, ProgressBar prg)
        {
            this.p = p;
            this.pos = pos;
            this.prg = prg;
        }
    }
}
