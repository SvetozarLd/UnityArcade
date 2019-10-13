using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Boss01Script : MonoBehaviour
{
    private GameObject Player;
    private GameObject shipMech;
    private Bezie bezie;
    public Vector2 LeftDownCorner;
    public Vector2 RightUpCorner;
    public int Steps = 100;
    public GameObject ShotBullet;
    public float TimeFreezing = 4;
    GameObject tmp;


    GameObject turret01;
    GameObject turret02;
    // Start is called before the first frame update
    void Start()
    {
        Player = MainSettings.Players.Player;
        tmp = transform.GetChild(0).gameObject;
        shipMech = transform.GetChild(1).gameObject;
        turret01 = transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;
        turret02 = transform.GetChild(1).gameObject.transform.GetChild(1).gameObject;
        bezie = new Bezie();
        StartCoroutine(Mooving());
    }

    // Update is called once per frame
    void Update()
    {
        tmp.transform.LookAt(Player.transform.position);
        shipMech.transform.rotation = Quaternion.Lerp(shipMech.transform.rotation, tmp.transform.rotation, 0.1f * Time.time);
    }

    IEnumerator Mooving()
    {
        float t = 0f;
        //Vector2 playerPosition = new Vector2(Player.transform.position.x, Player.transform.position.y);
        //Vector2[] tmp;
        Vector3 NewPlayerPosition;
        Vector2[] BeziePosition = new Vector2[4];
        while (true)
        {
            while (!MainSettings.NotPause) { yield return null; }
            BeziePosition[0] = new Vector2(transform.position.x, transform.position.y);
            BeziePosition[1] = new Vector2(Random.Range(LeftDownCorner.x, RightUpCorner.x), Random.Range(LeftDownCorner.y, RightUpCorner.y));
            BeziePosition[2] = new Vector2(Random.Range(LeftDownCorner.x, RightUpCorner.x), Random.Range(LeftDownCorner.y, RightUpCorner.y));
            BeziePosition[3] = new Vector2(Random.Range(LeftDownCorner.x, RightUpCorner.x), Random.Range(LeftDownCorner.y, RightUpCorner.y));
            t = 0f;
            float dt = 1f / Steps;
            for (int i = 0; i <= Steps; i++)
            {
                while (!MainSettings.NotPause) { yield return null; }
                NewPlayerPosition = bezie.GetBezie(t, BeziePosition);
                transform.position = new Vector3(NewPlayerPosition.x, NewPlayerPosition.y, transform.position.z);
                t += dt;
                yield return null;
            }
            StartCoroutine(Fire());
            yield return new WaitForSeconds(TimeFreezing);
        }
    }

    IEnumerator Fire()
    {
        for (int i = 0; i < 5; i++)
        {
            while (!MainSettings.NotPause) { yield return null; }
            yield return new WaitForSeconds(TimeFreezing / 8);
            PoolManager.GetObject(ShotBullet.transform.name, turret01.transform.position, Quaternion.identity);
            PoolManager.GetObject(ShotBullet.transform.name, turret02.transform.position, Quaternion.identity);
        }
    }
}
