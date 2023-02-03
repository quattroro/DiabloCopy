using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Attack : BaseState
{
    ZombieFSM zombieFSM;
    baseMonster monster;

    AttackInfo curAttackInfo;
    AttackInfo lastAttackInfo;


    public Attack(StateMachine _stateMachine) : base(_stateMachine)
    {
        zombieFSM = this.stateMachine as ZombieFSM;
        monster = zombieFSM.basemonster;
    }

    //public void Exit()
    //{
    //    Debug.Log("Idle Exit 실행");
    //}

    public void Enter()
    {
        monster.AnimationState = MONSTERSTATE.Attack;
    }

    //각 상황에 따라서 공격 정보를 설정한다.
    //
    public void Update()
    {
        //Debug.Log("Idle Update 실행");

        //플레이어가 공격범위 밖으로 나갔으면 
        //Idle상태로 전이된다.
        if (!zombieFSM.zombieMove.CheckAttackRange())
        {
            zombieFSM.ChangeState(zombieFSM.idleStste);
        }


    }
}
