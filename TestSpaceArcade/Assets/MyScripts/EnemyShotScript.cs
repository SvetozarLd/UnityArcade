using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyShotScript : MonoBehaviour
{
    public Vector3 Target;
    public float Speed;
    PoolObject po;
    // Start is called before the first frame update
    private void Awake()
    {
        po = GetComponent<PoolObject>();
    }

    void OnEnable()
    {
        Target = MainSettings.Players.Player.transform.position;
        transform.LookAt(new Vector3(Target.x, Target.y, transform.position.z));
    }

     
    // Update is called once per frame
    void Update()
    {
        if (MainSettings.NotPause)
        {
            transform.position += transform.forward * Time.deltaTime * Speed;
            if (transform.position.x > 35 || transform.position.x < -35 || transform.position.y > 20 || transform.position.y < -15)
            {
                po.ReturnToPool();
            }
        }
    }
}
