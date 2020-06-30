using System.Collections.Generic;
using System.Linq;
using BotAppData.Interfaces;
using BotAppData.Models;
using SchoolBot.Data;
using SchoolBot.Interfaces;

namespace SchoolBot.Commands
{
    class PersonalDataCommand : Command
    {
        public override string Name => "[Даю согласие]";

        public override List<string> Alias
        {
            get
            {
                return new List<string>();
            }
        }

        public override void Execute(IMessage msg, IClient client, IUser user)
        {
            string message = "";
            if (client.Platform == BotPlatform.Telegram)
            {
                client.switchKeyboard(KeyboardType.Standart);
                client.Keyboard.AddButton("[Отправить контактные данные]", "", type: ButtonType.RequestContact);
                message = StandartMessages.RequestPhoneMessageTelegram;
            }
            else if (client.Platform == BotPlatform.Vk)
            {
                client.switchKeyboard(KeyboardType.Nokeyboard);
                message = StandartMessages.RequestPhoneMessageVk;
            }
            client.SendTextMessage(user.UserId, message);

        }
    }
}
