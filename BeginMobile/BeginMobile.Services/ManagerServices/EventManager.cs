using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}