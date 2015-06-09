using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using BeginMobile.Services.DTO;
using BeginMobile.Services.Interfaces;
using BeginMobile.Services.Utils;

namespace BeginMobile.Services.ManagerServices
{
    public class EventManager
    {
        private static readonly string BaseAddress = ConfigBaseAddress.BaseAddress;
        private static readonly string SubAddress = ConfigBaseAddress.SubAddress;
        private static readonly string Identifier = ConfigBaseAddress.IdentifierEvents;

        private readonly GenericBaseClient<ProfileEvent> _eventClient =
            new GenericBaseClient<ProfileEvent>(BaseAddress, SubAddress);

        private static Object _factLockEvent = new Object();
        private static readonly string ThisClassName = typeof(EventManager).Name;
        public EventManager()
        {
        }

        public async Task<ObservableCollection<ProfileEvent>> GetEventsByParams(
            string authToken,
            string name = null,
            string cat = null,
            string limit = null
            )
        {
            try
            {
                var urlGetParams = "?q=" + name + "&cat=" + cat + "&limit=" + limit;
                var resultList = await _eventClient.GetListAsync(authToken, Identifier, urlGetParams);
                return await Task.Run(() => _eventClient.ListToObservableCollection(resultList));
            }
            catch (Exception exception)
            {
                AppContextError.Send(ThisClassName, "GetEventsByParams", exception, null, ExceptionLevel.Application);
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
                AppContextError.Send(ThisClassName, "GetEventById", exception, null, ExceptionLevel.Application);
                return null;
            }
        }
    }
}