using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ModelLib;
using Newtonsoft.Json;

namespace AutoVenProxyServer
{
    public class UDPWorker
    {
        public int port = 11001;
        public async void Start()
        {
            string outside = "";

            UdpClient client = new UdpClient(port);
            Console.WriteLine("UDP Receiver startet på port " + port);
            IPEndPoint remote = new IPEndPoint(IPAddress.Any, 0);
            using (HttpClient c = new HttpClient())
            {
                while (true)
                {
                    byte[] bytes = client.Receive(ref remote);
                    string s = Encoding.UTF8.GetString(bytes);
                    

                    if (s.StartsWith("o"))
                    {
                        outside = s.Substring(1);
                        Console.WriteLine("Det outside: " + outside);
                    }
                    else
                    {
                        Console.WriteLine(s);

                        //Get latest active status
                        List<Logging> loggingActive = new List<Logging>();
                        string loggingActiveStr = c.GetStringAsync("http://localhost:50850/api/Logging").Result;
                        loggingActive = JsonConvert.DeserializeObject<List<Logging>>(loggingActiveStr);
                        bool loggingActiveBool = loggingActive.Last().Aktiv;

                        //New logging
                        Logging log = new Logging();
                        log.Luftfugtighed = Convert.ToDouble(s, CultureInfo.InvariantCulture);
                        log.Dato = DateTime.Now;
                        log.Aktiv = loggingActiveBool; //Latest aktiv status

                        //Get current status in case of manual overwrite
                        Status status = new Status();
                        string allowChange = c.GetStringAsync("http://localhost:50850/api/Status").Result;
                        status = JsonConvert.DeserializeObject<Status>(allowChange);

                        if (Convert.ToDouble(s, CultureInfo.InvariantCulture) >= Convert.ToDouble(outside, CultureInfo.InvariantCulture))
                        {
                            if (status.AllowChange) log.Aktiv = true;
                        }
                        else
                        {
                            if(status.AllowChange) log.Aktiv = false;
                        }
                        string msg = JsonConvert.SerializeObject(log);

                        StringContent content = new StringContent(msg, Encoding.UTF8, "application/json");

                        await c.PostAsync("http://localhost:50850/api/Logging", content);

                    }
                }
            }
        }
    }
}