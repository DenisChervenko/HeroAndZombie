using UnityEngine;
using Zenject;
public class LevelAudioController : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _shoot;

    [Inject] private GlobalEvent _globalEvent;

    private void MakeShoot()
    {
        _audioSource.pitch = Random.Range(0.95f, 1.06f);
        _audioSource.PlayOneShot(_shoot);
    }

}
