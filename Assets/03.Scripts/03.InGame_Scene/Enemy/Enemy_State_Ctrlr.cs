using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    enemy_idle,
    enemy_talk,
    enemy_walk,
    enemy_chase,
    enemy_attack,
    enemy_die,
    enemy_escape,
}

public class Enemy_State_Ctrlr : MonoBehaviour
{
    public EnemyState e_State;

    private void Start() => StartFunc();

    private void StartFunc()
    {
        e_State = EnemyState.enemy_idle;
    }
}