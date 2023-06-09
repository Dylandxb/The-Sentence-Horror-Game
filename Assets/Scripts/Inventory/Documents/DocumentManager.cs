using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DocumentManager : MonoBehaviour
{
    public static DocumentManager instance;
    public Button[] buttons;
    //Put all documents in the Array, make sure they are in order from 0 to Maximum.
    public Document[] documents;
    public Cutscene cutsceneScript;
    public PauseSystem pauseSystemScript;
    public GameObject panel;
    public TMP_Text docTitle;
    public TMP_Text docText;
    public GameObject lights;
    public GameObject UI;
    private void Awake()
    {
        //Setting up singleton
        if (instance == null)
            instance = this;
        else
            Destroy(instance);

        buttons = GameObject.Find("ButtonHolder").transform.GetComponentsInChildren<Button>();
    }
    private void Update()
    {
        ShowDocuments();
    }

    //Activates the Document panel, showcasing the title and text from scriptable object.
    public void ReadDocument(int _docID)
    {
        panel.SetActive(true);
        docTitle.text = documents[_docID].docTitle;
        docText.text = documents[_docID].docText;
    }

    public void GameOver()
    {
        StartCoroutine(GameOverEnumerator());
    }

    private IEnumerator GameOverEnumerator()
    {
        //Changes quest.
        QuestManager.instance.questNumber++;

        //Sets document to be read only once.
        buttons[3].interactable = false;
        yield return new WaitForSeconds(0.01f);

        //Add cutscene to play before game over screen. Access cutscene script to play the coroutine
        StartCoroutine(cutsceneScript.CamSequence());
        lights.SetActive(true);
        UI.SetActive(false);
        yield return new WaitForSeconds(22);

        //Change to game over scene.
        SceneManager.LoadScene("GameOver");
    }

    private void ShowDocuments()
    {
        //Activates UI panel to be able to show and click document list.
        if (PauseSystem.instance.documentsAreOpen)
        {
            foreach (var button in buttons)
            {
                button.GetComponentInChildren<Image>().enabled = true;
                button.GetComponentInChildren<Button>().enabled = true;
            }
        }
        //Decativates the document panel and goes back to the pause menu.
        else if (!PauseSystem.instance.documentsAreOpen)
        {
            panel.SetActive(false);
            foreach (var button in buttons)
            {
                button.GetComponentInChildren<Image>().enabled = false;
                button.GetComponentInChildren<Button>().enabled = false;
            }
        }
    }
}
