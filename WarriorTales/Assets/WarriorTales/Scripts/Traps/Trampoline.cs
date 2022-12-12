using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [Range(25f, 35f)] public float launchForce = 25f;

    [SerializeField] private AudioClip launchSound;

    private Animator animator;
    new private BoxCollider2D collider;

    void Awake()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            SoundsManager.instance.PlaySound(launchSound);
            animator.SetTrigger("launch");
            other.GetComponent<PlayerController>().Launch(launchForce);
        }
    }
}
