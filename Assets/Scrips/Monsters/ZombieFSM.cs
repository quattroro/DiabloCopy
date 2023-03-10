using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieFSM : StateMachine
{
    public baseMonster basemonster;

    public Idle idleStste;
    public Move moveState;
    public Attack attackState;
    public Die dieState;
    public Attacked attackedState;

    public MonsterAttack zombieAttack;
    public MonsterMove zombieMove;



    private void Awake()
    {
        basemonster = GetComponent<baseMonster>();

        idleStste = new Idle(this);
        moveState = new Move(this);
        attackState = new Attack(this);
        dieState = new Die(this);
        attackedState = new Attacked(this);


        zombieAttack = GetComponent<MonsterAttack>();
        zombieMove = GetComponent<MonsterMove>();
    }


    float lastTime;
    float detectTime = 2.0f;

    public override void Update()
    {
        if ((Time.time - lastTime) >= detectTime)
        {
            lastTime = Time.time;

            //범위에 감지된 플레이어가 있으면 
            if (basemonster.CkeckPlayer())
            {
                //sc_player = obj.GetComponentInChildren<Player>();


                //Idle 또는 Move 때는 탐지한 쪽으로 움직임을 시작한다.
                if(GetCurstate == MONSTERSTATE.Idle.ToString()||GetCurstate == MONSTERSTATE.Move.ToString())
                {
                    basemonster.Move(basemonster.sc_player.transform.position);
                }

                //if (FSM.GetCurstate == MONSTERSTATE.Idle.ToString() || FSM.GetCurstate == MONSTERSTATE.Move.ToString())
                //{
                //    moveScript.MoveStart(sc_player.transform.position);
                //}

                //if (state != MONSTERSTATE.Attack || state != MONSTERSTATE.Move)
                //{
                //    sc_player = obj.GetComponentInChildren<Player>();
                //    NowDetected = true;
                //    moveScript.MoveStart(sc_player.transform.position);
                //}

            }
            else
            {
                basemonster.sc_player = null;
                basemonster.NowDetected = false;
            }

        }

        base.Update();

        if (Input.GetKeyDown(KeyCode.B))
        {
            ChangeState(idleStste);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeState(moveState);
        }



        ////움직이는 중 일때 
        //if(curState == moveState)
        //{
        //    //공격범위에 플레이어가 들어왔는지 지속적으로 확인하고 
        //    //공격범위 안에 들어왔으면 공격을 해준다.


        //}

        ////공격하는 중 일때
        //if(curState == attackState)
        //{
        //    //플레이어가 공격범위 밖으로 나갔으면 
        //}


    }


    

    ////일정 시간에 한번씩 주변의 플레이어를 감지한다
    //public GameObject DetectPlayer()
    //{
    //    GameObject obj = null;
    //    Vector2 direction;


    //    if (Time.time - LastDetestTime >= DetectTime)
    //    {
    //        LastDetestTime = Time.time;

    //        ////탐지범위에 플레이어가 있는지 판단
    //        //RaycastHit2D hit = Physics2D.CircleCast(transform.position, DetectRadius, new Vector2(0, 0), 0, PlayerLayer);

    //        //if (hit.collider != null)
    //        //{
    //        //    //플레이어가 판단되면 정면벡터와의 내적으로 각도를 구해서 정면이면 탐지
    //        //    direction = hit.transform.position - this.transform.position;
    //        //    direction.Normalize();

    //        //    DetectedAngle = Mathf.Acos(Vector3.Dot(WAYS[(int)Direction], direction)) * 180.0f / 3.14f;
    //        //    if (DetectedAngle <= DetectAngle)
    //        //    {
    //        //        obj = hit.transform.gameObject;
    //        //    }
    //        //    Debug.Log("플레이어 감지");
    //        //}

    //        //범위에 감지된 플레이어가 있으면 
    //        if (CkeckPlayer())
    //        {
    //            //sc_player = obj.GetComponentInChildren<Player>();


    //            //Idle 또는 Move 때는 탐지한 쪽으로 움직임을 시작한다.
    //            if (FSM.GetCurstate == MONSTERSTATE.Idle.ToString() || FSM.GetCurstate == MONSTERSTATE.Move.ToString())
    //            {
    //                moveScript.MoveStart(sc_player.transform.position);
    //            }

    //            //if (state != MONSTERSTATE.Attack || state != MONSTERSTATE.Move)
    //            //{
    //            //    sc_player = obj.GetComponentInChildren<Player>();
    //            //    NowDetected = true;
    //            //    moveScript.MoveStart(sc_player.transform.position);
    //            //}

    //        }
    //        else
    //        {
    //            sc_player = null;
    //            NowDetected = false;
    //        }


    //        ////탐지범위에 플레이어가 있는지 판단
    //        //RaycastHit2D hit = Physics2D.CircleCast(transform.position, DetectRadius, new Vector2(0, 0), 0, PlayerLayer);

    //        //if (hit.collider != null)
    //        //{
    //        //    //플레이어가 판단되면 정면벡터와의 내적으로 각도를 구해서 정면이면 탐지
    //        //    direction = hit.transform.position - this.transform.position;
    //        //    direction.Normalize();

    //        //    DetectedAngle = Mathf.Acos(Vector3.Dot(WAYS[(int)Direction], direction)) * 180.0f / 3.14f;
    //        //    if (DetectedAngle <= DetectAngle)
    //        //    {
    //        //        obj = hit.transform.gameObject;
    //        //    }
    //        //    Debug.Log("플레이어 감지");
    //        //}

    //        ////범위에 감지된 플레이어가 있으면 
    //        //if (obj != null)
    //        //{
    //        //    sc_player = obj.GetComponentInChildren<Player>();
    //        //    NowDetected = true;

    //        //    //Idle 또는 Move 때는 탐지한 쪽으로 움직임을 시작한다.
    //        //    if (FSM.GetCurstate == MONSTERSTATE.Idle.ToString() || FSM.GetCurstate == MONSTERSTATE.Move.ToString())
    //        //    {
    //        //        moveScript.MoveStart(sc_player.transform.position);
    //        //    }

    //        //    //if (state != MONSTERSTATE.Attack || state != MONSTERSTATE.Move)
    //        //    //{
    //        //    //    sc_player = obj.GetComponentInChildren<Player>();
    //        //    //    NowDetected = true;
    //        //    //    moveScript.MoveStart(sc_player.transform.position);
    //        //    //}

    //        //}
    //        //else
    //        //{
    //        //    sc_player = null;
    //        //    NowDetected = false;
    //        //}
    //    }
    //    return obj;
    //}



}
