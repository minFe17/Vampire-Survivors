using System.Collections.Generic;
using UnityEngine;
using Utils;

public class EnemyManager : IMediatorEvent
{
    // 싱글턴
    List<Transform> _spawnPosition = new List<Transform>();
    List<GameObject> _currentEnemy = new List<GameObject>(); 

    int _minCount = 2;
    int _maxCount = 5;
    int _killEnemyCount;

    public void Init(List<Transform> spawnPosition)
    {
        SimpleSingleton<MediatorManager>.Instance.Register(EMediatorType.SpawnEnemy, this);
        _spawnPosition = spawnPosition;
    }

    public void SpawnEnemy()
    {
        int randomPosIndex = Random.Range(0, _spawnPosition.Count);
        int randomSpawnCount = Random.Range(_minCount, _maxCount);
        for (int i = 0; i < randomSpawnCount; i++)
        {
            int randomEnemyType = Random.Range(0, (int)EEnemyType.Max);
            GameObject temp = MonoSingleton<ObjectPoolManager>.Instance.Push((EEnemyType)randomEnemyType);
            temp.transform.position = _spawnPosition[randomPosIndex].position;
            _currentEnemy.Add(temp);
        }
    }

    public Transform GetClosestEnemy(Vector3 playerPosition)
    {
        Transform closestEnemy = null;
        float closestDistance = float.MaxValue;

        for (int i = 0; i < _currentEnemy.Count; i++)
        {
            float distSqr = (_currentEnemy[i].transform.position - playerPosition).sqrMagnitude;
            if (distSqr < closestDistance)
            {
                closestDistance = distSqr;
                closestEnemy = _currentEnemy[i].transform;
            }
        }
        return closestEnemy;
    }

    public void KillEnemy(Enemy enemy)
    {
        _killEnemyCount++;
        _currentEnemy.Remove(enemy.gameObject);
        SimpleSingleton<MediatorManager>.Instance.Notify(EMediatorType.KillEnemy, _killEnemyCount);
    }

    public void HandleEvent(object data = null)
    {
        SpawnEnemy();
    }
}