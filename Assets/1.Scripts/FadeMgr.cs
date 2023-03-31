using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeMgr : MonoBehaviour
{
    public bool IsFadeOut = false;
    public bool IsFadeIn = false;

    //----- Fade ���� ������...
    [HideInInspector] public Image m_FadeImg = null;
    float AniDuring = 1.2f;         //���̵�ƿ� ���� �ð�
    bool m_StartFade = false;       //���̵� ���� ���� ����
    float m_CacTime = 0.0f;
    float m_AddTimer = 0.0f;
    Color m_Color;

    float m_StVal = 1.0f;           //���� ����
    float m_EndVal = 0.0f;          //��ǥ ����

    string m_SceneName = "";        //�̵��� �� �̸� ����� ����
    [HideInInspector] public bool is_FadeIn = false;
    //----- Fade ���� ������...

    public GameObject a_Canvas = null;

    public static FadeMgr Inst = null;

    private void Awake()
    {
        Inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //�Ʒ� ������� ã�� ���� : public���� �Ҵ��ϰ� ���� ���� ����.
        //��Ƽ�갡 ���� ������Ʈ�� �˻����� ������, ��ü�ؼ� Canvas�� ���� ����
        if (a_Canvas != null)
        {
            //true�� ���־�� ��Ƽ�갡 ���� ��ü�鵵 ���� ã�ƿ��� ��.
            Image[] a_ImgList = a_Canvas.transform.GetComponentsInChildren<Image>(true);

            for (int ii = 0; ii < a_ImgList.Length; ii++)
            {
                if (a_ImgList[ii].gameObject.name == "FadePanel")
                {
                    m_FadeImg = a_ImgList[ii];
                    break;
                }
            }
        }//if(a_Canvas != null)

        //----- Fade In ����
        if (m_FadeImg != null && IsFadeIn == true)
        {
            m_StVal = 1.0f;
            m_EndVal = 0.0f;
            m_FadeImg.color = new Color32(0, 0, 0, 255);
            m_FadeImg.gameObject.SetActive(true);
            m_StartFade = true;
        }
        //----- Fade In ����
    }

    // Update is called once per frame
    void Update()
    {
        if (m_FadeImg == null)
            return;

        FadeUpdate();
    }

    // ���̵� ��, ���̵� �ƿ� ��� �ݿ��ϴ� �Լ�.
    void FadeUpdate()
    {
        if (m_StartFade == false)
            return;

        //���� �������� ��ǥ ��������.
        if (m_CacTime < 1.0f)
        {
            m_AddTimer += Time.deltaTime;
            m_CacTime = m_AddTimer / AniDuring;
            m_Color = m_FadeImg.color;
            m_Color.a = Mathf.Lerp(m_StVal, m_EndVal, m_CacTime);
            //Mathf.Lerp(A, B, C); = �����Լ�. A�� ���� B�� ���� C�� ������ �����ϰڴٴ� ��.
            //                       ��) 0.0f, 1.0f, 0.3f, ���. 30%���� �� ���� �����ϰڴٴ� ��.
            //                       ���� B == C ��� B�� ���� ��. A == C�� A�� ���� ��.
            m_FadeImg.color = m_Color;

            if (1.0f <= m_CacTime)
            {
                if (m_StVal == 1.0f && m_EndVal == 0.0f)         //���� �� (���̵� ��)
                {
                    m_Color.a = 0.0f;
                    m_FadeImg.color = m_Color;
                    m_FadeImg.gameObject.SetActive(false);
                    m_StartFade = false;
                    is_FadeIn = true;
                }
                else if (m_StVal == 0.0f && m_EndVal == 1.0f)    //���� �� (���̵� �ƿ�)
                {
                    SceneManager.LoadScene(m_SceneName);
                }
            }//if(1.0f <= m_CacTime)
        }//if(m_CacTime < 1.0f)
    }//void FadeUpdate()

    public void SceneOut(string a_ScName)
    {
        if (m_FadeImg == null)
            return;

        m_SceneName = a_ScName;

        is_FadeIn = false;
        m_CacTime = 0.0f;
        m_AddTimer = 0.0f;
        m_StVal = 0.0f;
        m_EndVal = 1.0f;
        m_FadeImg.color = new Color32(0, 0, 0, 0);
        m_FadeImg.gameObject.SetActive(true);
        m_StartFade = true;
    }
}
