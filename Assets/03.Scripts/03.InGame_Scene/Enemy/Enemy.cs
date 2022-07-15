using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    //플레이어 찾아두기
    //위치나 state 상태 체크
    protected GameObject player;

    protected Enemy_State_Ctrlr E_State;

    //애니메이션
    protected Animator animator;

    //체력
    protected float MaxHp;
    protected float CurHp;

    //공격
    protected float e_Att;
    protected float e_Att_Range;
    protected float e_Att_Speed;

    //스킬
    protected float Skill1;
    protected float Skill2;
    protected float Skill3;
    protected float Skill4;

    //Patrol 타이머
    protected float patrol_Time;
    //Patrol 방향
    protected int right;
    //0이면 오른쪽, 1이면 왼쪽으로 Random.Range를 사용해서 구현 예정

    //Chase상태로 넘어가는 체크를 하기 위한 float (Vector2.Distance)를 위한 변수 필요
    protected float chaseDist;

    //이동속도
    protected float e_move_Speed;



    //Awake에서 찾아두기
    protected abstract void InitData();

    //패트롤
    protected abstract void M_Patrol();

    //추적
    protected abstract void M_Chase();

    //추적에 필요한 플레이어와 거리 체크 함수
    protected abstract void M_ChaseDist();

    //공격
    protected abstract void M_Attack();

    //후퇴
    protected abstract void M_Retreat();

    //피격
    protected abstract void M_Hit();

    //사망
    protected abstract void M_Death();

    //부활
    protected abstract void M_Resurrection();

}