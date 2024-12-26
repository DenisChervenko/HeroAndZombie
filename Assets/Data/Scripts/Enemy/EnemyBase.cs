using System;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

[RequireComponent(typeof(DamageAnimation))]
public class EnemyBase : MonoBehaviour, IUpdatable, IDamagable
{
    [SerializeField] private Transform _brain;
    protected Transform _player;


    [SerializeField] private float _health;
    private float _maxHealth;

    [SerializeField] private float _attack;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _stoppingDistance;

    public event Action onDie;

    private IDamagable _playerDamagable;
    private NavMeshAgent _agent;
    private Animator _animator;

    private DamageAnimation _damageAnimation;
    [Inject] private GlobalEvent _globalEvent;

    private void Start()
    {
        _maxHealth = _health;
        _damageAnimation = GetComponent<DamageAnimation>();
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

        _player = _globalEvent.onPlayerPositionGet?.Invoke();
        _playerDamagable = _globalEvent.onPlayerDamagableGet?.Invoke();

        _agent.updateRotation = false;
    }

    public void SubUpdate()
    {
        Vector3 distance = transform.position - _player.position;

        if(distance.sqrMagnitude > _stoppingDistance)
        {
            _animator.ResetTrigger("Attack");
            _animator.SetTrigger("Walk");
            _agent.SetDestination(_player.position);
            
            Quaternion targetRotation = Quaternion.LookRotation(_agent.velocity.normalized);
            gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }
        else
        {
            _animator.ResetTrigger("Walk");
            _animator.SetTrigger("Attack");
        }   
    }

    public void TakeDamage(float damage)
    {   
        _damageAnimation.StartAnimation();
        _health -= damage;
        _brain.localScale = Vector3.one * (_health/_maxHealth);

        if(_health < 0)
        {
            onDie?.Invoke();
            _globalEvent.onUpdateBalance(1, false);

            _animator.ResetTrigger("Attack");
            _animator.ResetTrigger("Walk");

            _damageAnimation.DieEffect();
        }
    }

    public void State(bool state) => this.enabled = state;

    public void OnAttackPlayer() => _playerDamagable.TakeDamage(_attack);

    public void ActiveState(bool state) => gameObject.SetActive(state);

    private void OnEnable() => _globalEvent.onUpdatableSubscription?.Invoke(this, true, false);
    private void OnDisable()
    {
        _globalEvent.onUpdatableSubscription?.Invoke(this, false, false);
        _globalEvent.onCustomTargetUpdate?.Invoke(gameObject.transform);
    }
}
