using System.Collections.Generic;
using BeginMobile.Services.DTO;

namespace BeginMobile.Services.Models
{
    public class MessageViewModel
    {
        public string Id { set; get; }
        public string ThreadId { set; get; }
        public string Subject { set; get; }
        public string MessageContent { set; get; }
        public string DateSent { set; get; }
        public string Sender { set; get; }
        public List<Message> Messages { get; set; } 
    }
}