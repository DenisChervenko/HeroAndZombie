using UnityEngine;
using Zenject;

public class NewLevelZone : MonoBehaviour
{
    [Inject] private GlobalEvent _globalEvent;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            _globalEvent.onAddNewTile?.Invoke();
            gameObject.SetActive(false);
        }
            
    }
}
