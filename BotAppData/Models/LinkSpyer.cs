using System;
using System.Collections.Generic;
using System.Text;

namespace BotAppData.Models
{
    public class LinkSpyer
    {
        public Guid Id { get; set; }
        public Guid LessonId { get; set; }
        public Lesson Lesson { get; set; }
        public DateTime CreatedAt { get; set; }
        public long UserId { get; set; }
        public User Users { get; set; }
        public LinkSpyer()
        {
            Id = new Guid();
            CreatedAt = DateTime.Now;
            UserId = 0;
        }
    }
}
