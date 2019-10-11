using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LaserScript : MonoBehaviour
{

    public GameObject Explosion;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider collider)
    {
        switch (collider.gameObject.tag)
        {
            case "Enemy":
                collider.transform.parent.gameObject.GetComponent<EnemyStats>().HP -= 10;
                Bang();
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void Bang()
    {
        GameObject go = Instantiate(Explosion, transform.position, Quaternion.identity);
        MainSettings.Enemylist.Remove(gameObject);
        Destroy(gameObject, 0);
        Destroy(go, 4f);
    }
}
