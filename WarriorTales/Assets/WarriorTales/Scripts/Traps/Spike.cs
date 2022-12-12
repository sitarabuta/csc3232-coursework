using System.Collections;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [Range(0, 10)] public int hitDamage = 3;

    new private BoxCollider2D collider;

    void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            collider.enabled = false;
            other.GetComponent<PlayerController>().Hit(hitDamage);
            StartCoroutine(EnableCollision());
        }
    }

    IEnumerator EnableCollision()
    {
        yield return new WaitForSeconds(0.5f);
        collider.enabled = true;
    }
}
