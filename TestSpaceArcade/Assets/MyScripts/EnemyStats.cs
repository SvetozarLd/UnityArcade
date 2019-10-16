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
                HP = HP - MainSettings.Weapon.MainWeapon.Damage;
                CheckHP();
                break;
            case "MissleShot":
                HP = HP - MainSettings.Weapon.Rocket.Damage;
                CheckHP();
                break;
            case "Player":
                HP = HP - MainSettings.Players.Damage;
                CheckHP();
                break;
                //case "Bomb":
                //    HP = HP - MainSettings.Weapon.Bomb.Damage;
                //    CheckHP();
                //    break;
        }
    }
    // Update is called once per frame
    public void CheckHP()
    {
        if (HP <= 0)
        {
            if (MainSettings.Enemylist.Contains(gameObject)) { MainSettings.Enemylist.Remove(gameObject); }
            po.ReturnToPool();
            MainSettings.CurPoolManager.GetObject("ExplosionSmall", transform.position, Quaternion.identity);
            MainSettings.Score.Scores += Scores;
        }
    }
}
