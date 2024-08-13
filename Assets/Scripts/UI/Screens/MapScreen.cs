using UnityEngine;
using Zenject;

namespace UI
{
    public class MapScreen : BaseScreen
    {
        [SerializeField] private TileManager tileManager;

        [Inject]
        public void Construct(
            QuestionsService questionsService,
            CategoryService categoryService,
            ScreensService screensService)
        {
            categoryService.InitCategories(tileManager.Categories);
            
            tileManager.InitField(questionsService.QuestionsData, categoryService, screensService);
        }
        
    }
}