using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEscape : MonoBehaviour
{
    private GameManager gm;

    private void Start()
    {
        gm = GameManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

        }
    }

    private void CheckEscape()
    {
        if (!gm.levelWon)
        {
            return;
        }

        Debug.Log("Level Escaped");
        gm.OnLevelExit();
    }
}
