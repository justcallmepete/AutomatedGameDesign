using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VecDeg
{
    public static int VecToDegree(Vector2 v, int div)
    {
        float angle = Vector2.Angle(Vector2.up, v);
        Vector3 cross = Vector3.Cross(Vector2.up, v);
        if (cross.z > 0) angle = 360 - angle;

        if (angle % (360f / div) > 180f / div)
        {
            angle += (360f / div) - (angle % (360f / div));
        }
        else angle -= angle % (360f / div);
        return (int)(div * angle / 360f);
    }
}
