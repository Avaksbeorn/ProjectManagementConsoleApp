using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using ProjectManagementConsoleApp.Domain.Entities;
using ProjectManagementConsoleApp.Domain.Enums;
using ProjectManagementConsoleApp.Infrastructure.Repositories.Interfaces;
using ProjectManagementConsoleApp.Services.Interfaces;

namespace ProjectManagementConsoleApp.Services
{
	/// <summary>
	/// Сервис задач.
	/// </summary>
	public class TaskService : ITaskService
	{
		private readonly ITaskRepository _taskRepo;
		private readonly IUserRepository _userRepo;
		private readonly ILogger<TaskService> _logger;

		/// <summary>
		/// Инициализация сервиса.
		/// </summary>
		public TaskService(
			ITaskRepository taskRepo,
			IUserRepository userRepo,
			ILogger<TaskService> logger)
		{
			_taskRepo = taskRepo;
			_userRepo = userRepo;
			_logger = logger;
		}

		/// <summary>
		/// Создание задачи.
		/// </summary>
		public ProjectTask CreateTask(string title, string description, Guid assignedToUserId)
		{
			if (_userRepo.GetById(assignedToUserId) == null)
			{
				throw new ArgumentException("Сотрудник для назначения не найден.");
			}

			var task = new ProjectTask(title, description, assignedToUserId);
			_taskRepo.Add(task);

			_logger.LogInformation(
				"Создана задача {TaskId} с заголовком '{Title}', назначенная пользователю {UserId}.",
				task.Id, task.Title, assignedToUserId);

			return task;
		}

		/// <summary>
		/// Получение задач.
		/// </summary>
		public IEnumerable<ProjectTask> GetTasksForUser(Guid userId) =>
			_taskRepo.GetByUserId(userId);

		/// <summary>
		/// Изменение статуса.
		/// </summary>
		public void ChangeStatus(Guid taskId, Domain.Enums.TaskStatus newStatus, Guid performedByUserId)
		{
			var task = _taskRepo.GetById(taskId)
				?? throw new ArgumentException("Задача не найдена.");
			var user = _userRepo.GetById(performedByUserId)
				?? throw new ArgumentException("Пользователь не найден.");

			if (user.Role == Role.Employee && task.AssignedToUserId != user.Id)
			{
				throw new InvalidOperationException("Нет прав менять статус этой задачи.");
			}

			var oldStatus = task.Status;
			task.Status = newStatus;
			_taskRepo.Update(task);

			_logger.LogInformation(
				"Пользователь {UserId} изменил статус задачи {TaskId} с {OldStatus} на {NewStatus}.",
				performedByUserId, taskId, oldStatus, newStatus);
		}
	}
}
