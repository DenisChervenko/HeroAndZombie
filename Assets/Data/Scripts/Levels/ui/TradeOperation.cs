using UnityEngine;
using Zenject;

public class TradeOperation : MonoBehaviour
{
    [SerializeField] private int _coinReward;
    [SerializeField] private int _skullLose;

    [SerializeField] private int _skullReward;
    [SerializeField] private int _coinLose;
    [Inject] private GlobalEvent _globalEvent; 
    public void OnTrade(bool isCoin)
    {
        if(isCoin)
            _globalEvent.onUpdateTradeBalance?.Invoke(_skullLose, _coinReward, isCoin);
        else
            _globalEvent.onUpdateTradeBalance?.Invoke(_coinLose, _skullReward, isCoin);

        _globalEvent.onUpdateDisplayInfo?.Invoke();
    }
}
