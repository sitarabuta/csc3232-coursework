using UnityEngine;
using UnityEngine.UI;

public class HeartsManager : MonoBehaviour
{
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    [SerializeField] private Image[] hearts;
    [SerializeField] private GameObject player;

    private PlayerController playerController;

    void Awake()
    {
        playerController = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < playerController.health / 2)
            {
                hearts[i].sprite = fullHeart;
            }
            else if ((i * 2) + 1 == playerController.health)
            {
                hearts[i].sprite = halfHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }

}