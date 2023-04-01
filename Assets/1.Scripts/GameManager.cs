using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public float clearTime;
   [SerializeField] private GameObject[] monsterArray;
   [SerializeField] private GameObject StageClearUI;
   [SerializeField] private int monsterCount;
   
   [SerializeField] private TextMeshProUGUI monsterCountText;

   private void Start()
   {
      monsterArray = GameObject.FindGameObjectsWithTag("Enemy");
      monsterCount = monsterArray.Length;
      monsterCountText.text = $"Monster: {monsterCount}";
   }

   private void Update()
   {
      if (monsterCount <= 0) return;
      clearTime += Time.deltaTime;
   }

   public void MonsterCountDown()
   {
      monsterCount--;
      monsterCountText.text = $"Monster: {monsterCount}";
      if (monsterCount <= 0)
      {
         StageClearUI.SetActive(true);
         StageClearUI.GetComponent<ClearUI>().SetText();
      }
   }
   
}
