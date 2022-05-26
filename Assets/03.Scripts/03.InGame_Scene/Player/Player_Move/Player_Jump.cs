using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Jump : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Player_Input p_input;
    private Player_State_Ctrlr P_State;
    Animator animator;

    private float jump_power = 600.0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        p_input = GetComponent<Player_Input>();
        P_State = GetComponent<Player_State_Ctrlr>();
        jump_power = 300.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            P_State.p_Move_state = PlayerMoveState.player_jump;
            animator.SetTrigger("JumpTrigger");
            P_Move_Jump();
        }
    }

    private void P_Move_Jump()
    {
        rigid.AddForce(transform.up * jump_power);
    }
}
