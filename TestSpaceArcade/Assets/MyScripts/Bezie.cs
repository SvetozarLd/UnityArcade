using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bezie
{
    private float c0;
    private float c1;
    private float c2;
    private float c3;
    public Vector2 GetBezie(float t, Vector2[] dataPoints) // 0<t<1
    {
        c0 = (1 - t) * (1 - t) * (1 - t);
        c1 = (1 - t) * (1 - t) * 3 * t;
        c2 = (1 - t) * t * 3 * t;
        c3 = t * t * t;

        return new Vector2
            (
            c0 * dataPoints[0].x + c1 * dataPoints[1].x + c2 * dataPoints[2].x + c3 * dataPoints[3].x,
            c0 * dataPoints[0].y + c1 * dataPoints[1].y + c2 * dataPoints[2].y + c3 * dataPoints[3].y
            );
    }

}
