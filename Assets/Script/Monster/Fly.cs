using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : Monster
{
    private Vector2 movePos;
    private void Start()
    {
        StartCoroutine(Flying());
    }

    private void Update()
    {
        transform.Translate(movePos * Time.deltaTime * moveSpeed);
    }

    private IEnumerator Flying()
    {
        movePos = Vector2.down;
        yield return new WaitForSeconds(1f);
        movePos = Vector2.up;
        yield return new WaitForSeconds(1f);
        StartCoroutine(Flying());
    }
}
