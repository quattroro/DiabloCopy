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

        ////움직이는 중 일때 
        //if(curState == moveState)
        //{
        //    //공격범위에 플레이어가 들어왔는지 지속적으로 확인하고 
        //    //공격범위 안에 들어왔으면 공격을 해준다.


        //}
        
        ////공격하는 중 일때
        //if(curState == attackState)
        //{
        //    //플레이어가 공격범위 밖으로 나갔으면 
        //}


    }
}
