using System;
using System.Collections.Generic;
using System.Text;

namespace BotAppData.Interfaces
{
    public enum Ages
    {
        Children = 1,
        Teens = 2,
        Students = 3,
        Adults = 4,
        Any = 5
    }
    public interface IUser
    {
        long UserId { get; set; }
        DateTime CreatedAt { get; set; }
        Guid? GroupId { get; set; }
        int Platform { get; set; }
        string Firstname { get; set; }
        string Lastname { get; set; }
        string Phone { get; set; }
        bool Registered { get; set; }
        int? AgeId { get; set; }
        bool IsAdmin { get; set; }
        bool IsTeacher { get; set; }
        decimal Balance { get; set; }
    }
}
