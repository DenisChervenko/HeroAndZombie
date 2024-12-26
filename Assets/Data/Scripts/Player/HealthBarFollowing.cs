using UnityEngine;
using Zenject;

public class HealthBarFollowing : MonoBehaviour, IUpdatable
{
    [SerializeField] private Transform _playerPosition;
    [SerializeField] private float _offset;

    private Camera mainCamera;
    [Inject] private GlobalEvent _globalEvent;
    private void Start()
    {
        if(_playerPosition == null)
            _playerPosition = GameObject.Find("Player").transform;

        mainCamera = Camera.main;
    } 

    public void SubUpdate() => gameObject.transform.position = _playerPosition.position * _offset;

    private void DisableComponent() => gameObject.SetActive(false);

    private void OnEnable()
    {
        _globalEvent.onUpdatableSubscription?.Invoke(this, true, true);
        _globalEvent.onDisableComponent += DisableComponent;
    } 
    private void OnDisable()
    {
        _globalEvent.onUpdatableSubscription?.Invoke(this, false, true);
        _globalEvent.onDisableComponent -= DisableComponent;
    } 
}
