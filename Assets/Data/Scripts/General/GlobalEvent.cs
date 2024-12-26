using UnityEngine;
using UnityEngine.Events;

public class GlobalEvent : MonoBehaviour
{
    #region delegate

    public delegate Transform OnObjectPosition();
    public OnObjectPosition onPlayerPositionGet;
    public OnObjectPosition onEndZonePositionGet;

    public delegate IDamagable OnPlayerDamagable();
    public OnPlayerDamagable onPlayerDamagableGet;

    public delegate bool OnMakeTransaction(int price, bool isCoin);
    public OnMakeTransaction onMakeTransaction;

    #endregion

    public UnityAction onSaveData;
    public UnityAction onShowWinScreen;
    public UnityAction onDisableComponent;

    public UnityAction<PlayableCharatcer> OnChangeCharcterList;

    public UnityAction onBindCamera;
    public UnityAction onKeyPicked;
    public UnityAction onMakeShoot;

    public UnityAction onRemoveLevelZone;
    public UnityAction onAddNewTile;

    public UnityAction onUpdateDisplayInfo;
    public UnityAction<int, bool> onUpdateBalance;
    public UnityAction<int, int, bool> onUpdateTradeBalance;

    public UnityAction onLowBalanceNotification;
    public UnityAction<int, bool, bool> onChangeBalanceNotification;

    public UnityAction<Transform> onCustomTargetUpdate;
    public UnityAction<IUpdatable, bool, bool> onUpdatableSubscription;
}
