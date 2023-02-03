using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Idle : BaseState
{
    ZombieFSM zombieFSM;
    baseMonster monster;
    MonsterMove monsterMove;

    float lastTime;
    float moveTime = 2.0f;
    float moveDistance;

    public Idle(StateMachine _stateMachine): base(_stateMachine)
    {
        zombieFSM = this.stateMachine as ZombieFSM;
        monster = zombieFSM.basemonster;
        monsterMove = monster.moveScript;
    }

    public virtual void Exit()
    {
        Debug.Log("Idle Exit 실행");
        
    }

    public virtual void Enter()
    {
        Debug.Log("Idle Enter 실행");
        lastTime = Time.time;
        if (monster == null)
            monster = zombieFSM.basemonster;
        monster.AnimationState = MONSTERSTATE.Idle;

    }

    

    public virtual void Update()
    {
        
        //일정시간 이후에 램덤으로 방향을 정해서 해당 방향으로 움직인다.
        if (zombieFSM.basemonster.NowDetected)
        {
            //탐지가 되었으면 일단 캐릭터를 따라 움직이게 하고 movestate에서 공격범위로 들어오면 움직임을 멈추고 공격하도록 한다.


        }
        else
        {
            if ((Time.time - lastTime) >= moveTime)
            {
                lastTime = Time.time;
                if(monsterMove==null)
                    monsterMove = monster.moveScript;

                stateMachine.ChangeState(zombieFSM.moveState);
                monsterMove.MoveStart(Random.Range(0, (int)DIRECTION.DIRECTIONMAX));
                
                Debug.Log("움직임 실행");

            }
        }

        //Debug.Log("Idle Update 실행");

    }
}
