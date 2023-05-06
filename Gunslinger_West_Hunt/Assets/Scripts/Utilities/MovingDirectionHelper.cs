using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MovingDirectionHelper
{
    public static MovingDirection detectMovingDirection(Vector2 movingVector)
    {
        float x = movingVector.x;
        float y = movingVector.y;

        if(x > 0 && y == 0)
        {
            return MovingDirection.E;
        }
        if (x < 0 && y == 0)
        {
            return MovingDirection.W;
        }
        if (x > 0 && y > 0)
        {
            return MovingDirection.NE;
        }
        if (x < 0 && y > 0)
        {
            return MovingDirection.NW;
        }
        if (x > 0 && y < 0)
        {
            return MovingDirection.SE;
        }
        if (x > 0 && y < 0)
        {
            return MovingDirection.SW;
        }
        if (x == 0 && y < 0)
        {
            return MovingDirection.S;
        }
        else
        {
            return MovingDirection.N;
        }
    }
}
