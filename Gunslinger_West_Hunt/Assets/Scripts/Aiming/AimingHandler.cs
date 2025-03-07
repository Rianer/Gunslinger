using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
public class AimingHandler : MonoBehaviour, IObserver<InputObserverArgs>
{
    private Rigidbody2D rb;
    private Vector2 mousePosition;
    public GameObject crossHairPrefab;
    private GameObject crossHair;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mousePosition = new Vector2();
        crossHair = Instantiate(crossHairPrefab, mousePosition, Quaternion.identity);
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        Vector2 lookDirection = mousePosition - rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    public void UpdateObserver(InputObserverArgs args)
    {
        mousePosition = args.MousePosition;
        crossHair.transform.position = mousePosition;
    }
}
