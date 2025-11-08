using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public void OnSceneChange(int _sceneNum)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(_sceneNum);
    }

    public void OnExit()
    {
        Application.Quit();
    }
}
