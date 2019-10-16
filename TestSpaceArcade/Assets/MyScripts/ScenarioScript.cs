using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ScenarioScript : MonoBehaviour
{
    [Space]
    public GameObject Background;
    [Space]
    public GameObject Enemy01;
    public GameObject Enemy02;
    public GameObject Enemy03;
    public GameObject Enemy04;
    public GameObject Enemy05;
    public GameObject Enemy06;
    public GameObject Enemy07;
    public GameObject Enemy08;
    [Space]
    public int EndGameCheckpoint;

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

        #region Creating Scenario (sin center)
        lst = new List<SingleEnemy>();
        en = new SingleEnemy(Enemy02, 0.2f, 10, 1, 4, new Vector3(0, 15, -10));
        lst.Add(en);
        Checkpoints.Add(390, new EnemyInstance(lst, true));
        #endregion

        #region Creating Scenario (droids)
        lst = new List<SingleEnemy>();
        lst.Add(new SingleEnemy(Enemy04, 0f, 1, 6, 1, new Vector3(0, 15, -10)));
        lst.Add(new SingleEnemy(Enemy04, 0f, 1, 7, 1, new Vector3(0, 15, -10)));
        lst.Add(en);
        Checkpoints.Add(385, new EnemyInstance(lst, true));
        #endregion

        #region Creating Scenario (sin left)
        lst = new List<SingleEnemy>();
        en = new SingleEnemy(Enemy02, 0.2f, 10, 2, 4, new Vector3(-15, 15, -10));
        lst.Add(en);
        Checkpoints.Add(380, new EnemyInstance(lst, true));
        #endregion

        #region Creating Scenario (sin right)
        lst = new List<SingleEnemy>();
        en = new SingleEnemy(Enemy02, 0.2f, 10, 3, 4, new Vector3(15, 15, -5));
        lst.Add(en);
        Checkpoints.Add(370, new EnemyInstance(lst, true));
        #endregion

        #region Creating Scenario (bezie01 Left)
        lst = new List<SingleEnemy>();
        en = new SingleEnemy(Enemy06, 0.25f, 20, 10, 4, new Vector3(-30, 15, -5));
        lst.Add(en);
        Checkpoints.Add(360, new EnemyInstance(lst, true));
        #endregion

        #region Creating Scenario (bezie01 right)
        lst = new List<SingleEnemy>();
        en = new SingleEnemy(Enemy07, 0.25f, 20, 10, 4, new Vector3(30, 15, -5));
        lst.Add(en);
        Checkpoints.Add(350, new EnemyInstance(lst, true));
        #endregion

        #region Creating Scenario (Wall)

        lst = new List<SingleEnemy>();
        for (int i = -36; i < 36; i = i + 3)
        {
            en = new SingleEnemy(Enemy03, 0f, 1, 4, 1, new Vector3(i, 15, -10));
            lst.Add(en);
        }
        Checkpoints.Add(340, new EnemyInstance(lst, true));
        #endregion
        #region Creating Scenario (droids)

        lst = new List<SingleEnemy>();
        //lst.Add(new SingleEnemy(Enemy04, 0f, 1, 6, 1, new Vector3(0, 15, -10)));
        //lst.Add(new SingleEnemy(Enemy04, 0f, 1, 7, 1, new Vector3(0, 15, -10)));
        lst.Add(new SingleEnemy(Enemy05, 0f, 1, 6, 1, new Vector3(0, 15, -10)));
        lst.Add(new SingleEnemy(Enemy05, 0f, 1, 7, 1, new Vector3(0, 15, -10)));
        Checkpoints.Add(338, new EnemyInstance(lst, true));
        #endregion
        #region Creating Scenario (droids)

        lst = new List<SingleEnemy>();
        //lst.Add(new SingleEnemy(Enemy04, 0f, 1, 6, 1, new Vector3(0, 15, -10)));
        //lst.Add(new SingleEnemy(Enemy04, 0f, 1, 7, 1, new Vector3(0, 15, -10)));
        lst.Add(new SingleEnemy(Enemy08, 0f, 1, 10, 1, new Vector3(0, 15, -10)));
        Checkpoints.Add(325, new EnemyInstance(lst, true));
        #endregion

        StartCoroutine(CheckPoints());
        StartCoroutine(CheckPoints());
        MainSettings.NotPause = true;
    }

    private IEnumerator CheckPoints()
    {
        while (true)
        {
            if (MainSettings.NotPause)
            {
                gamePosition = Mathf.RoundToInt(Background.transform.position.y);
                if (gamePosition > EndGameCheckpoint)
                {
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
                }
                else
                {
                    MainSettings.Lifes.Count = -1;
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
            GameObject go = MainSettings.CurPoolManager.GetObject(enemy.enemyObj.name, enemy.selfPosition, Quaternion.Euler(0, 0, 0));
            if (go != null)
            {
                component = go.transform.GetComponent<EnemyTrajectory>();
                if (component != null)
                {
                    component.Trajectory = enemy.trajectory;
                    component.speed = enemy.speed;
                    MainSettings.Enemylist.Add(go);
                    yield return new WaitForSeconds(enemy.timeDelay);
                }
                else
                {
                    MainSettings.Enemylist.Remove(go);
                    go.GetComponent<PoolObject>().ReturnToPool();
                    yield break;
                }
            }
        }
    }
}
