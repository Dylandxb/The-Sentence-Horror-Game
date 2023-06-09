using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Animator anim;
    public AnimationCurve curve;
    public float shakeDuration = 0.5f;
    public GhostAI ghostHalt;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    public void FOVShake()
    {
        anim.SetTrigger("Shake");
    }

    

    public void CameraShakeFunc()
    {
        //anim.SetTrigger("CamShake");
        StartCoroutine(CamShake());
        //Function to trigger an animation of the camera which its x and y positions are altered to trigger camera movement
        //Increment the speed/distance of the camera change at stress of 50% + to 100%
    }

    IEnumerator CamShake()
    {
        //Store position of camera
        Vector3 startPosition = transform.position;
        float elapsedTime = 0.0f;
        while (elapsedTime < shakeDuration)
        {
            //Loops until elapsedTime reaches the same value as shake duration
            elapsedTime += Time.deltaTime;
            //Alters the curves strength, for every moment in time the curve value is changing
            float shakeStrength = curve.Evaluate(elapsedTime / shakeDuration);
            //Transforms the current position of the camera by its start position + a random point with a radius between 0 and 1
            transform.position = startPosition + Random.insideUnitSphere * 0.025f;
           
            yield return null;
        }

        //Resets camera back to original position
        //transform.position = startPosition;
        //? Set the ghost navMesh to false during after the camera shake
        ghostHalt.ghostActivate.SetActive(false);
        yield return new WaitForSeconds(0.5f);

    }

    
}
