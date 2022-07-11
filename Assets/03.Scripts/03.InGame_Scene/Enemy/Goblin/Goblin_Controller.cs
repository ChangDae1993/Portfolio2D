using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_Controller : Enemy
{
    protected override void InitData()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        E_State = GetComponent<Enemy_State_Ctrlr>();
        animator = GetComponent<Animator>();
    }

    private void Awake()
    {
        InitData();
    }

    // Start is called before the first frame update
    void Start()
    {
        E_State.e_State = EnemyState.enemy_Idle;
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void M_Patrol()
    {
        throw new System.NotImplementedException();
    }
    protected override void M_Chase()
    {
        throw new System.NotImplementedException();
    }

    protected override void M_Attack()
    {
        throw new System.NotImplementedException();
    }

    protected override void M_Hit()
    {
        throw new System.NotImplementedException();
    }
    protected override void M_Retreat()
    {
        throw new System.NotImplementedException();
    }

    protected override void M_Death()
    {
        throw new System.NotImplementedException();
    }

    protected override void M_Resurrection()
    {
        throw new System.NotImplementedException();
    }
}
