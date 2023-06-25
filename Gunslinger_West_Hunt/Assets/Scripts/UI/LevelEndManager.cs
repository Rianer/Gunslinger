using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelEndManager : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private GameObject endScreen;
    [SerializeField] private TextMeshProUGUI levelName;
    [SerializeField] private TextMeshProUGUI status;
    [SerializeField] private TextMeshProUGUI money;



    private void Start()
    {
        endScreen.SetActive(false);
        gameManager = GameManager.Instance;
    }

    public void InitiateEndScreen()
    {
        endScreen.SetActive (true);
    }
}
