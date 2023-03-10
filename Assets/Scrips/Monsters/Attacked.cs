using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacked : BaseState
{
    ZombieFSM zombieFSM;
    baseMonster monster;

    public Attacked(StateMachine _stateMachine) : base(_stateMachine)
    {
        zombieFSM = this.stateMachine as ZombieFSM;
        monster = zombieFSM.basemonster;
    }

    //public void Exit()
    //{
    //    Debug.Log("Move Exit 실행");
    //}

    public void Enter()
    {
        //monster.AnimationState = MONSTERSTATE.Die;
        Debug.Log("Attacked Enter 실행");
        //monster.MonsterAnimator.speed = 0.0f;
        //yield return new WaitForSeconds(monster.StunTime);
        //monster.MonsterAnimator.speed = 1.0f;





        zombieFSM.ChangeState(zombieFSM.lastState);
    }

    //public void Update()
    //{
    //    Debug.Log("Move Update 실행");
    //}

}
