using UnityEngine;
using System.Collections.Generic;
using Zenject;
public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private EnemyBase[] _prefabEnemy;
    private List<EnemyBase> _readyPrefab = new List<EnemyBase>();
    private int _enemyIndex;

    [SerializeField] private int _spawnEnemyCount;

    [Inject] private DiContainer _container;

    private void Start()
    {
        for(int i = 1; i < _spawnEnemyCount + 1; i++)
        {
            _readyPrefab.Add(_container.InstantiatePrefab(_prefabEnemy[_spawnEnemyCount % i == 0 ? 0 : 1]).GetComponent<EnemyBase>());
            _readyPrefab[i - 1].ActiveState(false);
        }
    }

    public EnemyBase TakeEnemy()
    {
        if(_enemyIndex > _spawnEnemyCount)
            _enemyIndex = 0;

        EnemyBase enemy = _readyPrefab[_enemyIndex];
        _enemyIndex++;
        return enemy;
    }
}
