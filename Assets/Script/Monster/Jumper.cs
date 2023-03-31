using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : Monster
{
    [SerializeField] private float jumpCool;
    private Rigidbody2D _rigidbody;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(Jump());
    }

    private IEnumerator Jump()
    {
        _rigidbody.AddForce(Vector2.up * moveSpeed, ForceMode2D.Impulse);
        yield return new WaitForSeconds(jumpCool);
        StartCoroutine(Jump());
    }
}
