using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool IsPlayerAlive { get => isPlayerAlive; set => isPlayerAlive = value; }

    private Vector2 playerPosition;
    private bool isPlayerAlive;

    private void Awake()
    {
        Instance = this;
        IsPlayerAlive = true;
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
