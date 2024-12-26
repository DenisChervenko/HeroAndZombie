using DG.Tweening;
using UnityEngine;
using Zenject;
public class DeadZoneMovement : MonoBehaviour, IUpdatable
{
    [SerializeField] private float _movementSpeed;

    [Inject] private GlobalEvent _globalEvent;
    private Rigidbody _rigidbody;
    private void Start() => _rigidbody = GetComponent<Rigidbody>();

    public void SubUpdate() => _rigidbody.linearVelocity = Vector3.forward * _movementSpeed * Time.fixedDeltaTime;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
            Debug.Log("ПИЗДА");
    }

    private Transform GetPosition() => this.transform;
    private void DeactivateObject() => transform.DOScale(0, 0.2f).OnComplete(() => gameObject.SetActive(false));
    
    private void OnEnable()
    {
        _globalEvent.onUpdatableSubscription?.Invoke(this, true, false);
        _globalEvent.onEndZonePositionGet += GetPosition;
        _globalEvent.onDisableComponent += DeactivateObject;
    }
    private void OnDisable() 
    {
        _globalEvent.onUpdatableSubscription?.Invoke(this, false, false);
        _globalEvent.onEndZonePositionGet -= GetPosition;
        _globalEvent.onDisableComponent -= DeactivateObject;
    } 
}
