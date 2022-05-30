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

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Player_State = GetComponent<Player_State_Ctrlr>();
        p_input = GetComponent<Player_Input>();
        def_area = GetComponent<BoxCollider2D>();
        p_walk = GetComponent<Player_Walk>();
        Player_State.p_Defece_state = PlayerDefenceState.player_noShield;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Player_State.p_state = PlayerState.player_Shield;
            Player_State.p_Defece_state = PlayerDefenceState.player_onShield;
            def_area.enabled = true;
            p_walk.move_speed = 2.0f;
        }

        if (Input.GetMouseButtonUp(1))
        {
            Player_State.p_Defece_state = PlayerDefenceState.player_noShield;
            p_walk.move_speed = 3.5f;
            def_area.enabled = false;
        }
    }
}
