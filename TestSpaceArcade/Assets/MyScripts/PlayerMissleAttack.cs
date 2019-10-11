using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissleAttack : MonoBehaviour
{

    public GameObject Traejtory;
    public GameObject Explosion;

    private GameObject Player;
    private GameObject enemy;    
    private GameObject childrenMesh;

    // Start is called before the first frame update
    void Start()
    {
        Player = MainSettings.Players.Player;
        enemy = null;
        childrenMesh = transform.GetChild(0).gameObject;
        searcher = false;
        StartCoroutine(EnemySelector());

    }
    private void OnTriggerEnter(Collider collider)
    {
        switch (collider.gameObject.tag)
        {
            case "Enemy":
                collider.transform.parent.gameObject.GetComponent<EnemyStats>().HP = -1;
                enemy = null;
                MainSettings.Enemylist.Remove(collider.transform.parent.gameObject);
                Bang();
                break;
        }
    }
    public void Bang()
    {
        GameObject go = Instantiate(Explosion, transform.position, Quaternion.identity);
        Destroy(gameObject, 0);
        Destroy(go, 4f);
    }

    void Update()
    {
        if (MainSettings.NotPause)
        {
            if (enemy != null && enemy.GetComponent<EnemyStats>().HP > 0)
            {
                childrenMesh.transform.LookAt(new Vector3(enemy.transform.position.x, enemy.transform.position.y, transform.position.z));
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(enemy.transform.position.x, enemy.transform.position.y, transform.position.z), 15 * Time.deltaTime);
            }
            else
            {
                transform.position += childrenMesh.transform.forward * Time.deltaTime * 15;
                if (searcher)
                {
                    searcher = false;
                    StartCoroutine(EnemySelector());
                }
            }
        }
        if (transform.position.x < -40 || transform.position.x > 40 || transform.position.y < -20 || transform.position.y > 20){Destroy(gameObject, 0);}
        
    }

    private bool searcher = false;
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
                tmp = Vector3.Distance(new Vector3(go.transform.position.x, go.transform.position.y, transform.position.z), transform.position);
                if (tmp < dist) { tgo = go; dist = tmp; }
                yield return null;
            }
            enemy = tgo;
        }
        searcher = true;
    }


    //private GameObject SelectAim()
    //{
    //    if (MainSettings.Enemylist != null)
    //    {
    //        GameObject tgo = null;
    //        float dist = 1000;
    //        foreach (GameObject go in MainSettings.Enemylist)
    //        {
    //            if (dist > Vector3.Distance(new Vector3(go.transform.position.x, go.transform.position.y, transform.position.z), transform.position)) { tgo = go; }
    //        }

    //        return tgo;
    //    }
    //    else
    //    {
    //        return null;
    //    }

    //}
}
