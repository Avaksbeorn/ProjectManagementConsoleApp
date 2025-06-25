using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using ProjectManagementConsoleApp.Domain.Entities;
using ProjectManagementConsoleApp.Infrastructure.Repositories.Interfaces;

namespace ProjectManagementConsoleApp.Infrastructure.Repositories
{
	/// <summary>
	/// JSON-репозиторий задач.
	/// </summary>
	public class JsonTaskRepository : ITaskRepository
	{
		private readonly string _filePath;
		private readonly List<ProjectTask> _tasks;

		/// <summary>
		/// Инициализация и загрузка из файла.
		/// </summary>
		public JsonTaskRepository(string filePath = "tasks.json")
		{
			_filePath = filePath;
			// Для отладки: вывести, откуда читаем
			Console.WriteLine($"[TaskRepo] Loading from: {Path.GetFullPath(_filePath)}");

			if (File.Exists(_filePath))
			{
				var json = File.ReadAllText(_filePath);
				_tasks = JsonSerializer.Deserialize<List<ProjectTask>>(json, new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				})
				?? new List<ProjectTask>();
			}
			else
			{
				_tasks = new List<ProjectTask>();
				SaveChanges();
			}
		}

		/// <summary>Все задачи.</summary>
		public IEnumerable<ProjectTask> GetAll() => _tasks;

		/// <summary>Задачи по пользователю.</summary>
		public IEnumerable<ProjectTask> GetByUserId(Guid userId) =>
			_tasks.Where(t => t.AssignedToUserId == userId);

		/// <summary>Задача по идентификатору.</summary>
		public ProjectTask GetById(Guid id) =>
			_tasks.FirstOrDefault(t => t.Id == id);

		/// <summary>Добавить задачу.</summary>
		public void Add(ProjectTask task)
		{
			_tasks.Add(task);
			SaveChanges();
		}

		/// <summary>Обновить задачу.</summary>
		public void Update(ProjectTask task)
		{
			var idx = _tasks.FindIndex(t => t.Id == task.Id);
			if (idx == -1) throw new InvalidOperationException("Задача не найдена.");

			_tasks[idx] = task;
			SaveChanges();
		}

		/// <summary>Сохранить всё в файл.</summary>
		private void SaveChanges()
		{
			var json = JsonSerializer.Serialize(_tasks, new JsonSerializerOptions { WriteIndented = true });
			File.WriteAllText(_filePath, json);
		}
	}
}
