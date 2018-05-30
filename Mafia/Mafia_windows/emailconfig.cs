using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sufflearray
{
    
    public partial class EmailConfig : Form
    {
        public object econfig;
        public EmailConfig(object econfig)
        {
            this.econfig = econfig;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Emailconfiguration econfig = (Emailconfiguration)this.econfig;
            econfig.user = this.textBox_user.Text;
            econfig.pass = this.textBox_pass.Text;
            econfig.port = this.textBox_port.Text;
            econfig.smtp = this.textBox_smtp.Text;
            econfig.ssl = this.checkBox_ssl.Checked.ToString();
            this.Close();
        }
    }
}
