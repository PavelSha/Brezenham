using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Brezenham
{
    public partial class Form1 : Form
    {
        System.Drawing.Graphics GS;
        Bitmap pole,cube,square,cube2;
        bool poryadok_tochki; //указывает нажимается первый раз кнопка мыши или нет
        //false - в режиме получения первой точки
        //true в режиме получения второй точки
        int x1, y1, x2, y2; //координаты первой и второй точки
        const int xmin = 10;
        const int xmax = xmin + 29;
        const int ymin = 10;
        const int ymax = ymin + 29;
        int[] kray;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            GS = this.panel1.CreateGraphics();
            pole = new Bitmap("pole.bmp");
            cube = new Bitmap("cube.bmp");
            cube2 = new Bitmap("cube2.bmp");
            square = new Bitmap("square.bmp");
            GS.DrawImage(pole, 0, 0);
            GS.DrawImage(square, 100, 100);
            poryadok_tochki = false;
            //tkod1 = new int[4]; //код первой точки
            //tkod2 = new int[4]; //код второй точки
            //tkod1[1] = 1;
            kray = new int[2];
        }
        void cutline(int p_1_x__, int p1y, int p2x, int p2y, Graphics GS)
        {
            int osnx1 = p_1_x__, osny1 = p1y, osnx2 = p2x, osny2 = p2y; 
            int summ1, summ2; //для подсчета количетсва битов в коде tkod
            int[] tkod1 = new int[] { 0, 0, 0, 0 };
            int[] tkod2 = new int[] {0,0,0,0};
            //int i;
            int x = 0, y = 0;
            int f = 0;
            summ1 = 0; summ2 = 0;
            //!!!!!!!!!!!!!!!!!!!!!!
            if (p_1_x__ + 1 == xmin) { p_1_x__++; }
            if (p_1_x__ - 1 == xmax) { p_1_x__--; }
            if (p2x + 1 == xmin) { p2x++; }
            if (p2x - 1 == xmax) { p2x--; }
            if (p1y + 1 == ymin) { p1y++; }
            if (p1y - 1 == ymax) { p1y--; }
            if (p2y + 1 == ymin) { p2y++; }
            if (p2y - 1 == ymax) { p2y--; }
            //tkod1[3] = 1; tkod1[2] = 1; tkod1[1] = 1; tkod1[0] = 1;
            //create_tkod_1_2(tkod1, xmin, ymin, xmax, ymax, p1x, p1y);
            //create_tkod_1_2(tkod2, xmin, ymin, xmax, ymax, p2x, p2y);
            if (p2x < xmin) { tkod2[3] = 1; } else { tkod2[3] = 0; };
            if (p2x > xmax) { tkod2[2] = 1; } else { tkod2[2] = 0; };
            if (p2y < ymin) { tkod2[1] = 1; } else { tkod2[1] = 0; };
            if (p2y > ymax) { tkod2[0] = 1; } else { tkod2[0] = 0; };
            if (p_1_x__ < xmin)  tkod1[3] = 1;  else { tkod1[3] = 0; };
            if (p_1_x__ > xmax)  tkod1[2] = 1; else { tkod1[2] = 0; };
            if (p1y < ymin) { tkod1[1] = 1; } else { tkod1[1] = 0; };
            if (p1y > ymax) { tkod1[0] = 1; } else { tkod1[0] = 0; };
            //!!!!!!!!!!!!!!!!!!!!!!!!
            summ1 = getsumm(tkod1, summ1); summ2 = getsumm(tkod2, summ2);
            //for (i = 0; i <= 3; i++)
            //{
            //    summ1 = summ1 + tkod1[i]; summ2 = summ2 + tkod2[i];
            //}
            //!!!!!!!!!!!!!!!!!!!!!!!!!!
            if ((summ1 == 0) && (summ2 == 0)) { this.Brez(p_1_x__, p1y, p2x, p2y, GS, 2); return; }
            while ((summ1 != 0) || (summ2 != 0))
            {
                if ((tkod1[0] + tkod2[0] == 2) || (tkod1[1] + tkod2[1] == 2) || (tkod1[2] + tkod2[2] == 2) || (tkod1[3] + tkod2[3] == 2))
                {
                    return;
                }
                else
                {
                    if (summ1 != 0)
                    {
                        if ((tkod1[3] == 1))// && (f==0))
                        {
                            y = BrezFind(osnx1, osny1,osnx2 , osny2, GS, 3);
                            if (y == -1) { ;} else { p1y = y; p_1_x__ = xmin; }
                            //p1y = BrezFind(p_1_x__, p1y, p2x, p2y, GS, 3);
                            
                            //x = xmin; 
                            f = 1;
                        }
                        if ((tkod1[2] == 1))// && (f==0))
                        {
                            y = BrezFind(osnx1, osny1, osnx2, osny2, GS, 2);
                            if (y == -1) { ;} else { p1y = y; p_1_x__ = xmax; }
                            //p1y = BrezFind(p_1_x__, p1y, p2x, p2y, GS, 2);
                            
                            //x = xmax; 
                            f = 1;
                        }
                        if ((tkod1[1] == 1))// && (f==0))
                        {
                            x = BrezFind(osnx1, osny1, osnx2, osny2, GS, 1);
                            if (x == -1) { ;} else { p_1_x__ = x; p1y = ymin; }
                            //p_1_x__ = BrezFind(p_1_x__, p1y, p2x, p2y, GS, 1);

                            //y = ymin; 
                            f = 1;
                        }
                        if ((tkod1[0] == 1))// && (f==0))
                        {
                            x = BrezFind(osnx1, osny1, osnx2, osny2, GS, 0);
                            if (x == -1) { ;} else { p_1_x__ = x; p1y = ymax; }
                            //p_1_x__ = BrezFind(p_1_x__, p1y, p2x, p2y, GS, 0);
                            
                            //y = ymax;
                        }
                        create_tkod_1_2(tkod1, xmin, ymin, xmax, ymax, p_1_x__, p1y);
                        summ1 = getsumm(tkod1, summ1);
                        f = 0;
                        /*
                        if (f == 0)
                        {
                            if (p2x >= p_1_x__) p_1_x__ = p_1_x__ + ((y - p1y) * (p2x - p_1_x__)) / (p2y - p1y);
                            if (p2x < p_1_x__) p_1_x__ = p_1_x__ + ((y - p1y) * (p_1_x__ - p2x)) / (p2y - p1y);
                            p1y = y;
                            create_tkod_1_2(tkod1, xmin, ymin, xmax, ymax, p_1_x__, p1y);
                            summ1 = getsumm(tkod1, summ1);
                            //if (p1x < xmin) { tkod1[3] = 1; } else { tkod1[3] = 0; };
                            //if (p1x > xmax) { tkod1[2] = 1; } else { tkod1[2] = 0; };
                            //if (p1y < ymin) { tkod1[1] = 1; } else { tkod1[1] = 0; };
                            //if (p1y > ymax) { tkod1[0] = 1; } else { tkod1[0] = 0; };
                        }
                        if (f == 1)
                        {
                            if (p2y >= p1y) p1y = p1y + ((x - p_1_x__) * (p2y - p1y)) / (p2x - p_1_x__);
                            if (p1y >= p2y) p1y = p1y + ((x - p_1_x__) * (p1y - p2y)) / (p2x - p_1_x__);
                            p_1_x__ = x;
                            create_tkod_1_2(tkod1, xmin, ymin, xmax, ymax, p_1_x__, p1y);
                            summ1 = getsumm(tkod1, summ1);
                            //if (p1x < xmin) { tkod1[3] = 1; } else { tkod1[3] = 0; };
                            //if (p1x > xmax) { tkod1[2] = 1; } else { tkod1[2] = 0; };
                            //if (p1y < ymin) { tkod1[1] = 1; } else { tkod1[1] = 0; };
                            //if (p1y > ymax) { tkod1[0] = 1; } else { tkod1[0] = 0; };
                            f = 0;
                        }
                        */
                    }
                    else
                    {
                        //swap(p_1_x__, p1y, p2x, p2y, tkod1, tkod2);
                        int i;
                        int t1;
                        summ1 = 0; summ2 = 0;
                        t1 = p_1_x__; p_1_x__ = p2x; p2x = t1;
                        t1 = p1y; p1y = p2y; p2y = t1;
                        for (i = 0; i <= 3; i++)
                        {
                            t1 = tkod1[i]; tkod1[i] = tkod2[i]; tkod2[i] = t1;
                        }
                        for (i = 0; i <= 3; i++)
                        {
                            summ1 = summ1 + tkod1[i]; summ2 = summ2 + tkod2[i];
                        }
                    }
                }
            }
            this.Brez(p_1_x__, p1y, p2x, p2y, GS, 2);
        }
        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            string st1, st2;
            int ost; //остаток при делении
            if (poryadok_tochki==false)
            {
                ost = e.X % 10; x1 = (e.X - ost) / 10;
                ost = e.Y % 10; y1 = (e.Y - ost) / 10;
                poryadok_tochki=true;
                return;
            }; 
            if (poryadok_tochki==true)
            {
                ost = e.X % 10; x2 = (e.X - ost) / 10;
                ost = e.Y % 10; y2 = (e.Y - ost) / 10;
                Brez(x1, y1, x2, y2,GS,1);
                //kray = BrezFind(x1, y1, x2, y2, GS, 1);
                st1 = "" + kray[0]; st2 = "" + kray[1];
                label2.Text = st1; label3.Text = st2;
                cutline(x1, y1,x2,y2,GS);// kray[0], kray[1], GS);
                    //cutline(x2, y2, kray[0], kray[1], GS);
                y1 = 0; x1 = 0; x2 = 0; y2 = 0;
                poryadok_tochki = false;
                return;
            }
        }
        /*
         *                 //cutline(2,x1, y1, x2, y2, GS);
                ///_)-----
                ///int[] tkod1 = new int[] { 0, 0, 0, 1 };
                int[] tkod2 = new int[] { 0, 0, 0, 1 };
                int i;
                int x = 0, y = 0;
                int f = 0; summ1 = 0; summ2 = 0;
                //!!!!!!!!!!!!!!!!!!!!!!
                //tkod1[3] = 1; tkod1[2] = 1; tkod1[1] = 1; tkod1[0] = 1;
                //create_tkod_1_2(tkod1, xmin, ymin, xmax, ymax, p1x, p1y);
                //create_tkod_1_2(tkod2, xmin, ymin, xmax, ymax, p2x, p2y);
                if (x2 < xmin) { tkod2[3] = 1; } else { tkod2[3] = 0; };
                if (x2 > xmax) { tkod2[2] = 1; } else { tkod2[2] = 0; };
                if (y2 < ymin) { tkod2[1] = 1; } else { tkod2[1] = 0; };
                if (y2 > ymax) { tkod2[0] = 1; } else { tkod2[0] = 0; };
                if (p_1_x__ < xmin) tkod1[3] = 1; else { tkod1[3] = 0; };
                if (p_1_x__ > xmax) tkod1[2] = 1; else { tkod1[2] = 0; };
                if (p1y < ymin) { tkod1[1] = 1; } else { tkod1[1] = 0; };
                if (p1y > ymax) { tkod1[0] = 1; } else { tkod1[0] = 0; };
                //!!!!!!!!!!!!!!!!!!!!!!!!
                //summ1 = getsumm(tkod1, summ1); summ2 = getsumm(tkod2, summ2);
                for (i = 0; i <= 3; i++)
                {
                    summ1 = summ1 + tkod1[i]; summ2 = summ2 + tkod2[i];
                }
                //!!!!!!!!!!!!!!!!!!!!!!!!!!
                if ((summ1 == 0) && (summ2 == 0)) { this.Brez(p_1_x__, p1y, p2x, p2y, GS, 2); return; }
                while ((summ1 != 0) || (summ2 != 0))
                {
                    if ((tkod1[0] + tkod2[0] == 2) || (tkod1[1] + tkod2[1] == 2) || (tkod1[2] + tkod2[2] == 2) || (tkod1[3] + tkod2[3] == 2))
                    {
                        return;
                    }
                    else
                    {
                        if (summ1 != 0)
                        {
                            if (tkod1[3] == 1)
                            {
                                x = xmin; f = 1;
                            }
                            if (tkod1[2] == 1)
                            {
                                x = xmax; f = 1;
                            }
                            if (tkod1[1] == 1) y = ymin;
                            if (tkod1[0] == 1) y = ymax;
                            if (f == 0)
                            {
                                if (p2x >= p_1_x__) p_1_x__ = p_1_x__ + ((y - p1y) * (p2x - p_1_x__)) / (p2y - p1y);
                                if (p2x < p_1_x__) p_1_x__ = p_1_x__ + ((y - p1y) * (p_1_x__ - p2x)) / (p2y - p1y);
                                p1y = y;
                                create_tkod_1_2(tkod1, xmin, ymin, xmax, ymax, p_1_x__, p1y);
                                //if (p1x < xmin) { tkod1[3] = 1; } else { tkod1[3] = 0; };
                                //if (p1x > xmax) { tkod1[2] = 1; } else { tkod1[2] = 0; };
                                //if (p1y < ymin) { tkod1[1] = 1; } else { tkod1[1] = 0; };
                                //if (p1y > ymax) { tkod1[0] = 1; } else { tkod1[0] = 0; };
                            }
                            if (f == 1)
                            {
                                if (p2y >= p1y) p1y = p1y + ((x - p_1_x__) * (p2y - p1y)) / (p2x - p_1_x__);
                                if (p1y >= p2y) p1y = p1y + ((x - p_1_x__) * (p1y - p2y)) / (p2x - p_1_x__);
                                p_1_x__ = x;
                                create_tkod_1_2(tkod1, xmin, ymin, xmax, ymax, p_1_x__, p1y);
                                //if (p1x < xmin) { tkod1[3] = 1; } else { tkod1[3] = 0; };
                                //if (p1x > xmax) { tkod1[2] = 1; } else { tkod1[2] = 0; };
                                //if (p1y < ymin) { tkod1[1] = 1; } else { tkod1[1] = 0; };
                                //if (p1y > ymax) { tkod1[0] = 1; } else { tkod1[0] = 0; };
                                f = 0;
                            }
                        }
                        else
                        {
                            swap(p_1_x__, p1y, p2x, p2y, tkod1, tkod2);
                        }
                    }
                }
                this.Brez(p_1_x__, p1y, p2x, p2y, GS, 2);
                //////________________________
                y1 = 0; x1 = 0; x2 = 0; y2 = 0;
                poryadok_tochki = false;
                return;
            }
         */
        public int sing(int f)
        {
            if (f > 0) return (1);
            else
                if (f < 0) return (-1); else return (0);

        }
        
        public void create_tkod_1_2(int[] tkod,int xmin,int ymin,int xmax,int ymax,int x1,int y1)
        {
          if (x1<xmin) {tkod[3]=1;} else {tkod[3]=0;};
          if (x1>xmax) {tkod[2]=1;} else {tkod[2]=0;};
          if (y1<ymin) {tkod[1]=1;} else {tkod[1]=0;};
          if (y1>ymax) {tkod[0]=1;} else {tkod[0]=0;};
        }
        void swap(int p1x,int p1y,int p2x,int p2y,int[] tkod1,int[] tkod2)
        {
           int i;
           int t1;
           t1=p1x;p1x=p2x;p2x=t1;
           t1=p1y;p1y=p2y;p2y=t1;
           for (i=0;i<=3;i++)
           {
              t1=tkod1[i];tkod1[i]=tkod2[i];tkod2[i]=t1;
           }
        }
        int getsumm (int[] tkod,int summ)
        {
            int i;
            summ = 0;
            for (i=0;i<=3;i++)
            {
               summ = summ + tkod[i];
            }
            return (summ);
        }
        
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            GS.DrawImage(pole, 0, 0);
            GS.DrawImage(square, 100, 100);
        }
        public void Brez(int x1,int y1,int x2, int y2, Graphics GS,int type)
        {
            //---сам алгоритм---
            int i, temp, obmen;
            int x, y, dx, dy, s1, s2;
            float ee;
            //--------------------
            x = x1;
            y = y1;
            dx = Math.Abs(x2 - x1);
            dy = Math.Abs(y2 - y1);
            s1 = sing(x2 - x1);
            s2 = sing(y2 - y1);
            //--------------------
            if (dy > dx) { temp = dx; dx = dy; dy = temp; obmen = 1; } else obmen = 0;
            //--------------------
            ee = 2 * dy - dx;
            //--------------------
            for (i = 1; i <= dx; i++)
            {
                if (type == 1) GS.DrawImage(cube, x * 10, y * 10);
                if (type == 2) GS.DrawImage(cube2, x * 10, y * 10);
                while (ee >= 0)
                {
                    if (obmen == 1) x = x + s1; else y = y + s2;
                    ee = ee - 2 * dx;
                }
                if (obmen == 1) y = y + s2; else x = x + s1;
                ee = ee + 2 * dy;
            }
            if (type == 1) GS.DrawImage(cube, x2 * 10, y2 * 10);
            if (type == 2) GS.DrawImage(cube2, x2 * 10, y2 * 10);
            //---завершение алгоритма---
        }
        public int BrezFind(int x1, int y1, int x2, int y2, Graphics GS, int type)
        {
            //---сам алгоритм---
            //int[] kray = new int[2];
            int flag = 0;
            int i, temp, obmen;
            int x, y, dx, dy, s1, s2;
            float ee;
            //--------------------
            x = x1;
            y = y1;
            dx = Math.Abs(x2 - x1);
            dy = Math.Abs(y2 - y1);
            s1 = sing(x2 - x1);
            s2 = sing(y2 - y1);
            //--------------------
            if (dy > dx) { temp = dx; dx = dy; dy = temp; obmen = 1; } else obmen = 0;
            //--------------------
            ee = 2 * dy - dx;
            //--------------------
            for (i = 1; i <= dx; i++)
            {
                if (type == 3) 
                {
                    if ((x == xmin) && (y <= ymax) && (y >= ymin))
                    {
                        flag = y;
                        //return (y); 
                    }
                    else
                    {
                        if (flag != 0) return (flag);
                    }
                };
                if (type == 2)
                {
                    if ((x == xmax) && (y<=ymax) && (y>=ymin)) 
                    {
                        flag = y;
                        //return (y); 
                    }
                    else
                    {
                        if (flag != 0) return (flag);
                    }
                };
                if (type == 1)
                {
                    if ((y == ymin) && (x<=xmax) && (x>=xmin)) 
                    {
                        flag = x;
                        //return (x); 
                    }
                    else
                    {
                        if (flag != 0) return (flag);
                    }
                };
                if (type == 0)
                {
                    if ((y == ymax) && (x <= xmax) && (x >= xmin))
                    {
                        flag = x;
                        //return (x); 
                    }
                    else
                    {
                        if (flag != 0) return (flag);
                    }
                };
                while (ee >= 0)
                {
                    if (obmen == 1) x = x + s1; else y = y + s2;
                    ee = ee - 2 * dx;
                }
                if (obmen == 1) y = y + s2; else x = x + s1;
                ee = ee + 2 * dy;
            }
            //if (type == 1) GS.DrawImage(cube, x2 * 10, y2 * 10);
            //if (type == 2) GS.DrawImage(cube2, x2 * 10, y2 * 10);
            //kray[0] = -1; kray[1] = -1; return (kray); 
            /*if (type == 3)
            {
                if (x == xmin) { return (y); }
            };
            if (type == 2)
            {
                if (x == xmax) { return (y); }
            };
            if (type == 1)
            {
                if (y == ymin) { return (x); }
            };
            if (type == 0)
            {
                if (y == ymax) { return (x); }
            };
             * */
            return (-1); //error
            //---завершение алгоритма---
        }


    }
}
