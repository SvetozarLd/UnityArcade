using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTraejectory01 : MonoBehaviour
{
    public GameObject ShotBullet;
    public Vector2[] BeziePosition = new Vector2[4];
    public int Steps = 1000;

    private Bezie bezie;
    private PoolObject po;
    private bool mov;
    private float t = 0;
    private float dt = 0;
    private Vector2 tmp;
    private void Awake()
    {
        po = GetComponent<PoolObject>();
        bezie = new Bezie();
        transform.position = new Vector3(50, 50, 0);
        mov = true;
        t = 0;
        dt = 1f / Steps;
    }

    private void OnEnable()
    {
      StartCoroutine(Mooving()); 
    }


    IEnumerator Mooving()
    {
            t = 0f;
            Vector3 NewPlayerPosition;
            for (int i = 0; i <= Steps; i++)
            {
                while (!MainSettings.NotPause) { yield return null; }
                tmp = bezie.GetBezie(t, BeziePosition);
                NewPlayerPosition = new Vector3(tmp.x, tmp.y, transform.position.z);
                Vector3 direction = NewPlayerPosition - transform.position;
                Quaternion toRotation = Quaternion.LookRotation(transform.forward, direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 10 * Time.time);
                transform.position = NewPlayerPosition;
                t += dt;
                if (i == Steps / 2) { MainSettings.CurPoolManager.GetObject(ShotBullet.transform.name, new Vector3(transform.position.x, transform.position.y, -10), Quaternion.Euler(0, 0, 0)); }
                yield return null;
            }
            MainSettings.Enemylist.Remove(gameObject);
            po.ReturnToPool();
    }

}
