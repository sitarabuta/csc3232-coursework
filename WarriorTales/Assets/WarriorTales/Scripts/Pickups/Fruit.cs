using System.Collections;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [Range(1, 6)] public int healPoints = 1;

    [SerializeField] private AudioClip pickupSound;

    public Animator animator { get; private set; }
    public Rigidbody2D body { get; private set; }
    new public BoxCollider2D collider { get; private set; }

    void Awake()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            body.bodyType = RigidbodyType2D.Static;
            collider.enabled = false;

            SoundsManager.instance.PlaySound(pickupSound);
            collision.gameObject.GetComponent<PlayerController>().Heal(healPoints);

            animator.SetTrigger("collect");
            StartCoroutine(DestroyWithDelay(0.2f));
        }
    }

    IEnumerator DestroyWithDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
