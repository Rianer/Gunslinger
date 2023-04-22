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
public class MovementObserverArgs
{

    private Vector2 normalizedInputVector;
    public Vector2 NormalizedInputVector
    {
        get { return normalizedInputVector; }
    }
    /// <summary>
    /// Creates an instance of the class MovementObserverArgs
    /// </summary>
    /// <param name="normalizedInputVector">A normalized Vector2 that represents the player's input on vertical and horizontal axis</param>
    public MovementObserverArgs(Vector2 normalizedInputVector)
    {
        this.normalizedInputVector = normalizedInputVector;
    }
}