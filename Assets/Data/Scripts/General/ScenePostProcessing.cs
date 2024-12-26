using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
public class ScenePostProcessing : MonoBehaviour
{
    [SerializeField] private UniversalRenderPipelineAsset _profile;
    [SerializeField] private VolumeProfile _menuProfile;
    [SerializeField] private VolumeProfile _gameProfile;
    private void Awake()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
            _profile.volumeProfile = _menuProfile;
        else
            _profile.volumeProfile = _gameProfile;
    }
}
