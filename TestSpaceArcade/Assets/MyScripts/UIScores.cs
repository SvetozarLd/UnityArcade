using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScores : MonoBehaviour
{
    private void Start()
    {
        MainSettings.Score.UIText = GetComponent<Text>();
        MainSettings.Score.UIText.text = MainSettings.Score.Scores.ToString();
    }
}
