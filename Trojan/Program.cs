using System;
using System.Net.Sockets;

namespace TrojanService
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener server = new TcpListener(6000);
            server.Start();

            while (true)
            {

                ServerToClient c = new ServerToClient(server.AcceptTcpClient());
                Console.WriteLine("新的连接");
                //防止传输过程中客户端掉线
                try
                {
                    //c.send();
                    c.sendAdvance();
                    Console.WriteLine("发送完成");
                    c.close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("客户端已离线:" + ex.Message);
                }
                server.Stop();
                Console.ReadKey();

            }
        }

    }
}
