using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_TakeDamage : MonoBehaviour
{
    private Player_State_Ctrlr P_State;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        P_State = GetComponent<Player_State_Ctrlr>();
        animator = GetComponent<Animator>();
        P_State.p_state = PlayerState.player_takeDamage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void P_TakeDmage()
    {
        if(P_State.p_Defece_state == PlayerDefenceState.player_onShield)
        {
            P_State.p_Defece_state = PlayerDefenceState.player_ShieldActive;
            animator.SetTrigger("ShieldActive");
            P_State.p_Defece_state = PlayerDefenceState.player_onShield;
        }
        else
        {
            P_State.p_state = PlayerState.player_takeDamage;
            animator.SetTrigger("TakeDamage");
        }

    }
}
