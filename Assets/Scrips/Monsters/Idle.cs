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
        Debug.Log("Idle Exit ����");
        
    }

    public virtual void Enter()
    {
        Debug.Log("Idle Enter ����");
        lastTime = Time.time;
        if (monster == null)
            monster = zombieFSM.basemonster;
        monster.AnimationState = MONSTERSTATE.Idle;

    }

    

    public virtual void Update()
    {
        
        //�����ð� ���Ŀ� �������� ������ ���ؼ� �ش� �������� �����δ�.
        if (zombieFSM.basemonster.NowDetected)
        {
            //Ž���� �Ǿ����� �ϴ� ĳ���͸� ���� �����̰� �ϰ� movestate���� ���ݹ����� ������ �������� ���߰� �����ϵ��� �Ѵ�.


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
                
                Debug.Log("������ ����");

            }
        }

        //Debug.Log("Idle Update ����");

    }
}
