using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraCanvas : MonoBehaviour
{
    public static CameraCanvas Inst = null;

    private Animator anim = null;

    private void Awake()
    {
        Inst = this;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.enabled = false;
    }

    public void BG_Start()
    {
        anim.enabled = true;
    }

    public void Color_Change()
    {
        //Invoke("Change", 0.1f);
        PlayerController.Inst.ColorChange();
    }

    void Change()
    {
        PlayerController.Inst.ColorChange();
    }
}
