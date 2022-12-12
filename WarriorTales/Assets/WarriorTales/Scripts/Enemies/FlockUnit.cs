using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockUnit : MonoBehaviour
{
    [SerializeField]
    private int hitDamage;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            playerController.Hit(hitDamage);

            GetComponentInParent<Flock>().isRetreating = true;
        }
    }
}
