using System.Collections.Generic;
using System.Linq;
using BotAppData.Interfaces;
using BotAppData.Models;
using SchoolBot.Data;
using SchoolBot.Interfaces;

namespace SchoolBot.Commands
{
    class PhoneNumberCommand : Command
    {
        public override string Name => "Телефон";

        public override List<string> Alias
        {
            get
            {
                return new List<string> {
                    "79"
                };
            }
        }

        public override void Execute(IMessage msg, IClient client, IUser user)
        {
            using (var db = new Connect(client.ConnectionString).DBConnection())
            {
                try
                {
                    if (msg.Message.Length > 9)
                    {
                        msg.Message = msg.Message.Split(' ')[1];
                    }
                    else
                    {
                        client.switchKeyboard(KeyboardType.Standart);
                        client.SendTextMessage(user.UserId, StandartMessages.PhoneNotAccepted);
                        return;
                    }
                }
                catch (System.Exception)
                {
                    
                }
                if (msg.Message.Length < 11)
                {
                    client.switchKeyboard(KeyboardType.Standart);
                    client.SendTextMessage(user.UserId, StandartMessages.PhoneNotAccepted);
                    return;
                }
                user.Phone = msg.Message;
                db.Entry(user).Property(x => x.Phone).IsModified = true;
                db.SaveChanges();
                client.switchKeyboard(KeyboardType.Standart);
                var agesList = db.Ages.Where(a => a.IsShows == true).ToList();
                foreach (var age in agesList)
                {
                    client.Keyboard.AddButton($"Возраст {age.Name}", "", type: ButtonType.Standart);
                }
                client.SendTextMessage(user.UserId, StandartMessages.AgeMessage);
            }            
        }
    }
}
