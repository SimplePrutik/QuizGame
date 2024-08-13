using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public enum TileState
    {
        Enabled,
        Disabled,
        Answered
    }
    
    public class Tile : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private Image bg;
        [SerializeField] private Button button;

        private int id;
        private Vector2 tileSize;
        
        private readonly Color enabledColor = new Color(1f, 1f, 1f, 1f);
        private readonly Color disabledColor = new Color(0f, 0f, 0f, 0.3f);
        private readonly Color answeredColor = new Color(0f, 0f, 0f, 0f);
        private readonly Vector2 answeredScale = new Vector2(0.3f, 0.5f);

        public ReactiveCommand<int> OnButtonPressed = new ();

        public void Init(CategoryService.CategoryContent categoryContent, int questionId)
        {
            SetEnabled(TileState.Disabled);
            icon.sprite = categoryContent.IconSprite;
            bg.color = categoryContent.Color;
            id = questionId;
            tileSize = GetComponent<RectTransform>().sizeDelta;
            
            button.OnClickAsObservable()
                .Subscribe(_ => OnButtonPressed.Execute(id))
                .AddTo(this);
        }

        public void SetEnabled(TileState state)
        {
            switch (state)
            {
                case TileState.Enabled:
                    icon.color = enabledColor;
                    button.interactable = true;
                    break;
                case TileState.Disabled:
                    icon.color = disabledColor;
                    button.interactable = false;
                    break;
                case TileState.Answered:
                    icon.color = answeredColor;
                    GetComponent<RectTransform>().sizeDelta = new Vector2(
                        tileSize.x * answeredScale.x, 
                        tileSize.y * answeredScale.y);
                    button.interactable = false;
                    break;
            }
        }
    }
}