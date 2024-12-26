using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerRotator : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _sensitivityRotate;
    private Vector2 _startTouchPoint;
    private Vector2 _currentTouchPoint;

    public void OnBeginDrag(PointerEventData pointerEventData) => _startTouchPoint = pointerEventData.position;

    public void OnDrag(PointerEventData pointerEventData)
    {
        _currentTouchPoint = pointerEventData.position;
        float deltaX = _startTouchPoint.x - _currentTouchPoint.x;
        float rotateAngle = deltaX * _sensitivityRotate;
        _player.Rotate(0, rotateAngle, 0);
        _startTouchPoint = _currentTouchPoint;
    }
}
