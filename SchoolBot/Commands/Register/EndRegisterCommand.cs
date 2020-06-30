using System.Collections.Generic;
using System.Linq;
using BotAppData.Interfaces;
using BotAppData.Models;
using SchoolBot.Data;
using SchoolBot.Interfaces;

namespace SchoolBot.Commands
{
    class EndRegisterCommand : Command
    {
        public override string Name => "[Завершить регистрацию]";

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
                user.Registered = true;
                db.Entry(user).Property(x => x.Registered).IsModified = true;
                db.SaveChanges();
            }
            client.switchKeyboard(KeyboardType.Standart);
            client.SendTextMessage(user.UserId, StandartMessages.RegisterEnd);
            new SchoolBot.Commands.Utils.MenuCommand().Execute(msg, client, user);
        }
    }
}
