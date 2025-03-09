using System.Collections;
using System.Collections.Generic;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    public List<Quest> activeQuests = new List<Quest>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void AddQuest(Quest newQuest)
    {
        activeQuests.Add(newQuest);
        Debug.Log("Nueva misión añadida: " + newQuest.questName);
    }
}
