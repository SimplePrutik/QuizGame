using System;
using System.Collections.Generic;
using Zenject;

namespace UI
{
    public class ScreensService
    {
        private readonly IFactory<Type, BaseScreen> screensFactory;
        private readonly Dictionary<Type, BaseScreen> screensCache = new ();
        
        private BaseScreen currentScreen;

        public ScreensService(IFactory<Type, BaseScreen> screensFactory)
        {
            this.screensFactory = screensFactory;
        }

        public T ChangeScreen<T>() where T : BaseScreen
        {
            var screenType = typeof(T);
            currentScreen?.Hide();
            if (screensCache.ContainsKey(screenType))
                currentScreen = screensCache[screenType] as T;
            else
                currentScreen = screensFactory.Create(typeof(T));
            screensCache[screenType] = currentScreen;
            currentScreen.Show();
            return currentScreen as T;
        }
    }
}