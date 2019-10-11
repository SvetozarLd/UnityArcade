using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyWallMirror : MonoBehaviour
{
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(CheckOutMirror());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.y > 20)
        {
            rb.AddExplosionForce(300, new Vector3(transform.position.x, transform.position.y+10, transform.position.z), 20);
        }
        if (transform.position.y < -15)
        {
            rb.AddExplosionForce(300, new Vector3(transform.position.x, transform.position.y-10, transform.position.z), 20);
        }
        if (transform.position.x > 32)
        {
            rb.AddExplosionForce(300, new Vector3(transform.position.x + 10, transform.position.y, transform.position.z), 20);            
        }
        if (transform.position.x < -32)
        {
            rb.AddExplosionForce(300, new Vector3(transform.position.x - 10, transform.position.y, transform.position.z), 20);
        }
        //else {  }

    }

    private IEnumerator CheckOutMirror()
    {
        while (true)
        {
            if (rb != null)
            {
                if (transform.position.y < -5)
                {
                    //Vector3 direction = transform.position - transform.position;
                    //rb.AddForceAtPosition(Vector3.up, new Vector3(transform.position.x, transform.position.y - 10, transform.position.z), ForceMode.Impulse);
                    rb.AddForce(Vector3.up);
                    yield return null;
                }
                if (transform.position.y > 5)
                {
                    //rb.AddForceAtPosition(transform.position, new Vector3(transform.position.x, transform.position.y + 10, transform.position.z), ForceMode.Impulse);
                    rb.AddForce(Vector3.down);
                    yield return null;
                }
                //if (transform.position.x > 20)
                //{
                //    rb.AddExplosionForce(300, new Vector3(transform.position.x+10, transform.position.y, transform.position.z), 20);
                //    //Debug.Log(transform.position);
                //    //rb.AddForceAtPosition(Vector3.up, transform.position * -10f, ForceMode.Impulse);
                //    //rb.AddForce(-transform.forward*100) ;
                //    yield return null;
                //}
                //if (transform.position.x < -20)
                //{
                //    rb.AddExplosionForce(300, new Vector3(transform.position.x - 10, transform.position.y, transform.position.z), 20);
                //    //rb.AddForce(-transform.forward * 100);
                //    //Debug.Log(transform.position);
                //    //rb.AddForceAtPosition(Vector3.up, transform.position * -10f, ForceMode.Impulse);
                //    yield return null;
                //}
            }
            yield return null;
        }
    }
}