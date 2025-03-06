using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void Changescene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Changescene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
