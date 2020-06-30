namespace SchoolBot.Interfaces
{
    public enum ButtonType
    {
        Standart = 0,
        RequestContact = 1,
        RequestLocation = 2,
    }
    public interface IKeyboard
    {
        void SetPerRow(int count);
        void AddButton(string name, string callback, ButtonType type);
        object getKeyboard();
    }
}
