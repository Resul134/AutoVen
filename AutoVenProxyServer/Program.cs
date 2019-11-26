using System;

namespace AutoVenProxyServer
{
    class Program
    {
        static void Main(string[] args)
        {
            UDPWorker worker = new UDPWorker();
            worker.Start();
        }
    }
}
