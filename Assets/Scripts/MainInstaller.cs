using System;
using UI;
using Zenject;

namespace Installers
{
    public class MainInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindIFactory<Type, BaseScreen>().FromFactory<ScreensFactory>();
            Container.BindInterfacesAndSelfTo<ScreensService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<QuestionsService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CategoryService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ScoreService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<StartUp>().AsSingle().NonLazy();
        }
    }
}
