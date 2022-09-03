using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Staff : ShootWeapon
{
    

    public Weapon_Staff()
    {
        //base.damage = 30;
        //base.cooltime = 1.8f;
    }


    //어택이 실행되면 플레이어에 지정되어 있는 상대방 정보를 받아와서 데미지를 준다.
    public override void Attack()
    {
        //sc_monster = sc_player.sc_targetmonster;
        //monster=sc_player.TargetMonster
        if(sc_player.State!=PLAYERSTATE.ATTACK)
        {
            sc_player.State = PLAYERSTATE.ATTACK;
            monster = sc_player.targetmonster;
            sc_monster = monster.GetComponent<baseMonster>();
            StartCoroutine("ShootAttack");
        }
    }

    //public override void ShootAttack()
    //{
    //    Debug.Log("StaffAttack");

    //}

    //public override void Reload()
    //{
    //    Debug.Log("Reload is not define");
    //}


    //private void Awake()
    //{
    //    base.damage = 30f;
    //    base.cooltime = 1.8f;
    //}


    void Start()
    {
        //base.damage = 20;
        //base.cooltime = 2.0f;
        //meleekind = MELEEKIND.AXE;
        damage = Magics[(int)NowMagic].GetComponent<baseMagic>().Damage;
        cooltime = Magics[(int)NowMagic].GetComponent<baseMagic>().CoolTime;
        shootkind = SHOOTKIND.STAFF;
        sc_player = transform.parent.GetComponent<Player>();
        Debug.Log("Staff start");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
