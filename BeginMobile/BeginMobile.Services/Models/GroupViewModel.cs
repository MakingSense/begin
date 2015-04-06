using System;
using BeginMobile.Services.Utils;

namespace BeginMobile.Services.Models
{
    public class GroupViewModel
    {
        private const string PrivateGroup = "Private Group";
        private const string PublicGroup = "Public Group";
        private const string Private = "private";

        private string _statusGroup;

        public string StatusGroup
        {
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _statusGroup = "";
                }
                else
                {
                    _statusGroup = value == Private ? PrivateGroup : PublicGroup;
                }
            }
            get { return _statusGroup; }
        }

        private string _textActiveDate;

        public string TextActiveDate
        {
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _textActiveDate = "";
                }
                else
                {
                    var groupDate = Convert.ToDateTime(value);
                    _textActiveDate = DateConverter.GetTimeSpan(groupDate);
                }
            }
            get { return _textActiveDate; }
        }

        public string Description { set; get; }

        public string TextDate { set; get; }

        public string Name { set; get; }
    }
}