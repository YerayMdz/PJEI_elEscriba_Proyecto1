using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Campo público para seleccionar el nombre de la escena desde el Inspector
    public string sceneToLoad;

    // Este método se llamará cuando quieras cargar la nueva escena
    public void LoadScene()
    {
        // Asegúrate de que haya un nombre de escena válido
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            // Guarda el nombre de la próxima escena que se debe cargar
            PlayerPrefs.SetString("NextScene", sceneToLoad);

            // Carga la escena de carga primero
            SceneManager.LoadScene("LoadingScene");
        }
        else
        {
            Debug.LogError("Scene name is empty!");
        }
    }
}
