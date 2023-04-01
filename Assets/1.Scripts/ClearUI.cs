using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearUI : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI clearTimeText;

   public void SetText()
   {
      clearTimeText.text = $"ClearTime: {(int)GameObject.FindWithTag("GameManager").GetComponent<GameManager>().clearTime}s";
   }

   public void NextStage()
   {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   }
}
