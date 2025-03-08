using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NPCQuestGiver : MonoBehaviour
{
    public string[] dialogueLines; // L�neas de di�logo del NPC
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public Button nextButton;
    public Quest assignedQuest; // Misi�n que otorgar� el NPC
    private int currentLine = 0;
    private QuestCameraController cameraController;
    private bool missionAccepted = false;

    private void Start()
    {
        dialoguePanel.SetActive(false);
        nextButton.onClick.AddListener(NextDialogue);
        cameraController = FindObjectOfType<QuestCameraController>(); // Obtener el controlador de la c�mara

        if (cameraController == null)
        {
            Debug.LogError("NPCQuestGiver: No se encontr� QuestCameraController en la escena.");
        }

        if (dialoguePanel == null)
            Debug.LogError("NPCQuestGiver: Falta asignar el Dialogue Panel en " + gameObject.name);

        if (dialogueText == null)
            Debug.LogError("NPCQuestGiver: Falta asignar el Dialogue Text en " + gameObject.name);

        if (nextButton == null)
            Debug.LogError("NPCQuestGiver: Falta asignar el Next Button en " + gameObject.name);

        if (assignedQuest == null)
            Debug.LogError("NPCQuestGiver: Falta asignar la misi�n en " + gameObject.name);

        if (QuestManager.instance == null)
            Debug.LogError("NPCQuestGiver: No se encontr� QuestManager en la escena.");

        dialoguePanel.SetActive(false);
        nextButton.onClick.AddListener(NextDialogue);
    }

    private void OnMouseDown()
    {
        if (!missionAccepted && !dialoguePanel.activeSelf)
        {
            StartConversation();
        }
    }

    void StartConversation()
    {
        dialoguePanel.SetActive(true);
        cameraController.SwitchToQuestCamera(); // Cambiar a la c�mara de di�logo
        currentLine = 0;
        dialogueText.text = dialogueLines[currentLine];
    }

    void NextDialogue()
    {
        currentLine++;

        if (currentLine < dialogueLines.Length)
        {
            dialogueText.text = dialogueLines[currentLine];
        }
        else
        {
            dialoguePanel.SetActive(false);
            cameraController.SwitchToMainCamera(); // Volver a la c�mara del jugador
            AssignQuest(); // Asignar la misi�n
        }
    }

    void AssignQuest()
    {
        if (assignedQuest != null)
        {
            QuestManager.instance.AddQuest(assignedQuest);
            Debug.Log("Misi�n asignada: " + assignedQuest.questName);

            missionAccepted = true; // Bloquear interacci�n hasta que se complete
            MissionTracker.instance.UpdateMissionText("Dir�gete al marcador y derrota al enemigo.");
        }
    }
}

