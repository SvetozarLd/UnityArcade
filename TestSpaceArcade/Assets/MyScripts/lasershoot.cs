using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class lasershoot : MonoBehaviour
{
    PoolObject po;

    private void Awake()
    {
        po = GetComponent<PoolObject>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        switch (collider.gameObject.tag)
        {
            case "Enemy":
                MainSettings.CurPoolManager.GetObject("ExplosionSmall", transform.position, Quaternion.identity);
                po.ReturnToPool();
                break;
        }
    }

    void Update()
    {
        if (MainSettings.NotPause)
        {
            transform.position += transform.forward * Time.deltaTime * 10;
            if (transform.position.x>35|| transform.position.x < -35|| transform.position.y > 20 || transform.position.y < -15)
            {
                po.ReturnToPool();
            }
        }
    }

}
