namespace ProjectManagementConsoleApp.Domain.Enums
{
	/// <summary>
	/// Состояния задачи.
	/// </summary>
	public enum TaskStatus
	{
		/// <summary>
		/// Задача создана и ожидает выполнения.
		/// </summary>
		ToDo,

		/// <summary>
		/// Задача в процессе выполнения.
		/// </summary>
		InProgress,

		/// <summary>
		/// Задача завершена.
		/// </summary>
		Done
	}
}
