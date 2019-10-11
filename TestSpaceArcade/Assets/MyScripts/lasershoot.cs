using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lasershoot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (MainSettings.NotPause)
        {
            transform.position += transform.forward * Time.deltaTime * 10;
            if (transform.position.x>35|| transform.position.x < -35|| transform.position.y > 20 || transform.position.y < -15)
            {
                Destroy(gameObject, 0f);
            }
        }
    }

}
