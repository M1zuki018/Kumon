using UnityEngine;
using UniRx;

namespace Level3
{
    /// <summary>
    /// スコア管理クラス
    /// </summary>
    public class ScoreManager : MonoBehaviour, IScoreManager
    {
        public ReactiveProperty<int> ScoreProp { get; private set; } = new ReactiveProperty<int>(0); // スコア 
        
        /// <summary>
        /// スコアを加算するメソッド
        /// </summary>
        public void AddScore(int score)
        {
            ScoreProp.Value += score;
        }
    }
}
