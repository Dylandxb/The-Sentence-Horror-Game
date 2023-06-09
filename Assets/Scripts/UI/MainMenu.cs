using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject creditsPanel;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    public void SelectLevel(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenLink(string _url)
    {
        Application.OpenURL(_url);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            creditsPanel.SetActive(!creditsPanel.activeSelf);
    }
}
