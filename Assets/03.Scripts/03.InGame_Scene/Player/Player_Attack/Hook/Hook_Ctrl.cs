using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook_Ctrl : MonoBehaviour
{
    private Hook_Aim_Ctrl aim_Ctrl;
    private float hook_speed;
    private float hook_backSpeed;
    //private Vector3 target_Vec;
    //private float target_Dist;
    private Transform player_Pos;
    private float hook_back_Dist;

    private void Start() => StartFunc();

    private void StartFunc()
    {
        aim_Ctrl = GameObject.FindGameObjectWithTag("Player").GetComponent<Hook_Aim_Ctrl>();
        hook_speed = 10.0f;
        hook_backSpeed = 10.0f;
        player_Pos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        hook_back_Dist = 0.0f;
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        if(aim_Ctrl.isTouch == false)
        {
            Hook_Release();
        }
        else
        {
            Hook_Return();
        }
    }

    private void Hook_Release()
    {
        //aim_Ctrl.hook_Target_Pos를 목적지로 본인 위치부터 hook_Target_Pos까지 이동
        this.transform.position = Vector3.Lerp(this.transform.position, aim_Ctrl.hook_Target_Pos, Time.deltaTime * hook_speed);
    }

    private void Hook_Return()
    {
        Debug.Log("back");
        this.transform.position = Vector3.Lerp(this.transform.position, player_Pos.position, Time.deltaTime * hook_backSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("HOOK_AIM"))
        {
            aim_Ctrl.isTouch = true;
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("PLAYER"))
        {
            Destroy(this.gameObject);
        }
    }


}