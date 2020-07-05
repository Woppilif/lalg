using System;

namespace BotAppData.Models
{
    public class PageTemplate
    {
        public Guid PageTemplateId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public PageTemplate()
        {
            PageTemplateId = new Guid();
        }
    }
}
