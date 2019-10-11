using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private float controllerX = 0;
    private float controllerY = 0;
    public int MoovingSpeed = 50;
    public int RotateSpeed = 50;
    public int RotateAngle = 45;
    public Vector2 MinMaxWidth = new Vector2(-40, 40);
    public Vector2 MinMaxHeight = new Vector2(-13, 13);

    public GameObject Laser;
    public GameObject Bomb;
    public GameObject Rocket;

    private void Awake()
    {
        MainSettings.Players.Player = gameObject;
    }
    private void Start()
    {
        if (MainSettings.Enemylist != null && MainSettings.Enemylist.Count > 0)
        {
            enemy = MainSettings.Enemylist[0];
        }
            StartCoroutine(LaserShot());
    }
    private void Update()
    {
        if (MainSettings.NotPause)
        {
            controllerX = transform.position.x + Input.GetAxis("Mouse X");
            controllerY = transform.position.y + Input.GetAxis("Mouse Y");
            if (controllerX < MinMaxWidth.x) { controllerX = MinMaxWidth.x; } else { if (controllerX > MinMaxWidth.y) { controllerX = MinMaxWidth.y; } }
            if (controllerY < MinMaxHeight.x) { controllerY = MinMaxHeight.x; } else { if (controllerY > MinMaxHeight.y) { controllerY = MinMaxHeight.y; } }
            if (controllerX > transform.position.x + 0.1f) { transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(-90, -RotateAngle, 0), Time.deltaTime * RotateSpeed); } else { if (controllerX < transform.position.x - 0.01f) { transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(-90, RotateAngle, 0), Time.deltaTime * RotateSpeed); } else { transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(-90, 0, 0), Time.deltaTime * RotateSpeed); } }
            transform.position = Vector3.Lerp(transform.position, new Vector3(controllerX, controllerY, -20), Time.deltaTime * MoovingSpeed);

            if (Input.GetMouseButtonDown(0))
            {
                CreateRocketShot(Rocket, new Vector3(transform.position.x, transform.position.y, transform.position.z + 5), new Vector3(-90, 0, 0));
                //if (!MainSettings.Players.Autoshot)
                //{
                //    MainSettings.Players.Autoshot = true;
                //    StartCoroutine(LaserShot());
                //}

            }
            if (Input.GetMouseButtonDown(1))
            {
                CreateNuclearShot(Bomb, new Vector3(transform.position.x, transform.position.y, transform.position.z + 5), new Vector3(-90, 0, 0));
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {

            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (MainSettings.NotPause) { MainSettings.NotPause = false; } else { MainSettings.NotPause = true; }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            MainSettings.Players.LaserPower = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            MainSettings.Players.LaserPower = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            MainSettings.Players.LaserPower = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            MainSettings.Players.LaserPower = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            MainSettings.Players.LaserPower = 4;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            MainSettings.Players.LaserPower = 5;
        }
        //if (enemy != null)
        //{
        //    Debug.Log(enemy.transform.position);
        //}
        //else
        //{
        //    Debug.Log("NULL");
        //    if (MainSettings.Enemylist != null && MainSettings.Enemylist.Count > 0)
        //    {
        //        enemy = MainSettings.Enemylist[0];
        //    }
        //}
    }
    private GameObject enemy;

    private void OnTriggerEnter(Collider collider)
    {
        switch (collider.gameObject.tag)
        {
            case "Bonus":
                if (collider.transform.parent.gameObject.name.Equals("LaserUP(Clone)"))
                {
                    if (MainSettings.Players.LaserPower < 5) { MainSettings.Players.LaserPower++; } else { MainSettings.Players.Scores += 20; }
                    GameObject.Destroy(collider.transform.parent.gameObject, 0f);
                }
                break;
        }
    }

    void CreateRocketShot(GameObject weapon, Vector3 pos, Vector3 rot) //translating 'pooled' lazer shot to the defined position in the defined rotation
    {
        GameObject go = Instantiate(weapon, pos, Quaternion.Euler(rot));
        //GameObject.Destroy(go, 3f);
        //Debug.Log("posX"+pos.x+"|Y="+pos.y+"|Z="+pos.z);
    }

    void CreateNuclearShot(GameObject weapon, Vector3 pos, Vector3 rot) //translating 'pooled' lazer shot to the defined position in the defined rotation
    {
        GameObject go = Instantiate(weapon, pos, Quaternion.Euler(rot));
        //GameObject.Destroy(go, 3f);
        //Debug.Log("posX"+pos.x+"|Y="+pos.y+"|Z="+pos.z);
    }

    private IEnumerator LaserShot()
    {
        while (true)
        {
            if (MainSettings.NotPause && MainSettings.Players.Autoshot)
            {
                switch (MainSettings.Players.LaserPower)
                {
                    case 0:
                        Instantiate(Laser, new Vector3(transform.position.x, transform.position.y, transform.position.z + 5), Quaternion.Euler(new Vector3(-90, 90, 0)));
                        break;
                    case 1:
                        Instantiate(Laser, new Vector3(transform.position.x-0.8f, transform.position.y, transform.position.z + 5), Quaternion.Euler(new Vector3(-90, 90, 0)));
                        Instantiate(Laser, new Vector3(transform.position.x+0.8f, transform.position.y, transform.position.z + 5), Quaternion.Euler(new Vector3(-90, 90, 0)));
                        break;
                    case 2:
                        Instantiate(Laser, new Vector3(transform.position.x, transform.position.y, transform.position.z + 5), Quaternion.Euler(new Vector3(-90, 90, 0)));
                        Instantiate(Laser, new Vector3(transform.position.x+1, transform.position.y, transform.position.z + 5), Quaternion.Euler(new Vector3(-80, 90, 0)));
                        Instantiate(Laser, new Vector3(transform.position.x-1, transform.position.y, transform.position.z + 5), Quaternion.Euler(new Vector3(-100, 90, 0)));
                        break;
                    case 3:
                        Instantiate(Laser, new Vector3(transform.position.x - 0.8f, transform.position.y, transform.position.z + 5), Quaternion.Euler(new Vector3(-90, 90, 0)));
                        Instantiate(Laser, new Vector3(transform.position.x + 0.8f, transform.position.y, transform.position.z + 5), Quaternion.Euler(new Vector3(-90, 90, 0)));
                        Instantiate(Laser, new Vector3(transform.position.x+1, transform.position.y, transform.position.z + 5), Quaternion.Euler(new Vector3(-80, 90, 0)));
                        Instantiate(Laser, new Vector3(transform.position.x-1, transform.position.y, transform.position.z + 5), Quaternion.Euler(new Vector3(-100, 90, 0)));
                        break;
                    case 4:
                        Instantiate(Laser, new Vector3(transform.position.x - 0.8f, transform.position.y, transform.position.z + 5), Quaternion.Euler(new Vector3(-90, 90, 0)));
                        Instantiate(Laser, new Vector3(transform.position.x + 0.8f, transform.position.y, transform.position.z + 5), Quaternion.Euler(new Vector3(-90, 90, 0)));
                        Instantiate(Laser, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z + 5), Quaternion.Euler(new Vector3(-80, 90, 0)));
                        Instantiate(Laser, new Vector3(transform.position.x - 1, transform.position.y, transform.position.z + 5), Quaternion.Euler(new Vector3(-100, 90, 0)));
                        Instantiate(Laser, new Vector3(transform.position.x + 1, transform.position.y - 1, transform.position.z + 5), Quaternion.Euler(new Vector3(-70, 90, 0)));
                        Instantiate(Laser, new Vector3(transform.position.x - 1, transform.position.y - 1, transform.position.z + 5), Quaternion.Euler(new Vector3(-110, 90, 0)));
                        break;
                    case 5:
                        Instantiate(Laser, new Vector3(transform.position.x - 0.8f, transform.position.y, transform.position.z + 5), Quaternion.Euler(new Vector3(-90, 90, 0)));
                        Instantiate(Laser, new Vector3(transform.position.x + 0.8f, transform.position.y, transform.position.z + 5), Quaternion.Euler(new Vector3(-90, 90, 0)));
                        Instantiate(Laser, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z + 5), Quaternion.Euler(new Vector3(-80, 90, 0)));
                        Instantiate(Laser, new Vector3(transform.position.x - 1, transform.position.y, transform.position.z + 5), Quaternion.Euler(new Vector3(-100, 90, 0)));
                        Instantiate(Laser, new Vector3(transform.position.x + 1, transform.position.y - 1, transform.position.z + 5), Quaternion.Euler(new Vector3(-70, 90, 0)));
                        Instantiate(Laser, new Vector3(transform.position.x - 1, transform.position.y - 1, transform.position.z + 5), Quaternion.Euler(new Vector3(-110, 90, 0)));
                        Instantiate(Laser, new Vector3(transform.position.x + 1, transform.position.y - 1, transform.position.z + 5), Quaternion.Euler(new Vector3(-60, 90, 0)));
                        Instantiate(Laser, new Vector3(transform.position.x - 1, transform.position.y - 1, transform.position.z + 5), Quaternion.Euler(new Vector3(-120, 90, 0)));
                        break;
                }
            }
            yield return new WaitForSeconds(0.4f);
        }
    }
}
