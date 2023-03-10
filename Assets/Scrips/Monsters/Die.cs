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

    public IEnumerator Enter()
    {
        monster.AnimationState = MONSTERANISTATE.DEAD;
        yield return new WaitForSeconds(4.0f);
        GameObject.Destroy(monster.gameObject);
    }

    //public void Update()
    //{
    //    Debug.Log("Move Update 실행");
    //}
}
