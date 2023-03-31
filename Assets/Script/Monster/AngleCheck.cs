using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleCheck : MonoBehaviour
{
    [SerializeField] private GameObject enemyobject;

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            enemyobject.GetComponent<Walk>().FlipObject();
        }
    }
}
