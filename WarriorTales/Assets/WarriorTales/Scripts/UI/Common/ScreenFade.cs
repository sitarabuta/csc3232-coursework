using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFade : MonoBehaviour
{
    public bool fadeOutOnStart = false;

    private Canvas canvas;
    private Image image;

    void Awake()
    {
        canvas = GetComponent<Canvas>();
        image = GetComponentInChildren<Image>();

        if (fadeOutOnStart)
        {
            canvas.sortingOrder = 9;
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
        }
    }

    void Start()
    {
        if (fadeOutOnStart)
        {
            StartCoroutine(FadeOut());
        }
    }

    public IEnumerator FadeIn(Action action = null)
    {
        canvas.sortingOrder = 9;
        while (image.color.a <= 1f)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + Time.deltaTime * 2);
            yield return null;
        }

        image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);

        if (action != null)
            action();
    }

    public IEnumerator FadeOut(Action action = null)
    {
        while (image.color.a >= 0f)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - Time.deltaTime * 2);
            yield return null;
        }

        image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);

        if (action != null)
            action();

        canvas.sortingOrder = -9;
    }
}
