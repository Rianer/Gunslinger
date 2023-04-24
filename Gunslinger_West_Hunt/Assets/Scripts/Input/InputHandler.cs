using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class InputHandler : MonoBehaviour, ISubject<MovementObserverArgs>
{
    private List<IObserver<MovementObserverArgs>> observers;
    private float horizontalAxisInput;
    private float verticalAxisInput;

    private void Awake()
    {
        horizontalAxisInput = 0f;
        verticalAxisInput = 0f;
        observers = new List<IObserver<MovementObserverArgs>>();
    }

    private void Update()
    {
        horizontalAxisInput = Input.GetAxisRaw("Horizontal");
        verticalAxisInput = Input.GetAxisRaw("Vertical");

        NotifyObserver(new MovementObserverArgs(new Vector2(horizontalAxisInput, verticalAxisInput).normalized, Camera.main.ScreenToWorldPoint(Input.mousePosition)));
    }

    public Vector2 GetInputValues()
    {
        return new Vector2(horizontalAxisInput, verticalAxisInput);
    }

    public void AddObserver(IObserver<MovementObserverArgs> observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(IObserver<MovementObserverArgs> observer)
    {
        observers.Remove(observer);
    }

    public void NotifyObserver(MovementObserverArgs arguments)
    {
        foreach(IObserver<MovementObserverArgs> observer in observers)
        {
            observer.UpdateObserver(arguments);
        }
    }

}
