using System.Linq;
using UnityEngine;
using Zenject;
public class RadiusAttack : MonoBehaviour, IUpdatable
{
    [SerializeField] private LayerMask _layer;

    private float _searchRadius;
    private float _oldDistance = float.MaxValue;

    private float _timeUpdate;

    private Collider _targetView;
    private PlayerController _playerController;
    private PlayerCore _playerCore;
    [Inject] private GlobalEvent _globalEvent;

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
        _playerCore = GetComponent<PlayerCore>();
        _searchRadius = _playerCore.RadiusAttack;
    }

    public void SubUpdate()
    {
        _timeUpdate += Time.deltaTime;

        if(_timeUpdate < 1)
            return;

        _timeUpdate = 0;

        Collider[] hitColliders =  Physics.OverlapSphere(transform.position, _searchRadius, _layer);

        if (_targetView != null && !hitColliders.Contains(_targetView))
            _targetView = null;

        if (_targetView == null)
        {
            _oldDistance = float.MaxValue;
            foreach(var enemy in hitColliders)
            {
                if(enemy == null)
                    return;

                float newDistance = (transform.position - enemy.transform.position).sqrMagnitude;
                if(newDistance < _oldDistance)
                {
                    _oldDistance = newDistance;
                    _targetView = enemy;
                }
            }
        }

        if(_targetView != null)
        {
            _playerController.SetViewTarget(_targetView);
            _globalEvent.onMakeShoot?.Invoke(); 
        }
        else
            _playerController.SetViewTarget(null);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _searchRadius);
    }

    private void OnEnable() => _globalEvent.onUpdatableSubscription?.Invoke(this, true, false);
    private void OnDisable() => _globalEvent.onUpdatableSubscription?.Invoke(this, false, false);
}

