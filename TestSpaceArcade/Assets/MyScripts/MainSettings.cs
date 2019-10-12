using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

static public class MainSettings
{
    static public GameObject PausePanel { get; set; }
    static private bool notPause = false;
    static public bool NotPause
    {
        get { return notPause; }
        set
        {
            notPause = value;
            if (PausePanel != null)
            {
                if (notPause) { PausePanel.SetActive(false); } else { PausePanel.SetActive(true); }
            }
        }
    }



    static public List<GameObject> Enemylist = new List<GameObject>();
    static public Camera MainCamera { get; set; }



    static public class Players
    {
        static Players()
        {
            if (MaxLifeCount == 0) { MaxLifeCount = 10; }
            if (MaxRocketCount == 0) { MaxRocketCount = 10; }
            if (MaxNuclearCount == 0) { MaxNuclearCount = 5; }
        }

        static public GameObject Player { get; set; }
        static public float HitPoint { get; set; }

        static public GameObject InvulnerabilityObject { get; set; }
        static private bool invulnerability = false;
        static public bool Invulnerability
        {
            get { return invulnerability; }
            set
            {
                invulnerability = value;
                if (InvulnerabilityObject != null)
                    if (invulnerability)
                    {
                        InvulnerabilityObject.SetActive(true);
                        //Player.transform.GetChild(0).gameObject.SetActive(true);
                    }
                    else
                    {
                        InvulnerabilityObject.SetActive(false);
                        //Player.transform.GetChild(0).gameObject.SetActive(false);
                    }
            }
        }


        #region MainWeapon
        static public bool Autoshot { get; set; }
        static private int laserPower = 0;
        static public int LaserPower
        {
            get { return laserPower; }
            set { if (value >= 0 && value < 7) { laserPower = value; } }
        }
        #endregion

        #region Lifes
        static public LifesPanelScript LifesPanel { get; set; }
        static public int MaxLifeCount { get; set; }
        static private int lifeCount = 5;
        static public int LifeCount
        {
            get { return lifeCount; }
            set
            {
                if (value <= MaxLifeCount) { lifeCount = value; }
                if (LifesPanel != null) { LifesPanel.ChangeLifeCount(); }
            }

        }
        #endregion

        #region Scores
        static public Text ScoresText { get; set; }
        static private long scores { get; set; }
        static public long Scores
        {
            get { return scores; }
            set
            {
                scores = value;
                if (ScoresText != null) { ScoresText.text = "Scores:" + scores.ToString(); }
            }
        }
        #endregion

        #region Rockets
        static public Text RocketText { get; set; }
        static public int MaxRocketCount { get; set; }
        static private int rocketCount = 0;
        static public int RocketCount
        {
            get { return rocketCount; }
            set
            {
                if (value <= MaxRocketCount && value >= 0) { rocketCount = value; }
                if (RocketText != null) { RocketText.text = rocketCount.ToString("00"); }
            }

        }
        #endregion

        #region Nuclear
        static public Text NuclearText { get; set; }
        static public int MaxNuclearCount { get; set; }
        static private int nuclearCount = 0;
        static public int NuclearCount
        {
            get { return nuclearCount; }
            set
            {
                if (value <= MaxNuclearCount && value >= 0) { nuclearCount = value; }
                if (NuclearText != null) { NuclearText.text = nuclearCount.ToString("00"); }
            }

        }
        #endregion

    }

    static public class Weapon
    {
        static public class Rocket
        {

            static public int MaxCount { get; set; }
            static public int Count { get; set; }
            static public float Damage { get; set; }
            static public float Speed { get; set; }
        }
        static public class Bomb
        {
            static public bool Infinity { get; set; }
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
