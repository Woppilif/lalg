using System;
using System.Collections.Generic;
using SchoolBot.Interfaces;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.Keyboard;

namespace VkBot
{
    class Keyboard : IKeyboard
    {
        List<List<MessageKeyboardButton>> buttons;
        private int perRow = 0;

        public Keyboard()
        {
            buttons = new List<List<MessageKeyboardButton>>();
            perRow = 0;
        }
        public void AddButton(string name, string callback, ButtonType type)
        {
            buttons.Add(new List<MessageKeyboardButton> {
                new MessageKeyboardButton
                {
                    Action = new MessageKeyboardButtonAction
                    {
                        Type = KeyboardButtonActionType.Text, //Тип кнопки клавиатуры
                        Label = name, //Надпись на кнопке
                    },
                    Color = KeyboardButtonColor.Default //Цвет кнопки
                }
            });
        }

        public object getKeyboard()
        {
            if (buttons.Count == 0) return null;
            return new MessageKeyboard
            {
                Buttons = this.buttons
            };
        }

        //should be virtual
        public void SetPerRow(int count)
        {
            perRow = count;
        }
    }
}
