using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook_Ctrl : MonoBehaviour
{
    private Hook_Aim_Ctrl aim_Ctrl;
    private float hook_speed;
    private Vector3 target_Vec;
    private float target_Dist;
    private Transform player_Pos;

    private void Start() => StartFunc();

    private void StartFunc()
    {
        aim_Ctrl = GameObject.FindGameObjectWithTag("Player").GetComponent<Hook_Aim_Ctrl>();
        hook_speed = 6.0f;
        player_Pos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        //aim_Ctrl.hook_Target_Pos�� �������� ���� ��ġ���� hook_Target_Pos���� �̵�
        this.transform.position = Vector3.Lerp(this.transform.position, aim_Ctrl.hook_Target_Pos, Time.deltaTime * hook_speed);
        
        //aim�̶� ���� �Ÿ� ����� ���� �� ���ƿ��� ����
        //target_Vec = aim_Ctrl.hook_Target_Pos - this.transform.position;
        //target_Dist = Vector3.Magnitude(target_Vec);

        //if (0.5f <= target_Dist)
        //{
        //    this.transform.position = Vector3.Lerp(this.transform.position, player_Pos.position, Time.deltaTime * hook_speed);
        //}
    }
}