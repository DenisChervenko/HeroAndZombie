using UnityEngine;
using DG.Tweening;

public class LogoAnimation : MonoBehaviour
{
    [SerializeField] private Transform _text;
    [SerializeField] private Vector3 _rotateStep;

    Sequence sequanceLogoText;
    private void Start()
    {
        sequanceLogoText = DOTween.Sequence();
        sequanceLogoText.Append(_text.DOScale(1.2f, 3).SetEase(Ease.InOutSine));
        sequanceLogoText.Join(_text.DORotate(_rotateStep, 3).SetEase(Ease.Linear));

        sequanceLogoText.SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDisable() => sequanceLogoText.Kill();
}
