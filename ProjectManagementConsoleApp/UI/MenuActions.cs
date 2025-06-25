using System;
using System.Linq;
using ProjectManagementConsoleApp.Domain.Entities;
using ProjectManagementConsoleApp.Domain.Enums;
using ProjectManagementConsoleApp.Services.Interfaces;
using TaskStatus = ProjectManagementConsoleApp.Domain.Enums.TaskStatus;

namespace ProjectManagementConsoleApp.UI
{
	/// <summary>
	/// Действия меню.
	/// </summary>
	public class MenuActions
	{
		private readonly IUserService _userService;
		private readonly ITaskService _taskService;

		/// <summary>
		/// Инициализация.
		/// </summary>
		public MenuActions(
			IUserService userService,
			ITaskService taskService)
		{
			_userService = userService;
			_taskService = taskService;
		}

		/// <summary>
		/// Меню менеджера.
		/// </summary>
		public void ShowManagerMenu(User manager)
		{
			while (true)
			{
				Console.WriteLine("\n--- Меню менеджера ---");
				Console.WriteLine("1. Зарегистрировать нового сотрудника");
				Console.WriteLine("2. Создать и назначить задачу");
				Console.WriteLine("3. Просмотреть всех сотрудников");
				Console.WriteLine("4. Выйти");
				Console.Write("Выбор: ");
				var opt = Console.ReadLine();

				switch (opt)
				{
					case "1": RegisterEmployee(); break;
					case "2": CreateAndAssignTask(); break;
					case "3": ViewAllEmployees(); break;
					case "4":
						Console.WriteLine("Выход из системы...");
						return;
					default:
						Console.WriteLine("Неверный выбор.");
						break;
				}
			}
		}

		/// <summary>
		/// Регистрация сотрудника.
		/// </summary>
		private void RegisterEmployee()
		{
			Console.Write("Логин нового сотрудника: ");
			var login = Console.ReadLine();
			Console.Write("Пароль: ");
			var pwd = ReadPassword();
			try
			{
				var emp = _userService.Register(login, pwd, Role.Employee);
				Console.WriteLine($"Сотрудник '{emp.Login}' зарегистрирован.");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Ошибка: {ex.Message}");
			}
		}

		/// <summary>
		/// Создание задачи.
		/// </summary>
		private void CreateAndAssignTask()
		{
			var emps = _userService.GetAllEmployees().ToList();
			if (!emps.Any())
			{
				Console.WriteLine("Сотрудники ещё не зарегистрированы.");
				return;
			}

			Console.WriteLine("Выберите сотрудника:");
			for (int i = 0; i < emps.Count; i++)
				Console.WriteLine($"{i + 1}. {emps[i].Login}");
			Console.Write("Выбор: ");
			if (!int.TryParse(Console.ReadLine(), out int idx) ||
				idx < 1 || idx > emps.Count)
			{
				Console.WriteLine("Неверный выбор.");
				return;
			}
			var emp = emps[idx - 1];

			Console.Write("Название задачи: ");
			var title = Console.ReadLine();
			Console.Write("Описание: ");
			var desc = Console.ReadLine();

			try
			{
				var task = _taskService.CreateTask(title, desc, emp.Id);
				Console.WriteLine($"Задача '{task.Title}' назначена пользователю {emp.Login}.");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Ошибка: {ex.Message}");
			}
		}

		/// <summary>
		/// Просмотр сотрудников.
		/// </summary>
		private void ViewAllEmployees()
		{
			var emps = _userService.GetAllEmployees();
			Console.WriteLine("\nСотрудники:");
			foreach (var e in emps)
				Console.WriteLine($"- {e.Id} | {e.Login}");
		}

		/// <summary>
		/// Меню сотрудника.
		/// </summary>
		public void ShowEmployeeMenu(User employee)
		{
			while (true)
			{
				Console.WriteLine("\n--- Меню сотрудника ---");
				Console.WriteLine("1. Показать мои задачи");
				Console.WriteLine("2. Изменить статус задачи");
				Console.WriteLine("3. Выйти");
				Console.Write("Выбор: ");
				var opt = Console.ReadLine();

				switch (opt)
				{
					case "1": ViewMyTasks(employee); break;
					case "2": ChangeMyTaskStatus(employee); break;
					case "3":
						Console.WriteLine("Выход из системы...");
						return;
					default:
						Console.WriteLine("Неверный выбор.");
						break;
				}
			}
		}

		/// <summary>
		/// Просмотр моих задач.
		/// </summary>
		private void ViewMyTasks(User emp)
		{
			var tasks = _taskService.GetTasksForUser(emp.Id).ToList();
			if (!tasks.Any())
			{
				Console.WriteLine("Нет назначенных задач.");
				return;
			}

			Console.WriteLine("\nМои задачи:");
			foreach (var t in tasks)
				Console.WriteLine($"- {t.Id} | {t.Title} | {t.Description} | Статус: {t.Status}");
		}

		/// <summary>
		/// Изменение статуса задачи.
		/// </summary>
		private void ChangeMyTaskStatus(User emp)
		{
			var tasks = _taskService.GetTasksForUser(emp.Id).ToList();

			if (!tasks.Any())
			{
				Console.WriteLine("Нет задач для обновления.");
				return;
			}

			Console.WriteLine("Выберите задачу:");

			for (int i = 0; i < tasks.Count; i++)
			{
				Console.WriteLine($"{i + 1}. {tasks[i].Title} ({tasks[i].Status})");
			}

			Console.Write("Выбор: ");

			if (!int.TryParse(Console.ReadLine(), out int idx) ||
				idx < 1 || idx > tasks.Count)
			{
				Console.WriteLine("Неверный выбор.");
				return;
			}

			var task = tasks[idx - 1];

			Console.WriteLine("Выберите новый статус:");
			var statuses = Enum.GetValues(typeof(TaskStatus)).Cast<TaskStatus>().ToList();

			for (int i = 0; i < statuses.Count; i++)
			{
				Console.WriteLine($"{i + 1}. {statuses[i]}");
			}
			Console.Write("Выбор: ");

			if (!int.TryParse(Console.ReadLine(), out int si) ||
				si < 1 || si > statuses.Count)
			{
				Console.WriteLine("Неверный выбор.");
				return;
			}

			var newStatus = statuses[si - 1];

			try
			{
				_taskService.ChangeStatus(task.Id, newStatus, emp.Id);
				Console.WriteLine($"Статус изменён на {newStatus}.");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Ошибка: {ex.Message}");
			}
		}

		/// <summary>
		/// Чтение пароля.
		/// </summary>
		private string ReadPassword()
		{
			var pwd = string.Empty;
			ConsoleKey key;
			do
			{
				var info = Console.ReadKey(intercept: true);
				key = info.Key;
				if (key == ConsoleKey.Backspace && pwd.Length > 0)
				{
					Console.Write("\b \b");
					pwd = pwd[0..^1];
				}
				else if (!char.IsControl(info.KeyChar))
				{
					Console.Write("*");
					pwd += info.KeyChar;
				}
			} while (key != ConsoleKey.Enter);
			Console.WriteLine();
			return pwd;
		}
	}
}
