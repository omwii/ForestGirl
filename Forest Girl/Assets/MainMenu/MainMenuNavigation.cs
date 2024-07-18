using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuNavigation : MonoBehaviour
{
    [SerializeField] private GameObject _mainObj;
    [SerializeField] private GameObject _settingsObj;
    [SerializeField] private GameObject _infoObj;
    [Space]
    [SerializeField] private GameObject _videoObj;
    [SerializeField] private GameObject _audioObj;
    [SerializeField] private GameObject _controlObj;

    public void PlayGame()
    {
        SceneLoadContr.CurrentSceneToLoad = "FirstCut";
        SceneManager.LoadScene("Loading");
    }

    public void GoToPanel(int target)
    {
        switch (target)
        {
            case 0:
                _mainObj.SetActive(true);
                _settingsObj.SetActive(false);
                _infoObj.SetActive(false);
                break;
            case 1:
                _mainObj.SetActive(false);
                _settingsObj.SetActive(true);
                _infoObj.SetActive(false);
                break;
            case 2:
                _mainObj.SetActive(false);
                _settingsObj.SetActive(false);
                _infoObj.SetActive(true);
                break;
        }
    }

    public void GoToSettings(int target)
    {
        switch (target)
        {
            case 0:
                _videoObj.SetActive(true);
                _audioObj.SetActive(false);
                _controlObj.SetActive(false);
                break;
            case 1:
                _videoObj.SetActive(false);
                _audioObj.SetActive(true);
                _controlObj.SetActive(false);
                break;
            case 2:
                _videoObj.SetActive(false);
                _audioObj.SetActive(false);
                _controlObj.SetActive(true);
                break;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
