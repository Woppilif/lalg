using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BotAppData.Interfaces;
using BotAppData.Models;
using SchoolBot.Data;
using SchoolBot.Interfaces;

namespace SchoolBot.Commands.Lessons
{
    class WebinarCommand : Command
    {
        public override string Name => "[Записаться на вебинар]";

        public override List<string> Alias
        {
            get
            {
                return new List<string>
                {
                    "вебинар"
                };
            }
        }

        public override void Execute(IMessage msg, IClient client, IUser user)
        {
            //string message = "Ближайших вебинаров не найдено.\nСледите за новостями!";
            //using (var db = new Connect(client.ConnectionString).DBConnection())
            //{
            //    List<Group> groups = new List<Group>();
            //    var now = DateTime.Now;
            //    var lessons = db.Lessons.Where(lesson => lesson.LessonAt > DateTime.Now.AddHours(2) && lesson.Status == true).OrderBy(lesson => lesson.LessonAt).ToList();
            //    foreach (var item in lessons)
            //    {
            //        groups.Add(db.Groups.Find(item.Group));
            //    }
            //    Lesson lsn = null;
            //    var neededGroup = groups.Where(g => g != null && g.Type == 2).FirstOrDefault();
            //    if(neededGroup != null)
            //    {
            //        lsn = lessons.Where(lesson => lesson.Group == neededGroup.Id).FirstOrDefault();
            //    }

            //    if (lsn != null)
            //    {
            //        user.Group = lsn.Group;
            //        db.Entry(user).Property(x => x.Group).IsModified = true;
            //        db.SaveChanges();
            //        var patternMessage = db.PatternMessages.Where(pt => pt.PatternId == lsn.PatternId && pt.Order == 1).FirstOrDefault();
            //        if (patternMessage != null)
            //        {
            //            neededGroup.StudentsCount++;
            //            db.SaveChanges();
            //            string link = $"https://langalgorithm.ru/api/LinkSpyers/{lsn.Id}/{user.UserId}";
            //            message = patternMessage.MakeMessage(link, lsn.LessonAt) + "\nВы были успешно добавлены в группу вебинара!";
            //        }
            //    }
            //}
            //client.switchKeyboard(KeyboardType.Standart);
            //client.SendTextMessage(user.UserId, message);
        }
    }
}
