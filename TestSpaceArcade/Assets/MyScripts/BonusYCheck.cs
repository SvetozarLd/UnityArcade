using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusYCheck : MonoBehaviour
{
    void Update()
    {
        if (transform.position.y < -16)
        {
            GetComponent<PoolObject>().ReturnToPool();
        }
    }
}
