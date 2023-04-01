using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Walk : Monster
{
    private Animator anim = null;
    private SpriteRenderer _sprite;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckRadius = 0.1f;
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

        transform.Translate(Vector3.left  * moveSpeed * Time.deltaTime);

        if (!Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius))
        {
            transform.Rotate(0f, 180f, 0f);
        }

    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Ground"))
        {
            transform.Rotate(0f, 180f, 0f);
        }
    }
}
