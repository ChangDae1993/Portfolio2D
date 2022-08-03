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
    private LineRenderer line_Renderer;
    Vector2 startPos;
    Vector2 shootPos;
    private GameObject enemy;

    private void Start() => StartFunc();

    private void StartFunc()
    {
        aim_Ctrl = GameObject.FindGameObjectWithTag("Player").GetComponent<Hook_Aim_Ctrl>();
        hook_speed = 10.0f;
        hook_backSpeed = 7.0f;
        player_Pos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        line_Renderer = GetComponent<LineRenderer>();
        line_Renderer.startWidth = 0.2f;
        line_Renderer.endWidth = 0.2f;
        //startPos = player_Pos.position;
        startPos = new Vector2(player_Pos.position.x, player_Pos.position.y + 0.7f);
        aim_Ctrl.isTouch = false;

        enemy = null;

    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        shootPos = this.transform.position;
        line_Renderer.SetPosition(0, startPos);
        line_Renderer.SetPosition(1, shootPos);

        if(aim_Ctrl.isTouch == false)
        {
            Hook_Release();
        }
        else
        {
            Hook_Return();
        }

        if(aim_Ctrl.isTouch == true && enemy != null)
        {
            enemy.transform.position = this.transform.position;
        }

        //에너미 의 거리랑 플레이어 거리가 얼마 이상이면 isTouch false로 바꾸고 놓치기
        
    }

    private void Hook_Release()
    {
        //aim_Ctrl.hook_Target_Pos를 목적지로 본인 위치부터 hook_Target_Pos까지 이동
        this.transform.position = Vector3.Lerp(this.transform.position, aim_Ctrl.hook_Target_Pos, Time.deltaTime * hook_speed);


        //은범이가 알려준 아이디어
        Vector2 playervec = player_Pos.position - this.transform.position;
        //Debug.Log(playervec.x);
        if (playervec.x < 0)
        {
            this.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            this.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        //은범이가 알려준 아이디어
    }

    private void Hook_Return()
    {
        //Debug.Log("back");
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
            aim_Ctrl.isTouch = false;
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            aim_Ctrl.isTouch = true;

            enemy = collision.gameObject;

            //콜리더 당겨오기 구현 필요
            collision.transform.position = this.transform.position;
        }
    }

}