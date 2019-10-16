using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIBossHP : MonoBehaviour
{

    RectTransform bossRed;
    RectTransform bossGray;
    float panelSize;
    public int Maxim;
    public int Current;
    private void Awake()
    {
        bossRed = transform.GetChild(0).gameObject.GetComponent<RectTransform>();
        bossGray = transform.GetChild(1).gameObject.GetComponent<RectTransform>();
        panelSize = gameObject.transform.GetComponent<RectTransform>().sizeDelta.x-40;
    }
    private void OnEnable()
    {
        bossRed.sizeDelta = new Vector2(panelSize, bossRed.sizeDelta.y);
        bossGray.sizeDelta = new Vector2(0, bossRed.sizeDelta.y);
    }
    private void Start()
    {
        MainSettings.BossUIHPPanel = gameObject.GetComponent<UIBossHP>();
        gameObject.SetActive(false);
    }
    public void SetHP(int Max, int Cur)
    {
        float x = ((float)Cur / Max) * panelSize;
        if (x < 0) { x = 0; }
        bossRed.sizeDelta = new Vector2(x, bossRed.sizeDelta.y);
        bossGray.sizeDelta = new Vector2(panelSize - x, bossRed.sizeDelta.y);
        if (x == 0) { StartCoroutine(ToWin()); }
    }
    IEnumerator ToWin()
    {
        yield return new WaitForSeconds(2f);
        MainSettings.NotPause = false;
        MainSettings.PausePanel.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "You win!\r\n Press ESC to restart level";
        MainSettings.PausePanel.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().resizeTextForBestFit = true;
        MainSettings.Players.Player.GetComponent<PlayerController>().looser = true;
    }
}
