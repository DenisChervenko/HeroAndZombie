using Unity.Cinemachine;
using UnityEngine;
using Zenject;
public class CameraBind : MonoBehaviour
{
    [SerializeField] private CameraTarget _player;
    private CinemachineCamera _cinemachine;

    [Inject] private GlobalEvent _globalEvent;

    private void Start() => _cinemachine = GetComponent<CinemachineCamera>();

    private void BindCamera() => _cinemachine.Target = _player;
    private void UnbindCamera() => _cinemachine.Target = default;

    private void OnEnable() 
    {
        _globalEvent.onBindCamera += BindCamera;
        _globalEvent.onDisableComponent += UnbindCamera;
    }

    private void OnDisable() 
    {
        _globalEvent.onBindCamera -= BindCamera;
        _globalEvent.onDisableComponent -= UnbindCamera;
    }
}
