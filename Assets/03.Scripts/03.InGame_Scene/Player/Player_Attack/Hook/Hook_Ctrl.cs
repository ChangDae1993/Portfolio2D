using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook_Ctrl : MonoBehaviour
{
    private Hook_Aim_Ctrl aim_Ctrl;
    private float hook_speed;
    //private Vector3 target_Vec;
    //private float target_Dist;
    private Transform player_Pos;
    private float hook_back_Dist;

    private void Start() => StartFunc();

    private void StartFunc()
    {
        aim_Ctrl = GameObject.FindGameObjectWithTag("Player").GetComponent<Hook_Aim_Ctrl>();
        hook_speed = 6.0f;
        player_Pos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        hook_back_Dist = 0.0f;
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {

        //aim_Ctrl.hook_Target_Pos를 목적지로 본인 위치부터 hook_Target_Pos까지 이동
        this.transform.position = Vector3.Lerp(this.transform.position, aim_Ctrl.hook_Target_Pos, Time.deltaTime * hook_speed);
        hook_back_Dist = Mathf.Abs(this.transform.position.x - aim_Ctrl.hook_Target_Pos.x);
        //aim이랑 일정 거리 가까워 졌을 때 돌아오기 구현
        //target_Vec = aim_Ctrl.hook_Target_Pos - this.transform.position;
        //target_Dist = Vector3.Magnitude(target_Vec);

        if (this.transform.position == aim_Ctrl.hook_Target_Pos)
        {
            Hook_Return();
        }
        //if (0.5f <= target_Dist)
        //{
        //    this.transform.position = Vector3.Lerp(this.transform.position, player_Pos.position, Time.deltaTime * hook_speed);
        //}

    }

    private void Hook_Return()
    {
        Debug.Log("back");
        this.transform.position = Vector3.Lerp(this.transform.position, player_Pos.position, Time.deltaTime * hook_speed);
    }


}