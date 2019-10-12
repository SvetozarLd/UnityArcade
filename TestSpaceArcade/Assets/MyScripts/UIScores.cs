using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScores : MonoBehaviour
{
    private void Start()
    {
        MainSettings.Players.ScoresText = GetComponent<Text>();
        MainSettings.Players.ScoresText.text = MainSettings.Players.Scores.ToString();
    }
}
