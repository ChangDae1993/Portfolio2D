using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Walk : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Player_Input p_input;
    private Player_State_Ctrlr P_State;
    Animator animator;

    public float move_speed = 3.0f;
    private float crawl_speed = 2.0f;

    public int key = 0;

    // Start is called before the first frame update
    void Start()
    {
        P_State = GetComponent<Player_State_Ctrlr>();
        P_State.p_state = PlayerState.player_idle;
        P_State.p_Move_state = PlayerMoveState.player_noWalk;

        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        p_input = GetComponent<Player_Input>();
        move_speed = 3.5f;
        crawl_speed = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {

        if(p_input.horizontal != 0)
        {
            P_State.p_state = PlayerState.player_move;
            P_State.p_Move_state = PlayerMoveState.player_walk;
            P_Move_Walk();
            animator.SetBool("IsWalk", true);
        }
        else
        {
            P_State.p_state = PlayerState.player_idle;
            P_State.p_Move_state = PlayerMoveState.player_noWalk;
            animator.SetBool("IsWalk", false);
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            P_State.p_state = PlayerState.player_move;
            P_State.p_Move_state = PlayerMoveState.player_crawl;
            P_Move_Crawl();
        }
        else
        {
            move_speed = 3.5f;
        }
    }

    private void P_Move_Walk()
    {
        if (p_input.horizontal < 0)
            key = -1;

        if (0 < p_input.horizontal)
            key = 1;

        Vector2 p_vector = new Vector2(p_input.horizontal, .0f);
        Vector2 p_move = p_vector * move_speed * Time.deltaTime;
        rigid.position += p_move;

        if (P_State.p_Move_state != PlayerMoveState.player_noWalk)
        {
            if (p_input.horizontal != 0)
            {
                transform.localScale = new Vector3(key * 1.2f, 1.2f, 1);
            }
        }
    }

    private void P_Move_Crawl()
    {
        move_speed = crawl_speed;
    }
}
