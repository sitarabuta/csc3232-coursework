using UnityEngine;
using UnityEngine.UI;

public class LoadGameButtonStatus : MonoBehaviour
{
    void Start()
    {
        if (PlayerPrefs.GetInt("level", 1) < 2)
            GetComponent<Button>().interactable = false;
    }
}
