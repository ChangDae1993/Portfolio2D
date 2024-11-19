using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class Hook_Aim_Ctrl : MonoBehaviour
{
    //aim_On/Off
    private Vector2 world_Pos;
    public GameObject aim_Img;
    private Player_State_Ctrlr P_State;
    private Vector2 mouse_Pos;
    public bool aim_shoot;

    //hook ����
    [HideInInspector] public Vector3 hook_Target_Pos;
    [HideInInspector] public Transform start_Pos;
    public bool isTouch;
    private Player_Walk p_Walk;

    public Image coolImg;
    public bool isCool;
    public float coolTimer;

    private void Start() => StartFunc();

    private void StartFunc()
    {
        P_State = GetComponent<Player_State_Ctrlr>();
        p_Walk = GetComponent<Player_Walk>();
        P_State.p_state = PlayerState.player_attack;
        P_State.p_Attack_state = PlayerAttackState.player_noAttack;
        aim_shoot = false;
        isTouch = false;
        isCool = false;
        coolImg.gameObject.SetActive(false);
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {

        if (Input.GetKey(KeyCode.LeftControl))
        {
            p_Walk.move_speed = 1.0f;
            aim_shoot = false;
            P_State.p_state = PlayerState.player_attack;
            P_State.p_Attack_state = PlayerAttackState.player_hook_aim;
            mouse_Pos = Input.mousePosition;
            aim_Img.gameObject.SetActive(true);
            aim_Img.transform.position = Camera.main.ScreenToWorldPoint(mouse_Pos);
            aim_Img.transform.position = new Vector3(aim_Img.transform.position.x, aim_Img.transform.position.y, 1.0f);

        }
        else
        {
            aim_shoot = true;
            P_State.p_Attack_state = PlayerAttackState.player_hook_release;
            aim_Img.gameObject.SetActive(false);
        }


        if (P_State.p_Attack_state == PlayerAttackState.player_hook_aim)
        {
            if (Input.GetMouseButtonDown(0) && coolImg.fillAmount == 1.0f)
            {
                SoundMgr.Instance.PlayEffSound("hook_release", 0.6f);

                if (isTouch == true)
                    return;
                isCool = true;
                isTouch = false;
                aim_shoot = true;
                P_State.p_Attack_state = PlayerAttackState.player_hook_shoot;

                //��ư Ŭ�� ��, aim�� ������ �޾Ƶα�
                hook_Target_Pos = aim_Img.transform.position;

                //Prefab���� hook ���� �� hooj_Target_Pos�� ������
                //Prefab.Instantiate(GameObject)....etc;
                GameObject hook_prefab = (GameObject)Instantiate(Resources.Load("Prefab/hook")) as GameObject;
                hook_prefab.transform.parent = GameObject.FindGameObjectWithTag("Player").transform;
                hook_prefab.transform.position = start_Pos.position;
                //Vector3.Lerp �̿��ؼ� ������??
                //player���� hook���� Line Renderer ����
                //Debug.Log("Shoot");
                if (aim_shoot == true)
                {
                    aim_Img.gameObject.SetActive(false);
                }
            }
        }

        if (isCool)
        {
            coolImg.gameObject.SetActive(true);
            coolImg.fillAmount -= Time.deltaTime*0.25f;
            if(coolImg.fillAmount <= 0.0f)
            {
                coolImg.fillAmount = 1.0f;
                coolImg.gameObject.SetActive(false);
                isCool = false;
            }
        }


    }
}