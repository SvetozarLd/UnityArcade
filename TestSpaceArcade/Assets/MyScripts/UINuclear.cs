using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UINuclear : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MainSettings.Weapon.Bomb.UIText = GetComponent<Text>();
        MainSettings.Weapon.Bomb.UIText.text = MainSettings.Weapon.Bomb.Count.ToString("00");
    }

}
