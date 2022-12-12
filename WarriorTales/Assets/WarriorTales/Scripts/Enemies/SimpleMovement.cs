using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    public float leftBound;
    public float rightBound;

    private EnemyController enemyController;
    private bool isFacingRight;

    void Awake()
    {
        enemyController = GetComponent<EnemyController>();

        isFacingRight = true;
    }

    void Start()
    {
        enemyController.animator.SetBool("isRunning", true);
    }

    void Update()
    {
        if (isFacingRight && !enemyController.isStunned)
        {
            transform.position = new Vector2(transform.position.x + Time.deltaTime * enemyController.speedMultiplier, transform.position.y);
        }
        else if (!enemyController.isStunned)
        {
            transform.position = new Vector2(transform.position.x - Time.deltaTime * enemyController.speedMultiplier, transform.position.y);
        }

        if (transform.position.x > rightBound && isFacingRight)
        {
            isFacingRight = false;
            enemyController.sprite.flipX = true;
        }
        else if (transform.position.x < leftBound && !isFacingRight)
        {
            isFacingRight = true;
            enemyController.sprite.flipX = false;
        }
    }
}
