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
       // rb.velocity = Vector3.ClampMagnitude(rb.velocity, MaxSpeed);
       rb.velocity = rb.velocity.normalized* Speed;
        //if (rb.velocity.magnitude > MaxSpeed)
        //{
        //    rb.velocity = Vector3.ClampMagnitude(rb.velocity, MaxSpeed);
        //}
    }
}
