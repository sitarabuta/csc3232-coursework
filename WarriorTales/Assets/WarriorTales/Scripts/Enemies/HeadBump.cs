using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBump : MonoBehaviour
{
    private EnemyController enemyController;

    [Range(5f, 15f)] public float launchForce = 15f;
    [SerializeField] private AudioClip hitSound;

    void Awake()
    {
        enemyController = GetComponentInParent<EnemyController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerController playerController = other.GetComponent<PlayerController>();

            Vector2 direction = other.transform.position - transform.position;
            if (playerController.body.velocity.y < -0.001f && direction.y >= 1f)
            {
                SoundsManager.instance.PlaySound(hitSound);
                enemyController.Hit(1);
                playerController.Launch(launchForce);
                playerController.IncreaseScore(5);
            }
        }
    }
}
