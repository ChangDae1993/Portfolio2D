using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalData : MonoBehaviour
{
    public static int hpPotionNum = 10;

    public static float playerExp;


    private void Start() => StartFunc();
    private void StartFunc()
    {
        hpPotionNum = 5;
        playerExp = 0.0f;
    }

    private void Update() => UpdateFunc();
    private void UpdateFunc()
    {
        
    }
}