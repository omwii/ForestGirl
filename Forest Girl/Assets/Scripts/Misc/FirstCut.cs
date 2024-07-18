using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstCut : MonoBehaviour
{
    private void Awake()
    {
        Invoke("Invoeke", 5);
    }

    private void Invoeke()
    {
        SceneLoadContr.CurrentSceneToLoad = "GrayBox";
        SceneManager.LoadScene("Loading");
    }
}
