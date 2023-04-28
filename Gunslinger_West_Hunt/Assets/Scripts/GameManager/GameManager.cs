using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private Vector2 playerPosition;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdatePlayerPosition(Vector2 newPosition)
    {
        playerPosition = newPosition;
    }

    public Vector2 GetPlayerPosition()
    {
        return playerPosition;
    }
}
