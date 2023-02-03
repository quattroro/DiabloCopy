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
    //    Debug.Log("Idle Exit ����");
    //}

    public void Enter()
    {
        monster.AnimationState = MONSTERSTATE.Attack;
    }

    //�� ��Ȳ�� ���� ���� ������ �����Ѵ�.
    //
    public void Update()
    {
        //Debug.Log("Idle Update ����");

        //�÷��̾ ���ݹ��� ������ �������� 
        //Idle���·� ���̵ȴ�.
        if (!zombieFSM.zombieMove.CheckAttackRange())
        {
            zombieFSM.ChangeState(zombieFSM.idleStste);
        }


    }
}
