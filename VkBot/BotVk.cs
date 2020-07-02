using System;
using System.Collections.Generic;
using SchoolBot;
using SchoolBot.Interfaces;
using VkNet;
using VkNet.Model;
using VkNet.Model.Keyboard;
using VkNet.Model.RequestParams;
using VkNet.Utils;

namespace VkBot
{
    class Message : IMessage
    {
        public long From { get; set; }
        string IMessage.Message { get; set; }
    }

    public class BotVk : SchoolBotman
    {
        private VkApi _client;
        public string Code { get; set; }
        public BotVk(string ConnectionString, string token, string code = "") : base(ConnectionString)
        {
            Platform = BotPlatform.Vk;
            Keyboard = new Keyboard();
            _client = new VkApi();
            Code = code;
            _client.Authorize(new ApiAuthParams { AccessToken = token });
        }

        public override object GetClient()
        {
            return _client;
        }

        public override void SendTextMessage(long ChatId, string Message)
        {
            _client.Messages.Send(new MessagesSendParams
            {
                RandomId = new DateTime().Millisecond,
                PeerId = ChatId,
                Message = Message,
                Keyboard = this.Keyboard != null ? (MessageKeyboard)this.Keyboard.getKeyboard() : (MessageKeyboard)new Keyboard().getKeyboard()
            });
        }

        public async void Recieve(Update update)
        {
            if (update.Object != null)
            {
                var msg = VkNet.Model.Message.FromJson(new VkResponse(update.Object));

                IMessage message = new Message();
                message.From = msg.PeerId.Value;
                message.Message = msg.Text;
                List<long> ids = new List<long>();
                ids.Add(msg.PeerId.Value);
                var cl = await _client.Users.GetAsync(ids);
                this.ExecuteCommand(message, this, getUser(msg.PeerId.Value, cl[0].FirstName, cl[0].LastName));
                Console.WriteLine($"Received a text message in chat {message.Message}.");
            }
        }

        public override void switchKeyboard(KeyboardType keyboardType)
        {
            if (keyboardType == KeyboardType.Nokeyboard)
            {
                Keyboard = null;
            }
            else
            {
                Keyboard = new Keyboard();
            }
        }
    }
}
