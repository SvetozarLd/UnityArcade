using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMissleAttack : MonoBehaviour
{

    public GameObject Traejtory;
    private float Speed;
    GameObject Player;
    GameObject enemy;
    GameObject childrenMesh;
    PoolObject po;
    EnemyStats enemyStats;
    private void Awake()
    {
        po = GetComponent<PoolObject>();
        childrenMesh = transform.GetChild(0).gameObject;
        Speed = MainSettings.Weapon.Rocket.Speed;
    }
    // Start is called before the first frame update
    void Start()
    {
        Player = MainSettings.Players.Player;
        enemy = null;
    }

    private void OnEnable()
    {
        searcher = false;
        enemy = null;
        StartCoroutine(EnemySelector());
    }
    private void OnTriggerEnter(Collider collider)
    {
        switch (collider.gameObject.tag)
        {
            case "Enemy":
                //EnemyStats tmp = collider.transform.parent.gameObject.GetComponent<EnemyStats>();
                //tmp.HP = tmp.HP - MainSettings.Weapon.Rocket.Damage;
                //MainSettings.Enemylist.Remove(collider.transform.parent.gameObject);
                enemy = null;
                MainSettings.CurPoolManager.GetObject("ExplosionParticle", transform.position, Quaternion.identity);
                po.ReturnToPool();

                break;
        }
    }

    void Update()
    {
        if (MainSettings.NotPause)
        {
            if (enemy != null && enemyStats.HP > 0)
            {
                childrenMesh.transform.LookAt(new Vector3(enemy.transform.position.x, enemy.transform.position.y, transform.position.z));
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(enemy.transform.position.x, enemy.transform.position.y, transform.position.z), 15 * Time.deltaTime);
            }
            else
            {
                transform.position += childrenMesh.transform.forward * Time.deltaTime * Speed;
                if (searcher)
                {
                    searcher = false;
                    StartCoroutine(EnemySelector());
                }
            }
        }
        if (transform.position.x < -40 || transform.position.x > 40 || transform.position.y < -20 || transform.position.y > 20) { po.ReturnToPool(); }

    }

    bool searcher = false;
    IEnumerator EnemySelector()
    {
        GameObject tgo = null;
        if (MainSettings.Enemylist != null)
        {
            float dist = 1000;
            float tmp = 0;
            GameObject[] lst = MainSettings.Enemylist.ToArray();
            foreach (GameObject go in lst)
            {
                yield return null;
                if (go != null)
                {
                    tmp = Vector3.Distance(new Vector3(go.transform.position.x, go.transform.position.y, transform.position.z), transform.position);
                    if (tmp < dist) { tgo = go; dist = tmp; }
                }
            }
            enemy = tgo;
            if (enemy != null) { enemyStats = enemy.GetComponent<EnemyStats>(); }

        }
        searcher = true;
    }
}
