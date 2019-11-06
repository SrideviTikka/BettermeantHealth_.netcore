using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using BettermeantHealth.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BettermeantHealth.Controllers
{
    public abstract class BaseController : Controller
    {
        #region Send Mail
        /// <summary>
        /// Author: Sridevi
        /// Date: 18-10-2019
        /// Description: This region is Used for sending mails.
        /// </summary>
        /// <returns></returns>
        /// 
      private static IConfiguration _Iconfiguration;
        public BaseController(IConfiguration config)
        {
            _Iconfiguration = config;            
        }

        internal string SendMail(EmailAttributesModel mailitem)
        {
            
            string strMail = string.Empty;
            try
            {
                SmtpClient smtpClient = new SmtpClient(AppConfig.SMTPServerName, Convert.ToInt32(AppConfig.SMTPServerPort));
                smtpClient.Credentials = new NetworkCredential(AppConfig.SMTPEMAILFROM, AppConfig.SMTPEmailFromPassword);
                smtpClient.EnableSsl = true;
                MailMessage message = null;
                message = new MailMessage(mailitem.From, mailitem.To);
                message.To.Add(mailitem.To);
                message.Subject = mailitem.Subject;
                message.Body = mailitem.MessageBody;
                message.IsBodyHtml = true;
                message.BodyEncoding = UTF8Encoding.UTF8;
                message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                smtpClient.Send(message);
                strMail = "1";
            }
            catch (Exception exception)
            {
                strMail = exception.Message;
            }

            return strMail;
        }

        #endregion

        #region AppConfig

        public static class AppConfig
        {
            internal static string SMTPServerName
            {                
                get
                {
                    return _Iconfiguration["SMTP_SERVERNAME"];                   
                }
            }

            internal static string SMTPServerPort
            {
                get
                {
                    return _Iconfiguration["SMTP_SERVERPORT"];
                }
            }
            internal static string SMTPEMAILFROM
            {
                get
                {
                    return _Iconfiguration["SMTP_EMAILFROM"];
                }
            }
            internal static string SMTPEmailFromPassword
            {
                get
                {
                    return _Iconfiguration["SMTP_EMAILFROMPASSWORD"];
                }
            }
            internal static string HostName
            {
                get
                {
                    return _Iconfiguration["HOST_NAME"];
                }
            }
        }
        #endregion
    }
}