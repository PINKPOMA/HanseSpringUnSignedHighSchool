using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public enum Game_State
{
    GameStart,
    Play,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public Game_State s_GameState = Game_State.GameStart;
    public static GameManager Inst;

    public float clearTime;
    [SerializeField] private GameObject[] monsterArray;
    [SerializeField] private GameObject StageClearUI;
    [SerializeField] private int monsterCount;

    [SerializeField] private TextMeshProUGUI monsterCountText;

    [SerializeField] private GameObject Game_Spring;
    [SerializeField] private GameObject Game_Start;

    private void Awake()
    {
        Inst = this;
    }

    private void Start()
    {
        monsterArray = GameObject.FindGameObjectsWithTag("Enemy");
        monsterCount = monsterArray.Length;
        monsterCountText.text = $"Monster: {monsterCount}";
    }

    private void Update()
    {
        if (s_GameState != Game_State.Play)
            return;

        if (monsterCount <= 0) return;
        clearTime += Time.deltaTime;
    }

    public void MonsterCountDown()
    {
        monsterCount--;
        monsterCountText.text = $"Monster: {monsterCount}";
        if (monsterCount <= 0)
        {
            GlobalValue.g_PlayTime += clearTime;
            s_GameState = Game_State.GameOver;
            StageClearUI.SetActive(true);
            StageClearUI.GetComponent<ClearUI>().SetText();
        }
    }

    public void OpenStartImage()
    {
        Game_Start.SetActive(true);
        s_GameState = Game_State.Play;
        CameraCanvas.Inst.BG_Start();
        Invoke("GameStart", 0.5f);
    }

    void GameStart()
    {
        Game_Start.GetComponent<Image>().DOFade(0.0f, 1.5f);
        Game_Spring.GetComponent<Image>().DOFade(0.0f, 1.5f);
        //Invoke("Play", 1.0f);
    }

    //void Play()
    //{
    //    s_GameState = Game_State.Play;
    //    CameraCanvas.Inst.BG_Start();
    //}
}