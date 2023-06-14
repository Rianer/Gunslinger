using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DescriptionManager : MonoBehaviour
{
    public TextMeshProUGUI titleMesh;
    public TextMeshProUGUI descriptionMesh;

    private void Start()
    {
        ClearLevelDescription();
    }

    public void ApplyLevelDescription(LevelParametersSO levelParameters)
    {
        titleMesh.text = levelParameters.levelTitle;
        if (levelParameters.levelDescription != null && levelParameters.levelAvailable)
            descriptionMesh.text = levelParameters.levelDescription;
    }

    public void ClearLevelDescription()
    {
        titleMesh.text = "";
        descriptionMesh.text = "";
    }
}
