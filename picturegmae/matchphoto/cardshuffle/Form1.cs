using crop_join;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace card_shuffle
{
    public partial class Form1 : Form
    {
        //private List<int> listofindex;
        //private Image[] imgarray;
        //private int w=0;
        //private int w1=0;
        //private int h=0;
        //private int h1=0;
        //private Image img; 
        public Form1()
        {
            InitializeComponent();
          //  listofindex = getlist(52);
          //  string srcfile = Path.Combine(Directory.GetCurrentDirectory(), "allcards.jpg");
          //  img = Image.FromFile(srcfile);
          //  h = img.Height;
          //  w = img.Width;
          //  h1 = h / 4;
          //  w1 = w / 13;

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            string srcfile = Path.Combine(Directory.GetCurrentDirectory(), "allcards.jpg");
            //string srcfile2 = Path.Combine(Directory.GetCurrentDirectory(), "2.jpg");
            string desfile = Path.Combine(Directory.GetCurrentDirectory(), "op");
            Image[] imgarr = CropJoin.splitimg(srcfile,4,13);
            //Image[] imgarr2 = CropJoin.splitimg(srcfile2,10,10);
            //Image[] imgarr_original = (Image[])imgarr.Clone();
            //if (checkBox_ri.Checked)
            //{
            //    CropJoin.shuffle(imgarr2);
            //}
            CropJoin.shuffle(imgarr);
            Image img = CropJoin.combineimg(imgarr, 4,13);
            //CropJoin.overlayimg(img,imgarr2,10,10);
            CropJoin.saveimg(img, desfile);
            pictureBox1.Image = img;
            //CropJoin.saveimg(imgarr,desfile);
            /*
            imgarray = new Image[52];
            for (int i = 0; i < 13; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    var index = i * 4 + j;
                    Debug.Print(w1.ToString()+": "+h1.ToString());
                    imgarray[index] = new Bitmap(w1, h1);
                    var graphics = Graphics.FromImage(imgarray[index]);
                    graphics.DrawImage(img, new Rectangle(0, 0, w1, h1), new Rectangle(i * w1, j * h1, w1, h1), GraphicsUnit.Pixel);
                    graphics.Dispose(); 
                }
            }
            //pictureBox1.Image = imgrnd;
            
            Shuffle(listofindex);
            var imgrnd = new Bitmap(w, h);
            int rndidx = 0;
            var graphics2 = Graphics.FromImage(imgrnd);
            for (int i = 0; i < 13; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    var index = listofindex[rndidx];
                    rndidx++;
                    Debug.Print("Index: " + index);
                    graphics2.DrawImage(imgarray[index], new Rectangle(i * w1, j * h1, w1, h1), new Rectangle(0, 0, w1, h1), GraphicsUnit.Pixel);
                }
            }
            graphics2.Dispose();
            //pictureBox1.Image = imgarray[5];
            pictureBox1.Image = imgrnd;
            //pictureBox1.Image = imgarray[5];
            */

        }
        public List<int> getlist(int n)
        {
            List<int> intlist = new List<int>();
            for (int i = 0; i < n; i++)
            {
                intlist.Add(i);
            }
            return intlist;
        }
        public void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            Random rnd = new Random();
            while (n > 1)
            {
                int k = (rnd.Next(0, n) % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

    }
}
