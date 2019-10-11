using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bezie
{
    public Vector2 GetBezie(float t, Vector2[] dataPoints) // 0<t<1
    {
        float c0 = (1 - t) * (1 - t) * (1 - t);
        float c1 = (1 - t) * (1 - t) * 3 * t;
        float c2 = (1 - t) * t * 3 * t;
        float c3 = t * t * t;
        float x = c0 * dataPoints[0].x + c1 * dataPoints[1].x + c2 * dataPoints[2].x + c3 * dataPoints[3].x;
        float y = c0 * dataPoints[0].y + c1 * dataPoints[1].y + c2 * dataPoints[2].y + c3 * dataPoints[3].y;
        return new Vector2(x, y);
    }

}
