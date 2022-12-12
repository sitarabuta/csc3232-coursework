using UnityEngine;

public class LobbyCamera : MonoBehaviour
{
    public Vector3[] startPositions;

    [SerializeField] private Canvas fadeCanvas;
    [SerializeField][Range(10f, 60f)] private float panDuration = 30f;


    private int positionIndex = 0;

    void Start()
    {
        SetStartPosition();
        InvokeRepeating("SetStartPositionWithFade", panDuration, panDuration);
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x + Time.deltaTime, transform.position.y, transform.position.z);
    }

    void SetStartPositionWithFade()
    {
        StartCoroutine(fadeCanvas.GetComponent<ScreenFade>().FadeIn(SetStartPosition));
    }

    void SetStartPosition()
    {
        transform.position = startPositions[positionIndex];

        positionIndex += 1;
        if (positionIndex == startPositions.Length)
        {
            positionIndex = 0;
        }

        StartCoroutine(fadeCanvas.GetComponent<ScreenFade>().FadeOut());
    }
}
