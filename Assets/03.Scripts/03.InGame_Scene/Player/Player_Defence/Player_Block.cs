using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Block : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Player_Input p_input;
    private Player_State_Ctrlr Player_State;
    private BoxCollider2D def_area;
    private Player_Walk p_walk;
    Animator animator;

    //쉴드 상태일때 scale 값 받아두기
    public float Shield_scale;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Player_State = GetComponent<Player_State_Ctrlr>();
        p_input = GetComponent<Player_Input>();
        def_area = GetComponent<BoxCollider2D>();
        p_walk = GetComponent<Player_Walk>();
        Player_State.p_state = PlayerState.player_idle;
        Player_State.p_Defece_state = PlayerDefenceState.player_noShield;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Player_State.p_state = PlayerState.player_Shield;
            Player_State.p_Defece_state = PlayerDefenceState.player_onShield;
            def_area.enabled = true;
            p_walk.move_speed = 2.0f;
            Shield_scale = this.transform.localScale.x;
            //if(0 < p_input.horizontal)
            //{
            //    Debug.Log("Forword");
            //    if (p_walk.key == 1)
            //        transform.localScale = new Vector3(1.2f, 1.2f, 1);
            //    else if (p_walk.key == -1)
            //        transform.localScale = new Vector3(-1.2f, 1.2f, 1);
            //}
            //else if(p_input.horizontal < 0)
            //{
            //    Debug.Log("Back");
            //}

            //if (p_walk.key == 1)
            //{
            //    Debug.Log("Forward");
            //    if (Input.GetKey(KeyCode.A))
            //        transform.localScale = new Vector3(1.2f, 1.2f, 1);
            //}
            //else if(p_walk.key == -1)
            //{
            //    Debug.Log("Back");
            //}
            //Debug.Log(Shield_scale);
        }

        if (Input.GetMouseButtonUp(1))
        {
            Player_State.p_state = PlayerState.player_idle;
            Player_State.p_Defece_state = PlayerDefenceState.player_noShield;
            p_walk.move_speed = 3.5f;
            def_area.enabled = false;
        }
    }
}
