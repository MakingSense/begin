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
        public string SenderName { set; get; }
        public string ThreadUnRead { get; set; }
        //public bool ThreadUnRead { get; set; }
        public User Sender { set; get; }
        public List<Message> Messages { get; set; } 
    }
}