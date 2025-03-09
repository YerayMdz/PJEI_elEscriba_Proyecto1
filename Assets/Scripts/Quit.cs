using UnityEngine;

public class Quit : MonoBehaviour
{
    public void QuitGame()
    {
        // Sale del juego en una build (ejecutable)
        Application.Quit();

        // Detiene el juego en el editor de Unity
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
