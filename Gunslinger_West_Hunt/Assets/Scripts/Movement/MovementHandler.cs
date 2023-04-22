using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent (typeof(MovementConfig))]
public class MovementHandler : MonoBehaviour, IObserver<MovementObserverArgs>
{
    private Vector2 movement;
    private Rigidbody2D rb;
    private MovementConfig movementConfig;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        movement = new Vector2();
        movementConfig = GetComponent<MovementConfig>();
    }
    private void LateUpdate()
    {
        rb.velocity = movement;
    }
    public void UpdateObserver(MovementObserverArgs args)
    {
        movement = args.NormalizedInputVector * movementConfig.speed;
    }
}
