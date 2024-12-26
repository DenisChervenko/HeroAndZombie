using UnityEngine;
public class TrapController : MonoBehaviour
{

    private Animator _animator;


    private void Start() => _animator = GetComponent<Animator>();
        
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
            _animator.SetTrigger("Active");
    }
}
