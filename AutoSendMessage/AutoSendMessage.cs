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
        private SchoolBot.Notifications.Notification _notification;
        private TelegramClient _client;
        private BotVk _botVk;
        //private Li
        public static void Main(/*string[] args*/)
        {
            //Console.WriteLine("Hello World!");
            var users = LoadUser(1, guid);
            var patternMessage = LoadMessage(guid);

            foreach (var item in patternMessage)
            {
                SendMessage(item.Message, guid);
            }
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

        public void SendMessage(string message, Guid group)
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
    }
}
