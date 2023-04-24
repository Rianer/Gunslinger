using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The interface that defines the observer for the Subject-Obesrver d.p.
/// </summary>
/// <typeparam name="T">Argument class type that needs to be received from the subject</typeparam>
public interface IObserver<T>
{
    public void UpdateObserver(T args);
}

/// <summary>
/// Interface that defines the Subject for the Subject-Obesrver d.p.
/// </summary>
/// <typeparam name="T">Argument class type that needs to be transfered to the observers</typeparam>
public interface ISubject<T>
{
    public void AddObserver(IObserver<T> observer);
    public void RemoveObserver(IObserver<T> observer);
    public void NotifyObserver(T arguments);
}

/// <summary>
/// Argument class for movement parrameters
/// </summary>
public class InputObserverArgs
{
    private Vector2 mousePosition;
    private Vector2 normalizedInputVector;
    /// <summary>
    /// Binary variable that represents which buttons were clicked
    /// </summary>
    private ClickedButtons clickedButtons = new ClickedButtons();
    public Vector2 NormalizedInputVector
    {
        get { return normalizedInputVector; }
    }

    public Vector2 MousePosition
    {
        get { return mousePosition; }
    }

    public ClickedButtons ClickedButtons
    {
        get { return clickedButtons; }
    }

    /// <summary>
    /// Add the Normalized Input Vector and the Mouse Position Vector to the Input Arguments
    /// </summary>
    /// <param name="normalizedInputVector"></param>
    /// <param name="mousePosition"></param>
    /// <returns></returns>
    public InputObserverArgs Add(Vector2 normalizedInputVector, Vector2 mousePosition)
    {
        this.normalizedInputVector = normalizedInputVector;
        this.mousePosition = mousePosition;
        return this;
    }

    public InputObserverArgs Add(ClickedButtons clickedButtons)
    {
        this.clickedButtons = clickedButtons;
        return this;
    }
}


public class ClickedButtons
{
    public bool fire;
    public bool ability;
    public bool interract;

    public ClickedButtons()
    {
        fire = false;
        ability = false;
        interract = false;
    }
}