using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotAppData.Models
{
    public class Lesson
    {
        public Guid LessonId { get; set; }
        public Guid GroupId { get; set; }
        public Group Group { get; set; }

        //[DisplayFormat(DataFormatString = "{dd-MM-yyyy H:m}", ApplyFormatInEditMode = true)]
        public DateTime LessonAt { get; set; }
        public bool Status { get; set; }

        [Display(Name = "Ссылка на занятие")]
        public string Url { get; set; }
        public Guid PatternId { get; set; }
        public Pattern Pattern { get; set; }
        [Display(Name = "Занятие повторяется")]
        public bool IsRepeats { get; set; }
        public ICollection<Pattern> Patterns { get; set; }
        public Lesson()
        {
            LessonId = new Guid();
        }
    }
}
