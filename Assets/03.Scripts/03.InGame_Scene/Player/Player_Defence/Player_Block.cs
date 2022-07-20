using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Block : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Player_State_Ctrlr Player_State;
    public GameObject def_area;
    Animator animator;

    //쉴드 상태일때 scale 값 받아두기
    public float Shield_scale;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Player_State = GetComponent<Player_State_Ctrlr>();
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
            def_area.gameObject.SetActive(true);
            Shield_scale = this.transform.localScale.x;
        }

        if (Input.GetMouseButtonUp(1))
        {
            Player_State.p_state = PlayerState.player_idle;
            Player_State.p_Defece_state = PlayerDefenceState.player_noShield;
            def_area.gameObject.SetActive(false);
        }
    }
}
