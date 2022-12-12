using System.Collections.Generic;
using UnityEngine;


// Replaced by Clouds Particle System
public class CloudsManager : MonoBehaviour
{
    public GameObject[] clouds;
    public Vector2 startPosition;
    public Vector2 endPosition;
    public float density;
    public float speed;

    private List<GameObject> generatedClouds;

    void Start()
    {
        generatedClouds = new List<GameObject>();

        AttemptSpawn();
    }

    void Update()
    {
        foreach (GameObject cloud in generatedClouds)
        {
            cloud.transform.Translate(speed * Vector3.right * Time.deltaTime);
            if (cloud.transform.position.x > endPosition.x)
            {
                generatedClouds.Remove(cloud);
                Destroy(cloud);
            }
        }
    }

    void SpawnCloud()
    {
        GameObject cloud = Instantiate(clouds[Random.Range(0, clouds.Length)]);
        cloud.transform.position = new Vector3(startPosition.x, Random.Range(startPosition.y, endPosition.y));
        SpriteRenderer sprite = cloud.GetComponentInChildren<SpriteRenderer>();
        sprite.color = new Color(1f, 1f, 1f, Random.Range(0.75f, 1f));

        generatedClouds.Add(cloud);
    }

    void AttemptSpawn()
    {
        SpawnCloud();
        Invoke("AttemptSpawn", 1 / density);
    }
}
