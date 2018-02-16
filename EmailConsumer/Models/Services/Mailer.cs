using System;
using System.Globalization;
using System.IO;
using EmailConsumer.Models.Entities;
using MailKit.Net.Smtp;
using MimeKit;

namespace EmailConsumer.Models.Services
{
    public class Mailer
    {
        public bool SendImageRejectionEmail(string path, AppUser appUser)
        {
            bool success = false;
            //From Address
            var FromAddress = "support@camerack.com";
            var FromAdressTitle = "Camerack Studio";
            //To Address
            var toVendor = appUser.Email;
            //var toCustomer = email;
            var ToAdressTitle = "Camerack";
            var Subject = "Camerack Image Rejection";
            //var BodyContent = message;

            //Smtp Server
            var smtpServer = new AppConfig().EmailServer;
            //Smtp Port Number
            var smtpPortNumber = new AppConfig().Port;

            var mimeMessageVendor = new MimeMessage();
            mimeMessageVendor.From.Add(new MailboxAddress(FromAdressTitle, FromAddress));
            mimeMessageVendor.To.Add(new MailboxAddress(ToAdressTitle, toVendor));
            mimeMessageVendor.Subject = Subject;
            BodyBuilder bodyBuilder = new BodyBuilder();
            using (var data = File.OpenText(path))
            {
                if (data.BaseStream != null)
                {
                    //manage content

                    bodyBuilder.HtmlBody = data.ReadToEnd();
                    var body = bodyBuilder.HtmlBody;

                    var replace = body.Replace("IMAGE", appUser.Address);
                    replace = replace.Replace("DATE", appUser.DateCreated.ToString(CultureInfo.InvariantCulture));
                    replace = replace.Replace("REASON", appUser.Biography);
                    replace = replace.Replace("NAME", appUser.Name);
                    bodyBuilder.HtmlBody = replace;
                    mimeMessageVendor.Body = bodyBuilder.ToMessageBody();
                }
            }
            using (var client = new SmtpClient())
            {
                client.Connect(smtpServer, smtpPortNumber);
                // Note: only needed if the SMTP server requires authentication
                // Error 5.5.1 Authentication 
                client.Authenticate(new AppConfig().Email, new AppConfig().Password);
                client.Send(mimeMessageVendor);
                if (client.IsConnected)
                {
                    success = true;
                }
                client.Disconnect(true);
            }
            return success;
        }
        public bool SendNewSocialUserEmail(string path, AppUser appUser)
        {
            bool success = false;
            //From Address
            var FromAddress = "support@camerack.com";
            var FromAdressTitle = "Camerack Studio";
            //To Address
            var toVendor = appUser.Email;
            //var toCustomer = email;
            var ToAdressTitle = "Camerack";
            var Subject = "Welcome to Camerack.";
            //var BodyContent = message;

            //Smtp Server
            var smtpServer = new AppConfig().EmailServer;
            //Smtp Port Number
            var smtpPortNumber = new AppConfig().Port;

            var mimeMessageVendor = new MimeMessage();
            mimeMessageVendor.From.Add(new MailboxAddress(FromAdressTitle, FromAddress));
            mimeMessageVendor.To.Add(new MailboxAddress(ToAdressTitle, toVendor));
            mimeMessageVendor.Subject = Subject;
            BodyBuilder bodyBuilder = new BodyBuilder();
            using (var data = File.OpenText(path))
            {
                if (data.BaseStream != null)
                {
                    //manage content

                    bodyBuilder.HtmlBody = data.ReadToEnd();
                    var body = bodyBuilder.HtmlBody;

                    var replace = body.Replace("NAME", appUser.Name);
                    replace = replace.Replace("URL", new AppConfig().MarketPlaceBaseUrl);
                    replace = replace.Replace("SOCIALMEDIA", appUser.AccountType);
                    replace = replace.Replace("DATE", DateTime.Now.ToString(CultureInfo.InvariantCulture));
                    bodyBuilder.HtmlBody = replace;
                    mimeMessageVendor.Body = bodyBuilder.ToMessageBody();
                }
            }
            using (var client = new SmtpClient())
            {
                client.Connect(smtpServer, smtpPortNumber);
                // Note: only needed if the SMTP server requires authentication
                // Error 5.5.1 Authentication 
                client.Authenticate(new AppConfig().Email, new AppConfig().Password);
                client.Send(mimeMessageVendor);
                if (client.IsConnected)
                {
                    success = true;
                }
                client.Disconnect(true);
            }
            return success;
        }
        public bool SendNewUserEmail(string path, AppUser appUser,string role,AppUserAccessKey accessKey)
        {
            bool success = false;
            //From Address
            var FromAddress = "support@camerack.com";
            var FromAdressTitle = "Camerack Studio";
            //To Address
            var toVendor = appUser.Email;
            //var toCustomer = email;
            var ToAdressTitle = "Camerack Studio";
            var Subject = "Activate Account.";
            //var BodyContent = message;

            //Smtp Server
            var smtpServer = new AppConfig().EmailServer;
            //Smtp Port Number
            var smtpPortNumber = new AppConfig().Port;

            var mimeMessageVendor = new MimeMessage();
            mimeMessageVendor.From.Add(new MailboxAddress(FromAdressTitle, FromAddress));
            mimeMessageVendor.To.Add(new MailboxAddress(ToAdressTitle, toVendor));
            mimeMessageVendor.Subject = Subject;
            BodyBuilder bodyBuilder = new BodyBuilder();
            using (var data = File.OpenText(path))
            {
                if (data.BaseStream != null)
                {
                    //manage content

                    bodyBuilder.HtmlBody = data.ReadToEnd();
                    var body = bodyBuilder.HtmlBody;

                    var replace = body.Replace("NAME", appUser.Name);
                    replace = replace.Replace("URL", new AppConfig().MarketPlaceBaseUrl+"Account/AccountActivationLink?accessCode=" + accessKey.AccountActivationAccessCode);
                    replace = replace.Replace("ROLE", role);
                    replace = replace.Replace("DATE", DateTime.Now.ToString(CultureInfo.InvariantCulture));
                    bodyBuilder.HtmlBody = replace;
                    mimeMessageVendor.Body = bodyBuilder.ToMessageBody();
                }
            }
            using (var client = new SmtpClient())
            {
                client.Connect(smtpServer, smtpPortNumber);
                // Note: only needed if the SMTP server requires authentication
                // Error 5.5.1 Authentication 
                client.Authenticate(new AppConfig().Email, new AppConfig().Password);
                client.Send(mimeMessageVendor);
                if (client.IsConnected)
                {
                    success = true;
                }
                client.Disconnect(true);
            }
            return success;
        }
        public bool SendForgotPasswordResetLink(string path, AppUser appUser,AppUserAccessKey accessKey)
        {
            bool sucess = false;
            //From Address
            var FromAddress = "support@camerack.com";
            var FromAdressTitle = "Camerack Studio";
            //To Address
            var toVendor = appUser.Email;
            //var toCustomer = email;
            var ToAdressTitle = "Camerack Studio";
            var Subject = "Password Reset.";
            //var BodyContent = message;

            //Smtp Server
            var smtpServer = new AppConfig().EmailServer;
            //Smtp Port Number
            var smtpPortNumber = new AppConfig().Port;

            var mimeMessageVendor = new MimeMessage();
            mimeMessageVendor.From.Add(new MailboxAddress(FromAdressTitle, FromAddress));
            mimeMessageVendor.To.Add(new MailboxAddress(ToAdressTitle, toVendor));
            mimeMessageVendor.Subject = Subject;
            BodyBuilder bodyBuilder = new BodyBuilder();
            using (StreamReader data = File.OpenText(path))
            {
                if (data.BaseStream != null)
                {
                    //manage content

                    bodyBuilder.HtmlBody = data.ReadToEnd();
                    var body = bodyBuilder.HtmlBody;

                    var replace = body.Replace("NAME", appUser.Name);
                    replace = replace.Replace("DATE", DateTime.Now.ToString(CultureInfo.InvariantCulture));
                    replace = replace.Replace("URL", new AppConfig().MarketPlaceBaseUrl + "Account/ForgotPassword?accessCode=" + accessKey.PasswordAccessCode);
                    bodyBuilder.HtmlBody = replace;
                    mimeMessageVendor.Body = bodyBuilder.ToMessageBody();
                }
            }
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect(smtpServer, smtpPortNumber, true);
                // Note: only needed if the SMTP server requires authentication
                // Error 5.5.1 Authentication 
                client.Authenticate(new AppConfig().Email, new AppConfig().Password);
                client.Send(mimeMessageVendor);
                sucess = true;
                client.Disconnect(true);
            }
            return sucess;
        }
    }
}