using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProjectManagementConsoleApp.Domain.Enums;
using ProjectManagementConsoleApp.Infrastructure.Repositories;
using ProjectManagementConsoleApp.Infrastructure.Repositories.Interfaces;
using ProjectManagementConsoleApp.Services;
using ProjectManagementConsoleApp.Services.Interfaces;
using ProjectManagementConsoleApp.UI;

namespace ProjectManagementConsoleApp
{
	/// <summary>
	/// Точка входа приложения.
	/// </summary>
	class Program
	{
		/// <summary>
		/// Настройка DI, логирования и запуск интерфейса.
		/// </summary>
		static void Main(string[] args)
		{
			bool enableConsoleLogs = args
				.Contains("--enable-logs", StringComparer.OrdinalIgnoreCase);

			var services = new ServiceCollection();

			services.AddLogging(builder =>
			{
				builder.ClearProviders();
				if (enableConsoleLogs)
				{
					builder
						.AddConsole()
						.SetMinimumLevel(LogLevel.Information);
				}
			});

			services.AddSingleton<IUserRepository, JsonUserRepository>();
			services.AddSingleton<ITaskRepository, JsonTaskRepository>();
			services.AddSingleton<IAuthService, AuthService>();
			services.AddSingleton<IUserService, UserService>();
			services.AddSingleton<ITaskService, TaskService>();
			services.AddSingleton<MenuActions>();
			services.AddSingleton<UIManager>();

			var provider = services.BuildServiceProvider();

			var loggerFactory = provider.GetRequiredService<ILoggerFactory>();
			loggerFactory
				.CreateLogger<Program>()
				.LogInformation("Приложение запущено.");

			var userRepo = provider.GetRequiredService<IUserRepository>();
			var userSvc = provider.GetRequiredService<IUserService>();

			if (!userRepo.GetAll().Any())
			{
				userSvc.Register("admin", "admin", Role.Manager);
				Console.WriteLine("Создан менеджер по умолчанию: логин='admin', пароль='admin'");
				Console.WriteLine("Нажмите любую клавишу для продолжения...");
				Console.ReadKey();
			}

			// Запуск интерфейса
			var ui = provider.GetRequiredService<UIManager>();
			ui.Run();
		}
	}
}
