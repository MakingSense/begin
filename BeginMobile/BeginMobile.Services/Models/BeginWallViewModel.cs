using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeginMobile.Services.Models
{
    public class BeginWallViewModel
    {
        public string Name { set; get; }

        public string ExtraText { set; get; }
        public string Description { set; get; }

        public string Reason { set; get; }

        public string OtherDescription { set; get; }

        public string Date { set; get; }

        public bool IsMarked { set; get; }
    }
}
