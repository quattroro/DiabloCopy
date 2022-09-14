using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MonsterMove : MonoBehaviour
{

    public Player sc_player;
    public List<Node> MoveNodeList;
    public Vector3 direction;
    public float movespeed;
    public Vector3 startpos;
    public Vector3 targetpos;
    public bool Moving = false;
    public Vector3 curtarget;
    //public Node CurNode;
    public int Curindex = 0;

    public float Timeval = 1;

    [Header("NavMesh2D")]
    public NavMeshAgent2D NavAgent;

    [Header("탐지옵션")]
    public float DetectRadius;//탐지 거리
    public float DetectAngle;
    public LayerMask PlayerLayer;
    public float DetectTime;
    private float LastDetestTime;


    public MonsterAttack sc_Attack;
    public baseMonster sc_monster;

    private void Start()
    {
        NavAgent = GetComponent<NavMeshAgent2D>();
        sc_Attack = GetComponent<MonsterAttack>();
        sc_monster = GetComponent<baseMonster>();
    }

    //일정 시간에 한번씩 캐릭터가 탐지범위 안에 들어왔는지 확인한다.
    private void Update()
    {
        
    }

    //일정 시간에 한번씩 주변의 플레이어를 감지한다
    public void DetectPlayer()
    {
        if(Time.time-LastDetestTime>=DetectTime)
        {
            LastDetestTime = Time.time;

            //탐지범위에 플레이어가 있는지 판단
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, DetectRadius, new Vector2(0, 0), 0, PlayerLayer);

            //플레이어가 판단되면 정면벡터



            sc_Attack.StartAttack(sc_player, sc_monster);
        }
    }

    public void StartMove2(Vector3 start, Vector3 target)
    {
        direction = target - this.transform.position;
        sc_monster.SetDirection(direction.normalized);
        //목적지에 도착하면 상대방이 있는지 확인하고 상대방이 있으면 공격을 한다.
        if (direction.magnitude <= 0.5)
        {
            RaycastHit2D[] hit = Physics2D.CircleCastAll(sc_monster.attackcircle.transform.position, sc_monster.attackcircle.radius, new Vector2(0, 0), 0);
            foreach (RaycastHit2D a in hit)
            {
                if (a.transform.tag == "Player")
                {
                    Debug.Log("플레이어 감지");
                    Moving = false;
                    targetpos = a.point;
                    //sc_monster.State = MONSTERSTATE.ATTACK;
                    sc_player = a.transform.gameObject.GetComponent<Player>();
                    sc_Attack.StartAttack(sc_player, sc_monster);
                    return;
                }
            }
            Moving = false;
            sc_monster.State = MONSTERSTATE.IDLE;
            return;
        }

        startpos = start;
        targetpos = target;
        Moving = true;
        sc_monster.State = MONSTERSTATE.WALKING;
    }



    //astar없이 그냥 따라감
    public void NavMove()
    {


        //if (Moving)
        //{
        //    direction = targetpos - this.transform.position;//현재의 목표방향을 구하고 
        //                                                    //일정 거리 이상 근접했으면 현재타겟을 다음으로 바꿔주고 다시 움직인다 만약 마지막 노드까지 왔으면 진짜 타겟으로 움직여주고 끝

        //    if (direction.magnitude <= 0.5)
        //    {

        //        RaycastHit2D[] hit = Physics2D.CircleCastAll(sc_monster.attackcircle.transform.position, sc_monster.attackcircle.radius, new Vector2(0, 0), 0);
        //        foreach (RaycastHit2D a in hit)
        //        {
        //            if (a.transform.tag == "Player")
        //            {
        //                Debug.Log("플레이어 감지");
        //                Moving = false;
        //                targetpos = a.point;
        //                //sc_monster.State = MONSTERSTATE.ATTACK;
        //                sc_player = a.transform.gameObject.GetComponent<Player>();
        //                sc_Attack.StartAttack(sc_player, sc_monster);
        //                //GetComponent<MonsterAttack>().StartAttack(sc_player, sc_monster);
        //                return;
        //            }
        //        }
        //        Debug.Log("플레이어 감지못함");
        //        Moving = false;
        //        sc_monster.State = MONSTERSTATE.IDLE;
        //        return;
        //    }
        //    direction.Normalize();
        //    this.transform.position += direction * movespeed * Time.deltaTime;
        //}

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + sc_monster.WAYS[(int)sc_monster.Direction]);
        //DrawSolidArc(시작점, 노멀, 그려줄 방향벡터, 각도, 반지름)
        //Handles.DrawSolidArc
    }

}
