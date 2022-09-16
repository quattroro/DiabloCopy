using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Zombie : baseMonster
{
    public Vector2 Foword;
    public Vector2 Right;
    //public enum DIRECTION { D1 = 1, D2, D3, D4, D5, D6, D7, D8, DIRECTIONMAX };//정면 부터 시계방향으로 돌아가는 8방향
    //public int TestCurHP;



    public override void StartVirtual()
    {
        base.StartVirtual();

        Myname = "Zombie";
        MaxHP = 30;
        curhp = 30;
        MaxMP = 0;
        Damage = 10;
        ResisFire = 10;
        ResisLightning = 0;
        ResistMagic = 15;

        MoveScript = GetComponent<MonsterMove>();
        MonsterAnimator = GetComponentInChildren<Animator>();
        State = MONSTERSTATE.IDLE;
    }

    void isDead()
    {
        //this.MonsterAnimator.
        Destroy(this.gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        GameObject obj = DetectPlayer();


        //TestCurHP = this.CurHP;
    }
}
