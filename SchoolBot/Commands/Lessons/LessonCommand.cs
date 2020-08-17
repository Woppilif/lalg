using BotAppData.Interfaces;
using BotAppData.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SchoolBot.Data;
using SchoolBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolBot.Commands.Lessons
{
    class LessonCommand : Command
    {
        public override string Name => "Записаться на";

        public override List<string> Alias
        {
            get
            {
                return new List<string>();
            }
        }

        public override void Execute(IMessage msg, IClient client, IUser user)
        {
            if (user.Registered == true)
            {
                using (var db = new Connect(client.ConnectionString).DBConnection())
                {
                    client.switchKeyboard(KeyboardType.Standart);
                    var sub = db.Subscriptions.Where(s => s.UserId == user.UserId && s.End > DateTime.Now).FirstOrDefault();
                    if (sub != null)
                    {
                        client.SendTextMessage(user.UserId, "У вас уже есть оформленная подписка! Необходимо дождаться ее окончания");
                        return;
                    }

                    System.Text.StringBuilder prodStr = new System.Text.StringBuilder();
                    prodStr.Append(msg.Message);
                    prodStr.Remove(0, Name.Length+2);
                    prodStr.Replace("]", "");
                    var prodType = db.ProductTypes.Where(p => p.IsActive == true && p.Name == prodStr.ToString()).FirstOrDefault();
                    if (prodType == null)
                    {
                        client.SendTextMessage(user.UserId, "Данный тип продукта не найден!");
                        return;
                    }

                    var prod = db.Products.Where(p => p.ProductTypeId == prodType.ProductTypeId).FirstOrDefault();
                    if (prod == null)
                    {
                        client.SendTextMessage(user.UserId, "Данный тип товара не найден!");
                        return;
                    }

                    var groups = db.Groups.Where(g => g.ProductId == prod.ProductId && (g.Age.AgeId == user.AgeId || g.IsCommon == true)).ToList();
                    Group tempGroup = null;
                    foreach (var group in groups)
                    {
                        if (group.IsCommon == true)
                        {
                            tempGroup = group;
                            break;
                        }
                        else
                        {
                            var usersCount = db.Users.Where(u => u.GroupId == group.GroupId).Count();
                            if (usersCount < 12)
                            {
                                tempGroup = group;
                                break;
                            }
                        }
                    }

                    if (tempGroup == null)
                    {
                        client.SendTextMessage(user.UserId, "Подходящей для вас группы не найдено!");
                        return;
                    }

                    var lessons = db.Lessons.Where(l => l.GroupId == tempGroup.GroupId && l.Status == true).ToList();
                    if (lessons.Count() < 1)
                    {
                        client.SendTextMessage(user.UserId, "В подходящей для вас группе еще не добавлены занятия.\nПопробуйте позднее");
                        return;
                    }
                    bool active = false;
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
                            client.SendTextMessage(user.UserId, "Занятие не найдено, запишитесь на пробное занятие");
                            return;
                        }
                        begin = neededLesson.LessonAt;
                    }
                    DateTime end = DateTime.MinValue;

                    if (prod.FreeTimes != 0)
                    {
                        end = begin.AddDays(prod.FreeTimes);
                        active = true;
                    }
                    else
                    {
                        end = lessons.Last().LessonAt;
                    }
                    prodStr.Clear();                    
                    prodStr.Append($"Вы были добавлены в группу {neededLesson.Group.GroupType.Name}!\nБлижайшее занятие состоится {neededLesson.LessonAt}. Мы Вам обязательно напомним! :)\n");
                    var subscription = db.Subscriptions.Add(new Subscription { UserId = user.UserId, Begin = begin, End = end, ProductId = prod.ProductId, IsActive = active });
                    db.SaveChanges();
                    EntityEntry<Payment> payment = null;
                    if (prod.Price > 0 && prod.FreeTimes == 0)
                    {
                        Guid paymentId = Guid.NewGuid();
                        payment = db.Payments.Add(new Payment { Id = paymentId, Amount = prod.Price, SubscriptionId = subscription.Entity.SubscriptionId, UserId = user.UserId });
                        prodStr.Append($"\nНе забудьте оплатить занятия перед тем, как они начнутся. https://ewtm.ru/api/Payments/{paymentId}");
                    }
                    user.GroupId = tempGroup.GroupId;
                    db.Entry(user).Property(x => x.GroupId).IsModified = true;
                    db.SaveChanges();

                    var patternMessage = db.PatternMessages.Where(pt => pt.PatternId == neededLesson.PatternId && pt.IsGreeting == true).FirstOrDefault();
                    if (patternMessage != null)
                    {
                        string link = $"https://langalgorithm.ru/api/LinkSpyers/{neededLesson.LessonId}/{user.UserId}";
                        prodStr.Append(patternMessage.MakeMessage(link, neededLesson.LessonAt));
                    }
                    client.SendTextMessage(user.UserId, prodStr.ToString());
                    //TODO Посмотри тут
                    new SchoolBot.Commands.Utils.MenuCommand().Execute(msg, client, user);
                }
            }
        }
    }
}
