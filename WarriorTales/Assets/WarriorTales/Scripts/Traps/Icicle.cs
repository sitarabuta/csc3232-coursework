using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icicle : MonoBehaviour
{
    [Range(0, 10)] public int hitDamage = 4;
    [Range(0f, 1f)] public float fallChance = 0.125f;

    [SerializeField] private AudioClip destroySound;

    private Animator animator;
    private Rigidbody2D body;
    new private BoxCollider2D collider;

    void Awake()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        //collider = GetComponent<BoxCollider2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3 || collision.gameObject.layer == 9)
        {
            animator.SetTrigger("despawn");
            SoundsManager.instance.PlaySound(destroySound);
            StartCoroutine(DestroyWithDelay(0.2f));
        }
        else if (collision.gameObject.tag == "Player")
        {
            animator.SetTrigger("despawn");
            SoundsManager.instance.PlaySound(destroySound);
            collision.gameObject.GetComponent<PlayerController>().Hit(hitDamage);
            StartCoroutine(DestroyWithDelay(0.15f));
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // Should only try to drop the icicle once
            //collider.enabled = false;

            if (Random.value < fallChance)
            {
                body.bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }

    IEnumerator DestroyWithDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
