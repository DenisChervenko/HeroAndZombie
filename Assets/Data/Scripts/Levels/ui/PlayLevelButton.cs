using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayLevelButton : MonoBehaviour
{   
    public void StartLevel() => SceneManager.LoadScene(1);
}
