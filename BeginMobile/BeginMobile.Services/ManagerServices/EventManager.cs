using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BeginMobile.Services.DTO;
using Newtonsoft.Json;
using BeginMobile.Services.Interfaces;

namespace BeginMobile.Services.ManagerServices
{
    public class EventManager
    {
        private const string BaseAddress = "http://186.109.86.251:5432/";
        private const string SubAddress = "begin/api/v1/";
        private const string Identifier = "events";

        private readonly GenericBaseClient<ProfileEvent> _eventClient =
            new GenericBaseClient<ProfileEvent>(BaseAddress, SubAddress);

        public EventManager() { }

        public List<ProfileEvent> GetEventsByParams(
            string authToken, 
            string name = null, 
            string cat = null, 
            string limit = null
            )
        {
            try
            {
                var urlGetParams = "?q=" + name + "&cat=" + cat + "&limit=" + limit;
                return _eventClient.GetListAsync(authToken, Identifier, urlGetParams).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ProfileEvent GetEventById(string authToken, string eventId)
        {
            try
            {
                var urlId = "/" + eventId;
                return _eventClient.GetAsync(authToken, Identifier, urlId);

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private string BuildUrlParams(string name, string cat, string limit)
        {
            var resultUrl = "";

            if (!(string.IsNullOrEmpty(name) && string.IsNullOrEmpty(cat) && string.IsNullOrEmpty(limit)))
            {
                resultUrl = "?q=" + name + "&cat=" + cat + "&limit=" + limit;
            }
            else if (!(string.IsNullOrEmpty(name) && string.IsNullOrEmpty(cat)))
            {
                resultUrl = "?q=" + name + "&cat=" + cat;
            }
            else if (!(string.IsNullOrEmpty(cat) && string.IsNullOrEmpty(limit)))
            {
                resultUrl = "?cat=" + cat + "&limit=" + limit;
            }
            else if (!(string.IsNullOrEmpty(name) && string.IsNullOrEmpty(limit)))
            {
                resultUrl = "?q=" + name + "&limit=" + limit;
            }
            else if (!string.IsNullOrEmpty(name))
            {
                resultUrl = "?q=" + name;
            }
            else if (!string.IsNullOrEmpty(cat))
            {
                resultUrl = "?cat=" + cat;
            }
            else if (!string.IsNullOrEmpty(limit))
            {
                resultUrl = "?limit=" + limit;
            }

            return resultUrl;
        }

    }
}
