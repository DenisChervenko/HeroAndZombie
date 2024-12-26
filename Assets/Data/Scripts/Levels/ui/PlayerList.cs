using UnityEngine;
using Zenject;

public class PlayerList : MonoBehaviour
{
    [SerializeField] private GameObject[] _characters;
    public PlayableCharatcer[] _playableCharacter;

    private int _currentCharacter = 0;
    public int CurrentCharacter { get { return _currentCharacter; } }

    [Inject] private GlobalEvent _globalEvent;

    private void Start()
    {
        foreach(var character in _characters)
            character.SetActive(false);

        int indexCharacter = PlayerPrefs.GetInt("SelectedCharacter", 0);
        _globalEvent.OnChangeCharcterList?.Invoke(_playableCharacter[indexCharacter]);
        _characters[indexCharacter].SetActive(true);
        _currentCharacter = indexCharacter;
    }

    public void OnChangeCharacterButton(int index)
    {
        _characters[_currentCharacter].SetActive(false);

        if(index == 0)
        {
            _currentCharacter++;
            if(_currentCharacter >= _playableCharacter.Length)
                _currentCharacter = 0;
        }
        else
        {
            _currentCharacter--;
            if(_currentCharacter < 0)
                _currentCharacter = _playableCharacter.Length - 1;
        }

        _globalEvent.OnChangeCharcterList?.Invoke(_playableCharacter[_currentCharacter]);
        _characters[_currentCharacter].SetActive(true);
    }
}
