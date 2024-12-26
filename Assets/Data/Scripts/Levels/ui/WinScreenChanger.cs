using UnityEngine;
using DG.Tweening;
using Zenject;
using UnityEngine.SceneManagement;

public class WinScreenChanger : MonoBehaviour
{
    [SerializeField] private Transform[] _playableCanvas;
    [SerializeField] private Transform _winCanvas;

    [SerializeField] private Vector3 _direction;
    [SerializeField] private float _time;

    [SerializeField] private int _currentScreenIndex;

    [Inject] private GlobalEvent _globalEvent;

    private void ChangeScreen()
    {
        foreach(var screen in _playableCanvas)
            SubChange(screen);
    }

    private void SubChange(Transform screen)
    {
        screen.DOLocalMove(_direction, _time).OnComplete(() => 
        {
            if(_currentScreenIndex >= _playableCanvas.Length)
                _winCanvas.DOLocalMove(Vector3.zero, _time).SetEase(Ease.Linear);
        }).SetEase(Ease.Linear);
        _currentScreenIndex++;
    }

    public void ChangeScene(int index) => SceneManager.LoadScene(index);

    private void OnEnable() => _globalEvent.onShowWinScreen += ChangeScreen;
    private void OnDisable() => _globalEvent.onShowWinScreen += ChangeScreen;
}
