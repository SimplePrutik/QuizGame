using System;
using System.Collections;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI
{
    public class MapScreen : BaseScreen
    {
        private QuestionsService questionsService;
        private CategoryService categoryService;
        private ScreensService screensService;
        
        [SerializeField] private TileManager tileManager;
        [SerializeField] private TMP_Text loadingText;

        [Inject]
        public void Construct(
            QuestionsService questionsService,
            CategoryService categoryService,
            ScreensService screensService)
        {
            this.questionsService = questionsService;
            this.categoryService = categoryService;
            this.screensService = screensService;

            loadingText.gameObject.SetActive(true);
            Observable.Timer(TimeSpan.FromMilliseconds(500))
                .Subscribe(_ => InitField())
                .AddTo(this);
        }

        private void InitField()
        {
            StartCoroutine(DataExtractionCoroutine());
        }

        private IEnumerator DataExtractionCoroutine()
        {
            yield return questionsService.ExtractData();
            loadingText.gameObject.SetActive(false);
            categoryService.InitCategories(tileManager.Categories);
            tileManager.InitField(questionsService.QuestionsData, categoryService, screensService);
        }
    }
}