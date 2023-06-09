using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGameOver : MonoBehaviour
{
    private void Update()
    {
        if(QuestManager.instance.finishedLastQuest)
            StartCoroutine(GameOver());
    }

    private IEnumerator GameOver()
    {
        //Stops the quest system.
        QuestManager.instance.finishedLastQuest = false;
        QuestManager.instance.questNumber += 10;
        QuestManager.instance.questPanel.SetActive(false);
        //questPanel.SetActive(false);
        Debug.Log("Quest System - Stopped");
        //Start gameover routine. Add animation? Add some narrative? Add story text?
        yield return new WaitForSeconds(0);
    }
}
