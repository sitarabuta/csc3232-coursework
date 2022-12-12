using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject player;

    private Animator animator;
    private PlayerController playerController;

    void Awake()
    {
        animator = scoreText.GetComponent<Animator>();
        playerController = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        string textToSet = "Score: " + playerController.score.ToString();
        if (scoreText.text != textToSet)
        {
            animator.SetTrigger("animate");
            scoreText.text = textToSet;
        }
    }
}
