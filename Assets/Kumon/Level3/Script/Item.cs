using UnityEngine;

namespace Level3
{
    /// <summary>
    /// アイテムオブジェクトに関する処理
    /// </summary>
    public class Item : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            // プレイヤータグがついたオブジェクトが接触したら自身を破棄する
            if (other.gameObject.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
        }
    }

}
