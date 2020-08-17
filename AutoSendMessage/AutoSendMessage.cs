using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using BotAppData.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolBot.Data;
using TelegramBot;
using VkBot;
using System.Timers;
using System.Net;
using Newtonsoft.Json;
namespace AutoSendMessage
{
    public class AutoSendMessage
    {
        private static SchoolBot.Notifications.Notification _notification = new SchoolBot.Notifications.Notification();     //TODO ????откуда их взять????
        private static TelegramClient _client;
        private static BotVk _botVk;
        public static void Main(string[] args)
        {
            
        }
        public static void Start()
        {
            var builder = new ConfigurationBuilder()
                     .SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("appsettings.json");//TODO прикрепи сюда appsettings из lalg
            var config = builder.Build();

            var serviceProvider = new ServiceCollection()
                .AddSingleton(bot =>
                {
                    TelegramClient telegramClient = new TelegramClient(config.GetConnectionString("DefaultConnection")
                        , config.GetSection("BotsTokens").GetSection("Telegram").Value);
                    return telegramClient;
                })
                .AddSingleton(bot =>
                {
                    BotVk botVk = new BotVk(config.GetConnectionString("DefaultConnection"),
                        config.GetSection("BotsTokens").GetSection("Vk").Value);
                    return botVk;
                })
                .AddSingleton<SchoolBot.Notifications.Notification, SchoolBot.Notifications.Notification>()
                //.AddSingleton<Message, Message>()//TODO может это надо добавить?
                .BuildServiceProvider();
            //serviceProvider.GetService<Message>().Run();//TODO может это надо добавить? Хотя не работает
            _client = new TelegramClient(config.GetConnectionString("DefaultConnection")
                        , config.GetSection("BotsTokens").GetSection("Telegram").Value);
            _botVk = new BotVk(config.GetConnectionString("DefaultConnection"),
                        config.GetSection("BotsTokens").GetSection("Vk").Value);
        }

        public class Message
        {
            private List<Lesson> lessonsAll = null;
            private List<PatternMessage> patternMessages = null;
            private List<User> users = null;
            
            public void Run()
            {
                patternMessages = new List<PatternMessage>();
                lessonsAll = new List<Lesson>();
                //users = new List<User>();
                Console.WriteLine($"Check Message in {DateTime.Now.TimeOfDay}");
                lessonsAll = LoadLessons();

                using var db = new Connect(_client.ConnectionString).DBConnection();
                foreach (var lesson in lessonsAll)
                {
                    patternMessages = LoadMessage(lesson.PatternId);
                    //users = MyLoadUser(lesson.GroupId);
                    foreach (var item in patternMessages)
                    {
                        var mess = item.Message.Replace("<url>", lesson.Url);
                        mess.Replace("<datetime>", lesson.LessonAt.ToString("H:mm"));
                        SendMessage(mess, lesson.GroupId);
                        item.Status = false;
                        db.PatternMessages.Update(item);
                    }
                    db.SaveChanges();
                }
                //Вот это отправка сообщения определенному пользователю определенное сообщение
                //_client.SendTextMessage(610212420, "Ты добрадся до AutoSend");               
            }
            private List<User> LoadUser(Guid groupid)
            {
                
                //throw new NotImplementedException();
                using (var db = new Connect(_client.ConnectionString).DBConnection())
                {
                    return db.Users.Where(s => s.GroupId == groupid &&
                    s.Registered == true &&
                    s.IsTeacher == false).ToList();
                }
               
            }
            private List<PatternMessage> LoadMessage(Guid patternId)
            {
                using (var db = new Connect(_client.ConnectionString).DBConnection())
                {
                    return db.PatternMessages.Where(s => s.PatternId == patternId &&
                    s.AtTime <= DateTime.Now &&
                    s.Status == true &&
                    s.IsGreeting == false).ToList();
                }
            }
            private List<Lesson> LoadLessons()
            {
                using (var db = new Connect(_client.ConnectionString).DBConnection())
                {
                    return db.Lessons.Where(s => s.Status == true &&
                    (s.LessonAt >= DateTime.Now || (s.IsRepeats ==true && s.LessonAt.DayOfWeek == DateTime.Now.DayOfWeek))).ToList();
                }
            }

            private void SendMessage(string message, Guid group)
            {
                _notification._client = _client;
                _notification.Send(message, group);

                _notification._client = _botVk;
                _notification.Send(message, group);
                Console.WriteLine("Sending messages");
            }          
        }        
    }
}
