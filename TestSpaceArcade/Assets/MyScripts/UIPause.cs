using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPause : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MainSettings.PausePanel = gameObject;
    }
}
