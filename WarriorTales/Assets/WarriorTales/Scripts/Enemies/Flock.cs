using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public GameObject flockUnitPrefab;
    public int numberOfUnits = 8;
    public float spaceBetween = 1f;
    public float spawnRadius = 5f;
    public float reachMargin = 1.5f;
    public float detectionRadius = 10f;
    public float maxChaseDuration = 15f;
    public GameObject home;
    public GameObject player;

    public int leftBound;
    public int rightBound;

    private GameObject[] flockUnits;
    [System.NonSerialized]
    public GameObject currentTarget;
    private bool wasDetected;
    public bool isRetreating = false;
    private bool isFacingRight;
    private float chaseDuration;

    void Awake()
    {
        flockUnits = new GameObject[numberOfUnits];
        wasDetected = false;
        currentTarget = player;
        isFacingRight = true;
    }

    void Start()
    {
        for (int i = 0; i < numberOfUnits; i++)
        {
            flockUnits[i] = Instantiate(flockUnitPrefab, (Vector2)transform.position + Random.insideUnitCircle * spawnRadius, Quaternion.identity);
            flockUnits[i].transform.SetParent(gameObject.transform);
        }
    }

    void Update()
    {
        if (chaseDuration > maxChaseDuration)
            isRetreating = true;

        wasDetected = false;
        if (!isRetreating)
        {
            for (int i = 0; i < flockUnits.Length; i++)
            {
                if (Vector2.Distance(player.transform.position, flockUnits[i].transform.position) < detectionRadius)
                {
                    currentTarget = player;
                    wasDetected = true;
                    break;
                }
            }
        }

        if (!wasDetected)
        {
            currentTarget = home;
        }

        for (int i = 0; i < flockUnits.Length; i++)
        {
            if (Vector2.Distance(currentTarget.transform.position, flockUnits[i].transform.position) > reachMargin)
            {
                Vector2 direction = currentTarget.transform.position - flockUnits[i].transform.position;
                direction = direction + new Vector2(Random.Range(0f, 0.5f) * (Random.value < 0.5f ? 1 : -1), Random.Range(0f, 0.5f) * (Random.value < 0.5f ? 1 : -1));
                flockUnits[i].transform.Translate(Vector2.ClampMagnitude(direction, detectionRadius + 2.5f) * Time.deltaTime);

                if (direction.x > 0.01f)
                    flockUnits[i].GetComponent<SpriteRenderer>().flipX = false;
                else if (direction.x < -0.01f)
                    flockUnits[i].GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                isRetreating = false;
            }
            for (int j = 0; j < flockUnits.Length; j++)
            {
                if (flockUnits[i] != flockUnits[j])
                {
                    float distance = Vector2.Distance(flockUnits[i].transform.position, flockUnits[j].transform.position);
                    if (distance <= spaceBetween)
                    {
                        Vector2 direction = flockUnits[j].transform.position - flockUnits[i].transform.position;
                        flockUnits[j].transform.Translate(direction * Time.deltaTime * (5f - direction.normalized.magnitude * 2.5f));
                    }
                }
            }
        }

        if (wasDetected)
            chaseDuration += Time.deltaTime;
        else
            chaseDuration = 0f;

        if (isFacingRight)
        {
            home.transform.position = new Vector2(home.transform.position.x + Time.deltaTime, home.transform.position.y);
        }
        else
        {
            home.transform.position = new Vector2(home.transform.position.x - Time.deltaTime, home.transform.position.y);
        }

        if (home.transform.position.x > rightBound)
        {
            isFacingRight = false;
        }
        else if (home.transform.position.x < leftBound)
        {
            isFacingRight = true;
        }
    }
}
