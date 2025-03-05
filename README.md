# Playnera Test Task
## Автор: Войнаренко Сергей
### Билд: [ссылка](https://disk.yandex.ru/d/v-lV7ePmB-9KnA)
### Видео: [ссылка](https://disk.yandex.ru/i/HQmmhZEd6d9pdg)
##
## Управление:

&emsp;***Перетаскивание заднего фона*** — перемещение сцены

&emsp;***Перетаскивание цветного объекта*** — перемещение выбранного объекта

&emsp;***Кнопка или жест "назад"*** — выход из приложения

## Структура проекта:

&emsp;***/Art*** — графические материалы

&emsp;***/Code/***

&emsp;&emsp;***Gameplay/***

&emsp;&emsp;&emsp;***Element/***`Animate.cs` — анимация перемещаемого объекта

&emsp;&emsp;&emsp;***Element/***`Drag.cs` — перетягивание перемещаемого объекта

&emsp;&emsp;&emsp;***Input/***

&emsp;&emsp;&emsp;&emsp;***Service/***

&emsp;&emsp;&emsp;&emsp;&emsp;`IInputService.cs` — интерфейс сервиса ввода

&emsp;&emsp;&emsp;&emsp;&emsp;`InputService.cs` — сервис ввода

&emsp;&emsp;&emsp;&emsp;`IDraggable.cs` — интерфейс интерактивных объектов

&emsp;&emsp;&emsp;&emsp;`Process.cs` — обработка ввода

&emsp;&emsp;&emsp;***Scroll/***

&emsp;&emsp;&emsp;&emsp;`Correct.cs` — корректировка параметров цели слежения камеры

&emsp;&emsp;&emsp;&emsp;`Scroll.cs` — скроллинг сцены

&emsp;&emsp;&emsp;***Visual/***

&emsp;&emsp;&emsp;&emsp;`IAnimated.cs` — интерфейс анимированных объектов

&emsp;&emsp;***Infrastructure/***

&emsp;&emsp;&emsp;***Async/Service***

&emsp;&emsp;&emsp;&emsp;`AsyncService.cs` — сервис асинхронных операций

&emsp;&emsp;&emsp;&emsp;`IAsyncService.cs` — интерфейс сервиса асинхронных операций

&emsp;&emsp;&emsp;***Installers/***`BootstrapInstaller.cs` — инсталлятор зависимостей для Zenject

&emsp;&emsp;&emsp;***Loading/***

&emsp;&emsp;&emsp;&emsp;`ISceneLoader.cs` — интерфейс загрузчика сцен

&emsp;&emsp;&emsp;&emsp;`SceneLoader.cs` — загрузчик сцен

&emsp;&emsp;&emsp;&emsp;`Scenes.cs` — список сцен

&emsp;&emsp;&emsp;***States/***

&emsp;&emsp;&emsp;&emsp;***Factory/***

&emsp;&emsp;&emsp;&emsp;&emsp;`IStateFactory.cs` — интерфейс фабрики состояний

&emsp;&emsp;&emsp;&emsp;&emsp;`StateFactory.cs` — фабрика состояний

&emsp;&emsp;&emsp;&emsp;***GameStates/***

&emsp;&emsp;&emsp;&emsp;&emsp;`BootstrapState.cs` — состояние загрузки приложения

&emsp;&emsp;&emsp;&emsp;&emsp;`LevelEnterState.cs` — состояние входа в уровень

&emsp;&emsp;&emsp;&emsp;&emsp;`LevelLoadState.cs` — состояние загрузки уровня

&emsp;&emsp;&emsp;&emsp;&emsp;`LevelLoopState.cs` — состояние игрового цикла уровня

&emsp;&emsp;&emsp;&emsp;***StatesInfrastructure/***

&emsp;&emsp;&emsp;&emsp;&emsp;`IExitableState.cs` — интерфейс состояния, имеющего выход

&emsp;&emsp;&emsp;&emsp;&emsp;`IState.cs` — интерфейс состояния

&emsp;&emsp;&emsp;&emsp;***StateMachine/***

&emsp;&emsp;&emsp;&emsp;&emsp;`GameStateMachine.cs` — машина состояний игры

&emsp;&emsp;&emsp;&emsp;&emsp;`IGameStateMachine` — интерфейс машины состояний игры

&emsp;***/Plugins*** — внешние плагины

&emsp;***/Prefabs/***

&emsp;&emsp;`Level.prefab` — префаб уровня

&emsp;&emsp;`Movable.prefab` — префаб перемещаемого объекта

&emsp;&emsp;`Shelf.prefab` — префаб платформы

&emsp;***/Resources/***`ProjectContext.prefab` — контекст проекта Zenject

&emsp;***/Scenes/***

&emsp;&emsp;**Boot** — сцена инициализации

&emsp;&emsp;**Level** — сцена игрового уровня

&emsp;***/Settings*** — настройки

## Иерархия объектов в сцене:

**Main Camera** — основная камера

**CinemachineCamera** — виртуальная камера Cinemachine

**Global Light 2D** — источник света

**SceneContext** — контекст сцены Zenject

**Level** — игровой уровень

**InputListener** — объект, отслеживающий ввод

## Архитектура:

`BootstrapInstaller.cs` — точка входа. Метод `Initialize` переводит машину состояний в `BootstrapState`.

Из `BootstrapState` происходит переход в `LoadLevelState`, где происходит загрузка сцены игрового уровня, после чего осуществляется вход в состояние `LevelEnterState`, в котором можно выполнить операции, необходимые до запуска уровня. После этого включается состояние `LevelLoopState`, на входе в которое происходит включение управления, а на выходе — выключение.

Внедрение зависимостей происходит при помощи фреймворка Zenject. Для асинхронных операций используется UniTask, для процедурных анимаций — DOTween.