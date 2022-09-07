using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    enemy_Idle=0,
    enemy_Patrol=1,
    enemy_Chase = 2,
    enemy_Attack=3,
    enemy_Skill=4,
    enemy_Death=5,
    enemy_Hit=6,
    enemy_Retreat=7,
    enemy_Resurrection=8,
    enemy_Stun=9,
}

public class Enemy_State_Ctrlr : MonoBehaviour
{
    public EnemyState e_State;
    // Start is called before the first frame update
    void Start()
    {
        e_State = EnemyState.enemy_Idle;
    }
}
