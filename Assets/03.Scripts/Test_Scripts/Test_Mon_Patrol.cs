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

    private float m_Patrol_Time;

    private void Start() => StartFunc();

    private void StartFunc()
    {
        m_state = MonState.mon_idle;
        m_Patrol_Time = Random.Range(1.0f, 2.0f);
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        //m_Patrol_Time = Random.Range(0.0f, 5.0f);
        //Debug.Log(m_Patrol_Time);

        if(m_state == MonState.mon_idle)
        {
            m_Patrol_Time -= Time.deltaTime;
            Debug.Log("Idle");
            if(m_Patrol_Time <= 0.0f)
            {
                m_state = MonState.mon_patrol;
                m_Patrol_Time = Random.Range(2.0f, 3.0f);
            }
        }
        else if(m_state == MonState.mon_patrol)
        {
            if(m_Patrol_Time >= 0.0f)
            {
                m_Patrol_Time -= Time.deltaTime;
                Debug.Log("Patrol");

                if(m_Patrol_Time < 0.0f)
                {
                    m_state = MonState.mon_idle;
                    m_Patrol_Time = Random.Range(2.0f, 3.0f);
                }
            }

        }
    }
}