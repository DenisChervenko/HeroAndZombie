using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(DamageAnimation))]
public class PlayerCore : MonoBehaviour, IDamagable
{
    [SerializeField] private Slider _hpBar;
    [SerializeField] private PlayableCharatcer _curretnCharacter;

    private float _health;
    private float _defend;

    public float Speed { get {return _curretnCharacter.speed;}}
    public float AttackSpeed { get {return _curretnCharacter.attackSpeed;}}
    public float Damage { get {return _curretnCharacter.damage;}}
    public float RadiusAttack { get {return _curretnCharacter.attackRange;}}

    private float _maxHealth;

    private DamageAnimation _damageAnimation;
    [Inject] private GlobalEvent _globalEvent;

    private void Awake()
    {
        _damageAnimation = GetComponent<DamageAnimation>();

        InitCharacter();
        _hpBar.value = 1;
    }

    private void InitCharacter()
    {
        _health = _curretnCharacter.health;
        _defend = _curretnCharacter.defend;
        _maxHealth = _health;
    }
    public void TakeDamage(float damage)
    {
        _damageAnimation.StartAnimation();
        float totalDamage = damage - _defend;
        _health -= totalDamage;
        _hpBar.value -= totalDamage / _maxHealth;

        if(_health <= 0)
            Debug.Log("YOU DIE");
    }


    public void State(bool state) => this.enabled = state;

    private IDamagable GetPlayerDamagable() => this;

    private void OnEnable() => _globalEvent.onPlayerDamagableGet += GetPlayerDamagable;
    private void OnDisable() => _globalEvent.onPlayerDamagableGet -= GetPlayerDamagable;
}
