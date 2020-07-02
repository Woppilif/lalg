using BotAppData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolBot.Data
{
    public class Connect
    {
        private static string connectionString;
        public Connect(string connection)
        {
            connectionString = connection;
        }

        private static DbContextOptions<BotAppContext> Connection()
        {
            return new DbContextOptionsBuilder<BotAppContext>()
                   .UseNpgsql(connectionString)
                   .Options;
        }

        public BotAppContext DBConnection()
        {
            return new BotAppContext(Connection());
        }
    }
}
