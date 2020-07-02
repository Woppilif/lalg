using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BotAppData.Models
{
    public class Pattern
    {
        public Guid PatternId { get; set; }
        [Display(Name = "Название паттерна")]
        public string Name { get; set; }
        public ICollection<PatternMessage> PatternMessages { get; set; }
        public Pattern()
        {
            PatternId = new Guid();
        }
    }
}
