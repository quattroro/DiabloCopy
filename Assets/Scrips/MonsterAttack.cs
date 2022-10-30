using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{


    public baseMonster sc_monster;
    public Player sc_player;

    public float AttackTime;
    private float LastAttackTime;

    public AnimationEventSystem eventsystem;


    //현재 몬스터의 상태가 공격 상태면 1초에 한번 공격을 한다.
    //플레이어는 데미지를 받아서 죽으면 flase를 리턴 그러면 공격을 끝냄 
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
    //            Debug.Log("몬스터 공격");
    //            if (!sc_player.GetDemage(sc_monster.Damage))
    //            {
    //                sc_monster.State = MONSTERSTATE.IDLE;
    //                yield break;
    //            }
    //        }
    //        yield return new WaitForSeconds(AttackTime);
    //    }    
    //}

    public void StartAttack(Player player, baseMonster monster)
    {
        Debug.Log("몬스터 공격시작");
        if (monster.State!=MONSTERSTATE.ATTACK)
        {
            monster.State = MONSTERSTATE.ATTACK;
            sc_monster = monster;
            sc_player = player;
            //StartCoroutine("Attack");
            //Attack2();
        }
        
    }


    public void Attack(string val)
    {
        if (sc_monster.State != MONSTERSTATE.ATTACK)
        {
            return;
        }
        Debug.Log("몬스터 공격");
        if (!sc_player.GetDemage(sc_monster.Damage))
        {
            //sc_monster.State = MONSTERSTATE.IDLE;
            return;
        }
    }



    void Start()
    {
        eventsystem = GetComponentInChildren<AnimationEventSystem>();
        for(int i=1;i<=8;i++)
        {
            eventsystem.AddEvent(new KeyValuePair<string, AnimationEventSystem.beginCallback>(null, null),
                                 new KeyValuePair<string, AnimationEventSystem.midCallback>(null, null),
                                 new KeyValuePair<string, AnimationEventSystem.endCallback>($"ZombieAttack{i}", Attack));
        }
    }

}
