using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] checkpoints;

    private PlayerController playerController;
    private bool isRespawning = false;

    void Awake()
    {
        playerController = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (!isRespawning && playerController.health <= 0)
        {
            isRespawning = true;
            StartCoroutine(RespawnPlayer());
        }
    }

    IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(0.5f);

        for (int i = checkpoints.Length - 1; i >= 0; i--)
        {
            if (checkpoints[i].GetComponent<CheckpointController>().wasReached)
            {
                player.transform.position = checkpoints[i].transform.GetChild(0).position;
                player.transform.position = new Vector2(player.transform.position.x, player.transform.position.y - 1f);

                isRespawning = false;
                yield break;
            }
        }
    }
}
