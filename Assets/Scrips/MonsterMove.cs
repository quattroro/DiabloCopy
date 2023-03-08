using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MonsterMove : MonoBehaviour
{
    ZombieFSM zombieFSM;
    public float patrolDis;
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

    public MonsterAttack sc_Attack;
    public baseMonster sc_monster;

    private IEnumerator movecor = null;

    private void Start()
    {
        NavAgent = GetComponent<NavMeshAgent2D>();
        sc_Attack = GetComponent<MonsterAttack>();
        sc_monster = GetComponent<baseMonster>();
        zombieFSM = GetComponent<ZombieFSM>();
    }

    //해당 방향으로 정해진 거리만큼 이동한다.
    //이동하기 전에 
    public void MoveStart(int dir)
    {
        direction = sc_monster.WAYS[dir];
        direction = transform.position + (direction * patrolDis);
        
        MoveStart(direction);
    }
    
    public bool CheckAttackRange()
    {
        RaycastHit2D[] hit = Physics2D.CircleCastAll(sc_monster.attackcircle.transform.position, sc_monster.attackcircle.radius, new Vector2(0, 0), 0);
        foreach (RaycastHit2D a in hit)
        {
            if (a.transform.tag == "Player")
            {
                //Debug.Log("[MonsterAttack]공격감지");
                Moving = false;
                targetpos = a.point;
                //sc_monster.State = MONSTERSTATE.ATTACK;
                sc_player = a.transform.gameObject.GetComponent<Player>();
                //AttackInfo temp = null;
                //sc_Attack.StartAttack(sc_player, temp);
                return true;
            }
        }

        sc_player = null;
        return false;
    }


    public void MoveStart(Vector3 target)
    {
        
        direction = target - this.transform.position;
        sc_monster.SetDirection(direction.normalized);

        //움직임을 시작하기 전에 이미 공격 상태가 아니면 상대방이 공격범위에 있는지 확인하고 상대방이 있으면 공격을 한다.
        //if(CheckAttackRange())
        //{
        //    AttackInfo temp = null;
        //    sc_Attack.StartAttack(sc_player, temp);
        //    return;
        //}

        //if (sc_monster.NowDetected && sc_monster.state != MONSTERSTATE.Attack)
        //{
        //    RaycastHit2D[] hit = Physics2D.CircleCastAll(sc_monster.attackcircle.transform.position, sc_monster.attackcircle.radius, new Vector2(0, 0), 0);
        //    foreach (RaycastHit2D a in hit)
        //    {
        //        if (a.transform.tag == "Player")
        //        {
        //            Debug.Log("[MonsterAttack]공격감지");
        //            Moving = false;
        //            targetpos = a.point;
        //            //sc_monster.State = MONSTERSTATE.ATTACK;
        //            sc_player = a.transform.gameObject.GetComponent<Player>();
        //            AttackInfo temp = null;
        //            sc_Attack.StartAttack(sc_player, temp);
        //            return;
        //        }
        //    }
        //}


        //startpos = start;
        targetpos = target;
        

        NavMove();
    }


    //NavMesh 사용
    public void NavMove()
    {
        Moving = true;
        
        //Debug.Log("네브메쉬 들어옴");

        if (movecor != null)
        {
            StopCoroutine(movecor);
            movecor = null;
            //sc_monster.State = MONSTERSTATE.IDLE;
            sc_monster.FSM.ChangeState(sc_monster.zFSM.idleStste);
        }

        sc_monster.AnimationState = MONSTERSTATE.Move;
        sc_monster.FSM.ChangeState(sc_monster.zFSM.moveState);

        movecor = nav();
        StartCoroutine(movecor);


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

    //목적지에 도착하거나 움직임이 끝날때까지 목표지점으로 이동해준다.
    IEnumerator nav()
    {
        while (true)
        {
            if(zombieFSM.GetCurstate != MONSTERSTATE.Move.ToString())
            {
                yield break;
            }

            //목적지에 도착 했거나 플레이어가 탐지거리를 벗어났으면 움직임을 끝내준다.
            if (NavAgent.Trace(transform.position, targetpos)/*|| !sc_monster.NowDetected*/)
            {
                movecor = null;
                Debug.Log("움직임 끝");
                zombieFSM.ChangeState(zombieFSM.idleStste);
                //sc_monster.State = MONSTERSTATE.IDLE;
                yield break;
            }


            //RaycastHit2D[] hit = Physics2D.CircleCastAll(sc_monster.attackcircle.transform.position, sc_monster.attackcircle.radius, new Vector2(0, 0), 0);
            //foreach (RaycastHit2D a in hit)
            //{
            //    if (a.transform.tag == "Player")
            //    {
            //        Debug.Log("[MonsterAttack]공격감지");
            //        Moving = false;
            //        targetpos = a.point;
            //        //sc_monster.State = MONSTERSTATE.ATTACK;
            //        sc_player = a.transform.gameObject.GetComponent<Player>();
            //        AttackInfo temp = null;
            //        sc_Attack.StartAttack(sc_player, temp);
            //        movecor = null;
            //        yield break;
            //    }
            //}

            Debug.DrawLine(transform.position, targetpos,Color.red);

            yield return null;
        }

    }

    private void OnDrawGizmos()
    {
        //if (sc_monster != null)
        //{
        //    Gizmos.color = Color.blue;
        //    Gizmos.DrawLine(transform.position, (Vector2)transform.position + sc_monster.WAYS[(int)sc_monster.Direction]);
        //    Gizmos.color = Color.white;
        //    Handles.DrawSolidArc(transform.position, new Vector3(0, 0, 1), sc_monster.WAYS[(int)sc_monster.Direction], sc_monster.DetectAngle / 2, 1);
        //    Handles.DrawSolidArc(transform.position, new Vector3(0, 0, 1), sc_monster.WAYS[(int)sc_monster.Direction], -sc_monster.DetectAngle / 2, 1);

        //}
    }

}
