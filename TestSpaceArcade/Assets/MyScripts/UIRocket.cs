using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIRocket : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MainSettings.Players.RocketText = GetComponent<Text>();
        MainSettings.Players.RocketText.text = MainSettings.Players.RocketCount.ToString("00");
    }
}
