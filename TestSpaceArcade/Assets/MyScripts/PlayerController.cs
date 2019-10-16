using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private float controllerX = 0;
    private float controllerY = 0;
    public int MoovingSpeed = 50;
    public int RotateSpeed = 50;
    public int RotateAngle = 45;
    public Vector2 MinMaxWidth = new Vector2(-40, 40);
    public Vector2 MinMaxHeight = new Vector2(-13, 13);

    [Space]
    public GameObject LaserUP;
    public GameObject RocketUP;
    public GameObject BombUP;
    public GameObject LifeUP;

    public bool looser = false;
    GameObject mech;
    private void Awake()
    {
        mech = transform.GetChild(1).gameObject;
    }
    private void Start()
    {
        MainSettings.Players.Player = gameObject;
        StartCoroutine(StartPlayer());
        StartCoroutine(LaserShot());
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (looser)
            {
                MainSettings.SetDefaultSettings();
                SceneManager.LoadScene("Level");
            }
            MainSettings.NotPause = !MainSettings.NotPause;
            //if (MainSettings.NotPause) { MainSettings.NotPause = false; } else { MainSettings.NotPause = true; }
        }

        if (MainSettings.NotPause && MainSettings.Players.UnLockController)
        {
            controllerX = transform.position.x + Input.GetAxis("Mouse X");
            controllerY = transform.position.y + Input.GetAxis("Mouse Y");
            if (controllerX < MinMaxWidth.x) { controllerX = MinMaxWidth.x; } else { if (controllerX > MinMaxWidth.y) { controllerX = MinMaxWidth.y; } }
            if (controllerY < MinMaxHeight.x) { controllerY = MinMaxHeight.x; } else { if (controllerY > MinMaxHeight.y) { controllerY = MinMaxHeight.y; } }
            if (controllerX > transform.position.x + 0.1f)
            { mech.transform.rotation = Quaternion.Lerp(mech.transform.rotation, Quaternion.Euler(-90, -RotateAngle, 0), Time.deltaTime * RotateSpeed); }
            else
            {
                if (controllerX < transform.position.x - 0.01f)
                { mech.transform.rotation = Quaternion.Lerp(mech.transform.rotation, Quaternion.Euler(-90, RotateAngle, 0), Time.deltaTime * RotateSpeed); }
                else { mech.transform.rotation = Quaternion.Lerp(mech.transform.rotation, Quaternion.Euler(-90, 0, 0), Time.deltaTime * RotateSpeed); }
            }
            transform.position = Vector3.Lerp(transform.position, new Vector3(controllerX, controllerY, -20), Time.deltaTime * MoovingSpeed);

            if (Input.GetMouseButtonDown(0) && MainSettings.Weapon.Rocket.Count > 0) { MainSettings.Weapon.Rocket.Count--; CreateNotMainShot("MissleShot", new Vector3(transform.position.x, transform.position.y, transform.position.z + 5), new Vector3(-90, 0, 0)); }
            if (Input.GetMouseButtonDown(1) && MainSettings.Weapon.Bomb.Count > 0) { MainSettings.Weapon.Bomb.Count--; CreateNotMainShot("Bomb", new Vector3(transform.position.x, transform.position.y, transform.position.z + 5), new Vector3(-90, 0, 0)); }



        #region ForTesting
        if (Input.GetKeyDown(KeyCode.Space))
            {
                if (MainSettings.Players.Invulnerability) { MainSettings.Players.Invulnerability = false; } else { MainSettings.Players.Invulnerability = true; }
            }

            #region MainWeapon

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                MainSettings.Weapon.MainWeapon.Power = 0;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                MainSettings.Weapon.MainWeapon.Power = 1;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                MainSettings.Weapon.MainWeapon.Power = 2;
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                MainSettings.Weapon.MainWeapon.Power = 3;
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                MainSettings.Weapon.MainWeapon.Power = 4;
            }
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                MainSettings.Weapon.MainWeapon.Power = 5;
            }
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                Bonus( AddBonusScript.BonusType.MainWeapon);
            }
            #endregion
            #region Add|Subtract Life
            if (Input.GetKeyDown(KeyCode.Q))
            {            
                MainSettings.Lifes.Count = MainSettings.Lifes.Count -1;
            Debug.Log(MainSettings.Lifes.Count);
        }
            if (Input.GetKeyDown(KeyCode.W))
            {
                MainSettings.Lifes.Count++;
            Debug.Log(MainSettings.Lifes.Count);
        }
            if (Input.GetKeyDown(KeyCode.E))
            {
                Bonus(AddBonusScript.BonusType.Life);
            }
            #endregion
            #region Add|Subtract Rocket
            if (Input.GetKeyDown(KeyCode.A))
            {
                MainSettings.Weapon.Rocket.Count--;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                MainSettings.Weapon.Rocket.Count++;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                Bonus(AddBonusScript.BonusType.Rocket);
            }
            #endregion
            #region Add|Subtract Nuclear
            if (Input.GetKeyDown(KeyCode.Z))
            {
                MainSettings.Weapon.Bomb.Count--;
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                MainSettings.Weapon.Bomb.Count++;
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                Bonus(AddBonusScript.BonusType.Nuclear);
            }
            #endregion
            #endregion
        }
    }



    private void OnTriggerEnter(Collider collider)
    {
        if (!MainSettings.Players.Invulnerability && (collider.gameObject.tag.Equals("Enemy") || collider.gameObject.tag.Equals("EnemyShot")))
        {
            MainSettings.Players.Invulnerability = true;
            MainSettings.Weapon.MainWeapon.Power = 0;
            MainSettings.CurPoolManager.GetObject("ExplosionBig", transform.position, Quaternion.identity);
            transform.position = new Vector3(0, -16, -20);
            MainSettings.Lifes.Count--;
            if (MainSettings.Lifes.Count < 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    MainSettings.CurPoolManager.GetObject("ExplosionBig", new Vector3(Random.Range(-8, 9) * 3, Random.Range(-4, 5) * 2, -10), Quaternion.identity);
                }
                MainSettings.NotPause = false;
                looser = true;
                MainSettings.PausePanel.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "You lose!\r\n Press ESC to restart level";
                MainSettings.PausePanel.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().resizeTextForBestFit = true;
            }
            else
            {
               StartCoroutine(StartPlayer());
            }

        }
    }

    private void CreateNotMainShot(string obj, Vector3 pos, Vector3 rot)
    {
        MainSettings.CurPoolManager.GetObject(obj, pos, Quaternion.Euler(rot));
    }

    #region Main weapon Autoshot
    private IEnumerator LaserShot()
    {
        while (true)
        {
            if (MainSettings.NotPause && MainSettings.Weapon.MainWeapon.Autoshot)
            {
                switch (MainSettings.Weapon.MainWeapon.Power)
                {
                    case 0:
                        //Instantiate(Laser, new Vector3(transform.position.x, transform.position.y, transform.position.z + 5), Quaternion.Euler(new Vector3(-90, 90, 0)));
                        MainSettings.CurPoolManager.GetObject("LaserShot", new Vector3(transform.position.x, transform.position.y, transform.position.z + 5), Quaternion.Euler(new Vector3(-90, 90, 0)));
                        break;
                    case 1:
                        //Instantiate(Laser, new Vector3(transform.position.x - 0.8f, transform.position.y, transform.position.z + 5), Quaternion.Euler(new Vector3(-90, 90, 0)));
                        //Instantiate(Laser, new Vector3(transform.position.x + 0.8f, transform.position.y, transform.position.z + 5), Quaternion.Euler(new Vector3(-90, 90, 0)));
                        MainSettings.CurPoolManager.GetObject("LaserShot", new Vector3(transform.position.x - 0.8f, transform.position.y, transform.position.z + 5), Quaternion.Euler(new Vector3(-90, 90, 0)));
                        MainSettings.CurPoolManager.GetObject("LaserShot", new Vector3(transform.position.x + 0.8f, transform.position.y, transform.position.z + 5), Quaternion.Euler(new Vector3(-90, 90, 0)));
                        break;
                    case 2:
                        MainSettings.CurPoolManager.GetObject("LaserShot", new Vector3(transform.position.x, transform.position.y, transform.position.z + 5), Quaternion.Euler(new Vector3(-90, 90, 0)));
                        MainSettings.CurPoolManager.GetObject("LaserShot", new Vector3(transform.position.x + 1, transform.position.y, transform.position.z + 5), Quaternion.Euler(new Vector3(-80, 90, 0)));
                        MainSettings.CurPoolManager.GetObject("LaserShot", new Vector3(transform.position.x - 1, transform.position.y, transform.position.z + 5), Quaternion.Euler(new Vector3(-100, 90, 0)));
                        break;
                    case 3:
                        MainSettings.CurPoolManager.GetObject("LaserShot", new Vector3(transform.position.x - 0.8f, transform.position.y, transform.position.z + 5), Quaternion.Euler(new Vector3(-90, 90, 0)));
                        MainSettings.CurPoolManager.GetObject("LaserShot", new Vector3(transform.position.x + 0.8f, transform.position.y, transform.position.z + 5), Quaternion.Euler(new Vector3(-90, 90, 0)));
                        MainSettings.CurPoolManager.GetObject("LaserShot", new Vector3(transform.position.x + 1, transform.position.y, transform.position.z + 5), Quaternion.Euler(new Vector3(-80, 90, 0)));
                        MainSettings.CurPoolManager.GetObject("LaserShot", new Vector3(transform.position.x - 1, transform.position.y, transform.position.z + 5), Quaternion.Euler(new Vector3(-100, 90, 0)));
                        break;
                    case 4:
                        MainSettings.CurPoolManager.GetObject("LaserShot", new Vector3(transform.position.x - 0.8f, transform.position.y, transform.position.z + 5), Quaternion.Euler(new Vector3(-90, 90, 0)));
                        MainSettings.CurPoolManager.GetObject("LaserShot", new Vector3(transform.position.x + 0.8f, transform.position.y, transform.position.z + 5), Quaternion.Euler(new Vector3(-90, 90, 0)));
                        MainSettings.CurPoolManager.GetObject("LaserShot", new Vector3(transform.position.x + 1, transform.position.y, transform.position.z + 5), Quaternion.Euler(new Vector3(-80, 90, 0)));
                        MainSettings.CurPoolManager.GetObject("LaserShot", new Vector3(transform.position.x - 1, transform.position.y, transform.position.z + 5), Quaternion.Euler(new Vector3(-100, 90, 0)));
                        MainSettings.CurPoolManager.GetObject("LaserShot", new Vector3(transform.position.x + 1, transform.position.y - 1, transform.position.z + 5), Quaternion.Euler(new Vector3(-70, 90, 0)));
                        MainSettings.CurPoolManager.GetObject("LaserShot", new Vector3(transform.position.x - 1, transform.position.y - 1, transform.position.z + 5), Quaternion.Euler(new Vector3(-110, 90, 0)));
                        break;
                    case 5:
                        MainSettings.CurPoolManager.GetObject("LaserShot", new Vector3(transform.position.x - 0.8f, transform.position.y, transform.position.z + 5), Quaternion.Euler(new Vector3(-90, 90, 0)));
                        MainSettings.CurPoolManager.GetObject("LaserShot", new Vector3(transform.position.x + 0.8f, transform.position.y, transform.position.z + 5), Quaternion.Euler(new Vector3(-90, 90, 0)));
                        MainSettings.CurPoolManager.GetObject("LaserShot", new Vector3(transform.position.x + 1, transform.position.y, transform.position.z + 5), Quaternion.Euler(new Vector3(-80, 90, 0)));
                        MainSettings.CurPoolManager.GetObject("LaserShot", new Vector3(transform.position.x - 1, transform.position.y, transform.position.z + 5), Quaternion.Euler(new Vector3(-100, 90, 0)));
                        MainSettings.CurPoolManager.GetObject("LaserShot", new Vector3(transform.position.x + 1, transform.position.y - 1, transform.position.z + 5), Quaternion.Euler(new Vector3(-70, 90, 0)));
                        MainSettings.CurPoolManager.GetObject("LaserShot", new Vector3(transform.position.x - 1, transform.position.y - 1, transform.position.z + 5), Quaternion.Euler(new Vector3(-110, 90, 0)));
                        MainSettings.CurPoolManager.GetObject("LaserShot", new Vector3(transform.position.x + 1, transform.position.y - 1, transform.position.z + 5), Quaternion.Euler(new Vector3(-60, 90, 0)));
                        MainSettings.CurPoolManager.GetObject("LaserShot", new Vector3(transform.position.x - 1, transform.position.y - 1, transform.position.z + 5), Quaternion.Euler(new Vector3(-120, 90, 0)));
                        break;
                }
            }
            yield return new WaitForSeconds(0.4f);
        }
    }
    #endregion

    private IEnumerator StartPlayer()
    {
        MainSettings.Players.UnLockController = true;
        transform.position = new Vector3(0, -16, -20);
        MainSettings.Players.Invulnerability = true;
        while (transform.position.y < -5)
        {
            while (!MainSettings.NotPause) { yield return null; }
            transform.position = Vector3.Lerp(transform.position, new Vector3(0, 16, -20), Time.deltaTime * 0.2f);
            yield return null;
        }
        MainSettings.Players.UnLockController = true;        
        MainSettings.Weapon.MainWeapon.Autoshot = true;
        yield return new WaitForSeconds(3f);
        for (int i = 0; i < 10; i++)
        {
            while (!MainSettings.NotPause) { yield return null; }
            MainSettings.Players.Invulnerability = !MainSettings.Players.Invulnerability;
            yield return new WaitForSeconds(0.2f);

        }
        MainSettings.Players.Invulnerability = false;
    }

    public void Bonus(AddBonusScript.BonusType bonuse)
    {
        switch (bonuse)
        {
            case AddBonusScript.BonusType.Life: MainSettings.CurPoolManager.GetObject("LifeUP", new Vector3(Random.Range(-20, 20), 15, 20), Quaternion.identity); break;
            case AddBonusScript.BonusType.MainWeapon: MainSettings.CurPoolManager.GetObject("LaserUP", new Vector3(Random.Range(-20, 20), 15, 20), Quaternion.identity); break;
            case AddBonusScript.BonusType.Rocket: MainSettings.CurPoolManager.GetObject("RocketUP", new Vector3(Random.Range(-20, 20), 15, 20), Quaternion.identity); break;
            case AddBonusScript.BonusType.Nuclear: MainSettings.CurPoolManager.GetObject("BombUP", new Vector3(Random.Range(-20, 20), 15, 20), Quaternion.identity); break;
        }
        
    }
}
