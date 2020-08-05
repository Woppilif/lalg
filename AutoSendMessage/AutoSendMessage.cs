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
                users = new List<User>();
                Console.WriteLine($"Check Message in {DateTime.Now.TimeOfDay}");
                //LoadLessons();
                //patternMessages = LoadMessage();//Почему то не работает
                lessonsAll = MyLoadLessons();

                foreach (var lesson in lessonsAll)
                {
                    var db = new Connect(_client.ConnectionString).DBConnection();
                    patternMessages = LoadMessage(lesson.PatternId);
                    //users = MyLoadUser(lesson.GroupId);
                    foreach (var item in patternMessages)
                    {
                        SendMessage(item.Message, lesson.GroupId);
                        item.Status = false;
                        db.PatternMessages.Update(item);
                    }
                    db.SaveChanges();
                }

                //Вот это отправка сообщения определенному пользователю определенное сообщение
                //_client.SendTextMessage(610212420, "Ты добрадся до AutoSend");

                //foreach (var msg in patternMessages)
                //{
                //    var now = DateTime.Now.TimeOfDay;
                //    if (/*now >= msg.AtTime.TimeOfDay &&*/ msg.Status == false)
                //    {
                //        var lesson = lessonsAll.Where(les => les.PatternId == msg.PatternId).FirstOrDefault();
                //        if (lesson == null)
                //        {
                //            continue;
                //        }
                //        string link = $"https://langalgorithm.ru/api/LinkSpyers/{lesson.LessonId}/<UserId>";
                //        var message = msg.MakeMessage(link, lesson.LessonAt);
                //        //new Sender().Send(message, lesson.Group);
                //        SendMessage(message, lesson.GroupId);
                //        msg.Status = true;
                //    }
                //}

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
                    s.Status == true).ToList();
                }
            }
            private List<Lesson> MyLoadLessons()
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
