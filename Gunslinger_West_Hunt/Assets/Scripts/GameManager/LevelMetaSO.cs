using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelMeta", menuName = "Scriptable Objects/Levels/New Level Meta")]
public class LevelMetaSO : ScriptableObject
{
    public string levelName = "level";
    public int levelReward = 0;
    public int targets = 1;
}
