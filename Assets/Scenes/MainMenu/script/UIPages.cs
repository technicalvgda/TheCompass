using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIPages : MonoBehaviour
{
    public GameObject[] pages;
    public Button previous, next;
    int current = 0;

    void OnEnable()
    {
        for (int i = 0; i < pages.Length; i++)
            pages[i].SetActive(false);

        current = 0;
        pages[current].SetActive(true);
    }

    public void Previous()
    {
        if (current <= 0) return;

        pages[current].SetActive(false);
        current--;
        pages[current].SetActive(true);
    }

    public void Next()
    {
        if (current >= pages.Length - 1) return;

        pages[current].SetActive(false);
        current++;
        pages[current].SetActive(true);
    }
}
