using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Walk : Monster
{
    private SpriteRenderer _sprite;
    private int _dir = -1;

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector3.left *_dir * moveSpeed * Time.deltaTime);
        
    }

   private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Ground"))
        {
            FlipObject();
        }
    }

    public void FlipObject()
    {
        _sprite.flipX = true;
        _dir *= -1; 
    }
}
