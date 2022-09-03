using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Axe : MeleeWeapon
{
    
    public Weapon_Axe()
    {
        //base.damage = 30;
        //base.cooltime = 1.8f;
    }

    public override void Attack()
    {
        //sc_monster = sc_player.sc_targetmonster;
        //monster=sc_player.TargetMonster
        if (sc_player.State != PLAYERSTATE.ATTACK)
        {
            sc_player.State = PLAYERSTATE.ATTACK;
            monster = sc_player.targetmonster;
            sc_monster = monster.GetComponent<baseMonster>();
            StartCoroutine("MeleeAttack");
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        //base.damage = 30;
        //base.cooltime = 1.8f;
        meleekind = MELEEKIND.AXE;
        sc_player = transform.parent.GetComponent<Player>();
        Debug.Log("Axe start");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
