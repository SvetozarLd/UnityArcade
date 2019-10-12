using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GuiStats
{
    //public static GameObject Life;
    private static List<GameObject> GoLifes;
    private static LifeObjects lifeObjects;
    public static GameObject PanelLifes;
    static GuiStats()
    {
        lifeObjects = new LifeObjects();
        GoLifes = new List<GameObject>();
        for (int i =0; i<= MainSettings.Players.MaxLifeCount; i++)
        {
            GameObject go= lifeObjects.GetNewPrebaf();
            GoLifes.Add(go);
        }
        //if (positions[] == null)
        //{
        //    //positions = new Vector3[5]
        //    //{
        //    //new Vector3(-70,-25,0),
        //    //new Vector3(10,-25,0),
        //    //new Vector3(50,-25,0),
        //    //new Vector3(110,-25,0),
        //    //new Vector3(170,-25,0)
        //    //};
        //}




    }

    public static void Score()
    {
        MainSettings.myUI.Scores.text = "Scores: " + MainSettings.Players.Scores.ToString();
    }

    public static void Lifes(bool adding)
    {
        if (adding)
        {
            for (int i = 0; i <= MainSettings.Players.LifeCount, i++)
            {
                //if (lifes[i] == null)
                //{
                //    //GameObject go = Int
                //    //PanelLifes
                //}
            }


        }
        else
        {


        }

        //if (adding && MainSettings.Players.LifeCount<5) { MainSettings.Players.LifeCount++; } else {
        //    if ()
        //}
        //switch (MainSettings.Players.LifeCount)
        //{
        //    case 0:

        //        break;
        //}
    }

    public static void Rockets()
    {

    }

    public static void Bombs()
    {

    }
}

public class LifeObjects : MonoBehaviour
{
    public GameObject LifePrebaf;

    public GameObject GetNewPrebaf()
    {
        GameObject go = Instantiate(LifePrebaf, new Vector3(transform.position.x - 0.8f, transform.position.y, transform.position.z + 5), Quaternion.Euler(new Vector3(-90, 90, 0)));
        return go;
    }
}
