using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WidthPad : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int dir = 1;
    [SerializeField] private Vector3 value;
    void Start()
    {
        value = Vector3.right * speed * Time.deltaTime;
    }

    private void Update()
    {
       transform.Translate(value);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Ground"))
        {
            value.x *= -1;
        }
    }
}
