using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelEscape : MonoBehaviour
{
    private GameManager gm;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        gm = GameManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CheckEscape();
        }
    }

    private void CheckEscape()
    {
        if (!gm.levelWon)
        {
            return;
        }

        gm.OnLevelExit();
    }

    public void ShowEscape()
    {
        spriteRenderer.enabled = true;
    }

    public void HideEscape()
    {
        if(spriteRenderer == null)
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }
}
