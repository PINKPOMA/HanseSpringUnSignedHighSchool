using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float dashSpeed;
    [SerializeField] private bool isJump;
    [SerializeField] private bool isWall;
    [SerializeField] private bool isWallDash;
    [SerializeField] private bool isPlayerOver;
    [SerializeField] private PhysicsMaterial2D Friction0;

    private int direction;

    //---- 색 게이지 관련 변수
    public Image ColorGageBar;
    public Image CurColorKnob;

    private List<Color> m_ColorList = new List<Color>();
    private Color Cur_Color = Color.white;  //현재 유저의 색
    private Color Next_Color = Color.white; //유저의 다음 색

    private float m_ColorChangeTime = 3.0f;
    private float m_ColorCurTime = 0.0f;

    private int m_CurColorNum = 2;
    //---- 색 게이지 관련 변수

    public GameObject StartPoint;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _sprite;
    private Vector2 velocity = Vector2.zero;
    private Vector2 wallDashPos = Vector2.zero;
    private float m_FrictionTime = 0.0f;    //벽면에 얼마나 붙어 있었는지 시간값을 저장할 변수
   [SerializeField] private int m_JumpCount = 0;

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
         //   ColorChange();
        }

        Move();
        Jump();
        //Dummy();
        
    }

    private void ColorChange()
    {
        m_ColorCurTime = m_ColorChangeTime;

        //빨 -> 노 -> 초 순으로 순환
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
        if (Input.GetKeyDown(KeyCode.Space) && !isJump && !isWall) 
        {
            Debug.Log("점프");
            m_JumpCount++;
            _rigidbody.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            _rigidbody.sharedMaterial = Friction0;

            

            if(2 <= m_JumpCount)
                isJump = true;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && isWall)
        {
            Debug.Log("대쉬");
            if (!isWall) return;
            Debug.Log(direction);
            h = Input.GetAxis("Horizontal");
            wallDashPos.x = -dir * (moveSpeed / 2);
            wallDashPos.y = 1.0f * jumpSpeed;
            wallDashPos = wallDashPos.normalized * dashSpeed;
    
            
            StartCoroutine(WallDash());

        }
    }

    private IEnumerator WallDash()
    {
        isWallDash = true;
        //_rigidbody.AddForce(Vector2.right * -h * jumpSpeed, ForceMode2D.Impulse);
       // _rigidbody.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        _rigidbody.velocity = wallDashPos;
        _rigidbody.sharedMaterial = Friction0;
        yield return new WaitForSeconds(0.75f);   
        isWallDash = false;
        isJump = true;
    }
    
    private void Move()
    {                                                                                                                                                                                                                   
        if (isWallDash) return;
        h = Input.GetAxis("Horizontal");

        //바닥에 매몰되어 있을 경우(오브젝트 색 변경시 껴서 땅 아래 위치에 놓여질 경우)
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

        if (col.transform.CompareTag("Wall"))
        {
            //direction = (int)h;
            dir = -col.contacts[0].normal.x;
            //m_JumpCount = 3;
            //direction = col.contacts[0].point - (Vector2)transform.position;
            //direction = direction.normalized;
            isWall = true;
        }
    }

    public float dir;
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.CompareTag("Wall"))
        {
            isWall = false;
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
