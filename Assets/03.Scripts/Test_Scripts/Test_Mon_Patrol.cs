using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonState
{
    mon_idle,
    mon_patrol
}

public class Test_Mon_Patrol : MonoBehaviour
{
    public MonState m_state;

    //private float m_Patrol_Time;

    private void Start() => StartFunc();

    private void StartFunc()
    {
        m_state = MonState.mon_idle;
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        //m_Patrol_Time = Random.Range(0.0f, 5.0f);
        //Debug.Log(m_Patrol_Time);
    }
}