using System;
using System.Collections.Generic;
using System.Linq;
using BotAppData.Interfaces;
using Microsoft.EntityFrameworkCore;
using SchoolBot.Data;
using SchoolBot.Interfaces;

namespace SchoolBot.Commands.Student
{
    class ExtendSubscriptionCommand : Command
    {
        public override string Name => "[Продлить]";

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
                client.switchKeyboard(KeyboardType.Standart);
                var sub = db.Subscriptions.Where(s => s.UserId == user.UserId).Include(b => b.Product).FirstOrDefault();
                if (sub == null)
                {
                    client.SendTextMessage(user.UserId, "У вас еще нет ни одной подписки! Выберите одну из подписок и попытайтесь снова!");
                    return;
                }
                var paymentId = Guid.NewGuid();
                db.Payments.Add(new BotAppData.Models.Payment { Id = paymentId, Amount = sub.Product.Price, 
                    UserId = user.UserId, SubscriptionId = sub.SubscriptionId, IsExtends = true });
                db.SaveChanges();

                client.SendTextMessage(user.UserId, $"Перейдите по следующей ссылке для оплаты подписки: " +
                    $"После успешной оплаты вы будете возвращены в бот.\n" +
                    $"https://ewtm.ru/api/Payments/{paymentId}");

            }
        }
    }
}
