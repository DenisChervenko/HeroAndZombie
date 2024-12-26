using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButtonController : MonoBehaviour
{
    public void OnChangeScene(int sceneIndex) => SceneManager.LoadScene(sceneIndex);
}
