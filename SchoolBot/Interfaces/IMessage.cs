using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolBot.Interfaces
{
    public interface IMessage
    {
        long From { get; set; }
        string Message { get; set; }
    }
}
