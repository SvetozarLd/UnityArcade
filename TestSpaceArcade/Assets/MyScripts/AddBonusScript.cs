using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBonusScript : MonoBehaviour
{
    public enum BonusType
    {
        Life,
        MainWeapon,
        Rocket,
        Nuclear
    }
    public BonusType Bonus;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag.Equals(MainSettings.Players.Player.tag))
        {
            switch (Bonus)
            {
                case BonusType.Life: MainSettings.Players.LifeCount++; Destroy(gameObject.transform.parent.gameObject, 0f); break;
                case BonusType.MainWeapon: MainSettings.Players.LaserPower++; Destroy(gameObject.transform.parent.gameObject, 0f); break;
                case BonusType.Rocket: MainSettings.Players.RocketCount++; Destroy(gameObject.transform.parent.gameObject, 0f); break;
                case BonusType.Nuclear: MainSettings.Players.NuclearCount++; Destroy(gameObject.transform.parent.gameObject, 0f); break;
            }
        }        
    }
}
