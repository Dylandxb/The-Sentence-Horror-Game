using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocumentPickUp : MonoBehaviour
{
    public Document document;
    public PlayerCast playerCast;
    private void Update()
    {
        //Checks if player is in range of the document and checks for input of E.
        if (PlayerCast.instance.CheckForObject(gameObject))
        {
            InteractableText.instance.ShowInteractableText("E", "pick up a document");

            if (Input.GetKeyDown(KeyCode.E))
                PickUpDocument();
        }
    }

    private void PickUpDocument()
    {
        //Checks for ID to be the same as a button.
        for (int i = 0; i < DocumentManager.instance.buttons.Length; i++)
        {
            //If the document is the right ID, it will change UI button to be activated, destroying the gameobject and using animation to pick it up.
            if (i == document.docID)
            {
                if(document.docID == 3)
                    QuestManager.instance.questNumber++;

                DocumentManager.instance.buttons[i].interactable = true;
                HandAnimation.instance.PickUpAnim();
                Destroy(gameObject);
                return;
            }
        }
    }
}
