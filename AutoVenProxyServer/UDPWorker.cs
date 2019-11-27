using System;
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
            UdpClient client = new UdpClient(port);
            Console.WriteLine("UDP Receiver startet på port " + port);
            IPEndPoint remote = new IPEndPoint(IPAddress.Any, 0);
            using (HttpClient c = new HttpClient())
            {
                while (true)
                {
                    byte[] bytes = client.Receive(ref remote);
                    string s = Encoding.UTF8.GetString(bytes);

                    Logging log = new Logging();
                    log.Luftfugtighed = Convert.ToDouble(s);
                    log.Dato = DateTime.Now;

                    string msg = JsonConvert.SerializeObject(log);

                    StringContent content = new StringContent(msg, Encoding.UTF8, "application/json");

                    await c.PostAsync("http://localhost:50850/api/Logging", content);

                    Console.WriteLine(s);
                }
            }
        }
    }
}