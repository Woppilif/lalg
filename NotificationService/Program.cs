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

namespace NotificationService
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                     .SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("appsettings.json");
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
                .AddSingleton<Notifications, Notifications>()
                .BuildServiceProvider();

            serviceProvider.GetService<Notifications>().Run();
            //SetTimer();
            //Console.ReadKey();
        }

        private static void SetTimer()
        {
            System.Timers.Timer aTimer = new System.Timers.Timer(3000);
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            List<Lesson> lessons = JsonConvert.DeserializeObject<List<Lesson>>(LoadLessons());
            foreach (var lesson in lessons)
            {
                if (lesson.LessonAt <= DateTime.Now || (lesson.IsRepeats == true && lesson.LessonAt <= DateTime.Now))
                {
                    Console.WriteLine(lesson.LessonAt);
                }
            }
            Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",
                              e.SignalTime);
        }

        public class Notifications
        {
            private List<Lesson> lessonsAll = null;
            private List<PatternMessage> patternMessages = null;

            private SchoolBot.Notifications.Notification _notification;
            private TelegramClient _client;
            private BotVk _botVk;

            public Notifications(TelegramClient client, BotVk botVk,
                SchoolBot.Notifications.Notification notification)
            {
                _client = client;
                _botVk = botVk;
                _notification = notification;
            }

            public void Run()
            {
                patternMessages = new List<PatternMessage>();
                lessonsAll = new List<Lesson>();
                Console.WriteLine($"Notification service started {DateTime.Now.TimeOfDay} 1.1");
                LoadLessons();
                while (true)
                {
                    if (DateTime.Now.Second % 5 == 0 && lessonsAll.Count() != CheckCount())
                    {
                        Console.WriteLine($"{lessonsAll.Count()} {CheckCount()}");
                        LoadLessons();
                        Thread.Sleep(1000);
                    }
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
            }

            private void SendMessage(string message, Guid group)
            {
                _notification._client = _client;
                _notification.Send(message, group);

                _notification._client = _botVk;
                _notification.Send(message, group);
                Console.WriteLine("Sending messages");
            }

            private int CheckCount()
            {
                using (var db = new Connect(_client.ConnectionString).DBConnection())
                {
                    return db.Lessons.Where(lesson => lesson.Status == true &&
                    (lesson.LessonAt.Date >= DateTime.Now.Date || lesson.IsRepeats == true && lesson.LessonAt.DayOfWeek == DateTime.Now.DayOfWeek)).OrderBy(lesson => lesson.LessonAt).Count();
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

        public static string LoadLessons()
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://localhost:5001/api/Notifications");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";

            //using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            //{
            //    string json = "{\"user\":\"test\"," +
            //                  "\"password\":\"bla\"}";

            //    streamWriter.Write(json);
            //}
            HttpWebResponse httpResponse = null;
            try
            {
                httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    return streamReader.ReadToEnd();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Server is not available!");
                return "";
            }
        }
    }
}
