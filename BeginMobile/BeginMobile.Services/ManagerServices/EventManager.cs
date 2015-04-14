using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeginMobile.Services.DTO;
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

        public EventManager()
        {
        }

        public async  Task<List<ProfileEvent>> GetEventsByParams(
            string authToken,
            string name = null,
            string cat = null,
            string limit = null
            )
        {
            try
            {
                var urlGetParams = "?q=" + name + "&cat=" + cat + "&limit=" + limit;
                return await _eventClient.GetListAsync(authToken, Identifier, urlGetParams);
            }
            catch (Exception exception)
            {
                //TODO log exception
                return null;
            }
        }

        public ProfileEvent GetEventById(string authToken, string eventId)
        {
            try
            {
                var urlId = "/" + eventId;
                return _eventClient.Get(authToken, Identifier, urlId);
            }
            catch (Exception exception)
            {
                //TODO log exception
                return null;
            }
        }
    }
}