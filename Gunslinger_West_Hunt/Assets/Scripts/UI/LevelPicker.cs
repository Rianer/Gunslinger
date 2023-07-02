using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelPicker : MonoBehaviour
{
    public LevelParametersSO levelParameters;
    [SerializeField] private LevelMetaSO levelMeta;
    [SerializeField] private TextMeshProUGUI titleTextMesh;
    [SerializeField] private TextMeshProUGUI descriptionTextMesh;

    private Button button;

    private void Start()
    {
        levelParameters.levelAvailable = levelMeta.CheckAvailableLevel(levelParameters.levelTitle);
        //if (levelMeta.isNextLevelUnlocked && levelMeta.nextLevel == levelParameters.levelTitle)
        //{
        //    levelParameters.levelAvailable = true;
        //}
        button = GetComponent<Button>();
        GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        button.interactable = levelParameters.levelAvailable;
        if (levelMeta.HasRecordedLevel(levelParameters.levelTitle))
        {
            levelParameters.levelPassed = true;
        }
        if (levelParameters.levelPassed)
        {
            GetComponent<Image>().color = new Color32(180, 180, 180, 255);
        }
    }

    public void LoadScene()
    {
        MenuManager.Instance.soundManager.StopSound("main_menu_theme");
        ApplyLevelMeta();
        SceneManager.LoadScene(levelParameters.sceneName, LoadSceneMode.Single);
    }

    private void ApplyLevelMeta()
    {
        levelMeta.targets = levelParameters.targets;
        levelMeta.levelReward = levelParameters.reward;
        levelMeta.levelName = levelParameters.levelTitle;
        levelMeta.sceneName = levelParameters.sceneName;
        levelMeta.nextLevel = levelParameters.nextLevelUnlock;
        levelMeta.isNextLevelUnlocked = false;
        levelMeta.levelTheme = levelParameters.levelTheme;
    }
  

    
}
