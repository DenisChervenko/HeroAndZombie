using UnityEngine;
using DG.Tweening;
using Zenject;
public class PlayerController : MonoBehaviour, IUpdatable
{
    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _speedRotation;

    [Inject] private GlobalEvent  _globalEvent;

    private PlayerCore _playerCore;
    private Collider _viewTarget;
    private Animator _animator;

    private int _blendValue;
    private float _speedMovement;
    

    private void Start()
    {
        _playerCore = GetComponent<PlayerCore>();
        _animator = GetComponent<Animator>();
        _animator.Rebind();

        _blendValue = Animator.StringToHash("Blend");
        _speedMovement = _playerCore.Speed;

        if(_characterController == null)
            _characterController = GetComponent<CharacterController>();
        if(_speedMovement == 0)
            _speedMovement = 10;
    }

    public void SubUpdate()
    {
        _animator.SetFloat(_blendValue, _joystick.Direction.magnitude);

        if(_viewTarget != null)
        {
            Vector3 distance = new Vector3(_viewTarget.transform.position.x - gameObject.transform.position.x, 0, 
            _viewTarget.transform.position.z - gameObject.transform.position.z);
            Quaternion targetRotation = Quaternion.LookRotation(distance);
            gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, targetRotation, _speedRotation * Time.deltaTime);
        }

        if(_joystick.Direction == Vector2.zero)
            return;
        
        Vector3 direction = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);
        _characterController.Move(direction * _speedMovement * Time.deltaTime);

        if(_viewTarget == null)
            gameObject.transform.rotation = Quaternion.LookRotation(direction * _speedRotation * Time.deltaTime);
    }

    public void SetViewTarget(Collider viewTarget) => _viewTarget = viewTarget;
    private Transform PlayerPosition() => gameObject.transform;
    private void DisableMovement()
    {
        _globalEvent.onUpdatableSubscription?.Invoke(this, false, false);

        gameObject.transform.DORotate(Vector3.zero, 0.2f);
        gameObject.transform.DOMove(new Vector3(0, transform.position.y, transform.position.z + 30), 5f)
        .OnComplete(() => this.enabled = false).SetEase(Ease.Linear);

        _animator.SetFloat(_blendValue, 1);
    }

    private void OnEnable()
    {
        _globalEvent.onDisableComponent += DisableMovement;
        _globalEvent.onPlayerPositionGet += PlayerPosition;
        _globalEvent.onUpdatableSubscription?.Invoke(this, true, false);
    } 
    private void OnDisable()
    {
        _globalEvent.onDisableComponent -= DisableMovement;
        _globalEvent.onPlayerPositionGet -= PlayerPosition;
        _globalEvent.onUpdatableSubscription?.Invoke(this, false, false);
    } 
}
