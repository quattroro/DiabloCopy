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

    //�������� ������ ������ �ش� ���� ���̿� ��ֹ��� �ִ��� �˻��ϰ�
    //��ֹ��� ������ �׳� ���� ��ֹ��� ������ A-star ���� ����
    //�������� ������ ������ Astar�� �̿��� ���迭�� �޾ƿ��� ���� �߽���ǥ�� ���� ��ĭ�� �̵��Ѵ�.
    public void StartMove(Vector3 start, Vector3 target)
    {
        //�ϴ� ���ݹ��� �ȿ� ���Ͱ� �ִ��� Ȯ�� �Ѵ�. ���Ͱ� ������ �������� �ʰ� ������ ����
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
                        sc_player.SetDirection(direction);//�����ϱ� ���� ���������� ������ �����ش�.
                        Moving = false;
                        sc_player.weapon.Attack();
                        Debug.Log("�÷��̾� ���� ����");
                        return;
                    }
                }
                

            }
        }


        startpos = start;
        targetpos = target;

        if(!CheckWall(startpos, targetpos))
        {
            //Debug.Log("�� ����");
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

        //���������� �������� Ray�� ���� �߰��� ���� �ִ��� Ȯ���Ѵ�.
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

    //õ��° ����� ��ġ�� �����̰� �� �������� ���� ��� �������� ���� ��� �׷� ������ ������ ����� ��ġ���� ���� ó�� �Էµ� Ÿ�ٳ��� �̵�
    public IEnumerator CMove()
    {
        while(true)
        {
            if (!Moving)
            {
                MoveCor = null;
                yield break;
            }

            //���͸� ��ǥ�� �����϶��� �����϶����� ���ݹ����� ���Ͱ� ���Դ��� Ȯ���Ѵ�.
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
                            sc_player.SetDirection(direction);//�����ϱ� ���� ���������� ������ �����ش�.
                            Moving = false;
                            //sc_player.State = PLAYERSTATE.ATTACK;
                            sc_player.weapon.Attack();
                            //sc_player.weapon.Att
                            Debug.Log("�÷��̾� ���� ����");
                            MoveCor = null;
                            yield break;
                        }
                    }
                }

                //if(this.transform.position-sc_player.sc_targetmonster.transform.position<=)
            }



            direction = curtarget - this.transform.position;//������ ��ǥ������ ���ϰ� 
                                                            //���� �Ÿ� �̻� ���������� ����Ÿ���� �������� �ٲ��ְ� �ٽ� �����δ� ���� ������ ������ ������ ��¥ Ÿ������ �������ְ� ��

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
