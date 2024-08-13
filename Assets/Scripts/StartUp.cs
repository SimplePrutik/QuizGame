using UI;
using UnityEngine;
using Zenject;

public class StartUp : IInitializable
{
    private readonly ScreensService screensService;
    private readonly DiContainer container;
    
    public StartUp(
        ScreensService screensService,
        DiContainer container)
    {
        this.screensService = screensService;
        this.container = container;
    }
    
    public void Initialize()
    {
        screensService.ChangeScreen<MapScreen>();
        var resourcePath = "UI/Prefabs/ScoreWidget";
        var prefab = Resources.Load(resourcePath);
        container.InstantiatePrefab(prefab);
    }
}