using UnityEngine;
using DG.Tweening;

public class ObjectRotator : MonoBehaviour
{
    private Tween _animation;
    private void OnEnable() => _animation = transform.DOLocalRotate(new Vector3(0, 0, 360), 2f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1);
    private void OnDisable() => _animation.Kill();
}
