using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region 이거로 바꿀 예정
public enum PlayerState
{
    player_menu = 0,    //하위 class 있음
    player_move = 1,    //하위 class 있음
    player_attack = 2,  //하위 class 있음
    player_die = 3,
    player_idle = 4,
    player_talk = 5,
    player_map = 6,
    player_takeDamage = 7,
    player_Shield = 8
}

public enum PlayerMoveState
{
    player_walk = 0,
    player_roll = 1,
    player_jump = 2,
    player_fall = 3,
    player_crawl = 4,
    player_climb = 5,
    player_noWalk = 6,
    player_ShieldOn = 7
}

public enum PlayerAttackState
{
    player_Sword = 0,
    player_Shield = 1,
    player_noAttack = 2
}

public enum PlayerDefenceState
{
    player_noShield = 0,
    player_onShield = 1,
    player_ShieldActive =2
}
#endregion

public class Player_State_Ctrlr : MonoBehaviour
{
    public PlayerState p_state;
    public PlayerMoveState p_Move_state;
    public PlayerAttackState p_Attack_state;
    public PlayerDefenceState p_Defece_state;

    // Start is called before the first frame update
    void Start()
    {
        p_state = PlayerState.player_idle;
        p_Move_state = PlayerMoveState.player_walk;
        p_Attack_state = PlayerAttackState.player_noAttack;
        p_Defece_state = PlayerDefenceState.player_noShield;

    }
}
