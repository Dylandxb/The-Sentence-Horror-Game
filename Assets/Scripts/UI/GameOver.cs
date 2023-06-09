using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    public void Retry(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
