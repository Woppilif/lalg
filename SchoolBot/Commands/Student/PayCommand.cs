using System.Collections.Generic;
using BotAppData.Interfaces;
using SchoolBot.Interfaces;

namespace SchoolBot.Commands.Student
{
    class PayCommand : Command
    {
        public override string Name => "[Оплата занятий]";

        public override List<string> Alias
        {
            get
            {
                return new List<string>();
            }
        }

        public override void Execute(IMessage msg, IClient client, IUser user)
        {
            client.switchKeyboard(KeyboardType.Standart);
            client.SendTextMessage(user.UserId, "Оплата осуществляется через следующий сервис:" +
                "Пожалуйста, будьте внимательны при заполнении платежных данных!");
        }
    }
}
