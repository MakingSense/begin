using BeginMobile.Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeginMobile.Interfaces
{
    public interface ILoginManager {
		void ShowMainPage(LoginUser loginUser);
		void Logout();
	}
}

