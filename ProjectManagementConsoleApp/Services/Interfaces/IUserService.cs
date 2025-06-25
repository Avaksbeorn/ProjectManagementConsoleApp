using System;
using System.Collections.Generic;
using ProjectManagementConsoleApp.Domain.Entities;
using ProjectManagementConsoleApp.Domain.Enums;

namespace ProjectManagementConsoleApp.Services.Interfaces
{
	/// <summary>
	/// Сервис пользователей.
	/// </summary>
	public interface IUserService
	{
		/// <summary>
		/// Регистрация пользователя.
		/// </summary>
		User Register(string login, string password, Role role);

		/// <summary>
		/// Все сотрудники.
		/// </summary>
		IEnumerable<User> GetAllEmployees();

		/// <summary>
		/// Пользователь по Id.
		/// </summary>
		User GetById(Guid id);
	}
}
