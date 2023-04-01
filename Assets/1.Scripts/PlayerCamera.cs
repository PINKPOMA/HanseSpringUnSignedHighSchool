using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerCamera : MonoBehaviour
{
    //[SerializeField] private Vector2 maxPos;
    //[SerializeField] private Vector2 minPos;
    [SerializeField] private Vector3 changePos;
    [SerializeField] private GameObject cam;

    private void Update()
    {
        //if (transform.position.x > maxPos.x)
        //{
        //    maxPos.x += changePos.x;
        //    minPos.x += changePos.x;
        //    cam.transform.position += new Vector3(changePos.x, 0);
        //}
        //else if (transform.position.x < minPos.x)
        //{
        //    cam.transform.position -= new Vector3(changePos.x, 0);
        //    maxPos.x -= changePos.x;
        //    minPos.x -=changePos.x;
        //}  
        //else if (transform.position.y > maxPos.y)
        //{
        //    cam.transform.position += new Vector3(0, changePos.y);
        //    maxPos.y += changePos.y;
        //    minPos.y += changePos.y;
        //}
        //else if (transform.position.y < minPos.y)
        //{
        //    cam.transform.position -= new Vector3(0, changePos.y);
        //    maxPos.y -= changePos.y;
        //    minPos.y -=changePos.y;
        //}  
    }

    private void LateUpdate()
    {
        if(transform.position.x < cam.transform.position.x + -6.0f ||
           cam.transform.position.x + 6.0f < transform.position.x)
        {
            cam.transform.DOLocalMoveX(transform.position.x, 1.0f);
        }

        //changePos.x = transform.position.x;
        //changePos.y = cam.transform.position.y;
        //changePos.z = cam.transform.position.z;
        //cam.transform.position = Vector3.Lerp(cam.transform.position, changePos, Time.deltaTime);
    }
}
