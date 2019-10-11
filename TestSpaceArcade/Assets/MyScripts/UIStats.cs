using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIStats : MonoBehaviour
{
    private void Awake()
    {
        MainSettings.myUI.Stats = GetComponent<Text>();
    }
}
