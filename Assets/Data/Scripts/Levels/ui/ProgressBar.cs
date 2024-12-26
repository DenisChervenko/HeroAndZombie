using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ProgressBar : MonoBehaviour, IUpdatable
{
    [SerializeField] private Transform _endPoint;

    [SerializeField] private Slider _playerSlider;
    [SerializeField] private Slider _endZoneSlider;

    private Transform _playerPosition;
    private Transform _endZonePosition;

    private float _playerRemainingDistance;
    private float _endZoneRemainingDistance;

    private float _totalDistance;

    [Inject] private GlobalEvent _globalEvent;

    private void Start()
    {
        _playerPosition = _globalEvent.onPlayerPositionGet?.Invoke();
        _endZonePosition = _globalEvent.onEndZonePositionGet?.Invoke();

        _playerSlider.value = 0;
        _endZoneSlider.value = 0;

        _totalDistance = (_playerPosition.position - _endPoint.position).sqrMagnitude;
    }

    public void SubUpdate()
    {
        _playerRemainingDistance = (_playerPosition.position - _endPoint.position).sqrMagnitude;
        _endZoneRemainingDistance = (_endZonePosition.position - _endPoint.position).sqrMagnitude;

        float clampedDistance = 1f - Mathf.Clamp01(_playerRemainingDistance / _totalDistance);
        _playerSlider.value = clampedDistance;
        clampedDistance = 1f - Mathf.Clamp01(_endZoneRemainingDistance / _totalDistance);
        _endZoneSlider.value = clampedDistance;
    }

    private void OnEnable() => _globalEvent.onUpdatableSubscription?.Invoke(this, true, false);
    private void OnDisable() => _globalEvent.onUpdatableSubscription?.Invoke(this, false, false);
}
