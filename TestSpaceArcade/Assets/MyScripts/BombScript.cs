using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BombScript : MonoBehaviour
{
    //public GameObject 
    private float speed;
    private float radius;
    private Camera mainCamera;
    private bool ended = false;
    PoolObject po;
    private void Awake()
    {
        po = GetComponent<PoolObject>();
        mainCamera = MainSettings.MainCamera;
        speed = MainSettings.Weapon.Bomb.Speed;
        radius = MainSettings.Weapon.Bomb.Diameter / 2;
    }

    private void OnEnable()
    {
        ended = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                if (!ended)
                {
                    CreateExplosion("ExplosionBig", new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(-90, 0, 0));
                }
                break;
        }
    }

    void Update()
    {
        if (MainSettings.NotPause)
        {
            Vector3 point = mainCamera.WorldToViewportPoint(new Vector2(transform.position.x, transform.position.y + radius)); //Записываем положение объекта к границам камеры, X и Y это будут как раз верхние и нижние границы камеры
            if (point.y >= 1f)
            {
                if (!ended)
                {
                    CreateExplosion("ExplosionBig", new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(-90, 0, 0));
                }
            }
            else { transform.position += transform.forward * Time.deltaTime * speed; }
        }
    }
    void CreateExplosion(string explosion, Vector3 pos, Vector3 rot) //translating 'pooled' lazer shot to the defined position in the defined rotation
    {
        ended = true;
        po.ReturnToPool();
        PoolManager.GetObject("ExplosionBig", transform.position, Quaternion.identity);
        if (MainSettings.Enemylist != null)
        {
            GameObject[] lst = MainSettings.Enemylist.ToArray();
            EnemyStats tmp;
            foreach (GameObject goEnemy in lst)
            {
                if (goEnemy != null && goEnemy.tag == "Enemy")
                {
                    tmp = goEnemy.GetComponent<EnemyStats>();
                    tmp.HP = tmp.HP - MainSettings.Weapon.Bomb.Damage;
                }
            }
        }
    }
}
