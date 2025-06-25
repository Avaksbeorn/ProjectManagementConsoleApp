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
	/// JSON-репозиторий пользователей.
	/// </summary>
	public class JsonUserRepository : IUserRepository
	{
		private readonly string _filePath;
		private readonly List<User> _users;

		/// <summary>
		/// Инициализирует репозиторий и загружает данные из файла.
		/// </summary>
		public JsonUserRepository(string filePath = "users.json")
		{
			_filePath = filePath;
			if (File.Exists(_filePath))
			{
				var json = File.ReadAllText(_filePath);
				_users = JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
			}
			else
			{
				_users = new List<User>();
				SaveChanges();
			}
		}

		/// <summary>
		/// Возвращает всех пользователей.
		/// </summary>
		public IEnumerable<User> GetAll() => _users;

		/// <summary>
		/// Возвращает пользователя по идентификатору.
		/// </summary>
		public User GetById(Guid id) =>
			_users.FirstOrDefault(u => u.Id == id);

		/// <summary>
		/// Возвращает пользователя по логину.
		/// </summary>
		public User GetByLogin(string login) =>
			_users.FirstOrDefault(u => u.Login.Equals(login, StringComparison.OrdinalIgnoreCase));

		/// <summary>
		/// Добавляет нового пользователя.
		/// </summary>
		public void Add(User user)
		{
			if (_users.Any(u => u.Login.Equals(user.Login, StringComparison.OrdinalIgnoreCase)))
				throw new InvalidOperationException("Пользователь с таким логином уже существует.");

			_users.Add(user);
			SaveChanges();
		}

		/// <summary>
		/// Обновляет данные пользователя.
		/// </summary>
		public void Update(User user)
		{
			var idx = _users.FindIndex(u => u.Id == user.Id);
			if (idx == -1) throw new InvalidOperationException("Пользователь не найден.");

			_users[idx] = user;
			SaveChanges();
		}

		/// <summary>
		/// Сохраняет изменения в файл.
		/// </summary>
		private void SaveChanges()
		{
			var json = JsonSerializer.Serialize(_users, new JsonSerializerOptions { WriteIndented = true });
			File.WriteAllText(_filePath, json);
		}
	}
}
