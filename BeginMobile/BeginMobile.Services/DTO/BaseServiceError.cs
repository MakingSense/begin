using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BeginMobile.Services.DTO
{
    public class BaseServiceError
    {
        [JsonProperty("errors")]
        public List<ServiceError> Errors { set; get; }

        [JsonProperty("error")]
        public string Error { set; get; }

        private bool _hasError;
        public bool HasError
        {
            get
            {
                if (Errors != null)
                {
                    _hasError = Errors.Count > 0;
                }
                else
                {
                    _hasError = !string.IsNullOrEmpty(Error);
                }

                return _hasError;
            }
        }
    }
}
