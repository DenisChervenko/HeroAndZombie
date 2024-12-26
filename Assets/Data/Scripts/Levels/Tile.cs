using UnityEngine;

public class Tile : MonoBehaviour
{
    private Vector3 _spawnPoint;
    public Vector3 SpawnPoint { get { return _spawnPoint; } }
    private Renderer _renderer;
    private void Awake() => _renderer = GetComponent<Renderer>();

    public void ActiveState(bool state)
    {
        _spawnPoint = new Vector3(0, 0, _renderer.bounds.max.z-.1f);
        gameObject.SetActive(state);
    } 

    public bool CurrentObjectState() => gameObject.activeSelf;
}
