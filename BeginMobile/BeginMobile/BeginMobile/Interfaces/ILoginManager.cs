using BeginMobile.Services.DTO;

namespace BeginMobile.Interfaces
{
    public interface ILoginManager {
		void ShowMainPage(LoginUser loginUser);

        void ShowUploaderPage();
		void Logout();
	}
}

