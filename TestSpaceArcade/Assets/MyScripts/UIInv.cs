using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInv : MonoBehaviour
{
    private void Awake()
    {
        MainSettings.Players.InvulnerabilityObject = gameObject;
    }
}
