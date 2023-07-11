using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    public float speed = 20;
    public Vector3 offset;

    void Start()
    {
        target = FindObjectOfType<DuyAnhPlayer>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * speed);
    }
}
