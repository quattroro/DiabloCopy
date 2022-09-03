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

    //�������� ������ ������ Astar�� �̿��� ���迭�� �޾ƿ��� ���� �߽���ǥ�� ���� ��ĭ�� �̵��Ѵ�.
    public void StartMove(Vector3 start, Vector3 target)
    {
        if (sc_player.targetmonster != null)
        {
            //StartMove(this.transform.position, sc_player.sc_targetmonster.transform.position);

            RaycastHit2D[] hit = Physics2D.CircleCastAll(sc_player.AttackRange.transform.position, sc_player.AttackRange.radius, new Vector2(0, 0), 0);
            foreach (RaycastHit2D a in hit)
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
                    return;
                }

            }
        }
        startpos = start;
        targetpos = target;

        MoveNodeList = MoveAstar.GetI.PathFinding(start, target);
        Curindex = 1;
        curtarget = MapManager.GetI.MyGetCellCenterWorld(MoveNodeList[Curindex].x, MoveNodeList[Curindex].y);
        
        Curindex++;
        Moving = true;
        //sc_player.playeranimator.SetBool("Walking", true);
        sc_player.State = PLAYERSTATE.WALKING;
        direction = target - start;
        sc_player.SetDirection(direction.normalized);
    }    
    
    //õ��° ����� ��ġ�� �����̰� �� �������� ���� ��� �������� ���� ��� �׷� ������ ������ ����� ��ġ���� ���� ó�� �Էµ� Ÿ�ٳ��� �̵�
    public void CMove()
    {
        if (Moving)
        {
            //���͸� ��ǥ�� �����϶��� �����϶����� ���ݹ����� ���Ͱ� ���Դ��� Ȯ���Ѵ�.
            if(sc_player.targetmonster!=null)
            {
                //StartMove(this.transform.position, sc_player.sc_targetmonster.transform.position);
                RaycastHit2D[] hit = Physics2D.CircleCastAll(sc_player.AttackRange.transform.position, sc_player.AttackRange.radius, new Vector2(0, 0), 0);
                foreach(RaycastHit2D a in hit)
                {
                    if(sc_player.targetmonster==a.transform)
                    {
                        direction = a.point - (Vector2)this.transform.position;
                        sc_player.SetDirection(direction);//�����ϱ� ���� ���������� ������ �����ش�.
                        Moving = false;
                        //sc_player.State = PLAYERSTATE.ATTACK;
                        sc_player.weapon.Attack();
                        //sc_player.weapon.Att
                        Debug.Log("�÷��̾� ���� ����");
                        return;
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
                    return;
                }
                if (MoveNodeList.Count-1 <= Curindex)
                {
                    curtarget = targetpos;
                }
                else
                {
                    
                    curtarget = MapManager.GetI.MyGetCellCenterWorld(MoveNodeList[Curindex].x, MoveNodeList[Curindex].y);
                    Curindex++;
                }
            }
            direction.Normalize();
            this.transform.position += direction * movespeed * Time.deltaTime;
        }
        //if (Time.time>=curtime)
        //{
        //    curtime = Time.time + Timeval;
            
        //}
        
    }



    IEnumerator Moveroutine()
    {



        yield return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        sc_player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        CMove();
    }
}
