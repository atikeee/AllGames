using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace httpserver_testconsole
{
    public class clientmessageevent : EventArgs
    {
        public string msg { get; private set; }

        public clientmessageevent(string msg)
        {
            this.msg = msg;
        }
    }
    public class Logging
    {
        private TextWriter w;
        //private StreamWriter w;
        private int lvl;
        private string fn;
        private string[] logprefix = { "INFO ", "DEBUG", "WARN ", "ERROR", "CRITI" };
        private int[] loglvl = { 10, 20, 30, 40, 50 };

        public Logging(string fn, int lvl)
        {
            this.fn = fn;
            w = new StreamWriter(fn);
            //w = File.AppendText(fn);
            this.lvl = lvl;
        }
        public void resetlog()
        {
            //w.Close()
            File.WriteAllText(this.fn, string.Empty);
        }
        /*
           CRITICAL	50
            ERROR	40
            WARNING	30
            INFO	20
            DEBUG	10
            NOTSET	0
         */
        //public void close()
        ~Logging()
        {
            //w.Close();
        }
        public void logprint(object logMessage, int i)
        {
            if (this.lvl <= this.loglvl[i])
            {
                if (logMessage.GetType() == typeof(string))
                {
                    logMessage = this.logprefix[i] + ":\t" + logMessage;
                    w.WriteLine(logMessage);
                    w.Flush();
                }
                else if (logMessage.GetType() == typeof(Dictionary<string, string>))
                {
                    string convertedlogmessage = this.logprefix[i] + ": (Dictionary)";
                    w.WriteLine(convertedlogmessage);
                    foreach (KeyValuePair<string, string> t in (Dictionary<string, string>)logMessage)
                    {
                        convertedlogmessage = String.Format("\t\t{0,-50}:{1}", t.Key, t.Value);
                        //Debug.Print(t.Key);
                        w.WriteLine(convertedlogmessage);
                    }
                    w.Flush();
                }
                else if (logMessage.GetType() == typeof(Dictionary<string, object>))
                {
                    string valuemessage = "";
                    string convertedlogmessage = this.logprefix[i] + ":(Dictionary) - value as list of string";
                    w.WriteLine(convertedlogmessage);
                    foreach (KeyValuePair<string, object> t in (Dictionary<string, object>)logMessage)
                    {
                        //Debug.Print("st ob"+t.Key);
                        if (t.Value.GetType() == typeof(List<string>))
                        {
                            foreach (string ts in (List<string>)t.Value)
                            {
                                valuemessage += "\t" + ts;
                            }
                            convertedlogmessage = String.Format("\t\t{0,-50}:{1}", t.Key, valuemessage);
                            valuemessage = "";
                            w.WriteLine(convertedlogmessage);
                        }
                    }
                    w.Flush();

                }


            }

        }
        public void inf(object logMessage)
        {
            this.logprint(logMessage, 0);
        }
        public void deb(object logMessage)
        {
            this.logprint(logMessage, 1);
        }
        public void war(object logMessage)
        {
            this.logprint(logMessage, 2);
        }
        public void err(object logMessage)
        {
            this.logprint(logMessage, 3);
        }
        public void cri(object logMessage)
        {
            this.logprint(logMessage, 4);
        }


    }
    public class ReadConfig
    {
        private string filename;
        public Dictionary<string, string> allconfig = new Dictionary<string, string>();
        public List<List<string>> stringarr = new List<List<string>>();

        // repeated key 0 = no repeatation , 1 = pick first , 2 pick last
        // repeated key 3 will make it as list. 
        public ReadConfig(string fn, int repeatedkey = 0)
        {
            filename = fn;
            string line;
            Debug.Print("file:" + fn);
            // Read the file and display it line by line.
            StreamReader file = new StreamReader(fn);
            while ((line = file.ReadLine()) != null)
            {
                if (repeatedkey == 3)
                {
                    string[] kv = line.Split('\t');
                    List<string> lst = kv.OfType<string>().ToList();
                    if (!line.StartsWith("#") && (kv.Count() > 1))
                    {
                        stringarr.Add(lst);
                    }

                }
                else
                {
                    string[] kv = line.Split('=');

                    if ((!line.StartsWith("#")) && (kv.Count() > 1))
                        if (repeatedkey == 0)
                        {
                            allconfig.Add(kv[0].Trim(), kv[1].Trim());
                        }
                        else if (repeatedkey == 1)
                        {
                            if (!allconfig.ContainsKey(kv[0]))
                            {
                                allconfig.Add(kv[0].Trim(), kv[1].Trim());
                            }
                        }
                        else if (repeatedkey == 2)
                        {
                            if (allconfig.ContainsKey(kv[0]))
                            {
                                allconfig[kv[0].Trim()] = kv[1].Trim();
                            }
                            else
                            {
                                allconfig.Add(kv[0].Trim(), kv[1].Trim());
                            }
                        }
                }

            }

            file.Close();
        }
        public List<List<string>> getitem_list(Dictionary<int, string> inputdict, bool regx = false)
        {
            // This function is returning the whole line of the info as list based on the dictionary input. 
            // dictionary key = index and value is value. it supports regex and non regex both. 
            // first column Y will make it regex. non regex is default. if the first column doesnt say 

            List<List<string>> matchedlist = new List<List<string>>();
            Dictionary<string, string> matchedval = new Dictionary<string, string>();
            foreach (List<string> lst in stringarr)
            {
                matchedval.Clear();
                //List<string> t = new List<string>();
                //List<string> d = new List<string>(); 
                // this will override regex value if it is defined in the first column of the text file. 
                if (lst[0].ToLower() == "y")
                {
                    regx = true;
                }
                else if (lst[0].ToLower() == "n")
                {
                    regx = false;
                }
                if (regx)
                {
                    bool matchflag = true;
                    foreach (KeyValuePair<int, string> kvp in inputdict)
                    {
                        string pattern = lst[kvp.Key];
                        Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
                        Match mc = rgx.Match(kvp.Value);

                        if (mc.Success)
                        {
                            if (kvp.Key == 2)
                            {
                                matchedval.Add("#t1#", mc.Groups[1].Value);
                                matchedval.Add("#t2#", mc.Groups[2].Value);
                                matchedval.Add("#t3#", mc.Groups[3].Value);
                                matchedval.Add("#t4#", mc.Groups[4].Value);
                            }
                            else if (kvp.Key == 3)
                            {
                                matchedval.Add("#d1#", mc.Groups[1].Value);
                                matchedval.Add("#d2#", mc.Groups[2].Value);
                                matchedval.Add("#d3#", mc.Groups[3].Value);
                                matchedval.Add("#d4#", mc.Groups[3].Value);
                            }
                        }
                        else
                        {

                            Debug.Print("false");
                            matchflag = false;
                            continue;
                        }
                    }
                    if (matchflag)
                    {
                        List<string> token = new List<string>() { "#t1#", "#t2#", "#t3#", "#d1#", "#d2#", "#d3#" };



                        for (int i = 4; i < lst.Count - 1; i += 2)
                        {
                            if (lst[i].Trim() != "")
                            {
                                List<string> oneres = new List<string>();
                                string newtc = lst[i];
                                string newdesc = lst[i + 1];
                                //inputdict[2]
                                foreach (string t in token)
                                {
                                    if (newtc.Contains(t))
                                        newtc = newtc.Replace(t, matchedval[t]);
                                    if (newdesc.Contains(t))
                                        newdesc = newdesc.Replace(t, matchedval[t]);
                                }

                                oneres.Add(newtc);
                                oneres.Add(newdesc);
                                matchedlist.Add(oneres);
                            }
                        }
                        //matchedlist = lst;
                        break;
                    }

                }
                else
                {
                    bool match = true;
                    foreach (KeyValuePair<int, string> kvp in inputdict)
                    {

                        if (lst[kvp.Key] != kvp.Value)
                        {
                            match = false;
                            continue;
                        }
                    }
                    if (match)
                    {

                        for (int i = 5; i < lst.Count - 1; i += 2)
                        {
                            if (lst[i].Trim() != "")
                            {
                                List<string> oneres = new List<string>();
                                string newtc = lst[i];
                                string newdesc = lst[i + 1];
                                oneres.Add(newtc);
                                oneres.Add(newdesc);
                                matchedlist.Add(oneres);
                            }
                        }

                        //matchedlist = lst;
                        break;
                    }
                }
            }
            //Debug.Print(matchedval.ToString());
            if (matchedlist.Count == 0)
            {
                matchedlist.Add(new List<string>() { inputdict[2], inputdict[3], "" });
            }
            return matchedlist;

        }

        public string getvalue(string k)
        {
            string v = "";
            if (allconfig.ContainsKey(k))
                v = allconfig[k];
            else
                throw new KeyNotFoundException("Key is not in config");

            return v;
        }
        public List<string> getList(string k, char c = ',')
        {
            string v = "";
            if (allconfig.ContainsKey(k))
            {
                v = allconfig[k];
            }
            else
            {
                throw new KeyNotFoundException("Key " + k + " is not in config");
            }

            List<string> retval = new List<string>(v.Split(c));
            return retval;
        }
        public Dictionary<string, string> getdictionary(string k, char c = ',', char kvsep = ':')
        {
            Dictionary<string, string> retval = new Dictionary<string, string>();
            List<string> kv = this.getList(k, c);
            foreach (string oneitem in kv)
            {
                string[] dickv = oneitem.Split(kvsep);
                if (dickv.Count() == 2)
                {
                    retval.Add(dickv[0], dickv[1]);
                }
                else
                {
                    throw new Exception("Improperformat for dictionary");
                }
            }
            return retval;
        }
    }
    class MISC
    {
    }
}
