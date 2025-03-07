using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCBubbleText : MonoBehaviour
{
    public GameObject dialogueBubble; // Burbuja de texto completa (panel + texto)
    public TextMeshProUGUI dialogueText; // Texto dentro de la burbuja
    public string message = "¡Buenos días!"; // Mensaje del NPC
    private CanvasGroup canvasGroup; // Para hacer la transición

    private void Start()
    {
        canvasGroup = dialogueBubble.GetComponent<CanvasGroup>(); // Obtener CanvasGroup
        canvasGroup.alpha = 0; // Iniciar invisible
        dialogueBubble.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialogueBubble.SetActive(true);
            dialogueText.text = message;
            StopAllCoroutines();
            StartCoroutine(FadeInBubble());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopAllCoroutines();
            StartCoroutine(FadeOutBubble());
        }
    }

    IEnumerator FadeInBubble()
    {
        float duration = 0.5f; // Duración de la transición
        float startAlpha = canvasGroup.alpha;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 1, time / duration);
            yield return null;
        }

        canvasGroup.alpha = 1;
    }

    IEnumerator FadeOutBubble()
    {
        float duration = 0.5f;
        float startAlpha = canvasGroup.alpha;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0, time / duration);
            yield return null;
        }

        canvasGroup.alpha = 0;
        dialogueBubble.SetActive(false);
    }
}