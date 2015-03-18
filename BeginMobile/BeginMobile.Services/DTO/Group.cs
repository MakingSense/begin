using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeginMobile.Services.DTO
{
    public class Group
    {
        public string Id { set; get; }

        public string Name { set; get; }

        public string Category { set; get; }

        public string Type { set; get; }

        public string Members { set; get; }

        private static List<Group> _listGroup;

        public static List<Group> ListGroup
        {
            get
            {
                if (_listGroup == null)
                {
                    _listGroup = new List<Group>();
                    for (var i = 0; i < 15; i++)
                    {
                        var group = new Group()
                        {
                            Id = i.ToString(),
                            Name = "Name " + i,
                            Type = "Type " + i,
                            Category = "Category " + i,
                            Members = ((new Random().Next(1, 100)) + i).ToString(), 
                        };
                        _listGroup.Add(group);
                    }
                }

                return _listGroup;
            }
        }
    }
}
