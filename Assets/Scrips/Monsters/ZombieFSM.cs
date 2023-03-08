using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieFSM : StateMachine
{
    public baseMonster basemonster;

    public Idle idleStste;
    public Move moveState;
    public Attack attackState;
    public Die dieState;
    public Attacked attackedState;

    public MonsterAttack zombieAttack;
    public MonsterMove zombieMove;



    private void Awake()
    {
        basemonster = GetComponent<baseMonster>();

        idleStste = new Idle(this);
        moveState = new Move(this);
        attackState = new Attack(this);
        dieState = new Die(this);
        attackedState = new Attacked(this);


        zombieAttack = GetComponent<MonsterAttack>();
        zombieMove = GetComponent<MonsterMove>();
    }

    public override void Update()
    {
        base.Update();
        if(Input.GetKeyDown(KeyCode.B))
        {
            ChangeState(idleStste);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeState(moveState);
        }

        ////�����̴� �� �϶� 
        //if(curState == moveState)
        //{
        //    //���ݹ����� �÷��̾ ���Դ��� ���������� Ȯ���ϰ� 
        //    //���ݹ��� �ȿ� �������� ������ ���ش�.


        //}
        
        ////�����ϴ� �� �϶�
        //if(curState == attackState)
        //{
        //    //�÷��̾ ���ݹ��� ������ �������� 
        //}


    }
}
