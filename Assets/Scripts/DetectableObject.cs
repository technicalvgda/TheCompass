using UnityEngine;
using System.Collections;

public class DetectableObject : MonoBehaviour
{
    public float SpeedThreshhold;
    public float DetectionRange = 100;
    public GameObject arrowPrefab;
    Renderer _renderer;
    Rigidbody2D _rb2d;
    GameObject _arrow;
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _rb2d = GetComponent<Rigidbody2D>();
        _arrow = (GameObject)Instantiate(arrowPrefab);
        _arrow.SetActive(false);
    }
    void Update()
    {
        Vector3 playerPos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        Vector3 objectPos = transform.position;
        Vector3 heading = objectPos - playerPos;
        float distance = heading.magnitude;
        if (distance <= DetectionRange && !_renderer.isVisible && _rb2d.velocity.magnitude >= SpeedThreshhold)
        {
            _arrow.SetActive(true);
            UpdateArrow();
        }
        else
        {
            _arrow.SetActive(false);
        }
    }
    public void UpdateArrow()
    {
        Vector3 pos = transform.position;
        pos = Camera.main.WorldToViewportPoint(pos);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        _arrow.transform.position = Camera.main.ViewportToWorldPoint(pos);

        pos = _arrow.transform.position;
        float Angle = Mathf.Atan2(transform.position.y - pos.y, transform.position.x - pos.x) * 180 / Mathf.PI;
        _arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Angle));
    }
}