using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateConstant : MonoBehaviour
{
    public float speed = 10f;
    private float rx = 0.1f;
    private float ry = 0.1f;
    private float rz = 0.1f;
    private float tmp;
    public bool ObjectRotate = true;

    void Start()
    {
        rx = rotateRandomize(-1.0f, 1.0f);
        ry = rotateRandomize(-1.0f, 1.0f);
        rz = rotateRandomize(-1.0f, 1.0f);
        speed = rotateRandomize(-5.0f, 5.0f) * 10; 
        transform.localScale = new Vector3(Random.Range(0.3f, 0.8f), Random.Range(0.3f, 0.8f), Random.Range(0.3f, 0.8f)); 
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
