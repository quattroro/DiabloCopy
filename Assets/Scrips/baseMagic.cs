using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseMagic : MonoBehaviour
{
    public Weapon MyWeapon;//자신을 발사한 무기정보를 가지고 있는다
    public int Damage;
    public float CoolTime;
    protected baseMonster targetmonster;
    protected Vector3 direction;//방향정보가 벡터로 주어진다.
    //DIRECTION direction;
    protected float degreedir;
    public float magicspeed;

    public float timeval;
    protected float lasttime;

    public Animator animator = null;
    //이미지 자체를 회전시킨다.
    //public DIRECTION Direction
    //{
    //    get
    //    {
    //        return direction;
    //    }
    //    set
    //    {
    //        direction = value;
    //        this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, direction));
    //    }
    //}

    public virtual void MagicMove()
    {
        
    }

    //몬스터의 방향으로 따라가는 마법은 타겟을 설정해준다.
    public void SetTarget(Weapon myweapon, baseMonster sc_monster)
    {
        this.MyWeapon = myweapon;
        targetmonster = sc_monster;
    }

    public void SetDirection(Vector3 direction)
    {

    }
    

    public virtual void Collision()
    {

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MagicMove();
    }
}
