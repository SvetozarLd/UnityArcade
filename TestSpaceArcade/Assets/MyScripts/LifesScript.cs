using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifesScript : MonoBehaviour
{
    public Vector3 Position;
    public Vector3 Rotation;
    public float Speed;
    // Start is called before the first frame update
    void Start()
    {
        transform.localRotation = Quaternion.Euler(new Vector3(-230, 100, 50));
        transform.localScale = new Vector3(0, 0, 0);
    }

    private float Steps = 100;
    public void StartAnimation()
    {
        StartCoroutine(StartInitiate());
    }

    IEnumerator StartInitiate()
    {
        while (!MainSettings.NotPause) { yield return null; }
        transform.localRotation = Quaternion.Euler(new Vector3(-230, 100, 50));
        transform.localScale = new Vector3(0, 0, 0);
        float t = 0f;
        Vector2[] BeziePosition = new Vector2[4];
        Vector3 NewPlayerPosition;
        t = 0f;
        float dt = 1f / Steps;
        BeziePosition[0] = new Vector2(Position.x + 50, Position.y - 50);
        BeziePosition[1] = new Vector2(Position.x, Position.y - 100);
        BeziePosition[2] = new Vector2(Position.x - 100, Position.y);
        BeziePosition[3] = new Vector2(Position.x, Position.y);
        Bezie bezie = new Bezie();
        for (int i = 0; i <= Steps; i++)
        {
            while (!MainSettings.NotPause) { yield return null; }
            NewPlayerPosition = bezie.GetBezie(t, BeziePosition);
            transform.localPosition = new Vector3(NewPlayerPosition.x, NewPlayerPosition.y, transform.position.z);
            t += dt;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Rotation), Time.deltaTime * Speed * 2f);
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(4, 4, 4), Time.deltaTime * Speed);
            yield return null;
        }

    }

}
