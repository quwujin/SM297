using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mail;
using System.Web;
using System.IO;

namespace Common.Email
{
    public class EmailTool
    {
        public string Address_From;
        public string Address_FromName;
        public string Address_To;
        public string Subject;
        public string BodyText;
        public bool HtmlFormat;
        public string SMTPServer;
        public string Bcc;
        public string CC;
        public List<string> AttachFiles;
        public string ReplyTo;
        public string ReplyToName;

        System.Net.Mail.MailMessage TheMail;

        public EmailTool()
        {
            TheMail = new System.Net.Mail.MailMessage();
            AttachFiles = new List<string>();

        }

        public static void sendEmail(string address_To, string subject, string bodyText, string file )
        {  
              sendEmail( address_To,  subject, bodyText, file, false) ;
        }
        public static void sendEmail(string address_To, string subject,string bodyText,string file,bool ishtml) 
        {
            Common.Email.EmailTool email = new Common.Email.EmailTool();
            email.Address_From = "report@iseedling.com";
            email.Address_To = address_To;
            email.Subject = subject;
            email.BodyText = bodyText;
            email.HtmlFormat = ishtml;
            //用来设置附件
            if (!string.IsNullOrEmpty(file)) { email.AttachFiles.Add(file); }
            email.SMTPServer = "smtp.exmail.qq.com";
            var result = email.Send();
        }




        public string Send()
        {
            TheMail.From = new System.Net.Mail.MailAddress(Address_From, Address_FromName);

            if (string.IsNullOrEmpty(Address_To) == true)
            {
                return "";
            }

            string[] _addresses = Address_To.Split(';');

            foreach (string _item in _addresses)
            {
                if (string.IsNullOrEmpty(_item) == false)
                {

                    if (IsEmail(_item) == false)
                    {
                        return "the email address : " + _item + " is invalid";
                    }
                    TheMail.To.Add(_item);
                }
            }

            if (string.IsNullOrEmpty(this.CC) == false)
            {
                _addresses = this.CC.Split(';');
                foreach (string _item in _addresses)
                {
                    if (string.IsNullOrEmpty(_item) == false)
                    {
                        if (IsEmail(_item) == false)
                        {
                            return "the email address : " + _item + " is invalid";
                        }
                        TheMail.CC.Add(_item);
                    }
                }
            }





            if (string.IsNullOrEmpty(this.Bcc) == false)
            {
                _addresses = this.Bcc.Split(';');
                foreach (string _item in _addresses)
                {
                    if (string.IsNullOrEmpty(_item) == false)
                    {
                        if (IsEmail(_item) == false)
                        {
                            return "the email address : " + _item + " is invalid";
                        }
                        TheMail.Bcc.Add(_item);
                    }
                }
            }

            if (string.IsNullOrEmpty(this.ReplyTo) == false)
            {
                TheMail.ReplyTo = new System.Net.Mail.MailAddress(this.ReplyTo, this.ReplyToName);
            }
            foreach (string _filename in AttachFiles)
            {
                TheMail.Attachments.Add(new System.Net.Mail.Attachment(_filename));
            }

            TheMail.Subject = Subject;
            TheMail.Body = BodyText;

            TheMail.IsBodyHtml = HtmlFormat;
            TheMail.BodyEncoding = Encoding.UTF8;
            TheMail.Priority = System.Net.Mail.MailPriority.Normal;

            string _result = "";

            try
            {
                System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient();
                if (SMTPServer != "")
                {
                    smtpClient.Host = SMTPServer;
                }

                //string smtpUserName = System.Configuration.ConfigurationManager.AppSettings["SmtpUserName"];
                //string SmtpPassword = System.Configuration.ConfigurationManager.AppSettings["SmtpPassword"];

                string smtpUserName = "report@iseedling.com";
                string SmtpPassword = "yh9012309";

                if (string.IsNullOrEmpty(smtpUserName) == false && string.IsNullOrEmpty(SmtpPassword) == false)
                {
                    smtpClient.Credentials = new System.Net.NetworkCredential(smtpUserName, SmtpPassword);
                }
                smtpClient.EnableSsl = true;
                smtpClient.Port = 587;

                smtpClient.Send(TheMail);
            }
            catch (Exception ex)
            {

                _result = ex.Message + "--" + ex.InnerException + "--" + ex.StackTrace;
            }

            return _result;

        }


        private bool IsEmail(string source)
        {
            return Regex.IsMatch(source, @"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@" +
            @"([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$", RegexOptions.IgnoreCase);
        }

    }
}
