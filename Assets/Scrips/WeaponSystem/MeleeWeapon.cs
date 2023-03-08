using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 무기들의 공격과 스킬은 컴포넌트 디자인 패턴을 이용하여 제작
//컴포넌트 패턴에서 receiver를 담당(행동을 하게되는 객체)
public class MeleeWeapon : Weapon
{
    //public enum MELEEKIND {AXE,MACE,LONGSWORD,MELEEMAX};
    protected MELEEKIND kind;

    //Animator animator;

    //유저가 클릭을 했을때 해당 위치에 몬스터가 있으면 몬스터를 타깃으로 이동한다. 몬스터를 타깃으로 이동할때는 계속 몬스터 위치를 모니터 한다. 일단 해당 위치로 이동한다.At
    //일단 움직이는데 타깃 몬스터가 어택범위 안에 들어오면 움직이는걸 멈추고 마지막으로 방향 지정해 준다음에 공격시작
    public virtual void MeleeAttackStart(Player player, baseMonster monster)
    {
        this.sc_player = player;
        this.sc_monster = monster;
        


    }
    public override void Attack()
    {
        //StartCoroutine("MeleeAttack");
    }

    public IEnumerator MeleeAttack()
    {
        while(true)
        {
            if (sc_player.State == PLAYERSTATE.ATTACK)
            {
                sc_player.playeranimator.SetTrigger("Attacktrigger");
                //sc_monster.CurHP -= damage;

                if(!sc_monster.BeAttacked(damage))
                {
                    //아이템 드랍
                    //GameObject dropitem = GameObject.Instantiate(GameManager.GetI.DropItem);
                    //dropitem.GetComponent<DropItem>().SetItemType(Random.Range(0, (int)Item.ITEMS.ItemMax));
                    //dropitem.transform.position = sc_player.targetmonster.transform.position;

                    //몬스터가 죽었을때

                    //캐릭터 경험치 얻음
                    sc_player.GetExp(100);

                    //공격멈춤
                    sc_player.TargetMonster = null;
                    sc_player.State = PLAYERSTATE.IDLE;

                    //해당 몬스터 파괴
                    sc_monster.Dead();
                }

                ////플레이어가 움직이거나 할때의 멈춤은 다른곳에서 처리
                //if (sc_monster.CurHP <= 0)
                //{
                //    //GameObject dropitem = GameObject.Instantiate(GameManager.GetI.DropItem);
                //    //dropitem.GetComponent<DropItem>().SetItemType(Random.Range(0, (int)Item.ITEMS.ItemMax));
                //    //dropitem.transform.position = sc_player.targetmonster.transform.position;

                //    sc_player.GetExp(100);
                //    //공격멈춤
                //    sc_player.TargetMonster = null;
                //    sc_player.State = PLAYERSTATE.IDLE;
                //    GameObject.Destroy(sc_monster.gameObject);
                //    yield break;
                //}
            }
            else
            {
                yield break;
            }
            yield return new WaitForSeconds(cooltime);
        }
    }


    void Start()
    {
        weaponkind = WEAPONKIND.MELEE;
        Debug.Log("Melee Start");
    }

}
