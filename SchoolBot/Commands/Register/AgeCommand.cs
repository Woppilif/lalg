using BotAppData.Interfaces;
using BotAppData.Models;
using SchoolBot.Data;
using SchoolBot.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SchoolBot.Commands.Register
{
    class AgeCommand : Command
    {
        public override string Name => "Возраст";

        public override List<string> Alias
        {
            get
            {
                return new List<string>();
            }
        }

        public override void Execute(IMessage msg, IClient client, IUser user)
        {
            Age age = null;
            using (var db = new Connect(client.ConnectionString).DBConnection())
            {
                System.Text.StringBuilder ageStr = new System.Text.StringBuilder();
                ageStr.Append(msg.Message);
                ageStr.Remove(0, Name.Length +1);
                age = db.Ages.Where(a => a.Name == ageStr.ToString() && a.IsShows == true).FirstOrDefault();
                if (age == null)
                {
                    client.switchKeyboard(KeyboardType.Standart);
                    client.SendTextMessage(user.UserId, StandartMessages.AgeNotAccepted);
                    return;
                }
                if (user.Registered == false && user.AgeId == null)
                {
                    user.AgeId = age.AgeId;
                    db.Entry(user).Property(x => x.AgeId).IsModified = true;
                    db.SaveChanges();
                }
            }
            client.switchKeyboard(KeyboardType.Standart);
            client.Keyboard.AddButton("[Завершить регистрацию]", "", type: ButtonType.Standart);
            client.SendTextMessage(user.UserId, StandartMessages.PhoneAccepted);
        }
    }
}
