using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndManager : MonoBehaviour
{
    private string sceneName;
    [SerializeField] private GameObject endScreen;
    [SerializeField] private TextMeshProUGUI levelName;
    [SerializeField] private TextMeshProUGUI status;
    [SerializeField] private TextMeshProUGUI money;



    private void Start()
    {
        endScreen.SetActive(false);
    }

    public void InitiateEndScreen(string levelName, string status, string money, string sceneName)
    {
        this.sceneName = sceneName;
        this.levelName.text = levelName;
        this.status.text = status;
        this.money.text = money;
        endScreen.SetActive (true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Start Menu", LoadSceneMode.Single);
    }
}
