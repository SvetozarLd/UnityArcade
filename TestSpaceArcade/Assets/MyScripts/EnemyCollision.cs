using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    EnemyStats enemyStats;
    private void Awake()
    {
        enemyStats = transform.parent.transform.GetComponent<EnemyStats>();
    }
    private void OnTriggerEnter(Collider other){enemyStats.TriggerEntered(other);}
}
