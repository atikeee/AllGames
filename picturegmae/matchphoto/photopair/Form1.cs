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
        private Image[] selectedimg;
        private Image[] allimgs;
        private Image imginput;
        private int maximgcount;
        private string srcdir;
        private Thread workerThread = null;
        private int gridn = 4;
        private delegate void SetImageCallback(Image img);
        private int pxsz = 600; 
        public Form1()
        {
            InitializeComponent();
            srcdir = Path.Combine(Directory.GetCurrentDirectory(), "src");

        }

        private void button1_Click(object sender, EventArgs e)
        {

            
            
            int sz = 0;
            try
            {
                sz = int.Parse(textBox1.Text);
            }
            catch (Exception ex)
            {

            }
            if (sz>0 &&sz%2==0)
            {
                if (sz*sz<= maximgcount)
                {
                    button2.Enabled = true;
                    this.workerThread = new Thread(new ThreadStart(this.methodotherthread));
                    this.workerThread.Start();
                }else
                {
                    MessageBox.Show("Please select lower size. dont have enough picture.");
                }
                
            }else
            {
                MessageBox.Show("Please select even number(non zero) for size");
            }
            
            
        }
        private void methodotherthread()
        {
            //all processing here and call imagereload from here. ....
            string nmbrfile = Path.Combine(Directory.GetCurrentDirectory(), "numbers.jpg");
            Image[] numarr = CropJoin.splitimg(nmbrfile, 10, 10);
            gridn = int.Parse(textBox1.Text);
            int[] index = CropJoin.getrandomnumber(maximgcount / 2 - 1, gridn * gridn / 2);
            int delay = 1;
            try
            {
                delay = int.Parse(textBox3.Text);
            }
            catch (Exception ex)
            {

            }
            //Debug.Print("index: " + string.Join(",", index));

            selectedimg = new Image[gridn * gridn];
            imginput = CropJoin.creategrid(pxsz, pxsz, gridn, gridn);
            for (int ii = 0; ii < selectedimg.Count(); ii++)
            //foreach (Image oneimg in selectedimg)
            {
                //imgadd = Image.FromFile(one);
                CropJoin.addimg(imginput, numarr[ii], ii, pxsz, pxsz, gridn, gridn);
            }
            pictureBox1.Image = imginput;
            Thread.Sleep(2000);
            //int[] x = new int[10];
            for (int ii = 0; ii < index.Count(); ii++)
            {
                selectedimg[2 * ii] = allimgs[2 * index[ii]];
                selectedimg[2 * ii + 1] = allimgs[2 * index[ii] + 1];
                //Debug.Print(String.Format("selected img : {0},{1} from: {2},{3}", 2 * ii, (2 * ii + 1), index[ii], index[ii] + 1));
            }
            CropJoin.shuffle(selectedimg);
            int[] rndidx;
            if (checkBox1.Checked == true)
            {
                rndidx = CropJoin.getrandomnumber(selectedimg.Count() - 1, selectedimg.Count());
            }else
            {
                rndidx = Enumerable.Range(0, selectedimg.Count()).ToArray();
            }
            //Debug.Print(string.Join(",", x));
            //for (int ii = 0; ii < selectedimg.Count(); ii++)
            foreach(int ii in rndidx)
            //foreach (Image oneimg in selectedimg)
            {
                Image imageinclone = (Image)imginput.Clone();
                //imgadd = Image.FromFile(one);
                CropJoin.addimg(imageinclone, selectedimg[ii], ii, pxsz, pxsz, gridn, gridn);
                pictureBox1.Image = imageinclone;
                Thread.Sleep(1000*delay);
            }
            pictureBox1.Image = imginput;
        }
        private void imagereload(Image img)
        {
            //text = text.Remove(text.Length);
            if (this.pictureBox1.InvokeRequired)
            {
                if (!this.pictureBox1.IsDisposed)
                {
                    SetImageCallback d = new SetImageCallback(imagereload);
                    this.Invoke(d, new object[] { img });
                }
            }
            else
            {
                this.pictureBox1.Image = img;
            }
        }
        /*
        public void apendlog(string text, int lvl = 4)
        {
            if (this.textBoxlog.InvokeRequired)
            {
                if (!this.textBoxlog.IsDisposed)
                {
                    AppendErrorLog d = new AppendErrorLog(apendlog);
                    this.Invoke(d, new object[] { text, lvl });
                }
            }
            else
            {
                string prefix = "";
                if (lvl == 1)
                {
                    prefix = "CRITICAL";
                }
                else if (lvl == 2)
                {
                    prefix = "ERROR";
                }
                else if (lvl == 3)
                {
                    prefix = "WARN";
                }
                else
                {
                    prefix = "INFO";
                }
                //this.textBoxlog.Text +=String.Format("{0,-20}: {1,20}",prefix,text+ "\r\n");
                string[] array = textBoxlog.Lines;
                Array.Resize(ref array, array.Length + 1);
                array[array.Length - 1] = String.Format("{0}\t{1}", prefix, text);
                textBoxlog.Lines = array;

            }
        }
        */
        private void Form1_Load(object sender, EventArgs e)
        {
            var files = Directory.EnumerateFiles(srcdir, "*.*", SearchOption.AllDirectories)
         .Where(s => s.EndsWith(".jpg"));
            maximgcount = files.Count();
            //Debug.Print("Total img: "+maximgcount);
            allimgs = new Image[maximgcount];
            int i = 0;
            foreach (string f in files)
            {
                
                allimgs[i] = Image.FromFile(f);
                i++;
              //  Debug.Print(i.ToString()+" : "+f);
                //MessageBox.Show(f);
                //allpictures.Add(f);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Image imageinclone = (Image)imginput.Clone();
            for (int ii = 0; ii < selectedimg.Count(); ii++)
            //foreach (Image oneimg in selectedimg)
            {
                //imgadd = Image.FromFile(one);
                CropJoin.addimg(imageinclone, selectedimg[ii], ii, pxsz, pxsz, gridn, gridn);
                pictureBox1.Image = imageinclone;
                
            }
        }
       
    }
}
