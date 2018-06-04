using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace httpserver_testconsole
{



    class SimpleHTTPServer
    {
        private readonly string[] _indexFiles = {
        "index.html",
        "index.htm",
        "default.html",
        "default.htm"
    };

        private static IDictionary<string, string> _mimeTypeMappings = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase) {
        #region extension to MIME type list
        {".asf", "video/x-ms-asf"},
        {".asx", "video/x-ms-asf"},
        {".htm", "text/html"},
        {".html", "text/html"},
        #endregion
    };
        private Thread _serverThread;
        private string _rootDirectory;
        private HttpListener _listener;
        private int _port;
        private string _ip;
        public List<Players> allplayers = null;
        public static bool registered = false;
        public delegate void clientMSGRcv(object sender, clientmessageevent e);
        public event clientMSGRcv onRcvMSG;
        public static bool night = false;
        public int Port
        {
            get { return _port; }
            private set { }
        }

        /// <summary>
        /// Construct server with given port.
        /// </summary>
        /// <param name="path">Directory path to serve.</param>
        /// <param name="port">Port of the server.</param>
        public string indexfileorigin;
        public string indexfile;
        public Logging lg;
        public SimpleHTTPServer(string path, string ip, int port, Logging lg)
        {
            this.lg = lg;
            this.Initialize(path, ip, port);
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader("index.html"))
                {
                    // Read the stream to a string, and write the string to the console.
                    indexfileorigin = sr.ReadToEnd();

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Construct server with suitable port.
        /// </summary>
        /// <param name="path">Directory path to serve.</param>
        public SimpleHTTPServer(string path)
        {
            //get an empty port
            TcpListener l = new TcpListener(IPAddress.Loopback, 0);
            l.Start();
            int port = ((IPEndPoint)l.LocalEndpoint).Port;
            l.Stop();
            this.Initialize(path, "localhost", port);
        }

        /// <summary>
        /// Stop server and dispose all functions.
        /// </summary>
        public void Stop()
        {
            _serverThread.Abort();
            _listener.Stop();
        }

        private void Listen()
        {
            lg.inf("Start Listening . PORT: " + _port + " IP: " + _ip);
            _listener = new HttpListener();
            _listener.Prefixes.Add("http://*:" + _port.ToString() + "/");
            //_listener.Prefixes.Add("http://"+_ip+":" + _port.ToString() + "/");
            //_listener.Prefixes.Add("http://localhost:" + _port.ToString() + "/");
            //_listener.Prefixes.Add("http://127.0.0.1:" + _port.ToString() + "/");
            //_listener.Prefixes.Add("http://0.0.0.0:" + _port.ToString() + "/");
            _listener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;
            _listener.Start();
            while (true)
            {
                lg.inf("Loop for getting context...");
                try
                {
                    HttpListenerContext context = _listener.GetContext();
                    Process(context);

                }
                catch (Exception)
                {

                }
            }
        }
        private void addPlayer(string ip, string id)
        {

            bool a = true;
            foreach (Players pl in allplayers)
            {

                if (pl.clIP == ip)
                {
                    pl.clid = int.Parse(id);
                    a = false;
                }
            }
            if (a)
            {
                Players pl = new Players(lg);
                pl.clid = int.Parse(id);
                pl.clIP = ip;
                pl.name = Players.idvsname[id];
                lg.inf(string.Format("Adding Player id={0} name={1} ip ={2}", id, pl.name, ip));
                allplayers.Add(pl);
            }
        }
        private clientmessageevent args;
        private void Process(HttpListenerContext context)
        {
            lg.inf("Process.." + "IP: " + context.Request.RemoteEndPoint.Address);
            //string reply = "";
            string ipa = context.Request.RemoteEndPoint.Address.ToString(); 
            Debug.Print("Process.... " + "IP: " + context.Request.RemoteEndPoint.Address);
            Players p = null;
            string plist = "";
            foreach (Players pl in allplayers)
            {
                if (pl.clIP == ipa)
                    p = pl;
                else if ((pl.clid > 0) && (pl.stat))
                    plist += string.Format(@"<option value = ""{0}"" >{1}</option>", pl.clid, pl.name);
            }
            //plist += string.Format(@"<option value = ""0"" >None</option>");
            if (p != null)
            {
                lg.deb(string.Format("USERINFO: Name: {0} ID: {1} IP: {2} ROLE: {3} MSG: {4}", p.name, p.clid, p.clIP, p.role, p.idmsg));
            }
            string idpart = @"
            Select your ID.
            <form action="""">
            <input type=""text"" name=""id"" value=""""><br>
            <input type= ""submit"" value = ""Submit"" >
            </form >
               ";

            //<input type=""text"" name=""ip"" value=""""><br>

            string button0 = @"<button name=""do"" type=""submit"" value=""0"">DUMMY</button>";
            string button1 = @"<button name=""do"" type=""submit"" value=""1"">KILL</button>";
            string button2 = @"<button name=""do"" type=""submit"" value=""2"">SAVE</button>";
            string button3 = @"<button name=""do"" type=""submit"" value=""2"">GUESS MAFIA</button>";
            string button4 = @"<button name=""do"" type=""submit"" value=""2"">GUESS DETECTIVE</button>";
            string buttoncomb = "";

            string id = "";
            try
            {
                id = context.Request.QueryString["id"];

            }
            catch (Exception ex)
            {

                Debug.Print("Exception: id read. " + ex.Message);
            }
            if (id != "")
                lg.inf("Registering ID: " + id);
            // setting up the button as per user. 
            if (p != null)
                if (p.clid > 0)
                {
                    if (p.role == "MAFIA")
                    {
                        buttoncomb = button1;
                    }
                    else if (p.role == "DETECTIVE")
                    {
                        buttoncomb = button3;
                    }
                    else if (p.role == "MINION")
                    {
                        buttoncomb = button4;
                    }
                    else if (p.role == "WITCH")
                    {
                        buttoncomb = button1 + button2 + button0;
                    }
                    else if(p.role == "DOCTOR")
                    {
                        buttoncomb = button2;
                    }
                    else if (p.role == "VILLAGER")
                    {
                        buttoncomb = button0;
                    }
                    if (!p.stat)
                    {
                        plist = "";
                        buttoncomb = "";
                    }
                }

            string target = "";
            string op = "";
            //string filename = context.Request.Url.AbsolutePath;
            try
            {
                target = context.Request.QueryString["pl"];
                op = context.Request.QueryString["do"];
            }
            catch (Exception ex)
            {
                Debug.Print("Exception: target read. " + ex.Message);
            }

            string idname = "";


            if ((id != "") && (id != null))
            {
                if (Players.idvsname.ContainsKey(id))
                {
                    idname = Players.idvsname[id];
                }
                if (!registered)
                {
                    addPlayer(ipa, id);
                    string msg = string.Format("Register-  Name: " + idname + " IP: " + ipa + "    ID: " + id);
                    args = new clientmessageevent(msg);
                    onRcvMSG(this, args);
                }

            }
            if ((target != null) && (target != ""))
            {
                if ((p != null) && (p.stat))
                {
                    Debug.Print("processdata: " + target);
                    p.processdata(int.Parse(target), op);
                }
                //foreach(Players plyr in allplayers)
                //{
                //    if ((plyr.clIP == ipa)&&(plyr.stat))
                //    {
                //        plyr.processdata(int.Parse(target));
                //        break;
                //    }
                //}
            }

            // readdata called


            //Console.WriteLine(filename);
            //filename = filename.Substring(1);
            //
            //if (string.IsNullOrEmpty(filename))
            //{
            //    foreach (string indexFile in _indexFiles)
            //    {
            //        if (File.Exists(Path.Combine(_rootDirectory, indexFile)))
            //        {
            //            filename = indexFile;
            //            break;
            //        }
            //    }
            //}

            //filename = Path.Combine(_rootDirectory, filename);

            //if (File.Exists(filename))
            //{
            string rpl = "";
            try
            {
                if ((p != null) && (p.clid > 0))
                {
                    string rolemsg = p.idmsg;
                    idpart = string.Format("You are registered as ID: {0} Name: {1}<BR>{2}", p.clid, p.name, p.idmsg);
                    if ((p.reply != "") )
                    {
                        rpl = p.reply;
                    }
                    
                }
                else
                {
                    plist = "";
                    buttoncomb = "";
                }
                //Stream input = new FileStream(filename, FileMode.Open);
                indexfile = indexfileorigin.Replace("##id##", idpart);
                indexfile = indexfile.Replace("##players##", plist);
                indexfile = indexfile.Replace("##button##", buttoncomb);
                indexfile = indexfile.Replace("##reply##", rpl);
                //Adding permanent http response headers


                //context.Response.ContentType = _mimeTypeMappings.TryGetValue(Path.GetExtension(filename), out mime) ? mime : "application/octet-stream";
                //context.Response.ContentLength64 = input.Length;
                context.Response.AddHeader("Date", DateTime.Now.ToString("r"));
                //context.Response.AddHeader("Last-Modified", System.IO.File.GetLastWriteTime(filename).ToString("r"));

                byte[] buffer = new byte[1024 * 16];
                buffer = Encoding.ASCII.GetBytes(indexfile);
                int nbytes = buffer.Count();
                //while ((nbytes = input.Read(buffer, 0, buffer.Length)) > 0)
                context.Response.OutputStream.Write(buffer, 0, nbytes);
                //input.Close();

                context.Response.StatusCode = (int)HttpStatusCode.OK;
                context.Response.OutputStream.Flush();
            }
            catch (Exception)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            //}
            //else
            //{
            //    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            //}

            context.Response.OutputStream.Close();
            //context.Response.Redirect("http://"+_ip+":"+_port+"/");
        }

        private void Initialize(string path, string ip, int port)
        {
            this._rootDirectory = path;
            this._port = port;
            this._ip = ip;
            _serverThread = new Thread(this.Listen);
            _serverThread.Start();
        }


    }
}
