using UnityEngine;
using UnityEngine.InputSystem;

namespace RunGame
{
    /// <summary>
    /// ランゲームのプレイヤーコントローラー
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _speed = 10f; // プレイヤーが動くスピード
        private bool _isStop = false; // 止まっているか
        private Vector3 _inputDirection; // 移動量の入力を保存しておく
        private Rigidbody _rb; // プレイヤーオブジェクトのRigidbody
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        /*
        private void FixedUpdate()
        {
            // 止まるフラグがtrueなら、以降の処理は行わない
            if (_isStop) return;
            
            _rb.velocity = _inputDirection * _speed; // 入力に合わせて移動させる処理
        }
        */

        public void OnMove(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Vector2 input = context.ReadValue<Vector2>();
                _inputDirection = new Vector3(input.x, 0, 0); // 横移動の入力だけ取得する
                Move();
            }
        }

        /// <summary>
        /// レーン移動を行う
        /// </summary>
        private void Move()
        {
            if (_inputDirection.x >= 0.9f)
            {
                _rb.AddForce(Vector3.right * _speed, ForceMode.Impulse);
            }
            else if (_inputDirection.x <= -0.9f)
            {
                _rb.AddForce(Vector3.left * _speed, ForceMode.Impulse);
            }
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