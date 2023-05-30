using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCharacteristics_", menuName = "Scriptable Objects/Player/Player Characteristics")]
public class PlayerCharactersiticsSO : ScriptableObject
{
    public string playerCharacterName;
    public GameObject playerPrefab;
    public int playerHealthAmount;
}
