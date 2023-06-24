using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "_Level_Parameters", menuName = "Scriptable Objects/Levels/New Level Parameters")]

public class LevelParametersSO : ScriptableObject
{
    public string sceneName;
    public string levelTitle;
    [TextArea(15, 20)]
    public string levelDescription;
    public SpriteRenderer thumbnail;
    public bool levelPassed = false;
    public bool levelAvailable = false;

    public int reward = 0;
    public int targets = 0;
}
