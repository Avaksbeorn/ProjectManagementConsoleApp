using System;
using ProjectManagementConsoleApp.Domain.Enums;
using System.Text.Json.Serialization;
using TaskStatus = ProjectManagementConsoleApp.Domain.Enums.TaskStatus;

namespace ProjectManagementConsoleApp.Domain.Entities
{
	/// <summary>
	/// Представляет задачу в рамках проекта.
	/// </summary>
	public class ProjectTask
	{
		/// <summary>
		/// Уникальный идентификатор задачи.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Заголовок задачи.
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Подробное описание задачи.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Текущий статус задачи.
		/// </summary>
		public TaskStatus Status { get; set; }

		/// <summary>
		/// Идентификатор пользователя, которому назначена задача.
		/// </summary>
		public Guid AssignedToUserId { get; set; }

		/// <summary>
		/// Пустой конструктор для JSON-десериализации.
		/// </summary>
		public ProjectTask() { }

		/// <summary>
		/// Создаёт новую задачу с указанными заголовком, описанием и назначенным пользователем.
		/// </summary>
		/// <param name="title">Заголовок создаваемой задачи.</param>
		/// <param name="description">Описание создаваемой задачи.</param>
		/// <param name="assignedToUserId">Идентификатор пользователя, которому назначена задача.</param>
		public ProjectTask(string title, string description, Guid assignedToUserId)
		{
			Id = Guid.NewGuid();
			Title = title;
			Description = description;
			Status = TaskStatus.ToDo;
			AssignedToUserId = assignedToUserId;
		}
	}
}
