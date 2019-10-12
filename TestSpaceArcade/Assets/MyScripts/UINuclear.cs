using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UINuclear : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MainSettings.Players.NuclearText = GetComponent<Text>();
        MainSettings.Players.NuclearText.text = MainSettings.Players.NuclearCount.ToString("00");
    }

}
