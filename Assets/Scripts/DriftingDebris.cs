using UnityEngine;
using System.Collections;

public class DriftingDebris : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Vector2 pos1;
    private Vector2 pos2;
    private Vector2 nextPos;
    [SerializeField]
    private Transform childtransform;
    [SerializeField]
    private Transform transformPos2;
    // Use this for initialization
    void Start()
    {
        pos1 = childtransform.localPosition;
        pos2 = transformPos2.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Drift();
    }
    void Drift()
    {
        childtransform.localPosition = Vector2.MoveTowards(childtransform.localPosition, pos2, speed * Time.deltaTime);

    }
}
