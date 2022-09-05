using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Jump : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Player_State_Ctrlr Player_State;
    Animator animator;
    Player_Walk p_walk;

    private float jump_power = 7.0f;

    public bool isJumping;

    RaycastHit2D rayHit;
    RaycastHit2D rayenem;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        Player_State = GetComponent<Player_State_Ctrlr>();
        p_walk = GetComponent<Player_Walk>();
        jump_power = 7.0f;
        isJumping = false;
        Player_State.p_state = PlayerState.player_idle;
        Player_State.p_Move_state = PlayerMoveState.player_jump;
        rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("PLATFORM"));
        rayenem = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Enemy"));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            p_walk.isMove = false;
            if (Player_State.p_state == PlayerState.player_die)
                return;

            if (isJumping == false)
            {
                SoundMgr.Instance.PlayEffSound("Player_Jump", 0.8f);
                isJumping = true;
                P_Move_Jump();

            }
            else
            {
                Player_State.p_Move_state = PlayerMoveState.player_jump;
            }
        }
    }

    void FixedUpdate()
    {
        // Lending Platform
        if (rigid.velocity.y <= 0)
        {
            //Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0)); //에디터 상에서만 레이를 그려준다

            if (rayHit.collider != null) // 바닥 감지를 위해서 레이저를 쏜다! 
            {
                if (rayHit.distance < 0.5f)
                {
                    animator.SetBool("IsJump", false);
                    isJumping = false;
                }
            }


            if (rayenem.collider != null)
            {
                if (rayenem.distance < 0.5f)
                {
                    animator.SetBool("IsJump", false);
                    isJumping = false;
                }
            }
        }
    }

    private void P_Move_Jump()
    {
        Player_State.p_Move_state = PlayerMoveState.player_jump;
        animator.SetBool("IsJump", true);
        rigid.AddForce(transform.up * jump_power, ForceMode2D.Impulse);
    }
}
