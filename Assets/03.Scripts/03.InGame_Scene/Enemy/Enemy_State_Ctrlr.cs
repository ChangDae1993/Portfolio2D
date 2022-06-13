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

    ///원거리 공격시 일정 범위 내에 들어오면 화살 공격 실행
    ///일정 거리보다 가까워지면 플레이어 key값을 받아서 그 키값 * 이동속도만큼 플레이어로부터 멀어짐 실행
    ///(공격 끝내고 도망치는 것)
}