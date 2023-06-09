using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractableText : MonoBehaviour
{
    public static InteractableText instance;
    public TMP_Text interactableText;

    private void Awake()
    {
        //Singleton
        if (instance == null)
            instance = this;
        else
            Destroy(instance);
    }

    public void ShowInteractableText(string _button, string _command)
    {
        //Changes the text to correct button and command.
        interactableText.text = "Press [ " + _button + " ] to " + _command + ".";
    }

    public void HideInteractableText()
    {
        //Removes the text.
        interactableText.text = "";
    }
}
