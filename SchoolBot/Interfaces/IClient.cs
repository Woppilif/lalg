using BotAppData;

namespace SchoolBot.Interfaces
{
    public enum KeyboardType
    {
        Standart = 0,
        Inline = 1,
        Nokeyboard = 2,
    }

    public enum BotPlatform
    {
        Telegram = 1,
        Vk = 2,
    }

    public interface IClient
    {
        string ConnectionString { get; set; }
        IKeyboard Keyboard { get; set; }
        void switchKeyboard(KeyboardType keyboardType);
        BotPlatform Platform { get; set; }
        void SendTextMessage(long UserId, string Message);
        object GetClient();
    }
}
