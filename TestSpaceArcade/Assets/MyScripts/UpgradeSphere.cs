using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UpgradeSphere : MonoBehaviour
{
    public float speedX;
    public float speedY;
    private Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
    }
    float offsetX = 0;
    float offsetY = 0;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (MainSettings.NotPause)
        {
            offsetX += speedX;
            offsetY += speedY;
            if (offsetX > 1) { offsetX -= 1; } else { if (offsetX < -1) { offsetX += 1; } }
            if (offsetY > 1) { offsetY -= 1; } else { if (offsetY < -1) { offsetY += 1; } }
            rend.material.SetTextureOffset("_MainTex", new Vector2(offsetX, offsetY));
        }
    }
}
