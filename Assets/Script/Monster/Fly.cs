using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Fly : Monster
{
    private Vector2 movePos;
    private void Start()
    {
        // movePos = new Vector2(Random.Range(1, 3) == 1 ? -1 : 1, Random.Range(1, 3) == 1 ? -1 : 1);
        movePos = Vector2.up;
    }

    private void Update()
    {
        transform.Translate(movePos * Time.deltaTime * moveSpeed);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("EndWall"))
        {
            movePos = transform.position - GameObject.FindWithTag("Player").transform.position;
            movePos.Normalize();
            movePos *= -1;
        }
        else
        {
            var direction = col.contacts[0].point - (Vector2)transform.position;
            direction = direction.normalized;
            movePos = direction * -1;
        }
    }
}
