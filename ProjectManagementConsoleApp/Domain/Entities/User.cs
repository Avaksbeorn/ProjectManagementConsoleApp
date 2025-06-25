using System;
using ProjectManagementConsoleApp.Domain.Enums;
using System.Data;

namespace ProjectManagementConsoleApp.Domain.Entities
{
	/// <summary>
	/// Представляет пользователя.
	/// </summary>
	public class User
	{
		/// <summary>
		/// Уникальный идентификатор пользователя.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Логин пользователя.
		/// </summary>
		public string Login { get; set; }

		/// <summary>
		/// Хэш пароля пользователя.
		/// </summary>
		public string PasswordHash { get; set; }

		/// <summary>
		/// Роль пользователя в системе.
		/// </summary>
		public Role Role { get; set; }

		/// <summary>
		/// Конструктор для JSON-десериализации.
		/// </summary>
		public User() { }

		/// <summary>
		/// Инициализирует новый экземпляр <see cref="User"/> с указанными данными.
		/// </summary>
		/// <param name="login">Логин создаваемого пользователя.</param>
		/// <param name="passwordHash">Хэш пароля создаваемого пользователя.</param>
		/// <param name="role">Роль пользователя в системе.</param>
		public User(string login, string passwordHash, Role role)
		{
			Id = Guid.NewGuid();
			Login = login;
			PasswordHash = passwordHash;
			Role = role;
		}
	}
}
