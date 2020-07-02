using BotAppData.Interfaces;
using SchoolBot.Interfaces;
using System.Collections.Generic;

namespace SchoolBot.Commands
{
    public abstract class Command
    {
        public abstract string Name { get; }

        public abstract List<string> Alias { get; }

        public abstract void Execute(IMessage msg, IClient client, IUser user);

        public bool Contains(string command)
        {
            char[] spliters = { '#', ':', ' ' };
            string tempCommand = "";
            foreach (var item in spliters)
            {
                tempCommand = command.Split(item)[0].Trim();
                System.Console.WriteLine(tempCommand);
                if (tempCommand.Contains(Name) || Alias.Contains(tempCommand))
                {
                    return true;
                }
                tempCommand = command;
            }
            return false;
        }
    }
}
