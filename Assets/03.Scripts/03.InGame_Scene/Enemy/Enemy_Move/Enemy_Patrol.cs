using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Patrol : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Enemy_State_Ctrlr enemy_State;
    Animator animator;

    float walk_force = 100.0f;


    private void Start() => StartFunc();

    private void StartFunc()
    {
        rigid = GetComponent<Rigidbody2D>();
        enemy_State = GetComponent<Enemy_State_Ctrlr>();
        enemy_State.e_State = EnemyState.enemy_idle;
        animator = GetComponent<Animator>();
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        
    }

    public void EnemyWalk(float w)
    {
        if (enemy_State.e_State == EnemyState.enemy_die)
        {
            animator.enabled = false;
            return;
        }

        enemy_State.e_State = EnemyState.enemy_walk;
        int key = Random.Range(-1, 2);
        w = w - Time.deltaTime;
        if (w <= 0)
        {
            return;

        }
        else if (0 < w)
        {
            if (key != 0)
            {

                rigid.AddForce(transform.right * key * walk_force);
                animator.SetTrigger("EnemyWalk");
                transform.localScale = new Vector3(key * 3.0f, 3.0f, 1);
            }
        }
    }
}