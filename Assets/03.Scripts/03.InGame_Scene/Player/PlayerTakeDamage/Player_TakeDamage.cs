using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_TakeDamage : MonoBehaviour
{
    private Player_State_Ctrlr P_State;
    private Animator animator;

    public Image Hp_Img;
    public Image Mp_Img;

    public float maxHp;
    public float curHp;

    // Start is called before the first frame update
    void Start()
    {
        P_State = GetComponent<Player_State_Ctrlr>();
        animator = GetComponent<Animator>();
        P_State.p_state = PlayerState.player_takeDamage;
        maxHp = 100.0f;
        curHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void P_TakeDmage(float dam)
    {
        if (P_State.p_state == PlayerState.player_die)
            return;

        if(P_State.p_Defece_state == PlayerDefenceState.player_onShield)
        {
            P_State.p_Defece_state = PlayerDefenceState.player_ShieldActive;
            animator.SetTrigger("ShieldActive");
            P_State.p_Defece_state = PlayerDefenceState.player_onShield;
        }
        else
        {
            P_State.p_state = PlayerState.player_takeDamage;
            curHp -= dam;
            Hp_Img.fillAmount = curHp / maxHp;
            P_State.p_state = PlayerState.player_takeDamage;
            animator.SetTrigger("TakeDamage");
            Debug.Log(curHp);

            if(curHp <= 0.0f)
            {
                P_Die();
            }
        }
    }

    public void P_Die()
    {
        P_State.p_state = PlayerState.player_die;
        Debug.Log("DieFunc Here");
    }
}
