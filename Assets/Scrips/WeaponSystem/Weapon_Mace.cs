using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Weapon을 상속받음
public class Weapon_Mace : MeleeWeapon
{


    public Weapon_Mace()
    {
        base.damage = 30;
        base.cooltime = 1.8f;
    }

    //public override void MeleeAttack()
    //{
    //    Debug.Log("MaceAttack");
    //}


    //private void Awake()
    //{
    //    base.damage = 30f;
    //    base.cooltime = 1.8f;
    //}
    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
