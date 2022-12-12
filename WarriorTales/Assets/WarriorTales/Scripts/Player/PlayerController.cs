using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Range(1, 10)] public int maxHealth = 6;

    [SerializeField] private float speedMultiplier = 10f;
    [SerializeField] private float jumpingForce = 15f;
    [SerializeField] private LayerMask platformMask;
    [SerializeField] private LayerMask iceMask;
    [SerializeField] private PhysicsMaterial2D normalMaterial;
    [SerializeField] private PhysicsMaterial2D iceMaterial;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hitSound;

    public int health { get; private set; }
    public int score { get; private set; }
    public bool isStunned { get; private set; }
    public bool isVulnerable { get; private set; }
    public bool didJump { get; private set; }
    public bool didDoubleJump { get; private set; }
    public Animator animator { get; private set; }
    public Rigidbody2D body { get; private set; }
    new public BoxCollider2D collider { get; private set; }
    public SpriteRenderer sprite { get; private set; }
    public Vector2 scale { get; private set; }

    void Awake()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        scale = new Vector2(1f, 1f);

        // Reset scores in editor when moving back through levels without using the "New Game" button
        if (SceneManager.GetActiveScene().buildIndex < PlayerPrefs.GetInt("level", 1))
            PlayerPrefs.SetInt("score", 0);

        health = maxHealth;
        score = PlayerPrefs.GetInt("score", 0);
        isStunned = false;
        isVulnerable = true;
        didJump = false;
        didDoubleJump = false;
    }

    void Update()
    {
        // Re-enable jumping if player has almost no vertical velocity and is on ground
        if (Mathf.Abs(body.velocity.y) < 0.001f && Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, 0.1f, platformMask))
        {
            didJump = false;
            didDoubleJump = false;
        }

        // Handle jumping
        if (Input.GetButtonDown("Jump") && !didDoubleJump && !isStunned)
        {
            // Don't reset vertical velocity if we would set it to a value lower than the current one
            if (body.velocity.y <= jumpingForce)
            {
                body.velocity = new Vector2(body.velocity.x, jumpingForce);
            }

            // Update animator and varbiables for next frame based on jump type
            if (!didJump)
            {
                didJump = true;
                animator.SetTrigger("didJump");
            }
            else
            {
                didDoubleJump = true;
                animator.SetTrigger("didDoubleJump");
            }
        }

        // Update facing direction
        if (body.velocity.x > 0.001f)
            sprite.flipX = false;
        else if (body.velocity.x < -0.001f)
            sprite.flipX = true;

        // Interpolate to scale smoothly
        if ((Vector2)transform.localScale != scale)
            transform.localScale = Vector2.Lerp(transform.localScale, scale, 40f * Time.deltaTime);

        // Update animator parameters
        animator.SetBool("isRunning", Mathf.Abs(body.velocity.x) > 0.001f);
        animator.SetBool("isFalling", body.velocity.y < -0.001f);
    }

    void FixedUpdate()
    {
        if (!isStunned)
        {
            float input = Input.GetAxisRaw("Horizontal");
            // Set velocity to 0 only when player is not on ice
            if (!(Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, 0.1f, iceMask) && input == 0f))
            {
                collider.sharedMaterial = normalMaterial;
                body.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speedMultiplier, body.velocity.y);
            }
            else
            {
                collider.sharedMaterial = iceMaterial;
            }
        }
    }

    public void Heal(int amount)
    {
        health += Mathf.Clamp(amount, 0, maxHealth - health);
    }

    public void Hit(int damage)
    {
        IEnumerator InternalHit()
        {
            isVulnerable = false;
            isStunned = true;
            body.bodyType = RigidbodyType2D.Static;
            collider.enabled = false;

            SoundsManager.instance.PlaySound(hitSound);

            health -= damage;

            yield return new WaitForSeconds(0.45f);

            if (health <= 0)
            {
                animator.SetTrigger("despawn");
                SoundsManager.instance.PlaySound(deathSound);

                yield return new WaitForSeconds(0.5f);

                Spawn();
            }
            else
            {
                collider.enabled = true;
                body.bodyType = RigidbodyType2D.Dynamic;
                isStunned = false;

                yield return new WaitForSeconds(0.55f);

                isVulnerable = true;
            }
        }

        if (isVulnerable)
        {
            StartCoroutine(InternalHit());
            animator.SetTrigger("gotHit");
        }
    }

    public void IncreaseScore(int points)
    {
        score += points;
    }

    public void Launch(float force)
    {
        body.velocity = new Vector2(body.velocity.x, force);

        didJump = true;
        didDoubleJump = false;
        animator.SetTrigger("didJump");
    }

    public void Scale(Vector2 newScale)
    {
        animator.SetTrigger("scale");
        scale = newScale;
    }

    public void Spawn()
    {
        IEnumerator InternalSpawn()
        {
            didJump = false;
            didDoubleJump = false;
            health = maxHealth;

            yield return new WaitForSeconds(0.5f);

            collider.enabled = true;
            body.bodyType = RigidbodyType2D.Dynamic;
            isStunned = false;
            isVulnerable = true;
        }

        StartCoroutine(InternalSpawn());
        animator.SetTrigger("spawn");
    }
}
