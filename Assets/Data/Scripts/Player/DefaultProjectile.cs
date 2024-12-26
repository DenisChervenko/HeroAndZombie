using UnityEngine;
using Zenject;
public class DefaultProjectile : MonoBehaviour, IUpdatable
{
    [SerializeField] private float _lifeTime;
    private float _elapsedTime;

    [SerializeField] private float _speedMovement;
    [Inject] private GlobalEvent _globalEvent;

    private float _damage;
 
    public void InitVariable(float damage) => _damage = damage;

    public void SubUpdate()
    {
        _elapsedTime += Time.deltaTime;
        if( _elapsedTime > _lifeTime)
            gameObject.SetActive(false);    

        gameObject.transform.Translate(Vector3.forward * _speedMovement * Time.deltaTime);
    }
         
    public void StateControll(bool state) => gameObject.SetActive(state);
    public void UpdateTrasnform(Transform muzzleTransform)
    {
        gameObject.transform.rotation = muzzleTransform.rotation;
        gameObject.transform.position = muzzleTransform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            IDamagable idamagable = other.GetComponent<IDamagable>();
            idamagable.TakeDamage(_damage);
        }
        gameObject.SetActive(false);
    }

    private void OnEnable() => _globalEvent.onUpdatableSubscription?.Invoke(this, true, false);
    private void OnDisable() => _globalEvent.onUpdatableSubscription?.Invoke(this, false, false);

}   
