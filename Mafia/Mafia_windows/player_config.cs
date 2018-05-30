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
namespace sufflearray
{
    public partial class player_config : Form
    {
        public Object players;
        private int playeridx;
        private Player p; 
        public player_config(Object players)
        {
            InitializeComponent();
            this.players = players;
            this.playeridx = 0;

        }

        private void player_config_Load(object sender, EventArgs e)
        {
            List<Player> ps = (List<Player>)this.players;
            this.p = ps[this.playeridx];
            button_next.Enabled = true;
            button_prev.Enabled = true;
            label1.Text = this.playeridx.ToString();
            if (this.playeridx == 0)
            {
                button_prev.Enabled = false;
            }
            if (this.playeridx == ps.Count-1)
            {
                button_next.Enabled = false;
            }
            textBox_name.Text = this.p.name;
            textBox_phone.Text = this.p.phone;
            textBox_email.Text = this.p.email;
            checkBox_active.Checked = Convert.ToBoolean(this.p.active);
            if (this.p.msgtype == "B")
            {
                this.cbx1.Checked = true;
                this.cbx2.Checked = true;
            }
            else if(this.p.msgtype == "P")
            {
                this.cbx1.Checked = true;
                this.cbx2.Checked = false;
            }else if (this.p.msgtype == "E")
            {

                this.cbx1.Checked = false;
                this.cbx2.Checked = true;
            }else
            {

                this.cbx1.Checked = false;
                this.cbx2.Checked = false;
            }

        }

        private void button_prev_Click(object sender, EventArgs e)
        {
            this.playeridx -= 1;
            button_next_prev_click(sender, e);
        }

        private void button_next_Click(object sender, EventArgs e)
        {
            this.playeridx += 1;
            button_next_prev_click(sender, e);
           // MessageBox.Show("value idx " + this.playeridx.ToString());
        }
        private void button_next_prev_click(object sender, EventArgs e)
        {
            this.p.name = textBox_name.Text;
            this.p.email = textBox_email.Text;
            this.p.phone = textBox_phone.Text;
            this.p.active = checkBox_active.Checked.ToString();
            chkbox_CheckedChanged();
            player_config_Load(sender, e);
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            this.p.name = textBox_name.Text;
            this.p.email = textBox_email.Text;
            this.p.phone = textBox_phone.Text;
            this.p.active = checkBox_active.Checked.ToString();
            this.Close();
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            Player newp =  new Player();
            newp.name = textBox_name.Text;
            newp.email = textBox_email.Text;
            newp.phone = textBox_phone.Text;
            newp.active = checkBox_active.Checked.ToString();
            List<Player> ps = (List<Player>)this.players;
            ps.Add(newp);
            this.playeridx =  ps.Count-1;
            player_config_Load(sender, e);
            MessageBox.Show("After adding player restart your application!");

        }

        private void chkbox_CheckedChanged()
        {
            
            if ((this.cbx1.Checked)&&(this.cbx2.Checked))
            {
                this.p.msgtype = "B";
            }
            else if(this.cbx1.Checked)
            {
                this.p.msgtype = "P";
            }else if(this.cbx2.Checked)
            {
                this.p.msgtype = "E";
            }
            else
            {
                this.p.msgtype = "N";
            }
        }

    }
}
