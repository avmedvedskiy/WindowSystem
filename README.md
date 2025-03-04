# WindowService

**WindowService** - это сервис для управления окнами в Unity с поддержкой асинхронного открытия и закрытия окон через UniTask. Позволяет:

- Управлять созданием и уничтожением окон.
- Поддерживать очередь открытия окон.
- Использовать анимации при открытии и закрытии.
- Загружать окна из ресурсов или Addressables.

## 📌 Оглавление
- [Основные классы](#-основные-классы)
  - [WindowService](#-windowservice)
  - [BaseWindow](#-basewindow)
- [Как использовать](#-как-использовать)
- [Методы расширения](#-методы-расширения)
- [Принцип работы](#-принцип-работы)
- [Инициализация сервисов и фабрик](#-инициализация-сервисов-и-фабрик)

## 🏗 Основные классы

### WindowService

Ключевой сервис управления окнами. Отвечает за:

- Создание нового окна через `WindowFactory`.
- Открытие окон по `windowId` с передачей `payload`.
- Поддержку очереди открытия окон.
- Закрытие и уничтожение окон.

```csharp
public async UniTask<TWindow> OpenAsync<TWindow, TPayload>(string windowId, TPayload payload = default, bool inQueue = false)
    where TWindow : IWindow<TPayload>

public async UniTask<TWindow> OpenAsync<TWindow>(string windowId, bool inQueue = false)
    where TWindow : IWindow
```

Метод `OpenAsync<TWindow>` предназначен для открытия окна указанного типа `TWindow` по идентификатору `windowId`.

- **windowId** - идентификатор окна.
- **inQueue** - флаг, указывающий, должно ли окно открываться в очереди.
- Возвращает `UniTask<TWindow>`, который завершится, когда окно будет полностью открыто.

### BaseWindow

Базовый класс для окон. Поддерживает анимации, управление состояниями (`Opened`, `Closed`, `Opening`, `Closing`) и взаимодействие с `WindowService`.

```csharp
public abstract class BaseWindow : MonoBehaviour, IWindow
```

```csharp
public class BaseWindow<TPayload> : MonoBehaviour, IWindow<TPayload>
```

Методы:

- `OpenAsync(TPayload payload)`: открытие окна с анимацией.
- `CloseWindow()`: закрытие окна и возврат в `WindowService`.
- `SetStatus(Status status)`: изменение состояния окна.


## 🚀 Как использовать

1. Создать `WindowService` и передать ему `WindowFactory` и `WindowRootProvider`.
2. Вызвать `OpenAsync` для открытия окна.
3. Использовать `CloseAsync` для закрытия окна.

Пример:

```csharp
var window = await _windowService.OpenAsync<MyWindow, MyPayload>("MyWindow", new MyPayload());
window.DoSomething();
await _windowService.CloseAsync("MyWindow");
```

Пример открытия окна без `payload`:

```csharp
var window = await _windowService.OpenAsync<MyWindow>("MyWindow");
window.DoSomething();
await _windowService.CloseAsync("MyWindow");
```

## 🛠 Методы расширения

Дополнительные методы для удобного управления окнами:

```csharp
// Повторное открытие окна в очереди
await window.ReopenInQueue();

// Повторное открытие окна без очереди
await window.Reopen();

// Ожидание закрытия окна
await _windowService.OpenAsync<MyWindow, MyPayload>("MyWindow", new MyPayload()).AndWaitClose();

// Ожидание начала закрытия окна
await _windowService.OpenAsync<MyWindow, MyPayload>("MyWindow", new MyPayload()).AndWaitClosing();

// Открытие всплывающего окна и ожидание результата
var result = await _windowService.OpenPopup<MyPopupWindow, MyPayload, int>("PopupWindow", new MyPayload());
Debug.Log("Результат: " + result);
```

## ⚙️ Принцип работы

1. Проверяется, открыто ли окно (`HasWindow`).
2. Если окно уже открыто, вызывается `OpenAsync` у существующего окна.
3. Если окно отсутствует, создаётся новое через `WindowFactory`.
4. Окно получает `Status.Opening`, затем `Status.Opened`.
5. При закрытии вызывается `CloseAsync`, статус меняется на `Closing`, затем `Closed`.
6. Окно удаляется `DestroyWindow()`.

## 🏗 Инициализация сервисов и фабрик

Для корректной работы всех окон необходимо инициализировать `WindowService` и `WindowFactory`:

```csharp
[SerializeField] private WindowRootProvider _windowRootProvider;
[SerializeField] private WindowFactoryConfig _windowFactoryConfig;

private void InstallWindowsService()
{
    IWindowFactory factory = new WindowFactory(_windowFactoryConfig);
    IWindowService windowService = new WindowService(factory, _windowRootProvider);
}
```

После этого `windowService` можно использовать для управления окнами.