using UnityEngine;
using Zenject;

public class RemoveLevelZone : MonoBehaviour
{
    [SerializeField] private Collider _blockCollider;
    [Inject] private GlobalEvent _globalEvent;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            _globalEvent.onRemoveLevelZone?.Invoke();
            _blockCollider.enabled = true;
            gameObject.SetActive(false);
        }
    }
}
