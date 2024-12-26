using UnityEngine;
using Zenject;

public class ChestController : MonoBehaviour
{
    [SerializeField] private int _levelChest;
    private int _coinCount;
    private Animator _animator;
    [Inject] private GlobalEvent _globalEvent;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _coinCount = Random.Range(1, 501) * _levelChest;
    }

    public void DiactivateChest() => gameObject.SetActive(false);
    public void UpdateBalance() => _globalEvent.onUpdateBalance?.Invoke(_coinCount, true);

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        _animator.SetTrigger("OpenChest");
    }
}
