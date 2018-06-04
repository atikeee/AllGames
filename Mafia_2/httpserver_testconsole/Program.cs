using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace httpserver_testconsole
{



    class Program
    {
        //static void Main(string[] args)
        //{
        //    string myFolder = @"C:\Dropbox\Scripts\C#\serverlocation";
        //
        //    //create server with auto assigned port
        //    SimpleHTTPServer myServer = new SimpleHTTPServer(myFolder,8888);
        //    Console.WriteLine("Server is running on this port: " + myServer.Port.ToString());
        //
        //}
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MafiaServer());
        }
    }
}
