using System;
using System.Collections.Generic;
using System.Linq;
using ProjectManagementConsoleApp.Domain.Entities;
using ProjectManagementConsoleApp.Domain.Enums;
using ProjectManagementConsoleApp.Infrastructure.Repositories.Interfaces;
using ProjectManagementConsoleApp.Services.Interfaces;

namespace ProjectManagementConsoleApp.Services
{
	/// <summary>
	/// Сервис пользователей.
	/// </summary>
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepo;

		/// <summary>
		/// Инициализация сервиса.
		/// </summary>
		public UserService(IUserRepository userRepo)
		{
			_userRepo = userRepo;
		}

		/// <summary>
		/// Регистрация пользователя.
		/// </summary>
		public User Register(string login, string password, Role role)
		{
			if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
				throw new ArgumentException("Логин и пароль не могут быть пустыми.");

			var hash = AuthService_Reflection.ComputeHash(password);
			var user = new User(login, hash, role);
			_userRepo.Add(user);
			return user;
		}

		/// <summary>
		/// Все сотрудники.
		/// </summary>
		public IEnumerable<User> GetAllEmployees() =>
			_userRepo.GetAll().Where(u => u.Role == Role.Employee);

		/// <summary>
		/// Пользователь по Id.
		/// </summary>
		public User GetById(Guid id) =>
			_userRepo.GetById(id);
	}

	/// <summary>
	/// Рефлексия ComputeHash.
	/// </summary>
	internal static class AuthService_Reflection
	{
		private static readonly
			System.Reflection.MethodInfo? _computeHash =
				typeof(AuthService).GetMethod(
					"ComputeHash", System.Reflection.BindingFlags.NonPublic 
					| System.Reflection.BindingFlags.Static);

		/// <summary>
		/// Вычисление хэша.
		/// </summary>
		public static string ComputeHash(string password) =>
			(string)_computeHash.Invoke(null, new object[] { password });
	}
}
