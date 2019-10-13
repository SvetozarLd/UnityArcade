using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyTrajectory : MonoBehaviour
{
    public int Trajectory = 0;
    private GameObject Player;
    public float speed = 1f;
    // Start is called before the first frame update
    private void Start()
    {
        Player = MainSettings.Players.Player;
        switch (Trajectory)
        {
            case 0:
                transform.position = new Vector3(Random.Range(-9, 10) * 3, 19, -10);
                speed = 1f;
                break;
            case 1:
                //transform.position = new Vector3(0, 15, -10);
                speed = 5f;
                break;
            case 2:
                //transform.position = new Vector3(-15, 15, -10);
                speed = 5f;
                break;
            case 3:
                //transform.position = new Vector3(15, 15, -10);
                speed = 5f;
                break;
            case 4:
                //transform.position = new Vector3(15, 15, -10);
                speed = 0.5f;
                break;
            case 6:
                gameObject.transform.GetComponent<DroneScript>().FromLeft = true;
                gameObject.transform.GetComponent<DroneScript>().StartDrone();

                break;
            case 7:
                gameObject.transform.GetComponent<DroneScript>().FromLeft = false;
                gameObject.transform.GetComponent<DroneScript>().StartDrone();
                break;

        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (MainSettings.NotPause)
        {
            switch (Trajectory)
            {
                case 0:
                    transform.Translate(Vector3.down * speed * Time.deltaTime);
                    break;
                case 1:
                    transform.Translate(Vector3.down * speed * Time.deltaTime);
                    transform.position = Vector3.Lerp(transform.position, new Vector3(Mathf.Cos(transform.position.y / 2) * 15, transform.position.y, -15), Time.deltaTime * (speed / 3));
                    //transform.GetChild(0).transform.LookAt(new Vector3(Player.transform.position.x, Player.transform.position.y, transform.position.z));
                    CheckOutDestroy();
                    break;
                case 2:
                    transform.Translate(Vector3.down * speed * Time.deltaTime);
                    transform.position = Vector3.Lerp(transform.position, new Vector3(Mathf.Cos(transform.position.y / 2) * 15 - 15, transform.position.y, -15), Time.deltaTime * (speed / 3));
                    //transform.GetChild(0).transform.LookAt(new Vector3(Player.transform.position.x, Player.transform.position.y, transform.position.z));
                    CheckOutDestroy();
                    break;
                case 3:
                    transform.Translate(Vector3.down * speed * Time.deltaTime);
                    transform.position = Vector3.Lerp(transform.position, new Vector3(Mathf.Cos(transform.position.y / 2) * 15 + 15, transform.position.y, -15), Time.deltaTime * (speed / 3));
                    //transform.GetChild(0).transform.LookAt(new Vector3(Player.transform.position.x, Player.transform.position.y, transform.position.z));
                    CheckOutDestroy();
                    break;
                case 4:
                    transform.Translate(Vector3.down * speed * Time.deltaTime);
                    CheckOutDestroy();
                    break;
                case 5:
                    transform.Translate(Vector3.down * speed * Time.deltaTime);
                    CheckOutDestroy();
                    break;
            }
        }

        void CheckOutDestroy()
        {
            if (transform.position.y < -20 || transform.position.y > 20 || transform.position.x < -35 || transform.position.x > 35)
            {
                MainSettings.Enemylist.Remove(gameObject);
                GetComponent<PoolObject>().ReturnToPool();
            }
        }

    }
}
