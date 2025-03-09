using UnityEngine;

public class PlayerRespawner : MonoBehaviour
{
    void Start()
    {
        if (PlayerPositionManager.savedPosition != Vector3.zero)
        {
            // Restaurar desde la variable estática
            PlayerPositionManager.RestorePosition(gameObject);
            Debug.Log("Posición restaurada desde PlayerPositionManager: " + transform.position);
        }
        else
        {
            // Usar un punto de aparición por defecto
            Transform fallback = GameObject.Find("DefaultSpawnPoint")?.transform;

            if (fallback != null)
            {
                transform.position = fallback.position;
                Debug.Log("Usando posición por defecto: " + transform.position);
            }
            else
            {
                Debug.LogWarning("No se encontró DefaultSpawnPoint.");
            }
        }
    }
}

