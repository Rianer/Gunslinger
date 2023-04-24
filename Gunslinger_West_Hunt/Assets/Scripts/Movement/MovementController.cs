using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(InputHandler))]
[RequireComponent(typeof(MovementHandler))]
[RequireComponent(typeof(AimingHandler))]
public class MovementController : MonoBehaviour
{
    private InputHandler inputHandler;
    private MovementHandler movementHandler;
    private AimingHandler aimingHandler;

    private void Start()
    {
        inputHandler = GetComponent<InputHandler>();
        movementHandler = GetComponent<MovementHandler>();
        aimingHandler = GetComponent<AimingHandler>();
        inputHandler.AddObserver(movementHandler);
        inputHandler.AddObserver(aimingHandler);
    }
}
