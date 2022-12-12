using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BossController : MonoBehaviour
{
    public GameObject platform;

    public Transform target;
    public float speed = 10000f;
    public float nextWaypointDistance = 0f;

    public Transform homeTarget;

    private Path path;
    private int currentWaypoint = 0;

    public Animator animator { get; private set; }
    public Rigidbody2D body { get; private set; }
    new public BoxCollider2D collider { get; private set; }
    public Seeker seeker { get; private set; }
    public SpriteRenderer sprite { get; private set; }

    void Start()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        seeker = GetComponent<Seeker>();
        sprite = GetComponent<SpriteRenderer>();

        InvokeRepeating("UpdatePath", 0f, 0.25f);
    }

    void Update()
    {
        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - body.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        body.AddForce(new Vector2(force.x, 0));

        float distance = Vector2.Distance(body.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        animator.SetBool("isRunning", Mathf.Abs(body.velocity.x) > 0.001);

        if (body.velocity.x > 0.001f)
            sprite.flipX = false;
        else if (body.velocity.x < -0.001f)
            sprite.flipX = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            gameObject.transform.position = homeTarget.position;
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(body.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
}