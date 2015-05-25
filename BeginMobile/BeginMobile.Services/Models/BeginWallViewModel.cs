using System;
using BeginMobile.Services.Utils;

namespace BeginMobile.Services.Models
{
    public class BeginWallViewModel
    {
        public string ItemId { set; get; }

        public string Title { set; get; }

        public string DisplayName { set; get; }

        public string DisplayNameTwo { set; get; }

        public string ExtraText { set; get; }

        public string Description { set; get; }

        public string Reason { set; get; }

        public string OtherDescription { set; get; }

        public string Date { set; get; }

        public bool IsMarked { set; get; }

        public string Component { set; get; }

        public string Type { set; get; }

        public string PublicDate { set; get; }

        public string Icon { get; set; }

        private string _publicDateShort;
        public string PublicDateShort
        {
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _publicDateShort = "";
                }
                else
                {
                    var groupDate = Convert.ToDateTime(value);
                    _publicDateShort = DateConverter.GetTimeShortSpan(groupDate);
                }
            }
            get
            {
                return _publicDateShort;
            }
        }
    }
}