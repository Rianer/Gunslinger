using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool IsPlayerAlive { get => isPlayerAlive; set => isPlayerAlive = value; }
    public GameObject Player { get => player; }

    public int playerHealth;
    public int playerArmor;
    public HealthBar playerHealthBar;

    private Vector2 playerPosition;
    private bool isPlayerAlive;
    [SerializeField] private GameObject player;

    public LoadoutSO playerLoadout;
    public int remainingTargets;
    public LevelMetaSO levelMeta;
    public bool levelWon;

    public Sprite deadPlayerSprite;

    public LevelEndManager levelEndManager;

    private void Awake()
    {
        Debug.Log($"Entered the level {levelMeta.levelName}");
        Instance = this;
        IsPlayerAlive = true;
        levelWon = false;
        remainingTargets = levelMeta.targets;
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
        //Destroy(player);
        player.GetComponent<SpriteRenderer>().sprite = deadPlayerSprite;
        Inventory.GetInstance().GetInventoryItems().Clear();
        levelEndManager.InitiateEndScreen(levelMeta.levelName, "You Died!", "0", levelMeta.sceneName);

    }

    public void CheckWinCondition()
    {
        if(remainingTargets == 0)
        {
            levelWon = true;
            Debug.Log("Level Cleared");
        }
    }

    public void OnLevelExit()
    {
        Inventory inventory = Inventory.GetInstance();
        int totalReward = levelMeta.levelReward;
        foreach(KeyValuePair<ItemDetailSO, int> entry in inventory.GetInventoryItems())
        {
            if(entry.Key.type == ItemType.bounty)
            {
                totalReward += entry.Key.value * entry.Value;
            }
        }
        playerLoadout.playerMoney += totalReward;
        Debug.Log($"Player Gained {totalReward}");
        levelEndManager.InitiateEndScreen(levelMeta.levelName, "Level Won!", $"${totalReward}", levelMeta.sceneName);
        isPlayerAlive = false;
        levelMeta.RecordPassedLevel(levelMeta.levelName);
        levelMeta.isNextLevelUnlocked = true;
    }


}
