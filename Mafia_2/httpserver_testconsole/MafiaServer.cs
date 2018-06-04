using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace httpserver_testconsole
{
    public partial class MafiaServer : Form
    {
        private Logging lg;
        private bool iniflag = false;
        public Dictionary<string, string> roleid;
        public Dictionary<string, string> playeridname;
        private TCP_CS tcpcs;
        public delegate void AppendReport(string text);
        public void printReport(string text)
        {
            if (this.textBoxReport.InvokeRequired)
            {
                if (!this.textBoxReport.IsDisposed)
                {
                    AppendReport d = new AppendReport(printReport);
                    this.Invoke(d, new object[] { text });
                }
            }
            else
            {
                //this.textBoxlog.Text +=String.Format("{0,-20}: {1,20}",prefix,text+ "\r\n");

                textBoxReport.AppendText(text + Environment.NewLine);
                //string[] array = textBoxReport.Lines;
                //Array.Resize(ref array, array.Length + 1);
                //array[array.Length - 1] = String.Format("{0}", text);
                //textBoxReport.Lines = array;

            }
        }
        public MafiaServer()
        {
            lg = new Logging("log.txt", 0);
            InitializeComponent();
            tcpcs = new TCP_CS(lg);
        }
        private int sessioncount = -1;
        private void btn_start_Click(object sender, EventArgs e)
        {
            sessioncount = 0;
            btn_nightstart.Enabled = true;
            tcpcs.initializeplayer();
            tcpcs.startthegame();
            SimpleHTTPServer.registered = true;
            btn_start.Enabled = false;
        }

        private void btn_nightstart_Click(object sender, EventArgs e)
        {
            sessioncount++;
            // this =1 means first night. 
            lg.inf("Night start");
            SimpleHTTPServer.night = true;
            Players.killmafia = -1;
            Players.killwitch = -1;
            Players.savedoc = -1;
            Players.savewitch = -1;

            btn_nightend.Enabled = true;
            btn_nightstart.Enabled = false;
            //temp code. 
            //tcpcs.sendmsgtoclient(textBoxReport.Text);
            //tempcode. 
            //accept message from player and update the variables. 
        }

        private void btn_nightend_Click(object sender, EventArgs e)
        {
            lg.inf("Night End");
            SimpleHTTPServer.night = false;
            Players.mafiareply = "";
            bool playerready = true;
            foreach (Players pl in tcpcs.allplayers)
            {
                if ((pl.clid > 0) && (pl.stat))
                    if (pl.statnight < 3)
                    {
                        MessageBox.Show(pl.clid + " is not ready");
                        playerready = false;
                    }
            }
            if (playerready)
            {
                //btn_nightstart.Enabled = true;
                btnday.Enabled = true;
                btn_nightend.Enabled = false;
                tcpcs.nightover();
                comboBox1.Items.Clear();
                foreach (Players hcl in tcpcs.allplayers)
                {
                    if ((hcl.clid > 0) && (hcl.stat))
                        comboBox1.Items.Add(hcl.name);
                }
                if (gameover() == 1)
                {
                    MessageBox.Show("GAME OVER. GOOD Team WINS");
                    btn_start.Enabled = true;
                }
                else if (gameover() == 2)
                {
                    MessageBox.Show("GAME OVER. EVIL Team WINS");
                    btn_start.Enabled = true;
                }
                btnday.Enabled = true;
            }
        }
        private bool x;
        private Dictionary<string, Button> allbutton = new Dictionary<string, Button>();
        private void btn_refresh_Click(object sender, EventArgs e)
        {

            // remove all button here
            //btn_refresh.Enabled = false;
            lg.inf("REFRESH: refreshing player list. ");
            //Dictionary<string,string> players=            tcpcs.getallclient();
            //List<Button> allbtn = new List<Button>();
            tcpcs.refreshplayer();
            int cn = 0;
            int xlocation = groupBox1.Location.X;
            int ylocation = groupBox1.Location.Y;
            //Debug.Print(groupBox1.Left.ToString());

            foreach (Players pl in tcpcs.allplayers)
            {
                string clid = pl.clid.ToString();
                string clname = pl.name;
                lg.deb("PLAYERS: " + pl.name + " ID: " + pl.clid);
                Button btn;
                if (pl.clid > 0)

                {
                    if (allbutton.ContainsKey(pl.clid.ToString()))
                    {
                        btn = allbutton[pl.clid.ToString()];
                    }
                    else
                    {
                        btn = new Button();
                        btn.Font = new Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        btn.Location = new Point(40 + (cn % 2 * 120), 10 + (cn / 2) * 60);
                        btn.Name = "btn_" + clid;
                        btn.Size = new Size(115, 50);
                        btn.TabIndex = 7 + cn;
                        btn.Text = clid + ": " + clname;
                        btn.UseVisualStyleBackColor = true;
                        btn.BackColor = Color.LightGreen;
                    }
                    
                    //if (!pl.stat)
                    if(x)
                    {
                        btn.BackColor = Color.Red;
                        btn.Update();
                        groupBox1.Update();
                        this.Update();

                    }
                        x=true;
                    groupBox1.Controls.Add(btn);
                }
                //btn.Click += new System.EventHandler(this.btn_start_Click);

                //allbtn.Add(btn);
                cn++;
            }
            //tcpcs.validateallplayer();
            //Button btn = new Button();
            //btn.Font = new Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //btn.Location = new Point(80,10);
            //btn.Name = "btn_ssss";
            //btn.Size = new Size(113, 42);
            //btn.TabIndex = 7;
            //btn.Text = "text";
            //btn.UseVisualStyleBackColor = true;
            ////btn.Click += new System.EventHandler(this.btn_start_Click);,
            //
            ////allbtn.Add(btn);
            //groupBox1.Controls.Add(btn);
            btn_start.Enabled = true;
        }

        private void btnday_Click(object sender, EventArgs e)
        {
            btnday.Enabled = false;
            //btn_nightstart.Enabled = true;
            btn_refresh.Enabled = true;
            lg.inf("killed by villager");
            foreach (Players hcl in tcpcs.allplayers)
            {
                if (comboBox1.SelectedText == hcl.name)
                {
                    hcl.stat = false;
                }
            }
            if (gameover() == 1)
            {
                MessageBox.Show("GAME OVER. GOOD Team WINS");
                btn_start.Enabled = true;
            }
            else if (gameover() == 2)
            {
                MessageBox.Show("GAME OVER. EVIL Team WINS");
                btn_start.Enabled = true;
            }

        }
        private SimpleHTTPServer myServer;
        private void MafiaServer_Load(object sender, EventArgs e)
        {

            string myFolder = @"C:\Dropbox\Scripts\C#\serverlocation";
            if (!iniflag)
            {
                iniflag = true;
                TextBox tb = this.textBoxReport;
                //tcpcs.startservernewthread();
            }
            //create server with auto assigned port
            myServer = new SimpleHTTPServer(myFolder, tcpcs.ipstr, tcpcs.port, lg);
            //myServer.sessioncount = sessioncount;
            myServer.allplayers = tcpcs.allplayers;
            myServer.onRcvMSG += MyServer_onRcvMSG;
            printReport("Server is running on this port: " + myServer.Port.ToString());
        }

        private void MyServer_onRcvMSG(object sender, clientmessageevent e)
        {
            Debug.Print("Event from Remote.. " + e.msg);
            printReport(e.msg);
        }

        private int gameover()
        {
            int goodchar = 0;
            int mafiacount = 0;
            int badchar = 0;
            foreach (Players hcl in tcpcs.allplayers)
            {
                if (hcl.clid > 0)
                {
                    if (hcl.stat)
                    {
                        if (hcl.role.StartsWith("MAFIA"))
                        {
                            badchar++;
                            mafiacount++;
                        }
                        else if (hcl.role.StartsWith("MINION"))
                        {
                            badchar++;
                        }
                        else
                        {
                            goodchar++;
                        }
                    }
                }


            }
            if (goodchar < badchar)
            {

                return 2;
            }

            else if (mafiacount == 0)
            {

                return 1;
            }
            return 0;
        }

        private void MafiaServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            myServer.Stop();
        }


    }
}
