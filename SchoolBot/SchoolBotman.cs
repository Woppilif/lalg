using BotAppData.Interfaces;
using SchoolBot.Commands;
using SchoolBot.Data;
using SchoolBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SchoolBot
{
    public abstract class SchoolBotman : IClient
    {
        public string ConnectionString { get; set; }

        public Dictionary<string, Command> Commands = new Dictionary<string, Command>();

        public SchoolBotman(string connectionString)
        {
            ConnectionString = connectionString;
            foreach (Type type in
            Assembly.GetAssembly(typeof(Command)).GetTypes()
            .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(Command))))
            {
                var cmd = (Command)Activator.CreateInstance(type);
                Commands.Add(cmd.Name,cmd);
            }
        }
        public IKeyboard Keyboard { get; set; }
        public BotPlatform Platform { get; set; }

        public abstract object GetClient();

        public abstract void SendTextMessage(long UserId, string Message);

        public virtual void switchKeyboard(KeyboardType keyboardType)
        {
            throw new NotImplementedException();
        }

        public void ExecuteCommand(IMessage message, IClient client, IUser user)
        {
            var comm = Commands.Where(cmd => cmd.Key == message.Message || cmd.Value.Contains(message.Message) == true).FirstOrDefault();
            Console.WriteLine($"key: {comm.Key}");
            if (comm.Value != null)
            {
                comm.Value.Execute(message, client, user);
            }
            
            //foreach (var command in Commands)
            //{
            //    if (command.Contains(message.Message))
            //    {
            //        command.Execute(message, client, user);
            //        break;
            //    }
            //}
        }

        public IUser getUser(long UserId, string firstName = "", string lastName = "")
        {
            using (var db = new Connect(this.ConnectionString).DBConnection())
            {
                IUser users = db.Users.Where(u => u.UserId == UserId).FirstOrDefault();

                if (users == null)
                {
                    db.Users.Add(new BotAppData.Models.User { UserId = UserId, Platform = (int)this.Platform, Firstname = firstName, Lastname = lastName });
                    db.SaveChanges();
                    users = db.Users.Where(u => u.UserId == UserId).FirstOrDefault();
                }
                else
                {
                    if (users.Firstname == null)
                    {
                        users.Firstname = firstName;
                    }
                    if (users.Lastname == null)
                    {
                        users.Lastname = lastName;
                    }
                    db.SaveChanges();
                }
                return users;
            }
        }
    }
}
