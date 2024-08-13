using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class AnswerButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text textField;
        [SerializeField] private Button button;
        [SerializeField] private Image bg;

        private IDisposable btnDisposable;

        public ReactiveCommand<bool> OnAnswerClicked = new ();

        public void Init(string text, bool isRight)
        {
            textField.text = text;
            bg.color = Color.white;

            btnDisposable?.Dispose();
            btnDisposable = button.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    if (isRight)
                    {
                        bg.color = Color.green;
                    }
                    else
                    {
                        bg.color = Color.red;
                    }

                    OnAnswerClicked.Execute(isRight);
                });
        }

        public void SetButtonInteractable(bool isInteractable) => button.interactable = isInteractable;
    }
}