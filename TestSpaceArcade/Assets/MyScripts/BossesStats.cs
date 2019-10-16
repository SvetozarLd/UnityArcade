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

    private void OnTriggerEnter(Collider other)
    {
        enemyStats.TriggerEntered(other);
        MainSettings.BossUIHPPanel.SetHP(Maxim, enemyStats.HP);
    }
}
