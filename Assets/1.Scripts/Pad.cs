using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pad : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int dir = 1;
    [SerializeField] private bool isWidth;
    [SerializeField] private Vector3 value;
    void Start()
    {
        StartCoroutine(isWidth ?WidthPaded() : HeistPaded());
    }

    private void Update()
    {
        transform.Translate(value);
    }

    IEnumerator HeistPaded()
    {
        value = Vector3.up * -dir * speed * Time.deltaTime;
        yield return new WaitForSeconds(3f);
        value = Vector3.down * -dir * speed * Time.deltaTime;
        yield return new WaitForSeconds(3f);
        StartCoroutine(HeistPaded());
    }
    IEnumerator WidthPaded()
    {
        value = Vector3.right * -dir * speed * Time.deltaTime; 
        yield return new WaitForSeconds(3f);
        value = Vector3.left * -dir * speed * Time.deltaTime; 
        yield return new WaitForSeconds(3f);
        StartCoroutine(WidthPaded());
    }
}
