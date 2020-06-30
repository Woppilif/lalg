using System;
using System.Collections.Generic;
using System.Linq;
using BotAppData.Interfaces;
using SchoolBot.Data;
using SchoolBot.Interfaces;

namespace SchoolBot.Commands.Student
{
    class TimetableCommand : Command
    {
        public override string Name => "[Расписание занятий]";

        public override List<string> Alias
        {
            get
            {
                return new List<string>();
            }
        }

        public override void Execute(IMessage msg, IClient client, IUser user)
        {
            using (var db = new Connect(client.ConnectionString).DBConnection())
            {
                List<BotAppData.Models.Lesson> myLessons = null;
                string result = "Расписание занятий\n";
                if (user.IsTeacher == false)
                {
                    var sub = db.Subscriptions.Where(s => s.UserId == user.UserId && s.End > DateTime.Now && s.IsActive == true).FirstOrDefault();

                    if (sub == null)
                    {
                        client.switchKeyboard(KeyboardType.Standart);
                        client.SendTextMessage(user.UserId, "У вас нет подписки на занятия или она еще не оплачена!");
                        return;
                    }
                    var myGroup = db.Groups.Where(g => g.GroupId == user.GroupId).FirstOrDefault();
                    if (myGroup == null)
                    {
                        client.switchKeyboard(KeyboardType.Standart);
                        client.SendTextMessage(user.UserId, "Ваша группа не найдена!");
                        return;
                    }
                    myLessons = db.Lessons.Where(l => l.Group == myGroup && l.Status == true).ToList();
                    foreach (var item in myLessons)
                    {
                        result += $"{item.LessonAt.AddHours(2).ToString("dddd")} в {item.LessonAt.ToString("H:mm")}\n";
                    }
                }
                else
                {
                    var allGroups = db.Groups.ToList();
                    foreach (var group in allGroups)
                    {
                        result += $"Группа {group.Name}\n";
                        myLessons = db.Lessons.Where(l => l.Group == group && l.Status == true && (l.IsRepeats == true || l.LessonAt > DateTime.Now)).ToList();
                        foreach (var item in myLessons)
                        {
                            result += $"{item.LessonAt.AddHours(2).ToString("dddd")} в {item.LessonAt.ToString("H:mm")}\n";
                        }
                    }
                }


                client.switchKeyboard(KeyboardType.Standart);
                client.SendTextMessage(user.UserId, result);
                return;

            }
        }
    }
}
