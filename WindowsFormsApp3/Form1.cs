using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace paint_tree
{
    public partial class Form1 : Form
    {
        const double PI = Math.PI; 
        Random rad = new Random();
        static private Graphics graphics;
        double th1 = 40 * Math.PI / 180;
        double th2 = 30 * Math.PI / 180;
        double per1 = 0.6;
        double per2 = 0.7;
        public Form1()
        {
            InitializeComponent();
            this.AutoScaleDimensions = new Size(6, 14);
            this.ClientSize = new Size(1000, 1000);
            this.Paint += new PaintEventHandler(this.Form1_Paint);
            this.Click += new EventHandler(this.Redraw);
        }
      
        private void Form1_Paint(object sender,PaintEventArgs e)
        {
            graphics = e.Graphics;
            drawtree(10, 500, 500, 100, -PI / 2);
        }
        private void Redraw(object sender, EventArgs e)
        {
            this.Invalidate();
        }
       
        double rand()
        {
            return rad.NextDouble();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
       
        void drawtree(int n,double x0,double y0,double len,double th)
        {
            if (n == 0) return;
            double x1 = x0 + len * Math.Cos(th);
            double y1 = y0 + len * Math.Sin(th);
            drawLine(x0, y0, x1, y1, n / 2);
            drawtree(n - 1, x1, y1, per1 * len*(0.5+2*rand()), th + th1 * (0.5 + rand()));
            drawtree(n - 1, (x0+x1)/2, (y0+y1)/2, per2 * len*(0.4+2*rand()), th - th2 * (0.5 + rand()));
        }
        void drawLine(double x0,double y0,double x1,double y1,double width)
        {
            graphics.DrawLine(new Pen(Color.Green, (int)(width * (0.5 + 2*rand()))), (int)x0, (int)y0, (int)x1, (int)y1);
        }
    }
}
