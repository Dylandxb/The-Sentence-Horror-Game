using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    public TMP_Text questText;
    public int questNumber;
    public List<string> questObjective = new List<string>();
    public bool finishedLastQuest;
    public GameObject questPanel;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance);
    }

    private void Update()
    {
        if(questNumber <= questObjective.Count)
            questText.text = questObjective[questNumber];

        if (questNumber + 1 == questObjective.Count)
            finishedLastQuest = true;


        if(Input.GetKeyDown(KeyCode.L))
        {
            questNumber++;
        }
    }
}
