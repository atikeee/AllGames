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

        public Form1()
        {
            InitializeComponent();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            string srcfile = Path.Combine(Directory.GetCurrentDirectory(), "1.jpg");
            string srcfile2 = Path.Combine(Directory.GetCurrentDirectory(), "2.jpg");
            string desfile = Path.Combine(Directory.GetCurrentDirectory(), "op");
            Image[] imgarr = CropJoin.splitimg(srcfile, 10, 10);
            Image[] imgarr2 = CropJoin.splitimg(srcfile2, 10, 10);
            //Image[] imgarr_original = (Image[])imgarr.Clone();
            if (checkBox_ri.Checked)
            {
                CropJoin.shuffle(imgarr2);
            }
            CropJoin.shuffle(imgarr);
            Image img = CropJoin.combineimg(imgarr, 10, 10);
            CropJoin.overlayimg(img, imgarr2, 10, 10);
            CropJoin.saveimg(img, desfile);
            pictureBox1.Image = img;
            //CropJoin.saveimg(imgarr,desfile);
            string rndn="";
            Random rnd = new Random();
            int n = 10;
            while (n > 0)
            {
                int k = (rnd.Next(1, 100) % 100);
                n--;
                rndn += k.ToString() + "   ";
            }
            textBox1.Text = rndn;

        }
    }
}
