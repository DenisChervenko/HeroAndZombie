using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelController : MonoBehaviour
{
    [SerializeField] private Tile[] _propsVariationPrefab;
    [SerializeField] private List<Tile> _propsReady;
    
    private Vector3 _spawnPoint;

    private int _environmentIndex;
    private int _tileIndex = 1;

    [Inject] private GlobalEvent _globalEvent;
    [Inject] private DiContainer _container;

    private void Start()
    {
        for (int i = 0; i < _propsVariationPrefab.Length; i++)
        {
            _propsReady.Add(_container.InstantiatePrefab(_propsVariationPrefab[i]).GetComponent<Tile>());
            _propsReady[i].ActiveState(false);
        }

        OnGenerateStartTile();
    }

    private void OnGenerateStartTile()
    {
        Vector3 spawnPoint = Vector3.zero;

        _propsReady[0].transform.position = spawnPoint;
        _propsReady[0].ActiveState(true);
        _environmentIndex++;
    }

    private void OnGenerateNewTile()
    {
        if(_environmentIndex >= _propsReady.Count)
            return;
        
        _propsReady[_environmentIndex].ActiveState(true);

        _spawnPoint += _propsReady[_environmentIndex - 1].SpawnPoint;
        _propsReady[_environmentIndex].transform.position = _spawnPoint;

        _environmentIndex++;
    }

    private void OnRemoveOldTile()
    {
        for(int i = _tileIndex; i < _propsReady.Count; i++)
        {
            if( _propsReady[i].CurrentObjectState())
            {
                _propsReady[i - 1].ActiveState(false);
                _tileIndex++;
                break;
            }
        }
    }

    private void OnEnable()
    {
        _globalEvent.onAddNewTile += OnGenerateNewTile;
        _globalEvent.onRemoveLevelZone += OnRemoveOldTile;
    }
    private void OnDisable() 
    {
        _globalEvent.onAddNewTile -= OnGenerateNewTile;
        _globalEvent.onRemoveLevelZone -= OnRemoveOldTile;
    }
} 
