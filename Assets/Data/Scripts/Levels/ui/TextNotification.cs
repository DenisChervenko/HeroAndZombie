using TMPro;
using UnityEngine;
using DG.Tweening;
using Zenject;

public class TextNotification : MonoBehaviour
{
    [SerializeField] private TMP_Text _lowBalance;
    [SerializeField] private TMP_Text _balanceNotificationCoin;
    [SerializeField] private TMP_Text _balanceNotificationSkull;

    [Inject] private GlobalEvent _globalEvent;

    private void LowBalanceNotification() => _lowBalance.DOFade(1, 0.1f).
    OnComplete(() => _lowBalance.DOFade(0, 0.1f).SetDelay(2));

    private void ChangeBalanceNotification(int balance, bool isCoin, bool isAddBalance)
    {
        TMP_Text textLabel = null;

        if(isCoin)
            textLabel = _balanceNotificationCoin;
        else
            textLabel = _balanceNotificationSkull;

        textLabel.transform.rotation = Quaternion.Euler(0, 0, Random.Range(10, -11));

        textLabel.DOKill();
        textLabel.color = isAddBalance ? Color.green : Color.red;
        textLabel.text = (isAddBalance ? "+" : "-") + balance.ToString();
        textLabel.DOFade(1, 0.1f).OnComplete(() => textLabel.DOFade(0, 0.1f).SetDelay(2));
    }

    private void OnEnable()
    {
        _globalEvent.onLowBalanceNotification += LowBalanceNotification;
        _globalEvent.onChangeBalanceNotification += ChangeBalanceNotification;
    }
    private void OnDisable()
    {
        _globalEvent.onLowBalanceNotification -= LowBalanceNotification;
        _globalEvent.onChangeBalanceNotification -= ChangeBalanceNotification;
    }
}
