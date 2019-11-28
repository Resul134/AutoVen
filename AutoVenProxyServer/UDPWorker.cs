using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
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
                        Logging log = new Logging();
                        log.Luftfugtighed = Convert.ToDouble(s, CultureInfo.InvariantCulture);
                        log.Dato = DateTime.Now;

                        

                        if (Convert.ToDouble(s, CultureInfo.InvariantCulture) >= Convert.ToDouble(outside, CultureInfo.InvariantCulture))
                        {
                            //Skal checkes om den må ændres
                            log.Aktiv = true;

                            string msg = JsonConvert.SerializeObject(log);

                            StringContent content = new StringContent(msg, Encoding.UTF8, "application/json");

                            await c.PostAsync("http://localhost:50850/api/Logging", content);

                            Console.WriteLine(s);

                            //Status status = new Status();
                            //status.Id = 1;
                            //status.Dato = DateTime.Now;
                            //status.AllowChange = true;
                            //string msg2 = JsonConvert.SerializeObject(status);

                            //StringContent content2 = new StringContent(msg2, Encoding.UTF8, "application/json");

                            //await c.PutAsync("http://localhost:50850/api/Status/1", content2);
                        }
                        else
                        {
                            //Skal checkes om den må ændres
                            log.Aktiv = false;

                            string msg = JsonConvert.SerializeObject(log);

                            StringContent content = new StringContent(msg, Encoding.UTF8, "application/json");

                            await c.PostAsync("http://localhost:50850/api/Logging", content);

                            Console.WriteLine(s);
                            //Status status = new Status();
                            //status.Id = 1;
                            //status.Dato = DateTime.Now;
                            //status.AllowChange = false;
                            //string msg = JsonConvert.SerializeObject(status);

                            //StringContent content = new StringContent(msg, Encoding.UTF8, "application/json");

                            //await c.PutAsync("http://localhost:50850/api/Status/1", content);
                        }

                    }
                }
            }
        }
    }
}