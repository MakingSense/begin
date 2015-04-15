using BeginMobile.Services.DTO;
using Xamarin.Forms;

namespace BeginMobile.Services.Models
{
    public class NotificationViewModel
    {
        public string NotificationDescription { get; set; }
        public string IntervalDate { get; set; }
        public string Id { get; set; }
        public string ItemId { get; set; }
        public string Component { get; set; }
        public string Action { get; set; }
        public Group GroupViewModel { get; set; }
        public SubUser UserViewModel { get; set; }
    }
}