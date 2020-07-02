using BotAppData.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotAppData.Models
{
    public class LessonLog
    {
        public Guid LessonLogId { get; set; }
        public Guid LessonId { get; set; }
        public Lesson Lesson { get; set; }
        public long UserId { get; set; }
        public User Users { get; set; }
        public DateTime CreatedAt { get; set; }

        public LessonLog()
        {
            LessonLogId = new Guid();
            CreatedAt = DateTime.Now;
        }
    }
}
