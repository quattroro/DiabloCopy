using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DIRECTION { D1 = 1, D2, D3, D4, D5, D6, D7, D8, DIRECTIONMAX };//���� ���� �ð�������� ���ư��� 8����
public enum PLAYERSTATE { IDLE, WALKING, ATTACK, HIT, DEAD, STATEMAX };
public class Player : Status
{
    public int[,] WAYS = new int[(int)DIRECTION.DIRECTIONMAX - 1, 2] { { -1, 0 }, { 1, 0 }, { 0, -1 }, { 0, 1 }, { -1, 1 }, { -1, -1 }, { 1, -1 }, { 1, 1 } };
    [Header("=====Player=====")]
    public bool Attack;
    public DIRECTION Direction;
    public CharacterMove MoveScript;
    public float degree;
    public Animator playeranimator;
    
    public baseMonster sc_targetmonster;
    public Transform targetmonster;
    
    public ItemBag itemBag = null;
    [Header("�����ʿ�")]
    public CircleCollider2D AttackRange;
    public Weapon weapon = null;
    [SerializeField]
    protected PLAYERSTATE state;
    

    public Transform TargetMonster
    {
        get
        {
            return targetmonster;
        }
        set
        {
            if(value==null)
            {
                targetmonster = null;
                sc_targetmonster = null;
            }
            else
            {
                targetmonster = value;
                sc_targetmonster = targetmonster.GetComponent<baseMonster>();
            }
            
        }
    }
    public PLAYERSTATE State
    {
        get
        {
            return state;
        }
        set
        {
            for (PLAYERSTATE i = PLAYERSTATE.IDLE; i < PLAYERSTATE.STATEMAX; i++)
            {
                if (value==i)
                {
                    playeranimator.SetBool(i.ToString(), true);
                }
                else
                {
                    playeranimator.SetBool(i.ToString(), false);
                }
            }
            state = value;
        }
    }


    //�������� �޾Ƽ� ������ 
    public bool GetDemage(int damage)
    {
        //hp
        this.CurHP -= damage;
        if(CurHP<=0)
        {
            //���ӿ���

        }
        return true;
    }


    //���������� ���� �ִ� Ÿ���� ��ġ�� ������ �־�� ��
    //���� �ִ� 
    IEnumerator CreateCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);
            ///
        }
    }

    private void Awake()
    {
        
    }

    public void OpenItemBag()
    {
        //itemBag.OpenSlotWindow();

        //itemBag.Operate();
    }

    public void SetDirection(Vector3 normalidir)//���� �������͸� �����ָ� ���ϴ� ������ ���ؼ� ���� �����̴� ������ �����ش�.
    {
        float radian = Mathf.Atan2(normalidir.y,normalidir.x);
        degree = radian * Mathf.Rad2Deg;
        if(degree<0)
        {
            if (degree >= -20)
            {
                Direction = DIRECTION.D7;
            }
            else if (degree >= -65)
            {
                Direction = DIRECTION.D8;
            }
            else if (degree >= -110)
            {
                Direction = DIRECTION.D1;
            }
            else if (degree >= -155)
            {
                Direction = DIRECTION.D2;
            }
            else if (degree >= -180)
            {
                Direction = DIRECTION.D3;
            }
        }
        else
        {
            if (degree <= 25)
            {
                Direction = DIRECTION.D7;
            }
            else if (degree <= 70)
            {
                Direction = DIRECTION.D6;
            }
            else if (degree <= 115)
            {
                Direction = DIRECTION.D5;
            }
            else if (degree <= 160)
            {
                Direction = DIRECTION.D4;
            }
            else if(degree<=180)
            {
                Direction = DIRECTION.D3;
            }
        }
        playeranimator.SetInteger("direction", (int)Direction);
    }
    public void Move(Vector3 strat, Vector3 target)
    {
        
        if (this.targetmonster != null)
        {
            this.TargetMonster = null;
        }
        MoveScript.StartMove( strat,  target);
    }


    //public void AttackMove(Vector3 strat, Vector3 target,baseMonster targetmonster)
    //{
    //    this.sc_targetmonster = targetmonster;
    //    MoveScript.StartMove(strat, target);
    //}

    public void AttackMove(Vector3 strat, Vector3 target, Transform targetmonster)
    {
        TargetMonster = targetmonster;
        //this.targetmonster = targetmonster;
        //sc_targetmonster = targetmonster.GetComponent<baseMonster>();
        MoveScript.StartMove(strat, target);
    }
    //public void AttackMove(Vector3 start, Vector3 target)
    //{

    //}


    // Start is called before the first frame update
    void Start()
    {
        MoveScript = GetComponent<CharacterMove>();
        playeranimator = GetComponent<Animator>();
        //AttackRange = 
        State = PLAYERSTATE.IDLE;
        //itemBag = new ItemBag("Bag", Item.ITEMTYPE.EQUIP);
        //itemBag = new ItemBag();
        MaxHP = 100;
        curhp = 100;
        MaxMP = 100;
        curmp = 100;
        //if (itemBag.gameObject.activeSelf == false)
        //{
        //    itemBag.gameObject.SetActive(true);
        //    Item item = new HPPotion("HPPotion", Item.ITEMTYPE.USEABLE, 1, 1);
        //    Debug.Log("�÷��̾� add����");
        //    itemBag.Add(item);
        //    itemBag.gameObject.SetActive(false);
        //}
        //else
        //{
        //    Item item = new HPPotion("HPPotion", Item.ITEMTYPE.USEABLE, 1, 1);
        //    Debug.Log("�÷��̾� add����");
        //    itemBag.Add(item);
        //}


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
