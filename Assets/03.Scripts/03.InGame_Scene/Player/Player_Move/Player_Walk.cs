using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Walk : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Player_Input p_input;
    private Player_State_Ctrlr P_State;
    private Player_Block P_block;
    Animator animator;

    private GameObject startPos;
    public float move_speed;

    public int key = 0;

    [Header("===발자국 소리")]
    AudioSource audioSrc;
    public bool isMove = false;

    // Start is called before the first frame update
    void Start()
    {
        startPos = GameObject.Find("StartPos");
        P_State = GetComponent<Player_State_Ctrlr>();
        P_State.p_state = PlayerState.player_idle;
        //P_State.p_Move_state = PlayerMoveState.player_noWalk;
        P_State.p_Defece_state = PlayerDefenceState.player_noShield;
        P_block = GetComponent<Player_Block>();
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        p_input = GetComponent<Player_Input>();
        move_speed = 4.5f;

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(P_State.p_state != PlayerState.player_die)
        {
            if (p_input.horizontal != 0)
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
                isMove = false;
            }
        }

        if (isMove && P_State.p_Move_state == PlayerMoveState.player_walk)
        {
            if (!audioSrc.isPlaying)
                audioSrc.Play();
        }
        else if (!isMove && P_State.p_Move_state != PlayerMoveState.player_walk)
            audioSrc.Stop();

    }

    private void P_Move_Walk()
    {

        if (P_State.p_state == PlayerState.player_die)
        {
            move_speed = 0.0f;
            return;
        }

        if (p_input.horizontal < 0)
            key = -1;

        if (0 < p_input.horizontal)
            key = 1;

        Vector2 p_vector = new Vector2(p_input.horizontal, .0f);
        Vector2 p_move = p_vector * move_speed * Time.deltaTime;
        rigid.position += p_move;

        if (P_State.p_Move_state != PlayerMoveState.player_noWalk && P_State.p_Defece_state == PlayerDefenceState.player_noShield)
        {
            isMove = true;

            move_speed = 4.5f;
            if (key == 1)
                this.transform.localEulerAngles = new Vector3(0, 0, 0);
            else if (key == -1)
                this.transform.localEulerAngles = new Vector3(0, 180, 0);
        }


        if (P_State.p_Defece_state == PlayerDefenceState.player_onShield)
        {
            move_speed = 1.5f;

            if (this.transform.localEulerAngles.y == 0)
            {
                this.transform.localEulerAngles = new Vector3(0, 0, 0);
                //Debug.Log("Shield right fix");
            }
            else if (this.transform.localEulerAngles.y == -180)
            {
                this.transform.localEulerAngles = new Vector3(0, 0, 0);
                //Debug.Log("Shield left fix");
            }
            //Debug.Log("Shield On");
        }

        //이동 제한
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position); 
        if (pos.x < 0f) pos.x = 0f; 
        if (pos.x > 1f) pos.x = 1f; 
        if (pos.y < 0f) pos.y = 0f; 
        if (pos.y > 1f) pos.y = 1f; 
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }
}
