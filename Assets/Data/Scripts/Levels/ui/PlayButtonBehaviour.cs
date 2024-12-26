using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayButtonBehaviour : MonoBehaviour
{
    [SerializeField] private Image _blackScreen;

    [SerializeField] private GameObject[] _startScreenParts;
    [SerializeField] private GameObject[] _coreScreenParts;

    public void OnPlayButton()
    {
        _blackScreen.gameObject.SetActive(true);
        _blackScreen.DOFade(1, 0.2f).OnComplete(() => 
        {
                ChangeObjectState(_startScreenParts, false);
                ChangeObjectState(_coreScreenParts, true);
        });
        _blackScreen.DOFade(0, 0.2f).SetDelay(1).OnComplete(() => _blackScreen.gameObject.SetActive(false)).SetEase(Ease.Linear);
    }

    private void ChangeObjectState(GameObject[] screen, bool state)
    {
        foreach(var part in screen)
        {
            part.transform.DOMove(new Vector3(part.transform.position.x, 20, part.transform.position.z), 
            0.1f).OnComplete(() => 
            {
                part.SetActive(state);
            }).SetEase(Ease.Linear);
        }
    }
}
