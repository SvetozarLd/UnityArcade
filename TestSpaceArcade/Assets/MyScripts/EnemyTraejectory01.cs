using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTraejectory01 : MonoBehaviour
{
    private Bezie bezie;
    PoolObject po;
    public GameObject ShotBullet;
    private void Awake()
    {
        po = GetComponent<PoolObject>();
        bezie = new Bezie();
        mov = true;
    }
    private void OnEnable()
    {
        if (mov) { StartCoroutine(Mooving()); }
    }

    public Vector2[] BeziePosition = new Vector2[4];
    public int Steps = 500;

    private bool mov;
    IEnumerator Mooving()
    {
        mov = false;
        while (!MainSettings.NotPause) { yield return null; }
        float t = 0f;
        Vector3 NewPlayerPosition;
        t = 0f;
        float dt = 1f / Steps;
        for (int i = 0; i <= Steps; i++)
        {
            while (!MainSettings.NotPause) { yield return null; }
            NewPlayerPosition = bezie.GetBezie(t, BeziePosition);
            NewPlayerPosition = new Vector3(NewPlayerPosition.x, NewPlayerPosition.y, transform.position.z);
            Vector3 direction = NewPlayerPosition - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(transform.forward, direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 10 * Time.time);
            transform.position = NewPlayerPosition;
            t += dt;
            if (i == Steps / 2) { PoolManager.GetObject(ShotBullet.transform.name, new Vector3(transform.position.x, transform.position.y, -10), Quaternion.Euler(0, 0, 0)); }
            yield return null;
        }
        MainSettings.Enemylist.Remove(gameObject);
        po.ReturnToPool();
        mov = true;
    }

}
