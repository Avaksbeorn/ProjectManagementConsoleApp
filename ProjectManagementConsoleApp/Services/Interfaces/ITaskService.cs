using System;
using System.Collections.Generic;
using ProjectManagementConsoleApp.Domain.Entities;
using ProjectManagementConsoleApp.Domain.Enums;
using TaskStatus = ProjectManagementConsoleApp.Domain.Enums.TaskStatus;

namespace ProjectManagementConsoleApp.Services.Interfaces
{
	/// <summary>
	/// Сервис задач.
	/// </summary>
	public interface ITaskService
	{
		/// <summary>
		/// Создать задачу.
		/// </summary>
		ProjectTask CreateTask(string title, string description, Guid assignedToUserId);

		/// <summary>
		/// Задачи пользователя.
		/// </summary>
		IEnumerable<ProjectTask> GetTasksForUser(Guid userId);

		/// <summary>
		/// Изменить статус.
		/// </summary>
		void ChangeStatus(Guid taskId, TaskStatus newStatus, Guid performedByUserId);
	}
}
