using System;
using Zenject;
using UnityEngine;

namespace UI
{
    public class ScreensFactory : IFactory<Type, BaseScreen>
    {
        private readonly DiContainer container;
        private readonly Transform uiRoot;

        public ScreensFactory(DiContainer container)
        {
            this.container = container;
            
            var uiRootGameObject = new GameObject("UI Root");
            GameObject.DontDestroyOnLoad(uiRootGameObject);
            uiRoot = uiRootGameObject.transform;
        }

        public BaseScreen Create(Type screenType)
        {
            var resourcePath = $"UI/Screens/{screenType.Name}";
            var prefab = Resources.Load(resourcePath);
            return (BaseScreen)container.InstantiatePrefabForComponent(screenType, prefab, uiRoot, Array.Empty<object>());
        }
    }
}
