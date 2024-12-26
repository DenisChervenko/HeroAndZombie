using TMPro;
using UnityEngine;
using Zenject;

public class DataLoader : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;
    
    [SerializeField] private TMP_Text _coinBalance;
    [SerializeField] private TMP_Text _killedBalance;

    [Inject] private GlobalEvent _globalEvent;

    private void Start() => UpdateBalance();

    private void UpdateBalance()
    {
        _coinBalance.text = _playerData.CoinBalance.ToString();
        _killedBalance.text = _playerData.KilledBalance.ToString();
    }

    private void OnEnable() => _globalEvent.onUpdateDisplayInfo += UpdateBalance;
    private void OnDisable() => _globalEvent.onUpdateDisplayInfo -= UpdateBalance;
}
