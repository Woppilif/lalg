using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BotAppData.Models
{
    public class Age
    {
        public int AgeId { get; set; }
        public string Name { get; set; }
        [Display(Name = "Реальное значение возраста к его имени")]
        public int  Order { get; set; }
        [Display(Name = "Показывать в боте?")]
        public bool IsShows { get; set; }
        public ICollection<Group> Groups { get; set; }
    }
}
