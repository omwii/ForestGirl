using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _control;

    private bool _isOpened;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_isOpened)
            {
                OpenPause();
            }
            else
            {
                ClosePause();
                CloseControls();
            }
        }
    }

    public void OpenPause()
    {
        _isOpened = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0.0f;
        _pauseMenu.SetActive(true);
    }

    public void ClosePause()
    {
        _isOpened = false;
        Time.timeScale = 1f;
        _pauseMenu.SetActive(false);
    }

    public void OpenControls()
    {
        _control.SetActive(true);
    }

    public void CloseControls() 
    {
        _control.SetActive(false);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GrayBox");
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
