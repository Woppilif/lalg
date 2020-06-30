using System;

namespace BotAppData.Models
{
    public class Subscription
    {
        public Guid SubscriptionId { get; set; }
        public long UserId { get; set; }
        public User Users { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public bool IsActive { get; set; }
        public Subscription()
        {
            SubscriptionId = new Guid();
        }
    }
}
