using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCameraController : MonoBehaviour
{
    public Camera questCamera; // C�mara que se activar� para la misi�n
    private Camera mainCamera; // C�mara principal del juego
    private ArrowIndicator arrowIndicator; // Referencia a la flecha de navegaci�n

    private void Start()
    {
        mainCamera = Camera.main;
        arrowIndicator = FindObjectOfType<ArrowIndicator>(); // Buscar la flecha en la escena

        if (questCamera != null)
        {
            questCamera.gameObject.SetActive(false); // Asegurar que la c�mara de misi�n inicia desactivada
        }
        else
        {
            Debug.LogError("QuestCameraController: No se ha asignado la c�mara de misi�n.");
        }

        if (arrowIndicator == null)
        {
            Debug.LogError("QuestCameraController: No se encontr� ArrowIndicator en la escena.");
        }
    }

    public void SwitchToQuestCamera()
    {
        if (questCamera != null)
        {
            mainCamera.gameObject.SetActive(false); // Apagar la c�mara principal
            questCamera.gameObject.SetActive(true); // Activar la c�mara de misi�n

            if (arrowIndicator != null)
            {
                arrowIndicator.gameObject.SetActive(false); // Ocultar la flecha de navegaci�n
            }
        }
    }

    public void SwitchToMainCamera()
    {
        if (questCamera != null)
        {
            questCamera.gameObject.SetActive(false); // Apagar la c�mara de misi�n
            mainCamera.gameObject.SetActive(true); // Volver a la c�mara principal

            if (arrowIndicator != null)
            {
                arrowIndicator.gameObject.SetActive(true); // Mostrar la flecha de navegaci�n
            }
        }
    }
}
