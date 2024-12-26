using UnityEngine;
using Zenject;
using TMPro;
using System.Text;

public class CurrentLevelData : MonoBehaviour
{
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private TMP_Text _coinText;
    [SerializeField] private TMP_Text _killedText;
    private int _killedBalance;
    private int _coinBalance;
    private int _currentLevel;

    [Inject] private GlobalEvent _globalEvent;

    private StringBuilder _strignBuilder = new StringBuilder();

    private void Awake()
    {
        _currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        _levelText.text = "LEVEL " + _currentLevel;
    }

    private void UpdateBalance(int balance, bool isCoin)
    {
        if(isCoin)
        {
            _coinBalance += balance;
            ChangeText(_coinText, _coinBalance);
        }
        else
        {
            _killedBalance += balance;
            ChangeText(_killedText, _killedBalance);
        }
    }

    private void ChangeText(TMP_Text text, int textMoney)
    {
        _strignBuilder.Clear();
        _strignBuilder.Append(textMoney);
        text.text = _strignBuilder.ToString();
    }

    private void SaveData()
    {
        int previousCoinBalance = PlayerPrefs.GetInt("CoinBalance", 0);
        int previousKilledBalance = PlayerPrefs.GetInt("KilledBalance", 0);

        PlayerPrefs.SetInt("CoinBalance", _coinBalance + previousCoinBalance);
        PlayerPrefs.SetInt("KilledBalance", _killedBalance + previousKilledBalance);

        _currentLevel++;
        PlayerPrefs.SetInt("CurrentLevel", _currentLevel);
    }

    private void OnEnable()
    {
        _globalEvent.onUpdateBalance += UpdateBalance;
        _globalEvent.onSaveData += SaveData;
    } 
    private void OnDisable() 
    {
        _globalEvent.onUpdateBalance -= UpdateBalance;
        _globalEvent.onSaveData -= SaveData;
    } 
}