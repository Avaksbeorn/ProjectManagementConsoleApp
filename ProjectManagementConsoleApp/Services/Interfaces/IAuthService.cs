using ProjectManagementConsoleApp.Domain.Entities;

namespace ProjectManagementConsoleApp.Services.Interfaces
{
	public interface IAuthService
	{
		/// <summary>
		/// Проверяет логин/пароль.
		/// </summary>
		User Authenticate(string login, string password);
	}
}
