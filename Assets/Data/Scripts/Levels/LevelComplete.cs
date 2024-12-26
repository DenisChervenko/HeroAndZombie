using UnityEngine;
using Zenject;

public class LevelComplete : MonoBehaviour
{
    [SerializeField] private ParticleSystem _confetti;
    [Inject] private GlobalEvent _globalEvent;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
            WinBehaviour();
    }

    private void WinBehaviour()
    {
        _confetti.Play();
        _globalEvent.onShowWinScreen?.Invoke();
        _globalEvent.onDisableComponent?.Invoke();
        _globalEvent.onSaveData?.Invoke();
    }
}
