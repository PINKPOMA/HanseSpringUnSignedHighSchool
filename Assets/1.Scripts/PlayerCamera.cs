using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Vector2 maxPos;
    [SerializeField] private Vector2 minPos;
    [SerializeField] private GameObject cam;

    private void Update()
    {
        if (transform.position.x > maxPos.x)
        {
            maxPos += new Vector2(17.8f, 0);
            minPos += new Vector2(17.8f, 0);
            cam.transform.position += new Vector3(17.8f, 0);
        }
        else if (transform.position.x < minPos.x)
        {
            cam.transform.position -= new Vector3(17.8f, 0);
            maxPos -= new Vector2(17.8f, 0);
            minPos -= new Vector2(17.8f, 0);
        }  
        else if (transform.position.y > maxPos.y)
        {
            cam.transform.position += new Vector3(0, 10f);
            maxPos += new Vector2(0, 10f);
            minPos += new Vector2(0, 10f);
        }
        else if (transform.position.y < minPos.y)
        {
            cam.transform.position -= new Vector3(0, 10f);
            maxPos -= new Vector2(0, 10f);
            minPos -= new Vector2(0, 10f);
        }  
    }
}
