using System;
using System.Collections.Generic;
using System.Linq;
using BotAppData.Interfaces;
using BotAppData.Models;
using SchoolBot.Commands.Lessons;
using SchoolBot.Data;
using SchoolBot.Interfaces;

namespace SchoolBot.Commands
{
    public class StartCmd : Command
    {
        public override string Name => "/start";

        public override List<string> Alias
        {
            get
            {
                return new List<string>
                {
                    "Начать","Start"
                };
            }
        }

        public override void Execute(IMessage msg, IClient client, IUser user)
        {
            if(user.Registered == true)
            {
                new SchoolBot.Commands.Utils.MenuCommand().Execute(msg, client, user);
            }
            else
            {
                new WebinarCommand().Execute(msg, client, user);
                client.switchKeyboard(KeyboardType.Standart);
                client.Keyboard.AddButton("[Даю согласие]", "", type: ButtonType.Standart);
                client.SendTextMessage(user.UserId, StandartMessages.StartMessage);
                return;
            }
        }
    }
}
