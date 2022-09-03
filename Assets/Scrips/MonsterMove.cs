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

    [Header("연결필요")]
    public MonsterAttack sc_Attack;
    public baseMonster sc_monster;
    

    //시작점과 끝점이 들어오면 Astar를 이용해 노드배열을 받아오고 셀의 중심좌표를 따라서 한칸씩 이동한다.
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

    ////천번째 노드의 위치로 움직이고 그 다음에는 다음 노드 다음에는 다음 노드 그런 식으로 마지막 노드의 위치까지 오면 처음 입력된 타겟노드로 이동
    //public void CMove()
    //{
    //    if (Moving)
    //    {
    //        direction = curtarget - this.transform.position;//현재의 목표방향을 구하고 
    //                                                        //일정 거리 이상 근접했으면 현재타겟을 다음으로 바꿔주고 다시 움직인다 만약 마지막 노드까지 왔으면 진짜 타겟으로 움직여주고 끝

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



    //astar없이 그냥 따라감
    public void CMove2()
    {
        if (Moving)
        {
            direction = targetpos - this.transform.position;//현재의 목표방향을 구하고 
                                                            //일정 거리 이상 근접했으면 현재타겟을 다음으로 바꿔주고 다시 움직인다 만약 마지막 노드까지 왔으면 진짜 타겟으로 움직여주고 끝

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
                        //GetComponent<MonsterAttack>().StartAttack(sc_player, sc_monster);
                        return;
                    }
                }
                Debug.Log("플레이어 감지못함");
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
