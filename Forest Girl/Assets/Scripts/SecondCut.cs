using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecondCut : MonoBehaviour
{
    private void Awake()
    {
        Invoke("Invoeke", 5);
    }

    private void Invoeke()
    {
        SceneLoadContr.CurrentSceneToLoad = "MainMenu";
        SceneManager.LoadScene("Loading");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
