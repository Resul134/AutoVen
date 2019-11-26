using System;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;

namespace AutoVenProxyServer
{
    public class UDPWorker
    {
        public int port = 7000;
        public async void Start()
        {
            UdpClient client = new UdpClient(port);
            Console.WriteLine("UDP Receiver startet på port " + port);
            IPEndPoint remote = new IPEndPoint(IPAddress.Any, port);
            using (HttpClient c = new HttpClient())
            {
                while (true)
                {
                    byte[] bytes = client.Receive(ref remote);
                    string s = Encoding.UTF8.GetString(bytes);
                    
                    Console.WriteLine(s);
                }
            }
        }
    }
}