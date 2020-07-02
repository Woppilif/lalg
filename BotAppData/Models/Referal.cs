using System.ComponentModel.DataAnnotations;

namespace BotAppData.Models
{
    public class Referal
    {
        [Key]
        public int ReferalId { get; set; }
        public long? UserId { get; set; }
        public User Users { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
        public bool IsCommon { get; set; }
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }
    }
}
