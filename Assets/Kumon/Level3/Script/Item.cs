
using UnityEngine;

namespace Level3
{
    /// <summary>
    /// アイテムオブジェクトに関する処理
    /// </summary>
    public class Item : MonoBehaviour
    {
        [SerializeField] private int _score = 5; // スコア
        private IScoreManager _scoreManager;

        private void Start()
        {
            _scoreManager = FindObjectOfType<ScoreManager>() as IScoreManager;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            // プレイヤータグがついたオブジェクトが接触したら自身を破棄する
            if (other.gameObject.CompareTag("Player"))
            {
                _scoreManager.AddScore(_score);
                Destroy(gameObject);
            }
        }
    }

}
