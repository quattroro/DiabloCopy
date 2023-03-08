using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Zombie : baseMonster
{
    public Vector2 Foword;
    public Vector2 Right;
    //public enum DIRECTION { D1 = 1, D2, D3, D4, D5, D6, D7, D8, DIRECTIONMAX };//정면 부터 시계방향으로 돌아가는 8방향
    //public int TestCurHP;
    public ZombieFSM zfsm;


    public override void StartVirtual()
    {
        base.StartVirtual();

        StateMachine fsm = GetComponentInChildren<ZombieFSM>();
        Init(fsm);

        ClassName = "Zombie";
        MaxHP = 30;
        curhp = 30;
        MaxMP = 0;
        Damage = 10;
        ResisFire = 10;
        ResisLightning = 0;
        ResistMagic = 15;

        moveScript = GetComponent<MonsterMove>();
        MonsterAnimator = GetComponentInChildren<Animator>();

        

        //State = MONSTERSTATE.IDLE;
        //FSM.ChangeState(FSM.idleStste);
    }

    public override void Init(StateMachine fsm)
    {
        base.Init(fsm);

        zfsm = FSM as ZombieFSM;

        zfsm.ChangeState(zfsm.idleStste);
    }

    //void isDead()
    //{
    //    //this.MonsterAnimator.
    //    Destroy(this.gameObject);
    //}


    void Update()
    {
        if(FSM.GetCurstate == MONSTERSTATE.Move.ToString()|| FSM.GetCurstate == MONSTERSTATE.Idle.ToString())
        {
            GameObject obj = DetectPlayer();
        }
        

    }
}
