using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    enemy_Idle=0,
    enemy_Patrol=1,
    enemy_Attack=2,
    enemy_Skill=3,
    enemy_Death=4,
    enemy_Hit=5,
    enemy_Retreat=6,
    enemy_Resurrection=7,
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
