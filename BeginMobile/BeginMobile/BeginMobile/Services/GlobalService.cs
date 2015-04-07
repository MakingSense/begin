using System;
using System.Collections.Generic;
using System.Text;
using BeginMobile.Services.ManagerServices;
using BeginMobile.Services.DTO;

namespace BeginMobile.Services
{
    public class GlobalService
    {
        private readonly  GlobalManager _globalManager;

        private List<string> _sections;
        public List<string> Sections
        {
            private set
            {
                _sections = value == null ? new List<string>(): value;
            }
            get
            {
                return _sections;
            }
        }

        private List<string> _groupSections;
        public List<string> GroupSections
        {
            private set
            {
                _groupSections = value == null ? new List<string>() : value;
            }
            get
            {
                return _groupSections;
            }
        }

        private List<string> _wallType;
        public List<string> WallType
        {
            private set
            {
                _wallType = value == null ? new List<string>() : value;
            }
            get
            {
                return _wallType;
            }
        }

        private List<string> _wallFilter;
        public List<string> WallFilter
        {
            private set
            {
                _wallFilter = value == null ? new List<string>() : value;
            }
            get
            {
                return _wallFilter;
            }
        }


        public GlobalService()
        {
            _globalManager = new GlobalManager();
            LoadMeOptions();
            LoadGroupSections();

        }

        private void LoadMeOptions()
        {
            var resultRequest = _globalManager.GetMeOptions();
            if (resultRequest != null)
            {
                Sections = resultRequest.Sections;
                WallType = resultRequest.WallOptions.Type;
                WallFilter = resultRequest.WallOptions.Filter;
            }

        }

        private void LoadGroupSections()
        {
            var resultRequest = _globalManager.GetGroupOptions();
            if (resultRequest != null)
            {
                GroupSections = resultRequest.GroupSections;
            }
        }
    }
}
