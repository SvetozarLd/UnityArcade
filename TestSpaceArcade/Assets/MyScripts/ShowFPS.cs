using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShowFPS : MonoBehaviour
{
    Text fpsText;
    public float deltaTime;
    // Start is called before the first frame update
    void Awake()
    {
        fpsText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = "FPS:"+Mathf.Ceil(fps).ToString();
    }
}
