using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Hp_Mgr : MonoBehaviour
{
    private Enemy_State_Ctrlr enemy_State;
    public Image Hp_Bar;

    public float MaxHP = 100;
    private float CurHP;

    Animator animator;

    private void Start() => StartFunc();

    private void StartFunc()
    {
        enemy_State = GetComponent<Enemy_State_Ctrlr>();
        enemy_State.e_State = EnemyState.enemy_idle;
        animator = GetComponent<Animator>();

        CurHP = MaxHP;
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {

    }

    public void TakeDamage(int Damage)
    {
        CurHP -= Damage;

        //Play Hurt Anim
        animator.SetTrigger("EnemyHit");

        if(CurHP <= 0.0f)
        {
            E_Die();
        }
    }
        
    private void E_Die()
    {
        Debug.Log("enemy Dead");

        //Die Anim
        animator.SetBool("IsDead", true);

        //Disable the Enmey
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        this.enabled = false;

    }
}