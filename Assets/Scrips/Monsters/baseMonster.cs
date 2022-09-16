using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MONSTERSTATE { IDLE, WALKING, ATTACK, HIT, DEAD, STATEMAX };

public class baseMonster : Status
{
    public Vector2[] WAYS;
    [Header("=====BaseMonster=====")]
    public bool Attack;
    public DIRECTION Direction;


    public float degree;
    public CircleCollider2D collcircle;
    public CircleCollider2D attackcircle;
    public Vector3 targetpos;

    public float DetectTimeVal;
    private float LastDetectTime;

    public Vector3 test1;
    public float testradi;

    public CircleCollider2D testCircle;

    public Player sc_player = null;
    public MONSTERSTATE state;
    public Animator MonsterAnimator;
    [Header("연결필요")]
    public MonsterMove MoveScript;

    [Header("탐지옵션")]
    public float DetectRadius;//탐지 거리
    public float DetectAngle = 10f;
    public LayerMask PlayerLayer;
    public float DetectTime;
    public bool NowDetected;
    private float LastDetestTime;


    public float DetectedAngle = 0;
    public Player DetectedPlayer = null;


    public void Init()
    {
        MoveScript = GetComponent<MonsterMove>();
        MonsterAnimator = GetComponentInChildren<Animator>();
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
        
        //{ { -1, 0 }, { 1, 0 }, { 0, -1 }, { 0, 1 }, { -1, 1 }, { -1, -1 }, { 1, -1 }, { 1, 1 } }
    }

    public MONSTERSTATE State
    {
        get
        {
            return state;
        }
        set
        {
            for (MONSTERSTATE i = MONSTERSTATE.IDLE; i < MONSTERSTATE.STATEMAX; i++)
            {
                if (value == i)
                {
                    MonsterAnimator.SetBool(i.ToString(), true);
                }
                else
                {
                    MonsterAnimator.SetBool(i.ToString(), false);
                }
            }
            state = value;
        }
    }



    public void SetDirection(Vector3 normalidir)//방향 단위벡터를 구해주면 향하는 각도를 구해서 현재 움직이는 방향을 구해준다.
    {
        float radian = Mathf.Atan2(normalidir.y, normalidir.x);
        degree = radian * Mathf.Rad2Deg;
        if (degree < 0)
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
            else if (degree <= 180)
            {
                Direction = DIRECTION.D3;
            }
        }
        MonsterAnimator.SetInteger("direction", (int)Direction);
    }
    public void Move(Vector3 strat, Vector3 target)
    {
        //MoveScript.StartMove(strat, target);
        MoveScript.MoveStart(strat, target);
    }

    public GameObject DropItem()
    {
        GameObject temp = new GameObject();
        return temp;
    }

    //public void DetecePlayer()
    //{
    //    //Debug.Log("플레이어 감지");
    //    //일정 시간에 한번씩 감지 
    //    if(Time.time>=LastDetectTime)
    //    {
    //        //count++;
    //        //Debug.Log($"{count}");
    //        LastDetectTime = Time.time + DetectTimeVal;
    //        test1 = collcircle.transform.position;
    //        testradi = collcircle.radius;

    //        RaycastHit2D[] hit = Physics2D.CircleCastAll(collcircle.transform.position, collcircle.radius, new Vector2(1, 1), 0);

    //        foreach(RaycastHit2D a in hit)
    //        {
    //            if (a.transform.tag == "Player")
    //            {
    //                //Debug.Log("플레이어 감지");
    //                if(State!=MONSTERSTATE.WALKING|| State != MONSTERSTATE.ATTACK)
    //                {
    //                    targetpos = a.point;
    //                    this.Move(this.transform.position, targetpos);
    //                }
    //            }
    //        }
    //    }
    //}

    //일정 시간에 한번씩 주변의 플레이어를 감지한다
    public GameObject DetectPlayer()
    {
        GameObject obj = null;
        Vector2 direction;
        //RaycastHit2D hit = null;
        if (Time.time - LastDetestTime >= DetectTime)
        {
            LastDetestTime = Time.time;

            //탐지범위에 플레이어가 있는지 판단
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, DetectRadius, new Vector2(0, 0), 0, PlayerLayer);

            if (hit.collider != null)
            {
                //플레이어가 판단되면 정면벡터와의 내적으로 각도를 구해서 정면이면 탐지
                direction = hit.transform.position - this.transform.position;
                direction.Normalize();

                DetectedAngle = Mathf.Acos(Vector3.Dot(WAYS[(int)Direction], direction)) * 180.0f / 3.14f;
                if (DetectedAngle <= DetectAngle)
                {
                    obj = hit.transform.gameObject;
                }
                Debug.Log("플레이어 감지");
            }

            if (obj != null)
            {
                if (state != MONSTERSTATE.ATTACK || state != MONSTERSTATE.WALKING)
                {
                    sc_player = obj.GetComponentInChildren<Player>();
                    NowDetected = true;
                    MoveScript.MoveStart(transform.position, sc_player.transform.position);
                }

            }
            else
            {
                sc_player = null;
                NowDetected = false;
            }
        }



        return obj;
    }

    // Start is called before the first frame update
    //void Start()
    //{
    //    StartVirtual();

    //}

    public override void StartVirtual()
    {
        base.StartVirtual();

        sc_player = GameObject.FindObjectOfType<Player>();
        Init();
    }
}
