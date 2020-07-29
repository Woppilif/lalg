using SchoolBot;
using SchoolBot.Interfaces;
using System;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    class Message : IMessage
    {
        public long From { get; set; }
        string IMessage.Message { get; set; }
    }

    public class TelegramClient : SchoolBotman
    {
        private ITelegramBotClient _botClient;
        public TelegramClient(string ConnectionString, string token) : base(ConnectionString)
        {
            Platform = BotPlatform.Telegram;
            _botClient = new TelegramBotClient(token);
            var me = _botClient.GetMeAsync().Result;
            Console.WriteLine($"Hello, World! I am user {me.Id} and my name is {me.FirstName}.");
            _botClient.SendDiceAsync(610212420);
        }

        public override object GetClient()
        {
            return _botClient;
        }

        public async void SetWebHook(string url)
        {
            await _botClient.SetWebhookAsync($"{url}/api/Bots/Telegram");
        }

        public void Recieve(Update e)
        {
            if (e.Message.Text != null)
            {
                IMessage message = new Message();
                message.From = e.Message.From.Id;
                message.Message = e.Message.Text;
                this.ExecuteCommand(message, this, getUser(e.Message.Chat.Id, e.Message.Chat.FirstName, e.Message.Chat.LastName));
                Console.WriteLine($"Received a text message in chat {e.Message.Chat.Id}.");
            }
            if (e.Message.Contact != null)
            {
                IMessage message = new Message();
                message.From = e.Message.From.Id;
                message.Message = e.Message.Contact.PhoneNumber;
                var comm = this.Commands.Where(cmd => cmd.Key == "Телефон").FirstOrDefault();
                comm.Value.Execute(message, this, getUser(e.Message.Chat.Id, e.Message.Chat.FirstName, e.Message.Chat.LastName));
                //if (!message.Message.Contains('+'))
                //{
                //    message.Message = '+' + message.Message;
                //}
                //this.ExecuteCommand(message, this, getUser(e.Message.Chat.Id, e.Message.Chat.FirstName, e.Message.Chat.LastName));
                Console.WriteLine($"Received a Contact message in chat {e.Message.Chat.Id}.");
            }
        }

        public override void switchKeyboard(KeyboardType keyboardType)
        {
            if (keyboardType == KeyboardType.Inline)
            {
                Keyboard = new InlineKeyboard();
            }
            else if (keyboardType == KeyboardType.Standart)
            {
                Keyboard = new Keyboard();
            }
            else
            {
                Keyboard = null;
            }
        }

        public override async void SendTextMessage(long ChatId, string Message)
        {
            try
            {
                await _botClient.SendTextMessageAsync(
                  chatId: ChatId,
                  text: Message,
                  replyMarkup: Keyboard != null ? (ReplyMarkupBase)Keyboard.getKeyboard() : new ReplyKeyboardRemove()
                );
            }
            catch (Exception e)
            {
                Console.WriteLine($"New exception while sending message: {e}");
            }
        }
    }
}