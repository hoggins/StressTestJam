using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class GameFinishUi : MonoBehaviour
    {
        public float AnimationTime = 1.5f;
        public Text ScoreText;
        
        private int _score;
        private int _curValue;
        public void SetScore(int score)
        {
            _score = score;
        }
        private void Update()
        {
            _curValue = Mathf.CeilToInt(Mathf.Lerp(_curValue, _score, Time.deltaTime / AnimationTime));
            ScoreText.text = string.Format("Your score: {0}%", _curValue);
        }
    }
}