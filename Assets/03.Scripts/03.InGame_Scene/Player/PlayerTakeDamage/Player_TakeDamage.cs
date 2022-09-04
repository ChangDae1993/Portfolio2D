using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_TakeDamage : MonoBehaviour
{
    private Player_State_Ctrlr P_State;
    private Animator animator;
    private Player_Input P_Input;
    private Player_Block P_Block;

    public Image GameOver_Panel;
    public Image PlayerOver_Panel;
    public Button ReplayBtn;
    public Button ExitBtn;

    public Image Hp_Img;
    public Image Mp_Img;

    public float maxHp;
    public float curHp;

    private float slowTimer;


    // Start is called before the first frame update
    void Start()
    {
        P_State = GetComponent<Player_State_Ctrlr>();
        animator = GetComponent<Animator>();
        P_Input =GetComponent<Player_Input>();
        P_Block = GetComponent<Player_Block>();
        P_State.p_state = PlayerState.player_takeDamage;
        maxHp = 100.0f;
        curHp = maxHp;
        this.gameObject.layer = 7;
        slowTimer = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (curHp <= 0.0f)
        {
            slowTimer -= Time.deltaTime;
            if (0.0f <= slowTimer)
            {
                Time.timeScale = 0.5f;
            }
            else
                Time.timeScale = 1.0f;
        }
    }

    public void P_TakeDmage(float dam)
    {
        if (P_State.p_state == PlayerState.player_die)
            return;

        if (P_State.p_Defece_state == PlayerDefenceState.player_onShield)
        {
            P_State.p_Defece_state = PlayerDefenceState.player_ShieldActive;
            animator.SetTrigger("ShieldActive");
            P_State.p_Defece_state = PlayerDefenceState.player_onShield;

            P_Block.shield_Dur.fillAmount -= 0.1f;

            int block = Random.Range(0, 3);

            if(block == 0)
            {
                SoundMgr.Instance.PlayEffSound("block_1", 1.0f);
            }
            else if(block == 1)
            {
                SoundMgr.Instance.PlayEffSound("block_2", 1.0f);
            }
            else if (block == 2)
            {
                SoundMgr.Instance.PlayEffSound("block_3", 1.0f);
            }
        }
        else
        {
            if (P_State.p_Move_state == PlayerMoveState.player_roll)
            {
                return;
            }

            if (P_State.p_state != PlayerState.player_die)
            {
                P_State.p_state = PlayerState.player_takeDamage;
                curHp -= dam;
                Hp_Img.fillAmount = curHp / maxHp;
                SoundMgr.Instance.PlayEffSound("Player_Hit", 0.6f);
                animator.SetTrigger("TakeDamage");
                //Debug.Log(curHp);
            }
            if (curHp <= 0.0f)
            {
                this.gameObject.layer = 12;
                P_Die();
            }

        }
    }

    public void P_Die()
    {
        SoundMgr.Instance.PlayGUISound("Player_Die", 1.0f);
        P_State.p_state = PlayerState.player_die;
        animator.SetTrigger("Death");
        P_Input.enabled = false;
        //Debug.Log("DieFunc Here");
    }
}
