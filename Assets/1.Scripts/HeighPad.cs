using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HeighPad : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int dir = 1;
    [SerializeField] private Vector3 value;
    void Start()
    {
        value = Vector3.up * speed * Time.deltaTime;
    }

    private void Update()
    {
       transform.Translate(value);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Ground"))
        {
            value.y *= -1;
        }
    }
}
