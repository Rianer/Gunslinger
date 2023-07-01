using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelMeta", menuName = "Scriptable Objects/Levels/New Level Meta")]
public class LevelMetaSO : ScriptableObject
{
    public string levelName = "level";
    public int levelReward = 0;
    public int targets = 1;
    public string sceneName = "Start Menu";
    [SerializeField]private List<string> passedLevels = new List<string>();
    public string nextLevel = "none";
    public bool isNextLevelUnlocked = false;
    public string levelTheme = "none";

    public void RecordPassedLevel(string level)
    {
        if(!passedLevels.Contains(level)) 
        {
            passedLevels.Add(level);
        }
    }

    public bool HasRecordedLevel(string level)
    {
        return passedLevels.Contains(level);
    }
}
