using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class StressScript : MonoBehaviour
{
    public Image stressUI;
    public Sprite[] stressImageHolder;

    public Consumables[] consumables;

    private GameObject Player, Ghost, GhostSpawner;

    public GameObject flashLight;

    public CameraShake camShake;

    public float DistanceNum;
    public float stressIncreasePerSecond;

    public bool Alive = true;

    private float StressNum;
    private int stressRoundedUp;

    private InventorySystem inventory;
    public static StressScript instance;
    private AudioSource audioSrc;
    private float targetPitch;
    private float targetVolume;
    public float incrementPitch = 1.0f;
    public float decreasePitch = 0.1f;

    private GhostAI ghostAI;
    public GhostAI ghostHalt;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
        inventory = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventorySystem>();
        Player = this.gameObject; //Player script is connected to the player GameObject, easier to automatically set variables up than drag & drop.
        Ghost = GameObject.FindGameObjectWithTag("Ghost");
        ghostAI = Ghost.GetComponent<GhostAI>();
        GhostSpawner = GameObject.FindGameObjectWithTag("SpawnPlatform");
    }

    // Start is called before the first frame update
    void Start()
    {
        StressNum = 0;
        audioSrc = GetComponent<AudioSource>(); 
    }
    
    

    // Update is called once per frame
    void Update()
    {
        stressRoundedUp = Mathf.RoundToInt(StressNum);

        DistanceNum = Vector3.Distance(Player.transform.position, ghostAI.ghostActivate.transform.position); // calculates distance between player and ghost objects

        if(ghostAI.ghostActivate.activeSelf && !PauseSystem.instance.isPaused)
        {
            Debug.Log("Ghost is visible");
            if(!audioSrc.isPlaying && DistanceNum >= 0 && DistanceNum <= 10f) //Checks if audio source isnt playing and the player is in range, then trigger the Play function
                audioSrc.Play();  //Increase breathing sound speed with distance to Ghost, the closer you are the quicker the pitch


            if (DistanceNum <= 10f && DistanceNum > 8f)
            {
                targetPitch = 1.02f;
                stressIncreasePerSecond = 1;
                StressNum += stressIncreasePerSecond * Time.deltaTime; // increase stress by 1 every second
                audioSrc.volume = 1f;
                //Base pitch of audio is at 1.0f
                audioSrc.pitch = Mathf.Lerp(audioSrc.pitch, targetPitch, incrementPitch * Time.deltaTime);
                camShake.FOVShake();
            }
            else if (DistanceNum <= 8f && DistanceNum > 6f)
            {
                stressIncreasePerSecond = 2f;
                StressNum += stressIncreasePerSecond * Time.deltaTime; // increase stress by 2 every second
                targetPitch = 1.04f;
                audioSrc.volume = 1.0f;
                audioSrc.pitch = Mathf.Lerp(audioSrc.pitch, targetPitch, incrementPitch * Time.deltaTime);
                camShake.FOVShake();
            }
            else if (DistanceNum <= 6f && DistanceNum > 4f)
            {
                stressIncreasePerSecond = 5f;
                StressNum += stressIncreasePerSecond * Time.deltaTime; // increase stress by 5 every second
                targetPitch = 1.06f;
                audioSrc.volume = 1.0f;
                //Sets the audio target pitch to 1.08f which it will Lerp to the target value over a given time
                audioSrc.pitch = Mathf.Lerp(audioSrc.pitch, targetPitch, incrementPitch * Time.deltaTime);
                camShake.FOVShake();
            }
            else if (DistanceNum <= 4f && DistanceNum >= -10f)
            {
                stressIncreasePerSecond = 15f;
                StressNum += stressIncreasePerSecond * Time.deltaTime; // increase stress by 10 every second 
                targetPitch = 1.10f;
                audioSrc.volume = 1.0f;
                //Set audio volume back to 1 (start Volume)
                //Sets the audio target pitch to 1.10f which it will Lerp to at max stress level before the game transitions scenes
                audioSrc.pitch = Mathf.Lerp(audioSrc.pitch, targetPitch, incrementPitch * Time.deltaTime);
                camShake.FOVShake();
                Debug.Log("Distance: " + DistanceNum);
            }
            else if (DistanceNum >= 10f && audioSrc.isPlaying)           //Disable breathing audio when not in range, fade it to pitch of 0/volume to 0
            {
                targetPitch = 0.75f;
                //Store a variable for current pitch and decrease it
                audioSrc.pitch = Mathf.Lerp(audioSrc.pitch, targetPitch, decreasePitch * Time.deltaTime);
                targetVolume = 0f;
                audioSrc.volume = Mathf.Lerp(audioSrc.volume, targetVolume, 1f * Time.deltaTime);
            }
        }
        


        if (StressNum <= 0)
            StressNum = 0;
        if (StressNum >= 100)
            StressNum = 100;
        
        if (Alive == false) // character dies
        {
            //play death animation and sound
            // start a coroutine and then pop up screen with death menu
            
        }

        if (StressNum >= 5) //player dies
        {
            Alive = false;
        }
        //Debug.Log(StressNum);

        UseItemToDecreaseStress();
        ChangeStressImage();
    }
   
    private void UseItemToDecreaseStress()
    {
        //Use item when G is pressed.
        if(Input.GetKeyDown(KeyCode.G))
        {
            //Loops through inventory to check if there are any of the consumables in the specified array.
            for(int i = 0; i < consumables.Length; i++)
            {
                //Loops through inventory to check if any consumable is currently in inventory.
                //If all the items are present in inventory, uses an item that puts StressNum closer to 0, and doesn't go into negative numbers.
                //In current items I have made that; Pills: -10 stress | Marihuana: -20 stress | Cocaine: -30 stress.
                //E.g: If StressNum is 26, the inventory will use Cocaine as it will be closer to 0. If stress if 9, nothing will be used as the pills will make Stress -1.
                if (StressNum >= consumables[i].decreaseStress)
                {
                    if(consumables[i].consumableType == Consumables.ConsumableType.SANITY_PILLS && ghostAI.ghostActivate.activeSelf && inventory.Remove(consumables[i]))
                    {
                        audioSrc.Stop();
                        Destroy(GhostSpawner);
                        ghostAI.Ghost.isStopped = true;
                        ghostAI.ghostActivate.SetActive(false);
                        Debug.Log("Used consumable and removed ghost");
                        break;
                    }

                    else if(consumables[i].decreaseStress != 0 && inventory.Remove(consumables[i]))
                    {
                        Debug.Log("Stress Before: " + StressNum);
                        StressNum -= consumables[i].decreaseStress;
                        Debug.Log("Stress After: " + StressNum);
                        //ChangeStressImage();
                        break;
                    }
                }
                Debug.Log("No consumable in inventory to use");
            }
        }
    }

    private void ChangeStressImage()
    {
        switch(stressRoundedUp)
        {
            //Add sound to play after each bar was filled out?
            case int n when n < 10:
                stressUI.sprite = stressImageHolder[0];
                break;
            case int n when n >= 10 && n < 20:
                stressUI.sprite = stressImageHolder[1];
                break;
            case int n when n >= 20 && n < 30:
                stressUI.sprite = stressImageHolder[2];
                break;
            case int n when n >= 30 && n < 40:
                stressUI.sprite = stressImageHolder[3];
                break;
            case int n when n >= 40 && n < 50:
                stressUI.sprite = stressImageHolder[4];
                break;
            case int n when n >= 50 && n < 60:
                stressUI.sprite = stressImageHolder[5];
                //Play faint audio screams as stress increases above this level, trigger hallucinations (Cutscene maybe)
                break;
            case int n when n >= 60 && n < 70:
                stressUI.sprite = stressImageHolder[6];
                break;
            case int n when n >= 70 && n < 80:
                stressUI.sprite = stressImageHolder[7];
                break;
            case int n when n >= 80 && n < 90:
                stressUI.sprite = stressImageHolder[8];
                break;
            case int n when n >= 90 && n < 100:
                stressUI.sprite = stressImageHolder[9];
                //Play screen shake
                camShake.CameraShakeFunc();
                break;
            case 100:
                stressUI.sprite = stressImageHolder[10];
                //Rotate nav mesh agent to face player
                ghostHalt.RotateGhost();
                //Stops the nav mesh agent
                ghostHalt.Ghost.isStopped = true;
                //Play death animation then reset StressNum to 0. It will automatically change stressRoundedUp to 0 as well.
                StartCoroutine(WaitForAnim());
                break;
        }
    }

    IEnumerator WaitForAnim()
    {
        Destroy(flashLight);
        yield return new WaitForSeconds(0.2f);
        //Play animation, wait 1.53 seconds for anim to finish then transition to game over scene
        HandAnimation.instance.DeathAnim();
        yield return new WaitForSeconds(1.53f);
        //Trigger a cutscene before the scene change
        SceneManager.LoadScene("GameOver");

    }

    public void PauseBreathingAudio()
    {
        audioSrc.Pause();
    }

    public void StopBreathingAudio()
    {
        audioSrc.Stop();
    }

    public void PlayBreathingAudio()
    {
        if (StressNum > 0 && ghostAI.ghostActivate.activeSelf)      //Checks for a stress level greater than 0 before playing audio
            audioSrc.Play();
    }
}