using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalData : MonoBehaviour
{
    public static int hpPotionNum = 10;

    public static float playerExp;

    public static int stage_Progress = 0;


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