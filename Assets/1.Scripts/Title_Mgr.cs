using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Title_Mgr : MonoBehaviour
{
    public Image m_TitleInputImg;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("TitleFade");
    }

    // Update is called once per frame
    void Update()
    {
        if (!FadeMgr.Inst.is_FadeIn)
            return;

        if (Input.anyKeyDown)
            FadeMgr.Inst.SceneOut("SampleScene");
    }

    IEnumerator TitleFade()
    {
        var anim = m_TitleInputImg.DOFade(0.1f, 1f);
        yield return anim.WaitForCompletion();
        var anim2 = m_TitleInputImg.DOFade(1.1f, 1f);
        yield return anim2.WaitForCompletion();
        StartCoroutine("TitleFade");
    }
}
