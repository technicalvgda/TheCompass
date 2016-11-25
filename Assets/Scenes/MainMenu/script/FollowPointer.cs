using UnityEngine;
using System.Collections;

[RequireComponent(typeof(RectTransform))]
public class FollowPointer : MonoBehaviour
{
    public Camera mainCamera;
    RectTransform t;
    float x;

    void Start()
    {
        t = GetComponent<RectTransform>();
    }

    void OnEnable()
    {
        x = mainCamera.ScreenToWorldPoint(Input.mousePosition).x * 100;
    }

    void Update()
    {
        Vector3 pos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        t.localPosition = new Vector2(x, (pos.y - 0.15f) * 100);
    }
}
