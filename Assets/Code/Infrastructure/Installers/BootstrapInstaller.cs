using Code.Gameplay.Input.Service;
using Code.Infrastructure.Loading;
using Code.Infrastructure.States.Factory;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachine;
using Zenject;

namespace Code.Infrastructure.Installers
{
  public class BootstrapInstaller : MonoInstaller, IInitializable
  {
    public override void InstallBindings()
    {
      BindInputService();
      BindInfrastructureServices();
      BindCommonServices();
      BindStateMachine();
      BindStateFactory();
      BindGameStates();
    }

    private void BindInputService() =>
      Container.Bind<IInputService>().To<InputService>().AsSingle();

    private void BindInfrastructureServices() =>
      Container.BindInterfacesTo<BootstrapInstaller>().FromInstance(this).AsSingle();

    private void BindCommonServices() =>
      Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();

    private void BindStateMachine() =>
      Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();

    private void BindStateFactory() =>
      Container.BindInterfacesAndSelfTo<StateFactory>().AsSingle();

    private void BindGameStates()
    {
      Container.BindInterfacesAndSelfTo<BootstrapState>().AsSingle();
      Container.BindInterfacesAndSelfTo<LevelEnterState>().AsSingle();
      Container.BindInterfacesAndSelfTo<LevelLoadState>().AsSingle();
      Container.BindInterfacesAndSelfTo<LevelLoopState>().AsSingle();
    }

    public void Initialize() =>
      Container.Resolve<IGameStateMachine>().Enter<BootstrapState>();
  }
}