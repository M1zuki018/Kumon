using Level3;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RunGame
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _speed = 10f; // プレイヤーが動くスピード（初期値として5を設定）
        private Vector3 _inputDirection; // 移動量の入力を保存しておく
        private Rigidbody _rb; // プレイヤーオブジェクトのRigidbody
        private PlayerInput _inputSystem; // プレイヤーオブジェクトのPlayerInput
        private IScoreManager _scoreManager; //スコアを管理するクラス
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _inputSystem = GetComponent<PlayerInput>();
            _scoreManager = FindObjectOfType<ScoreManager>() as IScoreManager;
        }

        private void FixedUpdate()
        {
            _rb.velocity = _inputDirection * _speed; // 入力に合わせて移動させる処理
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();
            _inputDirection = new Vector3(input.x, 0, input.y); // 入力を取得する
        }
        
        private void OnCollisionEnter(Collision other)
        {
            // Goalタグをつけたゴールオブジェクトに触れたら、動きを止める
            if (other.gameObject.CompareTag("Goal"))
            {
                Debug.Log($"ゴール");
                _rb.velocity = Vector3.zero; // 移動を止めるためにRigidbodyのvelocityを0にする
            }
        }
    }   
}