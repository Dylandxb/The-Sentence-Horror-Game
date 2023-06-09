using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseSystem : MonoBehaviour
{
    public static PauseSystem instance;
    public GameObject[] pauseMenuButtons;
    public GameObject pausePanel;
    public GameObject backButton;
    public bool isPaused;
    public bool documentsAreOpen;
    public bool canPause;

    private AudioSource musicAudiosSrc;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance);

        musicAudiosSrc = GetComponent<AudioSource>();
    }

    private void Update()
    {
        PauseGame();
        ChangePauseUI();
        backButton.SetActive(documentsAreOpen);
    }

    private void PauseGame()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            //Disable breathing audio in pause, instance is used to call it only once
            StressScript.instance.PauseBreathingAudio();
            //Plays pause menu music audio
            musicAudiosSrc.Play();
            //Bug fix, checks if escape key has been pressed whilst in Pause state, then stop the music
            if (isPaused == false && Input.GetKeyDown(KeyCode.Escape))
            {
                musicAudiosSrc.Stop();
            }
            //Player foosteps.Stop
            //Lights audio stop
        }
        pausePanel.SetActive(isPaused);
        QuestManager.instance.questPanel.SetActive(!isPaused);
        CursorManagerOnPause();
    }

    private void CursorManagerOnPause()
    {
        if (isPaused)
        {
            //Lets cursor be visible and able to move within the screen space.
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            Time.timeScale = 0;
        }
        else
        {
            //Turns off documents if they were open in the first place.
            documentsAreOpen = false;
            //Changes cursor to invisible and locks it in the middle of the screen.
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            //Cursor.SetCursor; Change SetCursor to a different sprite.
            Time.timeScale = 1;
        }
    }

    private void ChangePauseUI()
    {
        if(documentsAreOpen)
        {
            //Sets all of pause buttons to false if documents are open.
            foreach (var pauseObjects in pauseMenuButtons)
            {
                pauseObjects.SetActive(false);
            }
        }
        else
        {
            //Otherwise the pause buttons are shown.
            foreach (var pauseObjects in pauseMenuButtons)
            {
                pauseObjects.SetActive(true);
            }
        }
    }

    public void Continue()
    {
        isPaused = !isPaused;
        //Calls instance of breathing audio to play where it left off
        StressScript.instance.PlayBreathingAudio();
        musicAudiosSrc.Stop();
    }

    public void TurnOnDocuments()
    {
        documentsAreOpen = !documentsAreOpen;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
