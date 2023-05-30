using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool IsPlayerAlive { get => isPlayerAlive; set => isPlayerAlive = value; }
    public GameObject Player { get => player;}

    public int playerHealth;
    public int playerArmor;
    public HealthBar playerHealthBar;

    private Vector2 playerPosition;
    private bool isPlayerAlive;
    [SerializeField] private GameObject player;

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

    public void KillPlayer()
    {
        Destroy(player);
    }
}
