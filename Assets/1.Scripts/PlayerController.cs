using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private bool isJump;
    [SerializeField] private PhysicsMaterial2D Friction0;

    //---- �� ������ ���� ����
    public Image ColorGageBar;
    public Image CurColorKnob;

    private List<Color> m_ColorList = new List<Color>();
    private Color Cur_Color = Color.white;  //���� ������ ��
    private Color Next_Color = Color.white; //������ ���� ��

    private float m_ColorChangeTime = 3.0f;
    private float m_ColorCurTime = 0.0f;

    private int m_CurColorNum = 2;
    //---- �� ������ ���� ����

    public GameObject StartPoint;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _sprite;
    private Vector2 velocity = Vector2.zero;
    private float m_FrictionTime = 0.0f;    //���鿡 �󸶳� �پ� �־����� �ð����� ������ ����
    private int m_JumpCount = 0;

    private float h;

    void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        _sprite = gameObject.GetComponent<SpriteRenderer>();

        transform.position = StartPoint.transform.position;

        m_ColorList.Add(Color.yellow);
        m_ColorList.Add(Color.green);
        m_ColorList.Add(Color.red);

        ColorChange();
    }

    void Update()
    {
        m_ColorCurTime -= Time.deltaTime;

        if (ColorGageBar != null)
            ColorGageBar.fillAmount = m_ColorCurTime / m_ColorChangeTime;

        if (m_ColorCurTime <= 0.0f)
        {
            ColorChange();
        }

        Move();
        Jump();
        //Dummy();
        
    }

    private void ColorChange()
    {
        m_ColorCurTime = m_ColorChangeTime;

        //�� -> �� -> �� ������ ��ȯ
        Cur_Color = m_ColorList[m_CurColorNum];

        ColorGageBar.color = Cur_Color;
        _sprite.color = Cur_Color;

        if (m_CurColorNum == 0)
            gameObject.layer = 6;
        else if (m_CurColorNum == 1)
            gameObject.layer = 7;
        else if (m_CurColorNum == 2)
            gameObject.layer = 8;

        
        m_CurColorNum++;

        if (m_ColorList.Count <= m_CurColorNum)
            m_CurColorNum = 0;

        Next_Color = m_ColorList[m_CurColorNum];

        CurColorKnob.color = Next_Color;
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
            m_JumpCount++;
            _rigidbody.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            _rigidbody.sharedMaterial = Friction0;

            if(2 <= m_JumpCount)
                isJump = true;
        }
    }
    private void Move()
    {
        h = Input.GetAxis("Horizontal");

        //�ٴڿ� �Ÿ��Ǿ� ���� ���(������Ʈ �� ����� ���� �� �Ʒ� ��ġ�� ������ ���)
        if(transform.position.y <= - 4.0f)
        {
            transform.position = new Vector3(transform.position.x, 0.0f, transform.position.z);
        }


        if(0.1f < m_FrictionTime)
        {
            if (_rigidbody.sharedMaterial == null)
                _rigidbody.sharedMaterial = Friction0;
        }
        else if(m_FrictionTime <= 0.0f)
        {
            m_FrictionTime = 0.0f;
            if (_rigidbody.sharedMaterial == Friction0)
                _rigidbody.sharedMaterial = null;
        }

        if (isJump)
            velocity = Vector2.right * h * (moveSpeed / 2.0f);
        else
            velocity = Vector2.right * h * moveSpeed;

        if (velocity.x == _rigidbody.velocity.x && velocity.y == 0.0f)
            m_FrictionTime += Time.deltaTime;

        velocity.y = _rigidbody.velocity.y;
        _rigidbody.velocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            isJump = false;
            _rigidbody.sharedMaterial = null;
            m_FrictionTime = 0.0f;
            m_JumpCount = 0;
        }

        if (col.transform.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            col.GetComponent<Monster>().Dead();
            _rigidbody.AddForce(Vector2.up * 7, ForceMode2D.Impulse);
            _rigidbody.sharedMaterial = Friction0;
        }
    }
}