using System;
using System.Collections.Generic;
using System.Linq;
using BotAppData.Interfaces;
using Microsoft.EntityFrameworkCore;
using SchoolBot.Data;
using SchoolBot.Interfaces;

namespace SchoolBot.Commands.Student
{
    class SubscCommand : Command
    {
        public override string Name => "[Моя подписка]";

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
                var sub = db.Subscriptions.Where(s => s.UserId == user.UserId && s.End > DateTime.Now).Include(b => b.Product).FirstOrDefault();
                if (sub != null)
                {
                    client.switchKeyboard(KeyboardType.Standart);
                    client.Keyboard.AddButton("[Меню]", "", ButtonType.Standart);
                    client.Keyboard.AddButton("[Продлить]", "", ButtonType.Standart);
                    client.SendTextMessage(user.UserId, $"Информация о вашей подписке:\n" +
                        $"Тип: {sub.Product.Name}\n" +
                        $"Статус оплаты: [{(sub.IsActive == true ? "Оплачена" : "Ожидает оплаты")}]\n" +
                        $"Действует с {sub.Begin} до {sub.End}\n" +
                        $"Вы можете продлить свою подписку на месяц, нажав на [Продлить]");
                    return;
                }
                else
                {
                    client.switchKeyboard(KeyboardType.Standart);
                    client.SendTextMessage(user.UserId, "У вас отсутствует подписка на занятия!");
                    return;
                }
            }
        }
    }
}
