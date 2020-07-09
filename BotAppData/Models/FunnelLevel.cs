using System;
using System.Collections.Generic;
using System.Text;

namespace BotAppData.Models
{
    public class FunnelLevel
    {
        public Guid FunnelLevelId { get; set; }
        public Guid FunnelId { get; set; }
        public Funnel Funnel { get; set; }
        public Guid GroupId { get; set; }
        public Group Group { get; set; }
        public Guid? ProductId { get; set; }
        public Product Product { get; set; }
        public FunnelLevel()
        {
            FunnelLevelId = new Guid();
        }
    }
}
