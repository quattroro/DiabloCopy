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
    //    Debug.Log("Move Exit ����");
    //}

    //�������� �����ϱ� ���� ���� ������ ���� �ִ��� �Ǵ��Ѵ�.
    public void Enter()
    {
        //monster.AnimationState = MONSTERSTATE.Move;

        ////�������� ���۵ɶ� �÷��̾ ���ݹ��� �ȿ� ���Դ��� Ȯ���Ѵ�.
        //if (zombieFSM.zombieMove.CheckAttackRange())
        //{
        //    zombieFSM.zombieAttack.StartAttack(monster.DetectedPlayer, monster.meleeAttacks[0]);
        //    zombieFSM.ChangeState(zombieFSM.attackState);
        //}

    }

    public void Update()
    {
        //���ݹ����� �÷��̾ ���Դ��� ���������� Ȯ���ϰ� 
        //���ݹ��� �ȿ� �������� ������ ���ش�.
        if(zombieFSM.zombieMove.CheckAttackRange())
        {
            zombieFSM.zombieAttack.StartAttack(monster.DetectedPlayer, monster.meleeAttacks[0]);
            zombieFSM.ChangeState(zombieFSM.attackState);
        }

    }
}
