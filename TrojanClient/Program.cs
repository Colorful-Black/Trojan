using System;
using System.Net;

namespace TrojanClient
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress serverIp = new IPAddress(new byte[] { 192, 168, 1, 108 });
            if (args.Length > 0)
                IPAddress.TryParse(args[0], out serverIp);

            Client c = new Client(serverIp);
            c.recv();

            Console.WriteLine("文件接收完成...");
            //Console.ReadKey();
        }        
    }
}
