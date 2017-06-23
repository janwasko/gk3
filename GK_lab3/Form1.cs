using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GK_lab3
{
    public partial class Form1 : Form
    {
        private Graphics graph;
        private Point[] pkt;
        private PointF pPrzeciecia;
        private Pen pen = new Pen(Color.Black, 2);
        Font drawFont = new Font("Arial", 10);
        System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
        int scaleX = 20;
        int scaleY = 20;

        public Form1()
        {
            InitializeComponent();
            graph = pictureBox1.CreateGraphics();
            pkt = new Point[4];
            pPrzeciecia = new Point(0, 0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void RysujOdcinek(Point p1, Point p2)
        {
            int xx = this.pictureBox1.Width / 4;
            int yy = this.pictureBox1.Height / 4;

            graph.DrawLine(pen, (p1.X * scaleX) + xx, (p1.Y * scaleY) + yy, (p2.X * scaleX) + xx, (p2.Y * scaleY) + yy);
        }
        private PointF WyznaczPktPrzecięcia()
        {
            float mi = 0;

            float delta = ((pkt[1].X - pkt[0].X) * (pkt[2].Y - pkt[3].Y)) - ((pkt[2].X - pkt[3].X) * (pkt[1].Y - pkt[0].Y));

            if (delta.Equals(0))
            {
                Console.WriteLine("Proste rownolegle");
            }
            else
            {
                float deltaMi = ((pkt[2].X - pkt[0].X) * (pkt[2].Y - pkt[3].Y)) - ((pkt[2].X - pkt[3].X) * (pkt[2].Y - pkt[0].Y));
                mi = deltaMi / delta;
            }
            float x = ((1 - mi) * pkt[0].X) + (mi * pkt[1].X);
            float y = ((1 - mi) * pkt[0].Y) + (mi * pkt[1].Y);

            PointF p = new PointF(x, y);
            return p;
        }

        private double obliczKat(PointF p1, PointF p2, PointF bazowy)
        {
            double angle = 0;
            double CosAngle = 0;

            PointF wektorA = new PointF(bazowy.X - p1.X, bazowy.Y - p1.Y);
            PointF wektorB = new PointF(bazowy.X - p2.X, bazowy.Y - p2.Y);
            double iloczynSkalarny = (wektorA.X * wektorB.X) + (wektorA.Y * wektorB.Y);
            double dlugoscA = Math.Sqrt(Math.Pow(wektorA.X, 2) + Math.Pow(wektorA.Y, 2));
            double dlugoscB = Math.Sqrt(Math.Pow(wektorB.X, 2) + Math.Pow(wektorB.Y, 2));

            CosAngle = iloczynSkalarny / (dlugoscA * dlugoscB);
            angle = Math.Acos(CosAngle) * (180 / Math.PI);

            return angle;
        }

        /*
        private Point3D punktProstejiPlaszczyzny(Point3D p1, Point3D p2, Point3D pl, double k)
        {
            Point3D pkt;

            Point3D b = p1;
            Point3D n = pl;
            Point3D d = new Point3D(p2.x - p1.x, p2.y - p1.y, p2.z - p1.z);

            double nb = (n.x * b.x) + (n.y * b.y) + (n.z * b.z);
            double nd = (n.x * d.x) + (n.y * d.y) + (n.z * d.z);

            double temp = (k - nb) / nd;
            double x = p1.x + (temp * d.x);
            double y = p1.y + (temp * d.y);
            double z = p1.z + (temp * d.z);

            pkt = new Point3D(x, y, z);

            return pkt;
        }
        */

        private void button1_Click(object sender, EventArgs e)
        {
            graph.Clear(Color.White);

            pkt[0].X = 1;
            pkt[0].Y = 1;
            pkt[1].X = 3;
            pkt[1].Y = 3;
            pkt[2].X = 1;
            pkt[2].Y = 3;
            pkt[3].X = 3;
            pkt[3].Y = 1;
            
            pPrzeciecia = WyznaczPktPrzecięcia();

            int xx = this.pictureBox1.Width / 4;
            int yy = this.pictureBox1.Height / 4;

            RysujOdcinek(pkt[0], pkt[1]);
            RysujOdcinek(pkt[2], pkt[3]);

            String temp = Convert.ToString("( " + pPrzeciecia.X + " ; " + pPrzeciecia.Y + " )");

            PointF p = new PointF((pPrzeciecia.X * scaleX) + xx, (pPrzeciecia.Y * scaleY) + yy + 10);

            graph.DrawString(temp, drawFont, myBrush, p);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            graph.Clear(Color.White);

            int xx = this.pictureBox1.Width / 4;
            int yy = this.pictureBox1.Height / 4;
            Font drawFont = new Font("Arial", 10);

            PointF V1;
            PointF V2;
            PointF Bazowy;

            V1 = new PointF(5, 0);
            V2 = new PointF(5, 5);

            Bazowy = new PointF(0, 0);

            double angle = obliczKat(V1, V2, Bazowy);

            String sAngle = Convert.ToString(angle);

            PointF p = new PointF(Bazowy.X + xx, Bazowy.Y + yy + 20);

            RysujOdcinek(new Point((int)Bazowy.X, (int)Bazowy.Y), new Point((int)V1.X, (int)V1.Y));
            RysujOdcinek(new Point((int)Bazowy.X, (int)Bazowy.Y), new Point((int)V2.X, (int)V2.Y));

            graph.DrawString(sAngle, drawFont, myBrush, p);
        }
    }
}
