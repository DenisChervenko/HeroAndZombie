using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class GeneralUpdate : MonoBehaviour
{
    private List<IUpdatable> _updatable = new List<IUpdatable>();

    private List<IUpdatable> _toAdd = new List<IUpdatable>();
    private List<IUpdatable> _toRemove = new List<IUpdatable>();

    private List<IUpdatable> _lateUpdatable = new List<IUpdatable>();

    private List<IUpdatable> _toAddLate = new List<IUpdatable>();
    private List<IUpdatable> _toRemoveLate = new List<IUpdatable>();

    [Inject] private GlobalEvent _globalEvent;

    private void Awake() => InitializeSuibscirbers();

    private void Update()
    {
        if (_updatable == null)
            return;

        foreach(var updatable in _updatable)
        {
            if(updatable == null)
                continue;
            updatable.SubUpdate();
        }
            
        TryToChangeMainUpdatable();
    }

    private void LateUpdate()
    {
        if (_lateUpdatable == null)
            return;

        foreach(var updatable in _lateUpdatable)
        {
            if(updatable == null)
                continue;
            updatable.SubUpdate();
        }
            
        TryToChangeLateUpdatable();   
    }

    private void TryToChangeMainUpdatable()
    {
        if(_toAdd != null)
        {
            foreach(var updatable in _toAdd)
                _updatable.Add(updatable);

            _toAdd.Clear();
        }
        
        if(_toRemove != null)
        {
            foreach(var updatable in _toRemove)
                _updatable.Remove(updatable);

            _toRemove.Clear();
        }
    }

    private void TryToChangeLateUpdatable()
    {
        if(_toAddLate != null)
        {
            foreach(var updatable in _toAddLate)
                _lateUpdatable.Add(updatable);

            _toAddLate.Clear();
        }
        
        if(_toRemoveLate != null)
        {
            foreach(var updatable in _toRemoveLate)
                _lateUpdatable.Remove(updatable);

            _toRemoveLate.Clear();
        }
    }

    private void OnUpdatableChanged(IUpdatable updatable, bool isAdded, bool isLateUpdate)
    {
        if(isLateUpdate)
        {
            if(isAdded)
                _toAddLate.Add(updatable);
            else
                _toRemoveLate.Add(updatable);
                
            return;
        }
        if (isAdded)
            _toAdd.Add(updatable);
        else
            _toRemove.Add(updatable);
    }

    private void OnEnable() => InitializeSuibscirbers();
    private void OnDisable() => _globalEvent.onUpdatableSubscription -= OnUpdatableChanged;

    private void InitializeSuibscirbers() => _globalEvent.onUpdatableSubscription += OnUpdatableChanged;
}
