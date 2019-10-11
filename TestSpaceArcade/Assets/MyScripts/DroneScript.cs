using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DroneScript : MonoBehaviour
{
    private GameObject Player;
    public GameObject ShotBullet;
    public bool FromLeft;
    private int CornerCounter = 0;
    private Bezie bezie;
    // Start is called before the first frame update
    void Start()
    {
        Player = MainSettings.Players.Player;
        bezie = new Bezie();
        StartCoroutine(StartInitiate());
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Player.transform.position);
    }

    public Vector2 LeftDownCorner;
    public Vector2 RightUpCorner;
    public int Steps = 100;
    public float TimeFreezing = 2;
    IEnumerator Mooving()
    {
        float t = 0f;
        Vector2 playerPosition = new Vector2(Player.transform.position.x, Player.transform.position.y);
        Vector2[] tmp;
        Vector3 NewPlayerPosition;
        while (true)
        {
            if (CornerCounter == 0 || CornerCounter == 2)
            {
                StartCoroutine(Fire());
                yield return new WaitForSeconds(TimeFreezing);
                playerPosition = new Vector2(Player.transform.position.x, Player.transform.position.y);
            }
            t = 0f;
            float dt = 1f / Steps;
            tmp = GetBezieDots(playerPosition);
            for (int i = 0; i <= Steps; i++)
            {
                NewPlayerPosition = bezie.GetBezie(t, tmp);
                transform.position = new Vector3(NewPlayerPosition.x, NewPlayerPosition.y, transform.position.z);
                t += dt;
                yield return null;
            }
        }
    }


    private Vector2[] GetBezieDots(Vector2 playerPosition)
    {
        Vector2[] BeziePosition = new Vector2[4];
        switch (CornerCounter)
        {
            case 0:
                BeziePosition[0] = new Vector2(LeftDownCorner.x, RightUpCorner.y);
                BeziePosition[1] = LeftDownCorner;
                BeziePosition[2] = new Vector2(playerPosition.x, LeftDownCorner.y);
                BeziePosition[3] = playerPosition;
                CornerCounter = 1;
                break;
            case 1:
                BeziePosition[0] = playerPosition;
                BeziePosition[1] = new Vector2(playerPosition.x, RightUpCorner.y);
                BeziePosition[2] = new Vector2(RightUpCorner.x, LeftDownCorner.y);
                BeziePosition[3] = RightUpCorner;
                CornerCounter = 2;
                break;
            case 2:
                BeziePosition[0] = RightUpCorner;
                BeziePosition[1] = new Vector2(RightUpCorner.x, LeftDownCorner.y);
                BeziePosition[2] = new Vector2(playerPosition.x, LeftDownCorner.y);
                BeziePosition[3] = playerPosition;
                CornerCounter = 3;
                break;
            case 3:
                BeziePosition[0] = playerPosition;
                BeziePosition[1] = new Vector2(playerPosition.x, RightUpCorner.y);
                BeziePosition[2] = LeftDownCorner;
                BeziePosition[3] = new Vector2(LeftDownCorner.x, RightUpCorner.y);
                CornerCounter = 0;
                break;
        }

        return BeziePosition;
    }


    IEnumerator Fire()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(TimeFreezing / 4);
            GameObject go = Instantiate(ShotBullet, new Vector3(transform.position.x, transform.position.y, -10), Quaternion.Euler(0, 0, 0));
            go.GetComponent<EnemyShotScript>().Target = new Vector2(Player.transform.position.x, Player.transform.position.y);
        }
    }

    IEnumerator StartInitiate()
    {
        float t = 0f;
        Vector2[] BeziePosition = new Vector2[4];
        Vector3 NewPlayerPosition;
        t = 0f;
        float dt = 1f / Steps;
        BeziePosition[0] = new Vector2(transform.position.x, transform.position.y);
        BeziePosition[1] = new Vector2(transform.position.x, RightUpCorner.y);
        BeziePosition[2] = new Vector2(transform.position.x, RightUpCorner.y);
        if (FromLeft)
        {
            CornerCounter = 0;
            BeziePosition[3] = new Vector2(LeftDownCorner.x, RightUpCorner.y);
        }
        else
        {
            CornerCounter = 2;
            BeziePosition[3] = new Vector2(RightUpCorner.x, RightUpCorner.y);
        }
        for (int i = 0; i <= Steps; i++)
        {
            NewPlayerPosition = bezie.GetBezie(t, BeziePosition);
            transform.position = new Vector3(NewPlayerPosition.x, NewPlayerPosition.y, transform.position.z);
            t += dt;
            yield return null;
        }
        StartCoroutine(Mooving());
    }

}
