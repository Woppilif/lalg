using System.Collections.Generic;
using SchoolBot.Interfaces;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    class Keyboard : IKeyboard
    {
        private List<KeyboardButton> args = null;
        private int perRow = 0;
        public Keyboard()
        {
            args = new List<KeyboardButton>();
            perRow = 0;
        }

        public void AddButton(string name, string callback, ButtonType type = ButtonType.Standart)
        {
            if(type == ButtonType.RequestContact)
            {
                args.Add(KeyboardButton.WithRequestContact(name));
            }
            else if (type == ButtonType.RequestLocation)
            {
                args.Add(KeyboardButton.WithRequestLocation(name));
            }
            else
            {
                args.Add(new KeyboardButton(name));
            }
        }

        public void SetPerRow(int count)
        {
            perRow = count;
        }
        public object getKeyboard()
        {
            List<List<KeyboardButton>> rows = new List<List<KeyboardButton>>();
            int inRow = 0;
            int count = 0;
            List<KeyboardButton> KeyboardButtons = null;
            foreach (var item in args)
            {
                if (inRow == 0)
                {
                    KeyboardButtons = new List<KeyboardButton>();
                }

                KeyboardButtons.Add(item);
                count++;
                if (inRow == perRow || args.Count == count)
                {
                    rows.Add(KeyboardButtons);
                    KeyboardButtons = null;
                    inRow = -1;
                }
                inRow++;
            }
            return new ReplyKeyboardMarkup(rows);
        }
    }
}
