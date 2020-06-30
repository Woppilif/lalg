using System;
using System.Collections.Generic;
using System.Linq;
using BotAppData.Interfaces;
using SchoolBot.Data;
using SchoolBot.Interfaces;

namespace SchoolBot.Commands.Utils
{
    class MenuCommand : Command
    {
        public override string Name => "/menu";

        public override List<string> Alias
        {
            get
            {
                return new List<string>
                {
                    "[Меню]"
                };
            }
        }

        public override void Execute(IMessage msg, IClient client, IUser user)
        {
            using (var db = new Connect(client.ConnectionString).DBConnection())
            {
                if (user.Registered == true)
                {
                    client.switchKeyboard(KeyboardType.Standart);
                    if(user.IsTeacher == true)
                    {
                        client.Keyboard.AddButton("[Расписание занятий]", "", type: ButtonType.Standart);
                        client.Keyboard.AddButton("[Мои группы]", "", type: ButtonType.Standart);
                    }
                    else
                    {
                        var sub = db.Subscriptions.Where(s => s.UserId == user.UserId && s.End > DateTime.Now).FirstOrDefault();
                        if (sub == null)
                        {
                            var productTypes = db.ProductTypes.Where(p => p.IsActive == true).Select(p => p.Name).ToList();
                            foreach (var prodType in productTypes)
                            {
                                client.Keyboard.AddButton($"[Записаться на {prodType}]", "", type: ButtonType.Standart);
                            }
                        }
                        else
                        {
                            client.Keyboard.AddButton("[Расписание занятий]", "", type: ButtonType.Standart);
                            client.Keyboard.AddButton("[Моя подписка]", "", type: ButtonType.Standart);
                            //if (sub.Product == BotAppData.Models.Products.Maraphone)
                            //{
                            //    client.Keyboard.AddButton("[Записаться на пробное занятие]", "", type: ButtonType.Standart);
                            //    client.Keyboard.AddButton("[Записаться на регулярные занятия]", "", type: ButtonType.Standart);
                            //}
                        }
                    }
                    if (user.IsTeacher == true)
                    {
                        client.SendTextMessage(user.UserId, StandartMessages.TeacherMessage);
                    }
                    else
                    {
                        client.SendTextMessage(user.UserId, StandartMessages.MenuCmd);
                    }
                    
                }
            }

        }
    }
}
