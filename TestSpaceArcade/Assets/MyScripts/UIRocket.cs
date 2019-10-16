using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIRocket : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MainSettings.Weapon.Rocket.UIText = GetComponent<Text>();
        MainSettings.Weapon.Rocket.UIText.text = MainSettings.Weapon.Rocket.Count.ToString("00");
    }
}
