using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    public int HP;
    public int Scores;
    private int hp;
    PoolObject po;
    void Awake()
    {
        po = GetComponent<PoolObject>();
        hp = HP;
    }
    void OnEnable()
    {
        HP = hp;
    }
    public void TriggerEntered(Collider other)
    {
        switch (other.gameObject.name)
        {
            case "LaserShot":
                HP = HP - MainSettings.Players.laserDamage;
                CheckHP();
                break;
            case "MissleShot":
                HP = HP - MainSettings.Weapon.Rocket.Damage;
                CheckHP();
                break;
            case "Bomb":
                HP = HP - MainSettings.Weapon.Bomb.Damage;
                CheckHP();
                break;
        }
    }
    // Update is called once per frame
    private void CheckHP()
    {
        if (HP <= 0)
        {
            MainSettings.Enemylist.Remove(gameObject);
            po.ReturnToPool();
            PoolManager.GetObject("ExplosionSmall", transform.position, Quaternion.identity);
            MainSettings.Players.Scores += Scores;
        }
    }
}
