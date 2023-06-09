using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CMF;
public class ImageFade : MonoBehaviour
{
    public static ImageFade instance;
    public Animator fadeAnimator;
    public bool isPlaying;
    public AdvancedWalkerController adv;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance);
    }

    public IEnumerator PlayerTeleport()
    {
        isPlaying = true;

        //Remembers the original movement speed.
        float originalMovement = adv.movementSpeed;

        //Sets the player movement speed to 0 which disables the player from moving.
        adv.movementSpeed = 0f;

        //Plays animation to fade in to black.
        fadeAnimator.Play("FadeIn");
        yield return new WaitForSeconds(1.5f);

        //Teleports player to a new location. Vector3 can be changed to any location.
        adv.transform.position = new Vector3(22, 0, -45);

        //Plays animation to fade out to normal screen.
        fadeAnimator.Play("FadeOut");
        yield return new WaitForSeconds(1.5f);

        //Changes the player's movement speed to the original movement speed, allowing player to move again.
        adv.movementSpeed = originalMovement;

        isPlaying = false;
    }
}
