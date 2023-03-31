using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title_Mgr : MonoBehaviour
{
    [Header("-------- Button -------")]
    public Button m_StartBtn = null;

    // Start is called before the first frame update
    void Start()
    {
        if (m_StartBtn != null)
            m_StartBtn.onClick.AddListener(()=> FadeMgr.Inst.SceneOut("SampleScene"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
