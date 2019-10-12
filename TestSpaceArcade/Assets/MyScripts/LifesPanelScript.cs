using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifesPanelScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject LifesPrefab;
    public float WidthSize = 60;
    public float SpeedAnimation = 1;
    public Vector3 StartPosition;
    public Vector3 RotatePosition;
    public GameObject Explosion;

    private List<GameObject> lifes = new List<GameObject>();
    void Start()
    {
        MainSettings.Players.LifesPanel = gameObject.GetComponent<LifesPanelScript>();
        StartCoroutine(StartingAnim());
    }

    private IEnumerator StartingAnim()
    {
        Vector3 tmp = StartPosition;
        for (int i = 0; i < MainSettings.Players.LifeCount; i++)
        {
            while (!MainSettings.NotPause) { yield return null; }
            CreateLife(new Vector3(StartPosition.x + (WidthSize * i), StartPosition.y, StartPosition.z));
            yield return new WaitForSeconds(1f);
        }
    }


    public void ChangeLifeCount()
    {
        StartCoroutine(changeLifeCount());
    }

    private IEnumerator changeLifeCount()
    {
        Vector3 tmp = StartPosition;
        int i = 0;
        while (lifes.Count != MainSettings.Players.LifeCount)
        {
            while (!MainSettings.NotPause) { yield return null; }
            i = lifes.Count;
            if (lifes.Count < MainSettings.Players.LifeCount)
            {
                CreateLife(new Vector3(StartPosition.x + (WidthSize * i), StartPosition.y, StartPosition.z));
            }
            else
            {
                if (i > 0)
                {
                    GameObject needDelete = lifes[i - 1];
                    GameObject go = Instantiate(Explosion, needDelete.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                    go.transform.parent = gameObject.transform;
                    go.transform.position = needDelete.transform.position;
                    lifes.RemoveAt(i - 1);
                    Destroy(needDelete, 0);
                }
            }
            yield return null;
        }
    }

    private void CreateLife(Vector3 newPosition)
    {
        GameObject go = Instantiate(LifesPrefab, StartPosition, Quaternion.Euler(new Vector3(0, 0, 0)));
        go.transform.parent = gameObject.transform;
        LifesScript cmp = go.transform.GetComponent<LifesScript>();
        cmp.Position = newPosition;
        cmp.Rotation = RotatePosition;
        cmp.Speed = SpeedAnimation;
        lifes.Add(go);
        cmp.StartAnimation();
    }

}
