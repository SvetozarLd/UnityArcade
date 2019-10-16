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
    public int MainShotStep = 3;
    private GameObject mainLaser;
    GameObject tmp;
    GameObject turret01;
    GameObject turret02;
    GameObject mainShot;
    private int roundCount = 0;

    private void Awake()
    {
        tmp = transform.GetChild(0).gameObject;
        shipMech = transform.GetChild(1).gameObject;
        turret01 = transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;
        turret02 = transform.GetChild(1).gameObject.transform.GetChild(1).gameObject;
        mainShot = transform.GetChild(2).gameObject;
        bezie = new Bezie();
    }

    //private void Start()
    //{

    //}

    private void OnEnable()
    {
        Player = MainSettings.Players.Player;
        MainSettings.BossUIHPPanel.gameObject.SetActive(true);
        StartCoroutine(Mooving());
    }

    private void Update()
    {
        if (roundCount == MainShotStep) { tmp.transform.LookAt(new Vector3(transform.position.x, -10, transform.position.z)); }
        else
        {
            tmp.transform.LookAt(Player.transform.position);            
        }
        shipMech.transform.rotation = Quaternion.Lerp(shipMech.transform.rotation, tmp.transform.rotation, 1f * Time.deltaTime);

    }
    IEnumerator Mooving()
    {
        float t;
        float dt = 1f / Steps;
        Vector3 NewPlayerPosition;
        Vector2[] BeziePosition = new Vector2[4];
        while (true)
        {
            while (!MainSettings.NotPause) { yield return null; }
            BeziePosition = GetPosition(BeziePosition);
            t = 0f;
            for (int i = 0; i <= Steps; i++)
            {
                while (!MainSettings.NotPause) { yield return null; }
                NewPlayerPosition = bezie.GetBezie(t, BeziePosition);
                transform.position = new Vector3(NewPlayerPosition.x, NewPlayerPosition.y, transform.position.z);
                t += dt;
                yield return null;
            }
            StartCoroutine(Fire(5));
            if (roundCount == MainShotStep)
            {
                mainShot.SetActive(true);
                yield return new WaitForSeconds(2f);
                StartCoroutine(Fire(5));
                yield return new WaitForSeconds(2f);
                StartCoroutine(Fire(5));
                yield return new WaitForSeconds(2f);
                StartCoroutine(Fire(5));
                yield return new WaitForSeconds(2f);
                StartCoroutine(Fire(5));
                yield return new WaitForSeconds(2f);
                mainShot.SetActive(false);
            }
            else { yield return new WaitForSeconds(TimeFreezing); }
        }
    }

    IEnumerator Fire(int max)
    {
        for (int i = 0; i < max; i++)
        {
            while (!MainSettings.NotPause) { yield return null; }
            yield return new WaitForSeconds(TimeFreezing / (max+2));
            MainSettings.CurPoolManager.GetObject(ShotBullet.transform.name, turret01.transform.position, Quaternion.identity);
            MainSettings.CurPoolManager.GetObject(ShotBullet.transform.name, turret02.transform.position, Quaternion.identity);
        }
    }

    Vector2[] GetPosition(Vector2[] BeziePosition)
    {
        roundCount++;
        if (roundCount > MainShotStep) { roundCount = 0; }
        if (roundCount == MainShotStep)
        {
            BeziePosition[0] = new Vector2(transform.position.x, transform.position.y);
            BeziePosition[1] = new Vector2(0, 0);
            BeziePosition[2] = new Vector2(0, 0);
            BeziePosition[3] = new Vector2(0, 11);
        }
        else
        {
            BeziePosition[0] = new Vector2(transform.position.x, transform.position.y);
            BeziePosition[1] = new Vector2(Random.Range(LeftDownCorner.x, RightUpCorner.x), Random.Range(LeftDownCorner.y, RightUpCorner.y));
            BeziePosition[2] = new Vector2(Random.Range(LeftDownCorner.x, RightUpCorner.x), Random.Range(LeftDownCorner.y, RightUpCorner.y));
            BeziePosition[3] = new Vector2(Random.Range(LeftDownCorner.x, RightUpCorner.x), Random.Range(LeftDownCorner.y, RightUpCorner.y));
        }
        

        return BeziePosition;
    }

}
