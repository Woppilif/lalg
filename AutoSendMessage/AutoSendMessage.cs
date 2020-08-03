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
        
        public static void Main(string[] args)
        {
            
        }
        public void Start()
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
                //.AddSingleton<Notifications, Notifications>()
                .BuildServiceProvider();
        }

        public class Message
        {
            private List<Lesson> lessonsAll = null;
            private List<PatternMessage> patternMessages = null;

            private SchoolBot.Notifications.Notification _notification;     //TODO ????откуда их взять????
            private TelegramClient _client;
            private BotVk _botVk;
            public void Run()
            {
                patternMessages = new List<PatternMessage>();
                lessonsAll = new List<Lesson>();
                Console.WriteLine($"Notification service started {DateTime.Now.TimeOfDay} 1.1");
                LoadLessons();

                //if (DateTime.Now.Second % 5 == 0 && lessonsAll.Count() != CheckCount())
                //{
                //    Console.WriteLine($"{lessonsAll.Count()} {CheckCount()}");
                //    LoadLessons();
                //    Thread.Sleep(1000);
                //}
                foreach (var msg in patternMessages)
                {
                    var now = DateTime.Now.TimeOfDay;
                    if (now >= msg.AtTime.TimeOfDay && msg.Status == false)
                    {
                        var lesson = lessonsAll.Where(les => les.PatternId == msg.PatternId).FirstOrDefault();
                        if (lesson == null)
                        {
                            continue;
                        }
                        string link = $"https://langalgorithm.ru/api/LinkSpyers/{lesson.LessonId}/<UserId>";
                        var message = msg.MakeMessage(link, lesson.LessonAt);
                        //new Sender().Send(message, lesson.Group);
                        SendMessage(message, lesson.GroupId);
                        msg.Status = true;
                    }
                }

            }

            private void SendMessage(string message, Guid group)
            {
                //throw new NotImplementedException();
                _notification._client = _client;
                _notification.Send(message, group);

                _notification._client = _botVk;
                _notification.Send(message, group);
                Console.WriteLine("Sending messages");
            }
            public List<User> LoadUser(int platform, Guid group)//Загрузка юзеров по платформе и группе
            {
                //throw new NotImplementedException();
                using (var db = new Connect(_client.ConnectionString).DBConnection())
                {
                    return db.Users.Where(s => s.GroupId == group &&
                    s.Platform == platform &&
                    s.Registered == true).ToList();
                }
            }
            public List<PatternMessage> LoadMessage(Guid patternid)//загрузка Сообщений
            {
                //throw new NotImplementedException();
                using (var db = new Connect(_client.ConnectionString).DBConnection())
                {
                    return db.PatternMessages.Where(s => s.PatternId == patternid &&
                    s.Status == true &&
                    s.AtTime >= DateTime.Now).ToList();
                }
            }

            private void LoadLessons()
            {
                patternMessages.Clear();
                lessonsAll.Clear();
                using (var db = new Connect(_client.ConnectionString).DBConnection())
                {
                    var lessons = db.Lessons.Where(lesson => lesson.Status == true &&
                    (lesson.LessonAt.Date >= DateTime.Now.Date || lesson.IsRepeats == true && lesson.LessonAt.DayOfWeek == DateTime.Now.DayOfWeek)).OrderBy(lesson => lesson.LessonAt).ToList();
                    foreach (var lesson in lessons)
                    {
                        lessonsAll.Add(lesson);
                        var messages = db.PatternMessages.Where(pt => pt.PatternId == lesson.PatternId).ToList();
                        foreach (var item in messages)
                        {
                            if (item.AtTime.TimeOfDay < DateTime.Now.TimeOfDay)
                            {
                                item.Status = true;
                            }
                            else
                            {
                                item.Status = false;
                            }
                            Console.WriteLine($"will send  at : {item.AtTime}");
                            patternMessages.Add(item);
                        }

                    }
                }
                Console.WriteLine($"{patternMessages.Count} messages to send loaded");
            }
        }

        
    }
}
