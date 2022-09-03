using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [Header("�����ʿ�")]
    public MonsterAttack sc_Attack;
    public baseMonster sc_monster;
    

    //�������� ������ ������ Astar�� �̿��� ���迭�� �޾ƿ��� ���� �߽���ǥ�� ���� ��ĭ�� �̵��Ѵ�.
    //public void StartMove(Vector3 start, Vector3 target)
    //{
    //    startpos = start;
    //    targetpos = target;

    //    MoveNodeList = MoveAstar.GetI.PathFinding(start, target);
    //    Curindex = 1;
    //    curtarget = MapManager.GetI.MyGetCellCenterWorld(MoveNodeList[Curindex].x, MoveNodeList[Curindex].y);

    //    Curindex++;
    //    Moving = true;
    //    //sc_player.playeranimator.SetBool("Walking", true);
    //    sc_monster.State = MONSTERSTATE.WALKING;
    //    direction = target - start;
    //    sc_monster.SetDirection(direction.normalized);
    //}

    ////õ��° ����� ��ġ�� �����̰� �� �������� ���� ��� �������� ���� ��� �׷� ������ ������ ����� ��ġ���� ���� ó�� �Էµ� Ÿ�ٳ��� �̵�
    //public void CMove()
    //{
    //    if (Moving)
    //    {
    //        direction = curtarget - this.transform.position;//������ ��ǥ������ ���ϰ� 
    //                                                        //���� �Ÿ� �̻� ���������� ����Ÿ���� �������� �ٲ��ְ� �ٽ� �����δ� ���� ������ ������ ������ ��¥ Ÿ������ �������ְ� ��

    //        if (direction.magnitude <= 0.05)
    //        {
    //            this.transform.position = curtarget;
    //            if (this.transform.position == targetpos)
    //            {
    //                Moving = false;
    //                //sc_player.playeranimator.SetBool("Walking", false);
    //                sc_monster.State = MONSTERSTATE.IDLE;
    //                return;
    //            }
    //            if (MoveNodeList.Count - 1 <= Curindex)
    //            {
    //                curtarget = targetpos;
    //            }
    //            else
    //            {

    //                curtarget = MapManager.GetI.MyGetCellCenterWorld(MoveNodeList[Curindex].x, MoveNodeList[Curindex].y);
    //                Curindex++;
    //            }
    //        }
    //        direction.Normalize();
    //        this.transform.position += direction * movespeed * Time.deltaTime;
    //    }


    //}


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
        //MoveNodeList = MoveAstar.GetI.PathFinding(start, target);
        //Curindex = 1;
        //curtarget = MapManager.GetI.MyGetCellCenterWorld(MoveNodeList[Curindex].x, MoveNodeList[Curindex].y);

        //Curindex++;

        //sc_player.playeranimator.SetBool("Walking", true);

        startpos = start;
        targetpos = target;
        Moving = true;
        sc_monster.State = MONSTERSTATE.WALKING;
        //sc_monster.SetDirection(direction.normalized);
    }



    //astar���� �׳� ����
    public void CMove2()
    {
        if (Moving)
        {
            direction = targetpos - this.transform.position;//������ ��ǥ������ ���ϰ� 
                                                            //���� �Ÿ� �̻� ���������� ����Ÿ���� �������� �ٲ��ְ� �ٽ� �����δ� ���� ������ ������ ������ ��¥ Ÿ������ �������ְ� ��

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
                        //GetComponent<MonsterAttack>().StartAttack(sc_player, sc_monster);
                        return;
                    }
                }
                Debug.Log("�÷��̾� ��������");
                Moving = false;
                sc_monster.State = MONSTERSTATE.IDLE;
                return;
            }
            direction.Normalize();
            this.transform.position += direction * movespeed * Time.deltaTime;
        }
        //if (Time.time >= curtime)
        //{
        //    curtime = Time.time + Timeval;

        //}

    }


    // Start is called before the first frame update
    void Start()
    {
        //sc_monster = GetComponent<baseMonster>();
        //sc_player = sc_monster.sc_player;
        //sc_monster = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        //CMove();
        CMove2();
    }
}
