using UnityEngine;
using DG.Tweening;

public class StartMenuNavigation : MonoBehaviour
{
    [SerializeField] private Transform _currentScreen;

    [SerializeField] private Vector3 _direction;
    [SerializeField] private float _time;

    public void ChangeScree(Transform targetScreen)
    {
        _currentScreen.DOLocalMove(_direction, _time).OnComplete(() => 
        {
            targetScreen.DOLocalMove(new Vector3(transform.position.x, 0, transform.position.z), _time).SetEase(Ease.Linear);
        }).SetEase(Ease.Linear);
    }
}
