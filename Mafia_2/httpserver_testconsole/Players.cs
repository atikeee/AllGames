using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace httpserver_testconsole
{
    public class Players
    {
        public Logging lg;
        public string idmsg = "";
        //public string replymsg = "";
        public bool stat = true; // true means alive.
        public string clIP;
        public int statnight = 0;// track number of message recieved.. 
        public int clid = -1;
        public string name = "NA";
        //public int destclid = -1;
        public string role = "NA";
        public static Dictionary<int, string> idvschar;
        public static Dictionary<string, string> idvsname;
        //NetworkStream netStream;
        //private Thread readThread;
        // private bool activestate = true; 
        public int msgcount = 0;

        public static int killmafia = -1;
        public static int killwitch = -1;
        public static int savedoc = -1;
        public static int savewitch = -1;
        //public static Dictionary<int, NetworkStream> allstream = new Dictionary<int, NetworkStream>();
        // NetworkStream writeStream;
        // public void startClient(TcpClient inClientSocket, string clientip)
        // {
        //     this.clsocket = inClientSocket;
        //     this.clIP = clientip;
        //     readThread = new Thread(readData);
        //     readThread.Start();
        //     netStream = clsocket.GetStream();
        //     netStream.ReadTimeout = Timeout.Infinite;
        // }
        //
        // public void closeClient()
        // {
        //     writeData("close");
        //     netStream.Close();
        //     clsocket.Close();
        //     threadflag = false;
        //     Thread.Sleep(500);
        //     if ((readThread != null) && (readThread.IsAlive))
        //         readThread.Join();
        // }
        //public void writeData(string serverResponse)
        //{
        //    Byte[] sendBytes = null;
        //    sendBytes = Encoding.ASCII.GetBytes(serverResponse);
        //    //writeStream.Write(sendBytes, 0, sendBytes.Length);
        //    //writeStream.Flush();
        //    netStream.Write(sendBytes, 0, sendBytes.Length);
        //    netStream.Flush();
        //    //Console.WriteLine(" >> " + serverResponse);
        //}
        //public void writeDataToOther(string serverResponse, int clid)
        //{
        //
        //    Byte[] sendBytes = null;
        //    sendBytes = Encoding.ASCII.GetBytes(serverResponse);
        //    //writeStream.Write(sendBytes, 0, sendBytes.Length);
        //    //writeStream.Flush();
        //    allstream[clid].Write(sendBytes, 0, sendBytes.Length);
        //    allstream[clid].Flush();
        //    //Console.WriteLine(" >> " + serverResponse);
        //}
        public Players(Logging lg)
        {
            this.lg = lg;
        }
        public string reply="";
        public static string mafiareply = "";
        public void processdata(int dest,string op)
        {
            
            statnight++;
            string detectivereply = "NO";
            string minionreply = "NO";
            int magician = -1;
            List<int> allmaf = new List<int>();
            List<int> allmas = new List<int>();
            foreach (KeyValuePair<int, string> kvp in idvschar)
            {
                if (kvp.Value == "MAFIA")
                {
                    if (kvp.Key == dest)
                    {
                        detectivereply = idvsname[dest.ToString()]+" is MAFIA";
                    }
                    allmaf.Add(kvp.Key);
                }
                else if ((kvp.Value == "DETECTIVE") && (kvp.Key == dest))
                {
                    minionreply = idvsname[dest.ToString()]+"is DETECTIVE";
                }
                else if (kvp.Value == "MAGICIAN")
                {
                    magician = kvp.Key;
                }
                else if (kvp.Value == "MASON")
                {
                    allmas.Add(kvp.Key);
                }
            }

            if (role == "MAFIA")
            {
                
                string msg = idvsname[clid.ToString()] + " wants to kill " + idvsname[dest.ToString()] ;
                mafiareply += msg + "<BR>";
                reply = mafiareply;
                if (dest == magician)
                {
                    killmafia = clid;
                }
                else
                {
                    killmafia = dest;
                }
                statnight = 3;
            }
            else if (role == "DETECTIVE")
            {
                //writeData(detectivereply);
                statnight = 3;
                if(SimpleHTTPServer.night)
                reply = detectivereply;
                
            }
            else if (role == "MINION")
            {
                //writeData(minionreply);
                statnight = 3;
                if (SimpleHTTPServer.night)

                    reply = minionreply;

            }
            else if (role == "DOCTOR")
            {
                savedoc = clid;
                statnight = 3;

            }
            else if (role == "WITCH")
            {
                //kill 1000 + x
                //doctor 2000 + x
                if (op=="2")
                {
                    savewitch = dest ;
                }
                else if(op=="1")
                {
                    killwitch = dest;
                }
                statnight = 3;

            }
            else if (role == "MASON")
            {
                string msg = idvsname[dest.ToString()] + "is  MASON";
                foreach (int im in allmas)
                {
                    //writeDataToOther(msg, im);
                }
            }

            else
            {

            }
            lg.inf(string.Format("Processing Data: INPUT- {0} {1} OUTPUT- {2}", dest.ToString(), op,reply));
        }
        
    }
}
