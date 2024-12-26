using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class InformationDisplay : MonoBehaviour
{
    [Header("Info on TOP")]
    [SerializeField] private TMP_Text _characterName;
    [SerializeField] private TMP_Text _characterClass;
    [SerializeField] private TMP_Text _characterRarity;
    [SerializeField] private TMP_Text _characterLevel;

    [SerializeField] private GameObject[] _rarityBackground;

    [Space()]
    [Header("Info on DOWN")]
    [SerializeField] private TMP_Text _characterAttack;
    [SerializeField] private TMP_Text _characterDefend;
    [SerializeField] private TMP_Text _characterHealth;
    [SerializeField] private TMP_Text _characterMovementSpeed;

    [Space()]
    [SerializeField] private Image _coinIcon;
    [SerializeField] private Image _skullIcon;
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private GameObject _upgradeLabel;

    [SerializeField] private TMP_Text _upgradeField;

    [Space()]
    [SerializeField] private TMP_Text _buyPrice;
    [SerializeField] private TMP_Text _upgradePrice;
    [SerializeField] private Button _selectButton;
    [SerializeField] private Button _buyButton;

    private StringBuilder _sb = new StringBuilder();
    private PlayableCharatcer _previousCharacter;

    [Inject] private GlobalEvent _globalEvent;

    private void UpdateInformation() => OnChangeDisplayInfo(_previousCharacter);

    private void OnChangeDisplayInfo(PlayableCharatcer character)
    {
        if(character.isSelected)
            _selectButton.interactable = false;
        else
            _selectButton.interactable = true;

        if(character.isBuyed)
        {
            _buyButton.gameObject.SetActive(false);
            _selectButton.gameObject.SetActive(true);
            _upgradeButton.gameObject.SetActive(true);
        }
        else
        {
            _buyPrice.text = character.price.ToString();
            _buyButton.gameObject.SetActive(true);
            _selectButton.gameObject.SetActive(false);
            _upgradeButton.gameObject.SetActive(false);  
        }

        if(character.level == 4 || character.level == 9)
        {
            _coinIcon.enabled = false;
            _skullIcon.enabled = true;
        }
        else
        {
            _coinIcon.enabled = true;
            _skullIcon.enabled = false; 
        }

        if(character.level == 10)
        {
            _upgradeButton.gameObject.SetActive(false);
            _upgradeLabel.SetActive(false);
        }
        else
        {
            _upgradeField.text = "To " + (character.level + 1) + "  Level";
            FloatToStringConvertor(_upgradePrice, character.upgradePrice[character.level]);
        }

        _characterName.text = character.name;
        _characterClass.text = character.ability;
        _characterRarity.text = character.rarity;
        FloatToStringConvertor(_characterLevel, character.level);
        FloatToStringConvertor(_characterAttack, character.damage);
        FloatToStringConvertor(_characterDefend, character.defend);
        FloatToStringConvertor(_characterHealth, character.health);
        FloatToStringConvertor(_characterMovementSpeed, character.speed);

        foreach(var background in _rarityBackground)
            background.SetActive(false);    

        _rarityBackground[character.rarityIndex].SetActive(true);
        _previousCharacter = character;
    }

    private void FloatToStringConvertor(TMP_Text textField, float text)
    {
        _sb.Append(text);
        textField.text = _sb.ToString();
        _sb.Clear();
    }

    private void OnEnable()
    {
        _globalEvent.OnChangeCharcterList += OnChangeDisplayInfo;
        _globalEvent.onUpdateDisplayInfo += UpdateInformation;
    } 
    private void OnDisable()
    {
        _globalEvent.OnChangeCharcterList -= OnChangeDisplayInfo;
        _globalEvent.onUpdateDisplayInfo -= UpdateInformation;
    }
}
