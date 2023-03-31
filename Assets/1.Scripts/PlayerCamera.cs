using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Vector2 maxPos;
    [SerializeField] private Vector2 minPos;
    [SerializeField] private Vector2 changePos;
    [SerializeField] private GameObject cam;

    private void Update()
    {
        if (transform.position.x > maxPos.x)
        {
            maxPos.x += changePos.x;
            minPos.x += changePos.x;
            cam.transform.position += new Vector3(changePos.x, 0);
        }
        else if (transform.position.x < minPos.x)
        {
            cam.transform.position -= new Vector3(changePos.x, 0);
            maxPos.x -= changePos.x;
            minPos.x -=changePos.x;
        }  
        else if (transform.position.y > maxPos.y)
        {
            cam.transform.position += new Vector3(0, changePos.y);
            maxPos.y += changePos.y;
            minPos.y += changePos.y;
        }
        else if (transform.position.y < minPos.y)
        {
            cam.transform.position -= new Vector3(0, changePos.y);
            maxPos.y -= changePos.y;
            minPos.y -=changePos.y;
        }  
    }
}
