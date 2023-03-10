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

            //������ ������ �÷��̾ ������ 
            if (basemonster.CkeckPlayer())
            {
                //sc_player = obj.GetComponentInChildren<Player>();


                //Idle �Ǵ� Move ���� Ž���� ������ �������� �����Ѵ�.
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



        ////�����̴� �� �϶� 
        //if(curState == moveState)
        //{
        //    //���ݹ����� �÷��̾ ���Դ��� ���������� Ȯ���ϰ� 
        //    //���ݹ��� �ȿ� �������� ������ ���ش�.


        //}

        ////�����ϴ� �� �϶�
        //if(curState == attackState)
        //{
        //    //�÷��̾ ���ݹ��� ������ �������� 
        //}


    }


    

    ////���� �ð��� �ѹ��� �ֺ��� �÷��̾ �����Ѵ�
    //public GameObject DetectPlayer()
    //{
    //    GameObject obj = null;
    //    Vector2 direction;


    //    if (Time.time - LastDetestTime >= DetectTime)
    //    {
    //        LastDetestTime = Time.time;

    //        ////Ž�������� �÷��̾ �ִ��� �Ǵ�
    //        //RaycastHit2D hit = Physics2D.CircleCast(transform.position, DetectRadius, new Vector2(0, 0), 0, PlayerLayer);

    //        //if (hit.collider != null)
    //        //{
    //        //    //�÷��̾ �ǴܵǸ� ���麤�Ϳ��� �������� ������ ���ؼ� �����̸� Ž��
    //        //    direction = hit.transform.position - this.transform.position;
    //        //    direction.Normalize();

    //        //    DetectedAngle = Mathf.Acos(Vector3.Dot(WAYS[(int)Direction], direction)) * 180.0f / 3.14f;
    //        //    if (DetectedAngle <= DetectAngle)
    //        //    {
    //        //        obj = hit.transform.gameObject;
    //        //    }
    //        //    Debug.Log("�÷��̾� ����");
    //        //}

    //        //������ ������ �÷��̾ ������ 
    //        if (CkeckPlayer())
    //        {
    //            //sc_player = obj.GetComponentInChildren<Player>();


    //            //Idle �Ǵ� Move ���� Ž���� ������ �������� �����Ѵ�.
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


    //        ////Ž�������� �÷��̾ �ִ��� �Ǵ�
    //        //RaycastHit2D hit = Physics2D.CircleCast(transform.position, DetectRadius, new Vector2(0, 0), 0, PlayerLayer);

    //        //if (hit.collider != null)
    //        //{
    //        //    //�÷��̾ �ǴܵǸ� ���麤�Ϳ��� �������� ������ ���ؼ� �����̸� Ž��
    //        //    direction = hit.transform.position - this.transform.position;
    //        //    direction.Normalize();

    //        //    DetectedAngle = Mathf.Acos(Vector3.Dot(WAYS[(int)Direction], direction)) * 180.0f / 3.14f;
    //        //    if (DetectedAngle <= DetectAngle)
    //        //    {
    //        //        obj = hit.transform.gameObject;
    //        //    }
    //        //    Debug.Log("�÷��̾� ����");
    //        //}

    //        ////������ ������ �÷��̾ ������ 
    //        //if (obj != null)
    //        //{
    //        //    sc_player = obj.GetComponentInChildren<Player>();
    //        //    NowDetected = true;

    //        //    //Idle �Ǵ� Move ���� Ž���� ������ �������� �����Ѵ�.
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
