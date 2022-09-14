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

    [Header("Ž���ɼ�")]
    public float DetectRadius;//Ž�� �Ÿ�
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

    //���� �ð��� �ѹ��� ĳ���Ͱ� Ž������ �ȿ� ���Դ��� Ȯ���Ѵ�.
    private void Update()
    {
        
    }

    //���� �ð��� �ѹ��� �ֺ��� �÷��̾ �����Ѵ�
    public void DetectPlayer()
    {
        if(Time.time-LastDetestTime>=DetectTime)
        {
            LastDetestTime = Time.time;

            //Ž�������� �÷��̾ �ִ��� �Ǵ�
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, DetectRadius, new Vector2(0, 0), 0, PlayerLayer);

            //�÷��̾ �ǴܵǸ� ���麤��



            sc_Attack.StartAttack(sc_player, sc_monster);
        }
    }

    public void StartMove2(Vector3 start, Vector3 target)
    {
        direction = target - this.transform.position;
        sc_monster.SetDirection(direction.normalized);
        //�������� �����ϸ� ������ �ִ��� Ȯ���ϰ� ������ ������ ������ �Ѵ�.
        if (direction.magnitude <= 0.5)
        {
            RaycastHit2D[] hit = Physics2D.CircleCastAll(sc_monster.attackcircle.transform.position, sc_monster.attackcircle.radius, new Vector2(0, 0), 0);
            foreach (RaycastHit2D a in hit)
            {
                if (a.transform.tag == "Player")
                {
                    Debug.Log("�÷��̾� ����");
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



    //astar���� �׳� ����
    public void NavMove()
    {


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
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + sc_monster.WAYS[(int)sc_monster.Direction]);
        //DrawSolidArc(������, ���, �׷��� ���⺤��, ����, ������)
        //Handles.DrawSolidArc
    }

}
