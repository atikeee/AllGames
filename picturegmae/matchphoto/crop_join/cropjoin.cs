using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using System.Threading;

namespace crop_join
{
    public static class CropJoin
    {
        public static Image[] splitimg(string srcfile,int r, int c)
        {
            Image[] imgarray = new Image[r*c];
            Image img = Image.FromFile(srcfile);
            int h = img.Height;
            int w = img.Width;
            int h1 = h / r;
            int w1 = w / c;
            for (int i = 0; i < c; i++)
            {
                for (int j = 0; j < r; j++)
                {
                    var index = i * r + j;
                  //  Debug.Print(index.ToString()+ w1.ToString() + ": " + h1.ToString());
                    imgarray[index] = new Bitmap(w1, h1);
                    var graphics = Graphics.FromImage(imgarray[index]);
                    graphics.DrawImage(img, new Rectangle(0, 0, w1, h1), new Rectangle(i * w1, j * h1, w1, h1), GraphicsUnit.Pixel);
                    graphics.Dispose();
                }
            }
            return imgarray;
        }
        public static void overlayimg(Image img, Image[] imgarray,int r, int c)
        {
           // Debug.Print(String.Format("size: {0} r: {1} c:{2}", imgarray.Count(), r, c));
            if (r > 0 && c > 0 && imgarray.Length == r * c)
            {
                int w1 = imgarray[0].Width;
                int h1 = imgarray[0].Height;
                int w = img.Width;
                int h = img.Height;
               // Debug.Print(String.Format("w: {0} h: {1} w1:{2} h1:{3}", w, h, w1, h1));

                //img = new Bitmap(w, h);
                var graphics2 = Graphics.FromImage(img);
                int index = 0;
                for (int i = 0; i < c; i++)
                {
                    for (int j = 0; j < r; j++)
                    {
                        //var index = listofindex[rndidx];
                        //Debug.Print("Index: " + index);
                        graphics2.DrawImage(imgarray[index], new Rectangle(i * w/10+8, j * h/10+8, w1, h1), new Rectangle(0, 0, w1, h1), GraphicsUnit.Pixel);
                        index++;
                    }
                }
                graphics2.Dispose();
                //pictureBox1.Image = imgarray[5];

            }
            else
            {
                img = new Bitmap(1, 1);
                Debug.Print("array size or row col mismatch");
            }
            //return img;
        }
        public static Image creategrid(int h, int w, int hn, int wn)
        {
            Bitmap img = new Bitmap(w+2,h+2);
            var graphics2 = Graphics.FromImage(img);
            Pen p = new Pen(Color.Red);
            for (int i = 0; i<=hn; i++)
            {
                int h1 = i * (h / hn);
                graphics2.DrawLine(p,0,h1,w,h1);
            }
            for (int i = 0; i <=wn; i++)
            {
                int w1 = i * (w / wn);
                graphics2.DrawLine(p, w1, 0, w1, h);
            }
            return img;

        }
        public static void addimg(Image wholeimg, Image img,int imgidx, int h, int w, int hn, int wn)
        {
            //Bitmap wholeimg = new Bitmap(w + 2, h + 2);
            int hpos = 0;
            int wpos = 0;
            int h1 = h / hn;
            int w1 = w / wn;
            if (imgidx != 0)
            {
                hpos = h1 * (int)(imgidx/hn);
                wpos = w1 * (int)(imgidx % wn);
            }
            
            
            var graphics2 = Graphics.FromImage(wholeimg);
            //graphics2.DrawImage(img, new Rectangle(wpos+1,hpos+1, w1-2, h1-2), new Rectangle(0, 0, w1-2, h1-2), GraphicsUnit.Pixel);
            graphics2.DrawImage(img, new Rectangle(wpos+1,hpos+1, w1-2, h1-2), new Rectangle(0, 0, img.Width,img.Width), GraphicsUnit.Pixel);
            //return wholeimg; 
        }
        public static Image removeimgpartially(Image[] imgarray, int r, int c,int d)
        {
            Bitmap imgrnd;
            if (r > 0 && c > 0 && imgarray.Length == r * c)
            {
                int w1 = imgarray[0].Width;
                int h1 = imgarray[0].Height;
                int w = w1 * c;
                int h = h1 * r;
                int showimgcount = (r * c / d);
                //Debug.Print(String.Format("w: {0} h: {1} w1:{2} h1:{3}", w,h,w1,h1));
                int[] randidx = getrandomnumber(r*c-1,showimgcount);
                imgrnd = new Bitmap(w, h);
                var graphics2 = Graphics.FromImage(imgrnd);
                int index = 0;
                for (int i = 0; i < c; i++)
                {
                    for (int j = 0; j < r; j++)
                    {
                        //var index = listofindex[rndidx];
                        //Debug.Print("Index: " + index);
                        bool match = false; 
                        foreach(int k in randidx)
                        {
                            if (k == index)
                            {
                                match = true; 
                            }
                        }
                        if (match)
                        {
                          graphics2.DrawImage(imgarray[index], new Rectangle(i * w1, j * h1, w1, h1), new Rectangle(0, 0, w1, h1), GraphicsUnit.Pixel);
                        }
                        index++;
                    }
                }
                graphics2.Dispose();
                //pictureBox1.Image = imgarray[5];

            }
            else
            {
                imgrnd = new Bitmap(1, 1);
                // Debug.Print("array size or row col mismatch");
            }
            return imgrnd;
        }
        public static void showimgpartially(Image[] imgarray, int r, int c,int d, System.Windows.Forms.PictureBox pictureBox1)
        {
            Bitmap imgrnd;
            if (r > 0 && c > 0 && imgarray.Length == r * c)
            {
                int w1 = imgarray[0].Width;
                int h1 = imgarray[0].Height;
                int w = w1 * c;
                int h = h1 * r;
                int showimgcount = (r * c / d);
                //Debug.Print(String.Format("w: {0} h: {1} w1:{2} h1:{3}", w,h,w1,h1));
                int[] randidx = getrandomnumber(r*c-1,showimgcount);
                foreach(int rx in randidx)
                {
                    imgrnd = new Bitmap(w, h);
                    var graphics2 = Graphics.FromImage(imgrnd);
                    int i = rx / r;
                    int j = rx % r; 
                    graphics2.DrawImage(imgarray[rx], new Rectangle(i * w1, j * h1, w1, h1), new Rectangle(0, 0, w1, h1), GraphicsUnit.Pixel);
                    pictureBox1.Image = imgrnd;
                    Thread.Sleep(100);
                    graphics2.Dispose();
                }
                
                //pictureBox1.Image = imgarray[5];

            }
            else
            {
                imgrnd = new Bitmap(1, 1);
                // Debug.Print("array size or row col mismatch");
            }
            //return imgrnd;
        }
        // Array should be size of r *c
        public static Image combineimg (Image[] imgarray, int r , int c)
        {
            Bitmap imgrnd;
           // Debug.Print(String.Format("size: {0} r: {1} c:{2}",imgarray.Count(),r,c));
            if (r>0 && c>0&& imgarray.Length==r*c)
            {
                int w1 = imgarray[0].Width;
                int h1 = imgarray[0].Height;
                int w = w1*c;
                int h = h1*r;
                //Debug.Print(String.Format("w: {0} h: {1} w1:{2} h1:{3}", w,h,w1,h1));

                imgrnd = new Bitmap(w, h);
                var graphics2 = Graphics.FromImage(imgrnd);
                int index = 0;
                for (int i = 0; i < c; i++)
                {
                    for (int j = 0; j < r; j++)
                    {
                        //var index = listofindex[rndidx];
                        //Debug.Print("Index: " + index);
                        graphics2.DrawImage(imgarray[index], new Rectangle(i * w1, j * h1, w1, h1), new Rectangle(0, 0, w1, h1), GraphicsUnit.Pixel);
                        index++;
                    }
                }
                graphics2.Dispose();
                //pictureBox1.Image = imgarray[5];

            }
            else
            {
                imgrnd = new Bitmap(1, 1);
               // Debug.Print("array size or row col mismatch");
            }
            return imgrnd;

        }
        public  static void saveimg(Image[] imgarr,string imgname="output")
        {
            int i = 0;
            foreach(Image img in imgarr)
            {
                string fn = String.Format("{0}.jpg",imgname+i.ToString().PadLeft(2,'0'));
               // Debug.Print(fn);
                i++;
                img.Save(fn, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }
        public static void saveimg(Image img,string imgname="output")
        {
            string fn = String.Format("{0}.jpg", imgname);
            img.Save(fn, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
        public static void shuffle(Image[] imgarray)
        {
            int n = imgarray.Count();
            Image[] outimgarr = new Image[n];
            //outimgarr = imgarray;
            Random rnd = new Random();
            while (n > 1)
            {
                int k = (rnd.Next(0, n) % n);
                n--;
                Image t = imgarray[k];
                imgarray[k] = imgarray[n];
                imgarray[n] = t;
            }
            //return outimgarr;
        }
        public static void shuffle(int[] intinput)
        {
            int n = intinput.Count();
            int[] outimgarr = new int[n];
            //outimgarr = imgarray;
            Random rnd = new Random();
            while (n > 1)
            {
                int k = (rnd.Next(0, n-1) % n);
                n--;
                int t = intinput[k];
                intinput[k] = intinput[n];
                intinput[n] = t;
            }
            //return outimgarr;
        }
        public static int[] getrandomnumber(int maxval, int retcount)

        {
            int[] retval = new int[retcount];
            int[] intarr    = Enumerable.Range(0, maxval + 1).ToArray();
            shuffle(intarr);
            //int[] retval = new int[retcount];
            //Random rnd = new Random();
            for (int i =0; i<retcount; i++)
            {
                retval[i] = intarr[i];
            }
            return retval;
        }
    }
}
