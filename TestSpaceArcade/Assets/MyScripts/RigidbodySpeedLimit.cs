using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RigidbodySpeedLimit : MonoBehaviour
{
    public float Speed;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       rb.velocity = rb.velocity.normalized* Speed;
    }
}
