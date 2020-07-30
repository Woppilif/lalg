using BotAppData.Interfaces;
using SchoolBot.Data;
using SchoolBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
//using Telegram.Bot.Types;

namespace NotificationService
{
    class SendMessage
    {
        private bool sleep = true;
        public int milisec = 300;
        public void SetSleep(int milisec)
        {
            this.milisec = milisec;
        }

        public string ChangeURL(string url = "")
        {
            return url.Replace("<url>", url);
        }

        public async void AutoSend( IClient client, string patternName)
        {
            Thread.Sleep(milisec);
            using (var db = new Connect(client.ConnectionString).DBConnection())
            {
                var users = db.Users.Where(s => s.Platform == 1 && s.Group.Name == patternName/*????????????? Это так??????????*/).ToList();
                var message = db.PatternMessages.Where(s => s.AtTime >= DateTime.Now && patternName == s.Pattern.Name).Select(s => s.Message).First();
                var new_mess = ChangeURL(message);
                foreach (var item in users)
                {
                    client.SendTextMessage(item.UserId, new_mess);
                }
            }
        }
    }
}
