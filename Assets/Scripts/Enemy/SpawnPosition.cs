using System.Collections.Generic;
using UnityEngine;
using Utils;

public class SpawnPosition : MonoBehaviour
{
    [SerializeField] List<Transform> _spawnPosList;

    void Start()
    {
        SimpleSingleton<EnemyManager>.Instance.Init(_spawnPosList);
    }

    void LateUpdate()
    {
        transform.position = SimpleSingleton<GameManager>.Instance.Player.transform.position;
    }
}