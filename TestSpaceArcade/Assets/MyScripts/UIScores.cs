using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScores : MonoBehaviour
{
    private void Awake()
    {
        MainSettings.myUI.Scores = GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        MainSettings.myUI.Scores.text = "Scores: " + MainSettings.Players.Scores.ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //GetComponent<Text>().text = "Scores: " + MainSettings.Players.Scores.ToString();
    }
}
