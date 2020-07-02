using System;
using System.Collections.Generic;
using System.Text;

namespace BotAppData.Models
{
    public class Group
    {
        public Guid GroupId { get; set; }
        public string Name { get; set; }
        public int AgeId { get; set; }
        public Age Age { get; set; }
        public int GroupTypeId { get; set; }
        public GroupType GroupType { get; set; }
        public Guid Creator { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public bool IsClosed { get; set; }
        public bool IsCommon { get; set; }
        public ICollection<Lesson> Lessons { get; set; }
        public Group()
        {
            GroupId = new Guid();
        }
    }
}
