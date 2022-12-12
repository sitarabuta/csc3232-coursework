using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    [Range(0, 500)] public int scorePoints = 250;
    public bool wasReached = false;

    [SerializeField] private AudioClip reachSound;

    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !wasReached)
        {
            wasReached = true;
            animator.SetBool("wasReached", true);

            SoundsManager.instance.PlaySound(reachSound);
            other.GetComponent<PlayerController>().IncreaseScore(scorePoints);
        }
    }
}
