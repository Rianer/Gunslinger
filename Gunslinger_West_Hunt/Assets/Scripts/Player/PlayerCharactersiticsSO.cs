using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCharacteristics_", menuName = "Scriptable Objects/Player/Player Characteristics")]
public class PlayerCharactersiticsSO : ScriptableObject
{
    public string playerCharacterName;
    public GameObject playerPrefab;
    public int playerHealthAmount;


    #region VALIDATION
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckEmptyString(this, nameof(playerCharacterName), playerCharacterName);
        HelperUtilities.ValidateCheckNullValue(this, nameof(playerPrefab), playerPrefab);
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(playerHealthAmount), playerHealthAmount, false);

    }
#endif
#endregion VALIDATION
}
