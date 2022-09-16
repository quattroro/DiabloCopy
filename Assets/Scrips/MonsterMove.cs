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




    public MonsterAttack sc_Attack;
    public baseMonster sc_monster;

    private void Start()
    {
        NavAgent = GetComponent<NavMeshAgent2D>();
        sc_Attack = GetComponent<MonsterAttack>();
        sc_monster = GetComponent<baseMonster>();
    }


    public void MoveStart(Vector3 start, Vector3 target)
    {
        direction = target - this.transform.position;
        sc_monster.SetDirection(direction.normalized);

        //�������� �����ϱ� ���� �̹� ���� ���°� �ƴϸ� ������ ���ݹ����� �ִ��� Ȯ���ϰ� ������ ������ ������ �Ѵ�.
        if (sc_monster.NowDetected && sc_monster.state != MONSTERSTATE.ATTACK)
        {
            RaycastHit2D[] hit = Physics2D.CircleCastAll(sc_monster.attackcircle.transform.position, sc_monster.attackcircle.radius, new Vector2(0, 0), 0);
            foreach (RaycastHit2D a in hit)
            {
                if (a.transform.tag == "Player")
                {
                    Debug.Log("���ݰ���");
                    Moving = false;
                    targetpos = a.point;
                    //sc_monster.State = MONSTERSTATE.ATTACK;
                    sc_player = a.transform.gameObject.GetComponent<Player>();
                    sc_Attack.StartAttack(sc_player, sc_monster);
                    return;
                }
            }
        }


        startpos = start;
        targetpos = target;
        

        NavMove();
    }


    //NavMesh ���
    public void NavMove()
    {
        Moving = true;
        sc_monster.State = MONSTERSTATE.WALKING;
        Debug.Log("�׺�޽� ����");
        NavAgent.SetDestination(targetpos);

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

    private void OnDrawGizmos()
    {
        if (sc_monster != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + sc_monster.WAYS[(int)sc_monster.Direction]);
            Gizmos.color = Color.white;
            Handles.DrawSolidArc(transform.position, new Vector3(0, 0, 1), sc_monster.WAYS[(int)sc_monster.Direction], sc_monster.DetectAngle / 2, 1);
            Handles.DrawSolidArc(transform.position, new Vector3(0, 0, 1), sc_monster.WAYS[(int)sc_monster.Direction], -sc_monster.DetectAngle / 2, 1);

        }
    }

}
