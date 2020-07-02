using System;
using System.Linq;
using SchoolBot.Data;
using SchoolBot.Interfaces;

namespace SchoolBot.Notifications
{
    public class Notification
    {
        public SchoolBotman _client { get; set; }

        public void Send(string message, Guid group)
        {
            using (var db = new Connect(_client.ConnectionString).DBConnection())
            {
                var users = db.Users.Where(u => u.Platform == Convert.ToInt32(_client.Platform) && u.GroupId == group).ToList();
                Console.WriteLine("Sending message.....");
                foreach (var user in users)
                {
                    try
                    {
                        message = message.Replace("<UserId>", user.UserId.ToString());
                        _client.SendTextMessage(user.UserId, message);
                    }
                    catch (Exception e)
                    {
                        //user.GroupId = Guid.Empty;
                        //db.SaveChanges();
                        Console.WriteLine($"New exception while sending message: {e}");
                    }

                }
            }
        }
    }
}
