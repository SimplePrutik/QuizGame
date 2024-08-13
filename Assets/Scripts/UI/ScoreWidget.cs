using Installers;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI
{
    public class ScoreWidget : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreField;

        [Inject]
        public void Construct(ScoreService scoreService)
        {
            scoreService.ScoreValue
                .Subscribe(value => { scoreField.text = value.ToString(); })
                .AddTo(this);
        }
    }
}