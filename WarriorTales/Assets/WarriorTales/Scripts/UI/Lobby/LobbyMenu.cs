using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyMenu : MonoBehaviour
{
    [SerializeField] private Canvas fadeCanvas;

    public void NewGame()
    {
        StartCoroutine(fadeCanvas.GetComponent<ScreenFade>().FadeIn(NewGameCallback));
    }

    void NewGameCallback()
    {
        PlayerPrefs.SetInt("level", 1);
        PlayerPrefs.SetInt("score", 0);
        SceneManager.LoadScene(1);
    }

    public void LoadGame()
    {
        StartCoroutine(fadeCanvas.GetComponent<ScreenFade>().FadeIn(LoadGameCallback));
    }

    void LoadGameCallback()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("level", 1));
    }

    public void QuitGame()
    {
        StartCoroutine(fadeCanvas.GetComponent<ScreenFade>().FadeIn(QuitGameCallback));
    }

    void QuitGameCallback()
    {
        Application.Quit();
    }
}
