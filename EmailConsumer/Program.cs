using System;
using System.Text;
using EmailConsumer.Models;
using EmailConsumer.Models.Entities;
using EmailConsumer.Models.Services;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace EmailConsumer
{
    public class Program
    {
        public static void Main(string[] args)
        {

            AppUserTransport appUserTransport = null;
            var factory = new ConnectionFactory() {HostName = "localhost"};
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "EmailTaskManager",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    //check if message is available
                    if (!string.IsNullOrEmpty(message))
                    {
                        appUserTransport = JsonConvert.DeserializeObject<AppUserTransport>(message);

                        if (appUserTransport != null)
                        {
                            if (appUserTransport.RequestType == "UserRegistration")
                            {
                                //send user email
                                var mailer = new Mailer();
                                var success = mailer.SendNewUserEmail(new AppConfig().NewUserHtml,
                                    appUserTransport.AppUser,
                                    appUserTransport.Role,
                                    appUserTransport.AppUserAccessKey);
                                if (success)
                                {
                                    Console.WriteLine(JsonConvert.SerializeObject(appUserTransport.AppUser));
                                }
                            }
                            if (appUserTransport.RequestType == "ImageRejection")
                            {
                                //send user email
                                var mailer = new Mailer();
                                var success = mailer.SendImageRejectionEmail(new AppConfig().ImageRejectionHtml,
                                    appUserTransport.AppUser);
                                if (success)
                                {
                                    Console.WriteLine(JsonConvert.SerializeObject(appUserTransport.AppUser));
                                }
                            }
                            if (appUserTransport.RequestType == "ForgotPassword")
                            {
                                //send user email
                                var mailer = new Mailer();
                                var success = mailer.SendForgotPasswordResetLink(new AppConfig().ForgotPasswordHtml,
                                    appUserTransport.AppUser,
                                    appUserTransport.AppUserAccessKey);
                                if (success)
                                {
                                    Console.WriteLine(JsonConvert.SerializeObject(appUserTransport.AppUser));
                                }
                            }
                            else
                            {
                                var appUser = JsonConvert.DeserializeObject<AppUser>(message);
                                //send user email
                                var mailer = new Mailer();
                                var success = mailer.SendNewSocialUserEmail(new AppConfig().NewUserSocialHtml,
                                    appUser);
                                if (success)
                                {
                                    Console.WriteLine(JsonConvert.SerializeObject(appUser));
                                }
                            }

                        }
                        Console.WriteLine(" [x] Received {0}", message);

                    }
             

           

                };
                channel.BasicConsume(queue: "EmailTaskManager",
                    autoAck: true,
                    consumer: consumer);
                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
