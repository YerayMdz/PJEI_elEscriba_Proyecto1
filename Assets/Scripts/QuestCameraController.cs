using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCameraController : MonoBehaviour
{
    public Camera questCamera; // Cámara que se activará para la misión
    private Camera mainCamera; // Cámara principal del juego
    private ArrowIndicator arrowIndicator; // Referencia a la flecha de navegación

    private void Start()
    {
        mainCamera = Camera.main;
        arrowIndicator = FindObjectOfType<ArrowIndicator>(); // Buscar la flecha en la escena

        if (questCamera != null)
        {
            questCamera.gameObject.SetActive(false); // Asegurar que la cámara de misión inicia desactivada
        }
        else
        {
            Debug.LogError("QuestCameraController: No se ha asignado la cámara de misión.");
        }

        if (arrowIndicator == null)
        {
            Debug.LogError("QuestCameraController: No se encontró ArrowIndicator en la escena.");
        }
    }

    public void SwitchToQuestCamera()
    {
        if (questCamera != null)
        {
            mainCamera.gameObject.SetActive(false); // Apagar la cámara principal
            questCamera.gameObject.SetActive(true); // Activar la cámara de misión

            if (arrowIndicator != null)
            {
                arrowIndicator.gameObject.SetActive(false); // Ocultar la flecha de navegación
            }
        }
    }

    public void SwitchToMainCamera()
    {
        if (questCamera != null)
        {
            questCamera.gameObject.SetActive(false); // Apagar la cámara de misión
            mainCamera.gameObject.SetActive(true); // Volver a la cámara principal

            if (arrowIndicator != null)
            {
                arrowIndicator.gameObject.SetActive(true); // Mostrar la flecha de navegación
            }
        }
    }
}
