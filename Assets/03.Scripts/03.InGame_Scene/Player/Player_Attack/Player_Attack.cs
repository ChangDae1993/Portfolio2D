using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    //rigid body가 필요할지는 모르겠는데 일단 받아둠
    private Rigidbody2D rigid;
    private Player_Input p_input;
    private Player_State_Ctrlr Player_State;
    //private Enemy_Hp_Mgr enemy_Hp;
    Animator animator;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public int attackDamage;

    private void Start() => StartFunc();

    private void StartFunc()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Player_State = GetComponent<Player_State_Ctrlr>();
        p_input = GetComponent<Player_Input>();
        //enemy_Hp = GameObject.FindGameObjectWithTag("ENEMY").GetComponent<Enemy_Hp_Mgr>();
        Player_State.p_state = PlayerState.player_attack;
        Player_State.p_Attack_state = PlayerAttackState.player_noAttack;
        attackDamage = 20;
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (Player_State.p_Attack_state == PlayerAttackState.player_hook_aim)
                return;

            Player_State.p_state = PlayerState.player_attack;
            Player_State.p_Attack_state = PlayerAttackState.player_Sword;
        }
    }

    public void Sword_Attack(int a)
    {
        if (Player_State.p_state == PlayerState.player_die)
            return;

        if (a == 0)
        {
            animator.SetTrigger("Sword_Attack_1");
            SoundMgr.Instance.PlayEffSound("SwordAtt_1", 0.5f);
        }
        if (a == 1)
        {
            animator.SetTrigger("Sword_Attack_2");
            SoundMgr.Instance.PlayEffSound("SwordAtt_2", 0.5f);
        }
        if (a == 2)
        {
            animator.SetTrigger("Sword_Attack_3");
            SoundMgr.Instance.PlayEffSound("SwordAtt_3", 0.5f);
        }

        //Detect Enemy
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //Damage Enemy
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().M_Hit(10.0f);
            //Debug.Log("Hit");
            int att = Random.Range(0, 3);
            if (att == 0)
            {
                SoundMgr.Instance.PlayEffSound("SwordMon_1", 1.0f);
            }
            else if (att == 1)
            {
                SoundMgr.Instance.PlayEffSound("SwordMon_2", 1.0f);
            }
            else
            {
                SoundMgr.Instance.PlayEffSound("SwordMon_3", 1.0f);
            }
        }

    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}