using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MonsterAttack : MonoBehaviour
{

    ZombieFSM zombieFSM;
    public baseMonster sc_monster;
    public Player sc_player;

    

    public AnimationEventSystem eventsystem;

    private float LastAttackTime;

    public bool NowAttacking = false;

    AttackInfo NowAttackInfo;

    public Animator animator;

    IEnumerator attackcor = null;

    //������ ���ͺ��� �޶�����.
    //[Header("���� �ɼǵ�")]
    //public AttackInfo[] attackInfo;

    //public AttackInfo curAttack;
    
    //public class AttackInfo
    //{
    //    public AttackType AttackType;
    //    public float AttackName;
    //    public float AttackTime;
    //}

    //���� ������ ���°� ���� ���¸� 1�ʿ� �ѹ� ������ �Ѵ�.
    //�÷��̾�� �������� �޾Ƽ� ������ flase�� ���� �׷��� ������ ���� 
    //IEnumerator Attack()
    //{
    //    while(true)
    //    {
    //        if (sc_monster.State != MONSTERSTATE.ATTACK)
    //        {
    //            yield break;
    //        }
    //        if (sc_monster.State == MONSTERSTATE.ATTACK)
    //        {
    //            Debug.Log("���� ����");
    //            if (!sc_player.GetDemage(sc_monster.Damage))
    //            {
    //                sc_monster.State = MONSTERSTATE.IDLE;
    //                yield break;
    //            }
    //        }
    //        yield return new WaitForSeconds(AttackTime);
    //    }    
    //}

    public void StartAttack(Player player, AttackInfo attackinfo)
    {
        Debug.Log("���� ���ݽ���");
        if (sc_monster.FSM.GetCurstate != MONSTERSTATE.Attack.ToString())
        {
            //sc_monster.State = MONSTERSTATE.ATTACK;
            NowAttackInfo = attackinfo;
            zombieFSM.ChangeState(zombieFSM.attackState);

            //sc_monster = monster;
            sc_player = player;

            attackcor = AttackRoutine();

            StartCoroutine(attackcor);

            //StartCoroutine("Attack");
            //Attack2();
        }

        //if (sc_monster.State!=MONSTERSTATE.ATTACK)
        //{
        //    sc_monster.State = MONSTERSTATE.ATTACK;
        //    zombieFSM.ChangeState(zombieFSM.attackState);

        //    //sc_monster = monster;
        //    sc_player = player;
        //    //StartCoroutine("Attack");
        //    //Attack2();
        //}
        
    }


    public void Attack(string val)
    {

        //if (sc_monster.State != MONSTERSTATE.ATTACK)
        //{
        //    return;
        //}

        if (sc_monster.FSM.GetCurstate != MONSTERSTATE.Attack.ToString())
        {
            return;
        }

        Debug.Log("���� ����");


        if (!sc_player.GetDemage(sc_monster.Damage + NowAttackInfo.attackDamage))
        {
            //sc_monster.State = MONSTERSTATE.IDLE;
            return;
        }

        //LastAttackTime = Time.time;
    }

    public void AttackEnd(string val)
    {

    }

    void Start()
    {
        eventsystem = GetComponentInChildren<AnimationEventSystem>();
        for(int i=1;i<=8;i++)
        {
            eventsystem.AddEvent(new KeyValuePair<string, AnimationEventSystem.beginCallback>(null, null), 0.0f,
                                 new KeyValuePair<string, AnimationEventSystem.midCallback>($"ZombieAttack{i}", Attack), 0.70f,
                                 new KeyValuePair<string, AnimationEventSystem.endCallback>($"ZombieAttack{i}", AttackEnd), 1.00f);
        }
        zombieFSM = GetComponent<ZombieFSM>();
        sc_monster = GetComponent<baseMonster>();
        animator = GetComponentInChildren<Animator>();
    }


    IEnumerator AttackRoutine()
    {
        while(true)
        {
            if (sc_monster.FSM.GetCurstate != MONSTERSTATE.Attack.ToString())
            {
                Debug.Log("���� �ڷ�ƾ ����");
                yield break;
            }

            if(Time.time - LastAttackTime >=NowAttackInfo.attackInterval)
            {
                LastAttackTime = Time.time;
                animator.SetTrigger("Attacktrigger");
                Debug.Log("���� �ִϸ��̼� ����");
            }

            yield return null;
        }
        
    }

    //private void Update()
    //{
    //    //if(sc_monster.)
    //    if (Time.time - LastAttackTime >= NowAttackInfo.attackInterval)
    //    {
    //        LastAttackTime = Time.time;

    //        animator.SetTrigger("Attacktrigger");
    //    }
    //}
}
