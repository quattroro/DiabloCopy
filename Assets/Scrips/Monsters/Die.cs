using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : BaseState
{
    ZombieFSM zombieFSM;
    baseMonster monster;

    public Die(StateMachine _stateMachine) : base(_stateMachine)
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
        monster.AnimationState = MONSTERSTATE.Die;
    }

    //public void Update()
    //{
    //    Debug.Log("Move Update 실행");
    //}
}
