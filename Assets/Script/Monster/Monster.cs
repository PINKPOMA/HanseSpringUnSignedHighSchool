using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float moveSpeed;

    public void Dead()
    {
        GameObject.FindWithTag("GameManager").GetComponent<GameManager>().MonsterCountDown();
        Destroy(gameObject);
    }
}
