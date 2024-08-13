using System;
using System.Collections.Generic;
using System.Linq;
using Installers;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class QuestionScreen : BaseScreen
    {
        [SerializeField] private List<AnswerButton> answers;
        [SerializeField] private Image questionPicture;
        [SerializeField] private TMP_Text questionTitle;
        //Должно приходить с бэка
        [SerializeField] private List<Sprite> questionPictures;
        
        private QuestionsService questionsService;
        private ScoreService scoreService;
        private ScreensService screensService;

        private const int ANSWERS_COUNT = 4;
        
        [Inject]
        public void Construct(
            QuestionsService questionsService,
            ScoreService scoreService,
            ScreensService screensService)
        {
            this.questionsService = questionsService;
            this.scoreService = scoreService;
            this.screensService = screensService;
            
            InitButtons();
        }

        public void InitQuestion(int id)
        {
            var question = questionsService.QuestionsData[id];
            question.answers = question.answers.OrderBy(x => Guid.NewGuid()).ToArray();
            for (int i = 0; i < ANSWERS_COUNT; ++i)
            {
                answers[i].Init(question.answers[i].text, question.answers[i].isRight);
                answers[i].SetButtonInteractable(true);
            }

            questionTitle.text = question.title;
            questionPicture.sprite = questionPictures[id];
            
            LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
        }

        private void InitButtons()
        {
            foreach (var answerButton in answers)
            {
                answerButton.OnAnswerClicked
                    .Subscribe(isRight =>
                    {
                        foreach (var answerButton in answers)
                        {
                            answerButton.SetButtonInteractable(false);
                        }
                        if (isRight)
                            scoreService.IncScore();
                        Observable.Timer(TimeSpan.FromSeconds(1))
                            .Subscribe(_ =>
                            {
                                screensService.ChangeScreen<MapScreen>();
                            })
                            .AddTo(this);
                    })
                    .AddTo(this);
            }
        }
        
    }
}