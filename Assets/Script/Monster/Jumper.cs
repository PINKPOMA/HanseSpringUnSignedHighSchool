using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : Monster
{
    [SerializeField] private float jumpCool;
    private Animator anim;
    private Rigidbody2D _rigidbody;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        StartCoroutine(Jump());
    }

    private IEnumerator Jump()
    {
        anim.SetBool("IsJump", true);
        _rigidbody.AddForce(Vector2.up * moveSpeed, ForceMode2D.Impulse);
        yield return new WaitForSeconds(jumpCool);
        anim.SetBool("IsJump", false);
        yield return new WaitForSeconds(0.9f);
        StartCoroutine(Jump());
    }
}
