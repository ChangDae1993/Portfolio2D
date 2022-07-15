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
    public bool isDash;

    private float roll_time;

    private float roll_speed;

    private float roll_Cool;

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
        roll_Cool = 3.0f;

        P_State.p_state = PlayerState.player_move;
        P_State.p_Move_state = PlayerMoveState.player_walk;
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift) && roll_Cool < 0.0f)
        {
            if (P_State.p_Defece_state == PlayerDefenceState.player_onShield)
                return;

            if (P_State.p_Move_state == PlayerMoveState.player_jump)
            {
                return;
            }

            isDash = true;
            roll_time = 0.4f;
            roll_Cool = 3.0f;
            animator.SetBool("IsDash", true);
        }
        else
        {
            P_State.p_Move_state = PlayerMoveState.player_walk;
            roll_speed = p_Walk.move_speed;
            roll_time -= Time.deltaTime;
            roll_Cool -= Time.deltaTime;
            animator.SetBool("IsDash", false);
            isDash = false;
        }

        if (0.0f < roll_time)
        {
            P_State.p_Move_state = PlayerMoveState.player_roll;
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