using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPath : MonoBehaviour
{
    public List<Transform> checkPoints;
    public bool isCycle = false;

    private int currentCheckPointIndex;
    private int nextCheckPointIndex;
    private bool isRegressing;


    private void Awake()
    {
        isRegressing = false;
        if(checkPoints.Count > 0)
        {
            currentCheckPointIndex = 0;
            nextCheckPointIndex = currentCheckPointIndex + 1;
        }
        else
        {
            currentCheckPointIndex = -1;
            nextCheckPointIndex = -1;
        }
    }

    public Transform GetNextCheckPoint()
    {
        currentCheckPointIndex = nextCheckPointIndex;

        if(isRegressing)
            nextCheckPointIndex--;
        else
            nextCheckPointIndex++;
        
        if(nextCheckPointIndex >= checkPoints.Count || nextCheckPointIndex < 0)
        {
            HandlePathEnd();
        }

        return checkPoints[currentCheckPointIndex];
    }

    private void HandlePathEnd()
    {
        if (isCycle)
        {
            nextCheckPointIndex = 0;
        }
        else
        {
            if (isRegressing)
            {
                nextCheckPointIndex = currentCheckPointIndex + 1;
            }
            else
            {
                nextCheckPointIndex = currentCheckPointIndex - 1;
            }
            isRegressing = !isRegressing;
        }
    }

}
