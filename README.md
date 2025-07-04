# Project Management Console App

## 📋 Содержание

* [Роли и привилегии](#роли-и-привилегии)
* [Главное меню](#главное-меню)
  * [Управление пользователями (Admin)](#управление-пользователями-admin)
  * [Управление проектами (Admin)](#управление-проектами-admin)
  * [Управление задачами (Admin)](#управление-задачами-admin)
  * [Выход](#выход)
* [Как работать Пользователю](#как-работать-пользователю)

---

## Роли и привилегии

| Роль         | Привилегии по умолчанию                                                                                                                                                                                                                          |
| ------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| **Manager**  | *Администратор системы*. Может создавать/удалять пользователей, проекты и задачи, назначать ответственных, менять статусы любых задач, просматривать всю статистику. <br>По умолчанию создаётся учётка **`admin` / `admin`** при первом запуске. |
| **Employee** | Видит только назначенные ему задачи, может переводить их в `In Progress` и `Done`, просматривать базовую информацию о проектах.                                                                                                                  |


---

## Главное меню

| Опция               | Назначение                                                         |
| ------------------- | ------------------------------------------------------------------ |
| **1 — Войти**       | Авторизация существующего пользователя.                            |
| **2 — Регистрация** | Быстрая регистрация (только менеджер может создавать сотрудников). |
| **3 — Выход**       | Завершение работы приложения.                                      |

После успешной авторизации приложение выводит меню, соответствующее вашей роли.

### Управление пользователями (Admin)

```text
[1] Список сотрудников
[2] Добавить сотрудника
[3] Удалить сотрудника
[4] Назад
```

* **Список** — отображает ID, логин и количество задач на сотруднике.
* **Добавить** — предлагает ввести логин и пароль; роль автоматически `Employee`.
* **Удалить** — при подтверждении снимает все задачи с пользователя.

### Управление проектами (Admin)

```text
[1] Список проектов
[2] Создать проект
[3] Переименовать проект
[4] Удалить проект
[5] Назад
```

Каждый проект имеет `Id`, `Название` и набор задач. При удалении проекта удаляются все связанные задачи.

### Управление задачами (Admin)

```text
[1] Список задач по проекту
[2] Создать задачу
[3] Назначить исполнителя
[4] Изменить статус
[5] Удалить задачу
[6] Назад
```

* **Создать** — ввод названия, описания, проекта‑родителя.
* **Назначить** — выбор сотрудника из списка; при повторном выборе исполнитель сменится.
* **Изменить статус** — `ToDo → InProgress → Done`.

### Выход

Команда из любого подменю возвращает вас на уровень выше. В главном меню «Выход» завершает процесс приложения.

---

## Как работать Пользователю

1. Получите свои учётные данные у менеджера.
2. Авторизуйтесь через «Войти».
3. В пользовательском меню выберите «Мои задачи», чтобы увидеть текущий список.
4. Для каждой задачи можно:

   * просмотреть краткое описание и проект;
   * сменить статус с `ToDo` → `InProgress` или `InProgress` → `Done`.

---




![TaskSystem](https://github.com/user-attachments/assets/2daf85e6-44e7-4501-b6df-3c53245027e1)
