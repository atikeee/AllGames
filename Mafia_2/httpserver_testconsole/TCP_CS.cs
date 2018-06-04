using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Configuration;

namespace httpserver_testconsole
{

        public class TCP_CS
        {

        public string ipstr;
        private TcpClient clientSocket = default(TcpClient);
        public int port;
        private bool el = false;
        public Logging lg;
        public Dictionary<string, string> roleid;
        public Dictionary<string, string> playeridname;
        public List<int> mafialist = new List<int>();
        public int detective;
        public List<int> masonlist = new List<int>();
        public List<int> activeplayers = new List<int>();
        public List<Players> allplayers = new List<Players>();
        public Dictionary<int, string> idrole = new Dictionary<int, string>();
        //public Form1 f1;
        public TCP_CS(Logging lg)
        {
            this.lg = lg;
            this.el = Convert.ToBoolean(ConfigurationManager.AppSettings["enablelogging"]);
            int loglevel = Convert.ToInt16(ConfigurationManager.AppSettings["loglevel"]);
            this.ipstr = ConfigurationManager.AppSettings["ip"];
            this.port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            ReadConfig rcplr = new ReadConfig("playername.conf");
            playeridname = rcplr.allconfig;
            Players.idvsname = playeridname;
            //onRcvMSG(this, new clientmessageevent("Initializing..."));

        }


        //public bool validateallplayer()
        //{
        //    foreach (Players hcl in allplayers)
        //    {
        //        if ((hcl.clid > 0) && (hcl.stat))
        //        {
        //
        //        }
        //
        //    }
        //    return true;
        //}
        public Dictionary<string, string> refreshplayer()
        {
            Dictionary<string, string> activeplayerdic = new Dictionary<string, string>();
            foreach (Players pl in allplayers)
            {
                //MessageBox.Show(hcl.clid.ToString());
                if (pl.clid > 0)
                {
                    activeplayers.Add(pl.clid);
                    activeplayerdic.Add(pl.clid.ToString(), pl.name);
                }
            }
            return activeplayerdic;

        }
        //public void sendmsgtoclient(string msg)
        //{
        //    foreach (Players hc in allclients)
        //    {
        //        hc.writeData(msg);
        //    }
        //}

        public void initializeplayer(bool resetid = false)
        {
            foreach (Players pl in allplayers)
            {
                //pl.registered = true;
                pl.stat = true;
                pl.statnight = 0;
                if (resetid)
                    pl.clid = -1;
            }
            Players.killmafia = -1;
            Players.killwitch = -1;
            Players.savedoc = -1;
            Players.savewitch = -1;
        }
        public void startthegame()
        {
            List<string> playerroles = new List<string>();
            foreach (int pr in activeplayers)
                playerroles.Add("VILLAGER");
            ReadConfig rcroles = new ReadConfig("roles.conf");
            int ind = 0;
            foreach (KeyValuePair<string, string> kvp in rcroles.allconfig)
            {
                int rc = int.Parse(kvp.Value);

                for (int i = 0; i < rc; i++)
                {
                    if (ind >= playerroles.Count)
                    {
                        MessageBox.Show("Not Enough Player Registered. Total player: "+playerroles.Count.ToString());
                        //break;
                        return;
                    }
                    playerroles[ind] = kvp.Key;
                    ind++;
                }
                
            }
            //all roles are in list playerroles
            //Shuffle roles. 
            Random rand1 = new Random();

            for (int i = 0; i < playerroles.Count; i++)
            {
                int randidx = rand1.Next(playerroles.Count);
                string tempstr = playerroles[i];
                playerroles[i] = playerroles[randidx];
                playerroles[randidx] = tempstr;
            }
            ind = 0;
            foreach (int id in activeplayers)
            {
                idrole.Add(id, playerroles[ind]);
                lg.deb(string.Format("id: {0} role: {1} ",id,playerroles[ind]));
                ind++;
            }
            // Assign roles to player. 
            Players.idvschar = idrole;
            foreach(KeyValuePair<int,string> kvp in idrole)
            {
                lg.inf(string.Format("Player Role: {0} {1}", kvp.Key.ToString(),kvp.Value));
            }

            publishidinfo();
        }
        public void nightover()
            {
                foreach (Players pl in allplayers)
                {
                    if (pl.clid == Players.killmafia)
                    {
                        pl.stat = false;
                    }
                    if (pl.clid == Players.killwitch)
                    {
                        pl.stat = false;
                    }

                }
                if ((Players.killmafia > 0)&&(Players.killmafia != Players.savedoc) && (Players.killmafia != Players.savewitch))
                {
                    // KillFlag
                    MessageBox.Show(Players.idvsname[Players.killmafia.ToString()] + " Was killed tonight...");
                }
                if (Players.killwitch > 0)
                {
                    MessageBox.Show(Players.idvsname[Players.killwitch.ToString()] + " Was killed tonight...");
                }

            }
        private void publishidinfo()
        {
            
            string mafia = "";
            //
            string playerlistmsg = ""; 
            
            foreach (Players pl in allplayers)
            {
                pl.role = Players.idvschar[pl.clid];
                if (pl.role.StartsWith("MAFIA"))
                {
                    mafia += pl.name + " = " + pl.role + "  <BR>";
                    mafialist.Add(pl.clid);
                }
                playerlistmsg += string.Format("{0}:{1}|", pl.clid, pl.name);
            }

            foreach (Players pl in allplayers)
            {
                //sendmsgtoclient(playerlistmsg);

                if (pl.role.StartsWith("MAFIA"))
                {
                    //sendmsgtoclient(mafia);
                    pl.idmsg = mafia;
                }
                else if (pl.role == "MINION")
                {
                    pl.idmsg = "You Are MINION<BR>" + mafia;
                    //sendmsgtoclient("You are MINION " + hcl.name);
                    //sendmsgtoclient(mafia);
                }
                else
                {
                    pl.idmsg = "You are " + pl.role;
                    //sendmsgtoclient("You are " + hcl.role + " , " + hcl.name);
                }
            }
        }
        //private bool connectionflag = true;
            private ManualResetEvent connectionInitiated = new ManualResetEvent(false);
            //public void startservernewthread()
            //{
            //     serverinithread = new Thread(() =>
            //            {
            //                startserver();
            //            });
            //    //serverinithread.IsBackground = true;
            //    serverinithread.Start();
            //}
            public void closethreads()
            {

                //connectionflag = false;
                /*    
                foreach(Players hc in allclients)
                {
                    hc.closeClient();
                }
                if(listener!=null)
                    listener.Stop();
                Thread.Sleep(300);
                if ((acceptConnectionThread != null) && (acceptConnectionThread.IsAlive))
                {
                    acceptConnectionThread.Join();
                    //serverinithread.Interrupt();
                    //serverinithread.Abort();// Join();
                }
                */
            }
            
            //private void HandleIncomingConnections()
            //{
            //    try
            //    {
            //        localEndPoint = new IPEndPoint(IPAddress.Parse(this.ipstr), this.port);
            //        // Bind the socket to the local endpoint and listen for incoming connections.  
            //        connectionListener.Bind(localEndPoint);
            //        connectionListener.Listen(100);
            //
            //        this.isRunning = true;
            //        while (this.isRunning)
            //        {
            //            // Set the event to nonsignaled state.  
            //            connectionInitiated.Reset();
            //            // Start an asynchronous socket to listen for connections.  
            //            connectionListener.BeginAccept(new AsyncCallback(AcceptConnectionCallback), connectionListener);
            //            // Wait until a connection is made before continuing.  
            //            connectionInitiated.WaitOne();
            //        }
            //
            //    }
            //    catch (Exception e)
            //    {
            //        Debug.Print("error");
            //    }
            //}
            //private static int clientCount;
            //private void AcceptConnectionCallback(IAsyncResult result)
            //{
            //    // Signal the main thread to continue.  
            //    connectionInitiated.Set();
            //
            //    // Get the socket that handles the client request.  
            //    Socket connectionlistener = (Socket)result.AsyncState;
            //    Socket individualConnectionSocket = connectionlistener.EndAccept(result);
            //
            //    // Create the state object.  
            //    lock (this)
            //    {
            //        // Create a new connection Info
            //        RemoteClientConnection connectionInfo = new RemoteClientConnection(clientCount, individualConnectionSocket);
            //        // Print message
            //        Debug.Print(string.Format("Connected to client #{0}", clientCount));
            //        // Add it to the list
            //        this.connectedClientSockets.Add(connectionInfo);
            //        // Increment the client count
            //        connectionInfo.onRcvMSG += ConnectionInfo_onRcvMSG;
            //        clientCount++;
            //    }
            //}
            //public delegate void clientMSGRcv(object sender, clientmessageevent e);
            //public event clientMSGRcv onRcvMSG;
            //private void ConnectionInfo_onRcvMSG(object sender, clientmessageevent e)
            //{
            //    Debug.Print("Event from Remote.. " + e.msg);
            //    onRcvMSG(this, new clientmessageevent(e.msg));
            //
            //}

            //public void startservernewthread()
            //{
            //
            //    try
            //    {
            //        this.connectedClientSockets = new List<RemoteClientConnection>();
            //        IPAddress ipAddress = IPAddress.Parse(this.ipstr);
            //
            //        Debug.Print("Starting TCP listener...\n");
            //        if (el)
            //            lg.inf("STarting TCP Listener");
            //        connectionListener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            //        //listener = new TcpListener(ipAddress, port);
            //        onRcvMSG(this, new clientmessageevent("Accepting Connection.."));
            //        acceptConnectionThread = new Thread(() => HandleIncomingConnections());
            //
            //        acceptConnectionThread.Start();
            //        onRcvMSG(this, new clientmessageevent("Thread started."));
            //
            //    }
            //    catch (Exception ex)
            //    {
            //        Debug.Print(ex.Message);
            //    }
            //
            //}
            //public void connecttoserver()
            //{
            //    try
            //    {
            //        clientSocket = new TcpClient();
            //        clientSocket.Connect(ipstr, port);
            //        Debug.Print("Client Socket Program - Server Connected ...");
            //    }
            //    catch (Exception ex)
            //    {
            //        Debug.Print(ex.Message);
            //    }
            //}
            public bool isconnected()
            {
                return clientSocket.Connected;
            }
           

        }

    }

