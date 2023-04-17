using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperUtilities
{
    /// <summary>
    /// Debug an empty string field from an object
    /// </summary>
    /// <param name="thisObject">The object to be validated</param>
    /// <param name="fieldName">The field that needs to be checked</param>
    /// <param name="stringToCheck">The string value of the field</param>
    /// <returns>True if the value is null and False otherwise</returns>
    public static bool ValidateCheckEmptyString(Object thisObject, string fieldName, string stringToCheck)
    {
        if (stringToCheck == null)
        {
            Debug.Log(fieldName + " is empty and must contain a value in the object " + thisObject.name.ToString());
            return true;
        }
        return false;
    }

    /// <summary>
    /// Checks if list is empty or contains null values
    /// </summary>
    /// <param name="thisObject">The object to be validated</param>
    /// <param name="fieldName">The field that needs to be checked</param>
    /// <param name="enumerableObjectToCheck">The enumerable object value of the field</param>
    /// <returns>True if the list is empty or contains null values, False otherwise</returns>
    public static bool ValidateCheckEnumerableValues(Object thisObject, string fieldName, IEnumerable enumerableObjectToCheck)
    {
        bool error = false;
        int count = 0;

        foreach(var item in enumerableObjectToCheck)
        {
            if(item == null)
            {
                Debug.Log(fieldName + " has null values in object " + thisObject.name.ToString());
                error = true;
            }
            else
            {
                count++;
            }
        }

        if(count == 0)
        {
            Debug.Log(fieldName + " has no values in object " + thisObject.name.ToString());
            error = true;
        }

        return error;
    }
}


