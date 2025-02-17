using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Level3
{
    /// <summary>
    /// スコアテキストを表示するためのクラス
    /// </summary>
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private Text _scoreText; // スコアを表示するテキストコンポーネント
        [SerializeField] private ScoreManager _scoreManager; // スコアマネージャー

        private void Start()
        {
            _scoreManager.ScoreProp
                .ObserveEveryValueChanged(x => x.Value)
                .Subscribe(_ => ReWriteScoreText())
                .AddTo(this);
        }

        /// <summary>
        /// スコアが変更されたときにテキストUIを更新する
        /// </summary>
        private void ReWriteScoreText()
        {
            _scoreText.text = _scoreManager.ScoreProp.Value.ToString();
        }
    }
}
