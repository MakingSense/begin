using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BeginMobile.Services.DTO;
using BeginMobile.Services.Interfaces;
using Newtonsoft.Json;

namespace BeginMobile.Services.ManagerServices
{
    public class GroupManager 
    {
        private const string BaseAddress = "http://186.109.86.251:5432/";
        private const string SubAddress = "begin/api/v1/";
        private const string Identifier = "groups";


        private readonly GenericBaseClient<Group> _groupClient =
            new GenericBaseClient<Group>(BaseAddress, SubAddress);

        public GroupManager(){ }

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
            catch (Exception ex)
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
            catch (Exception ex)
            {
                return null;
            }
        }

        private string BuildUrlParams(string name, string cat, string limit, string sections)
        {
            var resultUrl = "";

            if (!(string.IsNullOrEmpty(name) && string.IsNullOrEmpty(cat) &&
                string.IsNullOrEmpty(limit) && string.IsNullOrEmpty(sections)))
            {
                resultUrl = "?q=" + name + "&cat=" + cat + "&limit=" + limit + "&sections=" + sections;
            }
            else if (!(string.IsNullOrEmpty(name) && string.IsNullOrEmpty(cat) && string.IsNullOrEmpty(limit)))
            {
                resultUrl = "?q=" + name + "&cat=" + cat + "&limit=" + limit;
            }
            else if (!(string.IsNullOrEmpty(name) && string.IsNullOrEmpty(cat) && string.IsNullOrEmpty(sections)))
            {
                resultUrl = "?q=" + name + "&cat=" + cat + "&sections=" + sections;
            }
            else if (!(string.IsNullOrEmpty(name) && string.IsNullOrEmpty(sections) && string.IsNullOrEmpty(limit)))
            {
                resultUrl = "?q=" + name + "&sections=" + sections + "&limit=" + limit;
            }
            else if (!(string.IsNullOrEmpty(sections) && string.IsNullOrEmpty(limit) && string.IsNullOrEmpty(cat)))
            {
                resultUrl = "?sections=" + sections + "&limit=" + limit + "&cat=" + cat;
            }
            else if (!(string.IsNullOrEmpty(name) && string.IsNullOrEmpty(cat)))
            {
                resultUrl = "?q=" + name + "&cat=" + cat;
            }
            else if (!(string.IsNullOrEmpty(name) && string.IsNullOrEmpty(limit)))
            {
                resultUrl = "?q=" + name + "&limit=" + limit;
            }
            else if (!(string.IsNullOrEmpty(name) && string.IsNullOrEmpty(sections)))
            {
                resultUrl = "?q=" + name + "&sections=" + sections;
            }
            else if (!(string.IsNullOrEmpty(cat) && string.IsNullOrEmpty(limit)))
            {
                resultUrl = "?cat=" + cat + "&limit=" + limit;
            }
            else if (!(string.IsNullOrEmpty(cat) && string.IsNullOrEmpty(sections)))
            {
                resultUrl = "?cat=" + cat + "&sections=" + sections;
            }
            else if (!(string.IsNullOrEmpty(limit) && string.IsNullOrEmpty(sections)))
            {
                resultUrl = "?limit=" + limit + "&sections=" + sections;
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
            else if (!string.IsNullOrEmpty(sections))
            {
                resultUrl = "?sections=" + sections;
            }

            return resultUrl;
        }
    }
}
