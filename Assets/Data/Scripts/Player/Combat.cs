using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Zenject;

public class Combat : MonoBehaviour
{
    [SerializeField] private LayerMask _ignoreMask;
    [SerializeField] private Transform _raycastPoint;
    

    [SerializeField] private DefaultProjectile _bulletPrefab;
    [SerializeField] private float _countPrefab;

    [SerializeField] private Transform _muzzlePoint;
    [SerializeField]  private ParticleSystem _shootEffect;

    private List<DefaultProjectile> _bulletProjectile = new List<DefaultProjectile>();
    private PlayerCore _playerCore;
   

    private int _indexProjectile;
    private float _attackSpeed;
    private float _damage;

    private bool _isCanAttack = true;

    [Inject] private GlobalEvent _globalEvent;
    [Inject] private DiContainer _container;

    private void Awake()
    {
        _playerCore = GetComponent<PlayerCore>();

        _attackSpeed = _playerCore.AttackSpeed;
        _damage = _playerCore.Damage;

        for(int i = 0; i < _countPrefab; i++)
        {
            _bulletProjectile.Add(_container.InstantiatePrefab(_bulletPrefab).GetComponent<DefaultProjectile>());
            _bulletProjectile[i].InitVariable(_damage);
            _bulletProjectile[i].StateControll(false);
        }
    }

    private void Start()
    {
        Transform shootComponents = gameObject.transform.Find("Armature");

        _shootEffect = shootComponents.GetComponentInChildren<ParticleSystem>();
        _muzzlePoint = _shootEffect.gameObject.transform;
    }

    private void MakeShoot()
    {
        Ray ray = new Ray(_raycastPoint.position, _raycastPoint.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 20, ~_ignoreMask))
        {
            if(!hit.collider.CompareTag("Enemy"))
                return;
        }

        if(!_isCanAttack)
            return; 

        _shootEffect.Play();

        _bulletProjectile[_indexProjectile].StateControll(true);
        _bulletProjectile[_indexProjectile].UpdateTrasnform(_muzzlePoint);
        _indexProjectile++;

        if(_indexProjectile > _countPrefab - 1)
            _indexProjectile = 0;

        _isCanAttack = false;
        StartCoroutine(AttackCooldown());
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(_attackSpeed);
        _isCanAttack = true;
    }

    private void OnEnable() => _globalEvent.onMakeShoot += MakeShoot;
    private void OnDisable() => _globalEvent.onMakeShoot -= MakeShoot;
}
