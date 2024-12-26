using System.Collections;
using UnityEngine;
using Zenject;
public class ToxicArea : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _timeToAttack;
    private IDamagable _playerDamagable;
    private Coroutine _attackPlayer;

    [Inject] private GlobalEvent _globalEvent;
    private void Start() => _playerDamagable = _globalEvent.onPlayerDamagableGet?.Invoke();

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && _attackPlayer == null)
            _attackPlayer = StartCoroutine(AttackPlayer());
    } 

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && _attackPlayer != null)
        {
            StopCoroutine(_attackPlayer);
            _attackPlayer = null;
        }
    }

    private IEnumerator AttackPlayer()
    {
        while(true)
        {
            yield return new WaitForSeconds(_timeToAttack);
            _playerDamagable.TakeDamage(_damage);
        }
    }
}
