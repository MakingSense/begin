using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeginMobile.Services.DTO
{
    public class Message
    {
        public string Id { set; get; }

        public string Title { set; get; }

        public string Content { set; get; }

        public string Type { set; get; }

        public string Thumbnail { set; get; }

        public string CreateDate { set; get; }

        private static List<Message> _listMessages;

        public static List<Message> Messages{
            get
            {
                string[] type = {"Inbox", "Sent", "Compose"};
                if (_listMessages == null)
                {
                    _listMessages = new List<Message>();
                    for (var i = 0; i < 10; i++)
                    {
                        var message = new Message()
                        {
                            Id = i.ToString(),
                            Title = "Title "+i,
                            Content = "Content of message "+i,
                            Type = type[new Random().Next(0, 2)],
                            CreateDate = DateTime.Now.ToString(),
                        };

                        _listMessages.Add(message);
                    }
                }

                return _listMessages;
            }
        }
    }
}
