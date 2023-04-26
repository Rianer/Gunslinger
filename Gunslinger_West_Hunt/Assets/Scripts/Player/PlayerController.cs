using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(InputHandler))]
[RequireComponent(typeof(MovementHandler))]
[RequireComponent(typeof(AimingHandler))]
[RequireComponent(typeof(WeaponHandler))]
public class PlayerController : MonoBehaviour
{
    private InputHandler inputHandler;
    private MovementHandler movementHandler;
    private AimingHandler aimingHandler;
    private WeaponHandler fireHandler;
    private CollisionHandler collisionHandler;

    private void Start()
    {
        inputHandler = GetComponent<InputHandler>();
        movementHandler = GetComponent<MovementHandler>();
        aimingHandler = GetComponent<AimingHandler>();
        fireHandler = GetComponent<WeaponHandler>();
        inputHandler.AddObserver(movementHandler);
        inputHandler.AddObserver(aimingHandler);
        inputHandler.AddObserver(fireHandler);
    }

    
}
