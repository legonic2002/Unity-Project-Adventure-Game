using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool isThrow;
    [HideInInspector] public Vector3 diretion;
    [SerializeField] private float speedThrow;

    // Update is called once per frame
    void Update()
    {
           if(isThrow)
        {
                transform.position = Vector3.MoveTowards(transform.position , transform.right , speedThrow * Time.deltaTime);
        }
    }
}
