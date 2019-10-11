using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateUpfrade : MonoBehaviour
{
    public float speed = 10f;
    private float rx = 0.1f;
    private float ry = 0.1f;
    private float rz = 0.1f;
    private float tmp;
    public bool ObjectRotate = true;

    void Start()
    {
        rx = 1; ry = 1; rz = 1;
        StartCoroutine(objectRotation());
    }

    private float rotateRandomize(float min, float max)
    {
        tmp = Random.Range(min, max);
        if (tmp == 0) { return 0.01f; } else { return tmp; }
    }

    IEnumerator objectRotation()
    {
        while (ObjectRotate)
        {
            transform.Rotate(new Vector3(rx, ry, rz) * Time.deltaTime * speed);
            yield return null;
        }
    }
}
