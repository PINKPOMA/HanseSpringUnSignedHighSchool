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

    float height = 0.0f;
    float width = 0.0f;

    [SerializeField] private Vector2 center;
    [SerializeField] private Vector2 mapSize;

    private void Start()
    {
        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
    }
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
        if (GameManager.Inst.s_GameState != Game_State.Play)
            return;

        float lx = mapSize.x - width;
        float clampX = Mathf.Clamp(transform.position.x, center.x - lx, center.x + lx);

        float ly = mapSize.y - height;
        float clampY = Mathf.Clamp(transform.position.y, 0.0f, center.y + ly); ;

        if (transform.position.x < cam.transform.position.x - 6.0f ||
            cam.transform.position.x + 6.0f < transform.position.x)
        {
            cam.transform.DOLocalMoveX(clampX, 1.0f);
        }

        if (transform.position.y < cam.transform.position.y - 2.0f ||
            cam.transform.position.y + 2.0f < transform.position.y)
        {
            cam.transform.DOLocalMoveY(clampY, 2.0f);
        }

    }
    // void OnDrawGizmos() //������ �׻� ����
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawCube(center, mapSize); //���簢�� �׸���
    // }
}
