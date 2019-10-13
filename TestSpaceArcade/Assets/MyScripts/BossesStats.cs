using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossesStats : MonoBehaviour
{
    EnemyStats enemyStats;
    int Maxim;
    private void Awake()
    {
        enemyStats = transform.parent.transform.GetComponent<EnemyStats>();
        Maxim = enemyStats.HP;
    }
    //// Start is called before the first frame update
    //void Start()
    //{

    //}
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("BOSS"+ other.name);
        MainSettings.BossUIHPPanel.SetHP(Maxim, enemyStats.HP);
    }
    //// Update is called once per frame
    //void Update()
    //{

    //}
}
