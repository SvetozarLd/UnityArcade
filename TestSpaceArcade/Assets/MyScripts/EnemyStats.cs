using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    public int HP;
    public int Scores;
    public GameObject Explosion;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
        {
            GameObject go = Instantiate(Explosion, transform.position, Quaternion.identity);
            MainSettings.Enemylist.Remove(gameObject);
            Destroy(gameObject, 0);
            Destroy(go, 4f);
            MainSettings.Players.Scores += Scores;
        }
    }
}
