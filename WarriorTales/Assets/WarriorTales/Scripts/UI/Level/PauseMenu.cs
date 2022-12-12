using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Canvas fadeCanvas;

    private Canvas canvas;
    private bool isPaused;

    void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        isPaused = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        canvas.sortingOrder = 1;
        canvas.enabled = true;
    }

    public void ResumeGame()
    {
        canvas.enabled = false;
        canvas.sortingOrder = -1;
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void ReturnToLobby()
    {
        Time.timeScale = 1f;
        StartCoroutine(fadeCanvas.GetComponent<ScreenFade>().FadeIn(ReturnToLobbyCallback));
    }

    void ReturnToLobbyCallback()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        StartCoroutine(fadeCanvas.GetComponent<ScreenFade>().FadeIn(QuitGameCallback));
    }

    void QuitGameCallback()
    {
        Application.Quit();
    }
}
