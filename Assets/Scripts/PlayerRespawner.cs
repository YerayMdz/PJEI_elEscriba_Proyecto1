using UnityEngine;

public class PlayerRespawner : MonoBehaviour
{
    void Start()
    {
        if (PlayerPositionManager.savedPosition != Vector3.zero)
        {
            // Restaurar desde la variable est�tica
            PlayerPositionManager.RestorePosition(gameObject);
            Debug.Log("Posici�n restaurada desde PlayerPositionManager: " + transform.position);
        }
        else
        {
            // Usar un punto de aparici�n por defecto
            Transform fallback = GameObject.Find("DefaultSpawnPoint")?.transform;

            if (fallback != null)
            {
                transform.position = fallback.position;
                Debug.Log("Usando posici�n por defecto: " + transform.position);
            }
            else
            {
                Debug.LogWarning("No se encontr� DefaultSpawnPoint.");
            }
        }
    }
}

