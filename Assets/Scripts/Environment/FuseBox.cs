using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseBox : MonoBehaviour
{
    public GameObject lightsHolder;
    public QuestItem questItem;
    private InventorySystem inventory;
    public AudioSource cuttingWireAudio;

    private void Start()
    {
        cuttingWireAudio = GetComponent<AudioSource>();
        
    }
    private void Awake()
    {
        inventory = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventorySystem>();
        
    }

    private void Update()
    {
        if (PlayerCast.instance.CheckForObject(gameObject))
        {
            InteractableText.instance.ShowInteractableText("E", "to cut down the electricity");
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (inventory.Remove(questItem))
                {
                    QuestManager.instance.questNumber++;

                    HandAnimation.instance.UseAnim();
                    
                    lightsHolder.SetActive(false);

                    Debug.Log("Used Cutters");
                    //cuttingWireAudio.time = 8.0f;
                    //cuttingWireAudio.Play();
                    gameObject.tag = "Untagged";
                }
            }
        }
    }
}
