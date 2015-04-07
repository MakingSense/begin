﻿using System;
using System.Collections.Generic;
using System.Linq;
using BeginMobile.Services.DTO;
using BeginMobile.Services.Interfaces;

namespace BeginMobile.Services.ManagerServices
{
    public class GroupManager
    {
        private const string BaseAddress = "http://186.109.86.251:5432/";
        private const string SubAddress = "begin/api/v1/";
        private const string Identifier = "groups";


        private readonly GenericBaseClient<Group> _groupClient =
            new GenericBaseClient<Group>(BaseAddress, SubAddress);

        public GroupManager()
        {
        }

        public List<Group> GetGroupsByParams(
            string authToken,
            string name = null,
            string cat = null,
            string limit = null,
            string sections = null
            )
        {
            try
            {
                var urlGetParams = "?q=" + name + "&cat=" + cat + "&limit=" + limit + "&sections=" + sections;
                return _groupClient.GetListAsync(authToken, Identifier, urlGetParams).ToList();
            }
            catch (Exception exeption)
            {
                return null;
            }
        }

        public Group GetGroupById(string authToken, string groupId, string sections = null)
        {
            try
            {
                var urlId = "/" + groupId;

                if (!string.IsNullOrEmpty(sections))
                {
                    urlId += "?sections=" + sections;
                }

                return _groupClient.GetAsync(authToken, Identifier, urlId);
            }
            catch (Exception exception)
            {
                return null;
            }
        }
    }
}