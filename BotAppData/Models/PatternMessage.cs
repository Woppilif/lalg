using System;
using System.ComponentModel.DataAnnotations;

namespace BotAppData.Models
{
    public class PatternMessage
    {
        public Guid PatternMessageId { get; set; }
        public Guid PatternId { get; set; }
        public Pattern Pattern { get; set; }
        public bool IsGreeting { get; set; }
        public string Message { get; set; }
        [DataType(DataType.Time)]
        public DateTime AtTime { get; set; }
        public bool Status { get; set; }

        public PatternMessage()
        {
            PatternMessageId = new Guid();
            AtTime = DateTime.Now;
            IsGreeting = false;
        }

        public string MakeMessage(string url,DateTime dateTime)
        {
            return Message.Replace("<url>", url)
                .Replace("<datetime>", $"{dateTime.ToString("MM/dd/yyyy H:mm")}");
        }

    }
}
