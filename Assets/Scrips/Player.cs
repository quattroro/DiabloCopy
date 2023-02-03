using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DIRECTION { D1 = 1, D2, D3, D4, D5, D6, D7, D8, DIRECTIONMAX };//정면(아래) 부터 시계방향으로 돌아가는 8방향
public enum PLAYERSTATE { IDLE, WALKING, ATTACK, HIT, DEAD, STATEMAX };
public class Player : Status
{
    public Vector2[] WAYS;
    //public int[,] WAYS = new int[(int)DIRECTION.DIRECTIONMAX - 1, 2] { { -1, 0 }, { 1, 0 }, { 0, -1 }, { 0, 1 }, { -1, 1 }, { -1, -1 }, { 1, -1 }, { 1, 1 } };
    [Header("=====Player=====")]
    public bool Attack;
    public DIRECTION Direction;
    public CharacterMove MoveScript;
    public float degree;
    public Animator playeranimator;
    
    public baseMonster sc_targetmonster;
    public Transform targetmonster;
    
    public ItemBag itemBag = null;
    [Header("연결필요")]
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


    //데미지를 받아서 죽으면 
    public bool GetDemage(int damage)
    {
        //hp
        Debug.Log($"[MonsterAttack] 캐릭터 데미지 들어옴 {damage}");
        HPDown(damage);
        //this.CurHP -= damage;
        if(CurHP<=0)
        {
            //게임오버

        }
        return true;
    }


    //내부적으로 현재 있는 타일의 위치를 가지고 있어야 함
    //내가 있는 
    IEnumerator CreateCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);
            ///
        }
    }

    public void OpenItemBag()
    {
        //itemBag.OpenSlotWindow();

        //itemBag.Operate();
    }

    public void SetDirection(Vector3 normalidir)//방향 단위벡터를 구해주면 향하는 각도를 구해서 현재 움직이는 방향을 구해준다.
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

    public override void StartVirtual()
    {
        base.StartVirtual();

        MoveScript = GetComponent<CharacterMove>();
        playeranimator = GetComponent<Animator>();

        WAYS = new Vector2[(int)DIRECTION.DIRECTIONMAX];
        WAYS[(int)DIRECTION.D1] = new Vector2(0, -1).normalized;
        WAYS[(int)DIRECTION.D2] = new Vector2(-1, -1).normalized;
        WAYS[(int)DIRECTION.D3] = new Vector2(-1, 0).normalized;
        WAYS[(int)DIRECTION.D4] = new Vector2(-1, 1).normalized;
        WAYS[(int)DIRECTION.D5] = new Vector2(0, 1).normalized;
        WAYS[(int)DIRECTION.D6] = new Vector2(1, 1).normalized;
        WAYS[(int)DIRECTION.D7] = new Vector2(1, 0).normalized;
        WAYS[(int)DIRECTION.D8] = new Vector2(1, -1).normalized;
        SetDirection(WAYS[(int)DIRECTION.D1]);

        //AttackRange = 
        State = PLAYERSTATE.IDLE;
        //itemBag = new ItemBag("Bag", Item.ITEMTYPE.EQUIP);
        //itemBag = new ItemBag();
        MaxHP = 100;
        CurHP = 100;
        MaxMP = 100;
        CurMP = 100;

        HPBar hpbar = UIManager.Instance.GetUIInstance(UIManager.UITYPES.HP) as HPBar;
        hpbar.MaxVal = MaxHP;
        hpbar.CurVal = CurHP;
        MPBar mpbar = UIManager.Instance.GetUIInstance(UIManager.UITYPES.MP) as MPBar;
        mpbar.MaxVal = MaxMP;
        mpbar.CurVal = CurMP;
        //if (itemBag.gameObject.activeSelf == false)
        //{
        //    itemBag.gameObject.SetActive(true);
        //    Item item = new HPPotion("HPPotion", Item.ITEMTYPE.USEABLE, 1, 1);
        //    Debug.Log("플레이어 add실행");
        //    itemBag.Add(item);
        //    itemBag.gameObject.SetActive(false);
        //}
        //else
        //{
        //    Item item = new HPPotion("HPPotion", Item.ITEMTYPE.USEABLE, 1, 1);
        //    Debug.Log("플레이어 add실행");
        //    itemBag.Add(item);
        //}


    }

    //오른쪽클릭 이동 왼쪽클릭 아이템획득, 몬스터 공격
    //마우스 움직임 관리
    //마우스가 캐릭터 위에 올라가거나 
    //몬스터 공격하도록
    //public void MouseClick()
    //{

    //    //마우스 왼클릭
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        if (Input.mousePosition.y >= 144f)
    //        {

    //            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //            Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);


    //            RaycastHit2D[] hit = Physics2D.CircleCastAll(point, 0.1f, Vector2.zero, 0);
    //            foreach (RaycastHit2D a in hit)
    //            {
    //                if (a.transform.tag == "Wall")
    //                {
    //                    return;
    //                }

    //                if (a.transform.tag == "Enemy")
    //                {
    //                    //Debug.Log("Attackmove");
    //                    this.AttackMove(this.transform.position, a.point, a.transform);
    //                    return;
    //                }


    //            }
    //            //Debug.Log("nomalmove");
    //            this.Move(this.transform.position, hit[0].point);

    //        }

    //    }
    //}

}
