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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace picturepuzzle
{
    public partial class Form1 : Form
    {
        private int curimgidx = 0;
        private int maximgcount=0;
        private string srcdir;
        private int splitcount=20; 
        private List<string> allpictures = new List<string>();
        private Image[] imgoriginal;

        public Form1()
        {
            InitializeComponent();
            srcdir = Path.Combine(Directory.GetCurrentDirectory(),"src");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            curimgidx++;
            reload();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            curimgidx--;
            reload();
        }
        private void reload()
        {
            if (splitcount < 1)
            {
                splitcount = 1;
            }
            textBox1.Text = splitcount.ToString();
            button1.Enabled = true;
            button3.Enabled = true;
            if (curimgidx == 0)
            {
                button1.Enabled = false;
            }
            if (curimgidx == maximgcount-1)
            {
                button3.Enabled = false;
            }
            Image[] imgarr = CropJoin.splitimg(allpictures[curimgidx], splitcount, splitcount);
            imgoriginal = (Image[])imgarr.Clone();
            Image imgpuzzle;
            if (radioButton1.Checked == true)
            {
                CropJoin.shuffle(imgarr);
                imgpuzzle = CropJoin.combineimg(imgarr, splitcount, splitcount);
                pictureBox1.Image = imgpuzzle;
            }
            else if (radioButton2.Checked == true)
            {
                imgpuzzle = CropJoin.removeimgpartially(imgarr, splitcount, splitcount, 5);
                pictureBox1.Image = imgpuzzle;
            }
            else if (radioButton3.Checked == true)
            {
                CropJoin.shuffle(imgarr);
                imgpuzzle = CropJoin.removeimgpartially(imgarr, splitcount, splitcount, 2);
                pictureBox1.Image = imgpuzzle;
            }
            else if (radioButton4.Checked == true)
            {
                
                CropJoin.showimgpartially(imgarr, splitcount, splitcount, 5, pictureBox1);
                
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var files = Directory.EnumerateFiles(srcdir, "*.*", SearchOption.AllDirectories)
          .Where(s => s.EndsWith(".jpg"));
            foreach (string f in files)
            {
                //MessageBox.Show(f);
                allpictures.Add(f);
            }
            maximgcount = allpictures.Count;
            reload();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            solvepuzzle();
        }
        private void solvepuzzle()
        {
            splitcount = int.Parse(textBox1.Text);
            pictureBox1.Image = CropJoin.combineimg(imgoriginal, splitcount, splitcount);
    
        }
   
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                splitcount = int.Parse(textBox1.Text);
            }catch(Exception exc)
            {
                MessageBox.Show("Enter Number only ");
            }
            finally
            {
                splitcount = int.Parse(textBox1.Text); 
            }
            reload();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            splitcount++;

            reload();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            splitcount --;
            reload();
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar ==101 || e.KeyChar == 69)
            {
                splitcount--;
                reload();
            }
            else if (e.KeyChar == 104 || e.KeyChar == 72)
            {
                splitcount++;
                reload();
            }
            else if (e.KeyChar == 114 || e.KeyChar == 82)
            {
                reload();
            }
            else if (e.KeyChar == 115 || e.KeyChar == 83)
            {
                solvepuzzle();
            }
            else if (e.KeyChar == 110 || e.KeyChar == 78)
            {
                curimgidx++;
                reload();
            }
            else if (e.KeyChar == 112 || e.KeyChar == 80)
            {
                curimgidx--;
                reload();
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
