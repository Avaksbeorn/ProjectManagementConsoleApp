using System;
using System.Security.Cryptography;
using System.Text;
using ProjectManagementConsoleApp.Domain.Entities;
using ProjectManagementConsoleApp.Infrastructure.Repositories.Interfaces;
using ProjectManagementConsoleApp.Services.Interfaces;

namespace ProjectManagementConsoleApp.Services
{
	/// <summary>
	/// Сервис аутентификации.
	/// </summary>
	public class AuthService : IAuthService
	{
		private readonly IUserRepository _userRepo;

		/// <summary>
		/// Инициализация сервиса.
		/// </summary>
		public AuthService(IUserRepository userRepo)
		{
			_userRepo = userRepo;
		}

		/// <summary>
		/// Аутентификация пользователя.
		/// </summary>
		public User Authenticate(string login, string password)
		{
			var user = _userRepo.GetByLogin(login);
			if (user == null) return null;

			var hash = ComputeHash(password);
			return user.PasswordHash == hash ? user : null;
		}

		/// <summary>
		/// Вычисление хэша.
		/// </summary>
		private static string ComputeHash(string input)
		{
			using var sha = SHA256.Create();
			var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
			var sb = new StringBuilder();
			foreach (var b in bytes)
				sb.Append(b.ToString("x2"));
			return sb.ToString();
		}
	}
}
