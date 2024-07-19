using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buttonscenes : MonoBehaviour
{
    public void SwitchScene(string LoadingScene)
    {
        SceneManager.LoadScene(LoadingScene);
    }
}