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

    //�ش� �������� ������ �Ÿ���ŭ �̵��Ѵ�.
    //�̵��ϱ� ���� 
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
                //Debug.Log("[MonsterAttack]���ݰ���");
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

        //�������� �����ϱ� ���� �̹� ���� ���°� �ƴϸ� ������ ���ݹ����� �ִ��� Ȯ���ϰ� ������ ������ ������ �Ѵ�.
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
        //            Debug.Log("[MonsterAttack]���ݰ���");
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


    //NavMesh ���
    public void NavMove()
    {
        Moving = true;
        
        //Debug.Log("�׺�޽� ����");

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
        //    direction = targetpos - this.transform.position;//������ ��ǥ������ ���ϰ� 
        //                                                    //���� �Ÿ� �̻� ���������� ����Ÿ���� �������� �ٲ��ְ� �ٽ� �����δ� ���� ������ ������ ������ ��¥ Ÿ������ �������ְ� ��

        //    if (direction.magnitude <= 0.5)
        //    {

        //        RaycastHit2D[] hit = Physics2D.CircleCastAll(sc_monster.attackcircle.transform.position, sc_monster.attackcircle.radius, new Vector2(0, 0), 0);
        //        foreach (RaycastHit2D a in hit)
        //        {
        //            if (a.transform.tag == "Player")
        //            {
        //                Debug.Log("�÷��̾� ����");
        //                Moving = false;
        //                targetpos = a.point;
        //                //sc_monster.State = MONSTERSTATE.ATTACK;
        //                sc_player = a.transform.gameObject.GetComponent<Player>();
        //                sc_Attack.StartAttack(sc_player, sc_monster);
        //                //GetComponent<MonsterAttack>().StartAttack(sc_player, sc_monster);
        //                return;
        //            }
        //        }
        //        Debug.Log("�÷��̾� ��������");
        //        Moving = false;
        //        sc_monster.State = MONSTERSTATE.IDLE;
        //        return;
        //    }
        //    direction.Normalize();
        //    this.transform.position += direction * movespeed * Time.deltaTime;
        //}

    }

    //�������� �����ϰų� �������� ���������� ��ǥ�������� �̵����ش�.
    IEnumerator nav()
    {
        while (true)
        {
            if(zombieFSM.GetCurstate != MONSTERSTATE.Move.ToString())
            {
                yield break;
            }

            //�������� ���� �߰ų� �÷��̾ Ž���Ÿ��� ������� �������� �����ش�.
            if (NavAgent.Trace(transform.position, targetpos)/*|| !sc_monster.NowDetected*/)
            {
                movecor = null;
                Debug.Log("������ ��");
                zombieFSM.ChangeState(zombieFSM.idleStste);
                //sc_monster.State = MONSTERSTATE.IDLE;
                yield break;
            }


            //RaycastHit2D[] hit = Physics2D.CircleCastAll(sc_monster.attackcircle.transform.position, sc_monster.attackcircle.radius, new Vector2(0, 0), 0);
            //foreach (RaycastHit2D a in hit)
            //{
            //    if (a.transform.tag == "Player")
            //    {
            //        Debug.Log("[MonsterAttack]���ݰ���");
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
