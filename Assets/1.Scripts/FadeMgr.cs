using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeMgr : MonoBehaviour
{
    public bool IsFadeOut = false;
    public bool IsFadeIn = false;

    //----- Fade 관련 변수들...
    [HideInInspector] public Image m_FadeImg = null;
    float AniDuring = 1.2f;         //페이드아웃 연출 시간
    bool m_StartFade = false;       //페이드 연출 시작 여부
    float m_CacTime = 0.0f;
    float m_AddTimer = 0.0f;
    Color m_Color;

    float m_StVal = 1.0f;           //시작 투명도
    float m_EndVal = 0.0f;          //목표 투명도

    string m_SceneName = "";        //이동할 씬 이름 저장용 변수
    [HideInInspector] public bool is_FadeIn = false;
    //----- Fade 관련 변수들...

    public GameObject a_Canvas = null;

    public static FadeMgr Inst = null;

    private void Awake()
    {
        Inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //아래 방식으로 찾는 이유 : public으로 할당하고 싶지 않을 때라.
        //액티브가 꺼진 오브젝트는 검색되지 않으니, 대체해서 Canvas에 먼저 접근
        if (a_Canvas != null)
        {
            //true를 해주어야 액티브가 꺼진 객체들도 같이 찾아오게 됨.
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

        //----- Fade In 설정
        if (m_FadeImg != null && IsFadeIn == true)
        {
            m_StVal = 1.0f;
            m_EndVal = 0.0f;
            m_FadeImg.color = new Color32(0, 0, 0, 255);
            m_FadeImg.gameObject.SetActive(true);
            m_StartFade = true;
        }
        //----- Fade In 설정
    }

    // Update is called once per frame
    void Update()
    {
        if (m_FadeImg == null)
            return;

        FadeUpdate();
    }

    // 페이드 인, 페이드 아웃 모두 반영하는 함수.
    void FadeUpdate()
    {
        if (m_StartFade == false)
            return;

        //시작 투명도에서 목표 투명도까지.
        if (m_CacTime < 1.0f)
        {
            m_AddTimer += Time.deltaTime;
            m_CacTime = m_AddTimer / AniDuring;
            m_Color = m_FadeImg.color;
            m_Color.a = Mathf.Lerp(m_StVal, m_EndVal, m_CacTime);
            //Mathf.Lerp(A, B, C); = 보강함수. A값 부터 B값 사이 C값 값으로 리턴하겠다는 말.
            //                       예) 0.0f, 1.0f, 0.3f, 라면. 30%정도 쯤 수를 리턴하겠다는 말.
            //                       만일 B == C 라면 B를 리턴 함. A == C도 A를 리턴 함.
            m_FadeImg.color = m_Color;

            if (1.0f <= m_CacTime)
            {
                if (m_StVal == 1.0f && m_EndVal == 0.0f)         //들어올 때 (페이드 인)
                {
                    m_Color.a = 0.0f;
                    m_FadeImg.color = m_Color;
                    m_FadeImg.gameObject.SetActive(false);
                    m_StartFade = false;
                    is_FadeIn = true;
                }
                else if (m_StVal == 0.0f && m_EndVal == 1.0f)    //나갈 때 (페이드 아웃)
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
