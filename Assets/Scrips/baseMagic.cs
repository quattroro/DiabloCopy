using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseMagic : MonoBehaviour
{
    public Weapon MyWeapon;//�ڽ��� �߻��� ���������� ������ �ִ´�
    public int Damage;
    public float CoolTime;
    protected baseMonster targetmonster;
    protected Vector3 direction;//���������� ���ͷ� �־�����.
    //DIRECTION direction;
    protected float degreedir;
    public float magicspeed;

    public float timeval;
    protected float lasttime;

    public Animator animator = null;
    //�̹��� ��ü�� ȸ����Ų��.
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

    //������ �������� ���󰡴� ������ Ÿ���� �������ش�.
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
