namespace BeginMobile.Services.Models
{
    public class Contact
    {
        public string Icon { get; set; }
        public string NameSurname { get; set; }
        public string UserName { get; set; }
        public string Registered { get; set; }
        public string Id { get; set; }
        public string Url { get; set; }
        public string Email { get; set; }

        public string FirstName
        {
            get
            {
                return NameSurname.Split(' ')[0];
            }
        }
        public string References { get; set; }

        public string Relationship { get; set; }
    }
}
