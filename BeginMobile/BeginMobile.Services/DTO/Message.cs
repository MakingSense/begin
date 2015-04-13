using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Newtonsoft.Json;

namespace BeginMobile.Services.DTO
{
    public class Message
    {
        [JsonProperty("id")]
        public string Id { set; get; }

        [JsonProperty("thread_id")]
        public string ThreadId { set; get; }

        [JsonProperty("subject")]
        public string Subject { set; get; }

        [JsonProperty("message")]
        public string MessageContent { set; get; }

        [JsonProperty("date_sent")]
        public string DateSent { set; get; }

        [JsonProperty("sender")]
        public User Sender { set; get; }


        //TODO remove from here to down
        public string Type { set; get; }

        public string IsRead { set; get; }

        private static List<Message> _listMessages;

        public static List<Message> Messages
        {
            get
            {
                string[] type = {"Inbox", "Sent", "Compose"};
                string[] isRead = {"0", "1", "0"};
                if (_listMessages == null)
                {
                    _listMessages = new List<Message>();
                    for (var i = 0; i < 10; i++)
                    {
                        var message = new Message()
                                      {
                                          Id = i.ToString(),
                                          Subject = "Re: Contact " + i,
                                          MessageContent = "Content of message " + i,
                                          Type = type[new Random().Next(0, 2)],
                                          DateSent = DateTime.Now.ToString(),
                                          IsRead = isRead[new Random().Next(0, 2)],
                                      };

                        _listMessages.Add(message);
                    }
                }

                return _listMessages;
            }
        }
    }


    public class GroupingMessage
    {
        public int CountByGroup { set; get; }

        public ObservableCollection<IGrouping<string, Message>> MessagesGroup { set; get; }
    }
}