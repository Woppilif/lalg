using System;
using System.Collections.Generic;
using System.Linq;
using BotAppData.Interfaces;
using BotAppData.Models;
using SchoolBot.Data;
using SchoolBot.Interfaces;

namespace SchoolBot.Commands.Lessons
{
    class TrialLessonCommand : Command
    {
        public override string Name => "[Записаться на пробное занятие]";

        public override List<string> Alias
        {
            get
            {
                return new List<string>();
            }
        }

        public override void Execute(IMessage msg, IClient client, IUser user)
        {
            //using (var db = new Connect(client.ConnectionString).DBConnection())
            //{
            //    var sub = db.Subscriptions.Where(s => s.UserId == user.UserId && s.End > DateTime.Now).FirstOrDefault();
            //    if (sub != null)
            //    {
            //        client.switchKeyboard(KeyboardType.Standart);
            //        client.SendTextMessage(user.UserId, "У вас уже есть оформленная подписка! Необходимо дождаться ее окончания");
            //        return;
            //    }

            //    //var myGroup = db.Groups.Where(g => g.StudentsCount < 12 && g.Age == Convert.ToInt32(user.Age) && g.Type == 1).FirstOrDefault();
            //    var myGroup = db.Groups.Where(g => g.StudentsCount <= 12 && g.Type == 1 && (g.Age == Convert.ToInt32(user.Age) || g.Age == 5)).FirstOrDefault();
            //    if (myGroup == null)
            //    {
            //        client.switchKeyboard(KeyboardType.Standart);
            //        client.SendTextMessage(user.UserId, "К сожалению, подходящей для вас группы не найдено.\n Попробуйте позднее!");
            //        return;
            //    }

            //    var neededLesson = db.Lessons.Where(l => l.Group == myGroup.Id && l.LessonAt >= DateTime.Now).FirstOrDefault();
            //    if (neededLesson == null)
            //    {
            //        client.switchKeyboard(KeyboardType.Standart);
            //        client.SendTextMessage(user.UserId, "К сожалению, подходящей для вас группы не найдено.\n Попробуйте позднее!");
            //        return;
            //    }
            //    db.Subscriptions.Add(new Subscription
            //    {
            //        UserId = user.UserId,
            //        IsActive = true,
            //        Product = Products.Trial,
            //        Begin = neededLesson.LessonAt.Date,
            //        End = neededLesson.LessonAt.Date.AddDays(1)
            //    });
            //    user.GroupId = myGroup.Id;
            //    db.Entry(user).Property(x => x.GroupId).IsModified = true;
            //    db.SaveChanges();
            //    client.switchKeyboard(KeyboardType.Nokeyboard);
            //    client.SendTextMessage(user.UserId, $"Вы были записаны на пробное занятие, которое состоится" +
            //        $" {neededLesson.LessonAt}! Вы были добавлены в группу пробного занятия");
            //}

        }
    }
}
