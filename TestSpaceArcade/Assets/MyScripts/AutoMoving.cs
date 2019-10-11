using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AutoMoving : MonoBehaviour
{
    public float speed;
    public direction Direction;
    public enum direction
    {
        Up,
        Down,
        Left,
        Right
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (MainSettings.NotPause)
        {
            switch (Direction)
            {
                case direction.Down:
                    transform.Translate(Vector3.down * speed * Time.deltaTime);
                    break;
                case direction.Up:
                    transform.Translate(Vector3.up * speed * Time.deltaTime);
                    break;
                case direction.Right:
                    transform.Translate(Vector3.right * speed * Time.deltaTime);
                    break;
                case direction.Left:
                    transform.Translate(Vector3.left * speed * Time.deltaTime);
                    break;
            }

        }
    }
}
