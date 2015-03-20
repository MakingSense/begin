using Newtonsoft.Json;

namespace BeginMobile.Services.DTO
{
    public class ProfileShop
    {
        [JsonProperty("product_id")]
        public int ProductId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("creation_date")]
        public string CreationDate { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("stock_status")]
        public string StockStatus { get; set; }

        [JsonProperty("regular_price")]
        public string RegularPrice { get; set; }

        private string _price;
        [JsonProperty("price")]
        public string Price {
            set
            {
                if (string.IsNullOrEmpty(value) || value == "0")
                {
                    _price = "Free";
                }
                else
                {
                    _price = value;
                }
            }

            get
            {
                return _price;
            }
        }
    }
}