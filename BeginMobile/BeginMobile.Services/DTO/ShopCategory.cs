using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BeginMobile.Services.DTO
{
    public class ShopCategory: BaseServiceError
    {
        [JsonProperty("cat_id")]
        public string CatId { set; get; }

        [JsonProperty("name")]
        public string Name { set; get; }

        [JsonProperty("parent_cat_id")]
        public string ParentCatId { set; get; }
    }
}
