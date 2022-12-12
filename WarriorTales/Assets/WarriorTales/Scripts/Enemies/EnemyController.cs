using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Range(1, 10)] public int maxHealth = 2;
    [Range(1, 10)] public int hitDamage = 2;
    [Range(0f, 15f)] public float speedMultiplier = 7.5f;

    [SerializeField] private GameObject dropPrefab;
    [SerializeField] private float dropChance = 0f;
    [SerializeField] private AudioClip deathSound;

    public int health { get; private set; }
    public bool isStunned { get; private set; }
    public bool didJump { get; private set; }


    public Animator animator { get; private set; }
    new public BoxCollider2D collider { get; private set; }
    public SpriteRenderer sprite { get; private set; }

    void Awake()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();

        health = maxHealth;
        isStunned = false;
        didJump = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !isStunned)
        {
            PlayerController playerController = other.GetComponent<PlayerController>();

            Vector2 direction = other.transform.position - transform.position;
            if (Vector2.Dot(transform.up, direction) < 1f)
            {
                animator.SetTrigger("attack");
                playerController.Hit(hitDamage);
            }
        }
    }

    public void Hit(int damage)
    {
        IEnumerator InternalHit()
        {
            collider.enabled = false;
            isStunned = true;

            health -= damage;

            yield return new WaitForSeconds(0.45f);

            if (health <= 0)
            {
                animator.SetTrigger("despawn");

                SoundsManager.instance.PlaySound(deathSound);
                GetComponentInChildren<BoxCollider2D>().enabled = false;

                yield return new WaitForSeconds(0.5f);

                if (Random.value < dropChance)
                    Instantiate(dropPrefab, transform.position, Quaternion.identity);

                Destroy(gameObject);
            }
            else
            {
                isStunned = false;
                yield return new WaitForSeconds(0.5f);
                collider.enabled = true;
            }
        }

        StartCoroutine(InternalHit());
        animator.SetTrigger("gotHit");
    }
}
