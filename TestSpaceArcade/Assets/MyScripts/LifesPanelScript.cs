using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifesPanelScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float WidthSize = 60;
    public float SpeedAnimation = 1;
    public Vector3 StartPosition;
    public Vector3 RotatePosition;

    private List<GameObject> lifes = new List<GameObject>();
    void Start()
    {
        MainSettings.Lifes.UIPanel = gameObject.GetComponent<LifesPanelScript>();
        StartCoroutine(StartingAnim());
    }

    private IEnumerator StartingAnim()
    {
        Vector3 tmp = StartPosition;
        for (int i = 0; i < MainSettings.Lifes.Count; i++)
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
            while (lifes.Count != MainSettings.Lifes.Count)
            {
                while (!MainSettings.NotPause) { yield return null; }
                i = lifes.Count;
                if (lifes.Count < MainSettings.Lifes.Count)
                {
                    CreateLife(new Vector3(StartPosition.x + (WidthSize * i), StartPosition.y, StartPosition.z));
                }
                else
                {
                    if (i > 0)
                    {
                        GameObject needDelete = lifes[i - 1];
                        GameObject go = MainSettings.CurPoolManager.GetObject("ExplosionSmallUI", StartPosition, Quaternion.identity);
                        go.transform.parent = gameObject.transform;
                        go.transform.position = needDelete.transform.position;
                        lifes.RemoveAt(i - 1);
                        needDelete.transform.GetComponent<PoolObject>().ReturnToPool();
                    }
                }
                yield return null;
            }
    }

    private void CreateLife(Vector3 newPosition)
    {
        GameObject go = MainSettings.CurPoolManager.GetObject("LifePrefab", StartPosition, Quaternion.Euler(new Vector3(0, 0, 0)));
        if (go != null)
        {
            go.transform.parent = gameObject.transform;
            LifesScript cmp = go.transform.GetComponent<LifesScript>();
            cmp.Position = newPosition;
            cmp.Rotation = RotatePosition;
            cmp.Speed = SpeedAnimation;
            lifes.Add(go);
            cmp.StartAnimation();
        }
    }

}
