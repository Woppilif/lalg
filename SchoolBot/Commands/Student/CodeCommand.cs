using BotAppData.Interfaces;
using BotAppData.Models;
using Microsoft.EntityFrameworkCore;
using SchoolBot.Data;
using SchoolBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolBot.Commands.Student
{
    class CodeCommand : Command
    {
        public override string Name => "/code";

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
                string code = "";
                try
                {
                    code = msg.Message.Split(' ')[1];
                }
                catch (Exception)
                {
                    Console.WriteLine("onCodeException");
                }
                var codeInst = db.Referals.Where(r => r.Code == code && (r.IsActive == true || r.IsCommon == true))
                    .Include(b => b.Users)
                    .Include(b => b.Users.Group)
                    .Include(b => b.Users.Group.Product)
                    .FirstOrDefault();
                if (codeInst == null)
                {
                    client.switchKeyboard(KeyboardType.Standart);
                    client.SendTextMessage(user.UserId, "Код не найден!");
                    return;
                }
                decimal newBalance = codeInst.Amount;
                if (user.Registered == false)
                {
                    var tempGroup = codeInst.Users.Group;
                    user.GroupId = tempGroup.GroupId;
                    user.Firstname = codeInst.Users.Firstname;
                    user.Phone = codeInst.Users.Phone;
                    user.Platform = Convert.ToInt32(client.Platform);
                    user.Balance = user.Balance + newBalance;
                    user.Registered = true;
                    db.Entry(user).Property(x => x.Firstname).IsModified = true;
                    db.Entry(user).Property(x => x.Phone).IsModified = true;
                    db.Entry(user).Property(x => x.Platform).IsModified = true;
                    db.Entry(user).Property(x => x.Balance).IsModified = true;
                    db.Entry(user).Property(x => x.Registered).IsModified = true;
                    db.Entry(user).Property(x => x.GroupId).IsModified = true;
                    db.Remove(codeInst.Users);
                    codeInst.IsActive = false;

                    var lessons = db.Lessons.Where(l => l.GroupId == tempGroup.GroupId && l.Status == true).ToList();
                    if (lessons.Count() < 1)
                    {
                        client.SendTextMessage(user.UserId, "В подходящей для вас группе еще не добавлены занятия.\nПопробуйте позднее");
                        return;
                    }
                    DateTime begin = DateTime.Now;
                    Lesson neededLesson = null;
                    if (lessons.First().LessonAt > DateTime.Now)
                    {
                        begin = lessons.First().LessonAt;
                        neededLesson = lessons.First();
                    }
                    else
                    {
                        neededLesson = lessons.Where(l => l.LessonAt > DateTime.Now).FirstOrDefault();
                        if (neededLesson == null)
                        {
                            client.SendTextMessage(user.UserId, "Произошла ошибка!");
                            return;
                        }
                        begin = neededLesson.LessonAt;
                    }
                    DateTime end = DateTime.MinValue;
                    if (tempGroup.Product.FreeTimes != 0)
                    {
                        end = begin.AddDays(tempGroup.Product.FreeTimes);
                    }
                    else
                    {
                        end = lessons.Last().LessonAt;
                    }
                    var subscription = db.Subscriptions.Add(new Subscription { UserId = user.UserId, Begin = begin, End = end, ProductId = tempGroup.ProductId, IsActive = true });
                }
                else
                {
                    user.Balance = user.Balance + newBalance;
                    db.Entry(user).Property(x => x.Balance).IsModified = true;
                    if (codeInst.IsCommon == false)
                    {
                        codeInst.IsActive = false;
                    }
                }
                db.SaveChanges();
                client.switchKeyboard(KeyboardType.Standart);
                client.SendTextMessage(user.UserId, $"Вы успешно активировали код!\nНа ваш баланс зачислено {newBalance}");
                new SchoolBot.Commands.Utils.MenuCommand().Execute(msg, client, user);
            }
        }
    }
}
