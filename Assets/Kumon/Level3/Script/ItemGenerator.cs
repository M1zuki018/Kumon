using UnityEngine;

namespace Level3
{
    /// <summary>
    /// アイテムを等間隔に設置するクラス
    /// </summary>
    public class ItemGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject _itemPrefab; // アイテムのプレハブ
        [SerializeField] private float _distance = 5f; // アイテムの生成間隔

        private void Start()
        {
            Vector3 position = new Vector3(0, 1, 0); // 基準点を設定
            for (int i = 0; i < 100 / _distance; i++) // 生成個数はFieldの長さ÷生成間隔
            {
                position.z += _distance; // 生成間隔分Z軸をずらす
                Instantiate(_itemPrefab, position, Quaternion.identity);
            }
        }
    }
}

