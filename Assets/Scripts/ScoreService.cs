using UniRx;

namespace Installers
{
    public class ScoreService
    {
        public ReactiveProperty<int> ScoreValue = new ();

        private const int RIGHT_ANSWER_SCORE = 10;

        public void IncScore()
        {
            ScoreValue.Value += RIGHT_ANSWER_SCORE;
        }
    }
}