using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClearUI : MonoBehaviour
{
   [SerializeField] private Text clearTimeText;
    private int m = 0;
    private int s = 0;

    public void SetText()
    {
        s = (int)GlobalValue.g_PlayTime;
        while(59 < s)
        {
            s -= 60;
            m++;
        }
        clearTimeText.text = m.ToString("00") + " : " + s.ToString("00");
    }

    private void Update()
    {
        if (GameManager.Inst.s_GameState != Game_State.GameOver)
            return;

        if(Input.anyKeyDown)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
