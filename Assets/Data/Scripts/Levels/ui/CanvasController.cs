using UnityEngine;
using DG.Tweening;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private float _duration = 0.1f;
    private CanvasGroup _currentCanvas;

    private void Start() => _currentCanvas = GetComponent<CanvasGroup>();

    public void CanvasChange(CanvasGroup targetCanvas)
    {
        _currentCanvas.interactable = false;
        _currentCanvas.blocksRaycasts = false;
        _currentCanvas.DOFade(0, _duration).OnComplete(() => 
            {
                targetCanvas.DOFade(1, _duration).OnComplete(() => 
                {
                    targetCanvas.blocksRaycasts = true;
                    targetCanvas.interactable = true;
                });
            }).SetEase(Ease.Linear);
    }
}
