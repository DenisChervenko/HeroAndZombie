using UnityEngine;
using DG.Tweening;
using TMPro;

public class TimerBeforeStart : MonoBehaviour
{
    [SerializeField] private TMP_Text _numbers;
    private int _counter = 3;// 3 seconds to start like everywere 
    private void Start()
    {
        _numbers.text = _counter.ToString();

        _numbers.transform.DOScale(0.7f, 1).OnStepComplete(() =>
        {
            _numbers.transform.DOScale(1, 0);
            _counter--;
            _numbers.text = _counter.ToString();;
            if(_counter == 0)
                gameObject.SetActive(false);
        }).SetLoops(3).SetEase(Ease.Linear);
    }
}
