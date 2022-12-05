using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace final_project_mini
{

    public class background
    {
        public int X, Y;
        public Bitmap img;
    }
    public class bullets
    {
        public int X, Y, W, H;
        public Bitmap imgB;
    }
    public class CShipActor
    {
        public int X, Y;
        public Bitmap img;
    }
    public class CchikenActor
    {
        public int X, Y;
        public Bitmap img;
        public int dir=0;


    }
    public partial class Form1 : Form
    {
        Bitmap off;
        int ffire = 1 ;
        int hit = 0;
        int ctt=0;
        int score = 0;
        List<CchikenActor> LEnms = new List<CchikenActor>();
        List<background> LBackground = new List<background>();
        List<CShipActor> LShip = new List<CShipActor>();
        List<bullets> LBullets = new List<bullets>();

        Timer tt = new Timer();
        public Form1()
        {
            this.WindowState = FormWindowState.Maximized;
            this.Load += Form1_Load;
            this.Paint += Form1_Paint1;
            this.KeyDown += Form1_KeyDown1; ;
            tt.Tick += T_Tick;
            tt.Start();
            this.KeyUp += Form1_KeyUp;
        }
     
       
    
       
        private void T_Tick(object sender, EventArgs e)
        {
            if (ffire == 0)
            {
                MoveBullet();
            }
            
        
            movechiken();
            
            drawdubb(this.CreateGraphics());
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                for (int k = 10; k > 0; k -= 1)
                {
                    LShip[0].X = LShip[0].X + k;
                    drawdubb(this.CreateGraphics());
                }

            }
            if (e.KeyCode == Keys.Left)
            {
                for (int k = 10; k > 0; k -= 1)
                {
                    LShip[0].X = LShip[0].X - k;
                    drawdubb(this.CreateGraphics());
                }
            }
            drawdubb(this.CreateGraphics());
        }
        private void Form1_KeyDown1(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Right)
            {

               if(LShip[0].X != 1400)
                {
                    LShip[0].X += 10;
                }

            }
         
            if (e.KeyCode == Keys.Left)
            {

                if (LShip[0].X != 10)
                {
                    LShip[0].X -= 10;
                }

            }
            if (e.KeyCode == Keys.Space)
            {

                ffire = 0;
                CreateBullet();

            }
          
        }
        private void Form1_Paint1(object sender, PaintEventArgs e)
        {
                drawdubb(e.Graphics);   
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            off = new Bitmap(ClientSize.Width, ClientSize.Height);

            CreateShip();
            Createbackground();
            Createchikens();
        
        }
      
        void CreateShip()
        {
            CShipActor pnn = new CShipActor();
            pnn.X =700;
            pnn.Y = 710;
            pnn.img = new Bitmap("ship.jpg");
            pnn.img.MakeTransparent(pnn.img.GetPixel(0, 0));
              LShip.Add(pnn);

        }
        void Createchikens()
        {
            int XX=50;  
            for (int i = 0; i <10; i++)
            {
                CchikenActor pnn = new CchikenActor();
                pnn.X = XX;
                pnn.Y = 100;
                pnn.img = new Bitmap("Chicken.jpg");   
                LEnms.Add(pnn);
                XX+=100;
            }
            XX = 50;
            for (int i = 0; i < 10; i++)
            {
                CchikenActor pnn = new CchikenActor();
                pnn.X = XX;
                pnn.Y = 200;
                pnn.img = new Bitmap("Chicken.jpg");
                LEnms.Add(pnn);
                XX += 100;
            }
            XX = 50;
            for (int i = 0; i < 10; i++)
            {
                CchikenActor pnn = new CchikenActor();
                pnn.X = XX;
                pnn.Y = 300;
                pnn.img = new Bitmap("Chicken.jpg");
                LEnms.Add(pnn);
                XX += 100;
            }
        }
        void Createbackground()
        {
            background pnn = new background();
            pnn.X = 0;
            pnn.Y = 0;
            pnn.img = new Bitmap("background.jpg");
            LBackground.Add(pnn);
        }
        void CreateBullet()
        {
            bullets pnnB = new bullets();
            pnnB.imgB = new Bitmap("FireBullet.png");
            pnnB.X = LShip[0].X + 55;
            pnnB.Y = LShip[0].Y - pnnB.imgB.Height;
            pnnB.W = 16;
            pnnB.H = 40;
            LBullets.Add(pnnB);

        }
        void movechiken()
        {
            for (int i = 0; i < LEnms.Count; i++)
            {
                if (LEnms[i].X <= 0)
                {
                    LEnms[i].dir = 1;
                }
               if (LEnms[i].dir == 1)
                {
                    LEnms[i].X += 15;
                }
                if (LEnms[i].X >= 1400)
                {
                    LEnms[i].dir = 0;
                }
                if (LEnms[i].dir == 0)
                {
                    LEnms[i].X -= 15;
                }
            }
        }
        void MoveBullet()
        {

            for (int i = 0; i < LBullets.Count; i++)
            {
                LBullets[i].Y -= 20;
            }

            for (int i = 0; i < LBullets.Count; i++)
            {
                for (int j = 0; j < LEnms.Count; j++)
                {
                    if (LBullets[i].X > LEnms[j].X &&
                        LBullets[i].X < LEnms[j].X + LEnms[j].img.Width &&
                        LBullets[i].Y < LEnms[j].Y + LEnms[j].img.Height)
                    {
                        LEnms.RemoveAt(j);
                        score++;
                        MessageBox.Show('score');
                        hit = 1;
                    }

                }
            }

        }
        void drawscene(Graphics g)
        {
           g.Clear(Color.DarkBlue);

            for (int i = 0; i < LBackground.Count; i++)
            {
                g.DrawImage(LBackground[i].img, LBackground[i].X, LBackground[i].Y);
            }
            for (int i = 0; i < LShip.Count; i++)
            {
                g.DrawImage(LShip[i].img, LShip[0].X, LShip[0].Y);
            }
            for (int i = 0; i < LEnms.Count; i++)
            {
                g.DrawImage(LEnms[i].img, LEnms[i].X, LEnms[i].Y);
            }
            if (ffire == 0)
            {
                for(int i=0; i<LBullets.Count; i++)
                {
                    g.DrawImage(LBullets[i].imgB, LBullets[i].X, LBullets[i].Y, LBullets[i].W, LBullets[i].H);
                }
                for (int i = 0; i < LBullets.Count; i++)
                {
                    if (hit == 1)
                    {
                       
                        hit = 0;
                        if (LBullets.Count >= 1)
                        {
                            LBullets.RemoveAt(i);
                             ctt++;
                            if (ctt == 30)
                            {
                                MessageBox.Show("you win");
                            }

                        }

                    }
                    
                }
                
            }
           
        }
        void drawdubb(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            drawscene(g2);
            g.DrawImage(off, 0, 0);
        }
    }
}
