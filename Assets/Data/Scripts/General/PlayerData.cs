using System.IO;
using UnityEngine;
using Zenject;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private int _coinBalance;
    [SerializeField] private int _killedBalance;

    public int CoinBalance {get { return _coinBalance; }}
    public int KilledBalance {get {return _killedBalance; }}

    [SerializeField] private int[] _buyedCharacters;
    private string _filePath;

    [Inject] private GlobalEvent _globalEvent;
    private void Awake()
    {
        _filePath = Path.Combine(Application.persistentDataPath, "GeneralData.json");
        if(File.Exists(_filePath))
            LoadData();

        _coinBalance += PlayerPrefs.GetInt("CoinBalance");
        _killedBalance += PlayerPrefs.GetInt("KilledBalance");

        SaveData();

        PlayerPrefs.SetInt("CoinBalance", 0);
        PlayerPrefs.SetInt("KilledBalance", 0);
    }

    private void SaveData()
    {
        string json = JsonUtility.ToJson(this);
        File.WriteAllText(_filePath, json);
    }

    public void LoadData()
    {
        if(File.Exists(_filePath))
        {
            string json = File.ReadAllText(_filePath);
            JsonUtility.FromJsonOverwrite(json, this);
        }
    }

    public bool MakeTransaction(int price, bool isCoin)
    {
        if(isCoin)
        {
            if(price < _coinBalance)
            {
                _coinBalance -= price;
                return true;
            }
        }
        else
        {
            if(price < _killedBalance)
            {
                _killedBalance -= price;
                return true;
            }
        }

        _globalEvent.onLowBalanceNotification?.Invoke();
        return false;
    }

    public void AddBalance(int countMoney, bool isCoin)
    {
        if(isCoin)
            _coinBalance += countMoney;
        else
            _killedBalance += countMoney;

        _globalEvent.onChangeBalanceNotification?.Invoke(countMoney, isCoin, true);
        _globalEvent.onSaveData?.Invoke();
    }

    public void TradeBalance(int countLose, int countTake, bool isCoinTrade)
    {
        bool isSucces = true;

        if(isCoinTrade)
        {
            if(_killedBalance >= countLose)
            {
                _coinBalance += countTake;
                _killedBalance -= countLose;
            }
            else
                isSucces = false;
        }
        else
        {
            if(_coinBalance >= countLose)
            {
                _killedBalance += countTake;
                _coinBalance -= countLose;
            }
            else
                isSucces = false;
        }

        if(!isSucces)
        {
            _globalEvent.onLowBalanceNotification?.Invoke();
            return;
        }

        _globalEvent.onChangeBalanceNotification?.Invoke(countTake, isCoinTrade, true);
        _globalEvent.onChangeBalanceNotification?.Invoke(countLose, !isCoinTrade, false);
        
        _globalEvent.onSaveData?.Invoke();
    }

    private void OnEnable()
    {
        _globalEvent.onUpdateTradeBalance += TradeBalance;
        _globalEvent.onUpdateBalance += AddBalance;
        _globalEvent.onMakeTransaction += MakeTransaction;
        _globalEvent.onSaveData += SaveData;
    } 
    private void OnDisable()
    {
        _globalEvent.onUpdateTradeBalance -= TradeBalance;
        _globalEvent.onUpdateBalance -= AddBalance;
        _globalEvent.onMakeTransaction -= MakeTransaction;
        _globalEvent.onSaveData -= SaveData;
    }

}
