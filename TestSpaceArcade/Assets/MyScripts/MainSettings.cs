using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class MainSettings
{
    static MainSettings()
    {
        SetDefaultSettings();
    }

    public static GameObject PausePanel { get; set; }
    public static UIBossHP BossUIHPPanel { get; set; }
    private static bool notPause = false;
    public static PoolManager CurPoolManager { get; set; }

    public static bool NotPause
    {
        get => notPause;
        set
        {
            notPause = value;
            if (PausePanel != null)
            {
                if (notPause) { PausePanel.SetActive(false); } else { PausePanel.SetActive(true); }
            }
        }
    }



    public static List<GameObject> Enemylist = new List<GameObject>();
    public static Camera MainCamera { get; set; }



    public static class Players
    {
        public static GameObject Player { get; set; }
        public static int HitPoint = 0;
        public static int Damage = 1000;
        public static bool UnLockController = false;
        public static GameObject InvulnerabilityObject { get; set; }
        private static bool invulnerability = false;
        public static bool Invulnerability
        {
            get => invulnerability;
            set { invulnerability = value; if (InvulnerabilityObject != null) { InvulnerabilityObject.SetActive(value); } }
        }
    }

    #region Scores
    public static class Score
    {
        public static Text UIText { get; set; }
        private static long scores { get; set; }
        public static long Scores
        {
            get => scores;
            set
            {
                BonusCheck((int)(value - scores));
                scores = value;
                if (UIText != null) { UIText.text = "Scores:" + scores.ToString(); }                
            }
        }

        static private void BonusCheck(int tmp)
        {
            BonusLife += tmp;
            BonusMainWeapon += tmp;
            BonusRocket += tmp;
            BonusBomb += tmp;
            if (BonusLife > Lifes.CurrentBonus) { BonusLife -= Lifes.CurrentBonus; Players.Player.GetComponent<PlayerController>().Bonus(AddBonusScript.BonusType.Life); }
            if (BonusMainWeapon > Weapon.MainWeapon.CurrentBonus) { BonusMainWeapon -= Weapon.MainWeapon.CurrentBonus; Players.Player.GetComponent<PlayerController>().Bonus(AddBonusScript.BonusType.MainWeapon); }
            if (BonusRocket > Weapon.Rocket.CurrentBonus) { BonusRocket -= Weapon.Rocket.CurrentBonus; Players.Player.GetComponent<PlayerController>().Bonus(AddBonusScript.BonusType.Rocket); }
            if (BonusBomb > Weapon.Bomb.CurrentBonus) { BonusBomb -= Weapon.Bomb.CurrentBonus; Players.Player.GetComponent<PlayerController>().Bonus(AddBonusScript.BonusType.Nuclear); }
        }
        static public int BonusLife = 0;
        static public int BonusMainWeapon = 0;
        static public int BonusRocket = 0;
        static public int BonusBomb = 0;
    }
    #endregion

    #region Lifes
    public static class Lifes
    {
        public static LifesPanelScript UIPanel { get; set; }
        public static int MaxCount { get; set; }
        private static int count = 5;
        public static int Count
        {
            get => count;
            set
            {
                if (value <= MaxCount) { count = value; CurrentBonus = (int)(count * ScoresBonus * ScoresBonusRatio) + ScoresBonus; }
                if (UIPanel != null) { UIPanel.ChangeLifeCount(); }
            }
        }
        public static int ScoresBonus = 100;
        public static float ScoresBonusRatio = 2f;
        public static int CurrentBonus = 0;
    }
    #endregion

    #region Weapon
    public static class Weapon
    {
        #region MainWeapon
        public static class MainWeapon
        {
            public static bool Autoshot { get; set; }
            private static int power = 0;
            public static int Damage = 5;
            public static int Power
            {
                get { return power; }
                set
                {
                    if (value >= 0 && value < 6)
                    {
                        power = value;
                        CurrentBonus = (int)(power * ScoresBonus * ScoresBonusRatio) + ScoresBonus;
                    }
                }
            }

            public static int ScoresBonus = 50;
            public static float ScoresBonusRatio = 1.5f;
            public static int CurrentBonus = 0;
        }
        #endregion
        #region Rockets
        public static class Rocket
        {
            public static int Damage = 100;
            public static float Speed = 15f;
            public static Text UIText { get; set; }
            public static int MaxCount { get; set; }
            private static int count = 0;
            public static int Count
            {
                get => count;
                set
                {
                    if (value <= MaxCount && value >= 0) { count = value; CurrentBonus = (int)(count * ScoresBonus * ScoresBonusRatio) + ScoresBonus ; }
                    if (UIText != null) { UIText.text = count.ToString("00"); }
                }

            }

            public static int ScoresBonus = 150;
            public static float ScoresBonusRatio = 1f;
            public static int CurrentBonus = 0;
        }
        #endregion
        #region Nuclear
        public static class Bomb
        {
            public static int Damage = 500;
            public static float Diameter = 10;
            public static float Speed = 10f;
            public static float Size = 0.4f;

            public static Text UIText { get; set; }
            public static int MaxCount { get; set; }
            private static int count = 0;
            public static int Count
            {
                get => count;
                set
                {
                    if (value <= MaxCount && value >= 0) { count = value; CurrentBonus = (int)(count * ScoresBonus * ScoresBonusRatio) + ScoresBonus; }
                    if (UIText != null) { UIText.text = count.ToString("00"); }
                }

            }

            public static int ScoresBonus = 200;
            public static float ScoresBonusRatio = 1.5f;
            public static int CurrentBonus = 0;
        }
        #endregion
    }
    #endregion
    #region Reset
    public static void SetDefaultSettings()
    {
        notPause = false;
        PausePanel = null;
        BossUIHPPanel = null;
        CurPoolManager = null;
        CurPoolManager = new PoolManager();
        if (Enemylist != null) { Enemylist.Clear(); }
        Enemylist = new List<GameObject>();
        MainCamera = null;

        Players.Player = null;
        Players.HitPoint = 0;
        Players.Damage = 1000;
        Players.UnLockController = false;
        Players.InvulnerabilityObject = null;
        Players.Invulnerability = true;


        Weapon.MainWeapon.Autoshot = false;
        Weapon.MainWeapon.Damage = 5;
        Weapon.MainWeapon.Power = 0;

        Lifes.UIPanel = null;
        Lifes.MaxCount = 10;
        Lifes.Count = 5;


        Score.UIText = null;
        Score.Scores = 0;
        Score.BonusLife = 0;
        Score.BonusMainWeapon = 0;
        Score.BonusRocket = 0;
        Score.BonusBomb = 0;

        Weapon.Rocket.UIText = null;
        Weapon.Rocket.MaxCount = 10;
        Weapon.Rocket.Count = 5;

        Weapon.Bomb.UIText = null;
        Weapon.Bomb.MaxCount = 5;
        Weapon.Bomb.Count = 1;
    }
    #endregion
}
