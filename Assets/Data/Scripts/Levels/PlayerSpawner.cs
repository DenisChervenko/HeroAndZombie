using UnityEngine;
using DG.Tweening;
using Zenject;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private CanvasGroup _healthBar;

    [SerializeField] private Transform[] _topUI;
    [SerializeField] private Transform _player;

    [SerializeField] private ParticleSystem _spawnParticle;

    [SerializeField] private PlayableCharatcer[] _characters;
    [SerializeField] private GameObject[] _charatersPrefab;

    [Inject] private GlobalEvent _globalEvent;

    private void Awake()
    {
        Transform selectedCharatcer = null;
        for(int i = 0; i < _characters.Length; i++)
        {
            if(!_characters[i].isSelected)
            {
                _charatersPrefab[i].SetActive(false);
                Destroy(_charatersPrefab[i]);
            }
            else
            {
                selectedCharatcer = _charatersPrefab[i].transform;
            }
        }
        
        _player.Find(selectedCharatcer.name + "/Armature").transform.SetParent(_player);
 
        float defaultY = _player.position.y;
        _player.position = new Vector3(0, 50, 0);
        _player.DOMove(new Vector3(0, defaultY, 0), 0.5f).OnComplete(() => 
        {
            _globalEvent.onBindCamera?.Invoke();

            _spawnParticle.Play();
            _healthBar.DOFade(1, 0.5f).SetEase(Ease.Linear);
            
            foreach(var ui in _topUI)
                ui.DOLocalMove(Vector3.zero, 0.5f).SetEase(Ease.Linear);
        }).SetEase(Ease.Linear);
    }
}
