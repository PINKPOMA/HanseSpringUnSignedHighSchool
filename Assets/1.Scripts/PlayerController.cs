using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Inst;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private bool isJump;
    [SerializeField] private bool isWallDash;
    //[SerializeField] private PhysicsMaterial2D Friction0;

    private int direction;

    //---- 색 게이지 관련 변수
    public Image ColorGageBar;
    public Image CurColorKnob;

    private List<Color> m_ColorList = new List<Color>();
    private Color Cur_Color = Color.white;  //현재 유저의 색
    private Color Next_Color = Color.white; //유저의 다음 색

    private float m_ColorChangeTime = 2.0f;
    private float m_ColorCurTime = 0.0f;

    private int m_CurColorNum = 2;
    //---- 색 게이지 관련 변수

    public GameObject StartPoint;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _sprite;
    private Vector2 velocity = Vector2.zero;
    private Vector2 wallDashPos = Vector2.zero;
   [SerializeField] private int m_JumpCount = 0;
   [SerializeField] private ParticleSystem dustParticleSystem;
   [SerializeField] private ParticleSystem landingParticleSystem;
   [SerializeField] private ParticleSystem jumpParticleSystem;
   [SerializeField] private ParticleSystem hitParticleSystem;
   [SerializeField] private GameObject doubleJump;

   private Animator anim = null;

    private float h;

    private void Awake()
    {
        Inst = this;
    }
    void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        _sprite = gameObject.GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();

        transform.position = StartPoint.transform.position;

        m_ColorList.Add(Color.yellow);
        m_ColorList.Add(Color.blue);
        m_ColorList.Add(Color.red);

        ColorChange();
    }

    void Update()
    {
        if (GameManager.Inst.s_GameState != Game_State.Play)
            return;

        //m_ColorCurTime -= Time.deltaTime;

        //if (ColorGageBar != null)
        //    ColorGageBar.fillAmount = m_ColorCurTime / m_ColorChangeTime;

        //if (m_ColorCurTime <= 0.0f)
        //{
        //    ColorChange();
        //}

        Move();
        Jump();
        
    }

    public void ColorChange()
    {
        //m_ColorCurTime = m_ColorChangeTime;

        // //노 파 빨 순으로 순환
        Cur_Color = m_ColorList[m_CurColorNum];
        
        ColorGageBar.color = Cur_Color;

        if (m_CurColorNum == 0)
        {
            gameObject.layer = 6;
            anim.SetInteger("ColorNum", 0);
        }
        else if (m_CurColorNum == 1)
        {
            gameObject.layer = 8;
            anim.SetInteger("ColorNum", 1);
        }
        else if (m_CurColorNum == 2)
        {
            gameObject.layer = 7;
            anim.SetInteger("ColorNum", 2);
        }
            

        
        m_CurColorNum++;

        if (m_ColorList.Count <= m_CurColorNum)
            m_CurColorNum = 0;

        Next_Color = m_ColorList[m_CurColorNum];

        CurColorKnob.color = Next_Color;
    }
    
    private void Jump()
    {
        if (1 < m_JumpCount)
            return;

        if (Input.GetKeyDown(KeyCode.Space) && !isJump) 
        {
            Instantiate(jumpParticleSystem, new Vector3(transform.position.x, transform.position.y - 0.5f),
                Quaternion.identity);
            m_JumpCount++;
            _rigidbody.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            //_rigidbody.sharedMaterial = Friction0;
            isJump = true;
            Invoke("JumpCount_Down", 1.0f);
        }
        else if(Input.GetKeyDown(KeyCode.Space) && isJump && m_JumpCount == 1)
        {
            Instantiate(doubleJump, new Vector3(transform.position.x, transform.position.y - 0.5f),
                Quaternion.identity);
            
            m_JumpCount++;
            //_rigidbody.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 9.0f);
            //Invoke("JumpCount_Down", 1.0f);
        }

        //else if (Input.GetKeyDown(KeyCode.Space) && isWall)
        //{
        //    if (!isWall) return;
        //    Debug.Log(direction);
        //    h = Input.GetAxis("Horizontal");
        //    wallDashPos.x = -dir * (moveSpeed / 2);
        //    wallDashPos.y = 1.0f * jumpSpeed;
        //    wallDashPos = wallDashPos.normalized * dashSpeed;
    
            
        //    StartCoroutine(WallDash());

        //}
    }

    private void JumpCount_Down()
    {
        m_JumpCount = 0;
    }

    private void Move()
    {                                                                                                                                                                                                                   
        if (isWallDash) return;
        h = Input.GetAxis("Horizontal");

        if (0 < h)
        {
            anim.SetBool("IsMove", true);
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            dustParticleSystem.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            //_sprite.flipX = true;
        }
        else if (h < 0f)
        {
            anim.SetBool("IsMove", true);
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            dustParticleSystem.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        else
        {
            anim.SetBool("IsMove", false);
            _rigidbody.velocity = new Vector2(0.0f, _rigidbody.velocity.y);
        }
            

        if (transform.position.y <= -10.0f)
            transform.position = StartPoint.transform.position;

        #region ---- 수정 전
        //if(0.1f < m_FrictionTime)
        //{
        //    if (_rigidbody.sharedMaterial == null)
        //        _rigidbody.sharedMaterial = Friction0;
        //}
        //else if(m_FrictionTime <= 0.0f)
        //{
        //    m_FrictionTime = 0.0f;
        //    if (_rigidbody.sharedMaterial == Friction0)
        //        _rigidbody.sharedMaterial = null;
        //}

        //if (isJump)
        //    velocity = Vector2.right * h * (moveSpeed / 2.0f);
        //else
        //    velocity = Vector2.right * h * moveSpeed;

        //if (velocity.x == _rigidbody.velocity.x && velocity.y == 0.0f)
        //    m_FrictionTime += Time.deltaTime;

        //velocity.y = _rigidbody.velocity.y;

        //velocity.x = Math.Clamp(velocity.x, -moveSpeed, moveSpeed);

        //Debug.Log(velocity);

        //_rigidbody.velocity = velocity;
        #endregion

        _rigidbody.AddForce(Vector2.right * h, ForceMode2D.Impulse);
    }

    private void LateUpdate()
    {
        velocity = _rigidbody.velocity;
        velocity.x = Math.Clamp(velocity.x, -moveSpeed, moveSpeed);

        if (isJump)
            velocity.x = velocity.x * 0.95f;
        else
        {
            if (0 < h)
                dustParticleSystem.Play();
            else if (h < 0)
                dustParticleSystem.Play();
        }

        _rigidbody.velocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
       

        if (col.transform.CompareTag("Enemy"))
        {
            transform.position = StartPoint.transform.position;
        }

        //if (col.transform.CompareTag("Wall"))
        //{
        //    //direction = (int)h;
        //    dir = -col.contacts[0].normal.x;
        //    //m_JumpCount = 3;
        //    //direction = col.contacts[0].point - (Vector2)transform.position;
        //    //direction = direction.normalized;
        //    isWall = true;
        //}
    }

    private void OnCollisionStay2D(Collision2D collisionInfo)
    {
        if (collisionInfo.transform.CompareTag("Wall"))
        {
            var dis = collisionInfo.transform.position.x - transform.position.x;
            if(Math.Abs(dis)  >=15f)
                transform.Translate(new Vector2(dis, 0) * 3 * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            Instantiate(landingParticleSystem, new Vector3(transform.position.x, transform.position.y - 0.5f),
                Quaternion.identity);
            isJump = false;
            ////_rigidbody.sharedMaterial = null;
            
            if(_rigidbody.velocity.y < 0.1f)
                m_JumpCount = 0;

        }
        if (col.CompareTag("Enemy"))
        {
            Instantiate(hitParticleSystem, col.transform.position, quaternion.identity);
            col.GetComponent<Monster>().Dead();
            _rigidbody.AddForce(Vector2.up * 7, ForceMode2D.Impulse);
            //_rigidbody.sharedMaterial = Friction0;
        }
    }
    
}
