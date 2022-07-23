using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook_Ctrl : MonoBehaviour
{
    //aim_On/Off
    private Vector2 start_Pos;
    private Vector2 world_Pos;
    public GameObject aim_Img;
    private Player_State_Ctrlr P_State;
    private Vector2 mouse_Pos;

    //hook ����
    public Vector2 hook_Target_Pos;

    private void Start() => StartFunc();

    private void StartFunc()
    {
        P_State = GetComponent<Player_State_Ctrlr>();
        P_State.p_state = PlayerState.player_attack;
        P_State.p_Attack_state = PlayerAttackState.player_noAttack;
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        start_Pos = this.transform.position;

        if (Input.GetKey(KeyCode.LeftControl))
        {
            P_State.p_state = PlayerState.player_attack;
            P_State.p_Attack_state = PlayerAttackState.player_hook_aim;
            mouse_Pos = Input.mousePosition;
            aim_Img.gameObject.SetActive(true);
            aim_Img.transform.position = Camera.main.ScreenToWorldPoint(mouse_Pos);
            aim_Img.transform.position = new Vector3(aim_Img.transform.position.x, aim_Img.transform.position.y, 1.0f);

            if (P_State.p_Attack_state == PlayerAttackState.player_hook_aim)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    P_State.p_Attack_state = PlayerAttackState.player_hook_shoot;

                    //��ư Ŭ�� ��, aim�� ������ �޾Ƶα�
                    hook_Target_Pos = aim_Img.transform.position;

                    //Prefab���� hook ���� �� hooj_Target_Pos�� ������
                    //Prefab.Instantiate(GameObject)....etc;
                    //Vector3.Lerp �̿��ؼ� ������??
                    //player���� hook���� Line Renderer ����
                    Debug.Log("Shoot");
                }
            }
        }
        else
        {
            P_State.p_Attack_state = PlayerAttackState.player_hook_release;
            aim_Img.gameObject.SetActive(false);
        }
        
    }
}