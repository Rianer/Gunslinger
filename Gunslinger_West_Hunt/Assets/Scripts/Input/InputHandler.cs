using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

[DisallowMultipleComponent]
public class InputHandler : MonoBehaviour, ISubject<InputObserverArgs>
{
    private List<IObserver<InputObserverArgs>> observers;
    private float horizontalAxisInput;
    private float verticalAxisInput;
    private ClickedButtons clickedButtons;

    private void Awake()
    {
        horizontalAxisInput = 0f;
        verticalAxisInput = 0f;
        clickedButtons = new ClickedButtons();
        observers = new List<IObserver<InputObserverArgs>>();
    }

    private void Update()
    {

        HandleClickedFireB();
        horizontalAxisInput = Input.GetAxisRaw("Horizontal");
        verticalAxisInput = Input.GetAxisRaw("Vertical");
        Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 normalizedInputVector = new Vector2(horizontalAxisInput, verticalAxisInput).normalized;
        NotifyObserver(new InputObserverArgs()
            .Add(normalizedInputVector, cursorPosition)
            .Add(clickedButtons)
            );
    }

    public Vector2 GetInputValues()
    {
        return new Vector2(horizontalAxisInput, verticalAxisInput);
    }

    public void AddObserver(IObserver<InputObserverArgs> observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(IObserver<InputObserverArgs> observer)
    {
        observers.Remove(observer);
    }

    public void NotifyObserver(InputObserverArgs arguments)
    {
        foreach(IObserver<InputObserverArgs> observer in observers)
        {
            observer.UpdateObserver(arguments);
        }
    }

    private void HandleClickedFireB()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            clickedButtons.fire = false;
            return;
        }
        if (Input.GetAxisRaw("Fire1") != 0)
        {
            clickedButtons.fire = true;
        }
        else if(Input.GetAxisRaw("Fire1") == 0)
        {
            clickedButtons.fire = false;
        }
    }

}
