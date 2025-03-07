using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using System.Collections;

public class DialogueCameraController : MonoBehaviour
{
    public Camera dialogueCamera; // Cámara de diálogo
    private Camera mainCamera; // Cámara principal
    public float transitionDuration = 1f; // Tiempo de transición en segundos
    private bool isTransitioning = false;

    private void Start()
    {
        mainCamera = Camera.main;
        dialogueCamera.gameObject.SetActive(false); // Asegurar que inicia desactivada
    }

    public void SwitchToDialogueCamera()
    {
        if (!isTransitioning)
            StartCoroutine(SmoothTransition(dialogueCamera, mainCamera, true));
    }

    public void SwitchToMainCamera()
    {
        if (!isTransitioning)
            StartCoroutine(SmoothTransition(mainCamera, dialogueCamera, false));
    }

    IEnumerator SmoothTransition(Camera targetCamera, Camera previousCamera, bool activatingDialogue)
    {
        isTransitioning = true;

        float elapsedTime = 0f;
        float startFOV = previousCamera.fieldOfView;
        float targetFOV = activatingDialogue ? 40f : 60f; // Reducir FOV al cambiar a la cámara de diálogo
        previousCamera.gameObject.SetActive(true); // Asegurar que la cámara anterior sigue activa al inicio

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / transitionDuration;

            // Suavizar la transición del FOV
            previousCamera.fieldOfView = Mathf.Lerp(startFOV, targetFOV, t);

            yield return null;
        }

        previousCamera.gameObject.SetActive(false); // Apagar la cámara anterior después de la transición
        targetCamera.gameObject.SetActive(true); // Activar la nueva cámara
        isTransitioning = false;
    }
}
