using UnityEngine;

namespace Level3
{
    /// <summary>
    /// Level3 プレイヤーを動かす処理を書いたクラス
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class Move : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f; // プレイヤーが動くスピード（初期値として5を設定）
        private bool _isStop = false; // 止まっているか
        private Rigidbody _rb; // プレイヤーオブジェクトのRigidbody
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();

        }

        private void Update()
        {
            // 止まるフラグがtrueなら、以降の処理は行わない
            if (_isStop) return;
            
            _rb.velocity = Vector3.forward * _speed; // 前に移動させる処理
        }

        private void OnCollisionEnter(Collision other)
        {
            // Goalタグをつけたゴールオブジェクトに触れたら、動きを止める
            if (other.gameObject.CompareTag("Goal"))
            {
                Debug.Log($"ゴール");
                _isStop = true;
                _rb.velocity = Vector3.zero; // 移動を止めるためにRigidbodyのvelocityを0にする
            }
        }
    }
}
