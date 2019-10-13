using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIBossHP : MonoBehaviour
{

    RectTransform bossRed;
    RectTransform bossGray;
    public int Maxim;
    public int Current;
    private void Awake()
    {
        MainSettings.BossUIHPPanel = gameObject.GetComponent<UIBossHP>();
        bossRed = transform.GetChild(0).gameObject.GetComponent<RectTransform>();
        bossGray = transform.GetChild(1).gameObject.GetComponent<RectTransform>();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    SetHP(Maxim, enemyStats.HP);
        //}
    }
    public void SetHP(int Max, int Cur)
    {
        float x = ((float)Cur / Max) * 480f;
        //Debug.Log("MAX:" + Max + "|Cur:" + Cur + "|x:" + x);
        //bossRed.SetSizeWithCurrentAnchors( RectTransform.Axis.Vertical, x);
        bossRed.sizeDelta = new Vector2(x, bossRed.sizeDelta.y);
        bossGray.sizeDelta = new Vector2(480-x, bossRed.sizeDelta.y);
    }
}
