using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    private Player sc_player;
    public List<Node> MoveNodeList;
    public Vector3 direction;
    public float movespeed;
    public Vector3 startpos;
    public Vector3 targetpos;
    public bool Moving = false;
    public Vector3 curtarget;
    public int Curindex = 0;

    public float Timeval = 1;
    float curtime = 0;

    IEnumerator MoveCor;


    AstarModule astarModule;

    //시작점과 끝점이 들어오면 해당 점들 사이에 장애물이 있는지 검사하고
    //장애물이 없으면 그냥 가고 장애물이 있으면 A-star 연산 실행
    //시작점과 끝점이 들어오면 Astar를 이용해 노드배열을 받아오고 셀의 중심좌표를 따라서 한칸씩 이동한다.
    public void StartMove(Vector3 start, Vector3 target)
    {
        //일단 공격범위 안에 몬스터가 있는지 확인 한다. 몬스터가 있으면 움직이지 않고 공격을 실행
        if (sc_player.targetmonster != null)
        {
            RaycastHit2D[] hitarr = Physics2D.CircleCastAll(sc_player.AttackRange.transform.position, sc_player.AttackRange.radius, new Vector2(0, 0), 0,sc_player.MonsterLayer);
            foreach (RaycastHit2D a in hitarr)
            {
                //if (a.transform.gameObject.layer == sc_player.MonsterLayer)
                {
                    if (sc_player.targetmonster == a.transform)
                    {
                        direction = a.point - (Vector2)this.transform.position;
                        sc_player.SetDirection(direction);//공격하기 전에 마지막으로 방향을 정해준다.
                        Moving = false;
                        sc_player.weapon.Attack();
                        Debug.Log("플레이어 공격 시작");
                        return;
                    }
                }
                

            }
        }


        startpos = start;
        targetpos = target;

        if(!CheckWall(startpos, targetpos))
        {
            //Debug.Log("벽 없음");
            curtarget = targetpos;
            //Moving = true;

        }
        else
        {
            //MoveNodeList = AstarModule.Instance.PathFinding(start, target);
            if (astarModule == null)
                astarModule = GameManager.Instance.GetAstarModule();

            MoveNodeList = astarModule.PathFinding(start, target);

            if (MoveNodeList == null)
            {
                Moving = false;
                if (MoveCor != null)
                    StopCoroutine(MoveCor);
                MoveCor = null;
                return;
            }

            Curindex = 1;
            curtarget = MapManager.Instance.MyGetCellCenterWorld(MoveNodeList[Curindex].x, MoveNodeList[Curindex].y);
            Curindex++;
        }

        Moving = true;
        //sc_player.playeranimator.SetBool("Walking", true);
        sc_player.State = PLAYERSTATE.WALKING;
        direction = target - start;
        sc_player.SetDirection(direction.normalized);

        if(MoveCor!=null)
        {
            StopCoroutine(MoveCor);
            MoveCor = null;
        }

        MoveCor = CMove();
        StartCoroutine(MoveCor);
    }    
    
    public bool CheckWall(Vector2 start, Vector2 dest)
    {

        bool result = false;
        float dis = (dest - start).magnitude;
        Vector2 dir = (dest - start).normalized;

        //시작점에서 끝점까지 Ray를 쏴서 중간에 벽이 있는지 확인한다.
        RaycastHit2D[] hit = Physics2D.RaycastAll(start, dir,dis);
        foreach(var a in hit)
        {
            if(a.transform.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                result = true;
                break;
            }   
        }
        //Debug.DrawLine(start, dest, Color.red);
        //RaycastHit2D hit = Physics2D.Raycast(startpos, dir, dis, LayerMask.NameToLayer("Wall"));
        //if (hit.Length > 0)
        //    result = true;

        return result;
    }

    //천번째 노드의 위치로 움직이고 그 다음에는 다음 노드 다음에는 다음 노드 그런 식으로 마지막 노드의 위치까지 오면 처음 입력된 타겟노드로 이동
    public IEnumerator CMove()
    {
        while(true)
        {
            if (!Moving)
            {
                MoveCor = null;
                yield break;
            }

            //몬스터를 목표로 움직일때는 움직일때마다 공격범위에 몬스터가 들어왔는지 확인한다.
            if (sc_player.targetmonster != null)
            {
                //StartMove(this.transform.position, sc_player.sc_targetmonster.transform.position);
                RaycastHit2D[] hit = Physics2D.CircleCastAll(sc_player.AttackRange.transform.position, sc_player.AttackRange.radius, new Vector2(0, 0), 0, sc_player.MonsterLayer);
                foreach (RaycastHit2D a in hit)
                {
                    //if (a.transform.gameObject.layer == sc_player.MonsterLayer)
                    {
                        if (sc_player.targetmonster == a.transform)
                        {
                            direction = a.point - (Vector2)this.transform.position;
                            sc_player.SetDirection(direction);//공격하기 전에 마지막으로 방향을 정해준다.
                            Moving = false;
                            //sc_player.State = PLAYERSTATE.ATTACK;
                            sc_player.weapon.Attack();
                            //sc_player.weapon.Att
                            Debug.Log("플레이어 공격 시작");
                            MoveCor = null;
                            yield break;
                        }
                    }
                }

                //if(this.transform.position-sc_player.sc_targetmonster.transform.position<=)
            }



            direction = curtarget - this.transform.position;//현재의 목표방향을 구하고 
                                                            //일정 거리 이상 근접했으면 현재타겟을 다음으로 바꿔주고 다시 움직인다 만약 마지막 노드까지 왔으면 진짜 타겟으로 움직여주고 끝

            if (direction.magnitude <= 0.05)
            {
                this.transform.position = curtarget;

                if (this.transform.position == targetpos)
                {
                    Moving = false;
                    //sc_player.playeranimator.SetBool("Walking", false);
                    sc_player.State = PLAYERSTATE.IDLE;
                    MoveCor = null;
                    yield break;
                }

                if (MoveNodeList.Count - 1 <= Curindex)
                {
                    curtarget = targetpos;
                }

                else
                {

                    curtarget = MapManager.Instance.MyGetCellCenterWorld(MoveNodeList[Curindex].x, MoveNodeList[Curindex].y);
                    Curindex++;
                }
            }
            direction.Normalize();
            this.transform.position += direction * movespeed * Time.deltaTime;

            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        sc_player = GetComponent<Player>();
        
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawLine(startpos, targetpos);
        //Gizmos.color = Color.blue;
        //if (sc_player != null)
        //    Gizmos.DrawLine(transform.position, (Vector2)transform.position + sc_player.WAYS[(int)sc_player.Direction]);
    }
}
