using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissionTracker : MonoBehaviour
{
    public static MissionTracker instance;
    public TextMeshProUGUI missionText;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void UpdateMissionText(string newText)
    {
        if (missionText != null)
            missionText.text = newText;
        else
            Debug.LogError("MissionTracker: No se ha asignado un TextMeshPro para la misión.");
    }
}