using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
static public class MainSettings
{
    static public bool NotPause { get; set; }
    static public List<GameObject> Enemylist = new List<GameObject>();
    static public Camera MainCamera { get; set; }

    static public class myUI
    {
        static public Text Scores { get; set; }
        static public Text Stats { get; set; }
    }

    static public class Players
    {
        static public GameObject Player {get;set;}
        static public bool Autoshot { get; set; }
        static public int LifeCount { get; set; }
        static public long Scores { get; set; }
        static public float HitPoint { get; set; }
        static public int LaserPower { get; set; }
    }

    static public class Weapon
    {
        static public class Rocket
        {
            static public int Count { get; set; }
            static public float Damage { get; set; }
            static public float Speed { get; set; }
        }
        static public class Bomb
        {

            static public int Count { get; set; }
            static public float Damage { get; set; }
            private static float diameter = 10;
            static public float Diameter
            {
                get { return diameter; }
                set { diameter = value; }
            }
            private static float speed = 10;
            static public float Speed
            {
                get { return speed; }
                set { speed = value; }
            }
            private static float size = 0.4f;
            static public float Size
            {
                get { return size; }
                set { size = value; }
            }
        }
        static public class GuiSet
        {
            static public int LifeCount { get; set; }
        }
    }
}
