using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputHandler))]
[RequireComponent(typeof(MovementHandler))]
public class MovementController : MonoBehaviour
{
    private InputHandler inputHandler;
    private MovementHandler movementHandler;

    private void Start()
    {
        inputHandler = GetComponent<InputHandler>();
        movementHandler = GetComponent<MovementHandler>();
        inputHandler.AddObserver(movementHandler);
    }
}
