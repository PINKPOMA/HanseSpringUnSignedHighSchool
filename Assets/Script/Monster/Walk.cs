using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Walk : Monster
{
    private Animator anim = null;
    private SpriteRenderer _sprite;
    private bool isFlip;
    private int _dir = -1;

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (0 < _dir)
        {
            anim.SetBool("IsMove", true);
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
        else if(_dir < 0)
        {
            anim.SetBool("IsMove", true);
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        else
            anim.SetBool("IsMove", false);

        transform.Translate(Vector3.left *_dir * moveSpeed * Time.deltaTime);
        
    }

   private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.CompareTag("Ground") && !isFlip)
        {
            StartCoroutine(Delay());
        }
    }

   private IEnumerator Delay()
   {
       isFlip = true;
       FlipObject();
       yield return new WaitForSeconds(0.25f);
       isFlip = false;
   }
   private void OnTriggerExit2D(Collider2D other)
   {
       if (other.CompareTag("Ground")&& !isFlip)  
       {
           StartCoroutine(Delay());
       }
   }
    public void FlipObject()
    {
        _sprite.flipX = true;
        _dir *= -1; 
    }
}
