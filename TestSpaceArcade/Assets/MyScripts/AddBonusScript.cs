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
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag.Equals(MainSettings.Players.Player.tag))
        {
            switch (Bonus)
            {
                case BonusType.Life: MainSettings.Lifes.Count++; transform.parent.GetComponent<PoolObject>().ReturnToPool(); break;
                case BonusType.MainWeapon: MainSettings.Weapon.MainWeapon.Power++; transform.parent.GetComponent<PoolObject>().ReturnToPool(); break;
                case BonusType.Rocket: MainSettings.Weapon.Rocket.Count++; transform.parent.GetComponent<PoolObject>().ReturnToPool(); break;
                case BonusType.Nuclear: MainSettings.Weapon.Bomb.Count++; transform.parent.GetComponent<PoolObject>().ReturnToPool(); break;
            }
        }        
    }
}
