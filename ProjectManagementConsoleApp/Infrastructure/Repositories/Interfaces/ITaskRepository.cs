using System;
using System.Collections.Generic;
using ProjectManagementConsoleApp.Domain.Entities;

namespace ProjectManagementConsoleApp.Infrastructure.Repositories.Interfaces
{
	/// <summary>
	/// Репозиторий задач.
	/// </summary>
	public interface ITaskRepository
	{
		/// <summary>
		/// Все задачи.
		/// </summary>
		IEnumerable<ProjectTask> GetAll();

		/// <summary>
		/// Задачи пользователя.
		/// </summary>
		IEnumerable<ProjectTask> GetByUserId(Guid userId);

		/// <summary>
		/// Задача по Id.
		/// </summary>
		ProjectTask GetById(Guid id);

		/// <summary>
		/// Добавить задачу.
		/// </summary>
		void Add(ProjectTask task);

		/// <summary>
		/// Обновить задачу.
		/// </summary>
		void Update(ProjectTask task);
	}
}
