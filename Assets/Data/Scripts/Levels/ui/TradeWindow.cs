using DG.Tweening;
using UnityEngine;

public class TradeWindow : MonoBehaviour
{
    [SerializeField] private Vector3 _moveDirection;

    public void ChangeWindow(Transform _targetWindow)
    {
        gameObject.transform.DOLocalMove(_moveDirection, 0.3f).OnComplete(() => 
        {
            _targetWindow.DOLocalMove(Vector3.zero, 0.3f);
        }).SetEase(Ease.Linear);
    }
}
