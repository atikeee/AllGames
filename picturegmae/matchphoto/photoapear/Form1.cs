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

namespace photoapear
{
    public partial class Form1 : Form
    {
        private int curimgidx = 0;
        private int maximgcount=0;
        private string srcdir;
        private int splitcount=20; 
        private List<string> allpictures = new List<string>();
        private Thread workerThread = null;
        private Image[] imgarr;
        private Image curimg;
        private int sp=1;
        private delegate void setImgCallback(Image img);
        public Form1()
        {
            InitializeComponent();
            srcdir = Path.Combine(Directory.GetCurrentDirectory(),"src");
            //AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
        }
        static void OnProcessExit(object sender, EventArgs e)
        {
            Console.WriteLine("I'm out of here");
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
        private void methodotherthread()
        {
            int w = curimg.Height;
            int h = curimg.Width;
            Image imgrnd = new Bitmap(w, h);
            var graphics2 = Graphics.FromImage(imgrnd);
            int w1 = w / splitcount;
            int h1 = h / splitcount;
            int[] rndidx = CropJoin.getrandomnumber(splitcount*splitcount-1,splitcount*splitcount);
            int loopcnt = 0; 
            foreach(int ind in rndidx)
            {
                int i = ind / splitcount;
                int j = ind % splitcount;
                graphics2.DrawImage(imgarr[ind], new Rectangle(i * h1, j * w1, h1, w1), new Rectangle(0, 0, h1, w1), GraphicsUnit.Pixel);
                setpic(imgrnd);
                if (loopcnt > splitcount * splitcount / 10)
                {
                    Thread.Sleep(sp);
                }
                loopcnt++;
            }
            graphics2.Dispose();
        }
        private void reload()
        {
            if (workerThread!= null)
            {
                
                workerThread.Abort();
            }
            Thread.Sleep(100);
            try
            {
                sp = int.Parse(textBox2.Text);
            }
            catch (Exception ex)
            {

            }
            int sz = 100;
            try
            {
                sz = int.Parse(textBox1.Text);
            }
            catch (Exception ex)
            {

            }
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
            if (curimgidx == maximgcount - 1)
            {
                button3.Enabled = false;
            }
            curimg = Image.FromFile(allpictures[curimgidx]);
            imgarr = CropJoin.splitimg(allpictures[curimgidx], splitcount, splitcount);

            this.workerThread = new Thread(new ThreadStart(this.methodotherthread));
            this.workerThread.Start();
            //Image imgpuzzle = CropJoin.removeimgpartially(imgarr, splitcount, splitcount, 4);
            //pictureBox1.Image = imgpuzzle;

        }
        
        private void setpic(Image img)
        {
            //text = text.Remove(text.Length);
            if (this.pictureBox1.InvokeRequired)
            {
                if (!this.pictureBox1.IsDisposed)
                {
                    setImgCallback d = new setImgCallback(setpic);
                    this.Invoke(d, new object[] { img });
                }
            }
            else
            {
                this.pictureBox1.Image = img;
            }
            //Thread.Sleep(sp);
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
           // reload();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            solvepuzzle();
            if (workerThread != null)
            {
                workerThread.Abort();
            }
        }
        private void solvepuzzle()
        {
            splitcount = int.Parse(textBox1.Text);
            pictureBox1.Image = CropJoin.combineimg(imgarr, splitcount, splitcount);
    
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

        
    }
}
