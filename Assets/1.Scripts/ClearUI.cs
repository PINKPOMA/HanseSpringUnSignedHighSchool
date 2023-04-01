using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClearUI : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI clearTimeText;

   public void SetText()
   {
      clearTimeText.text = $"ClearTime: {GameObject.FindWithTag("GameManager").GetComponent<GameManager>().clearTime}";
   }
}
