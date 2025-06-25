namespace ProjectManagementConsoleApp.Domain.Enums
{
	/// <summary>
	/// Роли пользователей в системе управления проектами.
	/// </summary>
	public enum Role
	{
		/// <summary>
		/// Менеджер: создаёт задачи, назначает исполнителей и регистрирует сотрудников.
		/// </summary>
		Manager,

		/// <summary>
		/// Сотрудник: выполняет и обновляет статус назначенных задач.
		/// </summary>
		Employee
	}
}
