using UnityEngine;
using Zenject;

[RequireComponent(typeof(PlayerList))]
public class CharacterTransaction : MonoBehaviour
{
    private PlayableCharatcer[] _characters;
    private PlayerList _playerList;

    [Inject] private GlobalEvent _globalEvent;

    private void Start()
    {
        _playerList = GetComponent<PlayerList>();

        _characters = new PlayableCharatcer[_playerList._playableCharacter.Length];
        for(int i = 0; i < _playerList._playableCharacter.Length; i++)
            _characters[i] = _playerList._playableCharacter[i];
    }

    public void UpdrageCharacter()
    {
        int index = _playerList.CurrentCharacter;
        bool isCoin = true;
        int level = _characters[index].level;

        if(_characters[index].level == 4 || _characters[index].level == 9)
            isCoin = false;

        if(_globalEvent.onMakeTransaction.Invoke(_characters[index].upgradePrice[level], isCoin))
        {
            _characters[index].level++;

            _characters[index].damage += _characters[index].modifierIndex[level];
            _characters[index].health += _characters[index].modifierIndex[level];
            _characters[index].defend += _characters[index].modifierIndex[level];
            
            _globalEvent.onChangeBalanceNotification?.Invoke(_characters[index].upgradePrice[level], isCoin, false);
            _globalEvent.onUpdateDisplayInfo?.Invoke();
            _globalEvent.onSaveData?.Invoke();
        }
    }

    public void BuyCharacter()
    {
        int index = _playerList.CurrentCharacter;
        bool isCoin = true;

        if(_globalEvent.onMakeTransaction.Invoke(_characters[index].price, isCoin))
        {
            _characters[index].isBuyed = true;

            _globalEvent.onChangeBalanceNotification?.Invoke(_characters[index].price, isCoin, false);
            _globalEvent.onUpdateDisplayInfo?.Invoke();
            _globalEvent.onSaveData?.Invoke();
        }
    }

    public void SelectCharacter()
    {
        foreach(var character in _characters)
            character.isSelected = false;

        _characters[_playerList.CurrentCharacter].isSelected = true;
        _globalEvent.onUpdateDisplayInfo?.Invoke();
        PlayerPrefs.SetInt("SelectedCharacter", _playerList.CurrentCharacter);
    }
}
