using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
public class AimingHandler : MonoBehaviour, IObserver<MovementObserverArgs>
{
    private Rigidbody2D rb;
    private Vector2 mousePosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mousePosition = new Vector2();
    }

    private void FixedUpdate()
    {
        Vector2 lookDirection = mousePosition - rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    public void UpdateObserver(MovementObserverArgs args)
    {
        mousePosition = args.MousePosition;
    }
}
