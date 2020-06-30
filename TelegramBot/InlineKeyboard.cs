using System;
using System.Collections.Generic;
using System.Text;
using SchoolBot.Interfaces;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    class InlineKeyboard : IKeyboard
    {

        public void AddButton(string name, string callback)
        {
            throw new NotImplementedException();
        }

        public void AddButton(string name, string callback, ButtonType type)
        {
            throw new NotImplementedException();
        }

        public void AddRow()
        {
            throw new NotImplementedException();
        }

        public InlineKeyboardMarkup createKeyboard(Dictionary<string, string> args, int perRow = 0)
        {
            List<List<InlineKeyboardButton>> rows = new List<List<InlineKeyboardButton>>();
            int inRow = 0;
            int count = 0;
            List<InlineKeyboardButton> inlineKeyboardButtons = null;
            foreach (var item in args)
            {
                if (inRow == 0) inlineKeyboardButtons = new List<InlineKeyboardButton>();
                inlineKeyboardButtons.Add(InlineKeyboardButton.WithCallbackData(item.Key, item.Value));
                count++;
                if (inRow == perRow || args.Count == count)
                {
                    rows.Add(inlineKeyboardButtons);
                    inlineKeyboardButtons = null;
                    inRow = -1;
                }
                inRow++;
            }
            return new InlineKeyboardMarkup(rows);
        }

        public object getKeyboard()
        {
            throw new NotImplementedException();
        }

        public void SetPerRow(int count)
        {
            throw new NotImplementedException();
        }
    }
}
