using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightManager : MonoBehaviour
{
    [SerializeField] private GameObject cageRoof;
    [SerializeField] private GameObject cageFloor;
    [SerializeField] private GameObject boxPrefab;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject player;
    [SerializeField] private Vector2 bottomLeftCorner;
    [SerializeField] private Vector2 topRightCorner;
    [SerializeField] private AstarPath astarPath;
    [SerializeField] private float gravityOne = 0.75f;
    [SerializeField] private float gravityTwo = 3f;
    [SerializeField] private float changingPlayerScale = 0.5f;

    private Rigidbody2D enemyBody;
    private Rigidbody2D playerBody;
    private PlayerController playerController;
    private GameObject currentBox;

    void Awake()
    {
        enemyBody = enemy.GetComponent<Rigidbody2D>();
        playerBody = player.GetComponent<Rigidbody2D>();
        playerController = player.GetComponent<PlayerController>();
    }

    void Start()
    {
        InvokeRepeating("ScalePlayer", 10f, 10f);
        InvokeRepeating("SpawnBoxes", 12.5f, 12.5f);
        InvokeRepeating("SwapGravity", 15f, 15f);
    }

    void Update()
    {
        if (enemy == null)
            cageFloor.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            cageRoof.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }

    void ScalePlayer()
    {
        if (Mathf.Approximately(playerController.scale.x, changingPlayerScale))
        {
            playerController.Scale(new Vector2(1f, 1f));
        }
        else
        {
            playerController.Scale(new Vector2(changingPlayerScale, changingPlayerScale));
        }
    }

    void SpawnBoxes()
    {
        if (currentBox != null)
            Destroy(currentBox);

        currentBox = Instantiate(boxPrefab, new Vector2(Random.Range(bottomLeftCorner.x, topRightCorner.x), Random.Range(bottomLeftCorner.y, topRightCorner.y)), Quaternion.identity);
        AstarPath.active.Scan();
    }

    void SwapGravity()
    {
        if (Mathf.Approximately(playerBody.gravityScale, gravityOne))
        {
            playerBody.gravityScale = gravityTwo;
            enemyBody.gravityScale = gravityTwo;
        }
        else
        {
            playerBody.gravityScale = gravityOne;
            enemyBody.gravityScale = gravityOne;
        }
    }
}
