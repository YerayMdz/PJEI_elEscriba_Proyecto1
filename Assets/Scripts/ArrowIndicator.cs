using UnityEngine;

public class ArrowIndicator : MonoBehaviour
{
    public Transform target; // El objetivo de la misi�n
    public Transform player; // El jugador
    public RectTransform arrowUI; // La imagen de la flecha en el Canvas
    public Camera playerCamera; // La c�mara del jugador
    public Camera currentCamera; // C�mara activa en la escena

    private bool isArrowActive = true; // Estado de la flecha

    void Update()
    {
        // Si la c�mara activa no es la del jugador, desactivar la flecha
        if (currentCamera != playerCamera)
        {
            if (isArrowActive) // Solo desactivar si a�n no lo est�
            {
                arrowUI.gameObject.SetActive(false);
                isArrowActive = false;
            }
            return;
        }
        else
        {
            if (!isArrowActive) // Reactivar si volvemos a la c�mara del jugador
            {
                arrowUI.gameObject.SetActive(true);
                isArrowActive = true;
            }
        }

        if (target == null || player == null || arrowUI == null || playerCamera == null)
        {
            Debug.LogError("Faltan referencias en ArrowIndicator.");
            return;
        }

        // Convertir la posici�n del objetivo a coordenadas de pantalla
        Vector3 targetScreenPos = Camera.main.WorldToScreenPoint(target.position);

        // Obtener la direcci�n en la pantalla (en 2D)
        Vector3 direction = targetScreenPos - arrowUI.position;

        // Calcular el �ngulo para la flecha (invertido)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Aplicar la rotaci�n a la UI (corrigiendo el giro)
        arrowUI.localRotation = Quaternion.Euler(0, 0, angle - 180);
    }

    // M�todo para cambiar la c�mara activa
    public void SetCurrentCamera(Camera newCamera)
    {
        currentCamera = newCamera;
    }
}

