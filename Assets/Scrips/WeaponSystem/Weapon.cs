using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 무기들의 공격과 스킬은 컴포넌트 디자인 패턴을 이용하여 제작
//컴포넌트 패턴에서 receiver를 담당(행동을 하게되는 객체)
public class Weapon : MonoBehaviour
{
    public enum MELEEKIND { NODATA = -1, AXE, MACE, LONGSWORD, MELEEMAX };
    public enum WEAPONKIND { NODATA = -1, MELEE, BULLET, WEAPONMAX };
    public enum SHOOTKIND { NODATA = -1, STAFF, SHOOTMAX };


    public WEAPONKIND weaponkind = WEAPONKIND.NODATA;
    public MELEEKIND meleekind = MELEEKIND.NODATA;
    public SHOOTKIND shootkind = SHOOTKIND.NODATA;

    public int damage;
    public float cooltime;

    public Player sc_player = null;
    public baseMonster sc_monster = null;
    public Transform monster = null;


    public virtual void Attack()
    {

    }

    //public virtual void Attack()
    //{

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
