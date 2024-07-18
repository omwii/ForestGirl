using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Rendering.HDROutputUtils;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] private Text _loadText;
    [SerializeField] private Image _loadingImage;
    [SerializeField] private Sprite[] _loadingSprites;

    private void OnEnable()
    {
        _loadingImage.sprite = _loadingSprites[Random.Range(0, _loadingSprites.Length)];
        StartCoroutine(LoadYourAsyncScene());
    }

    IEnumerator LoadYourAsyncScene()
    {
        var a = SceneManager.GetActiveScene();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneLoadContr.CurrentSceneToLoad);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            _loadText.text = ((int)(asyncLoad.progress * 100)).ToString() + " %";
            yield return new WaitForSeconds(.5f);
            if (asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
                SceneManager.UnloadSceneAsync(a);
            }
            yield return null;
        }
    }
}

public static class SceneLoadContr
{
    public static string CurrentSceneToLoad;
}