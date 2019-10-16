using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BombScript : MonoBehaviour
{
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
                    StartCoroutine(CreateExplosion("ExplosionBig", new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(-90, 0, 0)));
                }
                break;
        }
    }

    void Update()
    {
        if (MainSettings.NotPause)
        {
            transform.position += transform.forward * Time.deltaTime * speed;
            Vector3 point = mainCamera.WorldToViewportPoint(new Vector2(transform.position.x, transform.position.y + radius)); //Записываем положение объекта к границам камеры, X и Y это будут как раз верхние и нижние границы камеры
            if (point.y >= 1f)
            {
                if (!ended)
                {
                    StartCoroutine(CreateExplosion("ExplosionBig", new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(-90, 0, 0)));
                }
            }
        }
    }

    IEnumerator CreateExplosion(string explosion, Vector3 pos, Vector3 rot)
    {
        ended = true;

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
                    tmp.CheckHP();
                }
                yield return null;
            }
        }
        po.ReturnToPool();
        MainSettings.CurPoolManager.GetObject("ExplosionBig", transform.position, Quaternion.identity);
    }
}
