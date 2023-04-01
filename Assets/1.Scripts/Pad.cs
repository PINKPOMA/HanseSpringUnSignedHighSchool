using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pad : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int dir = 1;
    [SerializeField] private bool isWidth;
    [SerializeField] private Vector3 value;
    [SerializeField] private Vector3 maxPos;
    void Start()
    {
        value = (isWidth ?  Vector3.left: Vector3.up) * speed * Time.deltaTime;
    }

    private void Update()
    {
       transform.Translate(value);
       if (Math.Abs(transform.position.x) > maxPos.x || Math.Abs(transform.position.y) > maxPos.y)
       {
           value.x *= -1;
           value.y *= -1;
       }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Ground"))
        {
            value.x *= -1;
            value.y *= -1;
        }
    }
}
