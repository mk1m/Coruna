using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private Transform playerTransform;
    public float offset;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        //store current camera's position in variable temp
        Vector3 temp = transform.position;
        //set camera's position x to be equal to player's position x
        temp.x = playerTransform.position.x;

        temp.x += offset;
        transform.position = temp;

    }
}
