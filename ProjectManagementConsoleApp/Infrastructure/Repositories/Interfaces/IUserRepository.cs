using System;
using System.Collections.Generic;
using ProjectManagementConsoleApp.Domain.Entities;

namespace ProjectManagementConsoleApp.Infrastructure.Repositories.Interfaces
{
	/// <summary>
	/// Репозиторий пользователей.
	/// </summary>
	public interface IUserRepository
	{
		/// <summary>
		/// Все пользователи.
		/// </summary>
		IEnumerable<User> GetAll();

		/// <summary>
		/// Пользователь по Id.
		/// </summary>
		User GetById(Guid id);

		/// <summary>
		/// Пользователь по логину.
		/// </summary>
		User GetByLogin(string login);

		/// <summary>
		/// Добавить пользователя.
		/// </summary>
		void Add(User user);

		/// <summary>
		/// Обновить пользователя.
		/// </summary>
		void Update(User user);
	}
}
