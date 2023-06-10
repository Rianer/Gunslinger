using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresetSelector : MonoBehaviour
{
    public LoadoutSO currentLoadout;
    [SerializeField] private GameObject anchorObject;

    private void Awake()
    {
        Transform anchorPoint = anchorObject.transform;
        GameObject instantiatedGun = Instantiate(currentLoadout.gunPrefab, anchorPoint.position, anchorPoint.rotation);

        instantiatedGun.transform.parent = anchorObject.transform;
    }
}
