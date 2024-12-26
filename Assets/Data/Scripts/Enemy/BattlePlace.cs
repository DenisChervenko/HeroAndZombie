using UnityEngine;
using DG.Tweening;
using Unity.AI.Navigation;
using Zenject;

public class BattlePlace : MonoBehaviour
{
    [Space()]
    [SerializeField] private Transform _gate;
    [SerializeField] private Transform[] _spawnPoint;
    
    [Space()]
    [SerializeField] private NavMeshSurface _navMesh;
    [SerializeField] private int _countEnemyInBattle;

    [SerializeField] private ParticleSystem _spawnParticle;
    private int _killedEnemy;
    private Collider _collider;

    [Inject] private EnemyGenerator _generator;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _navMesh.BuildNavMesh();
        MeshRenderer renderer = _navMesh.gameObject.GetComponent<MeshRenderer>();
        renderer.enabled = false;
    }

    private void CreateBattlefield()
    {
        _collider.enabled = false;
        _gate.DOMove(new Vector3(_gate.position.x, 2, _gate.position.z), 0.1f);
        int spawnPointIndex = 0;
        
        for(int i = 0; i < _countEnemyInBattle; i++)
        {
            EnemyBase enemy = _generator.TakeEnemy();
            enemy.transform.position = _spawnPoint[spawnPointIndex].position;
            enemy.ActiveState(true);
            enemy.onDie += TryCloseBattlefield;

            spawnPointIndex++;
            if(spawnPointIndex >= _spawnPoint.Length)
                spawnPointIndex = 0;
        }
        _spawnParticle.Play();
    }

    private void TryCloseBattlefield() 
    {
        _killedEnemy++;
        if(_killedEnemy == _countEnemyInBattle)
            _gate.DOMove(new Vector3(_gate.position.x, 0, _gate.position.z), 0.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
            CreateBattlefield();
    }
}
