using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class LoadingMessage
{
    public string message;
    public float duration;
}

public class LoadingManager : MonoBehaviour
{
    public TMP_Text loadingText;

    // Lista de mensajes de carga con duración personalizada
    public List<LoadingMessage> loadingMessages = new List<LoadingMessage>();

    // Nombre de la escena a cargar
    public string sceneToLoad;

    private void Start()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            StartCoroutine(ShowLoadingMessages());
        }
        else
        {
            Debug.LogError("¡El nombre de la escena está vacío! Asigna una escena en el Inspector.");
        }
    }

    private IEnumerator ShowLoadingMessages()
    {
        foreach (LoadingMessage msg in loadingMessages)
        {
            loadingText.text = msg.message;
            yield return new WaitForSeconds(msg.duration);
        }

        LoadScene();
    }

    private void LoadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad);
    }
}