using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill2 : MonoBehaviour
{
    private Image sk2_coolImg;
    private GameObject player;
    private Player_Input p_Input;
    private Player_State_Ctrlr p_State;
    private Player_Block p_Block;
    private Player_Walk p_Walk;


    public bool shieldDash;
    [SerializeField] private float sDashTimer;
    [SerializeField] private float sDashTime;

    // Start is called before the first frame update
    void Start()
    {
        p_Input = GetComponentInParent<Player_Input>();
        p_State = GetComponentInParent<Player_State_Ctrlr>();
        p_Block = GetComponentInParent<Player_Block>();
        p_Walk = GetComponentInParent<Player_Walk>();
        shieldDash = false;
        sDashTime = 0.8f;
        sDashTimer = sDashTime;

        sk2_coolImg = GameObject.Find("Skill_Cool_CTRLR").GetComponent<Skill_Cool_Ctrlr>().s2_coolImg;
    }

    // Update is called once per frame
    void Update()
    {

        if (p_State.p_Defece_state == PlayerDefenceState.player_ShieldDash)
        {
            shieldDash = true;
        }

        if (shieldDash)
        {
            if (sDashTimer >= 0.0f)
            {
                sDashTimer -= Time.deltaTime;
                ShieldDash();
            }
        }
    }
    #region Shield Dash
    public void ShieldDash()
    {
        Debug.Log("Shield Dash");
        if (sDashTimer < 0.0f)
        {
            shieldDash = false;
            sDashTimer = sDashTime;
            p_State.p_Defece_state = PlayerDefenceState.player_onShield;
        }
    }
    #endregion

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy") && shieldDash)
        {
            sDashTimer = -1.0f;
            Debug.Log("Stun");
        }
    }
}
