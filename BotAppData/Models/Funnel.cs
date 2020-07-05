using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotAppData.Models
{
    public class Funnel
    {
        public Guid FunnelId { get; set; }
        public string Name { get; set; }
        public Guid? PageTemplateId { get; set; }
        public PageTemplate PageTemplate { get; set; }
        public decimal BonusAmount { get; set; }
        public bool IsActive { get; set; }
        public ICollection<FunnelLevel> FunnelLevels { get; set; }
        public Guid CreatorId { get; set; }
        public Funnel()
        {
            FunnelId = new Guid();
            IsActive = true;
            BonusAmount = 0m;
        }
    }
}
