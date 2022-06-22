using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Roll : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Player_Input p_input;
    private Player_State_Ctrlr P_State;
    private Player_Walk p_Walk;
    Animator animator;
    bool isDash;

    private float roll_time;

    private float roll_speed = 5.0f;

    private void Start() => StartFunc();

    private void StartFunc()
    {
        P_State = GetComponent<Player_State_Ctrlr>();
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        p_input = GetComponent<Player_Input>();
        p_Walk = GetComponent<Player_Walk>();
        roll_speed = 5.0f;
        roll_time = 0.4f;
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (P_State.p_Defece_state == PlayerDefenceState.player_onShield)
                return;

            if (P_State.p_Move_state == PlayerMoveState.player_crawl || P_State.p_Move_state == PlayerMoveState.player_jump)
            {
                return;
            }

            isDash = true;
            P_State.p_state = PlayerState.player_move;
            P_State.p_Move_state = PlayerMoveState.player_roll;
            roll_time = 0.4f;
            animator.SetBool("IsDash", true);
        }
        else
        {
            roll_speed = p_Walk.move_speed;
            roll_time -= Time.deltaTime;
            animator.SetBool("IsDash", false);
            isDash = false;
        }

        if (0.0f < roll_time)
        {
            P_Move_Roll();
            this.gameObject.layer = 3;
        }
        else
            this.gameObject.layer = 7;
    }

    private void P_Move_Roll()
    {
        Vector2 p_vector = new Vector2(p_input.horizontal, 0);
        Vector2 p_dash = p_vector * roll_speed * Time.deltaTime;
        rigid.position += p_dash;
    }
}