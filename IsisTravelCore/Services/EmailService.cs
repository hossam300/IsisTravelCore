using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace IsisTravelCore.Services
{
    public class EmailService : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            MailAddress fromAddress = new MailAddress("reservation@alasdeisis.com", "alas de Isis");
            //MailAddress toAddress = new MailAddress(ConfigurationManager.AppSettings["ToMail"]);
            string BodyToSend = htmlMessage;
            string ToMail = email;
            //Create the MailMessage instance 
            //MailMessage myMailMessage = new MailMessage(fromAddress, toAddress);
            MailMessage myMailMessage = new MailMessage
            {
                From = fromAddress
            };

            string[] ToMuliArr = ToMail.Split(',');
            foreach (string ToEMail in ToMuliArr)
            {
                myMailMessage.To.Add(new MailAddress(ToEMail)); //adding multiple TO Email Id
            }

            //Assign the MailMessage's properties 
            myMailMessage.Subject = subject;
            myMailMessage.Body = BodyToSend;
            myMailMessage.IsBodyHtml = true;
            myMailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            myMailMessage.SubjectEncoding = System.Text.Encoding.UTF8;

            //Create the SmtpClient object
            SmtpClient smtp = new SmtpClient
            {
                Host = "mail.alasdeisis.com",
                Port = 2525,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("reservation@alasdeisis.com", "Nuria@013"),
                EnableSsl = false
            };
            try
            {
                smtp.Send(myMailMessage);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                // Logger.WriteError(ex.ToString());
                return Task.CompletedTask;
            }



        }
        public Task SendEmailAsync(string name, string email, string subject, string htmlMessage)
        {
            MailAddress fromAddress = new MailAddress(email, name);
            //MailAddress toAddress = new MailAddress(ConfigurationManager.AppSettings["ToMail"]);
            string BodyToSend = htmlMessage;
            string ToMail = "semsem9000@gmail.com";
            //Create the MailMessage instance 
            //MailMessage myMailMessage = new MailMessage(fromAddress, toAddress);
            MailMessage myMailMessage = new MailMessage
            {
                From = fromAddress
            };

            string[] ToMuliArr = ToMail.Split(',');
            foreach (string ToEMail in ToMuliArr)
            {
                myMailMessage.To.Add(new MailAddress(ToEMail)); //adding multiple TO Email Id
            }

            //Assign the MailMessage's properties 
            myMailMessage.Subject = subject;
            myMailMessage.Body = BodyToSend;
            myMailMessage.IsBodyHtml = true;
            myMailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            myMailMessage.SubjectEncoding = System.Text.Encoding.UTF8;

            //Create the SmtpClient object
            SmtpClient smtp = new SmtpClient
            {
                Host = "mail.alasdeisis.com",
                Port = 2525,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("reservation@alasdeisis.com", "Nuria@013"),
                EnableSsl = false
            };
            try
            {
                smtp.Send(myMailMessage);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                // Logger.WriteError(ex.ToString());
                return Task.CompletedTask;
            }
        }
        public Task SendEmailAsync(string name,string email, string subject,  string Attachments, string htmlMessage)
        {
            MailAddress fromAddress = new MailAddress(email, name);
            //MailAddress toAddress = new MailAddress(ConfigurationManager.AppSettings["ToMail"]);
            string BodyToSend = htmlMessage;
            string ToMail = "semsem9000@gmail.com";
            //Create the MailMessage instance 
            //MailMessage myMailMessage = new MailMessage(fromAddress, toAddress);
            MailMessage myMailMessage = new MailMessage
            {
                From = fromAddress
            };

            string[] ToMuliArr = ToMail.Split(',');
            foreach (string ToEMail in ToMuliArr)
            {
                myMailMessage.To.Add(new MailAddress(ToEMail)); //adding multiple TO Email Id
            }
            if (Attachments != "" && Attachments != null || Attachments != string.Empty)
            {
                myMailMessage.Attachments.Add(new Attachment(Attachments));
            }
            //Assign the MailMessage's properties 
            myMailMessage.Subject = subject;
            myMailMessage.Body = BodyToSend;
            myMailMessage.IsBodyHtml = true;
            myMailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            myMailMessage.SubjectEncoding = System.Text.Encoding.UTF8;

            //Create the SmtpClient object
            SmtpClient smtp = new SmtpClient
            {
                Host = "mail.alasdeisis.com",
                Port = 2525,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("reservation@alasdeisis.com", "Nuria@013"),
                EnableSsl = false
            };
            try
            {
                smtp.Send(myMailMessage);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                // Logger.WriteError(ex.ToString());
                return Task.CompletedTask;
            }
           
        }
    }
}
