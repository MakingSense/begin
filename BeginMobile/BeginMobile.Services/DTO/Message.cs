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

        private static List<Message> _listMessages;

        public static List<Message> Messages{
            get
            {
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
                        };

                        _listMessages.Add(message);
                    }
                }

                return _listMessages;
            }
        }
    }
}
