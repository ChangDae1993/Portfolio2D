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
        if (a == 0)
        {
            animator.SetTrigger("Sword_Attack_1");
        }
        if (a == 1)
        {
            animator.SetTrigger("Sword_Attack_2");
        }
        if (a == 2)
        {
            animator.SetTrigger("Sword_Attack_3");
        }

        //Detect Enemy
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //Damage Enemy
        foreach(Collider2D enemy in hitEnemies)
        {
            //enemy_Hp.TakeDamage(attackDamage);
            Debug.Log("Hit");
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}