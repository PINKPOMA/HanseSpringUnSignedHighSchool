using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private bool isJump;
    
    
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _sprite;

    private float h;

    void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        _sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Move();
        Jump();
        Dummy();
    }

    private void Dummy()
    {
        if (Input.GetKeyDown(KeyCode.F1)) //yellow
        {
            gameObject.layer = 6;
            _sprite.color = Color.yellow;
        }

        if (Input.GetKeyDown(KeyCode.F2)) //Green
        {
            gameObject.layer = 7;
            _sprite.color = Color.green;
        }

        if (Input.GetKeyDown(KeyCode.F3)) //Red
        {
            gameObject.layer = 8;
            _sprite.color = Color.red;
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJump)
        {
            isJump = true;
            _rigidbody.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        }
    }
    private void Move()
    {
        h = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * h * moveSpeed *Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
            isJump = false;
    }
}
