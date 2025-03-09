using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscena : MonoBehaviour
{
    public string escenaDestino = "NombreDeLaEscenaDeCombate";

    void OnMouseDown()
    {
        // Guardar posición ANTES de cambiar de escena
        Transform playerTransform = GameObject.FindWithTag("Player")?.transform;

        if (playerTransform != null)
        {
            PlayerPositionManager.SavePosition(playerTransform);
            SceneManager.LoadScene(escenaDestino);
        }
        else
        {
            Debug.LogWarning("No se encontró el jugador para guardar la posición.");
        }
    }
}


