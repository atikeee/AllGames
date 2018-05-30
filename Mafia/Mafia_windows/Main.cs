using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Reflection;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Diagnostics;

namespace sufflearray
{
    public partial class Main : Form
    {
        public object players;
        public object econfig;
        public XmlDocument xmlDoc;
        //private int[] activeidx ;
        private List<int> activeidx;
        private string[] role;
        //private string outputmessage ;
        private int gameno;
        private MailMessage msg;
        private SmtpClient client;
        private NetworkCredential login;
        private Emailconfiguration ec;
        public Main()
        {
            InitializeComponent();
            this.gameno = 1;
            
        }


        private void configPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            player_config playconfig = new player_config(this.players);
            playconfig.Show();
        }

        private void configEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EmailConfig emailconfig = new EmailConfig(this.econfig);
            emailconfig.Show();
        }
        
        private int playercount;
        private void readplayerinfofromcsv()
        {

        }
        private void readplayerinfo()
        {
            xmlDoc = new XmlDocument();
            this.xmlDoc.Load("conf.xml");
            //String[] topnodes = { "players", "emailconfig" };
            Type t_player = typeof(Player);
            FieldInfo[] f_player = t_player.GetFields();
            List<Player> players = new List<Player>();
            XmlNodeList allp = this.xmlDoc.GetElementsByTagName("player");
            foreach (XmlNode pl in allp)
            {
                string[] info = new string[f_player.Length];
                for (int i = 0; i < f_player.Length; i++)
                {
                    //      Console.WriteLine("Name: {0} Value : {1}", f_player[i].Name,pl[f_player[i].Name].InnerText);
                    info[i] = pl[f_player[i].Name].InnerText;
                }
                Player p = new Player(info);
                players.Add(p);

            }
            this.players = players;
        }
        private void Main_Load(object sender, EventArgs e)
        {

            readplayerinfo();
            XmlNode emailcnf = this.xmlDoc.GetElementsByTagName("emailconfig").Item(0);
            Type t_email = typeof(Emailconfiguration);
            FieldInfo[] f_email = t_email.GetFields();
            string[] e_info = new string[f_email.Length];
            for (int i = 0; i < f_email.Length; i++)
            {
                e_info[i] = emailcnf[f_email[i].Name].InnerText;
            }
            this.econfig = new Emailconfiguration(e_info);
            this.ec = (Emailconfiguration)this.econfig;
            this.login = new NetworkCredential(ec.user, ec.pass);
            client = new SmtpClient(ec.smtp);
            client.Port = Convert.ToInt32(ec.port);
            client.EnableSsl = Convert.ToBoolean(ec.ssl);
            client.Credentials = login;
            //econfig = ec;
            List<Player> ps = (List<Player>)this.players;
            playercount = ps.Count;
            this.buttonArray = new Button[playercount];
            playersub = new List<string>();
            playermsg = new List<string>();
            for (int i = 0; i < playercount;i++)
            {
                playermsg.Add("DUMMY MSG");
                playersub.Add("NONE");
            }
            

            updateplayerlist();
        }
        private List<string> playersub;
        private List<string> playermsg;
        private Button[] buttonArray;
        private bool flagcreatebutton = true;
        //private List<Button> alla
        private void updateplayerlist()
        {
            this.activeidx = new List<int>();
            List<Player> ps = (List<Player>)this.players;
            
            int horizotal = 12;
            int vertical = 66;
            for (int i = 0; i < playercount; i++)
            {
                int xpos = i % 7;
                int ypos = i / 7;
                Player pp = ps[i];
                Button btn;
                if (flagcreatebutton)
                {
                    btn = new Button();
                    this.buttonArray[i] = btn;
                    btn.Size = new Size(90, 30);
                    horizotal = xpos * 95 + 30;
                    vertical = ypos * 35 + 40;
                    btn.Location = new Point(horizotal, vertical);
                    btn.UseVisualStyleBackColor = true;
                    btn.Text = pp.name;
                    btn.Name = i.ToString();
                    this.groupBox1.Controls.Add(buttonArray[i]);
                    btn.Click += new EventHandler(this.playerButton_Click);
                }
                else
                {
                    btn = this.buttonArray[i]; 
                }
                
                if (pp.active == "True")
                {
                    btn.Enabled = true;
                    this.activeidx.Add(i);
                }
                else
                {
                    //this.textBox_inactive.AppendText(pp.name);
                    //this.textBox_inactive.AppendText(Environment.NewLine);
                    btn.Enabled = false;
                }

            }
            flagcreatebutton = false;
            int size = this.activeidx.Count;
            this.role = new string[size];
            //this.label_active.Text = "Active Player (" + this.activeidx.Count.ToString() + ")";
            List<string> allactiverole = new List<string>();
            for (int rc = 0; rc < int.Parse(textBoxSMafia.Text); rc++)
            {
                allactiverole.Add("!!!MAFIA Super  -> Kill one at night");
            }
            for (int rc = 0; rc < int.Parse(textBoxMafia.Text); rc++)
            {
                allactiverole.Add("!!!MAFIA -> Kill one at night");
            }
            for (int rc = 0; rc < int.Parse(textBoxDet.Text); rc++)
            {
                allactiverole.Add("??DETECTIVE -> Guess one player for Mafia/werewolf");
            }
            for (int rc = 0; rc < int.Parse(textBoxMason.Text); rc++)
            {
                allactiverole.Add("*Mason -> See who else is Mason. you are a villager");
            }
            for (int rc = 0; rc < int.Parse(textBoxDoc.Text); rc++)
            {
                allactiverole.Add("❤DOCTOR -> can save one life");
            }
            for (int rc = 0; rc < int.Parse(textBoxMinion.Text); rc++)
            {
                allactiverole.Add("@Minion -> see who is Mafia. you are partner for mafia.");
            }
            for (int rc = 0; rc < int.Parse(textBoxwitch.Text); rc++)
            {
                allactiverole.Add("$Witch -> can save a life once. detect mafia once. ");
            }
            if (this.checkBox_mod.Checked)
            {
                allactiverole.Add("==MODERATOR==");
            }

            for (int i = 0; i < size; i++)
            {

                if (i < allactiverole.Count)
                {
                    this.role[i] = allactiverole[i];
                }

                else
                {
                    this.role[i] = ":-( POOR VILLAGER :-(";
                }


            }
            Random rand1 = new Random();

            for (int i = 0; i < size; i++)
            {
                int randidx = rand1.Next(size);
                string tempstr = this.role[i];
                this.role[i] = this.role[randidx];
                this.role[randidx] = tempstr;
            }

            
            //            MessageBox.Show(string.Join(",", this.role));

        }

        private void playerButton_Click(object sender, EventArgs e)
        {
            readplayerinfo();
            Button thisbtn = (Button)sender;
            int playerid = int.Parse(thisbtn.Name);
            Debug.Print(playermsg[playerid]);
            sendmsg(playerid, playersub[playerid], playermsg[playerid]);

        }



        private void saveAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Player> ps = (List<Player>)this.players;
            String[] topnodes = { "players", "emailconfig" };
            Type t_player = typeof(Player);
            FieldInfo[] f_player = t_player.GetFields();
            this.xmlDoc.LoadXml("<configuration></configuration>");
            XmlNode elem = this.xmlDoc.CreateNode(XmlNodeType.Element, topnodes[0], null);
            XmlNode elem1 = this.xmlDoc.DocumentElement.AppendChild(elem);
            //for (int i = 0; i < infos.Length; i++)
            for (int i = 0; i < ps.Count; i++)
            {
                elem = this.xmlDoc.CreateNode(XmlNodeType.Element, "player", null);
                xmlDoc.DocumentElement.AppendChild(elem);
                for (int j = 0; j < f_player.Length; j++)
                {
                    XmlNode elem2 = this.xmlDoc.CreateNode(XmlNodeType.Element, f_player[j].Name, null);
                    Player pp = ps[i];
                    elem2.InnerText = (string)f_player[j].GetValue(pp);
                    elem.AppendChild(elem2);
                }
                elem1.AppendChild(elem);
            }
            elem = this.xmlDoc.CreateNode(XmlNodeType.Element, topnodes[1], null);
            elem1 = this.xmlDoc.DocumentElement.AppendChild(elem);
            Type t_email = typeof(Emailconfiguration);
            FieldInfo[] f_email = t_email.GetFields();
            string[] e_info = new string[f_email.Length];
            for (int i = 0; i < f_email.Length; i++)
            {
                XmlNode elem2 = this.xmlDoc.CreateNode(XmlNodeType.Element, f_email[i].Name, null);
                //Emailconfigurationlayer ec = ps[i];
                elem2.InnerText = (string)f_email[i].GetValue((Emailconfiguration)econfig);
                elem1.AppendChild(elem2);
            }
            //elem1.AppendChild(elem);


            this.xmlDoc.DocumentElement.AppendChild(elem);
            this.xmlDoc.Save("conf.xml");
            MessageBox.Show("Configuration saved.");

        }

        private void button_update_Click(object sender, EventArgs e)
        {
            updateplayerlist();
            //MessageBox.Show("List Upldated.");
            label_status.Text = "Player Info update complete.";

        }

        private void button_play_Click(object sender, EventArgs e)
        {

            try
            {
                this.gameno = int.Parse(textBoxgn.Text);
            }
            catch (Exception)
            {


            }
            String subj = "Round ->" + this.gameno.ToString();
            updateplayerlist();
            List<string> mafialst = new List<string>();
            List<string> minlst = new List<string>();
            List<string> doclst = new List<string>();
            List<string> detlst = new List<string>();
            List<string> witchlst = new List<string>();
            for (int i = 0; i < this.activeidx.Count; i++)
            {
                List<Player> ps = (List<Player>)this.players;
                if (this.role[i].StartsWith("!!!MAFIA"))
                {
                    mafialst.Add(ps[this.activeidx[i]].name);
                }else if (this.role[i].StartsWith("@Minion"))
                {
                    minlst.Add(ps[this.activeidx[i]].name);
                }
                else if (this.role[i].StartsWith("??DETECTIVE"))
                {
                    detlst.Add(ps[this.activeidx[i]].name);
                }
                else if (this.role[i].StartsWith("❤DOCTOR"))
                {
                    doclst.Add(ps[this.activeidx[i]].name);
                }
                else if (this.role[i].StartsWith("$Witch"))
                {
                    witchlst.Add(ps[this.activeidx[i]].name);
                }
            }
            
            for (int i = 0; i < this.activeidx.Count; i++)
            {
                String r = this.role[i];
                if ((this.role[i].StartsWith("@Minion"))||(this.role[i].StartsWith("!!!MAFIA"))||(this.role[i].StartsWith("== MODERATOR ==")))
                {
                    r += "\nMafia=" + string.Join(" and ", mafialst);
                }
                if (this.role[i].StartsWith("== MODERATOR =="))
                {
                    r += "\nDoctor=" + string.Join(" and ", doclst);
                    r += "\nDetective=" + string.Join(" and ", detlst);
                    r += "\nWitch=" + string.Join(" and ", witchlst);
                    r += "\nMinion=" + string.Join(" and ", minlst);
                }
                playermsg[this.activeidx[i]] = r;
                playersub[this.activeidx[i]] = subj;
                sendmsg(this.activeidx[i],subj,r);
                Thread.Sleep(1000);
            }
            MessageBox.Show("Message Sending Complete.");
            this.label_status.Text = "IDLE";
            this.label_status.Refresh();
            this.gameno += 1;
            textBoxgn.Text = this.gameno.ToString();
            
        }

   

  
      
        private void sendmsg(int idx, string sub, string body)
        {
            string toaddress;
            List<Player> ps = (List<Player>)this.players;
            //for (int i = 0; i < this.activeidx.Count; i++)
            Player p = ps[idx];
            String r = body;
            string[] toaddarr = { "",""};
            if (p.msgtype == "B")
            {
                toaddarr[0] = p.phone;
                toaddarr[1] = p.email;
            }
            else if (p.msgtype == "E")
            {
                toaddarr[1] = p.email;
            } else if (p.msgtype == "P")
            {
                toaddarr[0] = p.phone;
            } 
            for (int xx = 0; xx < 2; xx++)
            {
                toaddress = toaddarr[xx];
                if (toaddress == "")
                    continue;
                try
                {
                    this.label_status.Text = "Sending Msg to " + p.name;
                    this.label_status.Refresh();
                    //this.textBox_active.Refresh();
                    MailAddress toadd = new MailAddress(toaddress);
                    MailAddress fromadd = new MailAddress(ec.user + ec.smtp.Replace("smtp.", "@"), "MAFIA_GAME", Encoding.UTF8);
                    msg = new MailMessage(fromadd, toadd);
                    //msg = new MailMessage { From = new MailAddress(ec.user + ec.smtp.Replace("smtp.", "@"), "MAFIA_GAME", Encoding.UTF8) };
                    //msg.To.Add(new MailAddress(toaddress));
                    msg.Subject = sub;
                    msg.Body = r;
                    msg.IsBodyHtml = true;
                    msg.Priority = MailPriority.Normal;
                    msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                    //client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
                    //string userstate = "Sending...";
                    //client.SendAsync(msg, userstate);
                    client.Send(msg);
                    //MessageBox.Show(subj+r);
                    Thread.Sleep(3000);
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Fail Sending msg:(" + p.name + ") " + exc.ToString());
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //for (int i = 0; i < this.activeidx.Count; i++)
            foreach (int idx in this.activeidx)
            {

                sendmsg(idx, "Test Subject", "Test");
            }
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
    class Player
    {
        public string name;
        public string phone;
        public string email;
        public string active;
        public string msgtype;
        private string currole;

        public Player(String[] s)
        {

            this.name = s[0];
            this.phone = s[1];
            this.email = s[2];
            this.active = s[3];
            this.msgtype = s[4];
        }
        public Player()
        {


        }
        public void setrole(string role)
        {
            this.currole = role;
        }
    }
    class Emailconfiguration
    {
        public string user;
        public string pass;
        public string port;
        public string smtp;
        public string ssl;
        public Emailconfiguration(string[] s)
        {
            this.user = s[0];
            this.pass = s[1];
            this.port = s[2];
            this.smtp = s[3];
            this.ssl = s[4];
        }
        public Emailconfiguration()
        {

        }


    }
}
