using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelPicker : MonoBehaviour
{
    public LevelParametersSO levelParameters;
    [SerializeField] private TextMeshProUGUI titleTextMesh;
    [SerializeField] private TextMeshProUGUI descriptionTextMesh;

    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.interactable = levelParameters.levelAvailable;
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(levelParameters.sceneName, LoadSceneMode.Single);
    }

    
}
