using UnityEngine;
using Zenject;

public class TrapDamage : MonoBehaviour
{
    [SerializeField] private float _damage;
    private IDamagable _playerDamagable;
    [Inject] private GlobalEvent _globalEvent;
    
    private void Start() => _playerDamagable = _globalEvent.onPlayerDamagableGet?.Invoke();
        

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
            _playerDamagable.TakeDamage(_damage);
    }
}
