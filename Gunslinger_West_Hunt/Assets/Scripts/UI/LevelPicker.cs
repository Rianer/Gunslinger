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
        button = GetComponent<Button>();
        GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        button.interactable = levelParameters.levelAvailable;
        if (levelParameters.levelPassed)
        {
            GetComponent<Image>().color = new Color32(180, 180, 180, 255);
        }
        
    }

    public void LoadScene()
    {
        ApplyLevelMeta();
        SceneManager.LoadScene(levelParameters.sceneName, LoadSceneMode.Single);
    }

    private void ApplyLevelMeta()
    {
        levelMeta.targets = levelParameters.targets;
        levelMeta.levelReward = levelParameters.reward;
        levelMeta.levelName = levelParameters.levelTitle;
    }

    
}
