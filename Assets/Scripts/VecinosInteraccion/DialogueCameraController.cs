using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using System.Collections;

public class DialogueCameraController : MonoBehaviour
{
    public Camera dialogueCamera; // C�mara de di�logo
    private Camera mainCamera; // C�mara principal
    public float transitionDuration = 1f; // Tiempo de transici�n en segundos
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
        float targetFOV = activatingDialogue ? 40f : 60f; // Reducir FOV al cambiar a la c�mara de di�logo
        previousCamera.gameObject.SetActive(true); // Asegurar que la c�mara anterior sigue activa al inicio

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / transitionDuration;

            // Suavizar la transici�n del FOV
            previousCamera.fieldOfView = Mathf.Lerp(startFOV, targetFOV, t);

            yield return null;
        }

        previousCamera.gameObject.SetActive(false); // Apagar la c�mara anterior despu�s de la transici�n
        targetCamera.gameObject.SetActive(true); // Activar la nueva c�mara
        isTransitioning = false;
    }
}
