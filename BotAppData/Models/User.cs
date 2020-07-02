using System;
using System.ComponentModel.DataAnnotations;
using BotAppData.Interfaces;

namespace BotAppData.Models
{
    public class User : IUser
    {
        [Key]
        public long UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? GroupId { get; set; }//это вроде бы есть в классе Group
        public Group Group { get; set; }
        public int Platform { get; set; }
        [Display(Name = "Имя")]
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        [Display(Name = "Номер телефона")]
        public string Phone { get; set; }
        public bool Registered { get; set; }
        public int? AgeId { get; set; }
        public Age Age { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsTeacher { get; set; }
        public decimal Balance { get; set; }
        public User()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
