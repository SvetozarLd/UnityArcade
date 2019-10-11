using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ScenarioScript : MonoBehaviour
{
    [Space]
    public GameObject Background;
    [Space]
    public GameObject LaserUP;
    public GameObject Bomb;
    public GameObject Life;
    [Space]
    public GameObject Enemy01;
    public GameObject Enemy02;
    public GameObject Enemy03;
    public GameObject Enemy04;
    public GameObject Enemy05;
    public GameObject Enemy06;
    [Space]

    private int gamePosition = 0;

    Dictionary<int, EnemyInstance> Checkpoints = new Dictionary<int, EnemyInstance>();

    public class EnemyInstance
    {
        public bool waiting { get; set; }
        public List<SingleEnemy> enemyObj { get; set; }
        public EnemyInstance(List<SingleEnemy> EnemyObj, bool Waiting) { enemyObj = EnemyObj; waiting = Waiting; }
    }

    public class SingleEnemy
    {
        public GameObject enemyObj { get; set; }
        public float timeDelay { get; set; }
        public int count { get; set; }
        public int trajectory { get; set; }
        public float speed { get; set; }
        public Vector3 selfPosition { get; set; }
        public SingleEnemy(GameObject EnemyObj, float TimeDelay, int Count, int Trajectory, float Speed, Vector3 SelfPosition) { selfPosition = SelfPosition; enemyObj = EnemyObj; timeDelay = TimeDelay; count = Count; trajectory = Trajectory; speed = Speed; }
    }

    void Start()
    {
        SingleEnemy en;
        List<SingleEnemy> lst;

        //#region Creating Scenario2
        //lst = new List<SingleEnemy>();
        //for (int i = 0; i < 10; i++)
        //{
        //    en = new SingleEnemy(Enemy01, 1f, 1, 0, 4, transform.position = new Vector3(Random.Range(-9, 10) * 3, 19, 0));
        //    lst.Add(en);
        //}
        //Checkpoints.Add(395, new EnemyInstance(lst, true));
        //#endregion


        #region Creating Scenario1
        lst = new List<SingleEnemy>();
        en = new SingleEnemy(Enemy02, 0.2f, 10, 1, 4, new Vector3(0, 15, -10));
        lst.Add(en);
        Checkpoints.Add(390, new EnemyInstance(lst, true));
        #endregion
        #region Creating Scenario1
        lst = new List<SingleEnemy>();
        en = new SingleEnemy(Enemy02, 0.2f, 10, 2, 4, new Vector3(-15, 15, -10));
        lst.Add(en);
        Checkpoints.Add(380, new EnemyInstance(lst, true));
        #endregion
        #region Creating Scenario1
        lst = new List<SingleEnemy>();
        en = new SingleEnemy(Enemy02, 0.2f, 10, 3, 4, new Vector3(15, 15, -5));
        lst.Add(en);
        Checkpoints.Add(370, new EnemyInstance(lst, true));
        #endregion

        #region Creating Scenario2

        lst = new List<SingleEnemy>();
        for (int i = -36; i < 36; i = i + 3)
        {
            en = new SingleEnemy(Enemy03, 0f, 1, 4, 1, new Vector3(i, 15, -10));
            lst.Add(en);
        }
        lst.Add(new SingleEnemy(Enemy04, 0f, 1, 4, 6, new Vector3(0, 15, -10)));
        lst.Add(new SingleEnemy(Enemy04, 0f, 1, 4, 7, new Vector3(0, 15, -10)));
        Checkpoints.Add(360, new EnemyInstance(lst, true));
        #endregion
        #region Creating Scenario2

        lst = new List<SingleEnemy>();
        lst.Add(new SingleEnemy(Enemy04, 0f, 1, 4, 6, new Vector3(0, 15, -10)));
        lst.Add(new SingleEnemy(Enemy04, 0f, 1, 4, 7, new Vector3(0, 15, -10)));
        Checkpoints.Add(361, new EnemyInstance(lst, true));
        #endregion

        //background = Background.gameObject.transform.Find("BackSpace01").gameObject;
        StartCoroutine(CheckPoints());

        //#region Creating Scenario2

        //lst = new List<SingleEnemy>();
        //for (int i = 0; i < 10; i++)
        //{
        //    en = new SingleEnemy(Enemy01, 1f, 1, 0, 4, transform.position = new Vector3(Random.Range(-9, 10) * 3, 19, 0));
        //    lst.Add(en);
        //}
        //Checkpoints.Add(350, new EnemyInstance(lst, true));
        //#endregion


        //background = Background.gameObject.transform.Find("BackSpace01").gameObject;
        StartCoroutine(CheckPoints());
        MainSettings.NotPause = true;
        MainSettings.Players.Autoshot = true;
        //if (!MainSettings.Players.Autoshot)
        //{
        //    MainSettings.Players.Autoshot = true;
        //    StartCoroutine(LaserShot());
        //}
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        SingleEnemy en = new SingleEnemy(Enemy01, 0.15f, 10, 0, 1);
    //        StartCoroutine(CreateEnemy(en));
    //    }

    //    //if (gamePosition == 390)
    //    //{
    //    //    StartCoroutine(CreateEnemy(Enemy01, 0.1f, 10, 0));
    //    //}
    //}

    private float UpgradeTmp = 2;
    private int laserUpgradesCount = 0;
    private IEnumerator CheckPoints()
    {
        while (true)
        {
            if (MainSettings.NotPause)
            {
                gamePosition = Mathf.RoundToInt(Background.transform.position.y);
                //Debug.Log(gamePosition);
                EnemyInstance item;
                Checkpoints.TryGetValue(gamePosition, out item);
                if (item != null && item.waiting)
                {
                    item.waiting = false;
                    foreach (SingleEnemy en in item.enemyObj)
                    {
                        StartCoroutine(CreateEnemy(en));
                    }
                }
                UpgradeTmp = MainSettings.Players.Scores / 100;
                //Debug.Log(MainSettings.Players.Scores + ":"+ TakeUpgrade.ToString() + ":"+TakeUpgradeTmp.ToString());
                if (UpgradeTmp > laserUpgradesCount)
                {
                    GameObject go = Instantiate(LaserUP, new Vector3(Random.Range(-20, 20), 15, 20), Quaternion.Euler(0, 0, 0));
                    laserUpgradesCount++;
                }
            }
            yield return null;
        }
    }

    private IEnumerator CreateEnemy(SingleEnemy enemy)
    {
        EnemyTrajectory component = null;
        for (int i = 0; i < enemy.count; i++)
        {
            while (!MainSettings.NotPause) { yield return null; }
            GameObject go = Instantiate(enemy.enemyObj, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
            component = go.GetComponent<EnemyTrajectory>();
            if (component != null)
            {
                component.Trajectory = enemy.trajectory;
                component.speed = enemy.speed;
                go.transform.position = enemy.selfPosition;
                MainSettings.Enemylist.Add(go);
                yield return new WaitForSeconds(enemy.timeDelay);
            }
            else
            {
                MainSettings.Enemylist.Remove(go);
                Destroy(go, 0);
                yield break;
            }
        }
    }

}
