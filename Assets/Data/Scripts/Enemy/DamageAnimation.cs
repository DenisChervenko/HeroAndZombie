using UnityEngine;
using DG.Tweening;
public class DamageAnimation : MonoBehaviour
{
    [SerializeField] private ParticleSystem _dieParticle;
    [SerializeField] private Renderer[] _renderer;

    private Material _instanceMaterial;
    private Color _defaultColor;
    private IDamagable _rootObject;
    private void Start()
    {
        _rootObject = GetComponent<IDamagable>();

        if(gameObject.tag == "Player")
            _renderer = GetComponentsInChildren<SkinnedMeshRenderer>();
        else
            _renderer = GetComponentsInChildren<Renderer>();

        _instanceMaterial = new Material(_renderer[0].material);
        _defaultColor = _instanceMaterial.GetColor("_BaseColor");

        foreach(var renderer in _renderer)
            renderer.material = _instanceMaterial;
    }

    public void StartAnimation()
    {
        _instanceMaterial.SetColor("_BaseColor", Color.red);

        transform.DOScale(0.8f, 0.07f).OnComplete(() => 
        {
            transform.DOScale(1, 0.07f);
            _instanceMaterial.SetColor("_BaseColor", _defaultColor);
        }).SetEase(Ease.Linear);
    }

    public void DieEffect()
    {
        _dieParticle.Play();
        _rootObject.State(false);
        transform.DOScale(0, 0.3f).OnComplete(() => 
        {
            gameObject.SetActive(false);
        }).SetEase(Ease.Linear);
    }
}
