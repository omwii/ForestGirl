using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AsuncLoading : MonoBehaviour
{
    public GameObject loadingScene;
    public Text progressText;

    private void OnEnable()
    {
        //LoadScene1("gameScene");
    }
    
    public void LoadScene1(string LoadingScene)
    {
        StartCoroutine(LoadAsynchronously(LoadingScene));
    }

    IEnumerator LoadAsynchronously(string gameScene)
    {
        var a = SceneManager.GetActiveScene();
        loadingScene.SetActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(gameScene);
        operation.allowSceneActivation = false;
        

        while (!operation.isDone)
        {
            Debug.Log(operation.progress);
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressText.text = (progress * 100f).ToString("F0") + "%";

            //yield return new WaitForSeconds(.5f);

            if (operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
                SceneManager.UnloadSceneAsync(a);
                
            }

            yield return null;
        }
       
    }
}
