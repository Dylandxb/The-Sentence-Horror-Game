using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCast : MonoBehaviour
{
    public static PlayerCast instance;

    public Camera cameraCast;
    public float distanceToTarget;

    private RaycastHit hit;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance);
    }
    private void Update()
    {
        //Define Ray to have value of mouse position.
        Ray ray = cameraCast.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit))
        {
            //Check the distance between player and the raycast.
            distanceToTarget = Vector3.Distance(gameObject.transform.position, hit.transform.position);
        }
    }

    public bool CheckForObject(GameObject _go)
    {
        if(hit.transform != null)
        {
            //Checks for the gameobject's name if it's matching the hit target and if the distance is less or equal to 3.
            if (_go.name == hit.transform.gameObject.name && distanceToTarget <= 3f)
                return true;

            if (hit.transform.tag != "Interactable" || (hit.transform.tag == "Interactable" && distanceToTarget >= 3f))
                InteractableText.instance.HideInteractableText();
        }
        return false;
    }
}
