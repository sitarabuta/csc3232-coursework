using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    [SerializeField] private int nextLevelIndex;
    [SerializeField] private GameObject player;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (nextLevelIndex != 0)
        {
            PlayerPrefs.SetInt("level", nextLevelIndex);
        }

        PlayerPrefs.SetInt("score", player.GetComponent<PlayerController>().score);

        SceneManager.LoadScene(nextLevelIndex);
    }
}
