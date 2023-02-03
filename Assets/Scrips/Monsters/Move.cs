using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Move : BaseState
{
    ZombieFSM zombieFSM;
    baseMonster monster;

    


    public Move(StateMachine _stateMachine) : base(_stateMachine)
    {
        zombieFSM = this.stateMachine as ZombieFSM;
        monster = zombieFSM.basemonster;
        
    }

    //public void Exit()
    //{
    //    Debug.Log("Move Exit 실행");
    //}

    //움직임을 시작하기 전에 공격 범위에 적이 있는지 판단한다.
    public void Enter()
    {
        //monster.AnimationState = MONSTERSTATE.Move;

        ////움직임이 시작될때 플레이어가 공격범위 안에 들어왔는지 확인한다.
        //if (zombieFSM.zombieMove.CheckAttackRange())
        //{
        //    zombieFSM.zombieAttack.StartAttack(monster.DetectedPlayer, monster.meleeAttacks[0]);
        //    zombieFSM.ChangeState(zombieFSM.attackState);
        //}

    }

    public void Update()
    {
        //공격범위에 플레이어가 들어왔는지 지속적으로 확인하고 
        //공격범위 안에 들어왔으면 공격을 해준다.
        if(zombieFSM.zombieMove.CheckAttackRange())
        {
            zombieFSM.zombieAttack.StartAttack(monster.DetectedPlayer, monster.meleeAttacks[0]);
            zombieFSM.ChangeState(zombieFSM.attackState);
        }

    }
}
