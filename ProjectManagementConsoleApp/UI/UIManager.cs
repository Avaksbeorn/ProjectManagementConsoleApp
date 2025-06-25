using System;
using ProjectManagementConsoleApp.Services.Interfaces;
using ProjectManagementConsoleApp.UI;

namespace ProjectManagementConsoleApp.UI
{
	/// <summary>
	/// Управление пользовательским интерфейсом.
	/// </summary>
	public class UIManager
	{
		private readonly IAuthService _authService;
		private readonly IUserService _userService;
		private readonly MenuActions _menu;

		/// <summary>
		/// Инициализация UIManager.
		/// </summary>
		public UIManager(
			IAuthService authService,
			IUserService userService,
			MenuActions menu)
		{
			_authService = authService;
			_userService = userService;
			_menu = menu;
		}

		/// <summary>
		/// Запуск приложения.
		/// </summary>
		public void Run()
		{
			Console.Title = "Система управления проектами";
			while (true)
			{
				Console.Clear();
				Console.WriteLine("=== Система управления проектами ===");
				Console.Write("Логин: ");
				var login = Console.ReadLine();
				Console.Write("Пароль: ");
				var password = ReadPassword();

				var user = _authService.Authenticate(login, password);
				if (user == null)
				{
					Console.WriteLine("\nНеверный логин или пароль. Нажмите любую клавишу для повторной попытки...");
					Console.ReadKey();
					continue;
				}

				Console.WriteLine($"\nДобро пожаловать, {user.Login}! Роль: {user.Role}");
				if (user.Role == Domain.Enums.Role.Manager)
					_menu.ShowManagerMenu(user);
				else
					_menu.ShowEmployeeMenu(user);
			}
		}

		/// <summary>
		/// Чтение пароля без отображения символов.
		/// </summary>
		private string ReadPassword()
		{
			var pwd = string.Empty;
			ConsoleKey key;
			do
			{
				var keyInfo = Console.ReadKey(intercept: true);
				key = keyInfo.Key;

				if (key == ConsoleKey.Backspace && pwd.Length > 0)
				{
					Console.Write("\b \b");
					pwd = pwd[0..^1];
				}
				else if (!char.IsControl(keyInfo.KeyChar))
				{
					Console.Write("*");
					pwd += keyInfo.KeyChar;
				}
			} while (key != ConsoleKey.Enter);
			Console.WriteLine();
			return pwd;
		}
	}
}
