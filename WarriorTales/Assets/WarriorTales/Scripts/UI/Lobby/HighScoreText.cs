using UnityEngine;
using TMPro;

public class HighScoreText : MonoBehaviour
{
    void Start()
    {
        int score = PlayerPrefs.GetInt("score", 0);
        int highScore = PlayerPrefs.GetInt("highScore", 0);

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("highScore", highScore);
        }

        GetComponent<TextMeshProUGUI>().text = "High Score: " + highScore.ToString();
    }
}
