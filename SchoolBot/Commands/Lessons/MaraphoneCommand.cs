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
    class MaraphoneCommand : Command
    {
        public override string Name => "[Записаться на марафон]";

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
            //string message = "Ближайших марафонов не найдено.\nСледите за новостями!";
            //using (var db = new Connect(client.ConnectionString).DBConnection())
            //{
            //    var users = db.Users.Where(u => u.UserId == user.UserId).FirstOrDefault();
            //    if (users == null)
            //    {
            //        return;
            //    }

            //    var sub = db.Subscriptions.Where(s => s.UserId == users.UserId && s.End > DateTime.Now).FirstOrDefault();
            //    if (sub != null)
            //    {
            //        client.switchKeyboard(KeyboardType.Standart);
            //        client.SendTextMessage(user.UserId, "У вас уже есть оформленная подписка! Необходимо дождаться ее окончания");
            //        return;
            //    }

            //    var webinar = db.LinkSpyers.Where(sp => sp.UserId == users.UserId).ToList().LastOrDefault();
            //    if(webinar == null)
            //    {
            //        client.switchKeyboard(KeyboardType.Standart);
            //        client.SendTextMessage(user.UserId, "Чтобы записаться на марафон, наобходимо сначала записаться на вебинар и пройти его" +
            //            ".\nПодсказка: Сначала запишитесь на вебинар.");
            //        return;
            //    }

            //    List<Group> groups = new List<Group>();
            //    var lessons = db.Lessons.Where(lesson => lesson.LessonAt >= DateTime.Now && lesson.Status == true).OrderBy(lesson => lesson.LessonAt).ToList();
            //    foreach (var item in lessons)
            //    {
            //        groups.Add(db.Groups.Find(item.Group));
            //    }
            //    Lesson lsn = null;
            //    var neededGroup = groups.Where(g => g != null && g.Type == 3).FirstOrDefault();
            //    if (neededGroup != null)
            //    {
            //        lsn = lessons.Where(lesson => lesson.Group == neededGroup.Id).FirstOrDefault();
            //    }

                
            //    //var lsn = db.Lessons.Where(lesson => lesson.LessonAt >= DateTime.Now && lesson.Status == true).OrderBy(lesson => lesson.LessonAt).FirstOrDefault();

            //    if (lsn != null)
            //    {
            //        var subscrId = Guid.NewGuid();
            //        db.Subscriptions.Add(new Subscription
            //        {
            //            Id = subscrId,
            //            UserId = users.UserId,
            //            IsActive = true,
            //            Product = Products.Maraphone,
            //            Begin = lsn.LessonAt.Date,
            //            End = lsn.LessonAt.Date.AddDays(1)
            //        });
            //        var paymentId = Guid.NewGuid();
            //        var product = db.ProductItem.Where(p => p.Product == Products.Maraphone).FirstOrDefault();
            //        if (product == null)
            //        {
            //            client.switchKeyboard(KeyboardType.Standart);
            //            client.SendTextMessage(user.UserId, "Для вашей возрастной группы пока нет услуг!");
            //            return;
            //        }
            //        //var neededProduct = product.Where(p => p.Ages.Contains(user.Age)).FirstOrDefault();
            //        //if (neededProduct == null)
            //        //{
            //        //    client.switchKeyboard(KeyboardType.Standart);
            //        //    client.SendTextMessage(user.UserId, "Для вашей возрастной группы пока нет услуг!");
            //        //    return;
            //        //}
            //        db.Payment.Add(new BotAppData.Models.Payments { Id = paymentId, Amount = product.Price, UserId = user.UserId, SubscriptionId = subscrId });
            //        users.Group = lsn.Group;
            //        //db.Entry(user).Property(x => x.Group).IsModified = true;
            //        db.SaveChanges();
            //        var patternMessage = db.PatternMessages.Where(pt => pt.PatternId == lsn.PatternId && pt.Order == 1).FirstOrDefault();
            //        if (patternMessage != null)
            //        {
            //            neededGroup.StudentsCount++;
            //            db.SaveChanges();
            //            string link = $"https://langalgorithm.ru/api/LinkSpyers/{lsn.Id}/{users.UserId}";
            //            message = patternMessage.MakeMessage(link, lsn.LessonAt) + "\nВы были успешно добавлены в группу марафона!\n" +
            //                "Обратите внимание на пункт подписка.\nДля участия в марафоне сначала нужно оплатить свою подписку.";
            //        }
            //    }
            //}
            //client.switchKeyboard(KeyboardType.Standart);
            //client.SendTextMessage(user.UserId, message);
            //new SchoolBot.Commands.Utils.MenuCommand().Execute(msg, client, user);
        }
    }
}
